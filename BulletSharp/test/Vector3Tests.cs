using System.Numerics;
using System.Runtime.InteropServices;
using BulletSharp;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("Vector3")]
    public class Vector3Tests
    {
        [Test]
        public void ReadVector3WithMarginTest()
        {
            const float mass = 1.0f;
            var vectorStruct = new Vector3WithPadding
            {
                LeftPadding = 2.0f,
                RightPadding = 3.0f
            };
            using (BoxShape shape = new BoxShape(1))
            {
                shape.CalculateLocalInertia(mass, out vectorStruct.Vector);
            }
            Assert.That(vectorStruct.LeftPadding, Is.EqualTo(2.0f));
            Assert.That(vectorStruct.RightPadding, Is.EqualTo(3.0f));
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Vector3WithPadding
        {
            [FieldOffset(0)]
            public float LeftPadding;
            [FieldOffset(4)]
            public Vector3 Vector;
            [FieldOffset(16)]
            public float RightPadding;
        }
    }
}
