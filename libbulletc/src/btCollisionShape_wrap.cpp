#include <BulletCollision/CollisionShapes/btCollisionShape.h>
#include <LinearMath/btSerializer.h>

#include "conversion.h"
#include "btCollisionShape_wrap.h"

void btCollisionShape_calculateLocalInertia(btCollisionShape* obj, btScalar mass, btScalar* inertia)
{
	VECTOR3_DEF(inertia);
	obj->calculateLocalInertia(mass, VECTOR3_USE(inertia));
	VECTOR3_DEF_OUT(inertia);
}

int btCollisionShape_calculateSerializeBufferSize(btCollisionShape* obj)
{
	return obj->calculateSerializeBufferSize();
}

void btCollisionShape_calculateTemporalAabb(btCollisionShape* obj, const btScalar* curTrans, const btScalar* linvel, const btScalar* angvel, btScalar timeStep, btScalar* temporalAabbMin, btScalar* temporalAabbMax)
{
	TRANSFORM_CONV(curTrans);
	VECTOR3_CONV(linvel);
	VECTOR3_CONV(angvel);
	VECTOR3_DEF(temporalAabbMin);
	VECTOR3_DEF(temporalAabbMax);
	obj->calculateTemporalAabb(TRANSFORM_USE(curTrans), VECTOR3_USE(linvel), VECTOR3_USE(angvel), timeStep, VECTOR3_USE(temporalAabbMin), VECTOR3_USE(temporalAabbMax));
	VECTOR3_DEF_OUT(temporalAabbMin);
	VECTOR3_DEF_OUT(temporalAabbMax);
}

void btCollisionShape_getAabb(btCollisionShape* obj, const btScalar* t, btScalar* aabbMin, btScalar* aabbMax)
{
	TRANSFORM_CONV(t);
	VECTOR3_DEF(aabbMin);
	VECTOR3_DEF(aabbMax);
	obj->getAabb(TRANSFORM_USE(t), VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEF_OUT(aabbMin);
	VECTOR3_DEF_OUT(aabbMax);
}

btScalar btCollisionShape_getAngularMotionDisc(btCollisionShape* obj)
{
	return obj->getAngularMotionDisc();
}

void btCollisionShape_getAnisotropicRollingFrictionDirection(btCollisionShape* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getAnisotropicRollingFrictionDirection(), value);
}

void btCollisionShape_getBoundingSphere(btCollisionShape* obj, btScalar* center, btScalar* radius)
{
	VECTOR3_DEF(center);
	obj->getBoundingSphere(VECTOR3_USE(center), *radius);
	VECTOR3_DEF_OUT(center);
}

btScalar btCollisionShape_getContactBreakingThreshold(btCollisionShape* obj, btScalar defaultContactThresholdFactor)
{
	return obj->getContactBreakingThreshold(defaultContactThresholdFactor);
}

void btCollisionShape_getLocalScaling(btCollisionShape* obj, btScalar* scaling)
{
	VECTOR3_OUT(&obj->getLocalScaling(), scaling);
}

btScalar btCollisionShape_getMargin(btCollisionShape* obj)
{
	return obj->getMargin();
}

const char* btCollisionShape_getName(btCollisionShape* obj)
{
	return obj->getName();
}

int btCollisionShape_getShapeType(btCollisionShape* obj)
{
	return obj->getShapeType();
}

int btCollisionShape_getUserIndex(btCollisionShape* obj)
{
	return obj->getUserIndex();
}

void* btCollisionShape_getUserPointer(btCollisionShape* obj)
{
	return obj->getUserPointer();
}

bool btCollisionShape_isCompound(btCollisionShape* obj)
{
	return obj->isCompound();
}

bool btCollisionShape_isConcave(btCollisionShape* obj)
{
	return obj->isConcave();
}

bool btCollisionShape_isConvex(btCollisionShape* obj)
{
	return obj->isConvex();
}

bool btCollisionShape_isConvex2d(btCollisionShape* obj)
{
	return obj->isConvex2d();
}

bool btCollisionShape_isInfinite(btCollisionShape* obj)
{
	return obj->isInfinite();
}

bool btCollisionShape_isNonMoving(btCollisionShape* obj)
{
	return obj->isNonMoving();
}

bool btCollisionShape_isPolyhedral(btCollisionShape* obj)
{
	return obj->isPolyhedral();
}

bool btCollisionShape_isSoftBody(btCollisionShape* obj)
{
	return obj->isSoftBody();
}

const char* btCollisionShape_serialize(btCollisionShape* obj, void* dataBuffer, btSerializer* serializer)
{
	return obj->serialize(dataBuffer, serializer);
}

void btCollisionShape_serializeSingleShape(btCollisionShape* obj, btSerializer* serializer)
{
	obj->serializeSingleShape(serializer);
}

void btCollisionShape_setLocalScaling(btCollisionShape* obj, const btScalar* scaling)
{
	VECTOR3_CONV(scaling);
	obj->setLocalScaling(VECTOR3_USE(scaling));
}

void btCollisionShape_setMargin(btCollisionShape* obj, btScalar margin)
{
	obj->setMargin(margin);
}

void btCollisionShape_setUserIndex(btCollisionShape* obj, int index)
{
	obj->setUserIndex(index);
}

void btCollisionShape_setUserPointer(btCollisionShape* obj, void* userPtr)
{
	obj->setUserPointer(userPtr);
}

void btCollisionShape_delete(btCollisionShape* obj)
{
	delete obj;
}
