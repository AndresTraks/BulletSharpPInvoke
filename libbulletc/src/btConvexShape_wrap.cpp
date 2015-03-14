#include <BulletCollision/CollisionShapes/btConvexShape.h>

#include "conversion.h"
#include "btConvexShape_wrap.h"

void btConvexShape_batchedUnitVectorGetSupportingVertexWithoutMargin(btConvexShape* obj, const btVector3* vectors, btVector3* supportVerticesOut, int numVectors)
{
	obj->batchedUnitVectorGetSupportingVertexWithoutMargin(vectors, supportVerticesOut, numVectors);
}

void btConvexShape_getAabbNonVirtual(btConvexShape* obj, const btScalar* t, btScalar* aabbMin, btScalar* aabbMax)
{
	TRANSFORM_CONV(t);
	VECTOR3_DEF(aabbMin);
	VECTOR3_DEF(aabbMax);
	obj->getAabbNonVirtual(TRANSFORM_USE(t), VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEF_OUT(aabbMin);
	VECTOR3_DEF_OUT(aabbMax);
}

void btConvexShape_getAabbSlow(btConvexShape* obj, const btScalar* t, btScalar* aabbMin, btScalar* aabbMax)
{
	TRANSFORM_CONV(t);
	VECTOR3_DEF(aabbMin);
	VECTOR3_DEF(aabbMax);
	obj->getAabbSlow(TRANSFORM_USE(t), VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEF_OUT(aabbMin);
	VECTOR3_DEF_OUT(aabbMax);
}

btScalar btConvexShape_getMarginNonVirtual(btConvexShape* obj)
{
	return obj->getMarginNonVirtual();
}

int btConvexShape_getNumPreferredPenetrationDirections(btConvexShape* obj)
{
	return obj->getNumPreferredPenetrationDirections();
}

void btConvexShape_getPreferredPenetrationDirection(btConvexShape* obj, int index, btScalar* penetrationVector)
{
	VECTOR3_DEF(penetrationVector);
	obj->getPreferredPenetrationDirection(index, VECTOR3_USE(penetrationVector));
	VECTOR3_DEF_OUT(penetrationVector);
}

void btConvexShape_localGetSupportingVertex(btConvexShape* obj, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localGetSupportingVertex(VECTOR3_USE(vec)), value);
}

void btConvexShape_localGetSupportingVertexWithoutMargin(btConvexShape* obj, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localGetSupportingVertexWithoutMargin(VECTOR3_USE(vec)), value);
}

void btConvexShape_localGetSupportVertexNonVirtual(btConvexShape* obj, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localGetSupportVertexNonVirtual(VECTOR3_USE(vec)), value);
}

void btConvexShape_localGetSupportVertexWithoutMarginNonVirtual(btConvexShape* obj, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localGetSupportVertexWithoutMarginNonVirtual(VECTOR3_USE(vec)), value);
}

void btConvexShape_project(btConvexShape* obj, const btScalar* trans, const btScalar* dir, btScalar* min, btScalar* max)
{
	TRANSFORM_CONV(trans);
	VECTOR3_CONV(dir);
	obj->project(TRANSFORM_USE(trans), VECTOR3_USE(dir), *min, *max);
}
