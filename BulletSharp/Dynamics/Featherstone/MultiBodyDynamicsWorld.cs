using System;
using System.Collections.Generic;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyDynamicsWorld : DiscreteDynamicsWorld
	{
		private List<MultiBody> _bodies;
		private List<MultiBodyConstraint> _constraints;

		public MultiBodyDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
			MultiBodyConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
		{
			IntPtr native = btMultiBodyDynamicsWorld_new(dispatcher.Native, pairCache.Native,
				constraintSolver.Native, collisionConfiguration.Native);
			InitializeUserOwned(native);
			InitializeMembers(dispatcher, pairCache, constraintSolver);

			_bodies = new List<MultiBody>();
			_constraints = new List<MultiBodyConstraint>();
		}

		public void AddMultiBody(MultiBody body, int group = (int)CollisionFilterGroups.DefaultFilter,
			int mask = (int)CollisionFilterGroups.AllFilter)
		{
			btMultiBodyDynamicsWorld_addMultiBody(Native, body.Native, group,
				mask);
			_bodies.Add(body);
		}

		public void AddMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			btMultiBodyDynamicsWorld_addMultiBodyConstraint(Native, constraint.Native);
			_constraints.Add(constraint);
		}

		public void BuildIslands()
		{
			btMultiBodyDynamicsWorld_buildIslands(Native);
		}

		public void ClearMultiBodyConstraintForces()
		{
			btMultiBodyDynamicsWorld_clearMultiBodyConstraintForces(Native);
		}

		public void ClearMultiBodyForces()
		{
			btMultiBodyDynamicsWorld_clearMultiBodyForces(Native);
		}

		public void DebugDrawMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			btMultiBodyDynamicsWorld_debugDrawMultiBodyConstraint(Native, constraint.Native);
		}

		public void ForwardKinematics()
		{
			btMultiBodyDynamicsWorld_forwardKinematics(Native);
		}

		public MultiBody GetMultiBody(int mbIndex)
		{
			return _bodies[mbIndex];
		}

		public MultiBodyConstraint GetMultiBodyConstraint(int constraintIndex)
		{
			return _constraints[constraintIndex];
		}

		public void IntegrateMultiBodyTransforms(float timeStep)
		{
			btMultiBodyDynamicsWorld_integrateMultiBodyTransforms(Native, timeStep);
		}

		public void IntegrateTransforms(float timeStep)
		{
			btMultiBodyDynamicsWorld_integrateTransforms(Native, timeStep);
		}

		public void PredictMultiBodyTransforms(float timeStep)
		{
			btMultiBodyDynamicsWorld_predictMultiBodyTransforms(Native, timeStep);
		}

		public void PredictUnconstraintMotion(float timeStep)
		{
			btMultiBodyDynamicsWorld_predictUnconstraintMotion(Native, timeStep);
		}

		public void RemoveMultiBody(MultiBody body)
		{
			btMultiBodyDynamicsWorld_removeMultiBody(Native, body.Native);
			_bodies.Remove(body);
		}

		public void RemoveMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			btMultiBodyDynamicsWorld_removeMultiBodyConstraint(Native, constraint.Native);
			_constraints.Remove(constraint);
		}

		public void SolveExternalForces(ContactSolverInfo solverInfo)
		{
			btMultiBodyDynamicsWorld_solveExternalForces(Native, solverInfo.Native);
		}

		public void SolveInternalConstraints(ContactSolverInfo solverInfo)
		{
			btMultiBodyDynamicsWorld_solveInternalConstraints(Native, solverInfo.Native);
		}

		public int NumMultibodies => _bodies.Count;

		public int NumMultiBodyConstraints => btMultiBodyDynamicsWorld_getNumMultiBodyConstraints(Native);
	}
}
