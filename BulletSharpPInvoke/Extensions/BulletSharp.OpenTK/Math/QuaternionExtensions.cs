using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QuaternionExtensions
    {
        public static BulletSharp.Math.Quaternion ToBullet(this OpenTK.Quaternion q)
        {
            return new BulletSharp.Math.Quaternion(q.X, q.Y, q.Z, q.W);
        }

        public static OpenTK.Quaternion ToOpenTK(this BulletSharp.Math.Quaternion q)
        {
            return new OpenTK.Quaternion(q.X, q.Y, q.Z, q.W);
        }
    }
}
