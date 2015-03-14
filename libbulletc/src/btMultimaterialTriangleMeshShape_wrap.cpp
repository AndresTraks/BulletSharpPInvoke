#include <BulletCollision/CollisionShapes/btMultimaterialTriangleMeshShape.h>

#include "conversion.h"
#include "btMultimaterialTriangleMeshShape_wrap.h"

btMultimaterialTriangleMeshShape* btMultimaterialTriangleMeshShape_new(btStridingMeshInterface* meshInterface, bool useQuantizedAabbCompression)
{
	return new btMultimaterialTriangleMeshShape(meshInterface, useQuantizedAabbCompression);
}

btMultimaterialTriangleMeshShape* btMultimaterialTriangleMeshShape_new2(btStridingMeshInterface* meshInterface, bool useQuantizedAabbCompression, bool buildBvh)
{
	return new btMultimaterialTriangleMeshShape(meshInterface, useQuantizedAabbCompression, buildBvh);
}

btMultimaterialTriangleMeshShape* btMultimaterialTriangleMeshShape_new3(btStridingMeshInterface* meshInterface, bool useQuantizedAabbCompression, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	return new btMultimaterialTriangleMeshShape(meshInterface, useQuantizedAabbCompression, VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax));
}

btMultimaterialTriangleMeshShape* btMultimaterialTriangleMeshShape_new4(btStridingMeshInterface* meshInterface, bool useQuantizedAabbCompression, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax, bool buildBvh)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	return new btMultimaterialTriangleMeshShape(meshInterface, useQuantizedAabbCompression, VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax), buildBvh);
}

const btMaterial* btMultimaterialTriangleMeshShape_getMaterialProperties(btMultimaterialTriangleMeshShape* obj, int partID, int triIndex)
{
	return obj->getMaterialProperties(partID, triIndex);
}
