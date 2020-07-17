using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class DeformableBodySolver : SoftBodySolver
	{
		public DeformableBodySolver()
		{
			IntPtr native = btDeformableBodySolver_new();
			InitializeUserOwned(native);
		}
	}
}
