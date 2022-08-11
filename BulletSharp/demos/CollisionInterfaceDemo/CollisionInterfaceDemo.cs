using BulletSharp;
using DemoFramework;
using System;
using System.Numerics;

namespace CollisionInterfaceDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<CollisionInterfaceDemo>();
        }
    }

    internal sealed class CollisionInterfaceDemo : IDemoConfiguration, IUpdateReceiver
    {
        private Vector3 _white = new Vector3(1, 1, 1);

        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(6, 4, 1);
            demo.FreeLook.Target = new Vector3(0, 3, 0);
            demo.IsDebugDrawEnabled = true;
            demo.Graphics.WindowTitle = "BulletSharp - Collision Interface Demo";
            return new CollisionInterfaceDemoSimulation();
        }

        public void Update(Demo demo)
        {
            var simulation = demo.Simulation as CollisionInterfaceDemoSimulation;
            CollisionObject movingObject = simulation.MovingObject;

            Matrix4x4 transform = movingObject.WorldTransform;
            Vector3 position = transform.Translation;
            transform.Translation = Vector3.Zero;
            transform *= Matrix4x4.CreateFromYawPitchRoll(0.1f * demo.FrameDelta, 0.05f * demo.FrameDelta, 0);
            transform.Translation = position;
            movingObject.WorldTransform = transform;

            if (demo.IsDebugDrawEnabled)
            {
                simulation.World.DebugDrawObjectRef(ref transform, movingObject.CollisionShape, ref _white);
                simulation.World.ContactTest(movingObject, simulation.RenderCallback);
            }
        }
    }

   internal sealed class CollisionInterfaceDemoSimulation : ISimulation
    {
        private readonly CollisionShape _staticShape;
        private readonly CollisionObject _staticObject;

        private readonly CollisionShape _movingBox;

        public CollisionInterfaceDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            Broadphase = new AxisSweep3(
                new Vector3(-1000, -1000, -1000),
                new Vector3(1000, 1000, 1000));

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            RenderCallback = new DrawingResult(World);

            _movingBox = new BoxShape(1.0f) { Margin = 0 };
            var rotation = Quaternion.CreateFromYawPitchRoll((float)Math.PI * 0.6f, (float)Math.PI * 0.2f, 0);
            MovingObject = new CollisionObject
            {
                CollisionShape = _movingBox,
                WorldTransform = Matrix4x4.CreateFromQuaternion(rotation) * Matrix4x4.CreateTranslation(0, 3, 0)
            };

            _staticShape = new BoxShape(0.5f) { Margin = 0 };
            _staticObject = new CollisionObject
            {
                CollisionShape = _staticShape,
                WorldTransform = Matrix4x4.CreateTranslation(0, 4.248f, 0)
            };
            World.AddCollisionObject(_staticObject);
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public CollisionObject MovingObject { get; }
        public DrawingResult RenderCallback { get; }

        public void Dispose()
        {
            RenderCallback.Dispose();
            MovingObject.Dispose();
            _movingBox.Dispose();
            _staticObject.Dispose();
            _staticShape.Dispose();

            this.StandardCleanup();
        }
    }

    class DrawingResult : ContactResultCallback
    {
        private Vector3 _red = new Vector3(1, 0, 0);
        private DynamicsWorld _world;

        public DrawingResult(DynamicsWorld world)
        {
            _world = world;
        }

        public override float AddSingleResult(ManifoldPoint cp,
            CollisionObjectWrapper colObj0Wrap, int partId0, int index0,
            CollisionObjectWrapper colObj1Wrap, int partId1, int index1)
        {
            Vector3 ptA = cp.PositionWorldOnA;
            Vector3 ptB = cp.PositionWorldOnB;
            _world.DebugDrawer.DrawLine(ref ptA, ref ptB, ref _red);
            return 0;
        }
    };
}
