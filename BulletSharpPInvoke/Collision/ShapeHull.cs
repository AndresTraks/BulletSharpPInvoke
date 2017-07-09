using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ShapeHull : IDisposable
	{
		internal IntPtr _native;

		private ConvexShape _shape;
		private UIntArray _indices;
		private Vector3Array _vertices;

		public ShapeHull(ConvexShape shape)
		{
			_native = btShapeHull_new(shape.Native);
			_shape = shape;
		}

		public bool BuildHull(float margin)
		{
			return btShapeHull_buildHull(_native, margin);
		}

		public IntPtr IndexPointer => btShapeHull_getIndexPointer(_native);

		public UIntArray Indices
		{
			get
			{
				if (_indices == null)
				{
					_indices = new UIntArray(IndexPointer, NumIndices);
				}
				return _indices;
			}
		}

		public int NumIndices => btShapeHull_numIndices(_native);

		public int NumTriangles => btShapeHull_numTriangles(_native);

		public int NumVertices => btShapeHull_numVertices(_native);

		public IntPtr VertexPointer => btShapeHull_getVertexPointer(_native);

		public Vector3Array Vertices
		{
			get
			{
				if (_vertices == null || _vertices.Count != NumVertices)
				{
					_vertices = new Vector3Array(VertexPointer, NumVertices);
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
				btShapeHull_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ShapeHull()
		{
			Dispose(false);
		}
	}
}
