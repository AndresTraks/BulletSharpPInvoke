using System.Numerics;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GImpactPair : BulletDisposableObject
	{
		public GImpactPair()
		{
			IntPtr native = GIM_PAIR_new();
			InitializeUserOwned(native);
		}

		public GImpactPair(GImpactPair pair)
		{
			IntPtr native = GIM_PAIR_new2(pair.Native);
			InitializeUserOwned(native);
		}

		public GImpactPair(int index1, int index2)
		{
			IntPtr native = GIM_PAIR_new3(index1, index2);
			InitializeUserOwned(native);
		}

		public int Index1
		{
			get => GIM_PAIR_getIndex1(Native);
			set => GIM_PAIR_setIndex1(Native, value);
		}

		public int Index2
		{
			get => GIM_PAIR_getIndex2(Native);
			set => GIM_PAIR_setIndex2(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			GIM_PAIR_delete(Native);
		}
	}

	public class PairSet : BulletDisposableObject
	{
		public PairSet()
		{
			IntPtr native = btPairSet_new();
			InitializeUserOwned(native);
		}

		public void PushPair(int index1, int index2)
		{
			btPairSet_push_pair(Native, index1, index2);
		}

		public void PushPairInv(int index1, int index2)
		{
			btPairSet_push_pair_inv(Native, index1, index2);
		}

		protected override void Dispose(bool disposing)
		{
			btPairSet_delete(Native);
		}
	}

	public sealed class GImpactBvhData : BulletDisposableObject
	{
		private Aabb _bound;

		public GImpactBvhData()
		{
			IntPtr native = GIM_BVH_DATA_new();
			InitializeUserOwned(native);
		}

		public Aabb Bound
		{
			get => _bound ?? (_bound = new Aabb(GIM_BVH_DATA_getBound(Native), this));
			set
			{
				GIM_BVH_DATA_setBound(Native, value.Native);
				_bound = value;
			}
		}

		public int Data
		{
			get => GIM_BVH_DATA_getData(Native);
			set => GIM_BVH_DATA_setData(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				GIM_BVH_DATA_delete(Native);
			}
		}
	}

	public class GimBvhTreeNode : BulletDisposableObject
	{
		private Aabb _bound;

		internal GimBvhTreeNode(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public GimBvhTreeNode()
		{
			IntPtr native = GIM_BVH_TREE_NODE_new();
			InitializeUserOwned(native);
		}

		public Aabb Bound
		{
			get => _bound ?? (_bound = new Aabb(GIM_BVH_TREE_NODE_getBound(Native), this));
			set
			{
				GIM_BVH_TREE_NODE_setBound(Native, value.Native);
				_bound = value;
			}
		}

		public int DataIndex
		{
			get => GIM_BVH_TREE_NODE_getDataIndex(Native);
			set => GIM_BVH_TREE_NODE_setDataIndex(Native, value);
		}

		public int EscapeIndex
		{
			get => GIM_BVH_TREE_NODE_getEscapeIndex(Native);
			set => GIM_BVH_TREE_NODE_setEscapeIndex(Native, value);
		}

		public bool IsLeafNode => GIM_BVH_TREE_NODE_isLeafNode(Native);

		protected override void Dispose(bool disposing)
		{
			GIM_BVH_TREE_NODE_delete(Native);
		}
	}

	public class GimBvhDataArray
	{
		internal IntPtr Native;

		internal GimBvhDataArray(IntPtr native)
		{
			Native = native;
		}
		/*
		public GimBvhDataArray()
		{
			Native = GIM_BVH_DATA_ARRAY_new();
		}
		*/
	}

	public class GimBvhTreeNodeArray
	{
		internal IntPtr Native;

		internal GimBvhTreeNodeArray(IntPtr native)
		{
			Native = native;
		}
		/*
		public GimBvhTreeNodeArray()
		{
			Native = GIM_BVH_TREE_NODE_ARRAY_new();
		}
		*/
	}

	public class BvhTree : BulletDisposableObject
	{
		internal BvhTree(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public BvhTree()
		{
			IntPtr native = btBvhTree_new();
			InitializeUserOwned(native);
		}

		public void BuildTree(GimBvhDataArray primitiveBoxes)
		{
			btBvhTree_build_tree(Native, primitiveBoxes.Native);
		}

		public void ClearNodes()
		{
			btBvhTree_clearNodes(Native);
		}

		public GimBvhTreeNode GetNodePointer()
		{
			return new GimBvhTreeNode(btBvhTree_get_node_pointer(Native), this);
		}

		public GimBvhTreeNode GetNodePointer(int index)
		{
			return new GimBvhTreeNode(btBvhTree_get_node_pointer2(Native, index), this);
		}

		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return btBvhTree_getEscapeNodeIndex(Native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return btBvhTree_getLeftNode(Native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			btBvhTree_getNodeBound(Native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return btBvhTree_getNodeData(Native, nodeIndex);
		}

		public int GetRightNode(int nodeIndex)
		{
			return btBvhTree_getRightNode(Native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return btBvhTree_isLeafNode(Native, nodeIndex);
		}

		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			btBvhTree_setNodeBound(Native, nodeIndex, bound.Native);
		}

		public int NodeCount => btBvhTree_getNodeCount(Native);

		protected override void Dispose(bool disposing)
		{
			btBvhTree_delete(Native);
		}
	}

	public class PrimitiveManagerBase : BulletDisposableObject
	{
		protected internal PrimitiveManagerBase(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		protected internal PrimitiveManagerBase()
		{
		}

		public void GetPrimitiveBox(int primitiveIndex, Aabb primitiveBox)
		{
			btPrimitiveManagerBase_get_primitive_box(Native, primitiveIndex, primitiveBox.Native);
		}

		public void GetPrimitiveTriangle(int primitiveIndex, PrimitiveTriangle triangle)
		{
			btPrimitiveManagerBase_get_primitive_triangle(Native, primitiveIndex, triangle.Native);
		}

		public bool IsTrimesh => btPrimitiveManagerBase_is_trimesh(Native);

		public int PrimitiveCount => btPrimitiveManagerBase_get_primitive_count(Native);

		protected override void Dispose(bool disposing)
		{
			btPrimitiveManagerBase_delete(Native);
		}
	}

	public class GImpactBvh : BulletDisposableObject
	{
		private PrimitiveManagerBase _primitiveManager;
		private Aabb _globalBox;

		internal GImpactBvh(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public GImpactBvh()
		{
			IntPtr native = btGImpactBvh_new();
			InitializeUserOwned(native);
		}

		public GImpactBvh(PrimitiveManagerBase primitiveManager)
		{
			IntPtr native = btGImpactBvh_new2(primitiveManager.Native);
			InitializeUserOwned(native);
			_primitiveManager = primitiveManager;
		}
		/*
		public bool BoxQuery(Aabb box, AlignedIntArray collidedResults)
		{
			return btGImpactBvh_boxQuery(Native, box.Native, collidedResults.Native);
		}

		public bool BoxQueryTrans(Aabb box, Matrix transform, AlignedIntArray collidedResults)
		{
			return btGImpactBvh_boxQueryTrans(Native, box.Native, ref transform,
				collidedResults.Native);
		}
		*/
		public void BuildSet()
		{
			btGImpactBvh_buildSet(Native);
		}

		public static void FindCollision(GImpactBvh boxSet1, ref Matrix4x4 transform1, GImpactBvh boxSet2,
			ref Matrix4x4 transform2, PairSet collisionPairs)
		{
			btGImpactBvh_find_collision(boxSet1.Native, ref transform1, boxSet2.Native,
				ref transform2, collisionPairs.Native);
		}

		public GimBvhTreeNode GetNodePointer(int index = 0)
		{
			return new GimBvhTreeNode(btGImpactBvh_get_node_pointer(Native, index), this);
		}

		public int GetEscapeNodeIndex(int nodeIndex)
		{
			return btGImpactBvh_getEscapeNodeIndex(Native, nodeIndex);
		}

		public int GetLeftNode(int nodeIndex)
		{
			return btGImpactBvh_getLeftNode(Native, nodeIndex);
		}

		public void GetNodeBound(int nodeIndex, Aabb bound)
		{
			btGImpactBvh_getNodeBound(Native, nodeIndex, bound.Native);
		}

		public int GetNodeData(int nodeIndex)
		{
			return btGImpactBvh_getNodeData(Native, nodeIndex);
		}

		public void GetNodeTriangle(int nodeIndex, PrimitiveTriangle triangle)
		{
			btGImpactBvh_getNodeTriangle(Native, nodeIndex, triangle.Native);
		}

		public int GetRightNode(int nodeIndex)
		{
			return btGImpactBvh_getRightNode(Native, nodeIndex);
		}

		public bool IsLeafNode(int nodeIndex)
		{
			return btGImpactBvh_isLeafNode(Native, nodeIndex);
		}
		/*
		public bool RayQuery(Vector3 rayDir, Vector3 rayOrigin, AlignedIntArray collidedResults)
		{
			return btGImpactBvh_rayQuery(Native, ref rayDir, ref rayOrigin, collidedResults.Native);
		}
		*/
		public void SetNodeBound(int nodeIndex, Aabb bound)
		{
			btGImpactBvh_setNodeBound(Native, nodeIndex, bound.Native);
		}

		public void Update()
		{
			btGImpactBvh_update(Native);
		}

		public Aabb GlobalBox => _globalBox ?? (_globalBox = new Aabb(btGImpactBvh_getGlobalBox(Native), this));

		public bool HasHierarchy => btGImpactBvh_hasHierarchy(Native);

		public bool IsTrimesh => btGImpactBvh_isTrimesh(Native);

		public int NodeCount => btGImpactBvh_getNodeCount(Native);

		public PrimitiveManagerBase PrimitiveManager
		{
			get => _primitiveManager ?? (_primitiveManager = new PrimitiveManagerBase(btGImpactBvh_getPrimitiveManager(Native), this));
			set => btGImpactBvh_setPrimitiveManager(Native, value.Native);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btGImpactBvh_delete(Native);
			}
		}
	}
}
