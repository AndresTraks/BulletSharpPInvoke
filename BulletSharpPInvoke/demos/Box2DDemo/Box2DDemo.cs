using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;

namespace Box2DDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<Box2DDemo>();
        }
    }

    internal sealed class Box2DDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(0, 15, 20);
            demo.FreeLook.Target = new Vector3(10, 10, 0);
            demo.Graphics.WindowTitle = "BulletSharp - Box 2D Demo";
            return new Box2DDemoSimulation();
        }
    }

    internal sealed class Box2DDemoSimulation : ISimulation
    {
        private const int NumObjectsX = 5, NumObjectsY = 5;
        private const float Depth = 0.04f;

        public Box2DDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();

            // Use the default collision dispatcher. For parallel processing you can use a diffent dispatcher.
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            var simplex = new VoronoiSimplexSolver();
            var pdSolver = new MinkowskiPenetrationDepthSolver();

            var convexAlgo2D = new Convex2DConvex2DAlgorithm.CreateFunc(simplex, pdSolver);

            Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.Convex2DShape, BroadphaseNativeType.Convex2DShape, convexAlgo2D);
            Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.Box2DShape, BroadphaseNativeType.Convex2DShape, convexAlgo2D);
            Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.Convex2DShape, BroadphaseNativeType.Box2DShape, convexAlgo2D);
            Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.Box2DShape, BroadphaseNativeType.Box2DShape, new Box2DBox2DCollisionAlgorithm.CreateFunc());

            Broadphase = new DbvtBroadphase();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            CreateGround();
            Create2dBodies();
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public void Dispose()
        {
            this.StandardCleanup();
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(150, 7, 150);
            var ground = PhysicsHelper.CreateStaticBody(Matrix.Identity, groundShape, World);
            ground.UserObject = "Ground";
        }

        private void Create2dBodies()
        {
            // Re-using the same collision is better for memory usage and performance
            float u = 0.96f;
            Vector3[] points = { new Vector3(0, u, 0), new Vector3(-u, -u, 0), new Vector3(u, -u, 0) };
            var childShape0 = new BoxShape(1, 1, Depth);
            var colShape = new Convex2DShape(childShape0);
            var childShape1 = new ConvexHullShape(points);
            var colShape2 = new Convex2DShape(childShape1);
            var childShape2 = new CylinderShapeZ(1, 1, Depth);
            var colShape3 = new Convex2DShape(childShape2);

            colShape.Margin = 0.03f;

            float mass = 1.0f;
            Vector3 localInertia = colShape.CalculateLocalInertia(mass);

            var rbInfo = new RigidBodyConstructionInfo(mass, null, colShape, localInertia);

            Vector3 x = new Vector3(-NumObjectsX, 8, -20);
            Vector3 y = Vector3.Zero;
            Vector3 deltaX = new Vector3(1, 2, 0);
            Vector3 deltaY = new Vector3(2, 0, 0);

            for (int i = 0; i < NumObjectsY; i++)
            {
                y = x;
                for (int j = 0; j < NumObjectsX; j++)
                {
                    Matrix startTransform = Matrix.Translation(y - new Vector3(-10, 0, 0));

                    //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
                    rbInfo.MotionState = new DefaultMotionState(startTransform);

                    switch (j % 3)
                    {
                        case 0:
                            rbInfo.CollisionShape = colShape;
                            break;
                        case 1:
                            rbInfo.CollisionShape = colShape3;
                            break;
                        default:
                            rbInfo.CollisionShape = colShape2;
                            break;
                    }
                    var body = new RigidBody(rbInfo)
                    {
                        //ActivationState = ActivationState.IslandSleeping,
                        LinearFactor = new Vector3(1, 1, 0),
                        AngularFactor = new Vector3(0, 0, 1)
                    };

                    World.AddRigidBody(body);

                    y += deltaY;
                }
                x += deltaX;
            }

            rbInfo.Dispose();
        }
    }
}
