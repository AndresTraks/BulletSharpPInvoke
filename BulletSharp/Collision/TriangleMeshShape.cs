using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class TriangleMeshShape : ConcaveShape
	{
		protected internal TriangleMeshShape()
		{
		}

		protected internal void InitializeMembers(StridingMeshInterface meshInterface)
		{
			MeshInterface = meshInterface;
		}

		public void LocalGetSupportingVertex(ref Vector3 vec, out Vector3 value)
		{
			btTriangleMeshShape_localGetSupportingVertex(Native, ref vec, out value);
		}

		public Vector3 LocalGetSupportingVertex(Vector3 vec)
		{
			Vector3 value;
			btTriangleMeshShape_localGetSupportingVertex(Native, ref vec, out value);
			return value;
		}

		public void LocalGetSupportingVertexWithoutMargin(ref Vector3 vec, out Vector3 value)
		{
			btTriangleMeshShape_localGetSupportingVertexWithoutMargin(Native, ref vec,
				out value);
		}

		public Vector3 LocalGetSupportingVertexWithoutMargin(Vector3 vec)
		{
			Vector3 value;
			btTriangleMeshShape_localGetSupportingVertexWithoutMargin(Native, ref vec,
				out value);
			return value;
		}

		public void RecalcLocalAabb()
		{
			btTriangleMeshShape_recalcLocalAabb(Native);
		}

		public Vector3 LocalAabbMax
		{
			get
			{
				Vector3 value;
				btTriangleMeshShape_getLocalAabbMax(Native, out value);
				return value;
			}
		}

		public Vector3 LocalAabbMin
		{
			get
			{
				Vector3 value;
				btTriangleMeshShape_getLocalAabbMin(Native, out value);
				return value;
			}
		}

		public StridingMeshInterface MeshInterface { get; private set; }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct TriangleMeshShapeData
	{
		public CollisionShapeData CollisionShapeData;
		public StridingMeshInterfaceData MeshInterface;
		public IntPtr QuantizedFloatBvh;
		public IntPtr QuantizedDoubleBvh;
		public IntPtr TriangleInfoMap;
		public float CollisionMargin;
		public int Pad;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(TriangleMeshShapeData), fieldName).ToInt32(); }
	}
}
