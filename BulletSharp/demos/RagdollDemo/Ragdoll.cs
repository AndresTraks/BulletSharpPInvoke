using BulletSharp;
using DemoFramework;
using System;
using System.Numerics;

namespace RagdollDemo
{
    internal enum BodyPart
    {
        Pelvis, Spine, Head,
        LeftUpperLeg, LeftLowerLeg,
        RightUpperLeg, RightLowerLeg,
        LeftUpperArm, LeftLowerArm,
        RightUpperArm, RightLowerArm,
        Count
    };

    internal enum Joint
    {
        PelvisSpine, SpineHead,
        LeftHip, LeftKnee,
        RightHip, RightKnee,
        LeftShoulder, LeftElbow,
        RightShoulder, RightElbow,
        Count
    };

    internal sealed class Ragdoll : IDisposable
    {
        private const float ConstraintDebugSize = 0.2f;
        private const float PI_2 = (float)Math.PI / 2;
        private const float PI_4 = (float)Math.PI / 4;

        private DynamicsWorld _world;
        private CollisionShape[] _shapes = new CollisionShape[(int)BodyPart.Count];
        private RigidBody[] _bodies = new RigidBody[(int)BodyPart.Count];
        private TypedConstraint[] _joints = new TypedConstraint[(int)Joint.Count];

        public Ragdoll(DynamicsWorld ownerWorld, Vector3 positionOffset)
        {
            _world = ownerWorld;

            SetupShapes();
            SetupBodies(positionOffset);
            SetupConstraints();
        }

        public void Dispose()
        {
            foreach (var joint in _joints)
            {
                _world.RemoveConstraint(joint);
                joint.Dispose();
            }

            foreach (var body in _bodies)
            {
                _world.RemoveRigidBody(body);
                body.MotionState.Dispose();
                body.Dispose();
            }

            foreach (var shape in _shapes)
            {
                shape.Dispose();
            }
        }

        private void SetupShapes()
        {
            _shapes[(int)BodyPart.Pelvis] = new CapsuleShape(0.15f, 0.20f);
            _shapes[(int)BodyPart.Spine] = new CapsuleShape(0.15f, 0.28f);
            _shapes[(int)BodyPart.Head] = new CapsuleShape(0.10f, 0.05f);
            _shapes[(int)BodyPart.LeftUpperLeg] = new CapsuleShape(0.07f, 0.45f);
            _shapes[(int)BodyPart.LeftLowerLeg] = new CapsuleShape(0.05f, 0.37f);
            _shapes[(int)BodyPart.RightUpperLeg] = new CapsuleShape(0.07f, 0.45f);
            _shapes[(int)BodyPart.RightLowerLeg] = new CapsuleShape(0.05f, 0.37f);
            _shapes[(int)BodyPart.LeftUpperArm] = new CapsuleShape(0.05f, 0.33f);
            _shapes[(int)BodyPart.LeftLowerArm] = new CapsuleShape(0.04f, 0.25f);
            _shapes[(int)BodyPart.RightUpperArm] = new CapsuleShape(0.05f, 0.33f);
            _shapes[(int)BodyPart.RightLowerArm] = new CapsuleShape(0.04f, 0.25f);
        }

