using BulletSharp.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr BT_BOX_BOX_TRANSFORM_CACHE_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_absolute_matrix(IntPtr native);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_from_full_invert(IntPtr native, [In] ref Matrix transform0, [In] ref Matrix transform1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_from_homogenic(IntPtr native, [In] ref Matrix transform0, [In] ref Matrix transform1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_getAR(IntPtr native, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_getR1to0(IntPtr native, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_getT1to0(IntPtr native, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_setAR(IntPtr native, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_setR1to0(IntPtr native, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_setT1to0(IntPtr native, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_transform(IntPtr obj, [In] ref Vector3 point, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_BOX_BOX_TRANSFORM_CACHE_delete(IntPtr native);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr BT_QUANTIZED_BVH_NODE_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int BT_QUANTIZED_BVH_NODE_getDataIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int BT_QUANTIZED_BVH_NODE_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool BT_QUANTIZED_BVH_NODE_isLeafNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_QUANTIZED_BVH_NODE_setDataIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_QUANTIZED_BVH_NODE_setEscapeIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(IntPtr obj, ushort[] quantizedMin, ushort[] quantizedMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void BT_QUANTIZED_BVH_NODE_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new2([In] ref Vector3 V1, [In] ref Vector3 V2, [In] ref Vector3 V3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new3([In] ref Vector3 V1, [In] ref Vector3 V2, [In] ref Vector3 V3, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new4(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAABB_new5(IntPtr other, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_appy_transform(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_appy_transform_trans_cache(IntPtr obj, IntPtr trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_collide_plane(IntPtr obj, [In] ref Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_collide_ray(IntPtr obj, [In] ref Vector3 vorigin, [In] ref Vector3 vdir);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_collide_triangle_exact(IntPtr obj, [In] ref Vector3 p1, [In] ref Vector3 p2, [In] ref Vector3 p3, [In] ref Vector4 triangle_plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_copy_with_margin(IntPtr obj, IntPtr other, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_find_intersection(IntPtr obj, IntPtr other, IntPtr intersection);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_get_center_extend(IntPtr obj, out Vector3 center, out Vector3 extend);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_getMax(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_getMin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_has_collision(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_increment_margin(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_invalidate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_merge(IntPtr obj, IntPtr box);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_overlapping_trans_cache(IntPtr obj, IntPtr box, IntPtr transcache, bool fulltest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_overlapping_trans_conservative(IntPtr obj, IntPtr box, [In] ref Matrix trans1_to_0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAABB_overlapping_trans_conservative2(IntPtr obj, IntPtr box, IntPtr trans1_to_0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PlaneIntersectionType btAABB_plane_classify(IntPtr obj, [In] ref Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_projection_interval(IntPtr obj, [In] ref Vector3 direction, out float vmin, out float vmax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_setMax(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_setMin(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAABB_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTree_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_build_tree(IntPtr obj, IntPtr primitive_boxes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_clearNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTree_get_node_pointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTree_get_node_pointer2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBvhTree_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBvhTree_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTree_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_new2(IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_boxQuery(IntPtr obj, IntPtr box, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_boxQueryTrans(IntPtr obj, IntPtr box, [In] ref Matrix transform, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_buildSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_find_collision(IntPtr boxset1, [In] ref Matrix trans1, IntPtr boxset2, [In] ref Matrix trans2, IntPtr collision_pairs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_get_node_pointer(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_getGlobalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_getNodeTriangle(IntPtr obj, int nodeindex, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactBvh_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactBvh_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_hasHierarchy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_isTrimesh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactBvh_rayQuery(IntPtr obj, [In] ref Vector3 ray_dir, [In] ref Vector3 ray_origin, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_setPrimitiveManager(IntPtr obj, IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_update(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactBvh_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactCollisionAlgorithm_getFace0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactCollisionAlgorithm_getFace1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactCollisionAlgorithm_getPart0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactCollisionAlgorithm_getPart1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_gimpact_vs_compoundshape(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_gimpact_vs_concave(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_gimpact_vs_gimpact(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_gimpact_vs_shape(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr shape0, IntPtr shape1, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCollisionAlgorithm_internalGetResultOut(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_registerAlgorithm(IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_setFace0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_setFace1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_setPart0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCollisionAlgorithm_setPart1(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new(IntPtr meshInterface, int part);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(IntPtr manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new3();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(IntPtr obj, int face_index, out uint i0, out uint i1, out uint i2b);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(IntPtr obj, uint vertex_index, out Vector3 vertex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_new2(IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_boxQuery(IntPtr obj, IntPtr box, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_boxQueryTrans(IntPtr obj, IntPtr box, [In] ref Matrix transform, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_buildSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_find_collision(IntPtr boxset1, [In] ref Matrix trans1, IntPtr boxset2, [In] ref Matrix trans2, IntPtr collision_pairs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_get_node_pointer(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_getGlobalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_getNodeTriangle(IntPtr obj, int nodeindex, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactQuantizedBvh_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactQuantizedBvh_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_hasHierarchy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_isTrimesh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactQuantizedBvh_rayQuery(IntPtr obj, [In] ref Vector3 ray_dir, [In] ref Vector3 ray_origin, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_setPrimitiveManager(IntPtr obj, IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_update(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactQuantizedBvh_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btIndexedMesh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btIndexedMesh_getIndexType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btIndexedMesh_getNumTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btIndexedMesh_getNumVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btIndexedMesh_getTriangleIndexBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btIndexedMesh_getTriangleIndexStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btIndexedMesh_getVertexBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btIndexedMesh_getVertexStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern PhyScalarType btIndexedMesh_getVertexType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setIndexType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setNumTriangles(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setNumVertices(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setTriangleIndexBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setTriangleIndexStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setVertexBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setVertexStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_setVertexType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btIndexedMesh_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPairSet_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_push_pair(IntPtr obj, int index1, int index2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_push_pair_inv(IntPtr obj, int index1, int index2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveManagerBase_get_primitive_box(IntPtr obj, int prim_index, IntPtr primbox);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPrimitiveManagerBase_get_primitive_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveManagerBase_get_primitive_triangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPrimitiveManagerBase_is_trimesh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveManagerBase_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvhTree_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_build_tree(IntPtr obj, IntPtr primitive_boxes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_clearNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btQuantizedBvhTree_get_node_pointer(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btQuantizedBvhTree_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btQuantizedBvhTree_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_quantizePoint(IntPtr obj, ushort[] quantizedpoint, [In] ref Vector3 point);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btQuantizedBvhTree_testQuantizedBoxOverlapp(IntPtr obj, int node_index, ushort[] quantizedMin, ushort[] quantizedMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btQuantizedBvhTree_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_DATA_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_DATA_getBound(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_BVH_DATA_getData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_DATA_setBound(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_DATA_setData(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_DATA_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_DATA_ARRAY_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_TREE_NODE_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_TREE_NODE_getBound(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_BVH_TREE_NODE_getDataIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_BVH_TREE_NODE_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool GIM_BVH_TREE_NODE_isLeafNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_TREE_NODE_setBound(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_TREE_NODE_setDataIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_TREE_NODE_setEscapeIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_BVH_TREE_NODE_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_BVH_TREE_NODE_ARRAY_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_PAIR_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_PAIR_new2(IntPtr p);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_PAIR_new3(int index1, int index2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_PAIR_getIndex1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_PAIR_getIndex2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_PAIR_setIndex1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_PAIR_setIndex2(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_PAIR_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_QUANTIZED_BVH_NODE_ARRAY_new();
	}
}
