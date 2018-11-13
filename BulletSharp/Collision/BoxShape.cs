using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class BoxShape : PolyhedralConvexShape
	{
		public BoxShape(Vector3 boxHalfExtents)
		{
			IntPtr native = btBoxShape_new(ref boxHalfExtents);
			InitializeCollisionShape(native);
		}

		public BoxShape(float boxHalfExtent)
		{
			IntPtr native = btBoxShape_new2(boxHalfExtent);
			InitializeCollisionShape(native);
		}

		public BoxShape(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ)
		{
			IntPtr native = btBoxShape_new3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ);
			InitializeCollisionShape(native);
		}

		public void GetPlaneEquation(out Vector4 plane, int i)
		{
			btBoxShape_getPlaneEquation(Native, out plane, i);
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
				btBoxShape_getHalfExtentsWithMargin(Native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
				btBoxShape_getHalfExtentsWithoutMargin(Native, out value);
				return value;
			}
		}
	}
}
