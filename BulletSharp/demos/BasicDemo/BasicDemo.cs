using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;

namespace BasicDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<BasicDemo>();
        }
    }

    internal sealed class BasicDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(30, 20, 15) * BasicDemoSimulation.Scale;
            demo.FreeLook.Target = new Vector3(0, 3, 0) * BasicDemoSimulation.Scale;
            demo.Graphics.WindowTitle = "BulletSharp - Basic Demo";
            return new BasicDemoSimulation();
        }
    }

    internal sealed class BasicDemoSimulation : ISimulation
    {
        public const double Scale = 0.5f;
        private const int NumBoxesX = 5, NumBoxesY = 5, NumBoxesZ = 5;
        private Vector3 _startPosition = new Vector3(0, 2, 0);

        public BasicDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            CreateGround();
            CreateBoxes();
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
            var groundShape = new BoxShape(Scale * new Vector3(50, 1, 50));
            //var groundShape = new StaticPlaneShape(Vector3.UnitY, Scale);

            CollisionObject ground = PhysicsHelper.CreateStaticBody(Matrix.Identity, groundShape, World);
            ground.UserObject = "Ground";
        }

        private void CreateBoxes()
        {
            const double mass = 1.0f;
            var shape = new BoxShape(Scale);
            Vector3 localInertia = shape.CalculateLocalInertia(mass);
            var bodyInfo = new RigidBodyConstructionInfo(mass, null, shape, localInertia);

            for (int y = 0; y < NumBoxesY; y++)
            {
                for (int x = 0; x < NumBoxesX; x++)
                {
                    for (int z = 0; z < NumBoxesZ; z++)
                    {
                        Vector3 position = _startPosition + Scale * 2 * new Vector3(x, y, z);

                        // make it drop from a height
                        position += new Vector3(0, Scale * 10, 0);

                        // using MotionState is recommended, it provides interpolation capabilities
                        // and only synchronizes 'active' objects
                        bodyInfo.MotionState = new DefaultMotionState(Matrix.Translation(position));
                        var body = new RigidBody(bodyInfo);

                        World.AddRigidBody(body);
                    }
                }
            }

            bodyInfo.Dispose();
        }
    }
}
