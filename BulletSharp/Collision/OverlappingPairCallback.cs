using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class OverlappingPairCallback : BulletDisposableObject
	{
		internal OverlappingPairCallback(ConstructionInfo info)
		{
		}
		/*
		protected OverlappingPairCallback()
		{
			Native = btOverlappingPairCallbackWrapper_new();
		}
		*/
		public abstract BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1);
		public abstract IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1, Dispatcher dispatcher);
		public abstract void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0, Dispatcher dispatcher);

		protected override void Dispose(bool disposing)
		{
			if (Owner == null)
			{
				btOverlappingPairCallback_delete(Native);
			}
		}
	}
}
