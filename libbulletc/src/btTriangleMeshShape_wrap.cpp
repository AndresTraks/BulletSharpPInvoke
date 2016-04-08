#include <BulletCollision/CollisionShapes/btTriangleMeshShape.h>

#include "conversion.h"
#include "btTriangleMeshShape_wrap.h"

void btTriangleMeshShape_getLocalAabbMax(btTriangleMeshShape* obj, btVector3* value)
{
	BTVECTOR3_COPY(value, &obj->getLocalAabbMax());
}

void btTriangleMeshShape_getLocalAabbMin(btTriangleMeshShape* obj, btVector3* value)
{
	BTVECTOR3_COPY(value, &obj->getLocalAabbMin());
}

btStridingMeshInterface* btTriangleMeshShape_getMeshInterface(btTriangleMeshShape* obj)
{
	return obj->getMeshInterface();
}

void btTriangleMeshShape_localGetSupportingVertex(btTriangleMeshShape* obj, const btVector3* vec,
	btVector3* value)
{
	BTVECTOR3_IN(vec);
	BTVECTOR3_SET(value, obj->localGetSupportingVertex(BTVECTOR3_USE(vec)));
}

void btTriangleMeshShape_localGetSupportingVertexWithoutMargin(btTriangleMeshShape* obj,
	const btVector3* vec, btVector3* value)
{
	BTVECTOR3_IN(vec);
	BTVECTOR3_SET(value, obj->localGetSupportingVertexWithoutMargin(BTVECTOR3_USE(vec)));
}

void btTriangleMeshShape_recalcLocalAabb(btTriangleMeshShape* obj)
{
	obj->recalcLocalAabb();
}
