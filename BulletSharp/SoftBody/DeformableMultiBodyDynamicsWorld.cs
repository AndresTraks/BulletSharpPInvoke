using System;
using System.Collections.Generic;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class DeformableMultiBodyDynamicsWorld : MultiBodyDynamicsWorld
	{
		private DeformableBodySolver _deformableBodySolver; // private ref passed to bodies during AddSoftBody
		private HashSet<DeformableLagrangianForce> _forces = new HashSet<DeformableLagrangianForce>();

		public DeformableMultiBodyDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
			DeformableMultiBodyConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration,
			DeformableBodySolver deformableBodySolver)
		{
			_deformableBodySolver = deformableBodySolver;

			IntPtr native = btDeformableMultiBodyDynamicsWorld_new(dispatcher.Native, pairCache.Native,
				constraintSolver.Native, collisionConfiguration.Native, deformableBodySolver.Native);
			InitializeUserOwned(native);
			InitializeMembers(dispatcher, pairCache, constraintSolver);

			WorldInfo = new SoftBodyWorldInfo(btDeformableMultiBodyDynamicsWorld_getWorldInfo(Native), this)
			{
				Dispatcher = dispatcher,
				Broadphase = pairCache
			};
		}

		public SoftBodyWorldInfo WorldInfo { get; }

		public void AddForce(SoftBody psb, DeformableLagrangianForce force)
		{
			btDeformableMultiBodyDynamicsWorld_addForce(Native, psb.Native, force.Native);
			_forces.Add(force);
		}

		public void AddSoftBody(SoftBody body)
		{
			AddSoftBody(body, CollisionFilterGroups.DefaultFilter, CollisionFilterGroups.AllFilter);
		}

		public void AddSoftBody(SoftBody body, CollisionFilterGroups collisionFilterGroup, CollisionFilterGroups collisionFilterMask)
		{
			AddSoftBody(body, (int)collisionFilterGroup, (int)collisionFilterMask);
		}

		public void AddSoftBody(SoftBody body, int collisionFilterGroup, int collisionFilterMask)
		{
			body.SoftBodySolver = _deformableBodySolver;
			CollisionObjectArray.Add(body, collisionFilterGroup, collisionFilterMask);
		}
	}
}
