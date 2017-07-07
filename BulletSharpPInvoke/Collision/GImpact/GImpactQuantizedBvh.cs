using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

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
			_native = BT_QUANTIZED_BVH_NODE_new();
		}

		public bool TestQuantizedBoxOverlapp(ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(_native, quantizedMin, quantizedMax);
		}

		public int DataIndex
		{
			get => BT_QUANTIZED_BVH_NODE_getDataIndex(_native);
			set => BT_QUANTIZED_BVH_NODE_setDataIndex(_native, value);
		}

		public int EscapeIndex
		{
			get => BT_QUANTIZED_BVH_NODE_getEscapeIndex(_native);
			set => BT_QUANTIZED_BVH_NODE_setEscapeIndex(_native, value);
		}

		public int EscapeIndexOrDataIndex
		{
			get => BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(_native);
			set => BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(_native, value);
		}

		public bool IsLeafNode => BT_QUANTIZED_BVH_NODE_isLeafNode(_native);
		/*
		public UShortArray QuantizedAabbMax
		{
			get => BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(_native);
		}

		public UShortArray QuantizedAabbMin
		{
			get => BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(_native);
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
				BT_QUANTIZED_BVH_NODE_delete(_native);
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
			_native = GIM_QUANTIZED_BVH_NODE_ARRAY_new();
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
			_native = btQuantizedBvhTree_new();
		}

		public void BuildTree(GimBvhDataArray primitiveBoxes)
		{
			btQuantizedBvhTree_build_tree(_native, primitiveBoxes._native);
		}

		public void ClearNodes()
		{
			btQuantizedBvhTree_clearNodes(_native);
		}
		/*
		public GImpactQuantizedBvhNode GetNodePointer(int index = 0)
		{
			return btQuantizedBvhTree_get_node_pointer(_native, index);
		}
		*/
		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return btQuantizedBvhTree_getEscapeNodeIndex(_native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return btQuantizedBvhTree_getLeftNode(_native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			btQuantizedBvhTree_getNodeBound(_native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return btQuantizedBvhTree_getNodeData(_native, nodeIndex);
		}

		public int GetRightNode(int nodeIndex)
		{
			return btQuantizedBvhTree_getRightNode(_native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return btQuantizedBvhTree_isLeafNode(_native, nodeIndex);
		}

		public void QuantizePoint(ushort[] quantizedpoint, Vector3 point)
		{
			btQuantizedBvhTree_quantizePoint(_native, quantizedpoint, ref point);
		}

		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			btQuantizedBvhTree_setNodeBound(_native, nodeIndex, bound.Native);
		}

		public bool TestQuantizedBoxOverlap(int nodeIndex, ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return btQuantizedBvhTree_testQuantizedBoxOverlapp(_native, nodeIndex, quantizedMin, quantizedMax);
		}

		public int NodeCount => btQuantizedBvhTree_getNodeCount(_native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btQuantizedBvhTree_delete(_native);
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
			_native = btGImpactQuantizedBvh_new();
		}

		public GImpactQuantizedBvh(PrimitiveManagerBase primitiveManager)
		{
			_native = btGImpactQuantizedBvh_new2(primitiveManager.Native);
			_primitiveManager = primitiveManager;
		}
		/*
		public bool BoxQuery(Aabb box, AlignedIntArray collidedResults)
		{
			return btGImpactQuantizedBvh_boxQuery(_native, box._native, collidedResults._native);
		}

		public bool BoxQueryTrans(Aabb box, Matrix transform, AlignedIntArray collidedResults)
		{
			return btGImpactQuantizedBvh_boxQueryTrans(_native, box._native, ref transform,
				collidedResults._native);
		}
		*/
		public void BuildSet()
		{
			btGImpactQuantizedBvh_buildSet(_native);
		}

		public static void FindCollision(GImpactQuantizedBvh boxset1, Matrix trans1,
			GImpactQuantizedBvh boxset2, Matrix trans2, PairSet collisionPairs)
		{
			btGImpactQuantizedBvh_find_collision(boxset1._native, ref trans1, boxset2._native,
				ref trans2, collisionPairs.Native);
		}
		/*
		public GImpactQuantizedBvhNode GetNodePointer(int index = 0)
		{
			return btGImpactQuantizedBvh_get_node_pointer(_native, index);
		}
		*/
		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return btGImpactQuantizedBvh_getEscapeNodeIndex(_native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return btGImpactQuantizedBvh_getLeftNode(_native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			btGImpactQuantizedBvh_getNodeBound(_native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return btGImpactQuantizedBvh_getNodeData(_native, nodeIndex);
		}

		public void GetNodeTriangle(int nodeIndex, PrimitiveTriangle triangle)
		{
			btGImpactQuantizedBvh_getNodeTriangle(_native, nodeIndex, triangle.Native);
		}

		public int GetRightNode(int nodeIndex)
		{
			return btGImpactQuantizedBvh_getRightNode(_native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return btGImpactQuantizedBvh_isLeafNode(_native, nodeIndex);
		}
		/*
		public bool RayQuery(Vector3 rayDir, Vector3 rayOrigin, AlignedIntArray collidedResults)
		{
			return btGImpactQuantizedBvh_rayQuery(_native, ref rayDir, ref rayOrigin,
				collidedResults._native);
		}
		*/
		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			btGImpactQuantizedBvh_setNodeBound(_native, nodeIndex, bound.Native);
		}

		public void Update()
		{
			btGImpactQuantizedBvh_update(_native);
		}

		public Aabb GlobalBox => new Aabb(btGImpactQuantizedBvh_getGlobalBox(_native));

		public bool HasHierarchy => btGImpactQuantizedBvh_hasHierarchy(_native);

		public bool IsTrimesh => btGImpactQuantizedBvh_isTrimesh(_native);

		public int NodeCount => btGImpactQuantizedBvh_getNodeCount(_native);

		public PrimitiveManagerBase PrimitiveManager
		{
			get => _primitiveManager;
			set
			{
				btGImpactQuantizedBvh_setPrimitiveManager(_native, value.Native);
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
				btGImpactQuantizedBvh_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~GImpactQuantizedBvh()
		{
			Dispose(false);
		}
	}
}
