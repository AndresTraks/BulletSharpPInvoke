using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GhostObject : CollisionObject
	{
		AlignedCollisionObjectArray _overlappingPairs;

		internal GhostObject(ConstructionInfo info)
			: base(info)
		{
		}

		public GhostObject()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btGhostObject_new();
			InitializeCollisionObject(native);
		}

		public void AddOverlappingObjectInternal(BroadphaseProxy otherProxy, BroadphaseProxy thisProxy = null)
		{
			btGhostObject_addOverlappingObjectInternal(Native, otherProxy.Native,
				(thisProxy != null) ? thisProxy.Native : IntPtr.Zero);
		}

		public void ConvexSweepTestRef(ConvexShape castShape, ref Matrix convexFromWorld,
			ref Matrix convexToWorld, ConvexResultCallback resultCallback, float allowedCcdPenetration = 0)
		{
			btGhostObject_convexSweepTest(Native, castShape.Native, ref convexFromWorld,
				ref convexToWorld, resultCallback.Native, allowedCcdPenetration);
		}

		public void ConvexSweepTest(ConvexShape castShape, Matrix convexFromWorld,
			Matrix convexToWorld, ConvexResultCallback resultCallback, float allowedCcdPenetration = 0)
		{
			btGhostObject_convexSweepTest(Native, castShape.Native, ref convexFromWorld,
				ref convexToWorld, resultCallback.Native, allowedCcdPenetration);
		}

		public CollisionObject GetOverlappingObject(int index)
		{
			return GetManaged(btGhostObject_getOverlappingObject(
				Native, index));
		}

		public void RayTestRef(ref Vector3 rayFromWorld, ref Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			btGhostObject_rayTest(Native, ref rayFromWorld, ref rayToWorld, resultCallback.Native);
		}

		public void RayTest(Vector3 rayFromWorld, Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			btGhostObject_rayTest(Native, ref rayFromWorld, ref rayToWorld, resultCallback.Native);
		}

		public void RemoveOverlappingObjectInternal(BroadphaseProxy otherProxy, Dispatcher dispatcher,
			BroadphaseProxy thisProxy = null)
		{
			btGhostObject_removeOverlappingObjectInternal(Native, otherProxy.Native,
				dispatcher.Native, (thisProxy != null) ? thisProxy.Native : IntPtr.Zero);
		}

		public static GhostObject Upcast(CollisionObject colObj)
		{
			return GetManaged(btGhostObject_upcast(colObj.Native)) as GhostObject;
		}

		public int NumOverlappingObjects => btGhostObject_getNumOverlappingObjects(Native);

		public AlignedCollisionObjectArray OverlappingPairs
		{
			get
			{
				if (_overlappingPairs == null)
				{
					_overlappingPairs = new AlignedCollisionObjectArray(btGhostObject_getOverlappingPairs(Native));
				}
				return _overlappingPairs;
			}
		}
	}

	public class PairCachingGhostObject : GhostObject
	{
		HashedOverlappingPairCache _overlappingPairCache;

		public PairCachingGhostObject()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btPairCachingGhostObject_new();
			InitializeCollisionObject(native);
		}

		public HashedOverlappingPairCache OverlappingPairCache
		{
			get
			{
				if (_overlappingPairCache == null)
				{
					_overlappingPairCache = new HashedOverlappingPairCache(btPairCachingGhostObject_getOverlappingPairCache(Native), this);
				}
				return _overlappingPairCache;
			}
		}
	}

	public sealed class GhostPairCallback : OverlappingPairCallback
	{
		public GhostPairCallback()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btGhostPairCallback_new();
			InitializeUserOwned(native);
		}

		public override BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return new BroadphasePair(btOverlappingPairCallback_addOverlappingPair(Native, proxy0.Native,
				proxy1.Native));
		}

		public override IntPtr RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1, Dispatcher dispatcher)
		{
			return btOverlappingPairCallback_removeOverlappingPair(Native, proxy0.Native,
				proxy1.Native, dispatcher.Native);
		}

		public override void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0,
			Dispatcher dispatcher)
		{
			btOverlappingPairCallback_removeOverlappingPairsContainingProxy(Native, proxy0.Native,
				dispatcher.Native);
		}
	}
}
