using BulletSharp;
using BulletSharp.Math;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("Callbacks")]
    public class ContactTestCallbackTests
    {
        private PhysicsContext _context;

        private CollisionShape _shape;
        private RigidBody _sphere1;
        private RigidBody _sphere2;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = new PhysicsContext();
            _context.InitializeWorld();

            _shape = new BoxShape(2);

            _sphere1 = _context.AddBody(_shape, Matrix.Translation(2, 2, 0), 10);
            _sphere2 = _context.AddBody(_shape, Matrix.Translation(0, 2, 0), 1);
        }

        [Test]
        public void ContactTestCallbackTest()
        {
            using (var callback = new ContactSensorCallback(_sphere1))
            {
                _context.World.ContactTest(_sphere1, callback);
                Assert.That(callback.WasCalled, Is.True);
            }

            _sphere1.CollisionFlags |= CollisionFlags.CustomMaterialCallback;
            _sphere2.CollisionFlags |= CollisionFlags.CustomMaterialCallback;
            using (var callback = new ContactSensorCallback(_sphere1))
            {
                _context.World.ContactPairTest(_sphere1, _sphere2, callback);
                Assert.That(callback.WasCalled, Is.True);
            }
            _sphere1.CollisionFlags &= ~CollisionFlags.CustomMaterialCallback;
            _sphere2.CollisionFlags &= ~CollisionFlags.CustomMaterialCallback;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
            _shape.Dispose();
        }

        private sealed class ContactSensorCallback : ContactResultCallback
        {
            private readonly RigidBody _monitoredBody;

            public ContactSensorCallback(RigidBody monitoredBody)
            {
                _monitoredBody = monitoredBody;
            }

            public bool WasCalled { get; private set; }

            public override bool NeedsCollision(BroadphaseProxy proxy)
            {
                // superclass will check CollisionFilterGroup and CollisionFilterMask
                if (base.NeedsCollision(proxy))
                {
                    return _monitoredBody.CheckCollideWithOverride(proxy.ClientObject as CollisionObject);
                }

                return false;
            }

            // Called with each contact for your own processing (e.g. test if contacts fall in within sensor parameters)
            public override float AddSingleResult(ManifoldPoint contact,
                CollisionObjectWrapper colObj0, int partId0, int index0,
                CollisionObjectWrapper colObj1, int partId1, int index1)
            {
                Vector3 collisionPoint; // relative to body
                if (colObj0.CollisionObject == _monitoredBody)
                {
                    collisionPoint = contact.LocalPointA;
                }
                else
                {
                    Assert.That(colObj1.CollisionObject, Is.EqualTo(_monitoredBody));
                    collisionPoint = contact.LocalPointB;
                }

                WasCalled = true;

                return 0;
            }
        }
    }
}
