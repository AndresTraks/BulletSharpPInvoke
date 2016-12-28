using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class ConvexTriangleMeshShape : PolyhedralConvexAabbCachingShape
	{
		private StridingMeshInterface _meshInterface;

		internal ConvexTriangleMeshShape(IntPtr native)
			: base(native)
		{
		}

		public ConvexTriangleMeshShape(StridingMeshInterface meshInterface, bool calcAabb = true)
			: base(btConvexTriangleMeshShape_new(meshInterface._native, calcAabb))
		{
			_meshInterface = meshInterface;
		}

		public void CalculatePrincipalAxisTransform(Matrix principal, out Vector3 inertia,
			out float volume)
		{
			btConvexTriangleMeshShape_calculatePrincipalAxisTransform(_native, ref principal,
				out inertia, out volume);
		}

		public StridingMeshInterface MeshInterface
		{
			get { return _meshInterface; }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexTriangleMeshShape_new(IntPtr meshInterface, bool calcAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexTriangleMeshShape_calculatePrincipalAxisTransform(IntPtr obj, ref Matrix principal, out Vector3 inertia, out float volume);
	}
}
