#include <BulletCollision/CollisionDispatch/btGhostObject.h>
#include <BulletCollision/CollisionShapes/btConvexShape.h>
#include <BulletDynamics/Character/btKinematicCharacterController.h>

#include "btKinematicCharacterController_wrap.h"

#ifndef BULLETC_DISABLE_IACTION_CLASSES

btKinematicCharacterController* btKinematicCharacterController_new(btPairCachingGhostObject* ghostObject, btConvexShape* convexShape, btScalar stepHeight)
{
	return new btKinematicCharacterController(ghostObject, convexShape, stepHeight);
}

btKinematicCharacterController* btKinematicCharacterController_new2(btPairCachingGhostObject* ghostObject, btConvexShape* convexShape, btScalar stepHeight, int upAxis)
{
	return new btKinematicCharacterController(ghostObject, convexShape, stepHeight, upAxis);
}

btPairCachingGhostObject* btKinematicCharacterController_getGhostObject(btKinematicCharacterController* obj)
{
	return obj->getGhostObject();
}

btScalar btKinematicCharacterController_getGravity(btKinematicCharacterController* obj)
{
	return obj->getGravity();
}

btScalar btKinematicCharacterController_getMaxSlope(btKinematicCharacterController* obj)
{
	return obj->getMaxSlope();
}

void btKinematicCharacterController_setFallSpeed(btKinematicCharacterController* obj, btScalar fallSpeed)
{
	obj->setFallSpeed(fallSpeed);
}

void btKinematicCharacterController_setGravity(btKinematicCharacterController* obj, btScalar gravity)
{
	obj->setGravity(gravity);
}

void btKinematicCharacterController_setJumpSpeed(btKinematicCharacterController* obj, btScalar jumpSpeed)
{
	obj->setJumpSpeed(jumpSpeed);
}

void btKinematicCharacterController_setMaxJumpHeight(btKinematicCharacterController* obj, btScalar maxJumpHeight)
{
	obj->setMaxJumpHeight(maxJumpHeight);
}

void btKinematicCharacterController_setMaxSlope(btKinematicCharacterController* obj, btScalar slopeRadians)
{
	obj->setMaxSlope(slopeRadians);
}

void btKinematicCharacterController_setUpAxis(btKinematicCharacterController* obj, int axis)
{
	obj->setUpAxis(axis);
}

void btKinematicCharacterController_setUseGhostSweepTest(btKinematicCharacterController* obj, bool useGhostObjectSweepTest)
{
	obj->setUseGhostSweepTest(useGhostObjectSweepTest);
}

#endif
