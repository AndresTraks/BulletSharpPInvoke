#include <BulletCollision/CollisionDispatch/btCollisionConfiguration.h>
#include <BulletCollision/CollisionDispatch/btCollisionWorld.h>
#include <BulletCollision/CollisionShapes/btConvexShape.h>
#include <LinearMath/btIDebugDraw.h>
#include <LinearMath/btSerializer.h>

#include "conversion.h"
#include "btCollisionWorld_wrap.h"

btCollisionWorld_ContactResultCallbackWrapper::btCollisionWorld_ContactResultCallbackWrapper(
	p_btCollisionWorld_ContactResultCallback_AddSingleResult addSingleResultCallback,
	p_btCollisionWorld_ContactResultCallback_NeedsCollision needsCollisionCallback)
{
	_addSingleResultCallback = addSingleResultCallback;
	_needsCollisionCallback = needsCollisionCallback;
}

btScalar btCollisionWorld_ContactResultCallbackWrapper::addSingleResult(btManifoldPoint& cp,
	const btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0, const btCollisionObjectWrapper* colObj1Wrap,
	int partId1, int index1)
{
	return _addSingleResultCallback(cp, colObj0Wrap, partId0, index0, colObj1Wrap,
		partId1, index1);
}

bool btCollisionWorld_ContactResultCallbackWrapper::needsCollision(btBroadphaseProxy* proxy0) const
{
	return _needsCollisionCallback(proxy0);
}

bool btCollisionWorld_ContactResultCallbackWrapper::baseNeedsCollision(btBroadphaseProxy* proxy0) const
{
	return btCollisionWorld_ContactResultCallback::needsCollision(proxy0);
}


btCollisionWorld_ConvexResultCallbackWrapper::btCollisionWorld_ConvexResultCallbackWrapper(
	p_btCollisionWorld_ConvexResultCallback_AddSingleResult addSingleResultCallback,
	p_btCollisionWorld_ConvexResultCallback_NeedsCollision needsCollisionCallback)
{
	_addSingleResultCallback = addSingleResultCallback;
	_needsCollisionCallback = needsCollisionCallback;
}

btScalar btCollisionWorld_ConvexResultCallbackWrapper::addSingleResult(btCollisionWorld_LocalConvexResult& convexResult,
	bool normalInWorldSpace)
{
	return _addSingleResultCallback(convexResult, normalInWorldSpace);
}

bool btCollisionWorld_ConvexResultCallbackWrapper::needsCollision(btBroadphaseProxy* proxy0) const
{
	return _needsCollisionCallback(proxy0);
}

bool btCollisionWorld_ConvexResultCallbackWrapper::baseNeedsCollision(btBroadphaseProxy* proxy0) const
{
	return btCollisionWorld_ConvexResultCallback::needsCollision(proxy0);
}


btCollisionWorld_RayResultCallbackWrapper::btCollisionWorld_RayResultCallbackWrapper(
	p_btCollisionWorld_RayResultCallback_AddSingleResult addSingleResultCallback, p_btCollisionWorld_RayResultCallback_NeedsCollision needsCollisionCallback)
{
	_addSingleResultCallback = addSingleResultCallback;
	_needsCollisionCallback = needsCollisionCallback;
}

btScalar btCollisionWorld_RayResultCallbackWrapper::addSingleResult(btCollisionWorld_LocalRayResult& rayResult,
	bool normalInWorldSpace)
{
	return _addSingleResultCallback(rayResult, normalInWorldSpace);
}

bool btCollisionWorld_RayResultCallbackWrapper::needsCollision(btBroadphaseProxy* proxy0) const
{
	return _needsCollisionCallback(proxy0);
}

bool btCollisionWorld_RayResultCallbackWrapper::baseNeedsCollision(btBroadphaseProxy* proxy0) const
{
	return btCollisionWorld_RayResultCallback::needsCollision(proxy0);
}


btCollisionWorld::AllHitsRayResultCallback* btCollisionWorld_AllHitsRayResultCallback_new(
	const btVector3* rayFromWorld, const btVector3* rayToWorld)
{
	BTVECTOR3_IN(rayFromWorld);
	BTVECTOR3_IN(rayToWorld);
	return ALIGNED_NEW(btCollisionWorld::AllHitsRayResultCallback) (BTVECTOR3_USE(rayFromWorld),
		BTVECTOR3_USE(rayToWorld));
}

