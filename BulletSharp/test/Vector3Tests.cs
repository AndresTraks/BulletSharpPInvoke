using BulletSharp;
using BulletSharp.Math;
using NUnit.Framework;
using System.Runtime.InteropServices;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("Vector3")]
    public class Vector3Tests
    {
        [Test]
        public void ReadVector3WithMarginTest()
        {
            const double mass = 1.0f;
            var vectorStruct = new Vector3WithPadding
            {
                LeftPadding = 2.0,
                RightPadding = 3.0
            };
            using (BoxShape shape = new BoxShape(1))
            {
                shape.CalculateLocalInertia(mass, out vectorStruct.Vector);
            }
            Assert.That(vectorStruct.LeftPadding, Is.EqualTo(2.0));
            Assert.That(vectorStruct.RightPadding, Is.EqualTo(3.0));
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Vector3WithPadding
        {
            [FieldOffset(0)]
            public double LeftPadding;
            [FieldOffset(8)]
            public Vector3 Vector;
            [FieldOffset(32)]
            public double RightPadding;
        }
    }
}
