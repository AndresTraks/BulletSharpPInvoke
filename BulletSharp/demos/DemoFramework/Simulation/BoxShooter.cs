using BulletSharp;
using BulletSharp.Math;
using System;
using System.Linq;

namespace DemoFramework
{
    public sealed class BoxShooter : IDisposable
    {
        private readonly DynamicsWorld _world;

        private BoxShape _shootBoxShape;
        private const double ShootBoxInitialSpeed = 40;
        private const double BoxHalfExtent = 1.0f;

        public BoxShooter(DynamicsWorld world)
        {
            _world = world;
        }

        public void Shoot(Vector3 eyePosition, Vector3 destination)
        {
            const double mass = 1.0f;

            if (_shootBoxShape == null)
            {
                _shootBoxShape = new BoxShape(BoxHalfExtent);
                //shootBoxShape.InitializePolyhedralFeatures();
            }

            RigidBody body = PhysicsHelper.CreateBody(mass, Matrix.Translation(eyePosition), _shootBoxShape, _world);
            body.LinearFactor = new Vector3(1, 1, 1);
            //body.Restitution = 1;

            Vector3 linVel = destination - eyePosition;
            linVel.Normalize();
            body.LinearVelocity = linVel * ShootBoxInitialSpeed;
            body.CcdMotionThreshold = 0.5f;
            body.CcdSweptSphereRadius = 0.9f;
        }

        public void Dispose()
        {
            if (_shootBoxShape != null)
            {
                var objects = _world.CollisionObjectArray
                    .Where(o => o.CollisionShape == _shootBoxShape);
                foreach (var obj in objects)
                {
                    _world.RemoveCollisionObject(obj);
                    obj.Dispose();
                }

                _shootBoxShape.Dispose();
                _shootBoxShape = null;
            }
        }
    }
}