        private void SetupBodies(Vector3 positionOffset)
        {
            Matrix4x4 offset = Matrix4x4.CreateTranslation(positionOffset);
            Matrix4x4 transform;
            transform = offset * Matrix4x4.CreateTranslation(0, 1, 0);
            _bodies[(int)BodyPart.Pelvis] = CreateBody(1, transform, _shapes[(int)BodyPart.Pelvis]);

            transform = offset * Matrix4x4.CreateTranslation(0, 1.2f, 0);
            _bodies[(int)BodyPart.Spine] = CreateBody(1, transform, _shapes[(int)BodyPart.Spine]);

            transform = offset * Matrix4x4.CreateTranslation(0, 1.6f, 0);
            _bodies[(int)BodyPart.Head] = CreateBody(1, transform, _shapes[(int)BodyPart.Head]);

            transform = offset * Matrix4x4.CreateTranslation(-0.18f, 0.6f, 0);
            _bodies[(int)BodyPart.LeftUpperLeg] = CreateBody(1, transform, _shapes[(int)BodyPart.LeftUpperLeg]);

            transform = offset * Matrix4x4.CreateTranslation(-0.18f, 0.2f, 0);
            _bodies[(int)BodyPart.LeftLowerLeg] = CreateBody(1, transform, _shapes[(int)BodyPart.LeftLowerLeg]);

            transform = offset * Matrix4x4.CreateTranslation(0.18f, 0.65f, 0);
            _bodies[(int)BodyPart.RightUpperLeg] = CreateBody(1, transform, _shapes[(int)BodyPart.RightUpperLeg]);

            transform = offset * Matrix4x4.CreateTranslation(0.18f, 0.2f, 0);
            _bodies[(int)BodyPart.RightLowerLeg] = CreateBody(1, transform, _shapes[(int)BodyPart.RightLowerLeg]);

            transform = Matrix4x4.CreateRotationZ(PI_2) * offset * Matrix4x4.CreateTranslation(-0.35f, 1.45f, 0);
            _bodies[(int)BodyPart.LeftUpperArm] = CreateBody(1, transform, _shapes[(int)BodyPart.LeftUpperArm]);

            transform = Matrix4x4.CreateRotationZ(PI_2) * offset * Matrix4x4.CreateTranslation(-0.7f, 1.45f, 0);
            _bodies[(int)BodyPart.LeftLowerArm] = CreateBody(1, transform, _shapes[(int)BodyPart.LeftLowerArm]);

            transform = Matrix4x4.CreateRotationZ(-PI_2) * offset * Matrix4x4.CreateTranslation(0.35f, 1.45f, 0);
            _bodies[(int)BodyPart.RightUpperArm] = CreateBody(1, transform, _shapes[(int)BodyPart.RightUpperArm]);

            transform = Matrix4x4.CreateRotationZ(-PI_2) * offset * Matrix4x4.CreateTranslation(0.7f, 1.45f, 0);
            _bodies[(int)BodyPart.RightLowerArm] = CreateBody(1, transform, _shapes[(int)BodyPart.RightLowerArm]);

            // Some damping on the bodies
            foreach (RigidBody body in _bodies)
            {
                body.SetDamping(0.05f, 0.85f);
                body.DeactivationTime = 0.8f;
                body.SetSleepingThresholds(1.6f, 2.5f);
            }
        }

