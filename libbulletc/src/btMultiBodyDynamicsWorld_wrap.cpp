#include <BulletCollision/CollisionDispatch/btCollisionConfiguration.h>
#include <BulletDynamics/Featherstone/btMultiBodyConstraint.h>
#include <BulletDynamics/Featherstone/btMultiBodyConstraintSolver.h>
#include <BulletDynamics/Featherstone/btMultiBodyDynamicsWorld.h>

#include "btMultiBodyDynamicsWorld_wrap.h"

btMultiBodyDynamicsWorld* btMultiBodyDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btMultiBodyConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration)
{
	return new btMultiBodyDynamicsWorld(dispatcher, pairCache, constraintSolver, collisionConfiguration);
}

void btMultiBodyDynamicsWorld_addMultiBody(btMultiBodyDynamicsWorld* obj, btMultiBody* body)
{
	obj->addMultiBody(body);
}

void btMultiBodyDynamicsWorld_addMultiBody2(btMultiBodyDynamicsWorld* obj, btMultiBody* body, short group)
{
	obj->addMultiBody(body, group);
}

void btMultiBodyDynamicsWorld_addMultiBody3(btMultiBodyDynamicsWorld* obj, btMultiBody* body, short group, short mask)
{
	obj->addMultiBody(body, group, mask);
}

void btMultiBodyDynamicsWorld_addMultiBodyConstraint(btMultiBodyDynamicsWorld* obj, btMultiBodyConstraint* constraint)
{
	obj->addMultiBodyConstraint(constraint);
}

void btMultiBodyDynamicsWorld_debugDrawMultiBodyConstraint(btMultiBodyDynamicsWorld* obj, btMultiBodyConstraint* constraint)
{
	obj->debugDrawMultiBodyConstraint(constraint);
}

void btMultiBodyDynamicsWorld_integrateTransforms(btMultiBodyDynamicsWorld* obj, btScalar timeStep)
{
	obj->integrateTransforms(timeStep);
}

void btMultiBodyDynamicsWorld_removeMultiBody(btMultiBodyDynamicsWorld* obj, btMultiBody* body)
{
	obj->removeMultiBody(body);
}

void btMultiBodyDynamicsWorld_removeMultiBodyConstraint(btMultiBodyDynamicsWorld* obj, btMultiBodyConstraint* constraint)
{
	obj->removeMultiBodyConstraint(constraint);
}
