using BulletSharp;
using DemoFramework;
using System;
using System.Globalization;
using System.IO;
using System.Numerics;
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
        private float _groundOffset = 0;

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
        private const float TriangleSize = 8.0f;
        private const int NumVertsX = 30;
        private const int NumVertsY = 30;
        private const float WaveHeight = 3.0f;
        private const int NumDynamicBoxesX = 30;
        private const int NumDynamicBoxesY = 30;

        private bool _animatedMesh = true;
        private Stream _vertexStream;
        private BinaryWriter _vertexWriter;

        private Vector3 _worldMin = new Vector3(-1000, -1000, -1000);
        private Vector3 _worldMax = new Vector3(1000, 1000, 1000);

        private TriangleIndexVertexArray _indexVertexArrays;
        private IndexedMesh _groundMesh;
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

        public void SetGroundAnimationOffset(float offset)
        {
            SetVertexPositions(WaveHeight, offset);

            GroundShape.RefitTreeRef(ref _worldMin, ref _worldMax);

            // Clear all contact points involving mesh proxy.
            // Note: this is a slow/unoptimized operation.
            Broadphase.OverlappingPairCache.CleanProxyFromPairs(_groundObject.BroadphaseHandle, Dispatcher);
        }

        public void Convexcast(float frameDelta)
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
            _groundMesh.Dispose();
            if (_vertexWriter != null)
            {
                _vertexWriter.Dispose();
                _vertexWriter = null;
            }
            _convexcastBatch.Dispose();

            this.StandardCleanup();
        }

        private void CreateGround()
        {
            const int totalVerts = NumVertsX * NumVertsY;
            const int totalTriangles = 2 * (NumVertsX - 1) * (NumVertsY - 1);
            const int triangleIndexStride = 3 * sizeof(int);
            const int vertexStride = 3 * sizeof(float);

            _groundMesh = new IndexedMesh();
            _groundMesh.Allocate(totalTriangles, totalVerts, triangleIndexStride, vertexStride);

            var indicesStream = _groundMesh.GetTriangleStream();
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
            _indexVertexArrays.AddIndexedMesh(_groundMesh);

            SetVertexPositions(WaveHeight, 0.0f);

            const bool useQuantizedAabbCompression = true;
            GroundShape = new BvhTriangleMeshShape(_indexVertexArrays, useQuantizedAabbCompression);

            _groundObject = PhysicsHelper.CreateStaticBody(Matrix4x4.Identity, GroundShape, World);
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
                    Matrix4x4 startTransform = Matrix4x4.CreateTranslation(5 * (i - NumDynamicBoxesX / 2), 10, 5 * (j - NumDynamicBoxesY / 2));
                    PhysicsHelper.CreateBody(1.0f, startTransform, shape, World);
                }
            }
        }

        private void SetVertexPositions(float waveHeight, float offset)
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
                    _vertexWriter.Write(waveHeight * (float)Math.Sin(i + offset) * (float)Math.Cos(j + offset));
                    _vertexWriter.Write((j - NumVertsY * 0.5f) * TriangleSize);
                }
            }
        }
    }

    // Scrolls back and forth over terrain
    internal sealed class ConvexcastBatch : IDisposable
    {
        private const int NumRays = 100;
        private Ray[] _rays = new Ray[NumRays];

        private int _frameCount;
        private float _time;
        private float _timeMin = float.MaxValue;
        private float _timeMax;
        private float _timeTotal;
        private int _sampleCount;

        private float _dx = 10;
        private float _minX = -40;
        private float _maxX = 20;
        private float _sign = 1;

        private Vector3 _boxBoundMin, _boxBoundMax;
        private BoxShape _boxShape;

        private const float NormalScale = 10.0f; // easier to see if this is big

        private Matrix4x4 _fromRotation = Matrix4x4.Identity; //Matrix.RotationX(0.7f);
        private Matrix4x4 _toRotation = Matrix4x4.CreateRotationX(0.7f);

        private static Vector3 _green = new Vector3(0.0f, 1.0f, 0.0f);
        private static Vector3 _white = new Vector3(1.0f, 1.0f, 1.0f);
        private static Vector3 _cyan = new Vector3(0.0f, 1.0f, 1.0f);

        public ConvexcastBatch(float rayLength, float z, float minY, float maxY)
        {
            _boxBoundMax = new Vector3(1.0f, 1.0f, 1.0f);
            _boxBoundMin = -_boxBoundMax;
            _boxShape = new BoxShape(_boxBoundMax);

            const float alpha = 4 * (float)Math.PI / NumRays;
            for (int i = 0; i < NumRays; i++)
            {
                Matrix4x4 transform = Matrix4x4.CreateRotationY(alpha * i);
                var direction = Vector3.Transform(Vector3.UnitX, transform);

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

        public void Move(float frameDelta)
        {
            if (frameDelta > 1.0f / 60.0f)
                frameDelta = 1.0f / 60.0f;

            float move = _sign * _dx * frameDelta;
            foreach (var ray in _rays)
            {
                ray.MoveX(move);
            }

            if (_rays[0].Source.X < _minX)
                _sign = 1.0f;
            else if (_rays[0].Source.X > _maxX)
                _sign = -1.0f;
        }

        public void Cast(CollisionWorld cw, ClosestConvexResultCallback callback, float frameDelta)
        {
            foreach (var ray in _rays)
            {
                callback.ClosestHitFraction = 1.0f;
                callback.ConvexFromWorld = ray.Source;
                callback.ConvexToWorld = ray.Destination;

                Matrix4x4 from = _fromRotation * Matrix4x4.CreateTranslation(ray.Source);
                Matrix4x4 to = _toRotation * Matrix4x4.CreateTranslation(ray.Destination);
                cw.ConvexSweepTestRef(_boxShape, ref from, ref to, callback);
                if (callback.HasHit)
                {
                    ray.HitPoint = callback.HitPointWorld;
                    ray.HitCenterOfMass = Vector3.Lerp(ray.Source, ray.Destination, callback.ClosestHitFraction);
                    ray.HitFraction = callback.ClosestHitFraction;
                    ray.Normal = Vector3.Normalize(callback.HitNormalWorld);
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

                Matrix4x4 fromTransform = _fromRotation * Matrix4x4.CreateTranslation(ray.Source);
                Matrix4x4 toTransform = _toRotation * Matrix4x4.CreateTranslation(ray.Destination);
                Vector3 linVel, angVel;
                TransformUtil.CalculateVelocity(ref fromTransform, ref toTransform, 1.0f, out linVel, out angVel);
                Matrix4x4 transform;
                TransformUtil.IntegrateTransform(ref fromTransform, ref linVel, ref angVel, ray.HitFraction, out transform);
                drawer.DrawBox(ref _boxBoundMin, ref _boxBoundMax, ref transform, ref _cyan);
            }
        }

        public void Dispose()
        {
            _boxShape.Dispose();
        }

        private void PrintStats()
        {
            float timeMean = _timeTotal / _sampleCount;
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
        public float HitFraction;
        public Vector3 Normal;

        public void MoveX(float move)
        {
            Source.X += move;
            Destination.X += move;
        }
    }
}
