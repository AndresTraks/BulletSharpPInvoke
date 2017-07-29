using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConvexShapeExtensions
	{
        /*
		public unsafe static void BatchedUnitVectorGetSupportingVertexWithoutMargin(this ConvexShape obj, ref OpenTK.Vector3 vectors, ref OpenTK.Vector3 supportVerticesOut, int numVectors)
		{
			fixed (OpenTK.Vector3* vectorsPtr = &vectors)
			{
				fixed (OpenTK.Vector3* supportVerticesOutPtr = &supportVerticesOut)
				{
					obj.BatchedUnitVectorGetSupportingVertexWithoutMargin(ref *(BulletSharp.Math.Vector3*)vectorsPtr, ref *(BulletSharp.Math.Vector3*)supportVerticesOutPtr, numVectors);
				}
			}
		}
        */
        public unsafe static void GetAabbNonVirtual(this ConvexShape obj, ref OpenTK.Matrix4 t, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Matrix4* tPtr = &t)
			{
				fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
				{
					fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
					{
                        obj.GetAabbNonVirtual(ref *(BulletSharp.Math.Matrix*)tPtr, out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr);
					}
				}
			}
		}

        public unsafe static void GetAabbSlow(this ConvexShape obj, ref OpenTK.Matrix4 t, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Matrix4* tPtr = &t)
			{
				fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
				{
					fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
					{
                        obj.GetAabbSlow(ref *(BulletSharp.Math.Matrix*)tPtr, out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr);
					}
				}
			}
		}

        public unsafe static void GetPreferredPenetrationDirection(this ConvexShape obj, int index, out OpenTK.Vector3 penetrationVector)
		{
			fixed (OpenTK.Vector3* penetrationVectorPtr = &penetrationVector)
			{
                obj.GetPreferredPenetrationDirection(index, out *(BulletSharp.Math.Vector3*)penetrationVectorPtr);
			}
		}

		public unsafe static OpenTK.Vector3 LocalGetSupportingVertex(this ConvexShape obj, ref OpenTK.Vector3 vec)
		{
			fixed (OpenTK.Vector3* vecPtr = &vec)
			{
				return obj.LocalGetSupportingVertex(ref *(BulletSharp.Math.Vector3*)vecPtr).ToOpenTK();
			}
		}

		public unsafe static OpenTK.Vector3 LocalGetSupportingVertexWithoutMargin(this ConvexShape obj, ref OpenTK.Vector3 vec)
		{
			fixed (OpenTK.Vector3* vecPtr = &vec)
			{
				return obj.LocalGetSupportingVertexWithoutMargin(ref *(BulletSharp.Math.Vector3*)vecPtr).ToOpenTK();
			}
		}

		public unsafe static OpenTK.Vector3 LocalGetSupportVertexNonVirtual(this ConvexShape obj, ref OpenTK.Vector3 vec)
		{
			fixed (OpenTK.Vector3* vecPtr = &vec)
			{
				return obj.LocalGetSupportVertexNonVirtual(ref *(BulletSharp.Math.Vector3*)vecPtr).ToOpenTK();
			}
		}

		public unsafe static OpenTK.Vector3 LocalGetSupportVertexWithoutMarginNonVirtual(this ConvexShape obj, ref OpenTK.Vector3 vec)
		{
			fixed (OpenTK.Vector3* vecPtr = &vec)
			{
				return obj.LocalGetSupportVertexWithoutMarginNonVirtual(ref *(BulletSharp.Math.Vector3*)vecPtr).ToOpenTK();
			}
		}
        /*
		public unsafe static void Project(this ConvexShape obj, ref OpenTK.Matrix4 trans, ref OpenTK.Vector3 dir, float min, float max)
		{
			fixed (OpenTK.Matrix4* transPtr = &trans)
			{
				fixed (OpenTK.Vector3* dirPtr = &dir)
				{
					obj.Project(ref *(BulletSharp.Math.Matrix*)transPtr, ref *(BulletSharp.Math.Vector3*)dirPtr, min, max);
				}
			}
		}
        */
	}
}
