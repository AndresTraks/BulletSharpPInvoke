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
            DemoRunner.Run<MultiThreadedDemo>();
        }
    }

    internal sealed class MultiThreadedDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(30, 20, 15);
            demo.FreeLook.Target = new Vector3(0, 3, 0);
            var simulation = new MultiThreadedDemoSimulation();
            demo.Graphics.WindowTitle = $"BulletSharp - Multi-threaded Demo ({Threads.TaskScheduler.GetType().Name})";
            return simulation;
        }
    }

    internal sealed class MultiThreadedDemoSimulation : ISimulation
    {
        private const int NumBoxesX = 5, NumBoxesY = 50, NumBoxesZ = 50;
        private Vector3 _startPosition = new Vector3(0, 2, 0);
        private const int MaxThreadCount = 64;
        private ConstraintSolverPoolMultiThreaded _constraintSolver;

        public MultiThreadedDemoSimulation()
        {
            SetTaskScheduler();

            using (var collisionConfigurationInfo = new DefaultCollisionConstructionInfo
            {
                DefaultMaxPersistentManifoldPoolSize = 80000,
                DefaultMaxCollisionAlgorithmPoolSize = 80000
            })
            {
                CollisionConfiguration = new DefaultCollisionConfiguration(collisionConfigurationInfo);
            };
            Dispatcher = new CollisionDispatcherMultiThreaded(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            _constraintSolver = new ConstraintSolverPoolMultiThreaded(MaxThreadCount);
            World = new DiscreteDynamicsWorldMultiThreaded(Dispatcher, Broadphase, _constraintSolver, CollisionConfiguration);
            World.SolverInfo.SolverMode = SolverModes.Simd | SolverModes.UseWarmStarting;

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

        private void SetTaskScheduler()
        {
            TaskScheduler scheduler = Threads.GetOpenMPTaskScheduler();
            if (scheduler == null)
            {
                scheduler = Threads.GetTbbTaskScheduler();
            }
            if (scheduler == null)
            {
                scheduler = Threads.GetPplTaskScheduler();
            }
            if (scheduler == null)
            {
                scheduler = Threads.GetSequentialTaskScheduler();
            }
            Threads.TaskScheduler = scheduler;
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(50, 1, 50);
            //var groundShape = new StaticPlaneShape(Vector3.UnitY, 1);

            CollisionObject ground = PhysicsHelper.CreateStaticBody(Matrix.Identity, groundShape, World);
            ground.UserObject = "Ground";
        }

        private void CreateBoxes()
        {
            const float mass = 1.0f;
            var shape = new BoxShape(1);
            Vector3 localInertia = shape.CalculateLocalInertia(mass);
            var bodyInfo = new RigidBodyConstructionInfo(mass, null, shape, localInertia);

            for (int y = 0; y < NumBoxesY; y++)
            {
                for (int x = 0; x < NumBoxesX; x++)
                {
                    for (int z = 0; z < NumBoxesZ; z++)
                    {
                        Vector3 position = _startPosition + 2 * new Vector3(x, y, z);

                        // make it drop from a height
                        position += new Vector3(0, 10, 0);

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
