using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GjkPairDetector : DiscreteCollisionDetectorInterface
	{
		internal GjkPairDetector(IntPtr native)
			: base(native)
		{
		}

		public GjkPairDetector(ConvexShape objectA, ConvexShape objectB, VoronoiSimplexSolver simplexSolver,
			ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(btGjkPairDetector_new(objectA.Native, objectB.Native, simplexSolver._native,
				(penetrationDepthSolver != null) ? penetrationDepthSolver._native : IntPtr.Zero))
		{
		}

		public GjkPairDetector(ConvexShape objectA, ConvexShape objectB, int shapeTypeA,
			int shapeTypeB, float marginA, float marginB, VoronoiSimplexSolver simplexSolver,
			ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(btGjkPairDetector_new2(objectA.Native, objectB.Native, shapeTypeA,
				shapeTypeB, marginA, marginB, simplexSolver._native, (penetrationDepthSolver != null) ? penetrationDepthSolver._native : IntPtr.Zero))
		{
		}

		public void GetClosestPointsNonVirtual(ClosestPointInput input, Result output,
			IDebugDraw debugDraw)
		{
			btGjkPairDetector_getClosestPointsNonVirtual(_native, input._native,
				output._native, DebugDraw.GetUnmanaged(debugDraw));
		}

		public void SetIgnoreMargin(bool ignoreMargin)
		{
			btGjkPairDetector_setIgnoreMargin(_native, ignoreMargin);
		}

		public void SetMinkowskiA(ConvexShape minkA)
		{
			btGjkPairDetector_setMinkowskiA(_native, minkA.Native);
		}

		public void SetMinkowskiB(ConvexShape minkB)
		{
			btGjkPairDetector_setMinkowskiB(_native, minkB.Native);
		}

		public void SetPenetrationDepthSolver(ConvexPenetrationDepthSolver penetrationDepthSolver)
		{
			btGjkPairDetector_setPenetrationDepthSolver(_native, penetrationDepthSolver._native);
		}

		public Vector3 CachedSeparatingAxis
		{
			get
			{
				Vector3 value;
				btGjkPairDetector_getCachedSeparatingAxis(_native, out value);
				return value;
			}
			set => btGjkPairDetector_setCachedSeparatingAxis(_native, ref value);
		}

		public float CachedSeparatingDistance => btGjkPairDetector_getCachedSeparatingDistance(_native);

		public int CatchDegeneracies
		{
			get => btGjkPairDetector_getCatchDegeneracies(_native);
			set => btGjkPairDetector_setCatchDegeneracies(_native, value);
		}

		public int CurIter
		{
			get => btGjkPairDetector_getCurIter(_native);
			set => btGjkPairDetector_setCurIter(_native, value);
		}

		public int DegenerateSimplex
		{
			get => btGjkPairDetector_getDegenerateSimplex(_native);
			set => btGjkPairDetector_setDegenerateSimplex(_native, value);
		}

		public int FixContactNormalDirection
		{
			get => btGjkPairDetector_getFixContactNormalDirection(_native);
			set => btGjkPairDetector_setFixContactNormalDirection(_native, value);
		}

		public int LastUsedMethod
		{
			get => btGjkPairDetector_getLastUsedMethod(_native);
			set => btGjkPairDetector_setLastUsedMethod(_native, value);
		}
	}
}
