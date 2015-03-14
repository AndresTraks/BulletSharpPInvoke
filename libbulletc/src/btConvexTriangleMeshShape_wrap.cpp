#include <BulletCollision/CollisionShapes/btConvexTriangleMeshShape.h>
#include <BulletCollision/CollisionShapes/btStridingMeshInterface.h>

#include "conversion.h"
#include "btConvexTriangleMeshShape_wrap.h"

btConvexTriangleMeshShape* btConvexTriangleMeshShape_new(btStridingMeshInterface* meshInterface)
{
	return new btConvexTriangleMeshShape(meshInterface);
}

btConvexTriangleMeshShape* btConvexTriangleMeshShape_new2(btStridingMeshInterface* meshInterface, bool calcAabb)
{
	return new btConvexTriangleMeshShape(meshInterface, calcAabb);
}

void btConvexTriangleMeshShape_calculatePrincipalAxisTransform(btConvexTriangleMeshShape* obj, btScalar* principal, btScalar* inertia, btScalar* volume)
{
	TRANSFORM_CONV(principal);
	VECTOR3_CONV(inertia);
	obj->calculatePrincipalAxisTransform(TRANSFORM_USE(principal), VECTOR3_USE(inertia), *volume);
	TRANSFORM_DEF_OUT(principal);
	VECTOR3_DEF_OUT(inertia);
}

const btStridingMeshInterface* btConvexTriangleMeshShape_getMeshInterface(btConvexTriangleMeshShape* obj)
{
	return obj->getMeshInterface();
}
