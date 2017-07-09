using BulletSharp.Math;
using BulletSharp.SoftBody;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("SoftBody")]
    class SoftBodyTests
    {
        private SoftBodyWorldInfo _softBodyWorldInfo;

        [Test]
        public void SoftBodyNoteTest()
        {
            using (var softBody = new SoftBody(_softBodyWorldInfo))
            {
                softBody.AppendNote("Note test", Vector3.Zero);
                Assert.That(softBody.Notes, Has.Count.EqualTo(1));
                var note = softBody.Notes[0];
                Assert.AreEqual("Note test", note.Text);
            }
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            _softBodyWorldInfo = new SoftBodyWorldInfo();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _softBodyWorldInfo.Dispose();
        }
    }
}
