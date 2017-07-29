using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using System.Collections.Generic;

namespace RagdollDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<RagdollDemo>();
        }
    }

    internal sealed class RagdollDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(0, 1, 5);
            demo.FreeLook.Target = new Vector3(0, 1, 0);
            demo.Graphics.WindowTitle = "BulletSharp - Ragdoll Demo";
            return new RagdollDemoSimulation();
        }
    }

    internal sealed class RagdollDemoSimulation : ISimulation
    {
        private List<Ragdoll> _ragdolls = new List<Ragdoll>();

        public RagdollDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new AxisSweep3(new Vector3(-10000, -10000, -10000), new Vector3(10000, 10000, 10000));
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            //World.DispatchInfo.UseConvexConservativeDistanceUtil = true;
            //World.DispatchInfo.ConvexConservativeDistanceThreshold = 0.01f;

            CreateGround();

            SpawnRagdoll(new Vector3(1, 0.5f, 0));
            SpawnRagdoll(new Vector3(-1, 0.5f, 0));
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public void Dispose()
        {
            foreach (var ragdoll in _ragdolls)
            {
                ragdoll.Dispose();
            }

            this.StandardCleanup();
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(100, 10, 100);
            Matrix groundTransform = Matrix.Translation(0, -10, 0);
            RigidBody ground = PhysicsHelper.CreateStaticBody(groundTransform, groundShape, World);
            ground.UserObject = "Ground";
        }

        private void SpawnRagdoll(Vector3 startOffset)
        {
            var ragdoll = new Ragdoll(World, startOffset);
            _ragdolls.Add(ragdoll);
        }
    }
}
