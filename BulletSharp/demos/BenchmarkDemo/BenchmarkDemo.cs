using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using DemoFramework.Meshes;
using System;

namespace BenchmarkDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<BenchmarkDemo>();
        }
    }

    internal sealed class BenchmarkDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(35, 20, 10);
            demo.FreeLook.Target = new Vector3(0, 0, -2);
            demo.Graphics.WindowTitle = "BulletSharp - Benchmark Demo";
            return new BenchmarkDemoSimulation();
        }
    }

    internal sealed class BenchmarkDemoSimulation : ISimulation
    {
        private const float defaultContactProcessingThreshold = 0.0f;

        private int _scene = 1;
        private Action[] _scenes;

        public BenchmarkDemoSimulation()
        {
            using (var cci = new DefaultCollisionConstructionInfo()
            { DefaultMaxPersistentManifoldPoolSize = 32768 })
            {
                CollisionConfiguration = new DefaultCollisionConfiguration(cci);
            }

            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Dispatcher.DispatcherFlags = DispatcherFlags.DisableContactPoolDynamicAllocation;

            // The maximum size of the collision world. Make sure objects stay within these boundaries
            // Don't make the world AABB size too large, it will harm simulation quality and performance
            var worldAabbMin = new Vector3(-1000, -1000, -1000);
            var worldAabbMax = new Vector3(1000, 1000, 1000);

            Broadphase = new AxisSweep3(worldAabbMin, worldAabbMax, 3500);
            //Broadphase = new DbvtBroadphase();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);
            World.SolverInfo.SolverMode |= SolverModes.EnableFrictionDirectionCaching;
            World.SolverInfo.NumIterations = 5;

            _scenes = new Action[] {
                Create3KBoxes, CreateStructures, CreateTaruStack, CreateShapesGravity, CreateTaruGravity
            };
            _scenes[_scene]();
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        private void CreateGround()
        {
            var groundShape = new BoxShape(250, 50, 250);
            var ground = PhysicsHelper.CreateStaticBody(Matrix.Translation(0, -50, 0), groundShape, World);
            ground.UserObject = "Ground";
        }

        public void Dispose()
        {
            this.StandardCleanup();
        }

        private void Create3KBoxes()
        {
            CreateGround();

            const float mass = 2.0f;
            const int arrayWidth = 8;
            const int arrayHeight = 47;
            const float cubeHalfExtent = 0.5f;
            const float cubeWidth = cubeHalfExtent * 2;
            const float spacing = cubeHalfExtent;
            const float offset = cubeWidth + spacing;
            var startPosition = new Vector3(-20, 0, -10);
            var blockShape = new BoxShape(cubeHalfExtent);

            CreateStack(startPosition, arrayWidth, arrayHeight, arrayWidth, offset, 1, mass, blockShape);
        }

        private void CreateStructures()
        {
            CreateGround();
            var boxSize = new Vector3(0.5f);
            CreatePyramid(new Vector3(-10, 0, 0), 12, boxSize);
            CreateWall(new Vector3(-1, 0, 0), 12, boxSize);
            CreateWall(new Vector3(2, 0, 0), 12, boxSize);
            CreateWall(new Vector3(5, 0, 0), 12, boxSize);
            CreateTowerCircle(new Vector3(12, 0, 0), 8, 24, boxSize);
        }

        private void CreateTaruStack()
        {
            CreateGround();

            const float mass = 2.0f;
            const int arrayWidth = 8;
            const int arrayHeight = 15;
            const float offset = 5;
            const float widthSpacingFactor = 1.02f;
            var startPosition = new Vector3(-30, 0, -10);
            var convexHullShape = new ConvexHullShape(Taru.Vertices);

            //this will enable polyhedral contact clipping, better quality, slightly slower
            convexHullShape.InitializePolyhedralFeatures();

            CreateStack(startPosition, arrayWidth, arrayHeight, arrayWidth, offset,
                widthSpacingFactor, mass, convexHullShape);
        }

        private void CreateShapesGravity()
        {
            const float cubeHalfExtent = 1.5f;
            const float cubeWidth = cubeHalfExtent * 2;
            var boxSize = new Vector3(cubeHalfExtent);
            float boxMass = 1.0f;
            float sphereRadius = 1.5f;
            float sphereMass = 1.0f;
            float capsuleHalf = 2.0f;
            float capsuleRadius = 1.0f;
            float capsuleMass = 1.0f;

            int stackSize = 10;
            int stackHeight = 10;

            float spacing = 2.0f;
            var position = new Vector3(0.0f, 20.0f, 0.0f);
            float offset = -stackSize * (cubeWidth + spacing) * 0.5f;

            int numBodies = 0;

            var random = new Random();

            for (int k = 0; k < stackHeight; k++)
            {
                for (int j = 0; j < stackSize; j++)
                {
                    position.Z = offset + j * (cubeWidth + spacing);
                    for (int i = 0; i < stackSize; i++)
                    {
                        position.X = offset + i * (cubeWidth + spacing);
                        Vector3 bpos = new Vector3(0, 25, 0) + new Vector3(5.0f * position.X, position.Y, 5.0f * position.Z);
                        int idx = random.Next(10);
                        Matrix trans = Matrix.Translation(bpos);

                        switch (idx)
                        {
                            case 0:
                            case 1:
                            case 2:
                                {
                                    float r = 0.5f * (idx + 1);
                                    var boxShape = new BoxShape(boxSize * r);
                                    LocalCreateRigidBody(boxMass * r, trans, boxShape);
                                }
                                break;

                            case 3:
                            case 4:
                            case 5:
                                {
                                    float r = 0.5f * (idx - 3 + 1);
                                    var sphereShape = new SphereShape(sphereRadius * r);
                                    LocalCreateRigidBody(sphereMass * r, trans, sphereShape);
                                }
                                break;

                            case 6:
                            case 7:
                            case 8:
                                {
                                    float r = 0.5f * (idx - 6 + 1);
                                    var capsuleShape = new CapsuleShape(capsuleRadius * r, capsuleHalf * r);
                                    LocalCreateRigidBody(capsuleMass * r, trans, capsuleShape);
                                }
                                break;
                        }

                        numBodies++;
                    }
                }
                offset -= 0.05f * spacing * (stackSize - 1);
                spacing *= 1.1f;
                position.Y += cubeWidth + spacing;
            }

            //CreateLargeMeshBody();
        }

        private void CreateTaruGravity()
        {
            const float mass = 1.0f;
            const float cubeHalfExtent = 1.5f;
            const float cubeWidth = cubeHalfExtent * 2;
            const int stackWidth = 10;
            const int stackHeight = 10;
            const float spacing = 5.0f;
            const float offset = cubeWidth + spacing;
            const float widthSpacingFactor = 1.12f;
            var startPosition = new Vector3(-250, 0, -150);

            var convexHullShape = new ConvexHullShape(Taru.Vertices);

            CreateStack(startPosition, stackWidth, stackHeight, stackWidth, offset,
                widthSpacingFactor, mass, convexHullShape);

            //CreateLargeMeshBody();
        }

        private void CreatePyramid(Vector3 offsetPosition, int stackSize, Vector3 boxSize)
        {
            const float mass = 1.0f;
            const float space = 0.0001f;

            var blockShape = new BoxShape(boxSize);

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

        private void CreateWall(Vector3 offsetPosition, int stackSize, Vector3 boxSize)
        {
            var blockShape = new BoxShape(boxSize);

            const float mass = 1.0f;
            var transform = Matrix.Identity;

            for (int y = 0; y < stackSize; y++)
            {
                float offset = 1 - y;
                float height = ((stackSize - y) * 2 - 1) * boxSize.Y;
                for (int i = 0; i < y; i++)
                {
                    transform.Origin = offsetPosition +
                        new Vector3(0, height, (offset + i * 2) * boxSize.Z);
                    LocalCreateRigidBody(mass, transform, blockShape);
                }
            }
        }

        private void CreateTowerCircle(Vector3 offsetPosition, int stackSize, int rotSize, Vector3 boxSize)
        {
            var blockShape = new BoxShape(boxSize);

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

        private void CreateStack(Vector3 startPosition, int widthX, int height, int widthZ, float offset,
            float widthSpacingFactor, float mass, CollisionShape shape)
        {
            float widthSpacing = 1;
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < widthZ; z++)
                {
                    for (int x = 0; x < widthX; x++)
                    {
                        Vector3 position = startPosition + offset * new Vector3(x * widthSpacing, y, z * widthSpacing);
                        LocalCreateRigidBody(mass, Matrix.Translation(position), shape);
                    }
                }
                widthSpacing *= widthSpacingFactor;
            }
        }

        private RigidBody LocalCreateRigidBody(float mass, Matrix startTransform, CollisionShape shape)
        {
            RigidBody body = PhysicsHelper.CreateBody(mass, startTransform, shape, World);
            body.ContactProcessingThreshold = defaultContactProcessingThreshold;
            return body;
        }
    }
}
