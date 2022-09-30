using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GimTriangleContact : BulletDisposableObject
	{
		public GimTriangleContact()
		{
			IntPtr native = GIM_TRIANGLE_CONTACT_new();
			InitializeUserOwned(native);
		}

		public GimTriangleContact(GimTriangleContact other)
		{
			IntPtr native = GIM_TRIANGLE_CONTACT_new2(other.Native);
			InitializeUserOwned(native);
		}

		public void CopyFrom(GimTriangleContact other)
		{
			GIM_TRIANGLE_CONTACT_copy_from(Native, other.Native);
		}
		/*
		public void MergePoints(Vector4 plane, double margin, Vector3 points, int pointCount)
		{
			GIM_TRIANGLE_CONTACT_merge_points(Native, ref plane, margin, ref points, pointCount);
		}
		*/
		public double PenetrationDepth
		{
			get => GIM_TRIANGLE_CONTACT_getPenetration_depth(Native);
			set => GIM_TRIANGLE_CONTACT_setPenetration_depth(Native, value);
		}

		public int PointCount
		{
			get => GIM_TRIANGLE_CONTACT_getPoint_count(Native);
			set => GIM_TRIANGLE_CONTACT_setPoint_count(Native, value);
		}

		public Vector3Array Points => new Vector3Array(GIM_TRIANGLE_CONTACT_getPoints(Native), 16);

		public Vector4 SeparatingNormal
		{
			get
			{
				Vector4 value;
				GIM_TRIANGLE_CONTACT_getSeparating_normal(Native, out value);
				return value;
			}
			set => GIM_TRIANGLE_CONTACT_setSeparating_normal(Native, ref value);
		}

		protected override void Dispose(bool disposing)
		{
			GIM_TRIANGLE_CONTACT_delete(Native);
		}
	}

	public class PrimitiveTriangle : BulletDisposableObject
	{
		internal PrimitiveTriangle(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public PrimitiveTriangle()
		{
			IntPtr native = btPrimitiveTriangle_new();
			InitializeUserOwned(native);
		}

		public void ApplyTransform(Matrix transform)
		{
			btPrimitiveTriangle_applyTransform(Native, ref transform);
		}

		public void BuildTriPlane()
		{
			btPrimitiveTriangle_buildTriPlane(Native);
		}
		/*
		public int ClipTriangle(PrimitiveTriangle other, Vector3 clippedPoints)
		{
			return btPrimitiveTriangle_clip_triangle(Native, other.Native, ref clippedPoints);
		}
		*/
		public bool FindTriangleCollisionClipMethod(PrimitiveTriangle other, GimTriangleContact contacts)
		{
			return btPrimitiveTriangle_find_triangle_collision_clip_method(Native, other.Native, contacts.Native);
		}

		public void GetEdgePlane(int edgeIndex, out Vector4 plane)
		{
			btPrimitiveTriangle_get_edge_plane(Native, edgeIndex, out plane);
		}

		public bool OverlapTestConservative(PrimitiveTriangle other)
		{
			return btPrimitiveTriangle_overlap_test_conservative(Native, other.Native);
		}

		public double Dummy
		{
			get => btPrimitiveTriangle_getDummy(Native);
			set => btPrimitiveTriangle_setDummy(Native, value);
		}

		public double Margin
		{
			get => btPrimitiveTriangle_getMargin(Native);
			set => btPrimitiveTriangle_setMargin(Native, value);
		}

		public Vector4 Plane
		{
			get
			{
				Vector4 value;
				btPrimitiveTriangle_getPlane(Native, out value);
				return value;
			}
			set => btPrimitiveTriangle_setPlane(Native, ref value);
		}

		public Vector3Array Vertices => new Vector3Array(btPrimitiveTriangle_getVertices(Native), 3);

		protected override void Dispose(bool disposing)
		{
			btPrimitiveTriangle_delete(Native);
		}
	}

	public class TriangleShapeEx : TriangleShape
	{
		public TriangleShapeEx()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btTriangleShapeEx_new();
			InitializeCollisionShape(native);
		}

		public TriangleShapeEx(Vector3 p0, Vector3 p1, Vector3 p2) 
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btTriangleShapeEx_new2(ref p0, ref p1, ref p2);
			InitializeCollisionShape(native);
		}

		public TriangleShapeEx(TriangleShapeEx other) 
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btTriangleShapeEx_new3(other.Native);
			InitializeCollisionShape(native);
		}

		public void ApplyTransform(Matrix transform)
		{
			btTriangleShapeEx_applyTransform(Native, ref transform);
		}

		public void BuildTriPlane(out Vector4 plane)
		{
			btTriangleShapeEx_buildTriPlane(Native, out plane);
		}

		public bool OverlapTestConservative(TriangleShapeEx other)
		{
			return btTriangleShapeEx_overlap_test_conservative(Native, other.Native);
		}
	}
}
