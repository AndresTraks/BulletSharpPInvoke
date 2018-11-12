using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MinkowskiPenetrationDepthSolver : ConvexPenetrationDepthSolver
	{
		public MinkowskiPenetrationDepthSolver()
		{
			IntPtr native = btMinkowskiPenetrationDepthSolver_new();
			InitializeUserOwned(native);
		}
	}
}
