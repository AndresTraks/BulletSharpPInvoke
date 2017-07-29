using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class Vector3Extensions
    {
        public static BulletSharp.Math.Vector3 ToBullet(this OpenTK.Vector3 v)
        {
            return new BulletSharp.Math.Vector3(v.X, v.Y, v.Z);
        }

        public static OpenTK.Vector3 ToOpenTK(this BulletSharp.Math.Vector3 v)
        {
            return new OpenTK.Vector3(v.X, v.Y, v.Z);
        }

        public static BulletSharp.Math.Vector3 Transform(this BulletSharp.Math.Vector3 coordinate, ref OpenTK.Matrix4 transform)
        {
            OpenTK.Vector4 vector = new OpenTK.Vector4();
            vector.X = (coordinate.X * transform.M11) + (coordinate.Y * transform.M21) + (coordinate.Z * transform.M31) + transform.M41;
            vector.Y = (coordinate.X * transform.M12) + (coordinate.Y * transform.M22) + (coordinate.Z * transform.M32) + transform.M42;
            vector.Z = (coordinate.X * transform.M13) + (coordinate.Y * transform.M23) + (coordinate.Z * transform.M33) + transform.M43;
            vector.W = 1f / ((coordinate.X * transform.M14) + (coordinate.Y * transform.M24) + (coordinate.Z * transform.M34) + transform.M44);

            return new BulletSharp.Math.Vector3(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W);
        }

        public static BulletSharp.Math.Vector3 Transform(this BulletSharp.Math.Vector3 coordinate, OpenTK.Matrix4 transform)
        {
            return Transform(coordinate, ref transform);
        }
    }
}
