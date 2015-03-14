using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class GhostObjectExtensions
	{
		public unsafe static void ConvexSweepTest(this GhostObject obj, ConvexShape castShape, ref OpenTK.Matrix4 convexFromWorld, ref OpenTK.Matrix4 convexToWorld, ConvexResultCallback resultCallback, float allowedCcdPenetration)
		{
			fixed (OpenTK.Matrix4* convexFromWorldPtr = &convexFromWorld)
			{
				fixed (OpenTK.Matrix4* convexToWorldPtr = &convexToWorld)
				{
					obj.ConvexSweepTest(castShape, ref *(BulletSharp.Math.Matrix*)convexFromWorldPtr, ref *(BulletSharp.Math.Matrix*)convexToWorldPtr, resultCallback, allowedCcdPenetration);
				}
			}
		}

		public unsafe static void ConvexSweepTest(this GhostObject obj, ConvexShape castShape, ref OpenTK.Matrix4 convexFromWorld, ref OpenTK.Matrix4 convexToWorld, ConvexResultCallback resultCallback)
		{
			fixed (OpenTK.Matrix4* convexFromWorldPtr = &convexFromWorld)
			{
				fixed (OpenTK.Matrix4* convexToWorldPtr = &convexToWorld)
				{
					obj.ConvexSweepTest(castShape, ref *(BulletSharp.Math.Matrix*)convexFromWorldPtr, ref *(BulletSharp.Math.Matrix*)convexToWorldPtr, resultCallback);
				}
			}
		}

		public unsafe static void RayTest(this GhostObject obj, ref OpenTK.Vector3 rayFromWorld, ref OpenTK.Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			fixed (OpenTK.Vector3* rayFromWorldPtr = &rayFromWorld)
			{
				fixed (OpenTK.Vector3* rayToWorldPtr = &rayToWorld)
				{
					obj.RayTest(ref *(BulletSharp.Math.Vector3*)rayFromWorldPtr, ref *(BulletSharp.Math.Vector3*)rayToWorldPtr, resultCallback);
				}
			}
		}
	}
}
