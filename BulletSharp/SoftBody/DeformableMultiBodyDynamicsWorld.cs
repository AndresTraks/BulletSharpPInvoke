using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class DeformableMultiBodyDynamicsWorld : MultiBodyDynamicsWorld
	{
		private DeformableBodySolver _deformableBodySolver; // private ref passed to bodies during AddSoftBody

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
