using BulletSharp;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("AlignedObjectArray")]
    class AlignedCollisionObjectArrayTests
    {
        private PhysicsContext _context;
        private CollisionShape _shape;
        private RigidBody _body1;
        private RigidBody _body2;
        private RigidBody _body3;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = new PhysicsContext();
            _context.InitializeWorld();

            _shape = new BoxShape(1);
            using (var bodyInfo = new RigidBodyConstructionInfo(1, null, _shape))
            {
                _body1 = new RigidBody(bodyInfo);
                _body2 = new RigidBody(bodyInfo);
                _body3 = new RigidBody(bodyInfo);
            }
        }

        [Test]
        public void AlignedCollisionObjectArrayTest()
        {
            var world = _context.World;
            var worldArray = world.CollisionObjectArray;
            Assert.IsEmpty(worldArray);

            world.AddRigidBody(_body1);
            world.AddRigidBody(_body2);
            Assert.That(worldArray, Has.Count.EqualTo(2));
            Assert.AreEqual(2, world.NumCollisionObjects);

            Assert.True(worldArray.Contains(_body1));
            Assert.True(worldArray.Contains(_body2));

            Assert.AreEqual(0, worldArray.IndexOf(_body1));
            Assert.AreEqual(1, worldArray.IndexOf(_body2));

            world.RemoveRigidBody(_body1);
            Assert.That(worldArray, Has.Count.EqualTo(1));
            Assert.AreEqual(1, world.NumCollisionObjects);

            world.RemoveRigidBody(_body1);
            Assert.That(worldArray, Has.Count.EqualTo(1));
            Assert.AreEqual(1, world.NumCollisionObjects);

            worldArray.Remove(_body2);
            Assert.AreEqual(0, world.NumCollisionObjects);

            world.AddRigidBody(_body1);
            world.AddRigidBody(_body2);
            world.AddRigidBody(_body3);
            worldArray.RemoveAt(0);
            Assert.False(_body1.IsInWorld);
            Assert.AreEqual(_body3, worldArray[0]);
            Assert.AreEqual(_body2, worldArray[1]);

            Assert.AreEqual(-1, worldArray.IndexOf(_body1));
            Assert.AreEqual(-1, worldArray.IndexOf(null));

            worldArray.Clear();
            Assert.AreEqual(0, world.NumCollisionObjects);
            Assert.False(_body2.IsInWorld);
            Assert.False(_body3.IsInWorld);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();

            _shape.Dispose();
            _body1.Dispose();
            _body2.Dispose();
            _body3.Dispose();
        }
    }
}
