using OpenTK;

namespace DemoFramework.OpenTK
{
    class MathHelper
    {
        public static Matrix4 Convert(ref BulletSharp.Math.Matrix m)
        {
            return new Matrix4(
                (float)m.M11, (float)m.M12, (float)m.M13, (float)m.M14,
                (float)m.M21, (float)m.M22, (float)m.M23, (float)m.M24,
                (float)m.M31, (float)m.M32, (float)m.M33, (float)m.M34,
                (float)m.M41, (float)m.M42, (float)m.M43, (float)m.M44);
        }

        public static BulletSharp.Math.Matrix Convert(ref Matrix4 m)
        {
            BulletSharp.Math.Matrix r = new BulletSharp.Math.Matrix();
            r.M11 = m.M11; r.M12 = m.M12; r.M13 = m.M13; r.M14 = m.M14;
            r.M21 = m.M21; r.M22 = m.M22; r.M23 = m.M23; r.M24 = m.M24;
            r.M31 = m.M31; r.M32 = m.M32; r.M33 = m.M33; r.M34 = m.M34;
            r.M41 = m.M41; r.M42 = m.M42; r.M43 = m.M43; r.M44 = m.M44;
            return r;
        }

        public static Vector3 Convert(BulletSharp.Math.Vector3 v)
        {
            return new Vector3((float)v.X, (float)v.Y, (float)v.Z);
        }
    }
}
