using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Face : IDisposable
	{
		internal IntPtr _native;

		internal Face(IntPtr native)
		{
			_native = native;
		}

		public Face()
		{
			_native = btFace_new();
		}
		/*
		public AlignedIntArray Indices
		{
			get { return new AlignedIntArray(btFace_getIndices(_native)); }
		}

		public ScalarArray Plane
		{
			get { return new ScalarArray(btFace_getPlane(_native)); }
		}
		*/
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btFace_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~Face()
		{
			Dispose(false);
		}
	}

	public class ConvexPolyhedron : IDisposable
	{
		internal IntPtr _native;

		//AlignedFaceArray _faces;
		AlignedVector3Array _uniqueEdges;
		AlignedVector3Array _vertices;

		internal ConvexPolyhedron(IntPtr native)
		{
			_native = native;
		}

		public ConvexPolyhedron()
		{
			_native = btConvexPolyhedron_new();
		}

		public void Initialize()
		{
			btConvexPolyhedron_initialize(_native);
		}

		public void ProjectRef(ref Matrix trans, ref Vector3 dir, out float minProj, out float maxProj,
			out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			btConvexPolyhedron_project(_native, ref trans, ref dir, out minProj,
				out maxProj, out witnesPtMin, out witnesPtMax);
		}

		public void Project(Matrix trans, Vector3 dir, out float minProj, out float maxProj,
			out Vector3 witnesPtMin, out Vector3 witnesPtMax)
		{
			btConvexPolyhedron_project(_native, ref trans, ref dir, out minProj,
				out maxProj, out witnesPtMin, out witnesPtMax);
		}

		public bool TestContainment()
		{
			return btConvexPolyhedron_testContainment(_native);
		}

		public Vector3 Extents
		{
			get
			{
				Vector3 value;
				btConvexPolyhedron_getExtents(_native, out value);
				return value;
			}
			set => btConvexPolyhedron_setExtents(_native, ref value);
		}
		/*
		public AlignedFaceArray Faces
		{
			get { return btConvexPolyhedron_getFaces(_native); }
		}
		*/
		public Vector3 LocalCenter
		{
			get
			{
				Vector3 value;
				btConvexPolyhedron_getLocalCenter(_native, out value);
				return value;
			}
			set => btConvexPolyhedron_setLocalCenter(_native, ref value);
		}

		public Vector3 C
		{
			get
			{
				Vector3 value;
				btConvexPolyhedron_getMC(_native, out value);
				return value;
			}
			set => btConvexPolyhedron_setMC(_native, ref value);
		}

		public Vector3 E
		{
			get
			{
				Vector3 value;
				btConvexPolyhedron_getME(_native, out value);
				return value;
			}
			set => btConvexPolyhedron_setME(_native, ref value);
		}

		public float Radius
		{
			get => btConvexPolyhedron_getRadius(_native);
			set => btConvexPolyhedron_setRadius(_native, value);
		}

		public AlignedVector3Array UniqueEdges
		{
			get
			{
				if (_uniqueEdges == null)
				{
					_uniqueEdges = new AlignedVector3Array(btConvexPolyhedron_getUniqueEdges(_native));
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
					_vertices = new AlignedVector3Array(btConvexPolyhedron_getVertices(_native));
				}
				return _vertices;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btConvexPolyhedron_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ConvexPolyhedron()
		{
			Dispose(false);
		}
	}
}
