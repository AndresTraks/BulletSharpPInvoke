using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class ConvexPenetrationDepthSolver : BulletDisposableObject
	{
		protected internal ConvexPenetrationDepthSolver()
		{
		}

		public bool CalcPenDepth(VoronoiSimplexSolver simplexSolver, ConvexShape convexA,
			ConvexShape convexB, Matrix4x4 transA, Matrix4x4 transB, out Vector3 v, out Vector3 pa,
			out Vector3 pb, DebugDraw debugDraw)
		{
			return btConvexPenetrationDepthSolver_calcPenDepth(Native, simplexSolver.Native,
				convexA.Native, convexB.Native, ref transA, ref transB, out v, out pa,
				out pb, debugDraw != null ? debugDraw.Native : IntPtr.Zero);
		}

		protected override void Dispose(bool disposing)
		{
			btConvexPenetrationDepthSolver_delete(Native);
		}
	}
}
