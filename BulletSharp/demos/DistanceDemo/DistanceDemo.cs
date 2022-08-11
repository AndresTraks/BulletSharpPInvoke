using BulletSharp;
using DemoFramework;
using System;
using System.Drawing;
using System.Numerics;

namespace DistanceDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<DistanceDemo>();
        }
    }

    internal sealed class DistanceDemo : IDemoConfiguration, IUpdateReceiver
    {
        private float _rotation = 0;

        public ISimulation CreateSimulation(Demo demo)
        {
            _rotation = 0;
            demo.FreeLook.Eye = new Vector3(30, 20, 10);
            demo.FreeLook.Target = new Vector3(0, 5, -4);
            demo.Graphics.WindowTitle = "BulletSharp - Distance Demo";
            demo.IsDebugDrawEnabled = true;
            return new DistanceDemoSimulation();
        }

        public void Update(Demo demo)
        {
            var simulation = demo.Simulation as DistanceDemoSimulation;

            _rotation += demo.FrameDelta;
            simulation.SetRotation(_rotation);

            if (demo.IsDebugDrawEnabled)
            {
                simulation.DrawDistance();
            }
        }
    }

    internal sealed class DistanceDemoSimulation : ISimulation
    {
        private readonly Matrix4x4 _rotBodyPosition = Matrix4x4.CreateTranslation(0, 10, 0);
        private RigidBody _rotatingBody, _staticBody;
        private ConvexShape _rotatingShape, _staticShape;

        private VoronoiSimplexSolver _gjkSimplexSolver = new VoronoiSimplexSolver();

        private static Vector3 _red = new Vector3(1.0f, 0.0f, 0.0f);

        public DistanceDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            CreateGround();

            Vector3[] rotatingPoints = {
                new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1)
            };
            _rotatingShape = new ConvexHullShape(rotatingPoints);
            _rotatingBody = PhysicsHelper.CreateStaticBody(_rotBodyPosition, _rotatingShape, World);
            _rotatingBody.CollisionFlags |= CollisionFlags.KinematicObject;
            _rotatingBody.ActivationState = ActivationState.DisableDeactivation;

            Vector3[] staticPoints = {
                new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(0,0,-1), new Vector3(-1,-1,0)
            };
            _staticShape = new ConvexHullShape(staticPoints);
            Matrix4x4 staticBodyPosition = Matrix4x4.CreateTranslation(0, 5, 0);
            _staticBody = PhysicsHelper.CreateStaticBody(staticBodyPosition, _staticShape, World);
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        internal void SetRotation(float rotation)
        {
            _rotatingBody.CenterOfMassTransform = Matrix4x4.CreateRotationX(rotation) * _rotBodyPosition;
            _rotatingBody.WorldTransform = _rotatingBody.CenterOfMassTransform;
        }

        public void DrawDistance()
        {
            var input = new DiscreteCollisionDetectorInterface.ClosestPointInput
            {
                TransformA = _rotatingBody.CenterOfMassTransform,
                TransformB = _staticBody.CenterOfMassTransform
            };

            using (var result = new PointCollector())
            {
                using (var detector = new GjkPairDetector(_rotatingShape, _staticShape, _gjkSimplexSolver, null))
                {
                    detector.CachedSeparatingAxis = new Vector3(0.00000000f, 0.059727669f, 0.29259586f);
                    detector.GetClosestPoints(input, result, null);
                }

                if (result.HasResult)
                {
                    Vector3 distanceFrom = result.PointInWorld;
                    Vector3 distanceTo = result.PointInWorld + result.NormalOnBInWorld * result.Distance;
                    World.DebugDrawer.DrawLine(ref distanceFrom, ref distanceTo, ref _red);
                }
            }

            input.Dispose();
        }

        public void Dispose()
        {
            _gjkSimplexSolver.Dispose();

            this.StandardCleanup();
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(50, 1, 50);
            CollisionObject ground = PhysicsHelper.CreateStaticBody(Matrix4x4.Identity, groundShape, World);
            ground.UserObject = "Ground";
        }
    }
}
