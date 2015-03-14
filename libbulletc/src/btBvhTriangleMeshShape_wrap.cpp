#include <BulletCollision/CollisionShapes/btBvhTriangleMeshShape.h>

#include "conversion.h"
#include "btBvhTriangleMeshShape_wrap.h"

btBvhTriangleMeshShape* btBvhTriangleMeshShape_new(btStridingMeshInterface* meshInterface, bool useQuantizedAabbCompression)
{
	return new btBvhTriangleMeshShape(meshInterface, useQuantizedAabbCompression);
}

btBvhTriangleMeshShape* btBvhTriangleMeshShape_new2(btStridingMeshInterface* meshInterface, bool useQuantizedAabbCompression, bool buildBvh)
{
	return new btBvhTriangleMeshShape(meshInterface, useQuantizedAabbCompression, buildBvh);
}

btBvhTriangleMeshShape* btBvhTriangleMeshShape_new3(btStridingMeshInterface* meshInterface, bool useQuantizedAabbCompression, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	return new btBvhTriangleMeshShape(meshInterface, useQuantizedAabbCompression, VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax));
}

btBvhTriangleMeshShape* btBvhTriangleMeshShape_new4(btStridingMeshInterface* meshInterface, bool useQuantizedAabbCompression, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax, bool buildBvh)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	return new btBvhTriangleMeshShape(meshInterface, useQuantizedAabbCompression, VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax), buildBvh);
}

void btBvhTriangleMeshShape_buildOptimizedBvh(btBvhTriangleMeshShape* obj)
{
	obj->buildOptimizedBvh();
}

btOptimizedBvh* btBvhTriangleMeshShape_getOptimizedBvh(btBvhTriangleMeshShape* obj)
{
	return obj->getOptimizedBvh();
}

bool btBvhTriangleMeshShape_getOwnsBvh(btBvhTriangleMeshShape* obj)
{
	return obj->getOwnsBvh();
}

btTriangleInfoMap* btBvhTriangleMeshShape_getTriangleInfoMap(btBvhTriangleMeshShape* obj)
{
	return obj->getTriangleInfoMap();
}

void btBvhTriangleMeshShape_partialRefitTree(btBvhTriangleMeshShape* obj, const btScalar* aabbMin, const btScalar* aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->partialRefitTree(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}

void btBvhTriangleMeshShape_performConvexcast(btBvhTriangleMeshShape* obj, btTriangleCallback* callback, const btScalar* boxSource, const btScalar* boxTarget, const btScalar* boxMin, const btScalar* boxMax)
{
	VECTOR3_CONV(boxSource);
	VECTOR3_CONV(boxTarget);
	VECTOR3_CONV(boxMin);
	VECTOR3_CONV(boxMax);
	obj->performConvexcast(callback, VECTOR3_USE(boxSource), VECTOR3_USE(boxTarget), VECTOR3_USE(boxMin), VECTOR3_USE(boxMax));
}

void btBvhTriangleMeshShape_performRaycast(btBvhTriangleMeshShape* obj, btTriangleCallback* callback, const btScalar* raySource, const btScalar* rayTarget)
{
	VECTOR3_CONV(raySource);
	VECTOR3_CONV(rayTarget);
	obj->performRaycast(callback, VECTOR3_USE(raySource), VECTOR3_USE(rayTarget));
}

void btBvhTriangleMeshShape_refitTree(btBvhTriangleMeshShape* obj, const btScalar* aabbMin, const btScalar* aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->refitTree(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}

void btBvhTriangleMeshShape_serializeSingleBvh(btBvhTriangleMeshShape* obj, btSerializer* serializer)
{
	obj->serializeSingleBvh(serializer);
}

void btBvhTriangleMeshShape_serializeSingleTriangleInfoMap(btBvhTriangleMeshShape* obj, btSerializer* serializer)
{
	obj->serializeSingleTriangleInfoMap(serializer);
}

void btBvhTriangleMeshShape_setOptimizedBvh(btBvhTriangleMeshShape* obj, btOptimizedBvh* bvh)
{
	obj->setOptimizedBvh(bvh);
}

void btBvhTriangleMeshShape_setOptimizedBvh2(btBvhTriangleMeshShape* obj, btOptimizedBvh* bvh, const btScalar* localScaling)
{
	VECTOR3_CONV(localScaling);
	obj->setOptimizedBvh(bvh, VECTOR3_USE(localScaling));
}

void btBvhTriangleMeshShape_setTriangleInfoMap(btBvhTriangleMeshShape* obj, btTriangleInfoMap* triangleInfoMap)
{
	obj->setTriangleInfoMap(triangleInfoMap);
}

bool btBvhTriangleMeshShape_usesQuantizedAabbCompression(btBvhTriangleMeshShape* obj)
{
	return obj->usesQuantizedAabbCompression();
}