btAlignedObjectArray_const_btCollisionObjectPtr* btCollisionWorld_AllHitsRayResultCallback_getCollisionObjects(
	btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_collisionObjects;
}

btAlignedObjectArray_btScalar* btCollisionWorld_AllHitsRayResultCallback_getHitFractions(
	btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_hitFractions;
}

btAlignedObjectArray_btVector3* btCollisionWorld_AllHitsRayResultCallback_getHitNormalWorld(
	btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_hitNormalWorld;
}

btAlignedObjectArray_btVector3* btCollisionWorld_AllHitsRayResultCallback_getHitPointWorld(
	btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_hitPointWorld;
}

void btCollisionWorld_AllHitsRayResultCallback_getRayFromWorld(btCollisionWorld::AllHitsRayResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_rayFromWorld);
}

void btCollisionWorld_AllHitsRayResultCallback_getRayToWorld(btCollisionWorld::AllHitsRayResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_rayToWorld);
}

void btCollisionWorld_AllHitsRayResultCallback_setRayFromWorld(btCollisionWorld::AllHitsRayResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_rayFromWorld, value);
}

void btCollisionWorld_AllHitsRayResultCallback_setRayToWorld(btCollisionWorld::AllHitsRayResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_rayToWorld, value);
}


btCollisionWorld::ClosestConvexResultCallback* btCollisionWorld_ClosestConvexResultCallback_new(
	const btVector3* convexFromWorld, const btVector3* convexToWorld)
{
	BTVECTOR3_IN(convexFromWorld);
	BTVECTOR3_IN(convexToWorld);
	return new btCollisionWorld::ClosestConvexResultCallback(BTVECTOR3_USE(convexFromWorld),
		BTVECTOR3_USE(convexToWorld));
}

void btCollisionWorld_ClosestConvexResultCallback_getConvexFromWorld(btCollisionWorld::ClosestConvexResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_convexFromWorld);
}

void btCollisionWorld_ClosestConvexResultCallback_getConvexToWorld(btCollisionWorld::ClosestConvexResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_convexToWorld);
}

const btCollisionObject* btCollisionWorld_ClosestConvexResultCallback_getHitCollisionObject(
	btCollisionWorld::ClosestConvexResultCallback* obj)
{
	return obj->m_hitCollisionObject;
}

void btCollisionWorld_ClosestConvexResultCallback_getHitNormalWorld(btCollisionWorld::ClosestConvexResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_hitNormalWorld);
}

void btCollisionWorld_ClosestConvexResultCallback_getHitPointWorld(btCollisionWorld::ClosestConvexResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_hitPointWorld);
}

void btCollisionWorld_ClosestConvexResultCallback_setConvexFromWorld(btCollisionWorld::ClosestConvexResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_convexFromWorld, value);
}

void btCollisionWorld_ClosestConvexResultCallback_setConvexToWorld(btCollisionWorld::ClosestConvexResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_convexToWorld, value);
}

void btCollisionWorld_ClosestConvexResultCallback_setHitCollisionObject(btCollisionWorld::ClosestConvexResultCallback* obj,
	const btCollisionObject* value)
{
	obj->m_hitCollisionObject = value;
}

void btCollisionWorld_ClosestConvexResultCallback_setHitNormalWorld(btCollisionWorld::ClosestConvexResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_hitNormalWorld, value);
}

void btCollisionWorld_ClosestConvexResultCallback_setHitPointWorld(btCollisionWorld::ClosestConvexResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_hitPointWorld, value);
}


btCollisionWorld::ClosestRayResultCallback* btCollisionWorld_ClosestRayResultCallback_new(
	const btVector3* rayFromWorld, const btVector3* rayToWorld)
{
	BTVECTOR3_IN(rayFromWorld);
	BTVECTOR3_IN(rayToWorld);
	return new btCollisionWorld::ClosestRayResultCallback(BTVECTOR3_USE(rayFromWorld),
		BTVECTOR3_USE(rayToWorld));
}

void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_hitNormalWorld);
}

void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_hitPointWorld);
}

void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_rayFromWorld);
}

void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_rayToWorld);
}

void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_hitNormalWorld, value);
}

void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_hitPointWorld, value);
}

void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_rayFromWorld, value);
}

void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_rayToWorld, value);
}


