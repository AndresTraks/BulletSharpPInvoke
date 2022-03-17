using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Triangle : BulletDisposableObject
	{
		internal Triangle(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public Triangle()
		{
			IntPtr native = btTriangle_new();
			InitializeUserOwned(native);
		}

		public int PartId
		{
			get => btTriangle_getPartId(Native);
			set => btTriangle_setPartId(Native, value);
		}

		public int TriangleIndex
		{
			get => btTriangle_getTriangleIndex(Native);
			set => btTriangle_setTriangleIndex(Native, value);
		}

		public Vector3 Vertex0
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex0(Native, out value);
				return value;
			}
			set => btTriangle_setVertex0(Native, ref value);
		}

		public Vector3 Vertex1
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex1(Native, out value);
				return value;
			}
			set => btTriangle_setVertex1(Native, ref value);
		}

		public Vector3 Vertex2
		{
			get
			{
				Vector3 value;
				btTriangle_getVertex2(Native, out value);
				return value;
			}
			set => btTriangle_setVertex2(Native, ref value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btTriangle_delete(Native);
			}
		}
	}

	public class TriangleBuffer : TriangleCallback
	{
		/*
		public TriangleBuffer()
			: base(btTriangleBuffer_new())
		{
		}
		*/
		public TriangleBuffer()
		{
		}

		public void ClearBuffer()
		{
			btTriangleBuffer_clearBuffer(Native);
		}

		public Triangle GetTriangle(int index)
		{
			return new Triangle(btTriangleBuffer_getTriangle(Native, index), this);
		}

		public override void ProcessTriangle(ref Vector3 vector0, ref Vector3 vector1, ref Vector3 vector2, int partId, int triangleIndex)
		{
			throw new NotImplementedException();
		}

		public int NumTriangles => btTriangleBuffer_getNumTriangles(Native);
	}
}
