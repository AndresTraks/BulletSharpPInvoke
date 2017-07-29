using System.ComponentModel;

namespace BulletSharp
{
    /*
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConcaveShapeExtensions
	{
		public unsafe static void ProcessAllTriangles(this ConcaveShape obj, TriangleCallback callback, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					obj.ProcessAllTriangles(callback, ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}
	}
    */
}
