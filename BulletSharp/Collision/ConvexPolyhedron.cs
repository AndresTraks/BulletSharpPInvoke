using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Face : BulletDisposableObject
	{
		internal Face(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public Face()
		{
			IntPtr native = btFace_new();
			InitializeUserOwned(native);
		}
		/*
		public AlignedIntArray Indices
		{
			get { return new AlignedIntArray(btFace_getIndices(Native)); }
		}

		public ScalarArray Plane
		{
			get { return new ScalarArray(btFace_getPlane(Native)); }
		}
		*/

		protected override void Dispose(bool disposing)
		{
			btFace_delete(Native);
		}
	}

	public class ConvexPolyhedron : BulletDisposableObject
	{
		//AlignedFaceArray _faces;
		AlignedVector3Array _uniqueEdges;
		AlignedVector3Array _vertices;

		internal ConvexPolyhedron(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public ConvexPolyhedron()
		{
			IntPtr native = btConvexPolyhedron_new();
			InitializeUserOwned(native);
		}

		public void Initialize()
		{
			btConvexPolyhedron_initialize(Native);
		}

		public void Initialize2()
		{
			btConvexPolyhedron_initialize2(Native);
		}

		public void ProjectRef(ref Matrix4x4 trans, ref Vector3 dir, out float minProj, out float maxProj,
			out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			btConvexPolyhedron_project(Native, ref trans, ref dir, out minProj,
				out maxProj, out witnesPtMin, out witnesPtMax);
		}

		public void Project(Matrix4x4 trans, Vector3 dir, out float minProj, out float maxProj,
			out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			btConvexPolyhedron_project(Native, ref trans, ref dir, out minProj,
				out maxProj, out witnesPtMin, out witnesPtMax);
		}

		public bool TestContainment()
		{
			return btConvexPolyhedron_testContainment(Native);
		}

		public Vector3 Extents
		{
			get
			{
				Vector3 value;
				btConvexPolyhedron_getExtents(Native, out value);
				return value;
			}
			set => btConvexPolyhedron_setExtents(Native, ref value);
		}
		/*
		public AlignedFaceArray Faces
		{
			get { return btConvexPolyhedron_getFaces(Native); }
		}
		*/
		public Vector3 LocalCenter
		{
			get
			{
				Vector3 value;
				btConvexPolyhedron_getLocalCenter(Native, out value);
				return value;
			}
			set => btConvexPolyhedron_setLocalCenter(Native, ref value);
		}

		public Vector3 C
		{
			get
			{
				Vector3 value;
				btConvexPolyhedron_getMC(Native, out value);
				return value;
			}
			set => btConvexPolyhedron_setMC(Native, ref value);
		}

		public Vector3 E
		{
			get
			{
				Vector3 value;
				btConvexPolyhedron_getME(Native, out value);
				return value;
			}
			set => btConvexPolyhedron_setME(Native, ref value);
		}

		public float Radius
		{
			get => btConvexPolyhedron_getRadius(Native);
			set => btConvexPolyhedron_setRadius(Native, value);
		}

		public AlignedVector3Array UniqueEdges
		{
			get
			{
				if (_uniqueEdges == null)
				{
					_uniqueEdges = new AlignedVector3Array(btConvexPolyhedron_getUniqueEdges(Native));
				}
				return _uniqueEdges;
			}
		}

		public AlignedVector3Array Vertices
		{
			get
			{
				if (_vertices == null)
				{
					_vertices = new AlignedVector3Array(btConvexPolyhedron_getVertices(Native));
				}
				return _vertices;
			}
		}

		protected override void Dispose(bool disposing)
		{
			btConvexPolyhedron_delete(Native);
		}
	}
}
