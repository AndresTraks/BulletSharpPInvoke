using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ManifoldResult : DiscreteCollisionDetectorInterface.Result
	{
		internal ManifoldResult(IntPtr native)
			: base(native)
		{
		}

		public ManifoldResult()
			: base(btManifoldResult_new())
		{
		}

		public ManifoldResult(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btManifoldResult_new2(body0Wrap.Native, body1Wrap.Native))
		{
		}

		public static float CalculateCombinedContactDamping(CollisionObject body0,
			CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedContactDamping(body0.Native,
				body1.Native);
		}

		public static float CalculateCombinedContactStiffness(CollisionObject body0,
			CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedContactStiffness(body0.Native,
				body1.Native);
		}

		public static float CalculateCombinedFriction(CollisionObject body0, CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedFriction(body0.Native, body1.Native);
		}

		public static float CalculateCombinedRestitution(CollisionObject body0, CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedRestitution(body0.Native, body1.Native);
		}

		public static float CalculateCombinedRollingFriction(CollisionObject body0,
			CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedRollingFriction(body0.Native,
				body1.Native);
		}

		public void RefreshContactPoints()
		{
			btManifoldResult_refreshContactPoints(_native);
		}

		public CollisionObject Body0Internal => CollisionObject.GetManaged(btManifoldResult_getBody0Internal(_native));

		public CollisionObjectWrapper Body0Wrap
		{
			get => new CollisionObjectWrapper(btManifoldResult_getBody0Wrap(_native));
			set => btManifoldResult_setBody0Wrap(_native, value.Native);
		}

		public CollisionObject Body1Internal => CollisionObject.GetManaged(btManifoldResult_getBody1Internal(_native));

		public CollisionObjectWrapper Body1Wrap
		{
			get => new CollisionObjectWrapper(btManifoldResult_getBody1Wrap(_native));
			set => btManifoldResult_setBody1Wrap(_native, value.Native);
		}

		public float ClosestPointDistanceThreshold
		{
			get => btManifoldResult_getClosestPointDistanceThreshold(_native);
			set => btManifoldResult_setClosestPointDistanceThreshold(_native, value);
		}

		public PersistentManifold PersistentManifold
		{
			get => new PersistentManifold(btManifoldResult_getPersistentManifold(_native), true);
			set => btManifoldResult_setPersistentManifold(_native, value._native);
		}
	}
}
