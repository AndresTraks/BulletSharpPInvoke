using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum ConstraintSolverType
	{
		SequentialImpulse = 1,
		Mlcp = 2,
		Nncg = 4
	}

	public abstract class ConstraintSolver : BulletDisposableObject
	{
		protected internal ConstraintSolver()
		{
		}

		public void AllSolved(ContactSolverInfo __unnamed0, DebugDraw __unnamed1)
		{
			btConstraintSolver_allSolved(Native, __unnamed0.Native, __unnamed1 != null ? __unnamed1.Native : IntPtr.Zero);
		}

		public void PrepareSolve(int __unnamed0, int __unnamed1)
		{
			btConstraintSolver_prepareSolve(Native, __unnamed0, __unnamed1);
		}

		public void Reset()
		{
			btConstraintSolver_reset(Native);
		}
		/*
		public double SolveGroup(CollisionObject bodies, int numBodies, PersistentManifold manifold,
			int numManifolds, TypedConstraint constraints, int numConstraints, ContactSolverInfo info,
			IDebugDraw debugDrawer, Dispatcher dispatcher)
		{
			return btConstraintSolver_solveGroup(Native, bodies._native, numBodies,
				manifold._native, numManifolds, constraints._native, numConstraints,
				info._native, DebugDraw.GetUnmanaged(debugDrawer), dispatcher._native);
		}
		*/
		public ConstraintSolverType SolverType => btConstraintSolver_getSolverType(Native);

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btConstraintSolver_delete(Native);
			}
		}
	}
}
