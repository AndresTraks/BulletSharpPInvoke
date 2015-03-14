#include <BulletCollision/CollisionShapes/btTriangleShape.h>

#include "conversion.h"
#include "btTriangleShape_wrap.h"

btTriangleShape* btTriangleShape_new()
{
	return new btTriangleShape();
}

btTriangleShape* btTriangleShape_new2(const btScalar* p0, const btScalar* p1, const btScalar* p2)
{
	VECTOR3_CONV(p0);
	VECTOR3_CONV(p1);
	VECTOR3_CONV(p2);
	return new btTriangleShape(VECTOR3_USE(p0), VECTOR3_USE(p1), VECTOR3_USE(p2));
}

void btTriangleShape_calcNormal(btTriangleShape* obj, btScalar* normal)
{
	VECTOR3_DEF(normal);
	obj->calcNormal(VECTOR3_USE(normal));
	VECTOR3_DEF_OUT(normal);
}

void btTriangleShape_getPlaneEquation(btTriangleShape* obj, int i, btScalar* planeNormal, btScalar* planeSupport)
{
	VECTOR3_DEF(planeNormal);
	VECTOR3_DEF(planeSupport);
	obj->getPlaneEquation(i, VECTOR3_USE(planeNormal), VECTOR3_USE(planeSupport));
	VECTOR3_DEF_OUT(planeNormal);
	VECTOR3_DEF_OUT(planeSupport);
}

const btScalar* btTriangleShape_getVertexPtr(btTriangleShape* obj, int index)
{
	return obj->getVertexPtr(index);
}

btVector3* btTriangleShape_getVertices1(btTriangleShape* obj)
{
	return obj->m_vertices1;
}
