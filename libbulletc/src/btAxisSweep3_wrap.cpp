#include <BulletCollision/BroadphaseCollision/btAxisSweep3.h>
#include <BulletCollision/BroadphaseCollision/btDispatcher.h>

#include "conversion.h"
#include "btAxisSweep3_wrap.h"

btAxisSweep3* btAxisSweep3_new(const btVector3* worldAabbMin, const btVector3* worldAabbMax)
{
	BTVECTOR3_IN(worldAabbMin);
	BTVECTOR3_IN(worldAabbMax);
	return new btAxisSweep3(BTVECTOR3_USE(worldAabbMin), BTVECTOR3_USE(worldAabbMax));
}

btAxisSweep3* btAxisSweep3_new2(const btVector3* worldAabbMin, const btVector3* worldAabbMax,
	unsigned short maxHandles)
{
	BTVECTOR3_IN(worldAabbMin);
	BTVECTOR3_IN(worldAabbMax);
	return new btAxisSweep3(BTVECTOR3_USE(worldAabbMin), BTVECTOR3_USE(worldAabbMax),
		maxHandles);
}

btAxisSweep3* btAxisSweep3_new3(const btVector3* worldAabbMin, const btVector3* worldAabbMax,
	unsigned short maxHandles, btOverlappingPairCache* pairCache)
{
	BTVECTOR3_IN(worldAabbMin);
	BTVECTOR3_IN(worldAabbMax);
	return new btAxisSweep3(BTVECTOR3_USE(worldAabbMin), BTVECTOR3_USE(worldAabbMax),
		maxHandles, pairCache);
}

btAxisSweep3* btAxisSweep3_new4(const btVector3* worldAabbMin, const btVector3* worldAabbMax,
	unsigned short maxHandles, btOverlappingPairCache* pairCache, bool disableRaycastAccelerator)
{
	BTVECTOR3_IN(worldAabbMin);
	BTVECTOR3_IN(worldAabbMax);
	return new btAxisSweep3(BTVECTOR3_USE(worldAabbMin), BTVECTOR3_USE(worldAabbMax),
		maxHandles, pairCache, disableRaycastAccelerator);
}

unsigned short btAxisSweep3_addHandle(btAxisSweep3* obj, const btVector3* aabbMin,
	const btVector3* aabbMax, void* pOwner, short collisionFilterGroup, short collisionFilterMask,
	btDispatcher* dispatcher, void* multiSapProxy)
{
	BTVECTOR3_IN(aabbMin);
	BTVECTOR3_IN(aabbMax);
	return obj->addHandle(BTVECTOR3_USE(aabbMin), BTVECTOR3_USE(aabbMax), pOwner,
		collisionFilterGroup, collisionFilterMask, dispatcher, multiSapProxy);
}

btAxisSweep3_Handle* btAxisSweep3_getHandle(btAxisSweep3* obj, unsigned short index)
{
	return obj->getHandle(index);
}

unsigned short btAxisSweep3_getNumHandles(btAxisSweep3* obj)
{
	return obj->getNumHandles();
}

btOverlappingPairCallback* btAxisSweep3_getOverlappingPairUserCallback(btAxisSweep3* obj)
{
	return const_cast<btOverlappingPairCallback*>(obj->getOverlappingPairUserCallback());
}
/*
void btAxisSweep3_processAllOverlappingPairs(btAxisSweep3* obj, btOverlapCallback* callback)
{
	obj->processAllOverlappingPairs(callback);
}
*/
void btAxisSweep3_quantize(btAxisSweep3* obj, unsigned short* out, const btVector3* point,
	int isMax)
{
	BTVECTOR3_IN(point);
	obj->quantize(out, BTVECTOR3_USE(point), isMax);
}

void btAxisSweep3_removeHandle(btAxisSweep3* obj, unsigned short handle, btDispatcher* dispatcher)
{
	obj->removeHandle(handle, dispatcher);
}

void btAxisSweep3_setOverlappingPairUserCallback(btAxisSweep3* obj, btOverlappingPairCallback* pairCallback)
{
	obj->setOverlappingPairUserCallback(pairCallback);
}

bool btAxisSweep3_testAabbOverlap(btAxisSweep3* obj, btBroadphaseProxy* proxy0, btBroadphaseProxy* proxy1)
{
	return obj->testAabbOverlap(proxy0, proxy1);
}

void btAxisSweep3_unQuantize(btAxisSweep3* obj, btBroadphaseProxy* proxy, btVector3* aabbMin,
	btVector3* aabbMax)
{
	BTVECTOR3_IN(aabbMin);
	BTVECTOR3_IN(aabbMax);
	obj->unQuantize(proxy, BTVECTOR3_USE(aabbMin), BTVECTOR3_USE(aabbMax));
}

void btAxisSweep3_updateHandle(btAxisSweep3* obj, unsigned short handle, const btVector3* aabbMin,
	const btVector3* aabbMax, btDispatcher* dispatcher)
{
	BTVECTOR3_IN(aabbMin);
	BTVECTOR3_IN(aabbMax);
	obj->updateHandle(handle, BTVECTOR3_USE(aabbMin), BTVECTOR3_USE(aabbMax), dispatcher);
}


