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
		public static extern IntPtr bt32BitAxisSweep3_new([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, uint maxHandles, IntPtr pairCache, bool disableRaycastAccelerator);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint bt32BitAxisSweep3_addHandle(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr pOwner, int collisionFilterGroup, int collisionFilterMask, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr bt32BitAxisSweep3_getHandle(IntPtr obj, uint index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint bt32BitAxisSweep3_getNumHandles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_quantize(IntPtr obj, uint o, [In] ref Vector3 point, int isMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_removeHandle(IntPtr obj, uint handle, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_setOverlappingPairUserCallback(IntPtr obj, IntPtr pairCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool bt32BitAxisSweep3_testAabbOverlap(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_unQuantize(IntPtr obj, IntPtr proxy, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void bt32BitAxisSweep3_updateHandle(IntPtr obj, uint handle, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);

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
		public static extern IntPtr btAxisSweep3_new([In] ref Vector3 worldAabbMin, [In] ref Vector3 worldAabbMax, ushort maxHandles, IntPtr pairCache, bool disableRaycastAccelerator);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ushort btAxisSweep3_addHandle(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr pOwner, int collisionFilterGroup, int collisionFilterMask, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btAxisSweep3_getHandle(IntPtr obj, ushort index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ushort btAxisSweep3_getNumHandles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_quantize(IntPtr obj, ushort o, [In] ref Vector3 point, int isMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_removeHandle(IntPtr obj, ushort handle, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_setOverlappingPairUserCallback(IntPtr obj, IntPtr pairCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btAxisSweep3_testAabbOverlap(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_unQuantize(IntPtr obj, IntPtr proxy, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btAxisSweep3_updateHandle(IntPtr obj, ushort handle, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dBox2dCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dBox2dCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dBox2dCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_new([In] ref Vector3 boxHalfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_new2(float boxHalfExtent);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_new3(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBox2dShape_getCentroid(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBox2dShape_getHalfExtentsWithMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBox2dShape_getHalfExtentsWithoutMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_getNormals(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBox2dShape_getPlaneEquation(IntPtr obj, out Vector4 plane, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBox2dShape_getVertices(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxBoxCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxBoxCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxBoxCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxBoxDetector_new(IntPtr box1, IntPtr box2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxBoxDetector_setBox1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxBoxDetector_setBox2(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxShape_new([In] ref Vector3 boxHalfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxShape_new2(float boxHalfExtent);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBoxShape_new3(float boxHalfExtentX, float boxHalfExtentY, float boxHalfExtentZ);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxShape_getHalfExtentsWithMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxShape_getHalfExtentsWithoutMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBoxShape_getPlaneEquation(IntPtr obj, out Vector4 plane, int i);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseAabbCallbackWrapper_new(IntPtr process);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseAabbCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_aabbTest(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_calculateOverlappingPairs(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseInterface_createProxy(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, int shapeType, IntPtr userPtr, int collisionFilterGroup, int collisionFilterMask, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_destroyProxy(IntPtr obj, IntPtr proxy, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_getAabb(IntPtr obj, IntPtr proxy, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_getBroadphaseAabb(IntPtr obj, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseInterface_getOverlappingPairCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_printStats(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_rayTest3(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_resetPool(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_setAabb(IntPtr obj, IntPtr proxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseInterface_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphasePair_getAlgorithm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphasePair_getPProxy0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphasePair_getPProxy1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphasePair_setAlgorithm(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphasePair_setPProxy0(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphasePair_setPProxy1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphasePair_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_getAabbMax(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_getAabbMin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseProxy_getClientObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBroadphaseProxy_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBroadphaseProxy_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBroadphaseProxy_getUid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBroadphaseProxy_getUniqueId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isCompound(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isConcave(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isConvex(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isConvex2d(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isInfinite(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isNonMoving(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isPolyhedral(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBroadphaseProxy_isSoftBody(BroadphaseNativeType proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setAabbMax(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setAabbMin(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setClientObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setCollisionFilterGroup(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setCollisionFilterMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_setUniqueId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseProxy_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseRayCallbackWrapper_new(IntPtr process);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btBroadphaseRayCallback_getLambda_max(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseRayCallback_getRayDirectionInverse(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBroadphaseRayCallback_getSigns(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseRayCallback_setLambda_max(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBroadphaseRayCallback_setRayDirectionInverse(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new2([In] ref Vector3 pt0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new3([In] ref Vector3 pt0, [In] ref Vector3 pt1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new4([In] ref Vector3 pt0, [In] ref Vector3 pt1, [In] ref Vector3 pt2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBU_Simplex1to4_new5([In] ref Vector3 pt0, [In] ref Vector3 pt1, [In] ref Vector3 pt2, [In] ref Vector3 pt3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBU_Simplex1to4_addVertex(IntPtr obj, [In] ref Vector3 pt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btBU_Simplex1to4_getIndex(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBU_Simplex1to4_reset(IntPtr obj);

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
		public static extern IntPtr btBvhTriangleMeshShape_new(IntPtr meshInterface, bool useQuantizedAabbCompression, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTriangleMeshShape_new2(IntPtr meshInterface, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_buildOptimizedBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTriangleMeshShape_getOptimizedBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBvhTriangleMeshShape_getOwnsBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btBvhTriangleMeshShape_getTriangleInfoMap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_partialRefitTree(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_performConvexcast(IntPtr obj, IntPtr callback, [In] ref Vector3 boxSource, [In] ref Vector3 boxTarget, [In] ref Vector3 boxMin, [In] ref Vector3 boxMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_performRaycast(IntPtr obj, IntPtr callback, [In] ref Vector3 raySource, [In] ref Vector3 rayTarget);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_refitTree(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_serializeSingleBvh(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_serializeSingleTriangleInfoMap(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_setOptimizedBvh(IntPtr obj, IntPtr bvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_setOptimizedBvh2(IntPtr obj, IntPtr bvh, [In] ref Vector3 localScaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btBvhTriangleMeshShape_setTriangleInfoMap(IntPtr obj, IntPtr triangleInfoMap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btBvhTriangleMeshShape_usesQuantizedAabbCompression(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCapsuleShape_new(float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCapsuleShape_getHalfHeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCapsuleShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCapsuleShape_getUpAxis(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCapsuleShapeX_new(float radius, float height);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCapsuleShapeZ_new(float radius, float height);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionAlgorithm_calculateTimeOfImpact(IntPtr obj, IntPtr body0, IntPtr body1, IntPtr dispatchInfo, IntPtr resultOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithm_getAllContactManifolds(IntPtr obj, IntPtr manifoldArray);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithm_processCollision(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr dispatchInfo, IntPtr resultOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithm_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionAlgorithmConstructionInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionAlgorithmConstructionInfo_new2(IntPtr dispatcher, int temp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmConstructionInfo_setDispatcher1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmConstructionInfo_setManifold(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmConstructionInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionAlgorithmCreateFunc_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(IntPtr obj, IntPtr __unnamed0, IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionAlgorithmCreateFunc_getSwapped(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmCreateFunc_setSwapped(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionAlgorithmCreateFunc_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionConfiguration_getClosestPointsAlgorithmCreateFunc(IntPtr obj, int proxyType0, int proxyType1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionConfiguration_getCollisionAlgorithmCreateFunc(IntPtr obj, int proxyType0, int proxyType1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionConfiguration_getCollisionAlgorithmPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionConfiguration_getPersistentManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionConfiguration_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionDispatcher_new(IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_defaultNearCallback(IntPtr collisionPair, IntPtr dispatcher, IntPtr dispatchInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionDispatcher_getCollisionConfiguration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern DispatcherFlags btCollisionDispatcher_getDispatcherFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionDispatcher_getNearCallback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_registerCollisionCreateFunc(IntPtr obj, BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1, IntPtr createFunc);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_registerClosestPointsCreateFunc(IntPtr obj, BroadphaseNativeType proxyType0, BroadphaseNativeType proxyType1, IntPtr createFunc);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_setCollisionConfiguration(IntPtr obj, IntPtr config);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_setDispatcherFlags(IntPtr obj, DispatcherFlags flags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionDispatcher_setNearCallback(IntPtr obj, IntPtr nearCallback);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionDispatcherMt_new(IntPtr collisionConfiguration, int grainSize);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_activate(IntPtr obj, bool forceActivation);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_checkCollideWith(IntPtr obj, IntPtr co);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_checkCollideWithOverride(IntPtr obj, IntPtr co);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_forceActivationState(IntPtr obj, ActivationState newState);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ActivationState btCollisionObject_getActivationState(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getAnisotropicFriction(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_getBroadphaseHandle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getCcdMotionThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getCcdSquareMotionThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getCcdSweptSphereRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern CollisionFlags btCollisionObject_getCollisionFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_getCollisionShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getCompanionId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getContactDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getContactProcessingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getContactStiffness(IntPtr obj);
		[return: MarshalAs(UnmanagedType.I1)]
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern bool btCollisionObject_getCustomDebugColor(IntPtr obj, out Vector3 colorRGB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getDeactivationTime(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern CollisionObjectTypes btCollisionObject_getInternalType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getInterpolationAngularVelocity(IntPtr obj, out Vector3 angvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getInterpolationLinearVelocity(IntPtr obj, out Vector3 linvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getInterpolationWorldTransform(IntPtr obj, out Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getIslandTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionObject_getSpinningFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getWorldArrayIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getUserIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObject_getUserIndex2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_getWorldTransform(IntPtr obj, out Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_hasAnisotropicFriction(IntPtr obj, AnisotropicFrictionFlags frictionMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_hasContactResponse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_internalGetExtensionPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_internalSetExtensionPointer(IntPtr obj, IntPtr pointer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_isActive(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_isKinematicObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_isStaticObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_isStaticOrKinematicObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionObject_mergesSimulationIslands(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_removeCustomDebugColor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObject_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_serializeSingleObject(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setActivationState(IntPtr obj, ActivationState newState);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setAnisotropicFriction(IntPtr obj, [In] ref Vector3 anisotropicFriction, AnisotropicFrictionFlags frictionMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setBroadphaseHandle(IntPtr obj, IntPtr handle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCcdMotionThreshold(IntPtr obj, float ccdMotionThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCcdSweptSphereRadius(IntPtr obj, float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCollisionFlags(IntPtr obj, CollisionFlags flags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCollisionShape(IntPtr obj, IntPtr collisionShape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCompanionId(IntPtr obj, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setContactProcessingThreshold(IntPtr obj, float contactProcessingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setContactStiffnessAndDamping(IntPtr obj, float stiffness, float damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setCustomDebugColor(IntPtr obj, ref Vector3 colorRGB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setDeactivationTime(IntPtr obj, float time);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setFriction(IntPtr obj, float frict);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setHitFraction(IntPtr obj, float hitFraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setIgnoreCollisionCheck(IntPtr obj, IntPtr co, bool ignoreCollisionCheck);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setInterpolationAngularVelocity(IntPtr obj, [In] ref Vector3 angvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setInterpolationLinearVelocity(IntPtr obj, [In] ref Vector3 linvel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setInterpolationWorldTransform(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setIslandTag(IntPtr obj, int tag);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setRestitution(IntPtr obj, float rest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setRollingFriction(IntPtr obj, float frict);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setSpinningFriction(IntPtr obj, float frict);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setWorldArrayIndex(IntPtr obj, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setUserIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setUserIndex2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setUserPointer(IntPtr obj, IntPtr userPointer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_setWorldTransform(IntPtr obj, [In] ref Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObject_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObjectWrapper_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObjectWrapper_getCollisionShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObjectWrapper_getIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionObjectWrapper_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionObjectWrapper_getPartId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_getWorldTransform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setParent(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setPartId(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionObjectWrapper_setShape(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_calculateLocalInertia(IntPtr obj, float mass, out Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionShape_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_calculateTemporalAabb(IntPtr obj, [In] ref Matrix curTrans, [In] ref Vector3 linvel, [In] ref Vector3 angvel, float timeStep, out Vector3 temporalAabbMin, out Vector3 temporalAabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_getAabb(IntPtr obj, [In] ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionShape_getAngularMotionDisc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_getAnisotropicRollingFrictionDirection(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_getBoundingSphere(IntPtr obj, out Vector3 center, out float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionShape_getContactBreakingThreshold(IntPtr obj, float defaultContactThresholdFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_getLocalScaling(IntPtr obj, out Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionShape_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionShape_getName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern BroadphaseNativeType btCollisionShape_getShapeType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionShape_getUserIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionShape_getUserPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isCompound(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isConcave(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isConvex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isConvex2d(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isInfinite(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isNonMoving(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isPolyhedral(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionShape_isSoftBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionShape_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_serializeSingleShape(IntPtr obj, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_setLocalScaling(IntPtr obj, [In] ref Vector3 scaling);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_setMargin(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_setUserIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_setUserPointer(IntPtr obj, IntPtr userPtr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionShape_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_ContactResultCallback_getClosestDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_ContactResultCallback_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ContactResultCallback_setClosestDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ContactResultCallback_setCollisionFilterMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ContactResultCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_ContactResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_ContactResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_ConvexResultCallback_getClosestHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_ConvexResultCallback_hasHit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ConvexResultCallback_setClosestHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_ConvexResultCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_ConvexResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_ConvexResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalConvexResult_new(IntPtr hitCollisionObject, IntPtr localShapeInfo, [In] ref Vector3 hitNormalLocal, [In] ref Vector3 hitPointLocal, float hitFraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalConvexResult_getHitCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_LocalConvexResult_getHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_getHitNormalLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_getHitPointLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalConvexResult_getLocalShapeInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setHitCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setHitNormalLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setHitPointLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_setLocalShapeInfo(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalConvexResult_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalRayResult_new(IntPtr collisionObject, IntPtr localShapeInfo, [In] ref Vector3 hitNormalLocal, float hitFraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalRayResult_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_LocalRayResult_getHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_getHitNormalLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalRayResult_getLocalShapeInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_setHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_setHitNormalLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_setLocalShapeInfo(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalRayResult_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_LocalShapeInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_LocalShapeInfo_getShapePart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_LocalShapeInfo_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalShapeInfo_setShapePart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalShapeInfo_setTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_LocalShapeInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCollisionWorld_RayResultCallback_getClosestHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_RayResultCallback_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_RayResultCallback_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_RayResultCallback_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btCollisionWorld_RayResultCallback_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_RayResultCallback_hasHit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setClosestHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setCollisionFilterMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_setFlags(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_RayResultCallback_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_RayResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_RayResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_new(IntPtr dispatcher, IntPtr broadphasePairCache, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_computeOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_contactPairTest(IntPtr obj, IntPtr colObjA, IntPtr colObjB, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_contactTest(IntPtr obj, IntPtr colObj, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_convexSweepTest(IntPtr obj, IntPtr castShape, [In] ref Matrix from, [In] ref Matrix to, IntPtr resultCallback, float allowedCcdPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_debugDrawObject(IntPtr obj, [In] ref Matrix worldTransform, IntPtr shape, [In] ref Vector3 color);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_debugDrawWorld(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getBroadphase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		internal static extern IntPtr btCollisionWorld_getCollisionObjectArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getDebugDrawer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getDispatcher(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getDispatchInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btCollisionWorld_getForceUpdateAllAabbs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCollisionWorld_getNumCollisionObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCollisionWorld_getPairCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_objectQuerySingle(IntPtr castShape, [In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObject, IntPtr collisionShape, [In] ref Matrix colObjWorldTransform, IntPtr resultCallback, float allowedPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_objectQuerySingleInternal(IntPtr castShape, [In] ref Matrix convexFromTrans, [In] ref Matrix convexToTrans, IntPtr colObjWrap, IntPtr resultCallback, float allowedPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_performDiscreteCollisionDetection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_rayTest(IntPtr obj, [In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_rayTestSingle([In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObject, IntPtr collisionShape, [In] ref Matrix colObjWorldTransform, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_rayTestSingleInternal([In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObjectWrap, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_setBroadphase(IntPtr obj, IntPtr pairCache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_setDebugDrawer(IntPtr obj, IntPtr debugDrawer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_setForceUpdateAllAabbs(IntPtr obj, bool forceUpdateAllAabbs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_updateAabbs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_updateSingleAabb(IntPtr obj, IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCollisionWorld_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCollisionAlgorithm_SwappedCreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCollisionAlgorithm_getChildAlgorithm(IntPtr obj, int n);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCompoundCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCompoundCollisionAlgorithm_SwappedCreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundCompoundCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShape_new(bool enableDynamicAabbTree, int initialChildCapacity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_calculatePrincipalAxisTransform(IntPtr obj, float[] masses, ref Matrix principal, out Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_createAabbTreeFromChildren(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShape_getChildShape(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_getChildTransform(IntPtr obj, int index, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShape_getDynamicAabbTree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCompoundShape_getUpdateRevision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_recalculateLocalAabb(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShape_updateChildTransform(IntPtr obj, int childIndex, [In] ref Matrix newChildTransform, bool shouldRecalculateLocalAabb);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCompoundShapeChild_getChildMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern BroadphaseNativeType btCompoundShapeChild_getChildShapeType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCompoundShapeChild_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_getTransform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setChildMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setChildShape(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setChildShapeType(IntPtr obj, BroadphaseNativeType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCompoundShapeChild_setTransform(IntPtr obj, [In] ref Matrix value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConcaveShape_processAllTriangles(IntPtr obj, IntPtr callback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConeShape_new(float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConeShape_getConeUpIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeShape_getHeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConeShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeShape_setConeUpIndex(IntPtr obj, int upIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeShape_setHeight(IntPtr obj, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConeShape_setRadius(IntPtr obj, float radius);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConeShapeX_new(float radius, float height);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConeShapeZ_new(float radius, float height);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConstraintSolverPoolMt_new(int numSolvers);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConstraintSolverPoolMt_new2(IntPtr solvers, int numSolvers);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btContinuousConvexCollision_new(IntPtr shapeA, IntPtr shapeB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btContinuousConvexCollision_new2(IntPtr shapeA, IntPtr plane);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvex2dConvex2dAlgorithm_CreateFunc_new(IntPtr simplexSolver, IntPtr pdSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvex2dConvex2dAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvex2dConvex2dAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setPdSolver(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setSimplexSolver(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvex2dConvex2dAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr simplexSolver, IntPtr pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvex2dConvex2dAlgorithm_getManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvex2dConvex2dAlgorithm_setLowLevelOfDetail(IntPtr obj, bool useLowLevel);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvex2dShape_new(IntPtr convexChildShape);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexCast_CastResult_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_DebugDraw(IntPtr obj, float fraction);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_drawCoordSystem(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexCast_CastResult_getAllowedPenetration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexCast_CastResult_getDebugDrawer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexCast_CastResult_getFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_getHitPoint(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_getHitTransformA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_getHitTransformB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_reportFailure(IntPtr obj, int errNo, int numIterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setAllowedPenetration(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setDebugDrawer(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setHitPoint(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setHitTransformA(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setHitTransformB(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_CastResult_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConvexCast_calcTimeOfImpact(IntPtr obj, [In] ref Matrix fromA, [In] ref Matrix toA, [In] ref Matrix fromB, [In] ref Matrix toB, IntPtr result);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexCast_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConcaveCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConcaveCollisionAlgorithm_SwappedCreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConcaveCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConcaveCollisionAlgorithm_clearCache(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConvexAlgorithm_CreateFunc_new(IntPtr pdSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexConvexAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexConvexAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConvexAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConvexAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConvexAlgorithm_CreateFunc_setPdSolver(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConvexAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr simplexSolver, IntPtr pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexConvexAlgorithm_getManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexConvexAlgorithm_setLowLevelOfDetail(IntPtr obj, bool useLowLevel);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexHullShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexHullShape_new4(float[] points, int numPoints, int stride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexHullShape_addPoint(IntPtr obj, [In] ref Vector3 point, bool recalculateLocalAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexHullShape_getNumPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexHullShape_getPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexHullShape_getScaledPoint(IntPtr obj, int i, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexHullShape_getUnscaledPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexHullShape_optimizeConvexHull(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalAabbCachingShape_recalcLocalAabb(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_getImplicitShapeDimensions(IntPtr obj, out Vector3 dimensions);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_getLocalScalingNV(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexInternalShape_getMarginNV(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_setImplicitShapeDimensions(IntPtr obj, [In] ref Vector3 dimensions);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_setSafeMargin(IntPtr obj, float minDimension, float defaultMarginMultiplier);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexInternalShape_setSafeMargin2(IntPtr obj, [In] ref Vector3 halfExtents, float defaultMarginMultiplier);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConvexPenetrationDepthSolver_calcPenDepth(IntPtr obj, IntPtr simplexSolver, IntPtr convexA, IntPtr convexB, [In] ref Matrix transA, [In] ref Matrix transB, out Vector3 v, out Vector3 pa, out Vector3 pb, IntPtr debugDraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPenetrationDepthSolver_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPlaneCollisionAlgorithm_CreateFunc_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexPlaneCollisionAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexPlaneCollisionAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPlaneCollisionAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPlaneCollisionAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPlaneCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPlaneCollisionAlgorithm_collideSingleContact(IntPtr obj, [In] ref Quaternion perturbeRot, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr dispatchInfo, IntPtr resultOut);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPointCloudShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPointCloudShape_new2(IntPtr points, int numPoints, [In] ref Vector3 localScaling, bool computeAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexPointCloudShape_getNumPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPointCloudShape_getScaledPoint(IntPtr obj, int index, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPointCloudShape_getUnscaledPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPointCloudShape_setPoints(IntPtr obj, IntPtr points, int numPoints, bool computeAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPointCloudShape_setPoints2(IntPtr obj, IntPtr points, int numPoints, bool computeAabb, [In] ref Vector3 localScaling);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPolyhedron_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_getExtents(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPolyhedron_getFaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_getLocalCenter(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_getMC(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_getME(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexPolyhedron_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPolyhedron_getUniqueEdges(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexPolyhedron_getVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_initialize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_project(IntPtr obj, [In] ref Matrix trans, [In] ref Vector3 dir, out float minProj, out float maxProj, out Vector3 witnesPtMin, out Vector3 witnesPtMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setExtents(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setLocalCenter(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setMC(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setME(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_setRadius(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btConvexPolyhedron_testContainment(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexPolyhedron_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_getAabbNonVirtual(IntPtr obj, [In] ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_getAabbSlow(IntPtr obj, [In] ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btConvexShape_getMarginNonVirtual(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btConvexShape_getNumPreferredPenetrationDirections(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_getPreferredPenetrationDirection(IntPtr obj, int index, out Vector3 penetrationVector);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_localGetSupportingVertex(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_localGetSupportingVertexWithoutMargin(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_localGetSupportVertexNonVirtual(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_localGetSupportVertexWithoutMarginNonVirtual(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexShape_project(IntPtr obj, [In] ref Matrix trans, [In] ref Vector3 dir, out float minProj, out float maxProj, out Vector3 witnesPtMin, out Vector3 witnesPtMax);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btConvexTriangleMeshShape_new(IntPtr meshInterface, bool calcAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btConvexTriangleMeshShape_calculatePrincipalAxisTransform(IntPtr obj, ref Matrix principal, out Vector3 inertia, out float volume);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShape_new([In] ref Vector3 halfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShape_new2(float halfExtentX, float halfExtentY, float halfExtentZ);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCylinderShape_getHalfExtentsWithMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btCylinderShape_getHalfExtentsWithoutMargin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btCylinderShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btCylinderShape_getUpAxis(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShapeX_new([In] ref Vector3 halfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShapeX_new2(float halfExtentX, float halfExtentY, float halfExtentZ);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShapeZ_new([In] ref Vector3 halfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btCylinderShapeZ_new2(float halfExtentX, float halfExtentY, float halfExtentZ);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtBroadphase_new(IntPtr paircache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_benchmark(IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_collide(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getCid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getCupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtBroadphase_getDeferedcollide(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getDupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getFixedleft(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getFupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getGid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtBroadphase_getNeedcleanup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getNewpairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getPid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvtBroadphase_getPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtBroadphase_getReleasepaircache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtBroadphase_getSets(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtBroadphase_getStageCurrent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtBroadphase_getStageRoots(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btDbvtBroadphase_getUpdates_call(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btDbvtBroadphase_getUpdates_done(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvtBroadphase_getUpdates_ratio(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvtBroadphase_getVelocityPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_optimize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_performDeferredRemoval(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setAabbForceUpdate(IntPtr obj, IntPtr absproxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr __unnamed3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setCid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setCupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setDeferedcollide(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setDupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setFixedleft(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setFupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setGid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setNeedcleanup(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setNewpairs(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setPaircache(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setPid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setPrediction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setReleasepaircache(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setStageCurrent(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setUpdates_call(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setUpdates_done(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setUpdates_ratio(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtBroadphase_setVelocityPrediction(IntPtr obj, float prediction);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_getChilds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_getData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtNode_getDataAsInt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtNode_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtNode_isinternal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtNode_isleaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtNode_setData(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtNode_setDataAsInt(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtNode_setParent(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtProxy_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtProxy_getLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtProxy_getStage(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtProxy_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtProxy_setStage(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_allocate(IntPtr ifree, IntPtr stock, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_benchmark();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_clear(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_clone(IntPtr obj, IntPtr dest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_clone2(IntPtr obj, IntPtr dest, IntPtr iclone);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_countLeaves(IntPtr node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_empty(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_extractLeaves(IntPtr node, IntPtr leaves);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_getFree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_getLeaves(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_getLkhd(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern uint btDbvt_getOpath(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_getRoot(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_getStkStack(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_insert(IntPtr obj, IntPtr box, IntPtr data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_maxdepth(IntPtr node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_nearest(int[] i, IntPtr a, float v, int l, int h);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_optimizeBottomUp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_optimizeIncremental(IntPtr obj, int passes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_optimizeTopDown(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_optimizeTopDown2(IntPtr obj, int bu_treshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_remove(IntPtr obj, IntPtr leaf);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setFree(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setLeaves(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setLkhd(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setOpath(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_setRoot(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_update(IntPtr obj, IntPtr leaf, IntPtr volume);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_update2(IntPtr obj, IntPtr leaf);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_update3(IntPtr obj, IntPtr leaf, int lookahead);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_update4(IntPtr obj, IntPtr leaf, IntPtr volume, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_update5(IntPtr obj, IntPtr leaf, IntPtr volume, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_update6(IntPtr obj, IntPtr leaf, IntPtr volume, [In] ref Vector3 velocity, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_write(IntPtr obj, IntPtr iwriter);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvtAabbMm_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Center(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvtAabbMm_Classify(IntPtr obj, [In] ref Vector3 n, float o, int s);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvtAabbMm_Contain(IntPtr obj, IntPtr a);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Expand(IntPtr obj, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Extents(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromCE([In] ref Vector3 c, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromCR([In] ref Vector3 c, float r);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromMM([In] ref Vector3 mi, [In] ref Vector3 mx);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromPoints([In] ref Vector3 ppts, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_FromPoints2([In] ref Vector3 pts, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Lengths(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Maxs(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_Mins(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvtAabbMm_ProjectMinimum(IntPtr obj, [In] ref Vector3 v, uint signs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_SignedExpand(IntPtr obj, [In] ref Vector3 e);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_tMaxs(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvtAabbMm_tMins(IntPtr obj, out Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_IClone_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IClone_CloneLeaf(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IClone_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_ICollide_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_ICollide_AllLeaves(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDbvt_ICollide_Descent(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_ICollide_Process(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_ICollide_Process2(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_ICollide_Process3(IntPtr obj, IntPtr n, float __unnamed1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_ICollide_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IWriter_Prepare(IntPtr obj, IntPtr root, int numnodes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IWriter_WriteLeaf(IntPtr obj, IntPtr __unnamed0, int index, int parent);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IWriter_WriteNode(IntPtr obj, IntPtr __unnamed0, int index, int parent, int child0, int child1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_IWriter_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkCLN_new(IntPtr n, IntPtr p);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkCLN_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkCLN_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkCLN_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkCLN_setParent(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkCLN_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNN_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNN_new2(IntPtr na, IntPtr nb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNN_getA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNN_getB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNN_setA(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNN_setB(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNN_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNP_new(IntPtr n, uint m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_sStkNP_getMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNP_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNP_setMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNP_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNP_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNPS_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNPS_new2(IntPtr n, uint m, float v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDbvt_sStkNPS_getMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDbvt_sStkNPS_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDbvt_sStkNPS_getValue(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNPS_setMask(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNPS_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNPS_setValue(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDbvt_sStkNPS_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConfiguration_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConfiguration_new2(IntPtr constructionInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(IntPtr obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(IntPtr obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConstructionInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConstructionInfo_getCollisionAlgorithmPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDefaultCollisionConstructionInfo_getCustomCollisionAlgorithmMaxElementSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDefaultCollisionConstructionInfo_getDefaultMaxCollisionAlgorithmPoolSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDefaultCollisionConstructionInfo_getDefaultMaxPersistentManifoldPoolSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDefaultCollisionConstructionInfo_getPersistentManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDefaultCollisionConstructionInfo_getUseEpaPenetrationAlgorithm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setCollisionAlgorithmPool(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setCustomCollisionAlgorithmMaxElementSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setDefaultMaxCollisionAlgorithmPoolSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setDefaultMaxPersistentManifoldPoolSize(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setPersistentManifoldPool(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_setUseEpaPenetrationAlgorithm(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDefaultCollisionConstructionInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDiscreteCollisionDetectorInterface_ClosestPointInput_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_Result_addContactPoint(IntPtr obj, [In] ref Vector3 normalOnBInWorld, [In] ref Vector3 pointInWorld, float depth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(IntPtr obj, int partId0, int index0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(IntPtr obj, int partId1, int index1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_Result_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_getClosestPoints(IntPtr obj, IntPtr input, IntPtr output, IntPtr debugDraw, bool swapResults);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteCollisionDetectorInterface_delete(IntPtr obj);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDiscreteDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_applyGravity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_debugDrawConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDiscreteDynamicsWorld_getApplySpeculativeContactRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDiscreteDynamicsWorld_getLatencyMotionStateInterpolation(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDiscreteDynamicsWorld_getSimulationIslandManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDiscreteDynamicsWorld_getSynchronizeAllMotionStates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_setApplySpeculativeContactRestitution(IntPtr obj, bool enable);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_setLatencyMotionStateInterpolation(IntPtr obj, bool latencyInterpolation);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_setNumTasks(IntPtr obj, int numTasks);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_setSynchronizeAllMotionStates(IntPtr obj, bool synchronizeAll);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_synchronizeSingleMotionState(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDiscreteDynamicsWorld_updateVehicles(IntPtr obj, float timeStep);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDiscreteDynamicsWorldMt_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_allocateCollisionAlgorithm(IntPtr obj, int size);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_clearManifold(IntPtr obj, IntPtr manifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_dispatchAllCollisionPairs(IntPtr obj, IntPtr pairCache, IntPtr dispatchInfo, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_findAlgorithm(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr sharedManifold, DispatcherQueryType queryType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_freeCollisionAlgorithm(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_getInternalManifoldPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_getInternalManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_getManifoldByIndexInternal(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcher_getNewManifold(IntPtr obj, IntPtr b0, IntPtr b1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDispatcher_getNumManifolds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcher_needsCollision(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcher_needsResponse(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_releaseManifold(IntPtr obj, IntPtr manifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcher_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDispatcherInfo_getAllowedCcdPenetration(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDispatcherInfo_getConvexConservativeDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDispatcherInfo_getDebugDraw(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern DispatchFunc btDispatcherInfo_getDispatchFunc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getEnableSatConvex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getEnableSPU(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDispatcherInfo_getStepCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDispatcherInfo_getTimeOfImpact(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btDispatcherInfo_getTimeStep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getUseContinuous(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getUseConvexConservativeDistanceUtil(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btDispatcherInfo_getUseEpa(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setAllowedCcdPenetration(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setConvexConservativeDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setDebugDraw(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setDispatchFunc(IntPtr obj, DispatchFunc value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setEnableSatConvex(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setEnableSPU(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setStepCount(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setTimeOfImpact(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setTimeStep(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setUseContinuous(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setUseConvexConservativeDistanceUtil(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDispatcherInfo_setUseEpa(IntPtr obj, bool value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_addAction(IntPtr obj, IntPtr action);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_addConstraint(IntPtr obj, IntPtr constraint, bool disableCollisionsBetweenLinkedBodies);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_clearForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDynamicsWorld_getConstraint(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDynamicsWorld_getConstraintSolver(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_getGravity(IntPtr obj, out Vector3 gravity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDynamicsWorld_getNumConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btDynamicsWorld_getSolverInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern DynamicsWorldType btDynamicsWorld_getWorldType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_removeAction(IntPtr obj, IntPtr action);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_removeConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_setConstraintSolver(IntPtr obj, IntPtr solver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_setGravity(IntPtr obj, [In] ref Vector3 gravity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_setInternalTickCallback(IntPtr obj, IntPtr cb, IntPtr worldUserInfo, bool isPreTick);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btDynamicsWorld_stepSimulation(IntPtr obj, float timeStep, int maxSubSteps, float fixedTimeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btDynamicsWorld_synchronizeMotionStates(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btEmptyAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btEmptyAlgorithm_new(IntPtr ci);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btEmptyShape_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btFace_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btFace_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGhostObject_addOverlappingObjectInternal(IntPtr obj, IntPtr otherProxy, IntPtr thisProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGhostObject_convexSweepTest(IntPtr obj, IntPtr castShape, [In] ref Matrix convexFromWorld, [In] ref Matrix convexToWorld, IntPtr resultCallback, float allowedCcdPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGhostObject_getNumOverlappingObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostObject_getOverlappingObject(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostObject_getOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGhostObject_rayTest(IntPtr obj, [In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGhostObject_removeOverlappingObjectInternal(IntPtr obj, IntPtr otherProxy, IntPtr dispatcher, IntPtr thisProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostObject_upcast(IntPtr colObj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGhostPairCallback_new();

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
		public static extern IntPtr btGImpactCompoundShape_new(bool children_has_transform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCompoundShape_addChildShape(IntPtr obj, [In] ref Matrix localTransform, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactCompoundShape_addChildShape2(IntPtr obj, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactCompoundShape_getCompoundPrimitiveManager(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShape_new(IntPtr meshInterface);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShape_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShape_getMeshPart(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShape_getMeshPartCount(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_new2(IntPtr meshInterface, int part);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_getPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactMeshShapePart_getTrimeshPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactMeshShapePart_getVertex(IntPtr obj, int vertex_index, out Vector3 vertex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactMeshShapePart_getVertexCount(IntPtr obj);

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
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactShapeInterface_childrenHasTransform(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactShapeInterface_getBoxSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getBulletTetrahedron(IntPtr obj, int prim_index, IntPtr tetrahedron);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getBulletTriangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getChildAabb(IntPtr obj, int child_index, [In] ref Matrix t, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getChildTransform(IntPtr obj, int index, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactShapeInterface_getLocalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGImpactShapeInterface_getNumChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGImpactShapeInterface_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_getPrimitiveTriangle(IntPtr obj, int index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactShapeInterface_hasBoxSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_lockChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactShapeInterface_needsRetrieveTetrahedrons(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btGImpactShapeInterface_needsRetrieveTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_postUpdate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_processAllTrianglesRay(IntPtr obj, IntPtr __unnamed0, [In] ref Vector3 __unnamed1, [In] ref Vector3 __unnamed2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_setChildTransform(IntPtr obj, int index, [In] ref Matrix transform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_unlockChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGImpactShapeInterface_updateBound(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGjkConvexCast_new(IntPtr convexA, IntPtr convexB, IntPtr simplexSolver);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGjkEpaPenetrationDepthSolver_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGjkPairDetector_new(IntPtr objectA, IntPtr objectB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btGjkPairDetector_new2(IntPtr objectA, IntPtr objectB, int shapeTypeA, int shapeTypeB, float marginA, float marginB, IntPtr simplexSolver, IntPtr penetrationDepthSolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_getCachedSeparatingAxis(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btGjkPairDetector_getCachedSeparatingDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getCatchDegeneracies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_getClosestPointsNonVirtual(IntPtr obj, IntPtr input, IntPtr output, IntPtr debugDraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getCurIter(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getDegenerateSimplex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getFixContactNormalDirection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btGjkPairDetector_getLastUsedMethod(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setCachedSeparatingAxis(IntPtr obj, [In] ref Vector3 seperatingAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setCatchDegeneracies(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setCurIter(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setDegenerateSimplex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setFixContactNormalDirection(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setIgnoreMargin(IntPtr obj, bool ignoreMargin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setLastUsedMethod(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setMinkowskiA(IntPtr obj, IntPtr minkA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setMinkowskiB(IntPtr obj, IntPtr minkB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btGjkPairDetector_setPenetrationDepthSolver(IntPtr obj, IntPtr penetrationDepthSolver);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHeightfieldTerrainShape_new(int heightStickWidth, int heightStickLength, IntPtr heightfieldData, float heightScale, float minHeight, float maxHeight, int upAxis, PhyScalarType heightDataType, bool flipQuadEdges);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btHeightfieldTerrainShape_new2(int heightStickWidth, int heightStickLength, IntPtr heightfieldData, float maxHeight, int upAxis, bool useFloatData, bool flipQuadEdges);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHeightfieldTerrainShape_setUseDiamondSubdivision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHeightfieldTerrainShape_setUseDiamondSubdivision2(IntPtr obj, bool useDiamondSubdivision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHeightfieldTerrainShape_setUseZigzagSubdivision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btHeightfieldTerrainShape_setUseZigzagSubdivision2(IntPtr obj, bool useZigzagSubdivision);

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
		public static extern int btITaskScheduler_getMaxNumThreads(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btITaskScheduler_getName(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btITaskScheduler_getNumThreads(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btITaskScheduler_setNumThreads(IntPtr obj, int numThreads);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldPoint_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldPoint_new2([In] ref Vector3 pointA, [In] ref Vector3 pointB, [In] ref Vector3 normal, float distance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getAppliedImpulseLateral1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getAppliedImpulseLateral2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedContactDamping1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedContactStiffness1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getCombinedRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getContactCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getContactERP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getContactMotion1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getContactMotion2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern ContactPointFlags btManifoldPoint_getContactPointFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getDistance1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldPoint_getFrictionCFM(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getIndex0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getIndex1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getLateralFrictionDir1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getLateralFrictionDir2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getLifeTime(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getLocalPointA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getLocalPointB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getNormalWorldOnB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getPartId0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btManifoldPoint_getPartId1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getPositionWorldOnA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_getPositionWorldOnB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldPoint_getUserPersistentData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setAppliedImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setAppliedImpulseLateral1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setAppliedImpulseLateral2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedContactDamping1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedContactStiffness1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setCombinedRollingFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactERP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactMotion1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactMotion2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setContactPointFlags(IntPtr obj, ContactPointFlags value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setDistance(IntPtr obj, float dist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setDistance1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setFrictionCFM(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setIndex0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setIndex1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLateralFrictionDir1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLateralFrictionDir2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLifeTime(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLocalPointA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setLocalPointB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setNormalWorldOnB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setPartId0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setPartId1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setPositionWorldOnA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setPositionWorldOnB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_setUserPersistentData(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldPoint_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_new2(IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedContactDamping(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedContactStiffness(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedFriction(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedRestitution(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_calculateCombinedRollingFriction(IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getBody0Internal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getBody0Wrap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getBody1Internal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getBody1Wrap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btManifoldResult_getClosestPointDistanceThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btManifoldResult_getPersistentManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_refreshContactPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_setBody0Wrap(IntPtr obj, IntPtr obj0Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_setBody1Wrap(IntPtr obj, IntPtr obj1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_setClosestPointDistanceThreshold(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btManifoldResult_setPersistentManifold(IntPtr obj, IntPtr manifoldPtr);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMinkowskiPenetrationDepthSolver_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMinkowskiSumShape_new(IntPtr shapeA, IntPtr shapeB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMinkowskiSumShape_getTransformA(IntPtr obj, out Matrix transA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMinkowskiSumShape_GetTransformB(IntPtr obj, out Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMinkowskiSumShape_setTransformA(IntPtr obj, [In] ref Matrix transA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMinkowskiSumShape_setTransformB(IntPtr obj, [In] ref Matrix transB);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_addMultiBody(IntPtr obj, IntPtr body, int group, int mask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_addMultiBodyConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_clearMultiBodyConstraintForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_clearMultiBodyForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_debugDrawMultiBodyConstraint(IntPtr obj, IntPtr constraint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_forwardKinematics(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodyDynamicsWorld_getNumMultiBodyConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_integrateTransforms(IntPtr obj, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_removeMultiBody(IntPtr obj, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyDynamicsWorld_removeMultiBodyConstraint(IntPtr obj, IntPtr constraint);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyLinkCollider_new(IntPtr multiBody, int link);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiBodyLinkCollider_getLink(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyLinkCollider_getMultiBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyLinkCollider_setLink(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiBodyLinkCollider_setMultiBody(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiBodyLinkCollider_upcast(IntPtr colObj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultimaterialTriangleMeshShape_new(IntPtr meshInterface, bool useQuantizedAabbCompression, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultimaterialTriangleMeshShape_new2(IntPtr meshInterface, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax, bool buildBvh);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiSphereShape_new(Vector3[] positions, float[] radi, int numSpheres);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btMultiSphereShape_new2(IntPtr positions, float[] radi, int numSpheres);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btMultiSphereShape_getSphereCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btMultiSphereShape_getSpherePosition(IntPtr obj, int index, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btMultiSphereShape_getSphereRadius(IntPtr obj, int index);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPairCachingGhostObject_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPairCachingGhostObject_getOverlappingPairCache(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPairSet_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_push_pair(IntPtr obj, int index1, int index2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_push_pair_inv(IntPtr obj, int index1, int index2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPairSet_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPointCollector_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btPointCollector_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPointCollector_getHasResult(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_getNormalOnBInWorld(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_getPointInWorld(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_setDistance(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_setHasResult(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_setNormalOnBInWorld(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPointCollector_setPointInWorld(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(IntPtr obj, [In] ref Matrix trans, out Vector3 aabbMin, out Vector3 aabbMax, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexAabbCachingShape_recalcLocalAabb(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPolyhedralConvexShape_getConvexPolyhedron(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexShape_getEdge(IntPtr obj, int i, out Vector3 pa, out Vector3 pb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPolyhedralConvexShape_getNumEdges(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPolyhedralConvexShape_getNumPlanes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPolyhedralConvexShape_getNumVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexShape_getPlane(IntPtr obj, out Vector3 planeNormal, out Vector3 planeSupport, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPolyhedralConvexShape_getVertex(IntPtr obj, int i, out Vector3 vtx);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPolyhedralConvexShape_initializePolyhedralFeatures(IntPtr obj, int shiftVerticesByMargin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPolyhedralConvexShape_isInside(IntPtr obj, [In] ref Vector3 pt, float tolerance);

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
		public static extern IntPtr btPrimitiveTriangle_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_applyTransform(IntPtr obj, [In] ref Matrix t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_buildTriPlane(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btPrimitiveTriangle_clip_triangle(IntPtr obj, IntPtr other, [Out] out Vector3 clipped_points);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPrimitiveTriangle_find_triangle_collision_clip_method(IntPtr obj, IntPtr other, IntPtr contacts);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_get_edge_plane(IntPtr obj, int edge_index, [Out] out Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btPrimitiveTriangle_getDummy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btPrimitiveTriangle_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_getPlane(IntPtr obj, [Out] out Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btPrimitiveTriangle_getVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btPrimitiveTriangle_overlap_test_conservative(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_setDummy(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_setPlane(IntPtr obj, [In] ref Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btPrimitiveTriangle_delete(IntPtr obj);

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
		public static extern IntPtr btRigidBody_new(IntPtr constructionInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_addConstraintRef(IntPtr obj, IntPtr c);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyCentralForce(IntPtr obj, [In] ref Vector3 force);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyCentralImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyDamping(IntPtr obj, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyForce(IntPtr obj, [In] ref Vector3 force, [In] ref Vector3 rel_pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyGravity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rel_pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyTorque(IntPtr obj, [In] ref Vector3 torque);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_applyTorqueImpulse(IntPtr obj, [In] ref Vector3 torque);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_clearForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_computeAngularImpulseDenominator(IntPtr obj, [In] ref Vector3 axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_computeGyroscopicForceExplicit(IntPtr obj, float maxGyroscopicForce, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_computeGyroscopicImpulseImplicit_Body(IntPtr obj, float step, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_computeGyroscopicImpulseImplicit_World(IntPtr obj, float dt, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_computeImpulseDenominator(IntPtr obj, [In] ref Vector3 pos, [In] ref Vector3 normal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getAabb(IntPtr obj, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getAngularDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getAngularFactor(IntPtr obj, out Vector3 angFac);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getAngularSleepingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getAngularVelocity(IntPtr obj, out Vector3 ang_vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_getBroadphaseProxy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getCenterOfMassPosition(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getCenterOfMassTransform(IntPtr obj, out Matrix xform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btRigidBody_getContactSolverType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern RigidBodyFlags btRigidBody_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btRigidBody_getFrictionSolverType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getGravity(IntPtr obj, out Vector3 acceleration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getInvInertiaDiagLocal(IntPtr obj, out Vector3 diagInvInertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getInvInertiaTensorWorld(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getInvMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getLinearDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getLinearFactor(IntPtr obj, out Vector3 linearFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btRigidBody_getLinearSleepingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getLinearVelocity(IntPtr obj, out Vector3 lin_vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getLocalInertia(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_getMotionState(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getOrientation(IntPtr obj, out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getTotalForce(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getTotalTorque(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_getVelocityInLocalPoint(IntPtr obj, [In] ref Vector3 rel_pos, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_integrateVelocities(IntPtr obj, float step);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRigidBody_isInWorld(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_predictIntegratedTransform(IntPtr obj, float step, out Matrix predictedTransform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_proceedToTransform(IntPtr obj, [In] ref Matrix newTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_removeConstraintRef(IntPtr obj, IntPtr c);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_saveKinematicState(IntPtr obj, float step);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setAngularFactor(IntPtr obj, [In] ref Vector3 angFac);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setAngularFactor2(IntPtr obj, float angFac);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setAngularVelocity(IntPtr obj, [In] ref Vector3 ang_vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setCenterOfMassTransform(IntPtr obj, [In] ref Matrix xform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setContactSolverType(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setDamping(IntPtr obj, float lin_damping, float ang_damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setFlags(IntPtr obj, RigidBodyFlags flags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setFrictionSolverType(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setGravity(IntPtr obj, [In] ref Vector3 acceleration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setInvInertiaDiagLocal(IntPtr obj, [In] ref Vector3 diagInvInertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setLinearFactor(IntPtr obj, [In] ref Vector3 linearFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setLinearVelocity(IntPtr obj, [In] ref Vector3 lin_vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setMassProps(IntPtr obj, float mass, [In] ref Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setMotionState(IntPtr obj, IntPtr motionState);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setNewBroadphaseProxy(IntPtr obj, IntPtr broadphaseProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_setSleepingThresholds(IntPtr obj, float linear, float angular);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_translate(IntPtr obj, [In] ref Vector3 v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btRigidBody_upcast(IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_updateDeactivation(IntPtr obj, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btRigidBody_updateInertiaTensor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btRigidBody_wantsSleeping(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btScaledBvhTriangleMeshShape_new(IntPtr childShape, [In] ref Vector3 localScaling);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btShapeHull_new(IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btShapeHull_buildHull(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btShapeHull_getIndexPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btShapeHull_getVertexPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btShapeHull_numIndices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btShapeHull_numTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btShapeHull_numVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btShapeHull_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_new(IntPtr worldInfo, int node_count, [In] Vector3[] x, [In] float[] m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_new2(IntPtr worldInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addAeroForceToFace(IntPtr obj, [In] ref Vector3 windVelocity, int faceIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addAeroForceToNode(IntPtr obj, [In] ref Vector3 windVelocity, int nodeIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addForce(IntPtr obj, [In] ref Vector3 force);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addForce2(IntPtr obj, [In] ref Vector3 force, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addVelocity(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_addVelocity2(IntPtr obj, [In] ref Vector3 velocity, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAnchor(IntPtr obj, int node, IntPtr body, [In] ref Vector3 localPivot, bool disableCollisionBetweenLinkedBodies, float influence);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAnchor2(IntPtr obj, int node, IntPtr body, bool disableCollisionBetweenLinkedBodies, float influence);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAngularJoint(IntPtr obj, IntPtr specs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAngularJoint2(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAngularJoint3(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendAngularJoint4(IntPtr obj, IntPtr specs, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendFace(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendFace2(IntPtr obj, int node0, int node1, int node2, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLinearJoint(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLinearJoint2(IntPtr obj, IntPtr specs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLinearJoint3(IntPtr obj, IntPtr specs, IntPtr body);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLinearJoint4(IntPtr obj, IntPtr specs, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLink(IntPtr obj, int node0, int node1, IntPtr mat, bool bcheckexist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLink2(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendLink3(IntPtr obj, IntPtr node0, IntPtr node1, IntPtr mat, bool bcheckexist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_appendMaterial(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNode(IntPtr obj, [In] ref Vector3 x, float m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote(IntPtr obj, IntPtr text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote2(IntPtr obj, IntPtr text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote3(IntPtr obj, IntPtr text, [In] ref Vector3 o, IntPtr feature);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote4(IntPtr obj, IntPtr text, [In] ref Vector3 o);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendNote5(IntPtr obj, IntPtr text, [In] ref Vector3 o, [In] ref Vector4 c, IntPtr n0, IntPtr n1, IntPtr n2, IntPtr n3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendTetra(IntPtr obj, int model, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_appendTetra2(IntPtr obj, int node0, int node1, int node2, int node3, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_applyClusters(IntPtr obj, bool drift);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_applyForces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_checkContact(IntPtr obj, IntPtr colObjWrap, [In] ref Vector3 x, float margin, IntPtr cti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_checkFace(IntPtr obj, int node0, int node1, int node2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_checkLink(IntPtr obj, IntPtr node0, IntPtr node1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_checkLink2(IntPtr obj, int node0, int node1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_cleanupClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterAImpulse(IntPtr cluster, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterCom(IntPtr obj, int cluster, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterCom2(IntPtr cluster, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_clusterCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterDAImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterDCImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterDImpulse(IntPtr cluster, [In] ref Vector3 rpos, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterImpulse(IntPtr cluster, [In] ref Vector3 rpos, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterVAImpulse(IntPtr cluster, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterVelocity(IntPtr cluster, [In] ref Vector3 rpos, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_clusterVImpulse(IntPtr cluster, [In] ref Vector3 rpos, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_cutLink(IntPtr obj, IntPtr node0, IntPtr node1, float position);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_cutLink2(IntPtr obj, int node0, int node1, float position);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_dampClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_defaultCollisionHandler(IntPtr obj, IntPtr pcoWrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_defaultCollisionHandler2(IntPtr obj, IntPtr psb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_evaluateCom(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_generateBendingConstraints(IntPtr obj, int distance, IntPtr mat);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_generateClusters(IntPtr obj, int k);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_generateClusters2(IntPtr obj, int k, int maxiterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getAabb(IntPtr obj, out Vector3 aabbMin, out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getAnchors(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getBounds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_getBUpdateRtCst(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getCdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getCfg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getClusterConnectivity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getCollisionDisabledObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getFaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getFdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getInitialWorldTransform(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getJoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getMass(IntPtr obj, int node);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getMaterials(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getNdbvt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getNotes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getPose(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getRcontacts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getRestLengthScale(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getScontacts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getSst(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getTetras(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getTimeacc(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getTotalMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getUserIndexMapping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_getWindVelocity(IntPtr obj, out Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_getWorldInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_indicesToPointers(IntPtr obj, int[] map);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_initDefaults(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_initializeClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_initializeFaceTree(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_integrateMotion(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_pointersToIndices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_predictMotion(IntPtr obj, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_prepareClusters(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_PSolve_Anchors(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_PSolve_Links(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_PSolve_RContacts(IntPtr psb, float kst, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_PSolve_SContacts(IntPtr psb, float __unnamed1, float ti);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_randomizeConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_rayTest2(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, ref float mint, out SoftBody.FeatureType feature, out int index, bool bcountonly);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_refine(IntPtr obj, IntPtr ifn, float accurary, bool cut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_releaseCluster(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_releaseClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_resetLinkRestLengths(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_rotate(IntPtr obj, [In] ref Quaternion rot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_scale(IntPtr obj, [In] ref Vector3 scl);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setBUpdateRtCst(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setInitialWorldTransform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setMass(IntPtr obj, int node, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setPose(IntPtr obj, bool bvolume, bool bframe);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setRestLengthScale(IntPtr obj, float restLength);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setSoftBodySolver(IntPtr obj, IntPtr softBodySolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setTag(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setTimeacc(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setTotalDensity(IntPtr obj, float density);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setTotalMass(IntPtr obj, float mass, bool fromfaces);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setVelocity(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setWindVelocity(IntPtr obj, [In] ref Vector3 velocity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setVolumeDensity(IntPtr obj, float density);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setVolumeMass(IntPtr obj, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_setWorldInfo(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_solveClusters(IntPtr bodies);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_solveClusters2(IntPtr obj, float sor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_solveCommonConstraints(IntPtr bodies, int count, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_solveConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_staticSolve(IntPtr obj, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_transform(IntPtr obj, [In] ref Matrix trs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_translate(IntPtr obj, [In] ref Vector3 trs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_upcast(IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateArea(IntPtr obj, bool averageArea);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateBounds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateConstants(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateLinkConstants(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updateNormals(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_updatePose(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_VSolve_Links(IntPtr psb, float kst);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexData(IntPtr obj, [In, Out] float[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexData(IntPtr obj, [In, Out] Vector3[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexNormalData(IntPtr obj, [In, Out] float[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexNormalData2(IntPtr obj, [In, Out] float[] vertices, [In, Out] float[] normals);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexNormalData(IntPtr obj, [In, Out] Vector3[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getFaceVertexNormalData2(IntPtr obj, [In, Out] Vector3[] vertices, [In, Out] Vector3[] normals);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getLinkVertexData(IntPtr obj, [In, Out] float[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getLinkVertexData(IntPtr obj, [In, Out] Vector3[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getLinkVertexNormalData(IntPtr obj, [In, Out] float[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getLinkVertexNormalData(IntPtr obj, [In, Out] Vector3[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexData(IntPtr obj, [In, Out] float[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexData(IntPtr obj, Vector3[] vertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexNormalData(IntPtr obj, [In, Out] float[] data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexNormalData2(IntPtr obj, [In, Out] float[] vectors, [In, Out] float[] normals);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexNormalData(IntPtr obj, [In, Out] Vector3[] value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_getTetraVertexNormalData2(IntPtr obj, [In, Out] Vector3[] vectors, [In, Out] Vector3[] normals);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_IControlWrapper_new(IntPtr prepareCallback, IntPtr speedCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_IControlWrapper_getWrapperData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_IControlWrapper_setWrapperData(IntPtr obj, IntPtr data);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_IControl_Default();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_IControl_Prepare(IntPtr obj, IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_AJoint_IControl_Speed(IntPtr obj, IntPtr __unnamed0, float current);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_IControl_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_Specs_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_Specs_getAxis(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_Specs_getIcontrol(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_Specs_setAxis(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_Specs_setIcontrol(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_getAxis(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_AJoint_getIcontrol(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_AJoint_setIcontrol(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Anchor_getBody(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_getC0(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_getC1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Anchor_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Anchor_getInfluence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_getLocal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Anchor_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setBody(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setC0(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setC1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setInfluence(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setLocal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Anchor_setNode(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_new2(IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_new3(IntPtr p);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_activate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_angularVelocity(IntPtr obj, [In] ref Vector3 rpos, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_angularVelocity2(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyAImpulse(IntPtr obj, IntPtr impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyDAImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyDCImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyDImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyImpulse(IntPtr obj, IntPtr impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyVAImpulse(IntPtr obj, [In] ref Vector3 impulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_applyVImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rpos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_getRigid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Body_getSoft(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Body_invMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_invWorldInertia(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_linearVelocity(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_setRigid(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_setSoft(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_velocity(IntPtr obj, [In] ref Vector3 rpos, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_xform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Body_delete(IntPtr obj);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_CJoint_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_CJoint_getLife(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_CJoint_getMaxlife(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_CJoint_getRpos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_setLife(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_setMaxlife(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_CJoint_setNormal(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getAdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getAv(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Cluster_getClusterIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Cluster_getCollide(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getCom(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Cluster_getContainsAnchor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getDimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getFramerefs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getFramexform(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getIdmass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getImass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getInvwi(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getLdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getLocii(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_getLv(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getMasses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getMatching(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getMaxSelfCollisionImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getNdamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Cluster_getNdimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Cluster_getNvimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Cluster_getSelfCollisionImpulseFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Cluster_getVimpulses(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setAdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setAv(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setClusterIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setCollide(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setCom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setContainsAnchor(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setFramexform(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setIdmass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setImass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setInvwi(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setLdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setLocii(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setLv(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setMatching(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setMaxSelfCollisionImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setNdamping(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setNdimpulses(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setNvimpulses(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Cluster_setSelfCollisionImpulseFactor(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.AeroModel btSoftBody_Config_getAeromodel(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Config_getCiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.Collisions btSoftBody_Config_getCollisions(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Config_getDiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Config_getDsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKAHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKCHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKDF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKDG(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKDP(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKKHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKLF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKMT(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKPR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSHR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSK_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSKHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSR_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSRHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSS_SPLT_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKSSHR_CL(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKVC(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getKVCF(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getMaxvolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Config_getPiterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Config_getPsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Config_getTimescale(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Config_getViterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Config_getVsequence(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setAeromodel(IntPtr obj, SoftBody.AeroModel value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setCiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setCollisions(IntPtr obj, SoftBody.Collisions value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setDiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKAHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKCHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKDF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKDG(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKDP(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKKHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKLF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKMT(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKPR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSHR(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSK_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSKHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSR_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSRHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSS_SPLT_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKSSHR_CL(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKVC(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setKVCF(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setMaxvolume(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setPiterations(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setTimescale(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Config_setViterations(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Element_getTag(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Element_setTag(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Face_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Face_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Face_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Face_getRa(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Face_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Face_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Face_setRa(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Feature_getMaterial(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Feature_setMaterial(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_ImplicitFnWrapper_new(IntPtr evalCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_ImplicitFn_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Impulse_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Impulse_getAsDrift(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Impulse_getAsVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_getDrift(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_getVelocity(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_operator_n(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_operator_m(IntPtr obj, float x);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_setAsDrift(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_setAsVelocity(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_setDrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_setVelocity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Impulse_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_Specs_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_Specs_getErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_Specs_getSplit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Specs_setCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Specs_setErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Specs_setSplit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Specs_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Joint_getBodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Joint_getDelete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_getDrift(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_getErp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_getMassmatrix(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Joint_getRefs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_getSdrift(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Joint_getSplit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Prepare(IntPtr obj, float dt, int iterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setCfm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setDelete(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setDrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setErp(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setMassmatrix(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setSdrift(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_setSplit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Solve(IntPtr obj, float dt, float sor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Joint_Terminate(IntPtr obj, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.JointType btSoftBody_Joint_Type(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Link_getBbending(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Link_getC0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Link_getC1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Link_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_getC3(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Link_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Link_getRl(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setBbending(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setC0(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setC1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setC3(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Link_setRl(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_LJoint_Specs_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_LJoint_Specs_getPosition(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_LJoint_Specs_setPosition(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_LJoint_getRpos(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.MaterialFlags btSoftBody_Material_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Material_getKAST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Material_getKLST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Material_getKVST(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Material_setFlags(IntPtr obj, SoftBody.MaterialFlags value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Material_setKAST(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Material_setKLST(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Material_setKVST(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Node_getArea(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Node_getBattach(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getF(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Node_getIm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Node_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getN(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getQ(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getV(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_getX(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setArea(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setBattach(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setF(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setIm(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setN(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setQ(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setV(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Node_setX(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Note_getCoords(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Note_getNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Note_getOffset(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_Note_getRank(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static extern string btSoftBody_Note_getText(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Note_setOffset(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Note_setRank(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Note_setText(IntPtr obj, IntPtr value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_getAqq(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Pose_getBframe(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSoftBody_Pose_getBvolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_getCom(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Pose_getPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_getRot(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_getScl(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Pose_getWgh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Pose_getVolume(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setAqq(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setBframe(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setBvolume(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setCom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setRot(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setScl(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Pose_setVolume(IntPtr obj, float value);


		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RayFromToCaster_new([In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, float mxt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RayFromToCaster_getFace(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RayFromToCaster_getMint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_getRayFrom(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_getRayNormalizedDirection(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_getRayTo(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_RayFromToCaster_getTests(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RayFromToCaster_rayFromToTriangle([In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, [In] ref Vector3 rayNormalizedDirection, [In] ref Vector3 a, [In] ref Vector3 b, [In] ref Vector3 c, float maxt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setFace(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setMint(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setRayFrom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setRayNormalizedDirection(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setRayTo(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RayFromToCaster_setTests(IntPtr obj, int value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getIsdt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getRadmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getSdt(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getUpdmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SolverState_getVelmrg(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setIsdt(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setRadmrg(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setSdt(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setUpdmrg(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SolverState_setVelmrg(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_sRayCast_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern SoftBody.FeatureType btSoftBody_sRayCast_getFeature(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_sRayCast_getFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftBody_sRayCast_getIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sRayCast_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Tetra_getC0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Tetra_getC1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Tetra_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Tetra_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_Tetra_getN(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_Tetra_getRv(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Tetra_setC1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Tetra_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Tetra_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_Tetra_setRv(IntPtr obj, float value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_SContact_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_SContact_getCfm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_SContact_getFace(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SContact_getFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_SContact_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_SContact_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_getWeights(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setFace(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_setWeights(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_SContact_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_sCti_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_sCti_getColObj(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_getNormal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_sCti_getOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_setColObj(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_setNormal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_setOffset(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_sCti_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RContact_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_getC0(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_getC1(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RContact_getC2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RContact_getC3(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBody_RContact_getC4(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RContact_getCti(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBody_RContact_getNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC0(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC3(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setC4(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_setNode(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBody_RContact_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyConcaveCollisionAlgorithm_clearCache(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyRigidBodyCollisionConfiguration_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyRigidBodyCollisionConfiguration_new2(IntPtr constructionInfo);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyWorldInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodyWorldInfo_getAir_density(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_getGravity(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodyWorldInfo_getMaxDisplacement(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftBodyWorldInfo_getSparsesdf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodyWorldInfo_getWater_density(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_getWater_normal(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSoftBodyWorldInfo_getWater_offset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setAir_density(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setBroadphase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setDispatcher(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setGravity(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setMaxDisplacement(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setWater_density(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setWater_normal(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_setWater_offset(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftBodyWorldInfo_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr col0, IntPtr col1Wrap, bool isSwapped);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration, IntPtr softBodySolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int btSoftRigidDynamicsWorld_getDrawFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidDynamicsWorld_getSoftBodyArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftRigidDynamicsWorld_getWorldInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSoftRigidDynamicsWorld_setDrawFlags(IntPtr obj, int f);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftSoftCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftSoftCollisionAlgorithm_new(IntPtr ci);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSoftSoftCollisionAlgorithm_new2(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereBoxCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereBoxCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btSphereBoxCollisionAlgorithm_getSphereDistance(IntPtr obj, IntPtr boxObjWrap, out Vector3 v3PointOnBox, out Vector3 normal, out float penetrationDepth, [In] ref Vector3 v3SphereCenter, float fRadius, float maxContactDistance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSphereBoxCollisionAlgorithm_getSpherePenetration(IntPtr obj, [In] ref Vector3 boxHalfExtent, [In] ref Vector3 sphereRelPos, out Vector3 closestPoint, out Vector3 normal);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereShape_new(float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btSphereShape_getRadius(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btSphereShape_setUnscaledRadius(IntPtr obj, float radius);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereSphereCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereSphereCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr col0Wrap, IntPtr col1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereSphereCollisionAlgorithm_new2(IntPtr ci);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereTriangleCollisionAlgorithm_CreateFunc_new();

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereTriangleCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool swapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btSphereTriangleCollisionAlgorithm_new2(IntPtr ci);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btStaticPlaneShape_new([In] ref Vector3 planeNormal, float planeConstant);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btStaticPlaneShape_getPlaneConstant(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStaticPlaneShape_getPlaneNormal(IntPtr obj, out Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_getClosestPointInB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btStorageResult_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_getNormalOnSurfaceB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_setClosestPointInB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_setDistance(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btStorageResult_setNormalOnSurfaceB(IntPtr obj, [In] ref Vector3 value);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTetrahedronShapeEx_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTetrahedronShapeEx_setVertices(IntPtr obj, [In] ref Vector3 v0, [In] ref Vector3 v1, [In] ref Vector3 v2, [In] ref Vector3 v3);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btThreads_btGetOpenMPTaskScheduler();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btThreads_btGetPPLTaskScheduler();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btThreads_btGetSequentialTaskScheduler();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btThreads_btGetTBBTaskScheduler();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btThreads_btSetTaskScheduler(IntPtr taskScheduler);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_getLocalAabbMax(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_getLocalAabbMin(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleMeshShape_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_localGetSupportingVertex(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_localGetSupportingVertexWithoutMargin(IntPtr obj, [In] ref Vector3 vec, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleMeshShape_recalcLocalAabb(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShape_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShape_new2([In] ref Vector3 p0, [In] ref Vector3 p1, [In] ref Vector3 p2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleShape_calcNormal(IntPtr obj, out Vector3 normal);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleShape_getPlaneEquation(IntPtr obj, int i, out Vector3 planeNormal, out Vector3 planeSupport);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShape_getVertexPtr(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShape_getVertices1(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShapeEx_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShapeEx_new2([In] ref Vector3 p0, [In] ref Vector3 p1, [In] ref Vector3 p2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btTriangleShapeEx_new3(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleShapeEx_applyTransform(IntPtr obj, [In] ref Matrix t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void btTriangleShapeEx_buildTriPlane(IntPtr obj, [Out] out Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool btTriangleShapeEx_overlap_test_conservative(IntPtr obj, IntPtr other);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr btUniformScalingShape_new(IntPtr convexChildShape, float uniformScalingFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float btUniformScalingShape_getUniformScalingFactor(IntPtr obj);

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

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_TRIANGLE_CONTACT_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_TRIANGLE_CONTACT_new2(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_copy_from(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern float GIM_TRIANGLE_CONTACT_getPenetration_depth(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int GIM_TRIANGLE_CONTACT_getPoint_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr GIM_TRIANGLE_CONTACT_getPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_getSeparating_normal(IntPtr obj, [Out] out Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_merge_points(IntPtr obj, [In] ref Vector4 plane, float margin, [In] ref Vector3 points, int point_count);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_setPenetration_depth(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_setPoint_count(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_setSeparating_normal(IntPtr obj, [In] ref Vector4 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void GIM_TRIANGLE_CONTACT_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Compute(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Compute2(IntPtr obj, bool fullCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Compute3(IntPtr obj, bool fullCH, bool exportDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_DenormalizeData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_GetAddExtraDistPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_GetAddFacesPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_GetAddNeighboursDistPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_GetCallBack(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_GetCH(IntPtr obj, int numCH, IntPtr points, IntPtr triangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetCompacityWeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetConcavity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetConnectDist(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNClusters(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNPointsCH(IntPtr obj, int numCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNTrianglesCH(IntPtr obj, int numCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern int HACD_HACD_GetNVerticesPerCH(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_GetPartition(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_GetPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetScaleFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern IntPtr HACD_HACD_GetTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern double HACD_HACD_GetVolumeWeight(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_NormalizeData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Save(IntPtr obj, IntPtr fileName, bool uniColor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool HACD_HACD_Save2(IntPtr obj, IntPtr fileName, bool uniColor, long numCluster);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetAddExtraDistPoints(IntPtr obj, bool addExtraDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetAddFacesPoints(IntPtr obj, bool addFacesPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetAddNeighboursDistPoints(IntPtr obj, bool addNeighboursDistPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetCallBack(IntPtr obj, IntPtr callBack);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetCompacityWeight(IntPtr obj, double alpha);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetConcavity(IntPtr obj, double concavity);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetConnectDist(IntPtr obj, double ccConnectDist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetNClusters(IntPtr obj, int nClusters);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetNPoints(IntPtr obj, int nPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetNTriangles(IntPtr obj, int nTriangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetNVerticesPerCH(IntPtr obj, int nVerticesPerCH);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetPoints(IntPtr obj, IntPtr points);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetScaleFactor(IntPtr obj, double scale);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetTriangles(IntPtr obj, IntPtr triangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_SetVolumeWeight(IntPtr obj, double beta);
		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void HACD_HACD_delete(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv)]
		public static extern void setGContactAddedCallback(IntPtr value);
	}
}