btCollisionWorld_ContactResultCallbackWrapper* btCollisionWorld_ContactResultCallbackWrapper_new(
	p_btCollisionWorld_ContactResultCallback_AddSingleResult addSingleResultCallback,
	p_btCollisionWorld_ContactResultCallback_NeedsCollision needsCollisionCallback)
{
	return new btCollisionWorld_ContactResultCallbackWrapper(addSingleResultCallback,
		needsCollisionCallback);
}

bool btCollisionWorld_ContactResultCallbackWrapper_needsCollision(btCollisionWorld_ContactResultCallbackWrapper* obj, btBroadphaseProxy* proxy0)
{
	return obj->baseNeedsCollision(proxy0);
}


btScalar btCollisionWorld_ContactResultCallback_addSingleResult(btCollisionWorld::ContactResultCallback* obj,
	btManifoldPoint* cp, const btCollisionObjectWrapper* colObj0Wrap, int partId0,
	int index0, const btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1)
{
	return obj->addSingleResult(*cp, colObj0Wrap, partId0, index0, colObj1Wrap, partId1,
		index1);
}

short btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(btCollisionWorld::ContactResultCallback* obj)
{
	return obj->m_collisionFilterGroup;
}

short btCollisionWorld_ContactResultCallback_getCollisionFilterMask(btCollisionWorld::ContactResultCallback* obj)
{
	return obj->m_collisionFilterMask;
}

bool btCollisionWorld_ContactResultCallback_needsCollision(btCollisionWorld::ContactResultCallback* obj,
	btBroadphaseProxy* proxy0)
{
	return obj->needsCollision(proxy0);
}

void btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(btCollisionWorld::ContactResultCallback* obj,
	short value)
{
	obj->m_collisionFilterGroup = value;
}

void btCollisionWorld_ContactResultCallback_setCollisionFilterMask(btCollisionWorld::ContactResultCallback* obj,
	short value)
{
	obj->m_collisionFilterMask = value;
}

void btCollisionWorld_ContactResultCallback_delete(btCollisionWorld::ContactResultCallback* obj)
{
	delete obj;
}


btCollisionWorld_ConvexResultCallbackWrapper* btCollisionWorld_ConvexResultCallbackWrapper_new(
	p_btCollisionWorld_ConvexResultCallback_AddSingleResult addSingleResultCallback,
	p_btCollisionWorld_ConvexResultCallback_NeedsCollision needsCollisionCallback)
{
	return ALIGNED_NEW(btCollisionWorld_ConvexResultCallbackWrapper) (addSingleResultCallback,
		needsCollisionCallback);
}

bool btCollisionWorld_ConvexResultCallbackWrapper_needsCollision(btCollisionWorld_ConvexResultCallbackWrapper* obj, btBroadphaseProxy* proxy0)
{
	return obj->baseNeedsCollision(proxy0);
}


btScalar btCollisionWorld_ConvexResultCallback_addSingleResult(btCollisionWorld::ConvexResultCallback* obj,
	btCollisionWorld_LocalConvexResult* convexResult, bool normalInWorldSpace)
{
	return obj->addSingleResult(*convexResult, normalInWorldSpace);
}

btScalar btCollisionWorld_ConvexResultCallback_getClosestHitFraction(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->m_closestHitFraction;
}

short btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->m_collisionFilterGroup;
}

short btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->m_collisionFilterMask;
}

bool btCollisionWorld_ConvexResultCallback_hasHit(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->hasHit();
}

bool btCollisionWorld_ConvexResultCallback_needsCollision(btCollisionWorld::ConvexResultCallback* obj,
	btBroadphaseProxy* proxy0)
{
	return obj->needsCollision(proxy0);
}

void btCollisionWorld_ConvexResultCallback_setClosestHitFraction(btCollisionWorld::ConvexResultCallback* obj,
	btScalar value)
{
	obj->m_closestHitFraction = value;
}

void btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(btCollisionWorld::ConvexResultCallback* obj,
	short value)
{
	obj->m_collisionFilterGroup = value;
}

void btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(btCollisionWorld::ConvexResultCallback* obj,
	short value)
{
	obj->m_collisionFilterMask = value;
}

void btCollisionWorld_ConvexResultCallback_delete(btCollisionWorld::ConvexResultCallback* obj)
{
	ALIGNED_FREE(obj);
}


