using BulletSharp;
using DemoFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SerializeDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<SerializeDemo>();
        }
    }

    internal sealed class SerializeDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(30, 20, 10);
            demo.FreeLook.Target = new Vector3(0, 5, 0);
            demo.Graphics.WindowTitle = "BulletSharp - Serialize Demo";
            return new SerializeDemoSimulation();
        }
    }

    internal sealed class SerializeDemoSimulation : ISimulation
    {
        const int NumObjectsX = 5, NumObjectsY = 5, NumObjectsZ = 5;

        ///scaling of the objects (0.1 = 20 centimeter boxes )
        float StartPosX = -5;
        float StartPosY = -5;
        float StartPosZ = -3;

        private BulletWorldImporter _fileLoader;

        private List<CollisionShape> _collisionShapes = new List<CollisionShape>();

        public SerializeDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            GImpactCollisionAlgorithm.RegisterAlgorithm(Dispatcher);

            string bulletFile;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
            {
                bulletFile = "testFile.bullet";
            }
            else
            {
                bulletFile = args[1];
            }

            _fileLoader = new CustomBulletWorldImporter(World);
            if (!_fileLoader.LoadFile(bulletFile))
            {
                var groundShape = new BoxShape(50);
                _collisionShapes.Add(groundShape);
                RigidBody ground = PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(0, -50, 0), groundShape, World);
                ground.UserObject = "Ground";

                // create a few dynamic rigidbodies
                float mass = 1.0f;

                Vector3[] positions = new[] { new Vector3(0.1f, 0.2f, 0.3f), new Vector3(0.4f, 0.5f, 0.6f) };
                float[] radi = new float[2] { 0.3f, 0.4f };

                var colShape = new MultiSphereShape(positions, radi);

                //var colShape = new CapsuleShapeZ(1, 1);
                //var colShape = new CylinderShapeZ(1, 1, 1);
                //var colShape = new BoxShape(1);
                //var colShape = new SphereShape(1);
                _collisionShapes.Add(colShape);

                Vector3 localInertia = colShape.CalculateLocalInertia(mass);

                float startX = StartPosX - NumObjectsX / 2;
                float startY = StartPosY;
                float startZ = StartPosZ - NumObjectsZ / 2;

                for (int y = 0; y < NumObjectsY; y++)
                {
                    for (int x = 0; x < NumObjectsX; x++)
                    {
                        for (int z = 0; z < NumObjectsZ; z++)
                        {
                            Matrix4x4 startTransform = Matrix4x4.CreateTranslation(
                                2 * x + startX,
                                2 * y + startY,
                                2 * z + startZ
                            );

                            // using motionstate is recommended, it provides interpolation capabilities
                            // and only synchronizes 'active' objects
                            DefaultMotionState myMotionState = new DefaultMotionState(startTransform);
                            RigidBodyConstructionInfo rbInfo =
                                new RigidBodyConstructionInfo(mass, myMotionState, colShape, localInertia);
                            RigidBody body = new RigidBody(rbInfo);
                            rbInfo.Dispose();

                            // make it drop from a height
                            body.Translate(new Vector3(0, 20, 0));

                            World.AddRigidBody(body);
                        }
                    }
                }

                using (var serializer = new DefaultSerializer())
                {
                    serializer.RegisterNameForObject(ground, "GroundName");

                    for (int i = 0; i < _collisionShapes.Count; i++)
                        serializer.RegisterNameForObject(_collisionShapes[i], "name" + i.ToString());

                    var p2p = new Point2PointConstraint((RigidBody) World.CollisionObjectArray[2],
                        new Vector3(0, 1, 0));
                    World.AddConstraint(p2p);
                    serializer.RegisterNameForObject(p2p, "constraintje");

                    World.Serialize(serializer);
                    byte[] dataBytes = new byte[serializer.CurrentBufferSize];
                    Marshal.Copy(serializer.BufferPointer, dataBytes, 0, dataBytes.Length);

                    using (var file = new FileStream("testFile.bullet", FileMode.Create))
                    {
                        file.Write(dataBytes, 0, dataBytes.Length);
                    }
                }
            }
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public void Dispose()
        {
            _fileLoader.DeleteAllData();

            this.StandardCleanup();
        }
    }

    internal sealed class CustomBulletWorldImporter : BulletWorldImporter
    {
        public CustomBulletWorldImporter(DynamicsWorld world)
            : base(world)
        {
        }

        public override RigidBody CreateRigidBody(bool isDynamic, float mass, ref Matrix4x4 startTransform, CollisionShape shape, string bodyName)
        {
            RigidBody body = base.CreateRigidBody(isDynamic, mass, ref startTransform, shape, bodyName);

            if (bodyName != null && bodyName.Equals("GroundName"))
                body.UserObject = "Ground";

            if (shape.ShapeType == BroadphaseNativeType.StaticPlaneShape)
                body.UserObject = "Ground";

            return body;
        }
    }
}
