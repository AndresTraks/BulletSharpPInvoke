using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SerializeDemo
{
    class CustomBulletWorldImporter : BulletWorldImporter
    {
        public CustomBulletWorldImporter(DynamicsWorld world)
            : base(world)
        {
        }

        public override RigidBody CreateRigidBody(bool isDynamic, float mass, ref Matrix startTransform, CollisionShape shape, string bodyName)
        {
            RigidBody body = base.CreateRigidBody(isDynamic, mass, ref startTransform, shape, bodyName);

            if (bodyName != null && bodyName.Equals("GroundName"))
                body.UserObject = "Ground";

            if (shape.ShapeType == BroadphaseNativeType.StaticPlaneShape)
                body.UserObject = "Ground";

            return body;
        }
    }

    class SerializeDemo : Demo
    {
        Vector3 eye = new Vector3(30, 20, 10);
        Vector3 target = new Vector3(0, 5, 0);

        ///create 125 (5x5x5) dynamic objects
        int ArraySizeX = 5, ArraySizeY = 5, ArraySizeZ = 5;

        ///scaling of the objects (0.1 = 20 centimeter boxes )
        float StartPosX = -5;
        float StartPosY = -5;
        float StartPosZ = -3;

        BulletWorldImporter fileLoader;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Serialize Demo");
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new DbvtBroadphase();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);

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

            fileLoader = new CustomBulletWorldImporter(World);
            if (!fileLoader.LoadFile(bulletFile))
            {
                CollisionShape groundShape = new BoxShape(50);
                CollisionShapes.Add(groundShape);
                RigidBody ground = LocalCreateRigidBody(0, Matrix.Translation(0, -50, 0), groundShape);
                ground.UserObject = "Ground";

                // create a few dynamic rigidbodies
                float mass = 1.0f;

                Vector3[] positions = new Vector3[2] { new Vector3(0.1f, 0.2f, 0.3f), new Vector3(0.4f, 0.5f, 0.6f) };
                float[] radi = new float[2] { 0.3f, 0.4f };

                CollisionShape colShape = new MultiSphereShape(positions, radi);

                //CollisionShape colShape = new CapsuleShapeZ(1, 1);
                //CollisionShape colShape = new CylinderShapeZ(1, 1, 1);
                //CollisionShape colShape = new BoxShape(1);
                //CollisionShape colShape = new SphereShape(1);
                CollisionShapes.Add(colShape);

                Vector3 localInertia = colShape.CalculateLocalInertia(mass);

                float startX = StartPosX - ArraySizeX / 2;
                float startY = StartPosY;
                float startZ = StartPosZ - ArraySizeZ / 2;

                for (int k = 0; k < ArraySizeY; k++)
                {
                    for (int i = 0; i < ArraySizeX; i++)
                    {
                        for (int j = 0; j < ArraySizeZ; j++)
                        {
                            Matrix startTransform = Matrix.Translation(
                                2 * i + startX,
                                2 * k + startY,
                                2 * j + startZ
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

                    for (int i = 0; i < CollisionShapes.Count; i++)
                        serializer.RegisterNameForObject(CollisionShapes[i], "name" + i.ToString());

                    Point2PointConstraint p2p = new Point2PointConstraint((RigidBody) World.CollisionObjectArray[2],
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

        public override void ExitPhysics()
        {
            fileLoader.DeleteAllData();
            base.ExitPhysics();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new SerializeDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
