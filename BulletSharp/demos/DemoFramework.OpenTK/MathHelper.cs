using OpenTK.Mathematics;

namespace DemoFramework.OpenTK
{
    class MathHelper
    {
        public static Matrix4 Convert(ref System.Numerics.Matrix4x4 m)
        {
            return new Matrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }

        public static System.Numerics.Matrix4x4 Convert(ref Matrix4 m)
        {
            System.Numerics.Matrix4x4 r = new System.Numerics.Matrix4x4();
            r.M11 = m.M11; r.M12 = m.M12; r.M13 = m.M13; r.M14 = m.M14;
            r.M21 = m.M21; r.M22 = m.M22; r.M23 = m.M23; r.M24 = m.M24;
            r.M31 = m.M31; r.M32 = m.M32; r.M33 = m.M33; r.M34 = m.M34;
            r.M41 = m.M41; r.M42 = m.M42; r.M43 = m.M43; r.M44 = m.M44;
            return r;
        }

        public static Vector3 Convert(System.Numerics.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }
    }
}
