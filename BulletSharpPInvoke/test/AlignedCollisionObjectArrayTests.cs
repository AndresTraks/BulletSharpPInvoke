using BulletSharp;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("AlignedObjectArray")]
    class AlignedCollisionObjectArrayTests
    {
        private DefaultCollisionConfiguration _conf;
        private CollisionDispatcher _dispatcher;
        private DbvtBroadphase _broadphase;
        private SequentialImpulseConstraintSolver _solver;
        private DiscreteDynamicsWorld _world;
        private CollisionShape _shape;
        private RigidBody _body1;
        private RigidBody _body2;

        [Test]
        public void AlignedCollisionObjectArrayTest()
        {
            var worldArray = _world.CollisionObjectArray;
            Assert.IsEmpty(worldArray);

            _world.AddRigidBody(_body1);
            _world.AddRigidBody(_body2);
            Assert.That(worldArray, Has.Count.EqualTo(2));
            Assert.AreEqual(2, _world.NumCollisionObjects);

            Assert.True(worldArray.Contains(_body1));
            Assert.True(worldArray.Contains(_body2));

            Assert.AreEqual(0, worldArray.IndexOf(_body1));
            Assert.AreEqual(1, worldArray.IndexOf(_body2));

            _world.RemoveRigidBody(_body1);
            Assert.That(worldArray, Has.Count.EqualTo(1));
            Assert.AreEqual(1, _world.NumCollisionObjects);

            _world.RemoveRigidBody(_body1);
            Assert.That(worldArray, Has.Count.EqualTo(1));
            Assert.AreEqual(1, _world.NumCollisionObjects);

            worldArray.Remove(_body2);
            Assert.AreEqual(0, _world.NumCollisionObjects);

            _world.AddRigidBody(_body1);
            _world.AddRigidBody(_body2);
            worldArray.Clear();
            Assert.AreEqual(0, _world.NumCollisionObjects);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            _conf = new DefaultCollisionConfiguration();
            _dispatcher = new CollisionDispatcher(_conf);
            _broadphase = new DbvtBroadphase();
            _solver = new SequentialImpulseConstraintSolver();
            _world = new DiscreteDynamicsWorld(_dispatcher, _broadphase, _solver, _conf);

            _shape = new BoxShape(1);
            using (var bodyInfo = new RigidBodyConstructionInfo(1, null, _shape))
            {
                _body1 = new RigidBody(bodyInfo);
                _body2 = new RigidBody(bodyInfo);
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _world.Dispose();
            _solver.Dispose();
            _broadphase.Dispose();
            _dispatcher.Dispose();
            _conf.Dispose();

            _shape.Dispose();
            _body1.Dispose();
            _body2.Dispose();
        }
    }
}