btCollisionWorld::LocalConvexResult* btCollisionWorld_LocalConvexResult_new(const btCollisionObject* hitCollisionObject,
	btCollisionWorld_LocalShapeInfo* localShapeInfo, const btVector3* hitNormalLocal,
	const btVector3* hitPointLocal, btScalar hitFraction)
{
	BTVECTOR3_IN(hitNormalLocal);
	BTVECTOR3_IN(hitPointLocal);
	return new btCollisionWorld::LocalConvexResult(hitCollisionObject, localShapeInfo,
		BTVECTOR3_USE(hitNormalLocal), BTVECTOR3_USE(hitPointLocal), hitFraction);
}

const btCollisionObject* btCollisionWorld_LocalConvexResult_getHitCollisionObject(
	btCollisionWorld::LocalConvexResult* obj)
{
	return obj->m_hitCollisionObject;
}

btScalar btCollisionWorld_LocalConvexResult_getHitFraction(btCollisionWorld::LocalConvexResult* obj)
{
	return obj->m_hitFraction;
}

void btCollisionWorld_LocalConvexResult_getHitNormalLocal(btCollisionWorld::LocalConvexResult* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_hitNormalLocal);
}

void btCollisionWorld_LocalConvexResult_getHitPointLocal(btCollisionWorld::LocalConvexResult* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_hitPointLocal);
}

btCollisionWorld_LocalShapeInfo* btCollisionWorld_LocalConvexResult_getLocalShapeInfo(
	btCollisionWorld::LocalConvexResult* obj)
{
	return obj->m_localShapeInfo;
}

void btCollisionWorld_LocalConvexResult_setHitCollisionObject(btCollisionWorld::LocalConvexResult* obj,
	const btCollisionObject* value)
{
	obj->m_hitCollisionObject = value;
}

void btCollisionWorld_LocalConvexResult_setHitFraction(btCollisionWorld::LocalConvexResult* obj,
	btScalar value)
{
	obj->m_hitFraction = value;
}

void btCollisionWorld_LocalConvexResult_setHitNormalLocal(btCollisionWorld::LocalConvexResult* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_hitNormalLocal, value);
}

void btCollisionWorld_LocalConvexResult_setHitPointLocal(btCollisionWorld::LocalConvexResult* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_hitPointLocal, value);
}

void btCollisionWorld_LocalConvexResult_setLocalShapeInfo(btCollisionWorld::LocalConvexResult* obj,
	btCollisionWorld_LocalShapeInfo* value)
{
	obj->m_localShapeInfo = value;
}

void btCollisionWorld_LocalConvexResult_delete(btCollisionWorld::LocalConvexResult* obj)
{
	delete obj;
}


btCollisionWorld::LocalRayResult* btCollisionWorld_LocalRayResult_new(const btCollisionObject* collisionObject,
	btCollisionWorld_LocalShapeInfo* localShapeInfo, const btVector3* hitNormalLocal,
	btScalar hitFraction)
{
	BTVECTOR3_IN(hitNormalLocal);
	return new btCollisionWorld::LocalRayResult(collisionObject, localShapeInfo,
		BTVECTOR3_USE(hitNormalLocal), hitFraction);
}

const btCollisionObject* btCollisionWorld_LocalRayResult_getCollisionObject(btCollisionWorld::LocalRayResult* obj)
{
	return obj->m_collisionObject;
}

btScalar btCollisionWorld_LocalRayResult_getHitFraction(btCollisionWorld::LocalRayResult* obj)
{
	return obj->m_hitFraction;
}

void btCollisionWorld_LocalRayResult_getHitNormalLocal(btCollisionWorld::LocalRayResult* obj,
	btVector3* value)
{
	BTVECTOR3_SET(value, obj->m_hitNormalLocal);
}

btCollisionWorld_LocalShapeInfo* btCollisionWorld_LocalRayResult_getLocalShapeInfo(
	btCollisionWorld::LocalRayResult* obj)
{
	return obj->m_localShapeInfo;
}

void btCollisionWorld_LocalRayResult_setCollisionObject(btCollisionWorld::LocalRayResult* obj,
	const btCollisionObject* value)
{
	obj->m_collisionObject = value;
}

void btCollisionWorld_LocalRayResult_setHitFraction(btCollisionWorld::LocalRayResult* obj,
	btScalar value)
{
	obj->m_hitFraction = value;
}

void btCollisionWorld_LocalRayResult_setHitNormalLocal(btCollisionWorld::LocalRayResult* obj,
	const btVector3* value)
{
	BTVECTOR3_COPY(&obj->m_hitNormalLocal, value);
}

