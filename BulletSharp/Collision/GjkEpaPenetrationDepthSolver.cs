using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GjkEpaPenetrationDepthSolver : ConvexPenetrationDepthSolver
	{
		public GjkEpaPenetrationDepthSolver()
		{
			IntPtr native = btGjkEpaPenetrationDepthSolver_new();
			InitializeUserOwned(native);
		}
	}
}
