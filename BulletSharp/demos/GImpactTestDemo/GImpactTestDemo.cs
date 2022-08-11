using BulletSharp;
using DemoFramework;
using DemoFramework.Meshes;
using System;
using System.Numerics;
using System.Windows.Forms;

namespace GImpactTestDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<GImpactTestDemo>();
        }
    }

    internal sealed class GImpactTestDemo : IDemoConfiguration, IUpdateReceiver
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(0, 10, 50);
            demo.FreeLook.Target = new Vector3(0, 10, -4);
            demo.DemoText = ". - Shoot Bunny";
            demo.Graphics.FarPlane = 400.0f;
            demo.Graphics.WindowTitle = "BulletSharp - GImpact Demo";
            return new GImpactTestDemoSimulation();
        }

        public void Update(Demo demo)
        {
            if (demo.Input.KeysPressed.Contains(Keys.OemPeriod))
            {
                var simulation = demo.Simulation as GImpactTestDemoSimulation;
                simulation.ShootTrimesh(demo.FreeLook.Eye, demo.FreeLook.Target);
            }
        }
    }

    internal sealed class GImpactTestDemoSimulation : ISimulation
    {
        private const float ShootBoxInitialSpeed = 10.0f;

        private GImpactMeshShape _torusShape;
        private GImpactMeshShape _bunnyShape;

        private TriangleIndexVertexArray _torusShapeData;
        private TriangleIndexVertexArray _bunnyShapeData;

        public GImpactTestDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            //Broadphase = new SimpleBroadphase();
            Broadphase = new AxisSweep3_32Bit(new Vector3(-10000, -10000, -10000), new Vector3(10000, 10000, 10000), 1024);

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            GImpactCollisionAlgorithm.RegisterAlgorithm(Dispatcher);

            _torusShapeData = new TriangleIndexVertexArray(Torus.Indices, Torus.Vertices);
            _torusShape = CreateGImpactShape(_torusShapeData);

            _bunnyShapeData = new TriangleIndexVertexArray(Bunny.Indices, Bunny.Vertices);
            _bunnyShape = CreateGImpactShape(_bunnyShapeData);

            CreateStaticScene();
            CreateTorusChain();
            CreateBoxes();
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public void ShootTrimesh(Vector3 cameraPosition, Vector3 destination)
        {
            const float mass = 4.0f;
            Matrix4x4 startTransform = Matrix4x4.CreateTranslation(cameraPosition);
            RigidBody body = PhysicsHelper.CreateBody(mass, startTransform, _bunnyShape, World);

            body.LinearVelocity = Vector3.Normalize(destination - cameraPosition) * ShootBoxInitialSpeed;
            body.AngularVelocity = Vector3.Zero;
        }

        public void Dispose()
        {
            this.StandardCleanup();

            _bunnyShape.Dispose();

            _torusShapeData.Dispose();
            _bunnyShapeData.Dispose();
        }

        private GImpactMeshShape CreateGImpactShape(TriangleIndexVertexArray shapeData)
        {
            var shape = new GImpactMeshShape(shapeData);
            shape.Margin = 0;
            shape.UpdateBound();
            return shape;
        }

        private GImpactMeshShape CreateGImpactConvexDecompositionShape(TriangleIndexVertexArray shapeData)
        {
            //GImpactConvexDecompositionShape shape =
            //    new GImpactConvexDecompositionShape(indexVertexArrays, new Vector3(1), 0.01f);
            //shape.Margin = 0.07f;
            //shape.UpdateBound();
            //return shape;
            throw new NotImplementedException();
        }

        private void CreateStaticScene()
        {
            var boxShape1 = new BoxShape(200, 1, 200); //floor
            PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(0, -10, 0), boxShape1, World);

            var boxShape2 = new BoxShape(1, 50, 200); //left wall
            PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(-200, 15, 0), boxShape2, World);

            var boxShape3 = new BoxShape(1, 50, 200); //right wall
            PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(200, 15, 0), boxShape3, World);

            var boxShape4 = new BoxShape(200, 50, 1); //front wall
            PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(0, 15, 200), boxShape4, World);

            var boxShape5 = new BoxShape(200, 50, 1); //back wall
            PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(0, 15, -200), boxShape5, World);


            Vector3 normal = Vector3.Normalize(new Vector3(-0.5f, 0.5f, 0.0f));
            var planeShape1 = new StaticPlaneShape(normal, 0.5f);
            PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(0, -9, 0), planeShape1, World);

            normal = Vector3.Normalize(new Vector3(0.5f, 0.7f, 0.0f));
            var planeShape2 = new StaticPlaneShape(normal, 0.0f);
            PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(0, -10, 0), planeShape2, World);
        }

        private void CreateTorusChain()
        {
            const float step = 2.5f;
            const float mass = 1.0f;
            const float quarterTurn = (float)Math.PI * 0.5f;

            float angle = quarterTurn;
            float height = 28;

            Matrix4x4 startTransform =
                Matrix4x4.CreateFromYawPitchRoll(angle, 0, quarterTurn) *
                Matrix4x4.CreateTranslation(0, height, -5);
            var kinematicTorus = PhysicsHelper.CreateStaticBody(startTransform, _torusShape, World);
            //kinematicTorus.CollisionFlags |= CollisionFlags.StaticObject;
            //kinematicTorus.ActivationState = ActivationState.IslandSleeping;
            kinematicTorus.CollisionFlags |= CollisionFlags.KinematicObject;
            kinematicTorus.ActivationState = ActivationState.DisableDeactivation;

            for (int i = 0; i < 12; i++)
            {
                angle += quarterTurn;
                height -= step;
                startTransform =
                    Matrix4x4.CreateFromYawPitchRoll(angle, 0, quarterTurn) *
                    Matrix4x4.CreateTranslation(0, height, -5);
                PhysicsHelper.CreateBody(mass, startTransform, _torusShape, World);
            }
        }

        private void CreateBoxes()
        {
            var boxShape = new BoxShape(0.5f);

            for (int i = 0; i < 16; i++)
            {
                PhysicsHelper.CreateBody(1, Matrix4x4.CreateTranslation(i - 5, 2, -3), boxShape, World);
            }
        }
    }
}
