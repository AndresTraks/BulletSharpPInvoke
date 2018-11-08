using BulletSharp;
using BulletSharp.Math;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("GImpact")]
    public class GImpactTests
    {
        private GImpactMeshShape _impactMesh;
        private TriangleIndexVertexArray _meshInterface;
        private IndexedMesh _indexedMesh;

        [OneTimeSetUp]
        public void SetUp()
        {
            int[] triangles1 = new[] { 0, 1, 2 };
            Vector3[] vertices1 = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0)
            };
            _meshInterface = new TriangleIndexVertexArray(triangles1, vertices1);

            int[] triangles2 = new[] { 0, 1, 2 };
            Vector3[] vertices2 = new Vector3[]
            {
                new Vector3(0, 0, 3),
                new Vector3(1, 0, 3),
                new Vector3(0, 1, 3)
            };
            _indexedMesh = new IndexedMesh();
            _indexedMesh.Allocate(triangles2.Length / 3, vertices2.Length);
            _indexedMesh.SetData(triangles2, vertices2);
            _meshInterface.AddIndexedMesh(_indexedMesh);

            _impactMesh = new GImpactMeshShape(_meshInterface);
        }

        [Test]
        public void CompoundFromGImpactTest()
        {
            const float depth = 0.1f;
            CompoundShape compoundShape = CompoundFromGImpact.Create(_impactMesh, depth);
            Assert.AreEqual(compoundShape.NumChildShapes, 2);
            foreach (CompoundShapeChild child in compoundShape.ChildList)
            {
                child.ChildShape.Dispose();
            }
            compoundShape.Dispose();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _impactMesh.Dispose();
            _indexedMesh.Dispose();
            _meshInterface.Dispose();
        }
    }
}
