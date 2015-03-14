#include <BulletCollision/CollisionDispatch/btCollisionWorld.h>
#include <BulletDynamics/Dynamics/btActionInterface.h>
#include <LinearMath/btIDebugDraw.h>

#include "btActionInterface_wrap.h"

btActionInterfaceWrapper::btActionInterfaceWrapper(pIAction_DebugDraw debugDrawCallback, pIAction_UpdateAction updateActionCallback)
{
	_debugDrawCallback = debugDrawCallback;
	_updateActionCallback = updateActionCallback;
}

void btActionInterfaceWrapper::debugDraw(btIDebugDraw* debugDrawer)
{
	_debugDrawCallback(debugDrawer);
}

void btActionInterfaceWrapper::updateAction(btCollisionWorld* collisionWorld, btScalar deltaTimeStep)
{
	_updateActionCallback(collisionWorld, deltaTimeStep);
}


btActionInterfaceWrapper* btActionInterfaceWrapper_new(pIAction_DebugDraw debugDrawCallback, pIAction_UpdateAction updateActionCallback)
{
	return new btActionInterfaceWrapper(debugDrawCallback, updateActionCallback);
}


void btActionInterface_debugDraw(btActionInterface* obj, btIDebugDraw* debugDrawer)
{
	obj->debugDraw(debugDrawer);
}

void btActionInterface_updateAction(btActionInterface* obj, btCollisionWorld* collisionWorld, btScalar deltaTimeStep)
{
	obj->updateAction(collisionWorld, deltaTimeStep);
}

void btActionInterface_delete(btActionInterface* obj)
{
	delete obj;
}
