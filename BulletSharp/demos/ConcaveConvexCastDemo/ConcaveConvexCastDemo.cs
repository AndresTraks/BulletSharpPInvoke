using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ConcaveConvexCastDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<ConcaveConvexCastDemo>();
        }
    }

    internal sealed class ConcaveConvexCastDemo : IDemoConfiguration, IUpdateReceiver
    {
        private double _groundOffset = 0;

        public ISimulation CreateSimulation(Demo demo)
        {
            _groundOffset = 0;
            demo.FreeLook.Eye = new Vector3(0, 15, 60);
            demo.FreeLook.Target = new Vector3(-5, 5, 0);
            demo.IsDebugDrawEnabled = true;
            demo.DebugDrawMode = DebugDrawModes.None;
            demo.DemoText = "G - Toggle animation";
            demo.Graphics.WindowTitle = "BulletSharp - Concave Convexcast Demo";
            return new ConcaveConvexCastDemoSimulation();
        }

        public void Update(Demo demo)
        {
            var simulation = demo.Simulation as ConcaveConvexCastDemoSimulation;
            if (demo.Input.KeysPressed.Contains(Keys.G))
            {
                simulation.IsGroundAnimated = !simulation.IsGroundAnimated;
            }

            if (simulation.IsGroundAnimated)
            {
                _groundOffset += demo.FrameDelta;
                simulation.SetGroundAnimationOffset(_groundOffset);

                demo.Graphics.MeshFactory.RemoveShape(simulation.GroundShape);
            }

            if (demo.IsDebugDrawEnabled)
            {
                simulation.Convexcast(demo.FrameDelta);
            }
        }
    }

    internal sealed class ConcaveConvexCastDemoSimulation : ISimulation
    {
        private const double TriangleSize = 8.0f;
        private const int NumVertsX = 30;
        private const int NumVertsY = 30;
        private const double WaveHeight = 3.0f;
        private const int NumDynamicBoxesX = 30;
        private const int NumDynamicBoxesY = 30;

        private bool _animatedMesh = true;
        private Stream _vertexStream;
        private BinaryWriter _vertexWriter;

        private Vector3 _worldMin = new Vector3(-1000, -1000, -1000);
        private Vector3 _worldMax = new Vector3(1000, 1000, 1000);

        private TriangleIndexVertexArray _indexVertexArrays;
        private ConvexcastBatch _convexcastBatch;
        private RigidBody _groundObject;
        private ClosestConvexResultCallback _callback;

        public ConcaveConvexCastDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            Broadphase = new AxisSweep3(_worldMin, _worldMax);

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);
            World.SolverInfo.SplitImpulse = 1;

            _convexcastBatch = new ConvexcastBatch(40.0f, 0.0f, -10.0f, 80.0f);
            _callback = new ClosestConvexResultCallback();

            CreateGround();
            CreateBoxes();
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public BvhTriangleMeshShape GroundShape { get; private set; }

        public bool IsGroundAnimated
        {
            get { return _animatedMesh; }
            set
            {
                _animatedMesh = value;
                if (value)
                {
                    _groundObject.CollisionFlags |= CollisionFlags.KinematicObject;
                    _groundObject.ActivationState = ActivationState.DisableDeactivation;
                }
                else
                {
                    _groundObject.CollisionFlags &= ~CollisionFlags.KinematicObject;
                    _groundObject.ActivationState = ActivationState.ActiveTag;
                }
            }
        }

        public void SetGroundAnimationOffset(double offset)
        {
            SetVertexPositions(WaveHeight, offset);

            GroundShape.RefitTreeRef(ref _worldMin, ref _worldMax);

            // Clear all contact points involving mesh proxy.
            // Note: this is a slow/unoptimized operation.
            Broadphase.OverlappingPairCache.CleanProxyFromPairs(_groundObject.BroadphaseHandle, Dispatcher);
        }

        public void Convexcast(double frameDelta)
        {
            _convexcastBatch.Move(frameDelta);
            _convexcastBatch.Cast(World, _callback, frameDelta);
            _convexcastBatch.Draw(World.DebugDrawer);
        }

        public void Dispose()
        {
            _callback.Dispose();
            _indexVertexArrays.IndexedMeshArray[0].Dispose();
            _indexVertexArrays.Dispose();
            if (_vertexWriter != null)
            {
                _vertexWriter.Dispose();
                _vertexWriter = null;
            }

            this.StandardCleanup();
        }

        private void CreateGround()
        {
            const int totalVerts = NumVertsX * NumVertsY;
            const int totalTriangles = 2 * (NumVertsX - 1) * (NumVertsY - 1);
            const int triangleIndexStride = 3 * sizeof(int);
            const int vertexStride = Vector3.SizeInBytes;

            var mesh = new IndexedMesh();
            mesh.Allocate(totalTriangles, totalVerts, triangleIndexStride, vertexStride);

            var indicesStream = mesh.GetTriangleStream();
            using (var indices = new BinaryWriter(indicesStream))
            {
                for (int x = 0; x < NumVertsX - 1; x++)
                {
                    for (int y = 0; y < NumVertsY - 1; y++)
                    {
                        int row1Index = x * NumVertsX + y;
                        int row2Index = row1Index + NumVertsX;
                        indices.Write(row1Index);
                        indices.Write(row1Index + 1);
                        indices.Write(row2Index + 1);

                        indices.Write(row1Index);
                        indices.Write(row2Index + 1);
                        indices.Write(row2Index);
                    }
                }
            }

            _indexVertexArrays = new TriangleIndexVertexArray();
            _indexVertexArrays.AddIndexedMesh(mesh);

            SetVertexPositions(WaveHeight, 0.0f);

            const bool useQuantizedAabbCompression = true;
            GroundShape = new BvhTriangleMeshShape(_indexVertexArrays, useQuantizedAabbCompression);

            _groundObject = PhysicsHelper.CreateStaticBody(Matrix.Identity, GroundShape, World);
            _groundObject.CollisionFlags |= CollisionFlags.StaticObject;
            _groundObject.UserObject = "Ground";
        }

        private void CreateBoxes()
        {
            var shape = new BoxShape(1);
            //var shape = new CapsuleShape(0.5f, 2.0f);
            //var shape = new SphereShape(1.0f);

            for (int j = 0; j < NumDynamicBoxesX; j++)
            {
                for (int i = 0; i < NumDynamicBoxesY; i++)
                {
                    Matrix startTransform = Matrix.Translation(5 * (i - NumDynamicBoxesX / 2), 10, 5 * (j - NumDynamicBoxesY / 2));
                    PhysicsHelper.CreateBody(1.0f, startTransform, shape, World);
                }
            }
        }

        private void SetVertexPositions(double waveHeight, double offset)
        {
            if (_vertexWriter == null)
            {
                _vertexStream = _indexVertexArrays.GetVertexStream();
                _vertexWriter = new BinaryWriter(_vertexStream);
            }
            _vertexStream.Position = 0;
            for (int i = 0; i < NumVertsX; i++)
            {
                for (int j = 0; j < NumVertsY; j++)
                {
                    _vertexWriter.Write((i - NumVertsX * 0.5f) * TriangleSize);
                    _vertexWriter.Write(waveHeight * (double)Math.Sin(i + offset) * (double)Math.Cos(j + offset));
                    _vertexWriter.Write((j - NumVertsY * 0.5f) * TriangleSize);
                }
            }
        }
    }

    // Scrolls back and forth over terrain
    internal sealed class ConvexcastBatch
    {
        private const int NumRays = 100;
        private Ray[] _rays = new Ray[NumRays];

        private int _frameCount;
        private double _time;
        private double _timeMin = double.MaxValue;
        private double _timeMax;
        private double _timeTotal;
        private int _sampleCount;

        private double _dx = 10;
        private double _minX = -40;
        private double _maxX = 20;
        private double _sign = 1;

        private Vector3 _boxBoundMin, _boxBoundMax;
        private BoxShape _boxShape;

        private const double NormalScale = 10.0f; // easier to see if this is big

        private Matrix _fromRotation = Matrix.Identity; //Matrix.RotationX(0.7f);
        private Matrix _toRotation = Matrix.RotationX(0.7f);

        private static Vector3 _green = new Vector3(0.0f, 1.0f, 0.0f);
        private static Vector3 _white = new Vector3(1.0f, 1.0f, 1.0f);
        private static Vector3 _cyan = new Vector3(0.0f, 1.0f, 1.0f);

        public ConvexcastBatch(double rayLength, double z, double minY, double maxY)
        {
            _boxBoundMax = new Vector3(1.0f, 1.0f, 1.0f);
            _boxBoundMin = -_boxBoundMax;
            _boxShape = new BoxShape(_boxBoundMax);

            const double alpha = 4 * (double)Math.PI / NumRays;
            for (int i = 0; i < NumRays; i++)
            {
                Matrix transform = Matrix.RotationY(alpha * i);
                var direction = Vector3.TransformCoordinate(Vector3.UnitX, transform);

                _rays[i] = new Ray
                {
                    Source = new Vector3(_minX, maxY, z),
                    Destination = new Vector3(
                        _minX + direction.X * rayLength,
                        minY,
                        z + direction.Z * rayLength),
                    Normal = new Vector3(1, 0, 0)
                };
            }
        }

        public void Move(double frameDelta)
        {
            if (frameDelta > 1.0f / 60.0f)
                frameDelta = 1.0f / 60.0f;

            double move = _sign * _dx * frameDelta;
            foreach (var ray in _rays)
            {
                ray.MoveX(move);
            }

            if (_rays[0].Source.X < _minX)
                _sign = 1.0f;
            else if (_rays[0].Source.X > _maxX)
                _sign = -1.0f;
        }

        public void Cast(CollisionWorld cw, ClosestConvexResultCallback callback, double frameDelta)
        {
            foreach (var ray in _rays)
            {
                callback.ClosestHitFraction = 1.0f;
                callback.ConvexFromWorld = ray.Source;
                callback.ConvexToWorld = ray.Destination;

                Matrix from = _fromRotation * Matrix.Translation(ray.Source);
                Matrix to = _toRotation * Matrix.Translation(ray.Destination);
                cw.ConvexSweepTestRef(_boxShape, ref from, ref to, callback);
                if (callback.HasHit)
                {
                    ray.HitPoint = callback.HitPointWorld;
                    Vector3.Lerp(ref ray.Source, ref ray.Destination, callback.ClosestHitFraction, out ray.HitCenterOfMass);
                    ray.HitFraction = callback.ClosestHitFraction;
                    ray.Normal = callback.HitNormalWorld;
                    ray.Normal.Normalize();
                }
                else
                {
                    ray.HitCenterOfMass = ray.Destination;
                    ray.HitPoint = ray.Destination;
                    ray.HitFraction = 1.0f;
                    ray.Normal = new Vector3(1.0f, 0.0f, 0.0f);
                }
            }

            _time += frameDelta;
            _frameCount++;
            if (_frameCount > 50)
            {
                if (_time < _timeMin) _timeMin = _time;
                if (_time > _timeMax) _timeMax = _time;
                _timeTotal += _time;
                _sampleCount++;
                PrintStats();
                _time = 0;
                _frameCount = 0;
            }
        }

        public void Draw(DebugDraw drawer)
        {
            foreach (var ray in _rays)
            {
                drawer.DrawLine(ref ray.Source, ref ray.HitCenterOfMass, ref _green);

                Vector3 to = ray.HitPoint + NormalScale * ray.Normal;
                drawer.DrawLine(ref ray.HitPoint, ref to, ref _white);

                Matrix fromTransform = _fromRotation * Matrix.Translation(ray.Source);
                Matrix toTransform = _toRotation * Matrix.Translation(ray.Destination);
                Vector3 linVel, angVel;
                TransformUtil.CalculateVelocity(ref fromTransform, ref toTransform, 1.0f, out linVel, out angVel);
                Matrix transform;
                TransformUtil.IntegrateTransform(ref fromTransform, ref linVel, ref angVel, ray.HitFraction, out transform);
                drawer.DrawBox(ref _boxBoundMin, ref _boxBoundMax, ref transform, ref _cyan);
            }
        }

        private void PrintStats()
        {
            double timeMean = _timeTotal / _sampleCount;
            Console.WriteLine("{0} rays in {1} s, min {2}, max {3}, mean {4}",
                    NumRays * _frameCount,
                    _time.ToString("0.000", CultureInfo.InvariantCulture),
                    _timeMin.ToString("0.000", CultureInfo.InvariantCulture),
                    _timeMax.ToString("0.000", CultureInfo.InvariantCulture),
                    timeMean.ToString("0.000", CultureInfo.InvariantCulture));
        }
    }

    internal sealed class Ray
    {
        public Vector3 Source;
        public Vector3 Destination;
        public Vector3 HitCenterOfMass;
        public Vector3 HitPoint;
        public double HitFraction;
        public Vector3 Normal;

        public void MoveX(double move)
        {
            Source.X += move;
            Destination.X += move;
        }
    }
}
