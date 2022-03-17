using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class BuSimplex1To4 : PolyhedralConvexAabbCachingShape
	{
		internal BuSimplex1To4(ConstructionInfo info)
		{
		}

		public BuSimplex1To4()
		{
			IntPtr native = btBU_Simplex1to4_new();
			InitializeCollisionShape(native);
		}

		public BuSimplex1To4(Vector3 pt0)
		{
			IntPtr native = btBU_Simplex1to4_new2(ref pt0);
			InitializeCollisionShape(native);
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1)
		{
			IntPtr native = btBU_Simplex1to4_new3(ref pt0, ref pt1);
			InitializeCollisionShape(native);
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1, Vector3 pt2)
		{
			IntPtr native = btBU_Simplex1to4_new4(ref pt0, ref pt1, ref pt2);
			InitializeCollisionShape(native);
		}

		public BuSimplex1To4(Vector3 pt0, Vector3 pt1, Vector3 pt2, Vector3 pt3)
		{
			IntPtr native = btBU_Simplex1to4_new5(ref pt0, ref pt1, ref pt2, ref pt3);
			InitializeCollisionShape(native);
		}

		public void AddVertexRef(ref Vector3 pt)
		{
			btBU_Simplex1to4_addVertex(Native, ref pt);
		}

		public void AddVertex(Vector3 pt)
		{
			btBU_Simplex1to4_addVertex(Native, ref pt);
		}

		public int GetIndex(int i)
		{
			return btBU_Simplex1to4_getIndex(Native, i);
		}

		public void Reset()
		{
			btBU_Simplex1to4_reset(Native);
		}
	}
}
