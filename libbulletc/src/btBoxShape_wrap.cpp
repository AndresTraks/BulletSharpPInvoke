#include <BulletCollision/CollisionShapes/btBoxShape.h>

#include "conversion.h"
#include "btBoxShape_wrap.h"

btBoxShape* btBoxShape_new(const btScalar* boxHalfExtents)
{
	VECTOR3_CONV(boxHalfExtents);
	return new btBoxShape(VECTOR3_USE(boxHalfExtents));
}

btBoxShape* btBoxShape_new2(btScalar boxHalfExtent)
{
	return new btBoxShape(btVector3(boxHalfExtent, boxHalfExtent, boxHalfExtent));
}

btBoxShape* btBoxShape_new3(btScalar boxHalfExtentX, btScalar boxHalfExtentY, btScalar boxHalfExtentZ)
{
	return new btBoxShape(btVector3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ));
}

void btBoxShape_getHalfExtentsWithMargin(btBoxShape* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getHalfExtentsWithMargin(), value);
}

void btBoxShape_getHalfExtentsWithoutMargin(btBoxShape* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getHalfExtentsWithoutMargin(), value);
}

void btBoxShape_getPlaneEquation(btBoxShape* obj, btScalar* plane, int i)
{
	VECTOR4_DEF(plane);
	obj->getPlaneEquation(VECTOR4_USE(plane), i);
	VECTOR4_DEF_OUT(plane);
}
