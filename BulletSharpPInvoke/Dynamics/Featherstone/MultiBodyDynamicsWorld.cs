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
			: base(btMultiBodyDynamicsWorld_new(dispatcher.Native, pairCache.Native,
				constraintSolver._native, collisionConfiguration.Native), dispatcher, pairCache)
		{
			_constraintSolver = constraintSolver;

			_bodies = new List<MultiBody>();
			_constraints = new List<MultiBodyConstraint>();
		}

		public void AddMultiBody(MultiBody body, int group = (int)CollisionFilterGroups.DefaultFilter,
			int mask = (int)CollisionFilterGroups.AllFilter)
		{
			btMultiBodyDynamicsWorld_addMultiBody(Native, body._native, group,
				mask);
			_bodies.Add(body);
		}

		public void AddMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			btMultiBodyDynamicsWorld_addMultiBodyConstraint(Native, constraint._native);
			_constraints.Add(constraint);
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
			btMultiBodyDynamicsWorld_debugDrawMultiBodyConstraint(Native, constraint._native);
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

		public void IntegrateTransforms(float timeStep)
		{
			btMultiBodyDynamicsWorld_integrateTransforms(Native, timeStep);
		}

		public void RemoveMultiBody(MultiBody body)
		{
			btMultiBodyDynamicsWorld_removeMultiBody(Native, body._native);
			_bodies.Remove(body);
		}

		public void RemoveMultiBodyConstraint(MultiBodyConstraint constraint)
		{
			btMultiBodyDynamicsWorld_removeMultiBodyConstraint(Native, constraint._native);
			_constraints.Remove(constraint);
		}

		public int NumMultibodies => _bodies.Count;

		public int NumMultiBodyConstraints => btMultiBodyDynamicsWorld_getNumMultiBodyConstraints(Native);
	}
}
