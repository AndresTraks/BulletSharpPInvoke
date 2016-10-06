using BulletSharp.Math;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BulletSharp;
using DemoFramework;

namespace ConcaveConvexCastDemo
{
    class ConcaveConvexCastDemo : Demo
    {
        Vector3 eye = new Vector3(0, 15, 60);
        Vector3 target = new Vector3(-5, 5, 0);

        const DebugDrawModes debugMode = DebugDrawModes.None;

        const float TriangleSize = 8.0f;
        const int NumVertsX = 30;
        const int NumVertsY = 30;
        const float WaveHeight = 5.0f;
        static float groundOffset = 0.0f;
        bool animatedMesh = true;
        const int NumDynamicBoxesX = 30;
        const int NumDynamicBoxesY = 30;

        Vector3 worldMin = new Vector3(-1000, -1000, -1000);
        Vector3 worldMax = new Vector3(1000, 1000, 1000);

        TriangleIndexVertexArray indexVertexArrays;
        BvhTriangleMeshShape groundShape;
        ConvexcastBatch convexcastBatch;
        RigidBody staticBody;
        ClosestConvexResultCallback callback;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Concave Convexcast Demo");

            IsDebugDrawEnabled = true;
            DebugDrawMode = debugMode;
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new AxisSweep3(worldMin, worldMax);
            Solver = new SequentialImpulseConstraintSolver();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World.SolverInfo.SplitImpulse = 1;
            World.Gravity = new Vector3(0, -10, 0);

            convexcastBatch = new ConvexcastBatch(40.0f, 0.0f, -10.0f, 80.0f);
            callback = new ClosestConvexResultCallback();

            CreateGround();
            CreateBoxes();
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

            indexVertexArrays = new TriangleIndexVertexArray();
            indexVertexArrays.AddIndexedMesh(mesh);

            SetVertexPositions(WaveHeight, 0.0f);

            const bool useQuantizedAabbCompression = true;
            groundShape = new BvhTriangleMeshShape(indexVertexArrays, useQuantizedAabbCompression);
            CollisionShapes.Add(groundShape);

            staticBody = LocalCreateRigidBody(0.0f, Matrix.Identity, groundShape);
            staticBody.CollisionFlags |= CollisionFlags.StaticObject;
            staticBody.UserObject = "Ground";
        }

        private void CreateBoxes()
        {
            var colShape = new BoxShape(1);
            //var colShape = new CapsuleShape(0.5f, 2.0f);//boxShape = new SphereShape(1.0f);
            CollisionShapes.Add(colShape);

            for (int j = 0; j < NumDynamicBoxesX; j++)
            {
                for (int i = 0; i < NumDynamicBoxesY; i++)
                {
                    Matrix startTransform = Matrix.Translation(5 * (i - NumDynamicBoxesX / 2), 10, 5 * (j - NumDynamicBoxesY / 2));
                    LocalCreateRigidBody(1.0f, startTransform, colShape);
                }
            }
        }

        public override void OnUpdate()
        {
            if (animatedMesh)
            {
                groundOffset += FrameDelta;
                SetVertexPositions(WaveHeight, groundOffset);
                Graphics.MeshFactory.RemoveShape(groundShape);

                groundShape.RefitTreeRef(ref worldMin, ref worldMax);

                //clear all contact points involving mesh proxy. Note: this is a slow/unoptimized operation.
                Broadphase.OverlappingPairCache.CleanProxyFromPairs(staticBody.BroadphaseHandle, Dispatcher);
            }

            convexcastBatch.Move(FrameDelta);
            convexcastBatch.Cast(World, callback, FrameDelta);
            if (IsDebugDrawEnabled)
            {
                convexcastBatch.Draw(World.DebugDrawer);
            }

            base.OnUpdate();
        }

        void SetVertexPositions(float waveheight, float offset)
        {
            var vertexStream = indexVertexArrays.GetVertexStream();
            using (var vertexWriter = new BinaryWriter(vertexStream))
            {
                for (int i = 0; i < NumVertsX; i++)
                {
                    for (int j = 0; j < NumVertsY; j++)
                    {
                        vertexWriter.Write((i - NumVertsX * 0.5f) * TriangleSize);
                        vertexWriter.Write(waveheight * (float)Math.Sin(i + offset) * (float)Math.Cos(j + offset));
                        vertexWriter.Write((j - NumVertsY * 0.5f) * TriangleSize);
                    }
                }
            }
        }

        public override void OnHandleInput()
        {
            if (Input.KeysPressed.Contains(Keys.G))
            {
                animatedMesh = !animatedMesh;
                if (animatedMesh)
                {
                    staticBody.CollisionFlags |= CollisionFlags.KinematicObject;
                    staticBody.ActivationState = ActivationState.DisableDeactivation;
                }
                else
                {
                    staticBody.CollisionFlags &= ~CollisionFlags.KinematicObject;
                    staticBody.ActivationState = ActivationState.ActiveTag;
                }
            }
            base.OnHandleInput();
        }

        public override void ExitPhysics()
        {
            callback.Dispose();

            base.ExitPhysics();
        }
    }

    // Scrolls back and forth over terrain
    class ConvexcastBatch
    {
        const int NumRays = 100;
        Ray[] _rays = new Ray[NumRays];

        int _frameCount;
        float _time;
        float _timeMin = float.MaxValue;
        float _timeMax;
        float _timeTotal;
        int _sampleCount;

        float _dx = 10;
        float _minX = -40;
        float _maxX = 20;
        float _sign = 1;

        Vector3 _boxBoundMin, _boxBoundMax;
        BoxShape _boxShape;

        const float NormalScale = 10.0f; // easier to see if this is big

        Matrix _fromRotation = Matrix.Identity; //Matrix.RotationX(0.7f);
        Matrix _toRotation = Matrix.RotationX(0.7f);

        public ConvexcastBatch(float rayLength, float z, float minY, float maxY)
        {
            _boxBoundMax = new Vector3(1.0f, 1.0f, 1.0f);
            _boxBoundMin = -_boxBoundMax;
            _boxShape = new BoxShape(_boxBoundMax);

            const float alpha = 4 * (float)Math.PI / NumRays;
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

        static Vector3 _green = new Vector3(0.0f, 1.0f, 0.0f);
        static Vector3 _white = new Vector3(1.0f, 1.0f, 1.0f);
        static Vector3 _cyan = new Vector3(0.0f, 1.0f, 1.0f);

        public void Draw(IDebugDraw drawer)
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
    }

    class Ray
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

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new ConcaveConvexCastDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