void btCollisionWorld_LocalRayResult_setLocalShapeInfo(btCollisionWorld::LocalRayResult* obj,
	btCollisionWorld_LocalShapeInfo* value)
{
	obj->m_localShapeInfo = value;
}

void btCollisionWorld_LocalRayResult_delete(btCollisionWorld::LocalRayResult* obj)
{
	delete obj;
}


btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalShapeInfo_new()
{
	return new btCollisionWorld::LocalShapeInfo();
}

int btCollisionWorld_LocalShapeInfo_getShapePart(btCollisionWorld::LocalShapeInfo* obj)
{
	return obj->m_shapePart;
}

int btCollisionWorld_LocalShapeInfo_getTriangleIndex(btCollisionWorld::LocalShapeInfo* obj)
{
	return obj->m_triangleIndex;
}

void btCollisionWorld_LocalShapeInfo_setShapePart(btCollisionWorld::LocalShapeInfo* obj,
	int value)
{
	obj->m_shapePart = value;
}

void btCollisionWorld_LocalShapeInfo_setTriangleIndex(btCollisionWorld::LocalShapeInfo* obj,
	int value)
{
	obj->m_triangleIndex = value;
}

void btCollisionWorld_LocalShapeInfo_delete(btCollisionWorld::LocalShapeInfo* obj)
{
	delete obj;
}


btCollisionWorld_RayResultCallbackWrapper* btCollisionWorld_RayResultCallbackWrapper_new(
	p_btCollisionWorld_RayResultCallback_AddSingleResult addSingleResultCallback, p_btCollisionWorld_RayResultCallback_NeedsCollision needsCollisionCallback)
{
	return ALIGNED_NEW(btCollisionWorld_RayResultCallbackWrapper) (addSingleResultCallback,
		needsCollisionCallback);
}

bool btCollisionWorld_RayResultCallbackWrapper_needsCollision(btCollisionWorld_RayResultCallbackWrapper* obj, btBroadphaseProxy* proxy0)
{
	return obj->baseNeedsCollision(proxy0);
}


btScalar btCollisionWorld_RayResultCallback_addSingleResult(btCollisionWorld::RayResultCallback* obj,
	btCollisionWorld_LocalRayResult* rayResult, bool normalInWorldSpace)
{
	return obj->addSingleResult(*rayResult, normalInWorldSpace);
}

btScalar btCollisionWorld_RayResultCallback_getClosestHitFraction(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_closestHitFraction;
}

short btCollisionWorld_RayResultCallback_getCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_collisionFilterGroup;
}

short btCollisionWorld_RayResultCallback_getCollisionFilterMask(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_collisionFilterMask;
}

const btCollisionObject* btCollisionWorld_RayResultCallback_getCollisionObject(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_collisionObject;
}

unsigned int btCollisionWorld_RayResultCallback_getFlags(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_flags;
}

bool btCollisionWorld_RayResultCallback_hasHit(btCollisionWorld::RayResultCallback* obj)
{
	return obj->hasHit();
}

bool btCollisionWorld_RayResultCallback_needsCollision(btCollisionWorld::RayResultCallback* obj,
	btBroadphaseProxy* proxy0)
{
	return obj->needsCollision(proxy0);
}

void btCollisionWorld_RayResultCallback_setClosestHitFraction(btCollisionWorld::RayResultCallback* obj,
	btScalar value)
{
	obj->m_closestHitFraction = value;
}

void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj,
	short value)
{
	obj->m_collisionFilterGroup = value;
}

void btCollisionWorld_RayResultCallback_setCollisionFilterMask(btCollisionWorld::RayResultCallback* obj,
	short value)
{
	obj->m_collisionFilterMask = value;
}

void btCollisionWorld_RayResultCallback_setCollisionObject(btCollisionWorld::RayResultCallback* obj,
	const btCollisionObject* value)
{
	obj->m_collisionObject = value;
}

void btCollisionWorld_RayResultCallback_setFlags(btCollisionWorld::RayResultCallback* obj,
	unsigned int value)
{
	obj->m_flags = value;
}

void btCollisionWorld_RayResultCallback_delete(btCollisionWorld::RayResultCallback* obj)
{
	ALIGNED_FREE(obj);
}


