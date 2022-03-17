using System.IO;
using System.Linq;
using System.Reflection;
using BulletSharp;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("Serialization")]
    class SerializationTest
    {
        private DefaultCollisionConfiguration _conf;
        private CollisionDispatcher _dispatcher;
        private DbvtBroadphase _broadphase;
        private SequentialImpulseConstraintSolver _solver;
        private DiscreteDynamicsWorld _world;

        [Test]
        public void SerializeTest()
        {
            var fileLoader = new BulletWorldImporter(_world);
            var objects = _world.CollisionObjectArray;

            Assert.True(LoadFile(fileLoader, "data\\bsp.bullet"));
            Assert.AreEqual(127, objects.Count);
            Assert.AreEqual(127, fileLoader.NumCollisionShapes);
            Assert.True(objects.All(o => o.CollisionShape is ConvexHullShape));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(LoadFile(fileLoader, "data\\concaveCompound.bullet"));
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

            Assert.True(LoadFile(fileLoader, "data\\constraints.bullet"));
            Assert.AreEqual(10, fileLoader.NumConstraints);
            Assert.AreEqual(10, _world.NumConstraints);
            Assert.AreEqual(17, objects.Count);
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(LoadFile(fileLoader, "data\\convex_decomposition.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(LoadFile(fileLoader, "data\\cylinders.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
            /*
            Assert.True(LoadFile(fileLoader, "data\\multibody.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(LoadFile(fileLoader, "data\\r2d2_multibody.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
            */
            Assert.True(LoadFile(fileLoader, "data\\ragdoll_6dof.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(LoadFile(fileLoader, "data\\ragdoll_conetwist.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(LoadFile(fileLoader, "data\\slope.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);

            Assert.True(LoadFile(fileLoader, "data\\spider.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
            /*
            Assert.True(LoadFile(fileLoader, "data\\testFile.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
            */
            Assert.True(LoadFile(fileLoader, "data\\testFileFracture.bullet"));
            fileLoader.DeleteAllData();
            Assert.AreEqual(0, objects.Count);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            _conf = new DefaultCollisionConfiguration();
            _dispatcher = new CollisionDispatcher(_conf);
            _broadphase = new DbvtBroadphase();
            _solver = new SequentialImpulseConstraintSolver();
            _world = new DiscreteDynamicsWorld(_dispatcher, _broadphase, _solver, _conf);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _world.Dispose();
            _solver.Dispose();
            _broadphase.Dispose();
            _dispatcher.Dispose();
            _conf.Dispose();
        }

        private static bool LoadFile(BulletWorldImporter fileLoader, string fileName)
        {
            string path = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                fileName);
            return fileLoader.LoadFile(path);
        }
    }
}
