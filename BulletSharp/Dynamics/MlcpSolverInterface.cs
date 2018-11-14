using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class MlcpSolverInterface : BulletDisposableObject
	{
		protected internal MlcpSolverInterface()
		{
		}
		/*
		public bool SolveMLCP(btMatrixX<double> a, btVectorX<double> b, btVectorX<double> x,
			btVectorX<double> lo, btVectorX<double> hi, AlignedObjectArray<int> limitDependency,
			int numIterations, bool useSparsity = true)
		{
			return btMLCPSolverInterface_solveMLCP(Native, a.Native, b.Native,
				x.Native, lo.Native, hi.Native, limitDependency.Native, numIterations,
				useSparsity);
		}
		*/

		protected override void Dispose(bool disposing)
		{
			btMLCPSolverInterface_delete(Native);
		}
	}
}