btCollisionWorld* btCollisionWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* broadphasePairCache,
	btCollisionConfiguration* collisionConfiguration)
{
	return new btCollisionWorld(dispatcher, broadphasePairCache, collisionConfiguration);
}

void btCollisionWorld_addCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject)
{
	obj->addCollisionObject(collisionObject);
}

void btCollisionWorld_addCollisionObject2(btCollisionWorld* obj, btCollisionObject* collisionObject,
	short collisionFilterGroup)
{
	obj->addCollisionObject(collisionObject, collisionFilterGroup);
}

void btCollisionWorld_addCollisionObject3(btCollisionWorld* obj, btCollisionObject* collisionObject,
	short collisionFilterGroup, short collisionFilterMask)
{
	obj->addCollisionObject(collisionObject, collisionFilterGroup, collisionFilterMask);
}

void btCollisionWorld_computeOverlappingPairs(btCollisionWorld* obj)
{
	obj->computeOverlappingPairs();
}

void btCollisionWorld_contactPairTest(btCollisionWorld* obj, btCollisionObject* colObjA,
	btCollisionObject* colObjB, btCollisionWorld_ContactResultCallback* resultCallback)
{
	obj->contactPairTest(colObjA, colObjB, *resultCallback);
}

void btCollisionWorld_contactTest(btCollisionWorld* obj, btCollisionObject* colObj,
	btCollisionWorld_ContactResultCallback* resultCallback)
{
	obj->contactTest(colObj, *resultCallback);
}

void btCollisionWorld_convexSweepTest(btCollisionWorld* obj, const btConvexShape* castShape,
	const btTransform* from, const btTransform* to, btCollisionWorld_ConvexResultCallback* resultCallback)
{
	BTTRANSFORM_IN(from);
	BTTRANSFORM_IN(to);
	obj->convexSweepTest(castShape, BTTRANSFORM_USE(from), BTTRANSFORM_USE(to), *resultCallback);
}

void btCollisionWorld_convexSweepTest2(btCollisionWorld* obj, const btConvexShape* castShape,
	const btTransform* from, const btTransform* to, btCollisionWorld_ConvexResultCallback* resultCallback,
	btScalar allowedCcdPenetration)
{
	BTTRANSFORM_IN(from);
	BTTRANSFORM_IN(to);
	obj->convexSweepTest(castShape, BTTRANSFORM_USE(from), BTTRANSFORM_USE(to), *resultCallback,
		allowedCcdPenetration);
}

void btCollisionWorld_debugDrawObject(btCollisionWorld* obj, const btTransform* worldTransform,
	const btCollisionShape* shape, const btVector3* color)
{
	BTTRANSFORM_IN(worldTransform);
	BTVECTOR3_IN(color);
	obj->debugDrawObject(BTTRANSFORM_USE(worldTransform), shape, BTVECTOR3_USE(color));
}

void btCollisionWorld_debugDrawWorld(btCollisionWorld* obj)
{
	obj->debugDrawWorld();
}

btBroadphaseInterface* btCollisionWorld_getBroadphase(btCollisionWorld* obj)
{
	return obj->getBroadphase();
}

const btCollisionObjectArray* btCollisionWorld_getCollisionObjectArray(btCollisionWorld* obj)
{
	return &obj->getCollisionObjectArray();
}

btIDebugDraw* btCollisionWorld_getDebugDrawer(btCollisionWorld* obj)
{
	return obj->getDebugDrawer();
}

const btDispatcher* btCollisionWorld_getDispatcher(btCollisionWorld* obj)
{
	return obj->getDispatcher();
}

const btDispatcherInfo* btCollisionWorld_getDispatchInfo(btCollisionWorld* obj)
{
	return &obj->getDispatchInfo();
}

bool btCollisionWorld_getForceUpdateAllAabbs(btCollisionWorld* obj)
{
	return obj->getForceUpdateAllAabbs();
}

int btCollisionWorld_getNumCollisionObjects(btCollisionWorld* obj)
{
	return obj->getNumCollisionObjects();
}

btOverlappingPairCache* btCollisionWorld_getPairCache(btCollisionWorld* obj)
{
	return obj->getPairCache();
}

