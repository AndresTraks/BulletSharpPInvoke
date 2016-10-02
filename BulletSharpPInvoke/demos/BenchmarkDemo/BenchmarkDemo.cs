using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;

namespace BenchmarkDemo
{
    class BenchmarkDemo : Demo
    {
        Vector3 eye = new Vector3(60, 40, 20);
        Vector3 target = new Vector3(0, 5, -4);

        int benchmark = 2;

        const float collisionRadius = 0.0f;
        const float defaultContactProcessingThreshold = 0.0f;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Benchmark Demo");
            Graphics.SetInfoText("Move using mouse and WASD+shift\n" +
                "F3 - Toggle debug\n" +
                //"F11 - Toggle fullscreen\n" +
                "Space - Shoot box");
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            using (var cci = new DefaultCollisionConstructionInfo()
                { DefaultMaxPersistentManifoldPoolSize = 32768 })
            {
                CollisionConf = new DefaultCollisionConfiguration(cci);
            }

            Dispatcher = new CollisionDispatcher(CollisionConf);
            Dispatcher.DispatcherFlags = DispatcherFlags.DisableContactPoolDynamicAllocation;

            // the maximum size of the collision world. Make sure objects stay within these boundaries
            // Don't make the world AABB size too large, it will harm simulation quality and performance
            Vector3 worldAabbMin = new Vector3(-1000, -1000, -1000);
            Vector3 worldAabbMax = new Vector3(1000, 1000, 1000);

            HashedOverlappingPairCache pairCache = new HashedOverlappingPairCache();
            Broadphase = new AxisSweep3(worldAabbMin, worldAabbMax, 3500, pairCache);
            //Broadphase = new DbvtBroadphase();

            Solver = new SequentialImpulseConstraintSolver();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
            World.Gravity = new Vector3(0, -10, 0);
            World.SolverInfo.SolverMode |= SolverModes.EnableFrictionDirectionCaching;
            World.SolverInfo.NumIterations = 5;

            if (benchmark < 5)
            {
                // create the ground
                var groundShape = new BoxShape(250, 50, 250);
                CollisionShapes.Add(groundShape);
                var ground = base.LocalCreateRigidBody(0, Matrix.Translation(0, -50, 0), groundShape);
                ground.UserObject = "Ground";
            }

            float cubeSize = 1.0f;
            float spacing = cubeSize;
            float mass = 1.0f;
            int size = 8;
            Vector3 pos = new Vector3(0.0f, cubeSize * 2, 0.0f);
            float offset = -size * (cubeSize * 2.0f + spacing) * 0.5f;

            switch (benchmark)
            {
                case 1:
                    // 3000

                    BoxShape blockShape = new BoxShape(cubeSize - collisionRadius);
                    mass = 2.0f;

                    for (int k = 0; k < 47; k++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            pos[2] = offset + j * (cubeSize * 2.0f + spacing);
                            for (int i = 0; i < size; i++)
                            {
                                pos[0] = offset + i * (cubeSize * 2.0f + spacing);
                                RigidBody cmbody = LocalCreateRigidBody(mass, Matrix.Translation(pos), blockShape);
                            }
                        }
                        offset -= 0.05f * spacing * (size - 1);
                        // spacing *= 1.01f;
                        pos[1] += (cubeSize * 2.0f + spacing);
                    }
                    break;

                case 2:
                    CreatePyramid(new Vector3(-20, 0, 0), 12, new Vector3(cubeSize));
                    CreateWall(new Vector3(-2.0f, 0.0f, 0.0f), 12, new Vector3(cubeSize));
                    CreateWall(new Vector3(4.0f, 0.0f, 0.0f), 12, new Vector3(cubeSize));
                    CreateWall(new Vector3(10.0f, 0.0f, 0.0f), 12, new Vector3(cubeSize));
                    CreateTowerCircle(new Vector3(25.0f, 0.0f, 0.0f), 8, 24, new Vector3(cubeSize));
                    break;

                case 3:
                    // TODO: Ragdolls
                    break;

                case 4:
                    cubeSize = 1.5f;

                    ConvexHullShape convexHullShape = new ConvexHullShape();

                    float scaling = 1;

                    convexHullShape.LocalScaling = new Vector3(scaling);

