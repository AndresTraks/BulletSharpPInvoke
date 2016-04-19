using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class SphereBoxCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btSphereBoxCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

            public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
            {
                return new SphereBoxCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
                    _native, __unnamed0._native, body0Wrap._native, body1Wrap._native));
            }

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSphereBoxCollisionAlgorithm_CreateFunc_new();
		}

		internal SphereBoxCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SphereBoxCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btSphereBoxCollisionAlgorithm_new(mf._native, ci._native, body0Wrap._native,
				body1Wrap._native, isSwapped))
		{
		}

        public bool GetSphereDistanceRef(CollisionObjectWrapper boxObjWrap, out Vector3 v3PointOnBox,
            out Vector3 normal, out float penetrationDepth, Vector3 v3SphereCenter,
            float fRadius, float maxContactDistance)
        {
            return btSphereBoxCollisionAlgorithm_getSphereDistance(_native, boxObjWrap._native,
                out v3PointOnBox, out normal, out penetrationDepth, ref v3SphereCenter,
                fRadius, maxContactDistance);
        }

		public bool GetSphereDistance(CollisionObjectWrapper boxObjWrap, out Vector3 v3PointOnBox,
			out Vector3 normal, out float penetrationDepth, Vector3 v3SphereCenter,
			float fRadius, float maxContactDistance)
		{
			return btSphereBoxCollisionAlgorithm_getSphereDistance(_native, boxObjWrap._native,
				out v3PointOnBox, out normal, out penetrationDepth, ref v3SphereCenter,
				fRadius, maxContactDistance);
		}

        public float GetSpherePenetrationRef(ref Vector3 boxHalfExtent, ref Vector3 sphereRelPos,
            out Vector3 closestPoint, out Vector3 normal)
        {
            return btSphereBoxCollisionAlgorithm_getSpherePenetration(_native, ref boxHalfExtent,
                ref sphereRelPos, out closestPoint, out normal);
        }

		public float GetSpherePenetration(Vector3 boxHalfExtent, Vector3 sphereRelPos,
			out Vector3 closestPoint, out Vector3 normal)
		{
			return btSphereBoxCollisionAlgorithm_getSpherePenetration(_native, ref boxHalfExtent,
				ref sphereRelPos, out closestPoint, out normal);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSphereBoxCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btSphereBoxCollisionAlgorithm_getSphereDistance(IntPtr obj, IntPtr boxObjWrap, out Vector3 v3PointOnBox, out Vector3 normal, out float penetrationDepth, [In] ref Vector3 v3SphereCenter, float fRadius, float maxContactDistance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSphereBoxCollisionAlgorithm_getSpherePenetration(IntPtr obj, [In] ref Vector3 boxHalfExtent, [In] ref Vector3 sphereRelPos, out Vector3 closestPoint, out Vector3 normal);
	}
}
