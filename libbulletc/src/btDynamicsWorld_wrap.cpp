#include <BulletDynamics/ConstraintSolver/btConstraintSolver.h>
#include <BulletDynamics/ConstraintSolver/btTypedConstraint.h>
#include <BulletDynamics/Dynamics/btActionInterface.h>
#include <BulletDynamics/Dynamics/btDynamicsWorld.h>

#include "conversion.h"
#include "btDynamicsWorld_wrap.h"

void btDynamicsWorld_addAction(btDynamicsWorld* obj, btActionInterface* action)
{
	obj->addAction(action);
}

void btDynamicsWorld_addConstraint(btDynamicsWorld* obj, btTypedConstraint* constraint)
{
	obj->addConstraint(constraint);
}

void btDynamicsWorld_addConstraint2(btDynamicsWorld* obj, btTypedConstraint* constraint, bool disableCollisionsBetweenLinkedBodies)
{
	obj->addConstraint(constraint, disableCollisionsBetweenLinkedBodies);
}

void btDynamicsWorld_addRigidBody(btDynamicsWorld* obj, btRigidBody* body)
{
	obj->addRigidBody(body);
}

void btDynamicsWorld_addRigidBody2(btDynamicsWorld* obj, btRigidBody* body, short group, short mask)
{
	obj->addRigidBody(body, group, mask);
}

void btDynamicsWorld_clearForces(btDynamicsWorld* obj)
{
	obj->clearForces();
}

btTypedConstraint* btDynamicsWorld_getConstraint(btDynamicsWorld* obj, int index)
{
	return obj->getConstraint(index);
}

btConstraintSolver* btDynamicsWorld_getConstraintSolver(btDynamicsWorld* obj)
{
	return obj->getConstraintSolver();
}

void btDynamicsWorld_getGravity(btDynamicsWorld* obj, btScalar* gravity)
{
	VECTOR3_OUT_VAL(obj->getGravity(), gravity);
}

int btDynamicsWorld_getNumConstraints(btDynamicsWorld* obj)
{
	return obj->getNumConstraints();
}

btContactSolverInfo* btDynamicsWorld_getSolverInfo(btDynamicsWorld* obj)
{
	return &obj->getSolverInfo();
}

btDynamicsWorldType btDynamicsWorld_getWorldType(btDynamicsWorld* obj)
{
	return obj->getWorldType();
}

void* btDynamicsWorld_getWorldUserInfo(btDynamicsWorld* obj)
{
	return obj->getWorldUserInfo();
}

void btDynamicsWorld_removeAction(btDynamicsWorld* obj, btActionInterface* action)
{
	obj->removeAction(action);
}

void btDynamicsWorld_removeConstraint(btDynamicsWorld* obj, btTypedConstraint* constraint)
{
	obj->removeConstraint(constraint);
}

void btDynamicsWorld_removeRigidBody(btDynamicsWorld* obj, btRigidBody* body)
{
	obj->removeRigidBody(body);
}

void btDynamicsWorld_setConstraintSolver(btDynamicsWorld* obj, btConstraintSolver* solver)
{
	obj->setConstraintSolver(solver);
}

void btDynamicsWorld_setGravity(btDynamicsWorld* obj, const btScalar* gravity)
{
	VECTOR3_CONV(gravity);
	obj->setGravity(VECTOR3_USE(gravity));
}

void btDynamicsWorld_setInternalTickCallback(btDynamicsWorld* obj, btInternalTickCallback cb)
{
	obj->setInternalTickCallback(cb);
}

void btDynamicsWorld_setInternalTickCallback2(btDynamicsWorld* obj, btInternalTickCallback cb, void* worldUserInfo)
{
	obj->setInternalTickCallback(cb, worldUserInfo);
}

void btDynamicsWorld_setInternalTickCallback3(btDynamicsWorld* obj, btInternalTickCallback cb, void* worldUserInfo, bool isPreTick)
{
	obj->setInternalTickCallback(cb, worldUserInfo, isPreTick);
}

void btDynamicsWorld_setWorldUserInfo(btDynamicsWorld* obj, void* worldUserInfo)
{
	obj->setWorldUserInfo(worldUserInfo);
}

int btDynamicsWorld_stepSimulation(btDynamicsWorld* obj, btScalar timeStep)
{
	return obj->stepSimulation(timeStep);
}

int btDynamicsWorld_stepSimulation2(btDynamicsWorld* obj, btScalar timeStep, int maxSubSteps)
{
	return obj->stepSimulation(timeStep, maxSubSteps);
}

int btDynamicsWorld_stepSimulation3(btDynamicsWorld* obj, btScalar timeStep, int maxSubSteps, btScalar fixedTimeStep)
{
	return obj->stepSimulation(timeStep, maxSubSteps, fixedTimeStep);
}

void btDynamicsWorld_synchronizeMotionStates(btDynamicsWorld* obj)
{
	obj->synchronizeMotionStates();
}
