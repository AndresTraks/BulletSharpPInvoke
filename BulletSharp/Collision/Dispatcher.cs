using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum DispatchFunc
	{
		Discrete = 1,
		Continuous
	}

	public enum DispatcherQueryType
	{
		ContactPointAlgorithms = 1,
		ClosestPointAlgorithms = 2
	}

	public class DispatcherInfo : BulletObject
	{
		internal DispatcherInfo(IntPtr native)
		{
			Initialize(native);
		}

		public double AllowedCcdPenetration
		{
			get => btDispatcherInfo_getAllowedCcdPenetration(Native);
			set => btDispatcherInfo_setAllowedCcdPenetration(Native, value);
		}

		public double ConvexConservativeDistanceThreshold
		{
			get => btDispatcherInfo_getConvexConservativeDistanceThreshold(Native);
			set => btDispatcherInfo_setConvexConservativeDistanceThreshold(Native, value);
		}

		public DebugDraw DebugDraw
		{
			get => DebugDraw.GetManaged(btDispatcherInfo_getDebugDraw(Native));
			set => btDispatcherInfo_setDebugDraw(Native, value != null ? value._native : IntPtr.Zero);
		}

		public bool DeterministicOverlappingPairs
		{
			get => btDispatcherInfo_getDeterministicOverlappingPairs(Native);
			set => btDispatcherInfo_setDeterministicOverlappingPairs(Native, value);
		}


		public DispatchFunc DispatchFunc
		{
			get => btDispatcherInfo_getDispatchFunc(Native);
			set => btDispatcherInfo_setDispatchFunc(Native, value);
		}

		public bool EnableSatConvex
		{
			get => btDispatcherInfo_getEnableSatConvex(Native);
			set => btDispatcherInfo_setEnableSatConvex(Native, value);
		}

		public bool EnableSpu
		{
			get => btDispatcherInfo_getEnableSPU(Native);
			set => btDispatcherInfo_setEnableSPU(Native, value);
		}

		public int StepCount
		{
			get => btDispatcherInfo_getStepCount(Native);
			set => btDispatcherInfo_setStepCount(Native, value);
		}

		public double TimeOfImpact
		{
			get => btDispatcherInfo_getTimeOfImpact(Native);
			set => btDispatcherInfo_setTimeOfImpact(Native, value);
		}

		public double TimeStep
		{
			get => btDispatcherInfo_getTimeStep(Native);
			set => btDispatcherInfo_setTimeStep(Native, value);
		}

		public bool UseContinuous
		{
			get => btDispatcherInfo_getUseContinuous(Native);
			set => btDispatcherInfo_setUseContinuous(Native, value);
		}

		public bool UseConvexConservativeDistanceUtil
		{
			get => btDispatcherInfo_getUseConvexConservativeDistanceUtil(Native);
			set => btDispatcherInfo_setUseConvexConservativeDistanceUtil(Native, value);
		}

		public bool UseEpa
		{
			get => btDispatcherInfo_getUseEpa(Native);
			set => btDispatcherInfo_setUseEpa(Native, value);
		}
	}

	public abstract class Dispatcher : BulletDisposableObject
	{
		protected internal Dispatcher()
		{
		}

		public IntPtr AllocateCollisionAlgorithm(int size)
		{
			return btDispatcher_allocateCollisionAlgorithm(Native, size);
		}

		public void ClearManifold(PersistentManifold manifold)
		{
			btDispatcher_clearManifold(Native, manifold.Native);
		}

		public void DispatchAllCollisionPairs(OverlappingPairCache pairCache, DispatcherInfo dispatchInfo,
			Dispatcher dispatcher)
		{
			btDispatcher_dispatchAllCollisionPairs(Native, pairCache.Native, dispatchInfo.Native,
				dispatcher.Native);
		}

		public CollisionAlgorithm FindAlgorithm(CollisionObjectWrapper body0Wrap,
			CollisionObjectWrapper body1Wrap, PersistentManifold sharedManifold,
			DispatcherQueryType queryType)
		{
			return new CollisionAlgorithm(btDispatcher_findAlgorithm(Native, body0Wrap.Native, body1Wrap.Native,
				sharedManifold.Native, queryType), this);
		}

		public void FreeCollisionAlgorithm(IntPtr ptr)
		{
			btDispatcher_freeCollisionAlgorithm(Native, ptr);
		}

		public PersistentManifold GetManifoldByIndexInternal(int index)
		{
			return new PersistentManifold(btDispatcher_getManifoldByIndexInternal(Native, index), this);
		}

		public PersistentManifold GetNewManifold(CollisionObject b0, CollisionObject b1)
		{
			return new PersistentManifold(btDispatcher_getNewManifold(Native, b0.Native, b1.Native), this);
		}

		public bool NeedsCollision(CollisionObject body0, CollisionObject body1)
		{
			return btDispatcher_needsCollision(Native, body0.Native, body1.Native);
		}

		public bool NeedsResponse(CollisionObject body0, CollisionObject body1)
		{
			return btDispatcher_needsResponse(Native, body0.Native, body1.Native);
		}

		public void ReleaseManifold(PersistentManifold manifold)
		{
			btDispatcher_releaseManifold(Native, manifold.Native);
		}
		/*
		public PersistentManifold InternalManifoldPointer
		{
			get { return btDispatcher_getInternalManifoldPointer(Native); }
		}

		public PoolAllocator InternalManifoldPool
		{
			get { return btDispatcher_getInternalManifoldPool(Native); }
		}
		*/
		public int NumManifolds => btDispatcher_getNumManifolds(Native);

		protected override void Dispose(bool disposing)
		{
			btDispatcher_delete(Native);
		}
	}
}
