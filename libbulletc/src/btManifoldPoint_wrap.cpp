#include <BulletCollision/CollisionDispatch/btManifoldResult.h>
#include <BulletCollision/NarrowPhaseCollision/btManifoldPoint.h>

#include "conversion.h"
#include "btManifoldPoint_wrap.h"

btManifoldPoint* btManifoldPoint_new()
{
	return new btManifoldPoint();
}

btManifoldPoint* btManifoldPoint_new2(const btScalar* pointA, const btScalar* pointB, const btScalar* normal, btScalar distance)
{
	VECTOR3_CONV(pointA);
	VECTOR3_CONV(pointB);
	VECTOR3_CONV(normal);
	return new btManifoldPoint(VECTOR3_USE(pointA), VECTOR3_USE(pointB), VECTOR3_USE(normal), distance);
}

btScalar btManifoldPoint_getAppliedImpulse(btManifoldPoint* obj)
{
	return obj->getAppliedImpulse();
}

btScalar btManifoldPoint_getAppliedImpulseLateral1(btManifoldPoint* obj)
{
	return obj->m_appliedImpulseLateral1;
}

btScalar btManifoldPoint_getAppliedImpulseLateral2(btManifoldPoint* obj)
{
	return obj->m_appliedImpulseLateral2;
}

btScalar btManifoldPoint_getCombinedFriction(btManifoldPoint* obj)
{
	return obj->m_combinedFriction;
}

btScalar btManifoldPoint_getCombinedRestitution(btManifoldPoint* obj)
{
	return obj->m_combinedRestitution;
}

btScalar btManifoldPoint_getCombinedRollingFriction(btManifoldPoint* obj)
{
	return obj->m_combinedRollingFriction;
}

btScalar btManifoldPoint_getContactCFM1(btManifoldPoint* obj)
{
	return obj->m_contactCFM1;
}

btScalar btManifoldPoint_getContactCFM2(btManifoldPoint* obj)
{
	return obj->m_contactCFM2;
}

btScalar btManifoldPoint_getContactMotion1(btManifoldPoint* obj)
{
	return obj->m_contactMotion1;
}

btScalar btManifoldPoint_getContactMotion2(btManifoldPoint* obj)
{
	return obj->m_contactMotion2;
}

btScalar btManifoldPoint_getDistance(btManifoldPoint* obj)
{
	return obj->getDistance();
}

btScalar btManifoldPoint_getDistance1(btManifoldPoint* obj)
{
	return obj->m_distance1;
}

int btManifoldPoint_getIndex0(btManifoldPoint* obj)
{
	return obj->m_index0;
}

int btManifoldPoint_getIndex1(btManifoldPoint* obj)
{
	return obj->m_index1;
}

void btManifoldPoint_getLateralFrictionDir1(btManifoldPoint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_lateralFrictionDir1, value);
}

void btManifoldPoint_getLateralFrictionDir2(btManifoldPoint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_lateralFrictionDir2, value);
}

bool btManifoldPoint_getLateralFrictionInitialized(btManifoldPoint* obj)
{
	return obj->m_lateralFrictionInitialized;
}

int btManifoldPoint_getLifeTime(btManifoldPoint* obj)
{
	return obj->getLifeTime();
}

void btManifoldPoint_getLocalPointA(btManifoldPoint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_localPointA, value);
}

void btManifoldPoint_getLocalPointB(btManifoldPoint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_localPointB, value);
}

void btManifoldPoint_getNormalWorldOnB(btManifoldPoint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_normalWorldOnB, value);
}

int btManifoldPoint_getPartId0(btManifoldPoint* obj)
{
	return obj->m_partId0;
}

int btManifoldPoint_getPartId1(btManifoldPoint* obj)
{
	return obj->m_partId1;
}

void btManifoldPoint_getPositionWorldOnA(btManifoldPoint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getPositionWorldOnA(), value);
}

void btManifoldPoint_getPositionWorldOnB(btManifoldPoint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getPositionWorldOnB(), value);
}