                    for (int i = 0; i < Taru.Vtx.Length / 3; i++)
                    {
                        Vector3 vtx = new Vector3(Taru.Vtx[i * 3], Taru.Vtx[i * 3 + 1], Taru.Vtx[i * 3 + 2]);
                        convexHullShape.AddPoint(vtx * (1.0f / scaling));
                    }

                    //this will enable polyhedral contact clipping, better quality, slightly slower
                    convexHullShape.InitializePolyhedralFeatures();

                    for (int k = 0; k < 15; k++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            pos[2] = offset + j * (cubeSize * 2.0f + spacing);
                            for (int i = 0; i < size; i++)
                            {
                                pos[0] = offset + i * (cubeSize * 2.0f + spacing);
                                LocalCreateRigidBody(mass, Matrix.Translation(pos), convexHullShape);
                            }
                        }
                        offset -= 0.05f * spacing * (size - 1);
                        spacing *= 1.01f;
                        pos.Y += cubeSize * 2.0f + spacing;
                    }
                    break;

                case 5:
                    Vector3 boxSize = new Vector3(1.5f);
                    float boxMass = 1.0f;
                    float sphereRadius = 1.5f;
                    float sphereMass = 1.0f;
                    float capsuleHalf = 2.0f;
                    float capsuleRadius = 1.0f;
                    float capsuleMass = 1.0f;

                    size = 10;
                    int height = 10;

                    cubeSize = boxSize[0];
                    spacing = 2.0f;
                    pos = new Vector3(0.0f, 20.0f, 0.0f);
                    offset = -size * (cubeSize * 2.0f + spacing) * 0.5f;

                    int numBodies = 0;

                    Random random = new Random();

