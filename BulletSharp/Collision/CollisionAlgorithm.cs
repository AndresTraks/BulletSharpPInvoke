using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class CollisionAlgorithmConstructionInfo : BulletDisposableObject
	{
		private Dispatcher _dispatcher1;
		private PersistentManifold _manifold;

		public CollisionAlgorithmConstructionInfo()
		{
			IntPtr native = btCollisionAlgorithmConstructionInfo_new();
			InitializeUserOwned(native);
		}

		public CollisionAlgorithmConstructionInfo(Dispatcher dispatcher, int temp)
		{
			Native = btCollisionAlgorithmConstructionInfo_new2((dispatcher != null) ? dispatcher.Native : IntPtr.Zero,
				temp);
			_dispatcher1 = dispatcher;
		}

		public Dispatcher Dispatcher
		{
			get => _dispatcher1;
			set
			{
				btCollisionAlgorithmConstructionInfo_setDispatcher1(Native, (value != null) ? value.Native : IntPtr.Zero);
				_dispatcher1 = value;
			}
		}

		public PersistentManifold Manifold
		{
			get => _manifold;
			set
			{
				btCollisionAlgorithmConstructionInfo_setManifold(Native, (value != null) ? value.Native : IntPtr.Zero);
				_manifold = value;
			}
		}

		protected override void Dispose(bool disposing)
		{
			btCollisionAlgorithmConstructionInfo_delete(Native);
		}
	}

	public class CollisionAlgorithm : BulletDisposableObject
	{
		protected internal CollisionAlgorithm()
		{
		}

		internal CollisionAlgorithm(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public float CalculateTimeOfImpact(CollisionObject body0, CollisionObject body1,
			DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			return btCollisionAlgorithm_calculateTimeOfImpact(Native, body0.Native,
				body1.Native, dispatchInfo.Native, resultOut.Native);
		}

		public void GetAllContactManifolds(AlignedManifoldArray manifoldArray)
		{
			btCollisionAlgorithm_getAllContactManifolds(Native, manifoldArray.Native);
		}

		public void ProcessCollision(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			btCollisionAlgorithm_processCollision(Native, body0Wrap.Native, body1Wrap.Native,
				dispatchInfo.Native, resultOut.Native);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btCollisionAlgorithm_delete(Native);
			}
		}
	}
}
