using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class EmptyAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btEmptyAlgorithm_CreateFunc_new(), false)
			{
			}

            public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
            {
                return new EmptyAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
                    _native, __unnamed0._native, body0Wrap._native, body1Wrap._native));
            }

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btEmptyAlgorithm_CreateFunc_new();
		}

		internal EmptyAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public EmptyAlgorithm(CollisionAlgorithmConstructionInfo ci)
			: base(btEmptyAlgorithm_new(ci._native))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btEmptyAlgorithm_new(IntPtr ci);
	}
}
