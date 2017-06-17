using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

    internal sealed class MultiThreadedDemo : IDemoConfiguration, IUpdateReceiver
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(80, 50, -30) * MultiThreadedDemoSimulation.Scale;
            demo.FreeLook.Target = new Vector3(0, 20, 0) * MultiThreadedDemoSimulation.Scale;
            var simulation = new MultiThreadedDemoSimulation();
            var scheduler = Threads.TaskScheduler;
            demo.Graphics.WindowTitle = "BulletSharp - Multi-threaded Demo";
            SetDemoText(demo);
            return simulation;
        }

        public void Update(Demo demo)
        {
            if (demo.Input.KeysPressed.Contains(Keys.T))
            {
                ((MultiThreadedDemoSimulation)demo.Simulation).NextTaskScheduler();
                SetDemoText(demo);
            }
        }

        private void SetDemoText(Demo demo)
        {
            var scheduler = Threads.TaskScheduler;
            demo.DemoText = $"T - Scheduler: {scheduler.Name}\n{scheduler.NumThreads}/{scheduler.MaxNumThreads} threads";
        }
    }

    internal sealed class MultiThreadedDemoSimulation : ISimulation
    {
        public const float Scale = 0.5f;
        private const int NumBoxesX = 5, NumBoxesY = 50, NumBoxesZ = 20;
        private Vector3 _startPosition = new Vector3(0, 2, 0);
        private const int MaxThreadCount = 64;
        private ConstraintSolverPoolMultiThreaded _constraintSolver;
        private List<TaskScheduler> _schedulers = new List<TaskScheduler>();
        private int _currentScheduler = 0;

        public MultiThreadedDemoSimulation()
        {
            CreateSchedulers();
            NextTaskScheduler();

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

        public void NextTaskScheduler()
        {
            _currentScheduler++;
            if (_currentScheduler >= _schedulers.Count)
            {
                _currentScheduler = 0;
            }
            TaskScheduler scheduler = _schedulers[_currentScheduler];
            scheduler.NumThreads = scheduler.MaxNumThreads;
            Threads.TaskScheduler = scheduler;
        }

        private void CreateSchedulers()
        {
            AddScheduler(Threads.GetSequentialTaskScheduler());
            AddScheduler(Threads.GetOpenMPTaskScheduler());
            AddScheduler(Threads.GetTbbTaskScheduler());
            AddScheduler(Threads.GetPplTaskScheduler());
        }

        private void AddScheduler(TaskScheduler scheduler)
        {
            if (scheduler != null)
            {
                _schedulers.Add(scheduler);
            }
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(Scale * new Vector3(100, 1, 100));
            //var groundShape = new StaticPlaneShape(Vector3.UnitY, Scale);

            CollisionObject ground = PhysicsHelper.CreateStaticBody(Matrix.Identity, groundShape, World);
            ground.UserObject = "Ground";
        }

        private void CreateBoxes()
        {
            const float mass = 1.0f;
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
