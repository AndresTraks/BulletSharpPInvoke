using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using DemoFramework.FileLoaders;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UrdfDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<UrdfDemo>();
        }
    }

    internal sealed class UrdfDemo : IDemoConfiguration, IUpdateReceiver
    {
        private string[] _files;
        private int _fileIndex = 0;

        public UrdfDemo()
        {
            string baseDirectory = "data";
            _files = Directory.EnumerateFiles(baseDirectory, "*.urdf")
                .Select(Path.GetFileName)
                .ToArray();
        }

        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(1, 2, 1);
            demo.FreeLook.Target = new Vector3(0, 0, 0);
            demo.Graphics.WindowTitle = "BulletSharp - URDF Demo";

            string[] args = Environment.GetCommandLineArgs();
            string urdfFileName;
            if (args.Length != 2)
            {
                if (_files.Any())
                {
                    urdfFileName = _files[_fileIndex];
                }
                else
                {
                    urdfFileName = "door.urdf";
                }
            }
            else
            {
                urdfFileName = args[1];
            }

            return new UrdfDemoSimulation(urdfFileName);
        }

        public void Update(Demo demo)
        {
            if (demo.Input.KeysPressed.Contains(Keys.L))
            {
                _fileIndex = (_fileIndex + 1) % _files.Length;
                demo.ResetScene();
            }
        }
    }

    internal sealed class UrdfDemoSimulation : ISimulation
    {
        private const int NumBoxesX = 5, NumBoxesY = 5, NumBoxesZ = 5;
        private Vector3 _startPosition = new Vector3(0, 2, 0);

        public UrdfDemoSimulation(string urdfFileName)
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            CreateGround();

            LoadUrdf(urdfFileName);
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
            var groundShape = new BoxShape(50, 1, 50);
            CollisionObject ground = PhysicsHelper.CreateStaticBody(
                Matrix.Translation(0, -2, 0), groundShape, World);
            ground.UserObject = "Ground";
        }

        private void LoadUrdf(string fileName)
        {
            string baseDirectory = "data";
            string path = Path.Combine(baseDirectory, fileName);
            UrdfRobot robot = UrdfLoader.FromFile(path);
            new UrdfToBullet(World).Convert(robot, baseDirectory);
        }
    }
}
