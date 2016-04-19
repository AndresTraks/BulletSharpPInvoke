using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class PoolAllocator : IDisposable
	{
		internal IntPtr _native;
		bool _preventDelete;

		internal PoolAllocator(IntPtr native, bool preventDelete)
		{
			_native = native;
			_preventDelete = preventDelete;
		}

		public PoolAllocator(int elemSize, int maxElements)
		{
			_native = btPoolAllocator_new(elemSize, maxElements);
		}

		public IntPtr Allocate(int size)
		{
			return btPoolAllocator_allocate(_native, size);
		}

		public void FreeMemory(IntPtr ptr)
		{
			btPoolAllocator_freeMemory(_native, ptr);
		}

		public bool ValidPtr(IntPtr ptr)
		{
			return btPoolAllocator_validPtr(_native, ptr);
		}

		public int ElementSize
		{
			get { return btPoolAllocator_getElementSize(_native); }
		}

		public int FreeCount
		{
			get { return btPoolAllocator_getFreeCount(_native); }
		}

		public int MaxCount
		{
			get { return btPoolAllocator_getMaxCount(_native); }
		}

		public IntPtr PoolAddress
		{
			get { return btPoolAllocator_getPoolAddress(_native); }
		}

		public int UsedCount
		{
			get { return btPoolAllocator_getUsedCount(_native); }
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
				if (!_preventDelete)
				{
					btPoolAllocator_delete(_native);
				}
				_native = IntPtr.Zero;
			}
		}

		~PoolAllocator()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPoolAllocator_new(int elemSize, int maxElements);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPoolAllocator_allocate(IntPtr obj, int size);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoolAllocator_freeMemory(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPoolAllocator_getElementSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPoolAllocator_getFreeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPoolAllocator_getMaxCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPoolAllocator_getPoolAddress(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPoolAllocator_getUsedCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btPoolAllocator_validPtr(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPoolAllocator_delete(IntPtr obj);
	}
}
