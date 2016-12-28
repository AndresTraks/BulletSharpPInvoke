using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultimaterialTriangleMeshShape : BvhTriangleMeshShape
	{
		public MultimaterialTriangleMeshShape(StridingMeshInterface meshInterface,
			bool useQuantizedAabbCompression, bool buildBvh = true)
			: base(btMultimaterialTriangleMeshShape_new(meshInterface._native, useQuantizedAabbCompression,
				buildBvh))
		{
			_meshInterface = meshInterface;
		}

		public MultimaterialTriangleMeshShape(StridingMeshInterface meshInterface,
			bool useQuantizedAabbCompression, Vector3 bvhAabbMin, Vector3 bvhAabbMax,
			bool buildBvh = true)
			: base(btMultimaterialTriangleMeshShape_new2(meshInterface._native, useQuantizedAabbCompression,
				ref bvhAabbMin, ref bvhAabbMax, buildBvh))
		{
			_meshInterface = meshInterface;
		}
        /*
		public BulletMaterial GetMaterialProperties(int partID, int triIndex)
		{
			return btMultimaterialTriangleMeshShape_getMaterialProperties(_native,
				partID, triIndex);
		}
        */
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultimaterialTriangleMeshShape_new(IntPtr meshInterface, bool useQuantizedAabbCompression, bool buildBvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultimaterialTriangleMeshShape_new2(IntPtr meshInterface, bool useQuantizedAabbCompression, [In] ref Vector3 bvhAabbMin, [In] ref Vector3 bvhAabbMax, bool buildBvh);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern IntPtr btMultimaterialTriangleMeshShape_getMaterialProperties(IntPtr obj, int partID, int triIndex);
	}
}
