using BulletSharp;
using DemoFramework;
using System;
using System.IO;
using System.Numerics;

namespace BulletXmlImportDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<BulletXmlImportDemo>();
        }
    }

    internal sealed class BulletXmlImportDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(30, 20, 10);
            demo.FreeLook.Target = new Vector3(0, 5, -4);
            demo.Graphics.WindowTitle = "BulletSharp - XML Import Demo";
            return new BulletXmlImportDemoSimulation();
        }
    }

    internal sealed class BulletXmlImportDemoSimulation : ISimulation
    {
        private readonly BulletXmlWorldImporter _importer;

        public BulletXmlImportDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            _importer = new BulletXmlWorldImporter(World);
            if (!_importer.LoadFile(Path.Combine("data", "bullet_basic.xml")))
            {
                //throw new FileNotFoundException();
            }
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public void Dispose()
        {
            _importer.DeleteAllData();
            this.StandardCleanup();
        }
    }
}
