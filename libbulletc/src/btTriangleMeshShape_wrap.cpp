#include <BulletCollision/CollisionShapes/btTriangleMeshShape.h>

#include "conversion.h"
#include "btTriangleMeshShape_wrap.h"

void btTriangleMeshShape_getLocalAabbMax(btTriangleMeshShape* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getLocalAabbMax(), value);
}

void btTriangleMeshShape_getLocalAabbMin(btTriangleMeshShape* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getLocalAabbMin(), value);
}

btStridingMeshInterface* btTriangleMeshShape_getMeshInterface(btTriangleMeshShape* obj)
{
	return obj->getMeshInterface();
}

void btTriangleMeshShape_localGetSupportingVertex(btTriangleMeshShape* obj, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localGetSupportingVertex(VECTOR3_USE(vec)), value);
}

void btTriangleMeshShape_localGetSupportingVertexWithoutMargin(btTriangleMeshShape* obj, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localGetSupportingVertexWithoutMargin(VECTOR3_USE(vec)), value);
}

void btTriangleMeshShape_recalcLocalAabb(btTriangleMeshShape* obj)
{
	obj->recalcLocalAabb();
}
