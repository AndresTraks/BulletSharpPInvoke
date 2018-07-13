using BulletSharp;
using BulletSharp.Math;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("GhostObject")]
    public class GhostObjectTests
    {
        private PhysicsContext _context;
        private GhostPairCallback _ghostPairCallback;
        private CollisionShape _shape;
        private PairCachingGhostObject _ghostObject;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = new PhysicsContext();
            _context.InitializeWorld();

            _ghostPairCallback = new GhostPairCallback();
            _context.Broadphase.OverlappingPairCache.SetInternalGhostPairCallback(_ghostPairCallback);

            _shape = new BoxShape(2);
            RigidBody staticBox = _context.AddStaticBody(_shape, Matrix.Translation(0, 0, 0));
            RigidBody dynamicBox = _context.AddBody(_shape, Matrix.Translation(0, 1, 0), 1);

            _ghostObject = new PairCachingGhostObject
            {
                CollisionShape = _shape,
                WorldTransform = Matrix.Translation(2, 2, 0)
            };
            _context.World.AddCollisionObject(_ghostObject);
        }

        [Test]
        public void GhostObjectPairsTest()
        {
            DiscreteDynamicsWorld world = _context.World;
            world.StepSimulation(1.0f / 60.0f);

            AlignedManifoldArray manifoldArray = new AlignedManifoldArray();
            AlignedBroadphasePairArray pairArray = _ghostObject.OverlappingPairCache.OverlappingPairArray;

            foreach (BroadphasePair pair in pairArray)
            {
                //unless we manually perform collision detection on this pair, the contacts are in the dynamics world paircache:
                BroadphasePair collisionPair = world.PairCache.FindPair(pair.Proxy0, pair.Proxy1);
                if (collisionPair == null)
                    continue;

                manifoldArray.Clear();

                if (collisionPair.Algorithm != null)
                    collisionPair.Algorithm.GetAllContactManifolds(manifoldArray);

                for (int j = 0; j < manifoldArray.Count; j++)
                {
                    PersistentManifold manifold = manifoldArray[j];
                    float directionSign = manifold.Body0 == _ghostObject ? -1.0f : 1.0f;
                    for (int p = 0; p < manifold.NumContacts; p++)
                    {
                        ManifoldPoint pt = manifold.GetContactPoint(p);
                        if (pt.Distance < 0.0f)
                        {
                            Vector3 ptA = pt.PositionWorldOnA;
                            Vector3 ptB = pt.PositionWorldOnB;
                            Vector3 normalOnB = pt.NormalWorldOnB;
                        }
                    }
                }
            }

            manifoldArray.Dispose();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
            _ghostPairCallback.Dispose();
            _shape.Dispose();
            _ghostObject.Dispose();
        }
    }
}
