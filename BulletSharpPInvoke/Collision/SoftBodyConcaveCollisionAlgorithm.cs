using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class TriIndex : IDisposable
	{
		internal IntPtr _native;

		internal TriIndex(IntPtr native)
		{
			_native = native;
		}

		public TriIndex(int partId, int triangleIndex, CollisionShape shape)
		{
			_native = btTriIndex_new(partId, triangleIndex, shape._native);
		}

		public CollisionShape ChildShape
		{
			get { return CollisionShape.GetManaged(btTriIndex_getChildShape(_native)); }
			set { btTriIndex_setChildShape(_native, value._native); }
		}

		public int PartId
		{
			get { return btTriIndex_getPartId(_native); }
		}

		public int PartIdTriangleIndex
		{
			get { return btTriIndex_getPartIdTriangleIndex(_native); }
			set { btTriIndex_setPartIdTriangleIndex(_native, value); }
		}

		public int TriangleIndex
		{
			get { return btTriIndex_getTriangleIndex(_native); }
		}

		public int Uid
		{
			get { return btTriIndex_getUid(_native); }
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
				btTriIndex_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~TriIndex()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriIndex_new(int partId, int triangleIndex, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriIndex_getChildShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriIndex_getPartId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriIndex_getPartIdTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriIndex_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTriIndex_getUid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriIndex_setChildShape(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriIndex_setPartIdTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriIndex_delete(IntPtr obj);
	}
/*
	public class SoftBodyTriangleCallback : TriangleCallback
	{
		internal SoftBodyTriangleCallback(IntPtr native)
			: base(native)
		{
		}

		public SoftBodyTriangleCallback(Dispatcher dispatcher, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btSoftBodyTriangleCallback_new(dispatcher._native, body0Wrap._native, body1Wrap._native, isSwapped))
		{
		}

		public void ClearCache()
		{
			btSoftBodyTriangleCallback_clearCache(_native);
		}

		public void SetTimeStepAndCounters(float collisionMarginTriangle, CollisionObjectWrapper triObjWrap, DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			btSoftBodyTriangleCallback_setTimeStepAndCounters(_native, collisionMarginTriangle, triObjWrap._native, dispatchInfo._native, resultOut._native);
		}

		public Vector3 AabbMax
		{
			get
			{
				Vector3 value;
				btSoftBodyTriangleCallback_getAabbMax(_native, out value);
				return value;
			}
		}

		public Vector3 AabbMin
		{
			get
			{
				Vector3 value;
				btSoftBodyTriangleCallback_getAabbMin(_native, out value);
				return value;
			}
		}

		public int TriangleCount
		{
			get { return btSoftBodyTriangleCallback_getTriangleCount(_native); }
			set { btSoftBodyTriangleCallback_setTriangleCount(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyTriangleCallback_new(IntPtr dispatcher, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyTriangleCallback_clearCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyTriangleCallback_getAabbMax(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyTriangleCallback_getAabbMin(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftBodyTriangleCallback_getTriangleCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyTriangleCallback_setTimeStepAndCounters(IntPtr obj, float collisionMarginTriangle, IntPtr triObjWrap, IntPtr dispatchInfo, IntPtr resultOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyTriangleCallback_setTriangleCount(IntPtr obj, int value);
	}
*/
	public class SoftBodyConcaveCollisionAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new();
		}

		public class SwappedCreateFunc : CollisionAlgorithmCreateFunc
		{
			internal SwappedCreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public SwappedCreateFunc()
				: base(btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new(), false)
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new();
		}

		internal SoftBodyConcaveCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SoftBodyConcaveCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btSoftBodyConcaveCollisionAlgorithm_new(ci._native, body0Wrap._native, body1Wrap._native, isSwapped))
		{
		}

		public void ClearCache()
		{
			btSoftBodyConcaveCollisionAlgorithm_clearCache(_native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyConcaveCollisionAlgorithm_new(IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, bool isSwapped);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyConcaveCollisionAlgorithm_clearCache(IntPtr obj);
	}
}
