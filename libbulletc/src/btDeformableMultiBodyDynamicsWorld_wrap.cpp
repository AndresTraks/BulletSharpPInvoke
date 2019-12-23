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

void btDeformableMultiBodyDynamicsWorld_addForce(btDeformableMultiBodyDynamicsWorld* obj, btSoftBody* psb,
	btDeformableLagrangianForce* force)
{
	obj->addForce(psb, force);
}

void btDeformableMultiBodyDynamicsWorld_addSoftBody(btDeformableMultiBodyDynamicsWorld* obj, btSoftBody* body,
	int collisionFilterGroup, int collisionFilterMask)
{
	obj->addSoftBody(body, collisionFilterGroup, collisionFilterMask);
}

btSoftBodyWorldInfo* btDeformableMultiBodyDynamicsWorld_getWorldInfo(btDeformableMultiBodyDynamicsWorld* obj)
{
	return &obj->getWorldInfo();
}
