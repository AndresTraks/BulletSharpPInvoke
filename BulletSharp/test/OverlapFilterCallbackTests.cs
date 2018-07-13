using BulletSharp;
using BulletSharp.Math;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("Callbacks")]
    public class OverlapFilterCallbackTests
    {
        private PhysicsContext _context;
        private CollisionShape _shape;
        private CustomOverlapFilterCallback _callback;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = new PhysicsContext();
            _context.InitializeWorld();

            _shape = new BoxShape(2);
            RigidBody staticSphere = _context.AddStaticBody(_shape, Matrix.Translation(0, 0, 0));
            RigidBody dynamicSphere = _context.AddBody(_shape, Matrix.Translation(0, 1, 0), 1);

            _callback = new CustomOverlapFilterCallback(staticSphere, dynamicSphere);
        }

        [Test]
        public void OverlapFilterCallbackTest()
        {
            HashedOverlappingPairCache pairCache = (HashedOverlappingPairCache)_context.World.PairCache;
            Assert.IsNull(pairCache.OverlapFilterCallback);

            pairCache.OverlapFilterCallback = _callback;

            for (int i = 0; i < 10; i++)
            {
                _context.World.StepSimulation(1.0f / 60.0f);
            }

            Assert.IsTrue(_callback.WasCalled);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
            _callback.Dispose();
            _shape.Dispose();
        }

        private class CustomOverlapFilterCallback : OverlapFilterCallback
        {
            private readonly RigidBody _expectedBody0;
            private readonly RigidBody _expectedBody1;

            public CustomOverlapFilterCallback(RigidBody expectedBody0, RigidBody expectedBody1)
            {
                _expectedBody0 = expectedBody0;
                _expectedBody1 = expectedBody1;
            }

            public bool WasCalled { get; private set; }

            public override bool NeedBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
            {
                Assert.IsTrue(
                    (proxy0.ClientObject == _expectedBody0 && proxy1.ClientObject == _expectedBody1) ||
                    (proxy0.ClientObject == _expectedBody1 && proxy1.ClientObject == _expectedBody0));

                WasCalled = true;
                return true;
            }
        }
    }
}
