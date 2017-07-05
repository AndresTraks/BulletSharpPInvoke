using BulletSharp.Math;
using System;

namespace BulletSharp
{
	public class GImpactQuantizedBvhNode : IDisposable
	{
		internal IntPtr _native;

		internal GImpactQuantizedBvhNode(IntPtr native)
		{
			_native = native;
		}

		public GImpactQuantizedBvhNode()
		{
			_native = UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_new();
		}

		public bool TestQuantizedBoxOverlapp(ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(_native, quantizedMin, quantizedMax);
		}

		public int DataIndex
		{
			get => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getDataIndex(_native);
			set => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_setDataIndex(_native, value);
		}

		public int EscapeIndex
		{
			get => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getEscapeIndex(_native);
			set => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_setEscapeIndex(_native, value);
		}

		public int EscapeIndexOrDataIndex
		{
			get => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(_native);
			set => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(_native, value);
		}

		public bool IsLeafNode => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_isLeafNode(_native);
		/*
		public UShortArray QuantizedAabbMax
		{
			get => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(_native);
		}

		public UShortArray QuantizedAabbMin
		{
			get => UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(_native);
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
				UnsafeNativeMethods.BT_QUANTIZED_BVH_NODE_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~GImpactQuantizedBvhNode()
		{
			Dispose(false);
		}
	}

	public class GImpactQuantizedBvhNodeArray
	{
		internal IntPtr _native;

		internal GImpactQuantizedBvhNodeArray(IntPtr native)
		{
			_native = native;
		}
/*
		public GimGImpactQuantizedBvhNodeArray()
		{
			_native = UnsafeNativeMethods.GIM_QUANTIZED_BVH_NODE_ARRAY_new();
		}
*/
	}

	public class QuantizedBvhTree : IDisposable
	{
		internal IntPtr _native;

		internal QuantizedBvhTree(IntPtr native)
		{
			_native = native;
		}

		public QuantizedBvhTree()
		{
			_native = UnsafeNativeMethods.btQuantizedBvhTree_new();
		}

		public void BuildTree(GimBvhDataArray primitiveBoxes)
		{
			UnsafeNativeMethods.btQuantizedBvhTree_build_tree(_native, primitiveBoxes._native);
		}

		public void ClearNodes()
		{
			UnsafeNativeMethods.btQuantizedBvhTree_clearNodes(_native);
		}
		/*
		public GImpactQuantizedBvhNode GetNodePointer(int index = 0)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_get_node_pointer(_native, index);
		}
		*/
		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_getEscapeNodeIndex(_native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_getLeftNode(_native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btQuantizedBvhTree_getNodeBound(_native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_getNodeData(_native, nodeIndex);
		}

		public int GetRightNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_getRightNode(_native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_isLeafNode(_native, nodeIndex);
		}

		public void QuantizePoint(ushort[] quantizedpoint, Vector3 point)
		{
			UnsafeNativeMethods.btQuantizedBvhTree_quantizePoint(_native, quantizedpoint, ref point);
		}

		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btQuantizedBvhTree_setNodeBound(_native, nodeIndex, bound.Native);
		}

		public bool TestQuantizedBoxOverlap(int nodeIndex, ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return UnsafeNativeMethods.btQuantizedBvhTree_testQuantizedBoxOverlapp(_native, nodeIndex, quantizedMin, quantizedMax);
		}

		public int NodeCount => UnsafeNativeMethods.btQuantizedBvhTree_getNodeCount(_native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btQuantizedBvhTree_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~QuantizedBvhTree()
		{
			Dispose(false);
		}
	}

	public class GImpactQuantizedBvh : IDisposable
	{
		internal IntPtr _native;

		private PrimitiveManagerBase _primitiveManager;

		internal GImpactQuantizedBvh(IntPtr native)
		{
			_native = native;
		}

		public GImpactQuantizedBvh()
		{
			_native = UnsafeNativeMethods.btGImpactQuantizedBvh_new();
		}

		public GImpactQuantizedBvh(PrimitiveManagerBase primitiveManager)
		{
			_native = UnsafeNativeMethods.btGImpactQuantizedBvh_new2(primitiveManager.Native);
			_primitiveManager = primitiveManager;
		}
		/*
		public bool BoxQuery(Aabb box, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_boxQuery(_native, box._native, collidedResults._native);
		}

		public bool BoxQueryTrans(Aabb box, Matrix transform, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_boxQueryTrans(_native, box._native, ref transform,
				collidedResults._native);
		}
		*/
		public void BuildSet()
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_buildSet(_native);
		}

		public static void FindCollision(GImpactQuantizedBvh boxset1, Matrix trans1,
			GImpactQuantizedBvh boxset2, Matrix trans2, PairSet collisionPairs)
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_find_collision(boxset1._native, ref trans1, boxset2._native,
				ref trans2, collisionPairs.Native);
		}
		/*
		public GImpactQuantizedBvhNode GetNodePointer(int index = 0)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_get_node_pointer(_native, index);
		}
		*/
		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_getEscapeNodeIndex(_native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_getLeftNode(_native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_getNodeBound(_native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_getNodeData(_native, nodeIndex);
		}

		public void GetNodeTriangle(int nodeIndex, PrimitiveTriangle triangle)
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_getNodeTriangle(_native, nodeIndex, triangle.Native);
		}

		public int GetRightNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_getRightNode(_native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_isLeafNode(_native, nodeIndex);
		}
		/*
		public bool RayQuery(Vector3 rayDir, Vector3 rayOrigin, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactQuantizedBvh_rayQuery(_native, ref rayDir, ref rayOrigin,
				collidedResults._native);
		}
		*/
		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_setNodeBound(_native, nodeIndex, bound.Native);
		}

		public void Update()
		{
			UnsafeNativeMethods.btGImpactQuantizedBvh_update(_native);
		}

		public Aabb GlobalBox => new Aabb(UnsafeNativeMethods.btGImpactQuantizedBvh_getGlobalBox(_native));

		public bool HasHierarchy => UnsafeNativeMethods.btGImpactQuantizedBvh_hasHierarchy(_native);

		public bool IsTrimesh => UnsafeNativeMethods.btGImpactQuantizedBvh_isTrimesh(_native);

		public int NodeCount => UnsafeNativeMethods.btGImpactQuantizedBvh_getNodeCount(_native);

		public PrimitiveManagerBase PrimitiveManager
		{
			get => _primitiveManager;
			set
			{
				UnsafeNativeMethods.btGImpactQuantizedBvh_setPrimitiveManager(_native, value.Native);
				_primitiveManager = value;
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
				UnsafeNativeMethods.btGImpactQuantizedBvh_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~GImpactQuantizedBvh()
		{
			Dispose(false);
		}
	}
}