void* btManifoldPoint_getUserPersistentData(btManifoldPoint* obj)
{
	return obj->m_userPersistentData;
}

void btManifoldPoint_setAppliedImpulse(btManifoldPoint* obj, btScalar value)
{
	obj->m_appliedImpulse = value;
}

void btManifoldPoint_setAppliedImpulseLateral1(btManifoldPoint* obj, btScalar value)
{
	obj->m_appliedImpulseLateral1 = value;
}

void btManifoldPoint_setAppliedImpulseLateral2(btManifoldPoint* obj, btScalar value)
{
	obj->m_appliedImpulseLateral2 = value;
}

void btManifoldPoint_setCombinedFriction(btManifoldPoint* obj, btScalar value)
{
	obj->m_combinedFriction = value;
}

void btManifoldPoint_setCombinedRestitution(btManifoldPoint* obj, btScalar value)
{
	obj->m_combinedRestitution = value;
}

void btManifoldPoint_setCombinedRollingFriction(btManifoldPoint* obj, btScalar value)
{
	obj->m_combinedRollingFriction = value;
}

void btManifoldPoint_setContactCFM1(btManifoldPoint* obj, btScalar value)
{
	obj->m_contactCFM1 = value;
}

void btManifoldPoint_setContactCFM2(btManifoldPoint* obj, btScalar value)
{
	obj->m_contactCFM2 = value;
}

void btManifoldPoint_setContactMotion1(btManifoldPoint* obj, btScalar value)
{
	obj->m_contactMotion1 = value;
}

void btManifoldPoint_setContactMotion2(btManifoldPoint* obj, btScalar value)
{
	obj->m_contactMotion2 = value;
}

void btManifoldPoint_setDistance(btManifoldPoint* obj, btScalar dist)
{
	obj->setDistance(dist);
}

void btManifoldPoint_setDistance1(btManifoldPoint* obj, btScalar value)
{
	obj->m_distance1 = value;
}

void btManifoldPoint_setIndex0(btManifoldPoint* obj, int value)
{
	obj->m_index0 = value;
}

void btManifoldPoint_setIndex1(btManifoldPoint* obj, int value)
{
	obj->m_index1 = value;
}

void btManifoldPoint_setLateralFrictionDir1(btManifoldPoint* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_lateralFrictionDir1);
}

void btManifoldPoint_setLateralFrictionDir2(btManifoldPoint* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_lateralFrictionDir2);
}

void btManifoldPoint_setLateralFrictionInitialized(btManifoldPoint* obj, bool value)
{
	obj->m_lateralFrictionInitialized = value;
}

void btManifoldPoint_setLifeTime(btManifoldPoint* obj, int value)
{
	obj->m_lifeTime = value;
}

void btManifoldPoint_setLocalPointA(btManifoldPoint* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_localPointA);
}

void btManifoldPoint_setLocalPointB(btManifoldPoint* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_localPointB);
}

void btManifoldPoint_setNormalWorldOnB(btManifoldPoint* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_normalWorldOnB);
}

void btManifoldPoint_setPartId0(btManifoldPoint* obj, int value)
{
	obj->m_partId0 = value;
}

void btManifoldPoint_setPartId1(btManifoldPoint* obj, int value)
{
	obj->m_partId1 = value;
}

void btManifoldPoint_setPositionWorldOnA(btManifoldPoint* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_positionWorldOnA);
}

void btManifoldPoint_setPositionWorldOnB(btManifoldPoint* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_positionWorldOnB);
}

void btManifoldPoint_setUserPersistentData(btManifoldPoint* obj, void* value)
{
	obj->m_userPersistentData = value;
}

void btManifoldPoint_delete(btManifoldPoint* obj)
{
	delete obj;
}

ContactAddedCallback getGContactAddedCallback()
{
	return gContactAddedCallback;
}

void setGContactAddedCallback(ContactAddedCallback value)
{
	gContactAddedCallback = value;
}
