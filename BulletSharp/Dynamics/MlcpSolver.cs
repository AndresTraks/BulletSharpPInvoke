using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MlcpSolver : SequentialImpulseConstraintSolver
	{
		private MlcpSolverInterface _mlcpSolver;

		public MlcpSolver(MlcpSolverInterface solver)
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btMLCPSolver_new(solver.Native);
			InitializeUserOwned(native);
			_mlcpSolver = solver;
		}

		public void SetMLCPSolver(MlcpSolverInterface solver)
		{
			btMLCPSolver_setMLCPSolver(Native, solver.Native);
			_mlcpSolver = solver;
		}

		public int NumFallbacks
		{
			get => btMLCPSolver_getNumFallbacks(Native);
			set => btMLCPSolver_setNumFallbacks(Native, value);
		}
	}
}
