using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class DeformableMultiBodyDynamicsWorld : MultiBodyDynamicsWorld
	{
		public DeformableMultiBodyDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
			DeformableMultiBodyConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration,
			DeformableBodySolver deformableBodySolver)
		{
			IntPtr native = btDeformableMultiBodyDynamicsWorld_new(dispatcher.Native, pairCache.Native,
				constraintSolver.Native, collisionConfiguration.Native, deformableBodySolver.Native);
			InitializeUserOwned(native);
			InitializeMembers(dispatcher, pairCache, constraintSolver);
		}
	}
}
