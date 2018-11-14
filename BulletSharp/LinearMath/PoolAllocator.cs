using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class PoolAllocator : BulletDisposableObject
	{
		internal PoolAllocator(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public PoolAllocator(int elemSize, int maxElements)
		{
			IntPtr native = btPoolAllocator_new(elemSize, maxElements);
			InitializeUserOwned(native);
		}

		public IntPtr Allocate(int size)
		{
			return btPoolAllocator_allocate(Native, size);
		}

		public void FreeMemory(IntPtr ptr)
		{
			btPoolAllocator_freeMemory(Native, ptr);
		}

		public bool ValidPtr(IntPtr ptr)
		{
			return btPoolAllocator_validPtr(Native, ptr);
		}

		public int ElementSize => btPoolAllocator_getElementSize(Native);

		public int FreeCount => btPoolAllocator_getFreeCount(Native);

		public int MaxCount => btPoolAllocator_getMaxCount(Native);

		public IntPtr PoolAddress => btPoolAllocator_getPoolAddress(Native);

		public int UsedCount => btPoolAllocator_getUsedCount(Native);

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btPoolAllocator_delete(Native);
			}
		}
	}
}
