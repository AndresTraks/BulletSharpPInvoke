using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ContinuousConvexCollision : ConvexCast
	{
		public ContinuousConvexCollision(ConvexShape shapeA, ConvexShape shapeB,
			VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver penetrationDepthSolver)
		{
			IntPtr native = btContinuousConvexCollision_new(shapeA.Native, shapeB.Native,
				simplexSolver.Native, penetrationDepthSolver.Native);
			InitializeUserOwned(native);
		}

		public ContinuousConvexCollision(ConvexShape shapeA, StaticPlaneShape plane)
		{
			IntPtr native = btContinuousConvexCollision_new2(shapeA.Native, plane.Native);
			InitializeUserOwned(native);
		}
	}
}
