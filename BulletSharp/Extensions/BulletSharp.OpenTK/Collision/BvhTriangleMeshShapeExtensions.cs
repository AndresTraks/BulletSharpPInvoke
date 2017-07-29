using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BvhTriangleMeshShapeExtensions
	{
		public unsafe static void PartialRefitTree(this BvhTriangleMeshShape obj, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					obj.PartialRefitTree(ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}
        /*
		public unsafe static void PerformConvexcast(this BvhTriangleMeshShape obj, TriangleCallback callback, ref OpenTK.Vector3 boxSource, ref OpenTK.Vector3 boxTarget, ref OpenTK.Vector3 boxMin, ref OpenTK.Vector3 boxMax)
		{
			fixed (OpenTK.Vector3* boxSourcePtr = &boxSource)
			{
				fixed (OpenTK.Vector3* boxTargetPtr = &boxTarget)
				{
					fixed (OpenTK.Vector3* boxMinPtr = &boxMin)
					{
						fixed (OpenTK.Vector3* boxMaxPtr = &boxMax)
						{
							obj.PerformConvexcast(callback, ref *(BulletSharp.Math.Vector3*)boxSourcePtr, ref *(BulletSharp.Math.Vector3*)boxTargetPtr, ref *(BulletSharp.Math.Vector3*)boxMinPtr, ref *(BulletSharp.Math.Vector3*)boxMaxPtr);
						}
					}
				}
			}
		}

		public unsafe static void PerformRaycast(this BvhTriangleMeshShape obj, TriangleCallback callback, ref OpenTK.Vector3 raySource, ref OpenTK.Vector3 rayTarget)
		{
			fixed (OpenTK.Vector3* raySourcePtr = &raySource)
			{
				fixed (OpenTK.Vector3* rayTargetPtr = &rayTarget)
				{
					obj.PerformRaycast(callback, ref *(BulletSharp.Math.Vector3*)raySourcePtr, ref *(BulletSharp.Math.Vector3*)rayTargetPtr);
				}
			}
		}
        */
		public unsafe static void RefitTree(this BvhTriangleMeshShape obj, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					obj.RefitTree(ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}
        /*
		public unsafe static void SetOptimizedBvh(this BvhTriangleMeshShape obj, OptimizedBvh bvh, ref OpenTK.Vector3 localScaling)
		{
			fixed (OpenTK.Vector3* localScalingPtr = &localScaling)
			{
				obj.SetOptimizedBvh(bvh, ref *(BulletSharp.Math.Vector3*)localScalingPtr);
			}
		}

		public unsafe static void SetOptimizedBvh(this BvhTriangleMeshShape obj, OptimizedBvh bvh)
		{
			obj.SetOptimizedBvh(bvh);
		}
        */
	}
}
