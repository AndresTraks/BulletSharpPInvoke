using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SequentialImpulseConstraintSolverMultiThreaded : SequentialImpulseConstraintSolver
	{
		public SequentialImpulseConstraintSolverMultiThreaded()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btSequentialImpulseConstraintSolverMt_new();
			InitializeUserOwned(native);
		}
	}
}
