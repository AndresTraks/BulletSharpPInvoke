using System;

namespace BulletSharp
{
	public class GImpactCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(UnsafeNativeMethods.btGImpactCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			{
				return new GImpactCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
					_native, __unnamed0._native, body0Wrap._native, body1Wrap._native));
			}
		}

		internal GImpactCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public GImpactCollisionAlgorithm(CollisionAlgorithmConstructionInfo constructionInfo, CollisionObjectWrapper body0Wrap,
			CollisionObjectWrapper body1Wrap)
			: base(UnsafeNativeMethods.btGImpactCollisionAlgorithm_new(constructionInfo._native, body0Wrap._native,
				body1Wrap._native))
		{
		}

		public void GImpactVsCompoundShape(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			GImpactShapeInterface shape0, CompoundShape shape1, bool swapped)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_gimpact_vs_compoundshape(_native, body0Wrap._native,
				body1Wrap._native, shape0._native, shape1._native, swapped);
		}

		public void GImpactVsConcave(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			GImpactShapeInterface shape0, ConcaveShape shape1, bool swapped)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_gimpact_vs_concave(_native, body0Wrap._native,
				body1Wrap._native, shape0._native, shape1._native, swapped);
		}

		public void GImpactVsGImpact(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			GImpactShapeInterface shape0, GImpactShapeInterface shape1)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_gimpact_vs_gimpact(_native, body0Wrap._native,
				body1Wrap._native, shape0._native, shape1._native);
		}

		public void GImpactVsShape(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			GImpactShapeInterface shape0, CollisionShape shape1, bool swapped)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_gimpact_vs_shape(_native, body0Wrap._native,
				body1Wrap._native, shape0._native, shape1._native, swapped);
		}

		public ManifoldResult InternalGetResultOut()
		{
			return new ManifoldResult(UnsafeNativeMethods.btGImpactCollisionAlgorithm_internalGetResultOut(_native));
		}

		public static void RegisterAlgorithm(CollisionDispatcher dispatcher)
		{
			UnsafeNativeMethods.btGImpactCollisionAlgorithm_registerAlgorithm(dispatcher._native);
		}

		public int Face0
		{
			get => UnsafeNativeMethods.btGImpactCollisionAlgorithm_getFace0(_native);
			set => UnsafeNativeMethods.btGImpactCollisionAlgorithm_setFace0(_native, value);
		}

		public int Face1
		{
			get => UnsafeNativeMethods.btGImpactCollisionAlgorithm_getFace1(_native);
			set => UnsafeNativeMethods.btGImpactCollisionAlgorithm_setFace1(_native, value);
		}

		public int Part0
		{
			get => UnsafeNativeMethods.btGImpactCollisionAlgorithm_getPart0(_native);
			set => UnsafeNativeMethods.btGImpactCollisionAlgorithm_setPart0(_native, value);
		}

		public int Part1
		{
			get => UnsafeNativeMethods.btGImpactCollisionAlgorithm_getPart1(_native);
			set => UnsafeNativeMethods.btGImpactCollisionAlgorithm_setPart1(_native, value);
		}
	}
}
