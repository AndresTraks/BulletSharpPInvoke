using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using System.Windows.Forms;

namespace CcdPhysicsDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<CcdPhysicsDemo>();
        }
    }

    internal sealed class CcdPhysicsDemo : IDemoConfiguration, IUpdateReceiver
    {
        private bool _ccdEnabled = true;

        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(0, 10, 40);
            demo.FreeLook.Target = Vector3.Zero;
            demo.Graphics.WindowTitle = "BulletSharp - Continous Collision Detection Demo";
            SetDemoText(demo);
            return new CcdPhysicsDemoSimulation(_ccdEnabled);
        }

        public void Update(Demo demo)
        {
            var keys = demo.Input.KeysPressed;
            if (keys.Contains(Keys.P))
            {
                _ccdEnabled = !_ccdEnabled;
                SetDemoText(demo);
                demo.ResetScene();
            }
            else if (keys.Contains(Keys.Space))
            {
                var ccdDemo = demo.Simulation as CcdPhysicsDemoSimulation;
                Vector3 destination = demo.GetCameraRayTo();
                ccdDemo.ShootBox(demo.FreeLook.Eye, destination);
                keys.Remove(Keys.Space);
            }
        }

        private void SetDemoText(Demo demo)
        {
            if (_ccdEnabled)
            {
                demo.DemoText = "CCD enabled (P to disable)";
            }
            else
            {
                demo.DemoText = "CCD enabled (P to enable)";
            }
        }
    }

    internal sealed class CcdPhysicsDemoSimulation : ISimulation
    {
        private const float CubeHalfExtents = 0.5f;
        private const float ExtraHeight = 0.5f;
        private const float ShootBoxInitialSpeed = 2000;

        private readonly bool _ccdEnabled;
        private BoxShape _shootBoxShape = new BoxShape(1);

        public CcdPhysicsDemoSimulation(bool ccdEnabled)
        {
            _ccdEnabled = ccdEnabled;

            CollisionConfiguration = new DefaultCollisionConfiguration();

            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            //Dispatcher.RegisterCollisionCreateFunc(BroadphaseNativeType.BoxShape, BroadphaseNativeType.BoxShape,
            //    CollisionConf.GetCollisionAlgorithmCreateFunc(BroadphaseNativeType.ConvexShape, BroadphaseNativeType.ConvexShape));

            Broadphase = new DbvtBroadphase();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);
            World.SolverInfo.SolverMode |= SolverModes.Use2FrictionDirections | SolverModes.RandomizeOrder;
            //World.SolverInfo.SplitImpulse = 0;
            World.SolverInfo.NumIterations = 20;

            World.DispatchInfo.UseContinuous = _ccdEnabled;

            CreateGround();
            CreateBoxStack();
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public void ShootBox(Vector3 camPos, Vector3 destination)
        {
            const float mass = 1.0f;

            if (_shootBoxShape == null)
            {
                _shootBoxShape = new BoxShape(0.5f);
                _shootBoxShape.InitializePolyhedralFeatures();
            }

            RigidBody body = PhysicsHelper.CreateBody(mass, Matrix.Translation(camPos), _shootBoxShape, World);
            body.LinearFactor = new Vector3(0.5f);
            //body.Restitution = 1;

            Vector3 linVel = destination - camPos;
            linVel.Normalize();
            body.LinearVelocity = linVel * ShootBoxInitialSpeed;
            body.AngularVelocity = Vector3.Zero;
            body.ContactProcessingThreshold = 1e30f;

            // when using CCD mode, disable regular CCD
            if (_ccdEnabled)
            {
                body.CcdMotionThreshold = 0.00005f;
                body.CcdSweptSphereRadius = 0.2f;
            }
        }

        public void Dispose()
        {
            this.StandardCleanup();
        }

        private void CreateGround()
        {
            var ground = new BoxShape(100, 0.5f, 100);
            ground.InitializePolyhedralFeatures();
            RigidBody body = PhysicsHelper.CreateStaticBody(Matrix.Identity, ground, World);
            body.Friction = 0.5f;
            //body.RollingFriction = 0.3f;
            body.UserObject = "Ground";
        }

        private void CreateBoxStack()
        {
            //var shape = new CylinderShape(CubeHalfExtents, CubeHalfExtents, CubeHalfExtents);
            var shape = new BoxShape(CubeHalfExtents);

            const int numObjects = 120;
            for (int i = 0; i < numObjects; i++)
            {
                //stack them
                const int colsize = 10;
                int row = (int)((i * CubeHalfExtents * 2) / (colsize * 2 * CubeHalfExtents));
                int row2 = row;
                int col = (i) % (colsize) - colsize / 2;

                if (col > 3)
                {
                    col = 11;
                    row2 |= 1;
                }

                Matrix trans = Matrix.Translation(col * 2 * CubeHalfExtents + (row2 % 2) * CubeHalfExtents,
                    row * 2 * CubeHalfExtents + CubeHalfExtents + ExtraHeight, 0);

                RigidBody body = PhysicsHelper.CreateBody(1, trans, shape, World);
                body.SetAnisotropicFriction(shape.AnisotropicRollingFrictionDirection, AnisotropicFrictionFlags.RollingFriction);
                body.Friction = 0.5f;
                //body.RollingFriction = 0.3f;

                if (_ccdEnabled)
                {
                    body.CcdMotionThreshold = 1e-7f;
                    body.CcdSweptSphereRadius = 0.9f * CubeHalfExtents;
                }
            }
        }
    }
}
