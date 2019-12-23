#include <BulletCollision/CollisionDispatch/btCollisionConfiguration.h>
#include <BulletSoftBody/btDeformableMultiBodyDynamicsWorld.h>

#include "btDeformableMultiBodyDynamicsWorld_wrap.h"

btDeformableMultiBodyDynamicsWorld* btDeformableMultiBodyDynamicsWorld_new(btDispatcher* dispatcher,
	btBroadphaseInterface* pairCache, btDeformableMultiBodyConstraintSolver* constraintSolver,
	btCollisionConfiguration* collisionCOnfiguration, btDeformableBodySolver* deformableBodySolver)
{
	return new btDeformableMultiBodyDynamicsWorld(dispatcher, pairCache, constraintSolver,
		collisionCOnfiguration, deformableBodySolver);
}