bt32BitAxisSweep3* bt32BitAxisSweep3_new(const btVector3* worldAabbMin, const btVector3* worldAabbMax)
{
	BTVECTOR3_IN(worldAabbMin);
	BTVECTOR3_IN(worldAabbMax);
	return new bt32BitAxisSweep3(BTVECTOR3_USE(worldAabbMin), BTVECTOR3_USE(worldAabbMax));
}

bt32BitAxisSweep3* bt32BitAxisSweep3_new2(const btVector3* worldAabbMin, const btVector3* worldAabbMax,
	unsigned int maxHandles)
{
	BTVECTOR3_IN(worldAabbMin);
	BTVECTOR3_IN(worldAabbMax);
	return new bt32BitAxisSweep3(BTVECTOR3_USE(worldAabbMin), BTVECTOR3_USE(worldAabbMax),
		maxHandles);
}

bt32BitAxisSweep3* bt32BitAxisSweep3_new3(const btVector3* worldAabbMin, const btVector3* worldAabbMax,
	unsigned int maxHandles, btOverlappingPairCache* pairCache)
{
	BTVECTOR3_IN(worldAabbMin);
	BTVECTOR3_IN(worldAabbMax);
	return new bt32BitAxisSweep3(BTVECTOR3_USE(worldAabbMin), BTVECTOR3_USE(worldAabbMax),
		maxHandles, pairCache);
}

bt32BitAxisSweep3* bt32BitAxisSweep3_new4(const btVector3* worldAabbMin, const btVector3* worldAabbMax,
	unsigned int maxHandles, btOverlappingPairCache* pairCache, bool disableRaycastAccelerator)
{
	BTVECTOR3_IN(worldAabbMin);
	BTVECTOR3_IN(worldAabbMax);
	return new bt32BitAxisSweep3(BTVECTOR3_USE(worldAabbMin), BTVECTOR3_USE(worldAabbMax),
		maxHandles, pairCache, disableRaycastAccelerator);
}

unsigned int bt32BitAxisSweep3_addHandle(bt32BitAxisSweep3* obj, const btVector3* aabbMin,
	const btVector3* aabbMax, void* pOwner, short collisionFilterGroup, short collisionFilterMask,
	btDispatcher* dispatcher, void* multiSapProxy)
{
	BTVECTOR3_IN(aabbMin);
	BTVECTOR3_IN(aabbMax);
	return obj->addHandle(BTVECTOR3_USE(aabbMin), BTVECTOR3_USE(aabbMax), pOwner,
		collisionFilterGroup, collisionFilterMask, dispatcher, multiSapProxy);
}

bt32BitAxisSweep3_Handle* bt32BitAxisSweep3_getHandle(bt32BitAxisSweep3* obj,
	unsigned int index)
{
	return obj->getHandle(index);
}

unsigned int bt32BitAxisSweep3_getNumHandles(bt32BitAxisSweep3* obj)
{
	return obj->getNumHandles();
}

btOverlappingPairCallback* bt32BitAxisSweep3_getOverlappingPairUserCallback(
	bt32BitAxisSweep3* obj)
{
	return const_cast<btOverlappingPairCallback*>(obj->getOverlappingPairUserCallback());
}
/*
void bt32BitAxisSweep3_processAllOverlappingPairs(bt32BitAxisSweep3* obj, btOverlapCallback* callback)
{
	obj->processAllOverlappingPairs(callback);
}
*/
void bt32BitAxisSweep3_quantize(bt32BitAxisSweep3* obj, unsigned int* out, const btVector3* point,
	int isMax)
{
	BTVECTOR3_IN(point);
	obj->quantize(out, BTVECTOR3_USE(point), isMax);
}

void bt32BitAxisSweep3_removeHandle(bt32BitAxisSweep3* obj, unsigned int handle,
	btDispatcher* dispatcher)
{
	obj->removeHandle(handle, dispatcher);
}

void bt32BitAxisSweep3_setOverlappingPairUserCallback(bt32BitAxisSweep3* obj, btOverlappingPairCallback* pairCallback)
{
	obj->setOverlappingPairUserCallback(pairCallback);
}

bool bt32BitAxisSweep3_testAabbOverlap(bt32BitAxisSweep3* obj, btBroadphaseProxy* proxy0,
	btBroadphaseProxy* proxy1)
{
	return obj->testAabbOverlap(proxy0, proxy1);
}

void bt32BitAxisSweep3_unQuantize(bt32BitAxisSweep3* obj, btBroadphaseProxy* proxy,
	btVector3* aabbMin, btVector3* aabbMax)
{
	BTVECTOR3_DEF(aabbMin);
	BTVECTOR3_DEF(aabbMax);
	obj->unQuantize(proxy, BTVECTOR3_USE(aabbMin), BTVECTOR3_USE(aabbMax));
	BTVECTOR3_DEF_OUT(aabbMin);
	BTVECTOR3_DEF_OUT(aabbMax);
}

void bt32BitAxisSweep3_updateHandle(bt32BitAxisSweep3* obj, unsigned int handle,
	const btVector3* aabbMin, const btVector3* aabbMax, btDispatcher* dispatcher)
{
	BTVECTOR3_IN(aabbMin);
	BTVECTOR3_IN(aabbMax);
	obj->updateHandle(handle, BTVECTOR3_USE(aabbMin), BTVECTOR3_USE(aabbMax), dispatcher);
}
