#include <BulletCollision/CollisionShapes/btOptimizedBvh.h>
#include <BulletCollision/CollisionShapes/btStridingMeshInterface.h>

#include "conversion.h"
#include "btOptimizedBvh_wrap.h"

btOptimizedBvh* btOptimizedBvh_new()
{
	return new btOptimizedBvh();
}

void btOptimizedBvh_build(btOptimizedBvh* obj, btStridingMeshInterface* triangles, bool useQuantizedAabbCompression, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	obj->build(triangles, useQuantizedAabbCompression, VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax));
}

btOptimizedBvh* btOptimizedBvh_deSerializeInPlace(void* i_alignedDataBuffer, unsigned int i_dataBufferSize, bool i_swapEndian)
{
	return btOptimizedBvh::deSerializeInPlace(i_alignedDataBuffer, i_dataBufferSize, i_swapEndian);
}

void btOptimizedBvh_refit(btOptimizedBvh* obj, btStridingMeshInterface* triangles, const btScalar* aabbMin, const btScalar* aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->refit(triangles, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}

void btOptimizedBvh_refitPartial(btOptimizedBvh* obj, btStridingMeshInterface* triangles, const btScalar* aabbMin, const btScalar* aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->refitPartial(triangles, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}

bool btOptimizedBvh_serializeInPlace(btOptimizedBvh* obj, void* o_alignedDataBuffer, unsigned int i_dataBufferSize, bool i_swapEndian)
{
	return obj->serializeInPlace(o_alignedDataBuffer, i_dataBufferSize, i_swapEndian);
}

void btOptimizedBvh_updateBvhNodes(btOptimizedBvh* obj, btStridingMeshInterface* meshInterface, int firstNode, int endNode, int index)
{
	obj->updateBvhNodes(meshInterface, firstNode, endNode, index);
}
