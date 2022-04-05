﻿using BulletSharp;
using System.Numerics;

namespace DemoFramework
{
    public static class PhysicsHelper
    {
        public static RigidBody CreateBody(float mass, Matrix4x4 startTransform, CollisionShape shape, DynamicsWorld world)
        {
            // A body with zero mass is considered static
            if (mass == 0)
            {
                return CreateStaticBody(startTransform, shape, world);
            }

            // Using a motion state is recommended,
            // it provides interpolation capabilities and only synchronizes "active" objects
            var myMotionState = new DefaultMotionState(startTransform);

            Vector3 localInertia = shape.CalculateLocalInertia(mass);

            RigidBody body;
            using (var rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia))
            {
                body = new RigidBody(rbInfo);
            }

            if (world != null)
            {
                world.AddRigidBody(body);
            }

            return body;
        }

        public static RigidBody CreateStaticBody(Matrix4x4 startTransform, CollisionShape shape, DynamicsWorld world)
        {
            const float staticMass = 0;

            RigidBody body;
            using (var rbInfo = new RigidBodyConstructionInfo(staticMass, null, shape)
            {
                StartWorldTransform = startTransform
            })
            {
                body = new RigidBody(rbInfo);
            }

            if (world != null)
            {
                world.AddRigidBody(body);
            }

            return body;
        }
    }
}
