using BulletSharp;
using DemoFramework;
using DemoFramework.FileLoaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace BspDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<BspDemo>();
        }
    }

    internal sealed class BspDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(10, 10, 10);
            demo.FreeLook.Target = Vector3.Zero;
            demo.FreeLook.Up = BspDemoSimulation.Up;
            demo.Graphics.WindowTitle = "BulletSharp - Quake BSP Physics Viewer";
            return new BspDemoSimulation();
        }
    }

    internal sealed class BspDemoSimulation : ISimulation
    {
        public BspDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            Broadphase = new DbvtBroadphase();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);
            World.Gravity = Up * -10.0f;

            LoadBspFile();
        }

        private void LoadBspFile()
        {
            var bspLoader = new BspLoader();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
            {
                bspLoader.LoadBspFile(Path.Combine("data", "BspDemo.bsp"));
            }
            else
            {
                bspLoader.LoadBspFile(args[1]);
            }

            var bsp2Bullet = new BspToBulletConverter(World);
            bsp2Bullet.ConvertBsp(bspLoader, 0.1f);
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public static Vector3 Up { get; } = Vector3.UnitZ;

        public void Dispose()
        {
            this.StandardCleanup();
        }
    }

    internal sealed class BspToBulletConverter : BspConverter
    {
        private DynamicsWorld _world;

        public BspToBulletConverter(DynamicsWorld world)
        {
            _world = world;
        }

        public override void AddCollider(List<Vector3> vertices)
        {
            if (vertices.Count == 0) return;

            const float mass = 0.0f;
            Matrix4x4 startTransform = Matrix4x4.CreateTranslation(0, 0, -10.0f); // shift
            var shape = new ConvexHullShape(vertices);

            PhysicsHelper.CreateBody(mass, startTransform, shape, _world);
        }
    }
}
