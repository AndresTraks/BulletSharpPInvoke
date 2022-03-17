using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ConvexPlaneCollisionAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native, BulletObject owner)
				: base(ConstructionInfo.Null)
			{
				InitializeSubObject(native, owner);
			}

			public CreateFunc()
				: base(ConstructionInfo.Null)
			{
				IntPtr native = btConvexPlaneCollisionAlgorithm_CreateFunc_new();
				InitializeUserOwned(native);
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new ConvexPlaneCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native), __unnamed0.Dispatcher);
			}

			public int MinimumPointsPerturbationThreshold
			{
				get => btConvexPlaneCollisionAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(Native);
				set => btConvexPlaneCollisionAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(Native, value);
			}

			public int NumPerturbationIterations
			{
				get => btConvexPlaneCollisionAlgorithm_CreateFunc_getNumPerturbationIterations(Native);
				set => btConvexPlaneCollisionAlgorithm_CreateFunc_setNumPerturbationIterations(Native, value);
			}
		}

		internal ConvexPlaneCollisionAlgorithm(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public ConvexPlaneCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped,
			int numPerturbationIterations, int minimumPointsPerturbationThreshold)
		{
			IntPtr native = btConvexPlaneCollisionAlgorithm_new(mf.Native, ci.Native, body0Wrap.Native,
				body1Wrap.Native, isSwapped, numPerturbationIterations, minimumPointsPerturbationThreshold);
			InitializeUserOwned(native);
		}

		public void CollideSingleContact(Quaternion perturbeRot, CollisionObjectWrapper body0Wrap,
			CollisionObjectWrapper body1Wrap, DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			btConvexPlaneCollisionAlgorithm_collideSingleContact(Native, ref perturbeRot,
				body0Wrap.Native, body1Wrap.Native, dispatchInfo.Native, resultOut.Native);
		}
	}
}
