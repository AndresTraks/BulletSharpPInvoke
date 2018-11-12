using BulletSharp;
using System;
using System.Collections.Generic;

namespace DemoFramework
{
    public static class SimulationExtensions
    {
        public static void StandardCleanup(this ISimulation simulation)
        {
            CleanupConstraints(simulation.World);
            CleanupBodiesAndShapes(simulation.World);

            var multiBodyWorld = simulation.World as MultiBodyDynamicsWorld;
            if (multiBodyWorld != null)
            {
                CleanupMultiBodyWorld(multiBodyWorld);
            }

            simulation.World.Dispose();
            simulation.Broadphase.Dispose();
            simulation.Dispatcher.Dispose();
            simulation.CollisionConfiguration.Dispose();
            
            if (BulletObjectTracker.Current != null)
            {
                if (BulletObjectTracker.Current.UserOwnedObjects.Count != 0)
                {
                    throw new Exception("Bullet has active objects that were not disposed.");
                }
            }
        }

        private static void CleanupConstraints(DynamicsWorld world)
        {
            var nonWorldObjects = new HashSet<CollisionObject>();

            for (int i = world.NumConstraints - 1; i >= 0; i--)
            {
                TypedConstraint constraint = world.GetConstraint(i);
                world.RemoveConstraint(constraint);
                if (constraint.RigidBodyA.BroadphaseHandle == null)
                {
                    nonWorldObjects.Add(constraint.RigidBodyA);
                }
                if (constraint.RigidBodyB.BroadphaseHandle == null)
                {
                    nonWorldObjects.Add(constraint.RigidBodyB);
                }
                constraint.Dispose();
            }

            foreach (var obj in nonWorldObjects)
            {
                obj.Dispose();
            }
        }

        private static void CleanupBodiesAndShapes(DynamicsWorld world)
        {
            var shapes = new HashSet<CollisionShape>();

            for (int i = world.NumCollisionObjects - 1; i >= 0; i--)
            {
                CollisionObject obj = world.CollisionObjectArray[i];
                var rigidBody = obj as RigidBody;
                if (rigidBody != null && rigidBody.MotionState != null)
                {
                    rigidBody.MotionState.Dispose();
                }
                world.RemoveCollisionObject(obj);
                GetShapeWithChildShapes(obj.CollisionShape, shapes);

                obj.Dispose();
            }

            foreach (var shape in shapes)
            {
                shape.Dispose();
            }
        }

        private static void CleanupMultiBodyWorld(MultiBodyDynamicsWorld world)
        {
            for (int i = world.NumMultiBodyConstraints - 1; i >= 0; i--)
            {
                MultiBodyConstraint multiBodyConstraint = world.GetMultiBodyConstraint(i);
                world.RemoveMultiBodyConstraint(multiBodyConstraint);
                multiBodyConstraint.Dispose();
            }

            for (int i = world.NumMultibodies - 1; i >= 0; i--)
            {
                MultiBody multiBody = world.GetMultiBody(i);
                world.RemoveMultiBody(multiBody);
                multiBody.Dispose();
            }
        }

        private static void GetShapeWithChildShapes(CollisionShape shape, HashSet<CollisionShape> shapes)
        {
            shapes.Add(shape);

            var convex2DShape = shape as Convex2DShape;
            if (convex2DShape != null)
            {
                GetShapeWithChildShapes(convex2DShape.ChildShape, shapes);
                return;
            }

            var compoundShape = shape as CompoundShape;
            if (compoundShape != null)
            {
                foreach (var childShape in compoundShape.ChildList)
                {
                    GetShapeWithChildShapes(childShape.ChildShape, shapes);
                }
                return;
            }

            var scaledTriangleMeshShape = shape as ScaledBvhTriangleMeshShape;
            if (scaledTriangleMeshShape != null)
            {
                GetShapeWithChildShapes(scaledTriangleMeshShape.ChildShape, shapes);
                return;
            }

            var uniformScalingShape = shape as UniformScalingShape;
            if (uniformScalingShape != null)
            {
                GetShapeWithChildShapes(uniformScalingShape.ChildShape, shapes);
                return;
            }
        }
    }
}
