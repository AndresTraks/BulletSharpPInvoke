using BulletSharp;
using DemoFramework;
using System;
using System.Numerics;

namespace ConstraintDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<ConstraintDemo>();
        }
    }

    internal sealed class ConstraintDemo : IDemoConfiguration
    {
        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(35, 10, 35);
            demo.FreeLook.Target = new Vector3(0, 5, 0);
            demo.IsDebugDrawEnabled = true;
            demo.DebugDrawMode = DebugDrawModes.DrawConstraints | DebugDrawModes.DrawConstraintLimits;
            demo.Graphics.WindowTitle = "BulletSharp - Constraints Demo";
            return new ConstraintDemoSimulation();
        }
    }

    internal sealed class ConstraintDemoSimulation : ISimulation
    {
        private const float CubeHalfExtent = 1.0f;
        private BoxShape _cubeShape;

        public ConstraintDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            CreateGround();

            _cubeShape = new BoxShape(CubeHalfExtent);

            CreateGears();
            CreateHingedBoxes();
            CreateMotorHinge();
            CreateMotorHinge2();
            CreateSlider();
            CreateD6Slider();
            CreateDoor();
            CreateGeneric6DofConstraint();
            CreateConeTwist();
            CreateUniversalConstraint();
            CreateGeneric6DofSpringConstraint();
            CreateHinge();
            CreateHinge2();
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
            var groundShape = new BoxShape(50, 1, 50);
            //var groundShape = new StaticPlaneShape(Vector3.UnitY, 1);
            RigidBody body = PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(0, -16, 0), groundShape, World);
            body.UserObject = "Ground";
        }

        private void CreateGears()
        {
            const float theta = (float)Math.PI / 4.0f;
            float radiusA = 2 - (float)Math.Tan(theta);
            float radiusB = 1 / (float)Math.Cos(theta);
            float ratio = radiusB / radiusA;

            Matrix4x4 transform = Matrix4x4.CreateTranslation(-8, 1, -8);
            RigidBody gearA = CreateGear(radiusA, transform);
            gearA.AngularFactor = new Vector3(0, 1, 0);

            var orientation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), -theta);
            transform = Matrix4x4.CreateFromQuaternion(orientation) * Matrix4x4.CreateTranslation(-10, 2, -8);
            RigidBody gearB = CreateGear(radiusB, transform);
            gearB.AngularVelocity = new Vector3(0, 3, 0);

            var hinge = new HingeConstraint(gearB, Vector3.Zero, new Vector3(0, 1, 0), true);
            World.AddConstraint(hinge);

            var axisA = new Vector3(0, 1, 0);
            var axisB = new Vector3(0, 1, 0);
            orientation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), -theta);
            Matrix4x4 mat = Matrix4x4.CreateFromQuaternion(orientation);
            axisB = new Vector3(mat.M21, mat.M22, mat.M23);

            var gear = new GearConstraint(gearA, gearB, axisA, axisB, ratio);
            World.AddConstraint(gear, true);
        }

        private RigidBody CreateGear(float radius, Matrix4x4 transform)
        {
            const float mass = 6.28f;

            var shape = new CompoundShape();
            var axle = new CylinderShape(0.2f, 0.25f, 0.2f);
            var wheel = new CylinderShape(radius, 0.025f, radius);
            shape.AddChildShape(Matrix4x4.Identity, axle);
            shape.AddChildShape(Matrix4x4.Identity, wheel);

            RigidBody body = PhysicsHelper.CreateBody(mass, transform, shape, World);
            body.LinearFactor = Vector3.Zero;
            return body;
        }

        private void CreateHingedBoxes()
        {
            const float mass = 1.0f;

            RigidBody body0 = PhysicsHelper.CreateBody(mass, Matrix4x4.CreateTranslation(0, 24, 0), _cubeShape, World);
            RigidBody body1 = PhysicsHelper.CreateBody(mass, Matrix4x4.CreateTranslation(2 * CubeHalfExtent, 24, 0), _cubeShape, World);

            Vector3 pivotInA = new Vector3(CubeHalfExtent, -CubeHalfExtent, -CubeHalfExtent);
            Vector3 axisInA = new Vector3(0, 0, 1);

            Matrix4x4 transform;
            Matrix4x4.Invert(body1.CenterOfMassTransform, out transform);
            transform *= body0.CenterOfMassTransform;
            Vector3 pivotInB = Vector3.Transform(pivotInA, transform);

            Matrix4x4.Invert(body1.CenterOfMassTransform, out transform);
            transform *= body1.CenterOfMassTransform;
            Vector3 axisInB = Vector3.Transform(axisInA, transform);

            //var pointToPoint = new Point2PointConstraint(body0, body1, pivotInA, pivotInB);
            //pointToPoint.DebugDrawSize = 5;
            //World.AddConstraint(pointToPoint);

            var hinge = new HingeConstraint(body0, body1, pivotInA, pivotInB, axisInA, axisInB);
            World.AddConstraint(hinge);
        }

        // Hinge connected to the world, with motor
        private void CreateMotorHinge()
        {
            const float mass = 1.0f;
            RigidBody body = PhysicsHelper.CreateBody(mass, Matrix4x4.CreateTranslation(0, 20, 0), _cubeShape, World);

            Vector3 pivotInA = new Vector3(CubeHalfExtent, -CubeHalfExtent, -CubeHalfExtent);
            Vector3 axisInA = new Vector3(0, 0, 1);

            var hinge = new HingeConstraint(body, pivotInA, axisInA);

            //use zero targetVelocity and a small maxMotorImpulse to simulate joint friction
            //const float targetVelocity = 0;
            //const float maxMotorImpulse = 0.01f;
            const float targetVelocity = 1.0f;
            const float maxMotorImpulse = 1.0f;
            hinge.EnableAngularMotor(true, targetVelocity, maxMotorImpulse);
            hinge.DebugDrawSize = 5;
            World.AddConstraint(hinge);
        }

        private void CreateMotorHinge2()
        {
            RigidBody body = PhysicsHelper.CreateBody(1.0f, Matrix4x4.Identity, _cubeShape, World);
            body.ActivationState = ActivationState.DisableDeactivation;

            Vector3 pivotInA = new Vector3(10.0f, 0.0f, 0.0f);
            Vector3 axisInA = new Vector3(0, 0, 1);

            var hinge = new HingeConstraint(body, pivotInA, axisInA);
            const float targetVelocity = -1.0f;
            const float maxMotorImpulse = 1.65f;
            hinge.EnableAngularMotor(true, targetVelocity, maxMotorImpulse);
            hinge.DebugDrawSize = 5;
            World.AddConstraint(hinge);
        }

        private void CreateSlider()
        {
            const float mass = 1.0f;

            RigidBody bodyA = PhysicsHelper.CreateBody(mass, Matrix4x4.CreateTranslation(-20, 0, 15), _cubeShape, World);
            bodyA.ActivationState = ActivationState.DisableDeactivation;

            // add dynamic rigid body B1
            RigidBody bodyB = PhysicsHelper.CreateBody(0, Matrix4x4.CreateTranslation(-30, 0, 15), _cubeShape, World);
            //RigidBody bodyB = PhysicsHelper.CreateBody(mass, Matrix.Translation(-20, 0, 15), cubeShape, World);
            bodyB.ActivationState = ActivationState.DisableDeactivation;

            // create slider constraint between A1 and B1 and add it to world
            var slider = new SliderConstraint(bodyA, bodyB, Matrix4x4.Identity, Matrix4x4.Identity, true)
            {
                LowerLinearLimit = -15.0f,
                UpperLinearLimit = -5.0f,
                //LowerLinearLimit = -10.0f,
                //UpperLinearLimit = -10.0f,
                LowerAngularLimit = -(float)Math.PI / 3.0f,
                UpperAngularLimit = (float)Math.PI / 3.0f,
                DebugDrawSize = 5.0f
            };
            World.AddConstraint(slider, true);
        }

        // Create a slider using the generic D6 constraint
        private void CreateD6Slider()
        {
            const float mass = 1.0f;

            Vector3 sliderAxis = Vector3.UnitX;
            const float angle = (float)Math.PI / 4;
            Matrix4x4 trans = Matrix4x4.CreateFromAxisAngle(sliderAxis, angle) * Matrix4x4.CreateTranslation(0, 10, 0);
            RigidBody body = PhysicsHelper.CreateBody(mass, trans, _cubeShape, World);
            body.ActivationState = ActivationState.DisableDeactivation;

            RigidBody fixedBody = PhysicsHelper.CreateStaticBody(trans, null, World);

            Matrix4x4 frameInA = Matrix4x4.CreateTranslation(0, 5, 0);
            Matrix4x4 frameInB = Matrix4x4.CreateTranslation(0, 5, 0);

            Vector3 lowerSliderLimit = new Vector3(-10, 0, 0);
            Vector3 hiSliderLimit = new Vector3(10, 0, 0);

            //const bool useLinearReferenceFrameA = false; //use fixed frame B for linear limits
            const bool useLinearReferenceFrameA = true; //use fixed frame A for linear limits
            var slider = new Generic6DofConstraint(fixedBody, body, frameInA, frameInB, useLinearReferenceFrameA)
            {
                LinearLowerLimit = lowerSliderLimit,
                LinearUpperLimit = hiSliderLimit,

                //range should be small, otherwise singularities will 'explode' the constraint
                //AngularLowerLimit = new Vector3(-1.5f,0,0),
                //AngularUpperLimit = new Vector3(1.5f,0,0),
                //AngularLowerLimit = new Vector3(0,0,0),
                //AngularUpperLimit = new Vector3(0,0,0),
                AngularLowerLimit = new Vector3((float)-Math.PI, 0, 0),
                AngularUpperLimit = new Vector3(1.5f, 0, 0),
                DebugDrawSize = 5
            };

            //slider.TranslationalLimitMotor.EnableMotor[0] = true;
            slider.TranslationalLimitMotor.TargetVelocity = new Vector3(-5.0f, 0, 0);
            slider.TranslationalLimitMotor.MaxMotorForce = new Vector3(0.1f, 0, 0);

            World.AddConstraint(slider);
        }

        // Create a door using a hinge constraint attached to the world
        private void CreateDoor()
        {
            const float mass = 1.0f;

            var doorShape = new BoxShape(2.0f, 5.0f, 0.2f);
            RigidBody doorBody = PhysicsHelper.CreateBody(mass, Matrix4x4.CreateTranslation(-5.0f, -2.0f, 0.0f), doorShape, World);
            doorBody.ActivationState = ActivationState.DisableDeactivation;

            var pivotA = new Vector3(10.0f + 2.1f, -2.0f, 0.0f); // right next to the door slightly outside
            var axisA = Vector3.UnitY; // pointing upwards, aka Y-axis

            var hinge = new HingeConstraint(doorBody, pivotA, axisA);
            hinge.DebugDrawSize = 5;

            //hinge.SetLimit(0.0f, (float)Math.PI / 2);
            // test problem values
            //hinge.SetLimit(-(float)Math.PI, (float)Math.PI * 0.8f);
            //hinge.SetLimit(1, -1);
            //hinge.SetLimit(-(float)Math.PI * 0.8f, (float)Math.PI);
            //hinge.SetLimit(-(float)Math.PI * 0.8f, (float)Math.PI, 0.9f, 0.3f, 0.0f);
            //hinge.SetLimit(-(float)Math.PI * 0.8f, (float)Math.PI, 0.9f, 0.01f, 0.0f); // "sticky limits"
            hinge.SetLimit(-(float)Math.PI * 0.25f, (float)Math.PI * 0.25f);
            //hinge.SetLimit(0, 0);
            World.AddConstraint(hinge);
        }

        private void CreateGeneric6DofConstraint()
        {
            const float mass = 1.0f;

            RigidBody fixedBody = PhysicsHelper.CreateBody(0, Matrix4x4.CreateTranslation(10, 6, 0), _cubeShape, World);
            fixedBody.ActivationState = ActivationState.DisableDeactivation;

            RigidBody dynamicbody = PhysicsHelper.CreateBody(mass, Matrix4x4.CreateTranslation(0, 6, 0), _cubeShape, World);
            dynamicbody.ActivationState = ActivationState.DisableDeactivation;

            Matrix4x4 frameInA = Matrix4x4.CreateTranslation(-5, 0, 0);
            Matrix4x4 frameInB = Matrix4x4.CreateTranslation(5, 0, 0);

            bool useLinearReferenceFrameA = true;
            var generic6Dof = new Generic6DofConstraint(fixedBody, dynamicbody, frameInA, frameInB, useLinearReferenceFrameA)
            {
                LinearLowerLimit = new Vector3(-10, -2, -1),
                LinearUpperLimit = new Vector3(10, 2, 1),
                //LinearLowerLimit = new Vector3(-10, 0, 0),
                //LinearUpperLimit = new Vector3(10, 0, 0),
                //LinearLowerLimit = new Vector3(0, 0, 0),
                //LinearUpperLimit = new Vector3(0, 0, 0),

                AngularLowerLimit = new Vector3(-(float)Math.PI / 4, -0.75f, -(float)Math.PI * 0.4f),
                AngularUpperLimit = new Vector3((float)Math.PI / 4, 0.75f, (float)Math.PI * 0.4f),
                //AngularLowerLimit = new Vector3(0, (float)Math.PI * 0.9f, 0),
                //AngularUpperLimit = new Vector3(0, -(float)Math.PI * 0.9f, 0),
                //AngularLowerLimit = new Vector3(0, 0, -(float)Math.PI),
                //AngularUpperLimit = new Vector3(0, 0, (float)Math.PI),

                //AngularLowerLimit = new Vector3(0, -0.75f, (float)Math.PI * 0.8f),
                //AngularUpperLimit = new Vector3(0, 0.75f, -(float)Math.PI * 0.8f),
                //AngularLowerLimit = new Vector3(0, -(float)Math.PI * 0.8f, (float)Math.PI * 1.98f),
                //AngularUpperLimit = new Vector3(0, (float)Math.PI * 0.8f, -(float)Math.PI * 1.98f),

                //AngularLowerLimit = new Vector3(-0.75f, -0.5f, -0.5f),
                //AngularUpperLimit = new Vector3(0.75f, 0.5f, 0.5f),
                //AngularLowerLimit = new Vector3(-1, 0, 0),
                //AngularUpperLimit = new Vector3(1, 0, 0),

                DebugDrawSize = 5.0f
            };

            //generic6Dof.TranslationalLimitMotor.EnableMotor[0] = true;
            //generic6Dof.TranslationalLimitMotor.TargetVelocity = new Vector3(5, 0, 0);
            //generic6Dof.TranslationalLimitMotor.MaxMotorForce = new Vector3(0.1f, 0, 0);

            World.AddConstraint(generic6Dof, true);
        }

        private void CreateConeTwist()
        {
            RigidBody bodyA = PhysicsHelper.CreateBody(1.0f, Matrix4x4.CreateTranslation(-10, 5, 0), _cubeShape, World);
            //bodyA = PhysicsHelper.CreateStaticBody(Matrix.Translation(-10, 5, 0), cubeShape, World);
            bodyA.ActivationState = ActivationState.DisableDeactivation;

            RigidBody bodyB = PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(-10, -5, 0), _cubeShape, World);
            //bodyB = PhysicsHelper.CreateBody(1.0f, Matrix.Translation(-10, -5, 0), cubeShape, World);

            Matrix4x4 frameInA = Matrix4x4.CreateFromYawPitchRoll(0, 0, (float)Math.PI / 2);
            frameInA *= Matrix4x4.CreateTranslation(0, -5, 0);
            Matrix4x4 frameInB = Matrix4x4.CreateFromYawPitchRoll(0, 0, (float)Math.PI / 2);
            frameInB *= Matrix4x4.CreateTranslation(0, 5, 0);

            var coneTwist = new ConeTwistConstraint(bodyA, bodyB, frameInA, frameInB);
            //coneTwist.SetLimit((float)Math.PI / 4, (float)Math.PI / 4, (float)Math.PI * 0.8f);
            //coneTwist.SetLimit((((float)Math.PI / 4) * 0.6f), (float)Math.PI / 4, (float)Math.PI * 0.8f, 1.0f); // soft limit == hard limit
            coneTwist.SetLimit((((float)Math.PI / 4) * 0.6f), (float)Math.PI / 4, (float)Math.PI * 0.8f, 0.5f);
            coneTwist.DebugDrawSize = 5;
            World.AddConstraint(coneTwist, true);
        }

        private void CreateUniversalConstraint()
        {
            // create two rigid bodies
            // static body A (parent) on top:
            RigidBody bodyA = PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(20, 4, 0), _cubeShape, World);
            bodyA.ActivationState = ActivationState.DisableDeactivation;
            // dynamic bodyB (child) below it :
            RigidBody bodyB = PhysicsHelper.CreateBody(1.0f, Matrix4x4.CreateTranslation(20, 0, 0), _cubeShape, World);
            bodyB.ActivationState = ActivationState.DisableDeactivation;

            // add some (arbitrary) data to build constraint frames
            Vector3 parentAxis = new Vector3(1, 0, 0);
            Vector3 childAxis = new Vector3(0, 0, 1);
            Vector3 anchor = new Vector3(20, 2, 0);

            var universal = new UniversalConstraint(bodyA, bodyB, anchor, parentAxis, childAxis);
            universal.SetLowerLimit(-(float)Math.PI / 4, -(float)Math.PI / 4);
            universal.SetUpperLimit((float)Math.PI / 4, (float)Math.PI / 4);

            // draw constraint frames and limits for debugging
            universal.DebugDrawSize = 5;

            World.AddConstraint(universal, true);
        }

        private void CreateGeneric6DofSpringConstraint()
        {
            RigidBody bodyA = PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(-20, 16, 0), _cubeShape, World);
            bodyA.ActivationState = ActivationState.DisableDeactivation;

            RigidBody bodyB = PhysicsHelper.CreateBody(1.0f, Matrix4x4.CreateTranslation(-10, 16, 0), _cubeShape, World);
            bodyB.ActivationState = ActivationState.DisableDeactivation;

            Matrix4x4 frameInA = Matrix4x4.CreateTranslation(10, 0, 0);
            Matrix4x4 frameInB = Matrix4x4.Identity;

            var generic6DofSpring = new Generic6DofSpringConstraint(bodyA, bodyB, frameInA, frameInB, true)
            {
                LinearUpperLimit = new Vector3(5, 0, 0),
                LinearLowerLimit = new Vector3(-5, 0, 0),
                AngularLowerLimit = new Vector3(0, 0, -1.5f),
                AngularUpperLimit = new Vector3(0, 0, 1.5f),
                DebugDrawSize = 5
            };

            generic6DofSpring.EnableSpring(0, true);
            generic6DofSpring.SetStiffness(0, 39.478f);
            generic6DofSpring.SetDamping(0, 0.5f);
            generic6DofSpring.EnableSpring(5, true);
            generic6DofSpring.SetStiffness(5, 39.478f);
            generic6DofSpring.SetDamping(0, 0.3f);
            generic6DofSpring.SetEquilibriumPoint();

            World.AddConstraint(generic6DofSpring, true);
        }

        // Create a Hinge joint between two dynamic bodies
        private void CreateHinge()
        {
            // static body (parent) on top
            RigidBody bodyA = PhysicsHelper.CreateBody(1.0f, Matrix4x4.CreateTranslation(-20, -2, 0), _cubeShape, World);
            bodyA.ActivationState = ActivationState.DisableDeactivation;
            // dynamic body
            RigidBody bodyB = PhysicsHelper.CreateBody(10.0f, Matrix4x4.CreateTranslation(-30, -2, 0), _cubeShape, World);
            bodyB.ActivationState = ActivationState.DisableDeactivation;

            // add some data to build constraint frames
            var axisA = new Vector3(0, 1, 0);
            var axisB = new Vector3(0, 1, 0);
            var pivotA = new Vector3(-5, 0, 0);
            var pivotB = new Vector3(5, 0, 0);
            var hinge = new HingeConstraint(bodyA, bodyB, pivotA, pivotB, axisA, axisB);
            hinge.SetLimit(-(float)Math.PI / 4, (float)Math.PI / 4);

            // draw constraint frames and limits for debugging
            hinge.DebugDrawSize = 5;

            World.AddConstraint(hinge, true);
        }

        private void CreateHinge2()
        {
            // static bodyA (parent) on top
            RigidBody bodyA = PhysicsHelper.CreateStaticBody(Matrix4x4.CreateTranslation(-20, 4, 0), _cubeShape, World);
            bodyA.ActivationState = ActivationState.DisableDeactivation;
            // dynamic bodyB (child) below it
            RigidBody bodyB = PhysicsHelper.CreateBody(1.0f, Matrix4x4.CreateTranslation(-20, 0, 0), _cubeShape, World);
            bodyB.ActivationState = ActivationState.DisableDeactivation;

            // add some data to build constraint frames
            Vector3 parentAxis = new Vector3(0, 1, 0);
            Vector3 childAxis = new Vector3(1, 0, 0);
            Vector3 anchor = new Vector3(-20, 0, 0);
            var hinge2 = new Hinge2Constraint(bodyA, bodyB, anchor, parentAxis, childAxis);

            hinge2.SetLowerLimit(-(float)Math.PI / 4);
            hinge2.SetUpperLimit((float)Math.PI / 4);

            // draw constraint frames and limits for debugging
            hinge2.DebugDrawSize = 5;

            World.AddConstraint(hinge2, true);
        }
    }
}
