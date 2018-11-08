using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using DemoFramework.FileLoaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CharacterDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<CharacterDemo>();
        }
    }

    internal sealed class CharacterDemo : IDemoConfiguration, IUpdateReceiver
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(10, 0, 10);
            demo.FreeLook.Target = Vector3.Zero;
            demo.DemoText = "Space - Jump";
            demo.Graphics.WindowTitle = "BulletSharp - Character Demo";
            return new CharacterDemoSimulation();
        }

        public void Update(Demo demo)
        {
            var simulation = demo.Simulation as CharacterDemoSimulation;

            Matrix transform = simulation.GhostObject.WorldTransform;

            Vector3 forwardDir = new Vector3(transform.M31, transform.M32, transform.M33);
            forwardDir.Normalize();

            Vector3 upDir = new Vector3(transform.M21, transform.M22, transform.M23);
            upDir.Normalize();

            Vector3 walkDirection = Vector3.Zero;
            const float walkVelocity = 1.1f * 4.0f;
            float walkSpeed = walkVelocity * demo.FrameDelta * 10;// * 0.0001f;
            float turnSpeed = demo.FrameDelta * 3;
            Vector3 position = transform.Origin;

            var keysDown = demo.Input.KeysDown;
            if (keysDown.Count > 0)
            {
                if (keysDown.Contains(Keys.Left))
                {
                    transform.Origin = Vector3.Zero;
                    transform *= Matrix.RotationAxis(upDir, -turnSpeed);
                    transform.Origin = position;
                    simulation.GhostObject.WorldTransform = transform;
                }
                if (keysDown.Contains(Keys.Right))
                {
                    transform.Origin = Vector3.Zero;
                    transform *= Matrix.RotationAxis(upDir, turnSpeed);
                    transform.Origin = position;
                    simulation.GhostObject.WorldTransform = transform;
                }
                if (keysDown.Contains(Keys.Up))
                {
                    walkDirection += forwardDir;
                }
                if (keysDown.Contains(Keys.Down))
                {
                    walkDirection -= forwardDir;
                }
            }

            simulation.Character.SetWalkDirection(walkDirection * walkSpeed);

            if (demo.Input.KeysPressed.Contains(Keys.Space))
            {
                demo.Input.KeysPressed.Remove(Keys.Space);
                simulation.Character.Jump();
            }

            Vector3 cameraPos = position - forwardDir * 12 + upDir * 5;
            UpdateCamera(demo, simulation, ref position, ref cameraPos);
        }

        private void UpdateCamera(Demo demo, CharacterDemoSimulation simulation, ref Vector3 position, ref Vector3 cameraPos)
        {
            //use the convex sweep test to find a safe position for the camera (not blocked by static geometry)
            var convexResultCallback = simulation.ConvexResultCallback;
            convexResultCallback.ConvexFromWorld = position;
            convexResultCallback.ConvexToWorld = cameraPos;
            convexResultCallback.ClosestHitFraction = 1.0f;
            simulation.World.ConvexSweepTest(simulation.CameraSphere,
                Matrix.Translation(position), Matrix.Translation(cameraPos), convexResultCallback);
            if (convexResultCallback.HasHit)
            {
                cameraPos = Vector3.Lerp(position, cameraPos, convexResultCallback.ClosestHitFraction);
            }
            demo.FreeLook.Eye = cameraPos;
            demo.FreeLook.Target = position;
        }
    }

    internal sealed class CharacterDemoSimulation : ISimulation
    {
        private GhostPairCallback _ghostPairCallback;

        public CharacterDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            Broadphase = new AxisSweep3(new Vector3(-1000, -1000, -1000), new Vector3(1000, 1000, 1000));

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);
            World.DispatchInfo.AllowedCcdPenetration = 0.0001f;

            CreateCharacter();

            var path = Path.Combine("data", "BspDemo.bsp");
            var bspLoader = new BspLoader();
            bspLoader.LoadBspFile(path);
            var bsp2Bullet = new BspToBulletConverter(World);
            bsp2Bullet.ConvertBsp(bspLoader, 0.1f);

            ConvexResultCallback = new ClosestConvexResultCallback();
            ConvexResultCallback.CollisionFilterMask = (short)CollisionFilterGroups.StaticFilter;
            CameraSphere = new SphereShape(0.2f);
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public PairCachingGhostObject GhostObject { get; private set; }
        public ClosestConvexResultCallback ConvexResultCallback { get; }
        public SphereShape CameraSphere { get; }
        public KinematicCharacterController Character { get; private set; }

        public void Dispose()
        {
            _ghostPairCallback.Dispose();
            CameraSphere.Dispose();
            ConvexResultCallback.Dispose();

            this.StandardCleanup();
        }

        private void CreateCharacter()
        {
            _ghostPairCallback = new GhostPairCallback();
            Broadphase.OverlappingPairCache.SetInternalGhostPairCallback(_ghostPairCallback);

            const float characterHeight = 1.75f;
            const float characterWidth = 1.75f;
            var capsule = new CapsuleShape(characterWidth, characterHeight);
            GhostObject = new PairCachingGhostObject()
            {
                CollisionShape = capsule,
                CollisionFlags = CollisionFlags.CharacterObject,
                WorldTransform = Matrix.Translation(10.210098f, -1.6433364f, 16.453260f)
            };
            World.AddCollisionObject(GhostObject, CollisionFilterGroups.CharacterFilter, CollisionFilterGroups.StaticFilter | CollisionFilterGroups.DefaultFilter);

            const float stepHeight = 0.35f;
            Character = new KinematicCharacterController(GhostObject, capsule, stepHeight);
            World.AddAction(Character);
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

            var verticesTransformed = vertices.Select(v => new Vector3(0.5f * v.X, 0.375f * v.Z, -0.5f * v.Y));
            var shape = new ConvexHullShape(verticesTransformed);

            const float mass = 0.0f;
            Matrix startTransform = Matrix.Translation(0, -10.0f, 0); // shift
            PhysicsHelper.CreateBody(mass, startTransform, shape, _world);
        }
    }
}