        private void SetupConstraints()
        {
            HingeConstraint hinge;
            ConeTwistConstraint cone;

            Matrix4x4 localA, localB;

            localA = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, 0.15f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, -0.15f, 0);
            hinge = new HingeConstraint(_bodies[(int)BodyPart.Pelvis], _bodies[(int)BodyPart.Spine], localA, localB);
            hinge.SetLimit(-PI_4, PI_2);
            _joints[(int)Joint.PelvisSpine] = hinge;
            hinge.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.PelvisSpine], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(0, 0, PI_2) * Matrix4x4.CreateTranslation(0, 0.30f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(0, 0, PI_2) * Matrix4x4.CreateTranslation(0, -0.14f, 0);
            cone = new ConeTwistConstraint(_bodies[(int)BodyPart.Spine], _bodies[(int)BodyPart.Head], localA, localB);
            cone.SetLimit(PI_4, PI_4, PI_2);
            _joints[(int)Joint.SpineHead] = cone;
            cone.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.SpineHead], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(0, 0, -PI_4 * 5) * Matrix4x4.CreateTranslation(-0.18f, -0.18f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(0, 0, -PI_4 * 5) * Matrix4x4.CreateTranslation(0, 0.225f, 0);
            cone = new ConeTwistConstraint(_bodies[(int)BodyPart.Pelvis], _bodies[(int)BodyPart.LeftUpperLeg], localA, localB);
            cone.SetLimit(PI_4, PI_4, 0);
            _joints[(int)Joint.LeftHip] = cone;
            cone.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.LeftHip], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, -0.225f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, 0.185f, 0);
            hinge = new HingeConstraint(_bodies[(int)BodyPart.LeftUpperLeg], _bodies[(int)BodyPart.LeftLowerLeg], localA, localB);
            hinge.SetLimit(0, PI_2);
            _joints[(int)Joint.LeftKnee] = hinge;
            hinge.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.LeftKnee], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(0, 0, PI_4) * Matrix4x4.CreateTranslation(0.18f, -0.10f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(0, 0, PI_4) * Matrix4x4.CreateTranslation(0, 0.225f, 0);
            cone = new ConeTwistConstraint(_bodies[(int)BodyPart.Pelvis], _bodies[(int)BodyPart.RightUpperLeg], localA, localB);
            cone.SetLimit(PI_4, PI_4, 0);
            _joints[(int)Joint.RightHip] = cone;
            cone.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.RightHip], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, -0.225f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, 0.185f, 0);
            hinge = new HingeConstraint(_bodies[(int)BodyPart.RightUpperLeg], _bodies[(int)BodyPart.RightLowerLeg], localA, localB);
            hinge.SetLimit(0, PI_2);
            _joints[(int)Joint.RightKnee] = hinge;
            hinge.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.RightKnee], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(0, 0, (float)Math.PI) * Matrix4x4.CreateTranslation(-0.2f, 0.15f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(0, 0, PI_2) * Matrix4x4.CreateTranslation(0, -0.18f, 0);
            cone = new ConeTwistConstraint(_bodies[(int)BodyPart.Spine], _bodies[(int)BodyPart.LeftUpperArm], localA, localB);
            cone.SetLimit(PI_2, PI_2, 0);
            _joints[(int)Joint.LeftShoulder] = cone;
            cone.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.LeftShoulder], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, 0.18f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, -0.14f, 0);
            hinge = new HingeConstraint(_bodies[(int)BodyPart.LeftUpperArm], _bodies[(int)BodyPart.LeftLowerArm], localA, localB);
            hinge.SetLimit(0, PI_2);
            _joints[(int)Joint.LeftElbow] = hinge;
            hinge.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.LeftElbow], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(0, 0, 0) * Matrix4x4.CreateTranslation(0.2f, 0.15f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(0, 0, PI_2) * Matrix4x4.CreateTranslation(0, -0.18f, 0);
            cone = new ConeTwistConstraint(_bodies[(int)BodyPart.Spine], _bodies[(int)BodyPart.RightUpperArm], localA, localB);
            cone.SetLimit(PI_2, PI_2, 0);
            _joints[(int)Joint.RightShoulder] = cone;
            cone.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.RightShoulder], true);


            localA = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, 0.18f, 0);
            localB = Matrix4x4.CreateFromYawPitchRoll(PI_2, 0, 0) * Matrix4x4.CreateTranslation(0, -0.14f, 0);
            hinge = new HingeConstraint(_bodies[(int)BodyPart.RightUpperArm], _bodies[(int)BodyPart.RightLowerArm], localA, localB);
            //hinge.SetLimit(-PI_2, 0);
            hinge.SetLimit(0, PI_2);
            _joints[(int)Joint.RightElbow] = hinge;
            hinge.DebugDrawSize = ConstraintDebugSize;

            _world.AddConstraint(_joints[(int)Joint.RightElbow], true);
        }

        private RigidBody CreateBody(float mass, Matrix4x4 startTransform, CollisionShape shape)
        {
            return PhysicsHelper.CreateBody(mass, startTransform, shape, _world);
        }
    }
}
