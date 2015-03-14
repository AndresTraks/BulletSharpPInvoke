using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IDebugDrawExtensions
    {
        public unsafe static void DrawBox(this IDebugDraw obj, ref OpenTK.Vector3 bbMin, ref OpenTK.Vector3 bbMax, ref OpenTK.Matrix4 trans, ref OpenTK.Vector3 color)
        {
            fixed (OpenTK.Vector3* bbMinValue = &bbMin)
            {
                fixed (OpenTK.Vector3* bbMaxValue = &bbMax)
                {
                    fixed (OpenTK.Matrix4* transValue = &trans)
                    {
                        fixed (OpenTK.Vector3* colorValue = &color)
                        {
                            obj.DrawBox(ref *(BulletSharp.Math.Vector3*)bbMinValue, ref *(BulletSharp.Math.Vector3*)bbMaxValue, ref *(BulletSharp.Math.Matrix*)transValue, ref *(BulletSharp.Math.Vector3*)colorValue);
                        }
                    }
                }
            }
        }

        public unsafe static void DrawLine(this IDebugDraw obj, ref OpenTK.Vector3 from, ref OpenTK.Vector3 to, ref OpenTK.Vector3 fromColor)
        {
            fixed (OpenTK.Vector3* fromValue = &from)
            {
                fixed (OpenTK.Vector3* toValue = &to)
                {
                    fixed (OpenTK.Vector3* fromColorValue = &fromColor)
                    {
                        obj.DrawLine(ref *(BulletSharp.Math.Vector3*)fromValue, ref *(BulletSharp.Math.Vector3*)toValue, ref *(BulletSharp.Math.Vector3*)fromColorValue);
                    }
                }
            }
        }
    }
}
