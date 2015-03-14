#include <BulletCollision/CollisionDispatch/btGhostObject.h>
#include <BulletCollision/CollisionShapes/btConvexShape.h>

#include "conversion.h"
#include "btGhostObject_wrap.h"

btGhostObject* btGhostObject_new()
{
	return new btGhostObject();
}

void btGhostObject_addOverlappingObjectInternal(btGhostObject* obj, btBroadphaseProxy* otherProxy)
{
	obj->addOverlappingObjectInternal(otherProxy);
}

void btGhostObject_addOverlappingObjectInternal2(btGhostObject* obj, btBroadphaseProxy* otherProxy, btBroadphaseProxy* thisProxy)
{
	obj->addOverlappingObjectInternal(otherProxy, thisProxy);
}

void btGhostObject_convexSweepTest(btGhostObject* obj, const btConvexShape* castShape, const btScalar* convexFromWorld, const btScalar* convexToWorld, btCollisionWorld::ConvexResultCallback* resultCallback)
{
	TRANSFORM_CONV(convexFromWorld);
	TRANSFORM_CONV(convexToWorld);
	obj->convexSweepTest(castShape, TRANSFORM_USE(convexFromWorld), TRANSFORM_USE(convexToWorld), *resultCallback);
}

void btGhostObject_convexSweepTest2(btGhostObject* obj, const btConvexShape* castShape, const btScalar* convexFromWorld, const btScalar* convexToWorld, btCollisionWorld::ConvexResultCallback* resultCallback, btScalar allowedCcdPenetration)
{
	TRANSFORM_CONV(convexFromWorld);
	TRANSFORM_CONV(convexToWorld);
	obj->convexSweepTest(castShape, TRANSFORM_USE(convexFromWorld), TRANSFORM_USE(convexToWorld), *resultCallback, allowedCcdPenetration);
}

int btGhostObject_getNumOverlappingObjects(btGhostObject* obj)
{
	return obj->getNumOverlappingObjects();
}

btCollisionObject* btGhostObject_getOverlappingObject(btGhostObject* obj, int index)
{
	return obj->getOverlappingObject(index);
}

btAlignedCollisionObjectArray* btGhostObject_getOverlappingPairs(btGhostObject* obj)
{
	return &obj->getOverlappingPairs();
}

void btGhostObject_rayTest(btGhostObject* obj, const btScalar* rayFromWorld, const btScalar* rayToWorld, btCollisionWorld::RayResultCallback* resultCallback)
{
	VECTOR3_CONV(rayFromWorld);
	VECTOR3_CONV(rayToWorld);
	obj->rayTest(VECTOR3_USE(rayFromWorld), VECTOR3_USE(rayToWorld), *resultCallback);
}

void btGhostObject_removeOverlappingObjectInternal(btGhostObject* obj, btBroadphaseProxy* otherProxy, btDispatcher* dispatcher)
{
	obj->removeOverlappingObjectInternal(otherProxy, dispatcher);
}

void btGhostObject_removeOverlappingObjectInternal2(btGhostObject* obj, btBroadphaseProxy* otherProxy, btDispatcher* dispatcher, btBroadphaseProxy* thisProxy)
{
	obj->removeOverlappingObjectInternal(otherProxy, dispatcher, thisProxy);
}

btGhostObject* btGhostObject_upcast(btCollisionObject* colObj)
{
	return btGhostObject::upcast(colObj);
}


btPairCachingGhostObject* btPairCachingGhostObject_new()
{
	return new btPairCachingGhostObject();
}

btHashedOverlappingPairCache* btPairCachingGhostObject_getOverlappingPairCache(btPairCachingGhostObject* obj)
{
	return obj->getOverlappingPairCache();
}


btGhostPairCallback* btGhostPairCallback_new()
{
	return new btGhostPairCallback();
}