                    for (int k = 0; k < height; k++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            pos[2] = offset + j * (cubeSize * 2.0f + spacing);
                            for (int i = 0; i < size; i++)
                            {
                                pos[0] = offset + i * (cubeSize * 2.0f + spacing);
                                Vector3 bpos = new Vector3(0, 25, 0) + new Vector3(5.0f * pos.X, pos.Y, 5.0f * pos.Z);
                                int idx = random.Next(10);
                                Matrix trans = Matrix.Translation(bpos);

                                switch (idx)
                                {
                                    case 0:
                                    case 1:
                                    case 2:
                                        {
                                            float r = 0.5f * (idx + 1);
                                            BoxShape boxShape = new BoxShape(boxSize * r);
                                            LocalCreateRigidBody(boxMass * r, trans, boxShape);
                                        }
                                        break;

                                    case 3:
                                    case 4:
                                    case 5:
                                        {
                                            float r = 0.5f * (idx - 3 + 1);
                                            SphereShape sphereShape = new SphereShape(sphereRadius * r);
                                            LocalCreateRigidBody(sphereMass * r, trans, sphereShape);
                                        }
                                        break;

                                    case 6:
                                    case 7:
                                    case 8:
                                        {
                                            float r = 0.5f * (idx - 6 + 1);
                                            CapsuleShape capsuleShape = new CapsuleShape(capsuleRadius * r, capsuleHalf * r);
                                            LocalCreateRigidBody(capsuleMass * r, trans, capsuleShape);
                                        }
                                        break;
                                }

                                numBodies++;
                            }
                        }
                        offset -= 0.05f * spacing * (size - 1);
                        spacing *= 1.1f;
                        pos[1] += (cubeSize * 2.0f + spacing);
                    }

                    //CreateLargeMeshBody();

                    break;

                case 6:
                    boxSize = new Vector3(1.5f);

                    convexHullShape = new ConvexHullShape();

                    for (int i = 0; i < Taru.Vtx.Length / 3; i++)
                    {
                        Vector3 vtx = new Vector3(Taru.Vtx[i * 3], Taru.Vtx[i * 3 + 1], Taru.Vtx[i * 3 + 2]);
                        convexHullShape.AddPoint(vtx);
                    }

                    size = 10;
                    height = 10;

                    cubeSize = boxSize[0];
                    spacing = 2.0f;
                    pos = new Vector3(0.0f, 20.0f, 0.0f);
                    offset = -size * (cubeSize * 2.0f + spacing) * 0.5f;


                    for (int k = 0; k < height; k++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            pos[2] = offset + j * (cubeSize * 2.0f + spacing);
                            for (int i = 0; i < size; i++)
                            {
                                pos[0] = offset + i * (cubeSize * 2.0f + spacing);
                                Vector3 bpos = new Vector3(0, 25, 0) + new Vector3(5.0f * pos.X, pos.Y, 5.0f * pos.Z);

                                LocalCreateRigidBody(mass, Matrix.Translation(bpos), convexHullShape);
                            }
                        }
                        offset -= 0.05f * spacing * (size - 1);
                        spacing *= 1.1f;
                        pos[1] += (cubeSize * 2.0f + spacing);
                    }

                    //CreateLargeMeshBody();

                    break;

                case 7:
                    // TODO
                    //CreateTest6();
                    //InitRays();
                    break;
            }
        }

        void CreatePyramid(Vector3 offsetPosition, int stackSize, Vector3 boxSize)
        {
            const float mass = 1.0f;
            const float space = 0.0001f;

            var blockShape = new BoxShape(boxSize - new Vector3(collisionRadius));

            Vector3 diff = boxSize * 1.02f;
            Vector3 offset = -stackSize * 0.5f * (diff * 2 + new Vector3(space));
            Vector3 position = new Vector3(0.0f, boxSize.Y, 0.0f);

            while (stackSize > 0)
            {
                for (int j = 0; j < stackSize; j++)
                {
                    position.Z = offset.Z + j * (diff.Z * 2.0f + space);
                    for (int i = 0; i < stackSize; i++)
                    {
                        position.X = offset.X + i * (diff.X * 2.0f + space);
                        LocalCreateRigidBody(mass, Matrix.Translation(offsetPosition + position), blockShape);
                    }
                }
                offset += diff;
                position.Y += diff.Y * 2.0f + space;
                stackSize--;
            }
        }

        void CreateWall(Vector3 offsetPosition, int stackSize, Vector3 boxSize)
        {
            var blockShape = new BoxShape(boxSize - new Vector3(collisionRadius));

            const float mass = 1.0f;
            var position = Matrix.Identity;

            for (int y = 0; y < stackSize; y++)
            {
                float offset = 1 - y;
                float height = ((stackSize - y) * 2 - 1) * boxSize.Y;
                for (int i = 0; i < y; i++)
                {
                    position.Origin = offsetPosition +
                        new Vector3(0, height, (offset + i * 2) * boxSize.Z);
                    LocalCreateRigidBody(mass, position, blockShape);
                }
            }
        }

        void CreateTowerCircle(Vector3 offsetPosition, int stackSize, int rotSize, Vector3 boxSize)
        {
            var blockShape = new BoxShape(boxSize - new Vector3(collisionRadius));

            const float mass = 1.0f;
            float radius = 1.3f * rotSize * boxSize.X / (float)Math.PI;

            // create active boxes
            float positionY = boxSize.Y;
            float rotation = 0;

            for (int i = 0; i < stackSize; i++)
            {
                for (int j = 0; j < rotSize; j++)
                {
                    Matrix trans = Matrix.Translation(0, positionY, radius);
                    trans *= Matrix.RotationY(rotation);
                    trans.Origin += offsetPosition;
                    LocalCreateRigidBody(mass, trans, blockShape);

                    rotation += (2.0f * (float)Math.PI) / rotSize;
                }

                positionY += boxSize.Y * 2.0f;
                rotation += (float)Math.PI / rotSize;
            }
        }

        public override RigidBody LocalCreateRigidBody(float mass, Matrix startTransform, CollisionShape shape)
        {
            //rigidbody is dynamic if and only if mass is non zero, otherwise static
            bool isDynamic = (mass != 0.0f);
            Vector3 localInertia = isDynamic ? shape.CalculateLocalInertia(mass) : Vector3.Zero;

            RigidBody body;
            using (var rbInfo = new RigidBodyConstructionInfo(mass, null, shape, localInertia))
            {
                body = new RigidBody(rbInfo)
                {
                    ContactProcessingThreshold = defaultContactProcessingThreshold,
                    WorldTransform = startTransform
                };
            }

            World.AddRigidBody(body);
            return body;
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new BenchmarkDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
