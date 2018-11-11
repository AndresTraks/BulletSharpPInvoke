using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class CollisionAlgorithmCreateFunc : BulletDisposableObject
	{
		internal CollisionAlgorithmCreateFunc(ConstructionInfo info)
		{
		}

		public CollisionAlgorithmCreateFunc()
		{
			IntPtr native = btCollisionAlgorithmCreateFunc_new();
			InitializeUserOwned(native);
		}

		public virtual CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
		{
			return null;
		}

		public bool Swapped
		{
			get => btCollisionAlgorithmCreateFunc_getSwapped(Native);
			set => btCollisionAlgorithmCreateFunc_setSwapped(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btCollisionAlgorithmCreateFunc_delete(Native);
			}
		}
	}
}
