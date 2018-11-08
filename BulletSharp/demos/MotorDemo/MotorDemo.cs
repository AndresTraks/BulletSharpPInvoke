using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using System;
using System.Collections.Generic;

namespace MotorDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<MotorDemo>();
        }
    }

    internal sealed class MotorDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(8, 4, 8);
            demo.FreeLook.Target = Vector3.Zero;
            demo.DebugDrawMode = DebugDrawModes.DrawConstraintLimits | DebugDrawModes.DrawConstraints | DebugDrawModes.DrawWireframe;
            demo.Graphics.WindowTitle = "BulletSharp - Motor Demo";
            return new MotorDemoSimulation();
        }
    }

    internal sealed class MotorDemoSimulation : ISimulation
    {
        private List<TestRig> _rigs = new List<TestRig>();
        private float _time = 0;
        private float _cyclePeriod = 2000.0f;
        private float _muscleStrength = 0.5f;

        private const int NumLegs = 6;

        public MotorDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            var worldAabbMin = new Vector3(-10000, -10000, -10000);
            var worldAabbMax = new Vector3(10000, 10000, 10000);
            Broadphase = new AxisSweep3(worldAabbMin, worldAabbMax);

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);
            World.SetInternalTickCallback(MotorPreTickCallback, this, true);

            CreateGround();

            SpawnTestRig(new Vector3(1, 0.5f, 0), false);
            SpawnTestRig(new Vector3(-2, 0.5f, 0), true);
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public void Dispose()
        {
            foreach (var testRig in _rigs)
            {
                testRig.Dispose();
            }
            _rigs.Clear();

            this.StandardCleanup();
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(200, 10, 200);
            CollisionObject ground = PhysicsHelper.CreateStaticBody(Matrix.Translation(0, -10, 0), groundShape, World);
            ground.UserObject = "Ground";
        }

        private void MotorPreTickCallback(DynamicsWorld world, float timeStep)
        {
            SetMotorTargets(timeStep);
        }

        private void SpawnTestRig(Vector3 startOffset, bool isFixed)
        {
            var rig = new TestRig(World, NumLegs, startOffset, isFixed);
            _rigs.Add(rig);
        }

        private void SetMotorTargets(float deltaTime)
        {
            float deltaTimeMs = deltaTime * 1000000.0f;
            float minFps = 1000000.0f / 60.0f;
            if (deltaTimeMs > minFps)
                deltaTimeMs = minFps;

            _time += deltaTimeMs;

            // set per-frame sinusoidal position targets using angular motor (hacky?)
            foreach (var rig in _rigs)
            {
                foreach (var leg in rig.Legs)
                {
                    SetJointMotorTarget(leg.Hip, deltaTimeMs);
                    SetJointMotorTarget(leg.Knee, deltaTimeMs);
                }
            }
        }

        private void SetJointMotorTarget(HingeConstraint joint, float deltaTimeMs)
        {
            float currentAngle = joint.HingeAngle;

            float targetPercent = ((int)(_time / 1000.0f) % (int)_cyclePeriod) / _cyclePeriod;
            float targetAngle = (float)(0.5 * (1 + Math.Sin(2.0f * Math.PI * targetPercent)));
            float targetLimitAngle = joint.LowerLimit + targetAngle * (joint.UpperLimit - joint.LowerLimit);
            float angleError = targetLimitAngle - currentAngle;
            float desiredAngularVel = 1000000.0f * angleError / deltaTimeMs;
            joint.EnableAngularMotor(true, desiredAngularVel, _muscleStrength);
        }
    }

    internal sealed class TestRig
    {
        private const float BodyRadius = 0.25f;
        private const float ThighLength = 0.45f;
        private const float ShinLength = 0.75f;

        private const float PI_2 = (float)(0.5f * Math.PI);
        private const float PI_4 = (float)(0.25f * Math.PI);
        private const float PI_8 = (float)(0.125f * Math.PI);

        private readonly DynamicsWorld _world;

        private readonly CollisionShape _bodyShape;
        private readonly CollisionShape _thighShape;
        private readonly CollisionShape _shinShape;

        private RigidBody _bodyObject;

        public TestRig(DynamicsWorld ownerWorld, int numLegs, Vector3 position, bool isFixed)
        {
            _world = ownerWorld;

            Legs = new Leg[numLegs];
            for (int i = 0; i < numLegs; i++)
            {
                Legs[i] = new Leg();
            }

            _bodyShape = new CapsuleShape(BodyRadius, 0.10f);
            _thighShape = new CapsuleShape(0.1f, ThighLength);
            _shinShape = new CapsuleShape(0.08f, ShinLength);

            SetupRigidBodies(position, isFixed);
            SetupConstraints();
        }

        public Leg[] Legs { get; }

        public void Dispose()
        {
            foreach (var leg in Legs)
            {
                _world.RemoveConstraint(leg.Hip);
                leg.Hip.Dispose();
                _world.RemoveConstraint(leg.Knee);
                leg.Knee.Dispose();
            }

            DisposeRigidBody(_bodyObject);
            foreach (var leg in Legs)
            {
                DisposeRigidBody(leg.Thigh);
                DisposeRigidBody(leg.Shin);
            }

            _bodyShape.Dispose();
            _thighShape.Dispose();
            _shinShape.Dispose();
        }

        private void DisposeRigidBody(RigidBody body)
        {
            _world.RemoveRigidBody(body);
            if (body.MotionState != null)
            {
                body.MotionState.Dispose();
            }
            body.Dispose();
        }

        private void SetupRigidBodies(Vector3 position, bool isFixed)
        {
            const float heightFromGround = 0.5f;
            Matrix offset = Matrix.Translation(position);

            // body
            Vector3 rootPosition = new Vector3(0, heightFromGround, 0);
            Matrix transform = Matrix.Translation(rootPosition);
            float mass = isFixed ? 0 : 1;
            _bodyObject = PhysicsHelper.CreateBody(mass, transform * offset, _bodyShape, _world);

            // legs
            for (int i = 0; i < Legs.Length; i++)
            {
                Leg leg = Legs[i];

                float fAngle = (float)(2 * Math.PI * i / Legs.Length);
                float fSin = (float)Math.Sin(fAngle);
                float fCos = (float)Math.Cos(fAngle);

                Vector3 boneOrigin = new Vector3(fCos * (BodyRadius + 0.5f * ThighLength), heightFromGround, fSin * (BodyRadius + 0.5f * ThighLength));

                Vector3 vToBone = boneOrigin - rootPosition;
                vToBone.Normalize();
                Vector3 vUp = Vector3.UnitY;
                Vector3 vAxis = Vector3.Cross(vToBone, vUp);
                
                transform = Matrix.RotationAxis(vAxis, PI_2) * Matrix.Translation(boneOrigin);
                leg.Thigh = PhysicsHelper.CreateBody(1, transform * offset, _thighShape, _world);

                transform = Matrix.Translation(fCos * (BodyRadius + ThighLength), heightFromGround - 0.5f * ShinLength, fSin * (BodyRadius + ThighLength));
                leg.Shin = PhysicsHelper.CreateBody(1, transform * offset, _shinShape, _world);

                SetBodyDamping(leg.Thigh);
                SetBodyDamping(leg.Shin);
            }
        }

        private void SetBodyDamping(RigidBody body)
        {
            body.SetDamping(0.05f, 0.85f);
            body.DeactivationTime = 0.8f;
            //body.SetSleepingThresholds(1.6f, 2.5f);
            body.SetSleepingThresholds(0.5f, 0.5f);
        }

        private void SetupConstraints()
        {
            Matrix localA, localB, localC;

            for (int i = 0; i < Legs.Length; i++)
            {
                Leg leg = Legs[i];

                float legAngle = (float)(2 * Math.PI * i / Legs.Length);
                float fSin = (float)Math.Sin(legAngle);
                float fCos = (float)Math.Cos(legAngle);

                localA = Matrix.RotationYawPitchRoll(-legAngle, 0, 0) * Matrix.Translation(fCos * BodyRadius, 0, fSin * BodyRadius);
                localB = localA * _bodyObject.WorldTransform * Matrix.Invert(leg.Thigh.WorldTransform);
                leg.Hip = new HingeConstraint(_bodyObject, leg.Thigh, localA, localB);
                leg.Hip.SetLimit(-0.75f * PI_4, PI_8);
                //leg.Hip.SetLimit(-0.1f, 0.1f);
                _world.AddConstraint(leg.Hip, true);

                localA = Matrix.RotationYawPitchRoll(-legAngle, 0, 0) * Matrix.Translation(fCos * (BodyRadius + ThighLength), 0, fSin * (BodyRadius + ThighLength));
                localB = localA * _bodyObject.WorldTransform * Matrix.Invert(leg.Thigh.WorldTransform);
                localC = localA * _bodyObject.WorldTransform * Matrix.Invert(leg.Shin.WorldTransform);
                leg.Knee = new HingeConstraint(leg.Thigh, leg.Shin, localB, localC);
                //leg.Knee.SetLimit(-0.01f, 0.01f);
                leg.Knee.SetLimit(-PI_8, 0.2f);
                _world.AddConstraint(leg.Knee, true);
            }
        }
    };

    public sealed class Leg
    {
        public RigidBody Thigh { get; set; }
        public RigidBody Shin { get; set; }
        public HingeConstraint Hip { get; set; }
        public HingeConstraint Knee { get; set; }
    }
}