void btCollisionWorld_objectQuerySingle(const btConvexShape* castShape, const btTransform* rayFromTrans,
	const btTransform* rayToTrans, btCollisionObject* collisionObject, const btCollisionShape* collisionShape,
	const btTransform* colObjWorldTransform, btCollisionWorld_ConvexResultCallback* resultCallback,
	btScalar allowedPenetration)
{
	BTTRANSFORM_IN(rayFromTrans);
	BTTRANSFORM_IN(rayToTrans);
	BTTRANSFORM_IN(colObjWorldTransform);
	btCollisionWorld::objectQuerySingle(castShape, BTTRANSFORM_USE(rayFromTrans),
		BTTRANSFORM_USE(rayToTrans), collisionObject, collisionShape, BTTRANSFORM_USE(colObjWorldTransform),
		*resultCallback, allowedPenetration);
}

void btCollisionWorld_objectQuerySingleInternal(const btConvexShape* castShape, const btTransform* convexFromTrans,
	const btTransform* convexToTrans, const btCollisionObjectWrapper* colObjWrap, btCollisionWorld_ConvexResultCallback* resultCallback,
	btScalar allowedPenetration)
{
	BTTRANSFORM_IN(convexFromTrans);
	BTTRANSFORM_IN(convexToTrans);
	btCollisionWorld::objectQuerySingleInternal(castShape, BTTRANSFORM_USE(convexFromTrans),
		BTTRANSFORM_USE(convexToTrans), colObjWrap, *resultCallback, allowedPenetration);
}

void btCollisionWorld_performDiscreteCollisionDetection(btCollisionWorld* obj)
{
	obj->performDiscreteCollisionDetection();
}

void btCollisionWorld_rayTest(btCollisionWorld* obj, const btVector3* rayFromWorld,
	const btVector3* rayToWorld, btCollisionWorld_RayResultCallback* resultCallback)
{
	BTVECTOR3_IN(rayFromWorld);
	BTVECTOR3_IN(rayToWorld);
	obj->rayTest(BTVECTOR3_USE(rayFromWorld), BTVECTOR3_USE(rayToWorld), *resultCallback);
}

void btCollisionWorld_rayTestSingle(const btTransform* rayFromTrans, const btTransform* rayToTrans,
	btCollisionObject* collisionObject, const btCollisionShape* collisionShape, const btTransform* colObjWorldTransform,
	btCollisionWorld_RayResultCallback* resultCallback)
{
	BTTRANSFORM_IN(rayFromTrans);
	BTTRANSFORM_IN(rayToTrans);
	BTTRANSFORM_IN(colObjWorldTransform);
	btCollisionWorld::rayTestSingle(BTTRANSFORM_USE(rayFromTrans), BTTRANSFORM_USE(rayToTrans),
		collisionObject, collisionShape, BTTRANSFORM_USE(colObjWorldTransform), *resultCallback);
}

void btCollisionWorld_rayTestSingleInternal(const btTransform* rayFromTrans, const btTransform* rayToTrans,
	const btCollisionObjectWrapper* collisionObjectWrap, btCollisionWorld_RayResultCallback* resultCallback)
{
	BTTRANSFORM_IN(rayFromTrans);
	BTTRANSFORM_IN(rayToTrans);
	btCollisionWorld::rayTestSingleInternal(BTTRANSFORM_USE(rayFromTrans), BTTRANSFORM_USE(rayToTrans),
		collisionObjectWrap, *resultCallback);
}

void btCollisionWorld_removeCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject)
{
	obj->removeCollisionObject(collisionObject);
}

void btCollisionWorld_serialize(btCollisionWorld* obj, btSerializer* serializer)
{
	obj->serialize(serializer);
}

void btCollisionWorld_setBroadphase(btCollisionWorld* obj, btBroadphaseInterface* pairCache)
{
	obj->setBroadphase(pairCache);
}

void btCollisionWorld_setDebugDrawer(btCollisionWorld* obj, btIDebugDraw* debugDrawer)
{
	obj->setDebugDrawer(debugDrawer);
}

void btCollisionWorld_setForceUpdateAllAabbs(btCollisionWorld* obj, bool forceUpdateAllAabbs)
{
	obj->setForceUpdateAllAabbs(forceUpdateAllAabbs);
}

void btCollisionWorld_updateAabbs(btCollisionWorld* obj)
{
	obj->updateAabbs();
}

void btCollisionWorld_updateSingleAabb(btCollisionWorld* obj, btCollisionObject* colObj)
{
	obj->updateSingleAabb(colObj);
}

void btCollisionWorld_delete(btCollisionWorld* obj)
{
	delete obj;
}
