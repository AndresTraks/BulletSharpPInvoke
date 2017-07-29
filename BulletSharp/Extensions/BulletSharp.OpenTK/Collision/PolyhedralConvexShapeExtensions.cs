using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PolyhedralConvexShapeExtensions
	{
        public unsafe static void GetEdge(this PolyhedralConvexShape obj, int i, out OpenTK.Vector3 pa, out OpenTK.Vector3 pb)
		{
			fixed (OpenTK.Vector3* paPtr = &pa)
			{
				fixed (OpenTK.Vector3* pbPtr = &pb)
				{
                    obj.GetEdge(i, out *(BulletSharp.Math.Vector3*)paPtr, out *(BulletSharp.Math.Vector3*)pbPtr);
				}
			}
		}

        public unsafe static void GetPlane(this PolyhedralConvexShape obj, out OpenTK.Vector3 planeNormal, out OpenTK.Vector3 planeSupport, int i)
		{
			fixed (OpenTK.Vector3* planeNormalPtr = &planeNormal)
			{
				fixed (OpenTK.Vector3* planeSupportPtr = &planeSupport)
				{
                    obj.GetPlane(out *(BulletSharp.Math.Vector3*)planeNormalPtr, out *(BulletSharp.Math.Vector3*)planeSupportPtr, i);
				}
			}
		}

        public unsafe static void GetVertex(this PolyhedralConvexShape obj, int i, out OpenTK.Vector3 vtx)
		{
			fixed (OpenTK.Vector3* vtxPtr = &vtx)
			{
                obj.GetVertex(i, out *(BulletSharp.Math.Vector3*)vtxPtr);
			}
		}

		public unsafe static bool IsInside(this PolyhedralConvexShape obj, ref OpenTK.Vector3 pt, float tolerance)
		{
			fixed (OpenTK.Vector3* ptPtr = &pt)
			{
				return obj.IsInside(ref *(BulletSharp.Math.Vector3*)ptPtr, tolerance);
			}
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PolyhedralConvexAabbCachingShapeExtensions
	{
        public unsafe static void GetNonvirtualAabb(this PolyhedralConvexAabbCachingShape obj, ref OpenTK.Matrix4 trans, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax, float margin)
		{
			fixed (OpenTK.Matrix4* transPtr = &trans)
			{
				fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
				{
					fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
					{
                        obj.GetNonvirtualAabb(ref *(BulletSharp.Math.Matrix*)transPtr, out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr, margin);
					}
				}
			}
		}
	}
}
