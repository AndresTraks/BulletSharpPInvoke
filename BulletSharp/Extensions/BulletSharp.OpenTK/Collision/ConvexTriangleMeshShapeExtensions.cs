using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConvexTriangleMeshShapeExtensions
	{
        public unsafe static void CalculatePrincipalAxisTransform(this ConvexTriangleMeshShape obj, ref OpenTK.Matrix4 principal, out OpenTK.Vector3 inertia, out float volume)
		{
			fixed (OpenTK.Matrix4* principalPtr = &principal)
			{
				fixed (OpenTK.Vector3* inertiaPtr = &inertia)
				{
                    obj.CalculatePrincipalAxisTransform(ref *(BulletSharp.Math.Matrix*)principalPtr, out *(BulletSharp.Math.Vector3*)inertiaPtr, out volume);
				}
			}
		}
	}
}
