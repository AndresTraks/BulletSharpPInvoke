using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;

namespace RollingFrictionDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<RollingFrictionDemo>();
        }
    }

    internal sealed class RollingFrictionDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(10, 10, 40);
            demo.FreeLook.Target = new Vector3(0, 5, -4);
            demo.Graphics.WindowTitle = "BulletSharp - Rolling Friction Demo";
            return new RollingFrictionDemoSimulation();
        }
    }

    internal sealed class RollingFrictionDemoSimulation : ISimulation
    {
        private const int NumObjectsX = 5, NumObjectsY = 5, NumObjectsZ = 5;

        private Vector3 _startPosition = new Vector3(-7f, 2, 0);

        public RollingFrictionDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            CreateGround();

            // create a few dynamic rigidbodies
            CollisionShape[] colShapes = {
                new SphereShape(1),
                new CapsuleShape(0.5f, 1),
                new CapsuleShapeX(0.5f, 1),
                new CapsuleShapeZ(0.5f, 1),
                new ConeShape(0.5f, 1),
                new ConeShapeX(0.5f, 1),
                new ConeShapeZ(0.5f, 1),
                new CylinderShape(new Vector3(0.5f, 1, 0.5f)),
                new CylinderShapeX(new Vector3(1, 0.5f, 0.5f)),
                new CylinderShapeZ(new Vector3(0.5f, 0.5f, 1)),
            };

            const float mass = 1.0f;
            var anisotropicRollingFrictionDirection = new Vector3(1, 1, 1);

            var rbInfo = new RigidBodyConstructionInfo(mass, null, null);

            int shapeIndex = 0;
            for (int y = 0; y < NumObjectsY; y++)
            {
                for (int x = 0; x < NumObjectsX; x++)
                {
                    for (int z = 0; z < NumObjectsZ; z++)
                    {
                        Vector3 position = _startPosition + 2 * new Vector3(x, y, z);
                        position += new Vector3(0, 10, 0);
                        Matrix startTransform = Matrix.Translation(position);

                        shapeIndex++;
                        var shape = colShapes[shapeIndex % colShapes.Length];

                        // using motionstate is recommended, it provides interpolation capabilities
                        // and only synchronizes 'active' objects
                        rbInfo.MotionState = new DefaultMotionState(startTransform);
                        rbInfo.CollisionShape = shape;
                        rbInfo.LocalInertia = shape.CalculateLocalInertia(rbInfo.Mass);

                        var body = new RigidBody(rbInfo)
                        {
                            Friction = 1,
                            RollingFriction = 0.1f,
                            SpinningFriction = 0.1f
                        };
                        body.SetAnisotropicFriction(shape.AnisotropicRollingFrictionDirection,
                            AnisotropicFrictionFlags.RollingFriction);

                        World.AddRigidBody(body);
                    }
                }
            }

            rbInfo.Dispose();
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
            var groundShape = new BoxShape(20, 50, 10);
            CollisionObject ground = PhysicsHelper.CreateStaticBody(
                Matrix.RotationAxis(new Vector3(0, 0, 1), (float)Math.PI * 0.03f) * Matrix.Translation(0, -50, 0),
                groundShape, World);
            ground.Friction = 1;
            ground.RollingFriction = 1;
            ground.UserObject = "Ground";

            groundShape = new BoxShape(100, 50, 100);
            ground = PhysicsHelper.CreateStaticBody(Matrix.Translation(0, -54, 0), groundShape, World);
            ground.Friction = 1;
            ground.RollingFriction = 1;
            ground.UserObject = "Ground";
        }
    }
}
