#include <BulletCollision/BroadphaseCollision/btBroadphaseInterface.h>
#include <BulletCollision/BroadphaseCollision/btDispatcher.h>

#include "conversion.h"
#include "btBroadphaseInterface_wrap.h"

btBroadphaseAabbCallbackWrapper::btBroadphaseAabbCallbackWrapper(pBroadphaseAabbCallback_Process processCallback)
{
	_processCallback = processCallback;
}

bool btBroadphaseAabbCallbackWrapper::process(const btBroadphaseProxy* proxy)
{
	return _processCallback(proxy);
}


btBroadphaseRayCallbackWrapper::btBroadphaseRayCallbackWrapper(pBroadphaseAabbCallback_Process processCallback)
{
	_processCallback = processCallback;
}

bool btBroadphaseRayCallbackWrapper::process(const btBroadphaseProxy* proxy)
{
	return _processCallback(proxy);
}


btBroadphaseAabbCallbackWrapper* btBroadphaseAabbCallbackWrapper_new(pBroadphaseAabbCallback_Process processCallback)
{
	return new btBroadphaseAabbCallbackWrapper(processCallback);
}


bool btBroadphaseAabbCallback_process(btBroadphaseAabbCallback* obj, const btBroadphaseProxy* proxy)
{
	return obj->process(proxy);
}

void btBroadphaseAabbCallback_delete(btBroadphaseAabbCallback* obj)
{
	delete obj;
}


btBroadphaseRayCallbackWrapper* btBroadphaseRayCallbackWrapper_new(pBroadphaseAabbCallback_Process processCallback)
{
	return new btBroadphaseRayCallbackWrapper(processCallback);
}


btScalar btBroadphaseRayCallback_getLambda_max(btBroadphaseRayCallback* obj)
{
	return obj->m_lambda_max;
}

void btBroadphaseRayCallback_getRayDirectionInverse(btBroadphaseRayCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_rayDirectionInverse, value);
}

unsigned int* btBroadphaseRayCallback_getSigns(btBroadphaseRayCallback* obj)
{
	return obj->m_signs;
}

void btBroadphaseRayCallback_setLambda_max(btBroadphaseRayCallback* obj, btScalar value)
{
	obj->m_lambda_max = value;
}

void btBroadphaseRayCallback_setRayDirectionInverse(btBroadphaseRayCallback* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_rayDirectionInverse);
}


void btBroadphaseInterface_aabbTest(btBroadphaseInterface* obj, const btScalar* aabbMin, const btScalar* aabbMax, btBroadphaseAabbCallback* callback)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->aabbTest(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax), *callback);
}

void btBroadphaseInterface_calculateOverlappingPairs(btBroadphaseInterface* obj, btDispatcher* dispatcher)
{
	obj->calculateOverlappingPairs(dispatcher);
}

btBroadphaseProxy* btBroadphaseInterface_createProxy(btBroadphaseInterface* obj, const btScalar* aabbMin, const btScalar* aabbMax, int shapeType, void* userPtr, short collisionFilterGroup, short collisionFilterMask, btDispatcher* dispatcher, void* multiSapProxy)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	return obj->createProxy(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax), shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher, multiSapProxy);
}

void btBroadphaseInterface_destroyProxy(btBroadphaseInterface* obj, btBroadphaseProxy* proxy, btDispatcher* dispatcher)
{
	obj->destroyProxy(proxy, dispatcher);
}

void btBroadphaseInterface_getAabb(btBroadphaseInterface* obj, btBroadphaseProxy* proxy, btScalar* aabbMin, btScalar* aabbMax)
{
	VECTOR3_DEF(aabbMin);
	VECTOR3_DEF(aabbMax);
	obj->getAabb(proxy, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEF_OUT(aabbMin);
	VECTOR3_DEF_OUT(aabbMax);
}

void btBroadphaseInterface_getBroadphaseAabb(btBroadphaseInterface* obj, btScalar* aabbMin, btScalar* aabbMax)
{
	VECTOR3_DEF(aabbMin);
	VECTOR3_DEF(aabbMax);
	obj->getBroadphaseAabb(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEF_OUT(aabbMin);
	VECTOR3_DEF_OUT(aabbMax);
}

btOverlappingPairCache* btBroadphaseInterface_getOverlappingPairCache(btBroadphaseInterface* obj)
{
	return obj->getOverlappingPairCache();
}

void btBroadphaseInterface_printStats(btBroadphaseInterface* obj)
{
	obj->printStats();
}

void btBroadphaseInterface_rayTest(btBroadphaseInterface* obj, const btScalar* rayFrom, const btScalar* rayTo, btBroadphaseRayCallback* rayCallback)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	obj->rayTest(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), *rayCallback);
}

void btBroadphaseInterface_rayTest2(btBroadphaseInterface* obj, const btScalar* rayFrom, const btScalar* rayTo, btBroadphaseRayCallback* rayCallback, const btScalar* aabbMin)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	VECTOR3_CONV(aabbMin);
	obj->rayTest(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), *rayCallback, VECTOR3_USE(aabbMin));
}

void btBroadphaseInterface_rayTest3(btBroadphaseInterface* obj, const btScalar* rayFrom, const btScalar* rayTo, btBroadphaseRayCallback* rayCallback, const btScalar* aabbMin, const btScalar* aabbMax)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->rayTest(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), *rayCallback, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}

void btBroadphaseInterface_resetPool(btBroadphaseInterface* obj, btDispatcher* dispatcher)
{
	obj->resetPool(dispatcher);
}

void btBroadphaseInterface_setAabb(btBroadphaseInterface* obj, btBroadphaseProxy* proxy, const btScalar* aabbMin, const btScalar* aabbMax, btDispatcher* dispatcher)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->setAabb(proxy, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax), dispatcher);
}

void btBroadphaseInterface_delete(btBroadphaseInterface* obj)
{
	delete obj;
}
