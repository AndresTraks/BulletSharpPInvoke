using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class ConvexPenetrationDepthSolver : IDisposable
	{
		internal IntPtr _native;

		internal ConvexPenetrationDepthSolver(IntPtr native)
		{
			_native = native;
		}

		public bool CalcPenDepth(VoronoiSimplexSolver simplexSolver, ConvexShape convexA,
			ConvexShape convexB, Matrix transA, Matrix transB, out Vector3 v, out Vector3 pa,
			out Vector3 pb, IDebugDraw debugDraw)
		{
			return btConvexPenetrationDepthSolver_calcPenDepth(_native, simplexSolver._native,
				convexA.Native, convexB.Native, ref transA, ref transB, out v, out pa,
				out pb, DebugDraw.GetUnmanaged(debugDraw));
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btConvexPenetrationDepthSolver_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ConvexPenetrationDepthSolver()
		{
			Dispose(false);
		}
	}
}
