#include <BulletCollision/CollisionDispatch/btCollisionWorld.h>
#include <BulletDynamics/Character/btCharacterControllerInterface.h>

#include "conversion.h"
#include "btCharacterControllerInterface_wrap.h"

#ifndef BULLETC_DISABLE_IACTION_CLASSES

bool btCharacterControllerInterface_canJump(btCharacterControllerInterface* obj)
{
	return obj->canJump();
}

void btCharacterControllerInterface_jump(btCharacterControllerInterface* obj)
{
	obj->jump();
}

bool btCharacterControllerInterface_onGround(btCharacterControllerInterface* obj)
{
	return obj->onGround();
}

void btCharacterControllerInterface_playerStep(btCharacterControllerInterface* obj, btCollisionWorld* collisionWorld, btScalar dt)
{
	obj->playerStep(collisionWorld, dt);
}

void btCharacterControllerInterface_preStep(btCharacterControllerInterface* obj, btCollisionWorld* collisionWorld)
{
	obj->preStep(collisionWorld);
}

void btCharacterControllerInterface_reset(btCharacterControllerInterface* obj, btCollisionWorld* collisionWorld)
{
	obj->reset(collisionWorld);
}

void btCharacterControllerInterface_setUpInterpolate(btCharacterControllerInterface* obj, bool value)
{
	obj->setUpInterpolate(value);
}

void btCharacterControllerInterface_setWalkDirection(btCharacterControllerInterface* obj, const btScalar* walkDirection)
{
	VECTOR3_CONV(walkDirection);
	obj->setWalkDirection(VECTOR3_USE(walkDirection));
}

void btCharacterControllerInterface_setVelocityForTimeInterval(btCharacterControllerInterface* obj, const btScalar* velocity, btScalar timeInterval)
{
	VECTOR3_CONV(velocity);
	obj->setVelocityForTimeInterval(VECTOR3_USE(velocity), timeInterval);
}

void btCharacterControllerInterface_warp(btCharacterControllerInterface* obj, const btScalar* origin)
{
	VECTOR3_CONV(origin);
	obj->warp(VECTOR3_USE(origin));
}

#endif
