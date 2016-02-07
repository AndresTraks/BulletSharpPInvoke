using System.Linq;
using BulletSharp;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    class SerializationTest
    {
        DefaultCollisionConfiguration _conf;
        CollisionDispatcher _dispatcher;
        DbvtBroadphase _broadphase;
        SequentialImpulseConstraintSolver _solver;
        DiscreteDynamicsWorld _world;

        [Test]
        public void SerializeTest()
        {
            var fileLoader = new BulletWorldImporter(_world);
            var objects = _world.CollisionObjectArray;

            Assert.True(fileLoader.LoadFile("data\\bsp.bullet"));
            Assert.AreEqual(127, objects.Count);
            Assert.AreEqual(127, fileLoader.NumCollisionShapes);
            Assert.True(objects.All(o => o.CollisionShape is ConvexHullShape));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\concaveCompound.bullet"));
            Assert.AreEqual(1, fileLoader.NumBvhs);
            Assert.AreEqual(6, fileLoader.NumCollisionShapes);
            Assert.AreEqual(11, objects.Count);
            Assert.AreEqual(10, objects.Count(o => o.CollisionShape is CompoundShape));
            var triangleMeshShape =
                objects.Select(o => o.CollisionShape).FirstOrDefault(s => s is BvhTriangleMeshShape) as BvhTriangleMeshShape;
            Assert.NotNull(triangleMeshShape);
            Assert.False(triangleMeshShape.OwnsBvh);
            Assert.NotNull(triangleMeshShape.OptimizedBvh);
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\constraints.bullet"));
            Assert.AreEqual(10, fileLoader.NumConstraints);
            Assert.AreEqual(10, _world.NumConstraints);
            Assert.AreEqual(17, objects.Count);
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\convex_decomposition.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\cylinders.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
            /*
            Assert.True(fileLoader.LoadFile("data\\multibody.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\r2d2_multibody.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
            */
            Assert.True(fileLoader.LoadFile("data\\ragdoll_6dof.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\ragdoll_conetwist.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\slope.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\spider.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(fileLoader.LoadFile("data\\testFile.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
            /*
            Assert.True(fileLoader.LoadFile("data\\testFileFracture.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
            */
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            _conf = new DefaultCollisionConfiguration();
            _dispatcher = new CollisionDispatcher(_conf);
            _broadphase = new DbvtBroadphase();
            _solver = new SequentialImpulseConstraintSolver();
            _world = new DiscreteDynamicsWorld(_dispatcher, _broadphase, _solver, _conf);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _world.Dispose();
            _solver.Dispose();
            _broadphase.Dispose();
            _dispatcher.Dispose();
            _conf.Dispose();
        }
    }
}
