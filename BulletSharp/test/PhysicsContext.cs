using System;
using System.Numerics;
using BulletSharp;

namespace BulletSharpTest
{
    public sealed class PhysicsContext : IDisposable
    {
        private DefaultCollisionConfiguration _conf;
        private CollisionDispatcher _dispatcher;
        private bool _isDisposed;

        public DiscreteDynamicsWorld World { get; private set; }
        public BroadphaseInterface Broadphase { get; private set; }

        public void InitializeWorld()
        {
            _conf = new DefaultCollisionConfiguration();
            _dispatcher = new CollisionDispatcher(_conf);
            //_broadphase = new AxisSweep3(new Vector3(-1000, -1000, -1000), new Vector3(1000, 1000, 1000));
            Broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(_dispatcher, Broadphase, null, _conf);
        }

        public RigidBody AddBody(CollisionShape shape, Matrix4x4 transform, float mass)
        {
            using (var info = new RigidBodyConstructionInfo(mass, null, shape, Vector3.Zero))
            {
                info.LocalInertia = info.CollisionShape.CalculateLocalInertia(mass);
                info.StartWorldTransform = transform;
                var body = new RigidBody(info);
                if (World != null)
                {
                    World.AddRigidBody(body);
                }
                return body;
            }
        }

        public RigidBody AddStaticBody(CollisionShape shape, Matrix4x4 transform)
        {
            const float mass = 0;
            using (var info = new RigidBodyConstructionInfo(mass, null, shape, Vector3.Zero))
            {
                info.StartWorldTransform = transform;
                var body = new RigidBody(info);
                if (World != null)
                {
                    World.AddRigidBody(body);
                }
                return body;
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (World != null)
            {
                AlignedCollisionObjectArray objectArray = World.CollisionObjectArray;
                while (objectArray.Count != 0)
                {
                    int objectIndex = objectArray.Count - 1;
                    CollisionObject collisionObject = objectArray[objectIndex];
                    objectArray.RemoveAt(objectIndex);
                    collisionObject.Dispose();
                }

                World.Dispose();
                _dispatcher.Dispose();
                Broadphase.Dispose();
                _conf.Dispose();
            }

            _isDisposed = true;
        }
    }
}
