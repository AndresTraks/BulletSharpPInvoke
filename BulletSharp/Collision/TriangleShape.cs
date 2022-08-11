using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class TriangleShape : PolyhedralConvexShape
	{
		private Vector3Array _vertices;

		internal TriangleShape(ConstructionInfo info)
		{
		}

		public TriangleShape()
		{
			IntPtr native = btTriangleShape_new();
			InitializeCollisionShape(native);
		}

		public TriangleShape(Vector3 p0, Vector3 p1, Vector3 p2)
		{
			IntPtr native = btTriangleShape_new2(ref p0, ref p1, ref p2);
			InitializeCollisionShape(native);
		}

		public void CalcNormal(out Vector3 normal)
		{
			btTriangleShape_calcNormal(Native, out normal);
		}

		public void GetPlaneEquation(int i, out Vector3 planeNormal, out Vector3 planeSupport)
		{
			btTriangleShape_getPlaneEquation(Native, i, out planeNormal, out planeSupport);
		}

		public IntPtr GetVertexPtr(int index)
		{
			return btTriangleShape_getVertexPtr(Native, index);
		}

		public Vector3Array Vertices
		{
			get
			{
				if (_vertices == null)
				{
					_vertices = new Vector3Array(btTriangleShape_getVertices1(Native), 3);
				}
				return _vertices;
			}
		}
	}
}
