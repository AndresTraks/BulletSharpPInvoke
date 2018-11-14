using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class DantzigSolver : MlcpSolverInterface
	{
		public DantzigSolver()
		{
			IntPtr native = btDantzigSolver_new();
			InitializeUserOwned(native);
		}
	}
}
