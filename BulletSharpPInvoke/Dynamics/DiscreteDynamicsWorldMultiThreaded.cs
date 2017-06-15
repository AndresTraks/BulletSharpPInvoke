using System;

namespace BulletSharp
{
	public class ConstraintSolverPoolMultiThreaded : ConstraintSolver
	{
		public ConstraintSolverPoolMultiThreaded(int numSolvers)
			: base(UnsafeNativeMethods.btConstraintSolverPoolMt_new(numSolvers), false)
		{
		}
	}

	public class DiscreteDynamicsWorldMultiThreaded : DiscreteDynamicsWorld
	{
		public DiscreteDynamicsWorldMultiThreaded(Dispatcher dispatcher, BroadphaseInterface pairCache,
			ConstraintSolverPoolMultiThreaded constraintSolver, CollisionConfiguration collisionConfiguration)
			: base(UnsafeNativeMethods.btDiscreteDynamicsWorldMt_new(dispatcher != null ? dispatcher._native : IntPtr.Zero,
				pairCache != null ? pairCache._native : IntPtr.Zero, constraintSolver != null ? constraintSolver._native : IntPtr.Zero,
				collisionConfiguration != null ? collisionConfiguration._native : IntPtr.Zero), dispatcher, pairCache)
		{
		}
	}
}
