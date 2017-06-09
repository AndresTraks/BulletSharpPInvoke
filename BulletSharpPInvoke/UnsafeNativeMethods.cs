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
	}
}
