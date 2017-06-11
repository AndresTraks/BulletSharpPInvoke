using BulletSharp.Math;
using System;

namespace BulletSharp
{
	public class GImpactPair : IDisposable
	{
		internal IntPtr Native;

		public GImpactPair()
		{
			Native = UnsafeNativeMethods.GIM_PAIR_new();
		}

		public GImpactPair(GImpactPair pair)
		{
			Native = UnsafeNativeMethods.GIM_PAIR_new2(pair.Native);
		}

		public GImpactPair(int index1, int index2)
		{
			Native = UnsafeNativeMethods.GIM_PAIR_new3(index1, index2);
		}

		public int Index1
		{
			get { return UnsafeNativeMethods.GIM_PAIR_getIndex1(Native); }
			set { UnsafeNativeMethods.GIM_PAIR_setIndex1(Native, value); }
		}

		public int Index2
		{
			get { return UnsafeNativeMethods.GIM_PAIR_getIndex2(Native); }
			set { UnsafeNativeMethods.GIM_PAIR_setIndex2(Native, value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.GIM_PAIR_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GImpactPair()
		{
			Dispose(false);
		}
	}

	public class PairSet : IDisposable
	{
		internal IntPtr Native;

		public PairSet()
		{
			Native = UnsafeNativeMethods.btPairSet_new();
		}

		public void PushPair(int index1, int index2)
		{
			UnsafeNativeMethods.btPairSet_push_pair(Native, index1, index2);
		}

		public void PushPairInv(int index1, int index2)
		{
			UnsafeNativeMethods.btPairSet_push_pair_inv(Native, index1, index2);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btPairSet_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~PairSet()
		{
			Dispose(false);
		}
	}

	public sealed class GImpactBvhData : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal GImpactBvhData(IntPtr native)
		{
			Native = native;
			_preventDelete = true;
		}

		public GImpactBvhData()
		{
			Native = UnsafeNativeMethods.GIM_BVH_DATA_new();
		}

		public Aabb Bound
		{
			get { return new Aabb(UnsafeNativeMethods.GIM_BVH_DATA_getBound(Native)); }
			set { UnsafeNativeMethods.GIM_BVH_DATA_setBound(Native, value.Native); }
		}

		public int Data
		{
			get { return UnsafeNativeMethods.GIM_BVH_DATA_getData(Native); }
			set { UnsafeNativeMethods.GIM_BVH_DATA_setData(Native, value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					UnsafeNativeMethods.GIM_BVH_DATA_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~GImpactBvhData()
		{
			Dispose(false);
		}
	}

	public class GimBvhTreeNode : IDisposable
	{
		internal IntPtr Native;

		internal GimBvhTreeNode(IntPtr native)
		{
			Native = native;
		}

		public GimBvhTreeNode()
		{
			Native = UnsafeNativeMethods.GIM_BVH_TREE_NODE_new();
		}

		public Aabb Bound
		{
			get { return new Aabb(UnsafeNativeMethods.GIM_BVH_TREE_NODE_getBound(Native)); }
			set { UnsafeNativeMethods.GIM_BVH_TREE_NODE_setBound(Native, value.Native); }
		}

		public int DataIndex
		{
			get { return UnsafeNativeMethods.GIM_BVH_TREE_NODE_getDataIndex(Native); }
			set { UnsafeNativeMethods.GIM_BVH_TREE_NODE_setDataIndex(Native, value); }
		}

		public int EscapeIndex
		{
			get { return UnsafeNativeMethods.GIM_BVH_TREE_NODE_getEscapeIndex(Native); }
			set { UnsafeNativeMethods.GIM_BVH_TREE_NODE_setEscapeIndex(Native, value); }
		}

		public bool IsLeafNode
		{
			get { return UnsafeNativeMethods.GIM_BVH_TREE_NODE_isLeafNode(Native); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.GIM_BVH_TREE_NODE_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~GimBvhTreeNode()
		{
			Dispose(false);
		}
	}

	public class GimBvhDataArray
	{
		internal IntPtr _native;

		internal GimBvhDataArray(IntPtr native)
		{
			_native = native;
		}
		/*
		public GimBvhDataArray()
		{
			_native = UnsafeNativeMethods.GIM_BVH_DATA_ARRAY_new();
		}
		*/
	}

	public class GimBvhTreeNodeArray
	{
		internal IntPtr _native;

		internal GimBvhTreeNodeArray(IntPtr native)
		{
			_native = native;
		}
		/*
		public GimBvhTreeNodeArray()
		{
			_native = UnsafeNativeMethods.GIM_BVH_TREE_NODE_ARRAY_new();
		}
		*/
	}

	public class BvhTree : IDisposable
	{
		internal IntPtr _native;

		internal BvhTree(IntPtr native)
		{
			_native = native;
		}

		public BvhTree()
		{
			_native = UnsafeNativeMethods.btBvhTree_new();
		}

		public void BuildTree(GimBvhDataArray primitiveBoxes)
		{
			UnsafeNativeMethods.btBvhTree_build_tree(_native, primitiveBoxes._native);
		}

		public void ClearNodes()
		{
			UnsafeNativeMethods.btBvhTree_clearNodes(_native);
		}

		public GimBvhTreeNode GetNodePointer()
		{
			return new GimBvhTreeNode(UnsafeNativeMethods.btBvhTree_get_node_pointer(_native));
		}

		public GimBvhTreeNode GetNodePointer(int index)
		{
			return new GimBvhTreeNode(UnsafeNativeMethods.btBvhTree_get_node_pointer2(_native, index));
		}

		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return UnsafeNativeMethods.btBvhTree_getEscapeNodeIndex(_native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btBvhTree_getLeftNode(_native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btBvhTree_getNodeBound(_native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return UnsafeNativeMethods.btBvhTree_getNodeData(_native, nodeIndex);
		}

		public int GetRightNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btBvhTree_getRightNode(_native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btBvhTree_isLeafNode(_native, nodeIndex);
		}

		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btBvhTree_setNodeBound(_native, nodeIndex, bound.Native);
		}

		public int NodeCount
		{
			get { return UnsafeNativeMethods.btBvhTree_getNodeCount(_native); }
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
				UnsafeNativeMethods.btBvhTree_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~BvhTree()
		{
			Dispose(false);
		}
	}

	public class PrimitiveManagerBase : IDisposable
	{
		internal IntPtr Native;

		internal PrimitiveManagerBase(IntPtr native)
		{
			Native = native;
		}

		public void GetPrimitiveBox(int primitiveIndex, Aabb primitiveBox)
		{
			UnsafeNativeMethods.btPrimitiveManagerBase_get_primitive_box(Native, primitiveIndex, primitiveBox.Native);
		}

		public void GetPrimitiveTriangle(int primitiveIndex, PrimitiveTriangle triangle)
		{
			UnsafeNativeMethods.btPrimitiveManagerBase_get_primitive_triangle(Native, primitiveIndex, triangle.Native);
		}

		public bool IsTrimesh
		{
			get { return UnsafeNativeMethods.btPrimitiveManagerBase_is_trimesh(Native); }
		}

		public int PrimitiveCount
		{
			get { return UnsafeNativeMethods.btPrimitiveManagerBase_get_primitive_count(Native); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				UnsafeNativeMethods.btPrimitiveManagerBase_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~PrimitiveManagerBase()
		{
			Dispose(false);
		}
	}

	public class GImpactBvh : IDisposable
	{
		internal IntPtr _native;

		private PrimitiveManagerBase _primitiveManager;

		internal GImpactBvh(IntPtr native)
		{
			_native = native;
		}

		public GImpactBvh()
		{
			_native = UnsafeNativeMethods.btGImpactBvh_new();
		}

		public GImpactBvh(PrimitiveManagerBase primitiveManager)
		{
			_native = UnsafeNativeMethods.btGImpactBvh_new2(primitiveManager.Native);
			_primitiveManager = primitiveManager;
		}
		/*
		public bool BoxQuery(Aabb box, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactBvh_boxQuery(_native, box._native, collidedResults._native);
		}

		public bool BoxQueryTrans(Aabb box, Matrix transform, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactBvh_boxQueryTrans(_native, box._native, ref transform,
				collidedResults._native);
		}
		*/
		public void BuildSet()
		{
			UnsafeNativeMethods.btGImpactBvh_buildSet(_native);
		}

		public static void FindCollision(GImpactBvh boxSet1, ref Matrix transform1, GImpactBvh boxSet2,
			ref Matrix transform2, PairSet collisionPairs)
		{
			UnsafeNativeMethods.btGImpactBvh_find_collision(boxSet1._native, ref transform1, boxSet2._native,
				ref transform2, collisionPairs.Native);
		}

		public GimBvhTreeNode GetNodePointer(int index = 0)
		{
			return new GimBvhTreeNode(UnsafeNativeMethods.btGImpactBvh_get_node_pointer(_native, index));
		}

		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactBvh_getEscapeNodeIndex(_native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactBvh_getLeftNode(_native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btGImpactBvh_getNodeBound(_native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactBvh_getNodeData(_native, nodeIndex);
		}

		public void GetNodeTriangle(int nodeIndex, PrimitiveTriangle triangle)
		{
			UnsafeNativeMethods.btGImpactBvh_getNodeTriangle(_native, nodeIndex, triangle.Native);
		}

		public int GetRightNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactBvh_getRightNode(_native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return UnsafeNativeMethods.btGImpactBvh_isLeafNode(_native, nodeIndex);
		}
		/*
		public bool RayQuery(Vector3 rayDir, Vector3 rayOrigin, AlignedIntArray collidedResults)
		{
			return UnsafeNativeMethods.btGImpactBvh_rayQuery(_native, ref rayDir, ref rayOrigin, collidedResults._native);
		}
		*/
		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			UnsafeNativeMethods.btGImpactBvh_setNodeBound(_native, nodeIndex, bound.Native);
		}

		public void Update()
		{
			UnsafeNativeMethods.btGImpactBvh_update(_native);
		}

		public Aabb GlobalBox
		{
			get { return new Aabb(UnsafeNativeMethods.btGImpactBvh_getGlobalBox(_native)); }
		}

		public bool HasHierarchy
		{
			get { return UnsafeNativeMethods.btGImpactBvh_hasHierarchy(_native); }
		}

		public bool IsTrimesh
		{
			get { return UnsafeNativeMethods.btGImpactBvh_isTrimesh(_native); }
		}

		public int NodeCount
		{
			get { return UnsafeNativeMethods.btGImpactBvh_getNodeCount(_native); }
		}

		public PrimitiveManagerBase PrimitiveManager
		{
			get { return new PrimitiveManagerBase(UnsafeNativeMethods.btGImpactBvh_getPrimitiveManager(_native)); }
			set { UnsafeNativeMethods.btGImpactBvh_setPrimitiveManager(_native, value.Native); }
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
				UnsafeNativeMethods.btGImpactBvh_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~GImpactBvh()
		{
			Dispose(false);
		}
	}
}
