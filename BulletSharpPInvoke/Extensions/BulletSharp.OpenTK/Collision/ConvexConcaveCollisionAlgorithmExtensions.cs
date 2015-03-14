using System.ComponentModel;

namespace BulletSharp
{
    /*
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConvexTriangleCallbackExtensions
	{
		public unsafe static void GetAabbMax(this ConvexTriangleCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AabbMax;
			}
		}

		public static OpenTK.Vector3 GetAabbMax(this ConvexTriangleCallback obj)
		{
			OpenTK.Vector3 value;
			GetAabbMax(obj, out value);
			return value;
		}

		public unsafe static void GetAabbMin(this ConvexTriangleCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AabbMin;
			}
		}

		public static OpenTK.Vector3 GetAabbMin(this ConvexTriangleCallback obj)
		{
			OpenTK.Vector3 value;
			GetAabbMin(obj, out value);
			return value;
		}
	}
    */
}
