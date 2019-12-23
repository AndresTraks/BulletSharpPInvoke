using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class DeformableMultiBodyConstraintSolver : MultiBodyConstraintSolver
	{
		public DeformableMultiBodyConstraintSolver()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btDeformableMultiBodyConstraintSolver_new();
			InitializeUserOwned(native);
		}

		public void SetDeformableSolver(DeformableBodySolver deformableSolver)
		{
			btDeformableMultiBodyConstraintSolver_setDeformableSolver(Native, deformableSolver.Native);
		}
	}
}
