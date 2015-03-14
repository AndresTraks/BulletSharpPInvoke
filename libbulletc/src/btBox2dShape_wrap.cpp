#include <BulletCollision/CollisionShapes/btBox2dShape.h>

#include "conversion.h"
#include "btBox2dShape_wrap.h"

btBox2dShape* btBox2dShape_new(const btScalar* boxHalfExtents)
{
	VECTOR3_CONV(boxHalfExtents);
	return new btBox2dShape(VECTOR3_USE(boxHalfExtents));
}

btBox2dShape* btBox2dShape_new2(btScalar boxHalfExtent)
{
	return new btBox2dShape(btVector3(boxHalfExtent, boxHalfExtent, boxHalfExtent));
}

btBox2dShape* btBox2dShape_new3(btScalar boxHalfExtentX, btScalar boxHalfExtentY, btScalar boxHalfExtentZ)
{
	return new btBox2dShape(btVector3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ));
}

void btBox2dShape_getCentroid(btBox2dShape* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getCentroid(), value);
}

void btBox2dShape_getHalfExtentsWithMargin(btBox2dShape* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getHalfExtentsWithMargin(), value);
}

void btBox2dShape_getHalfExtentsWithoutMargin(btBox2dShape* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getHalfExtentsWithoutMargin(), value);
}

const btVector3* btBox2dShape_getNormals(btBox2dShape* obj)
{
	return obj->getNormals();
}

void btBox2dShape_getPlaneEquation(btBox2dShape* obj, btScalar* plane, int i)
{
	VECTOR4_DEF(plane);
	obj->getPlaneEquation(VECTOR4_USE(plane), i);
	VECTOR4_DEF_OUT(plane);
}

int btBox2dShape_getVertexCount(btBox2dShape* obj)
{
	return obj->getVertexCount();
}

const btVector3* btBox2dShape_getVertices(btBox2dShape* obj)
{
	return obj->getVertices();
}
