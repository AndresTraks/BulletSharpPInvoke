#include <BulletCollision/CollisionShapes/btPolyhedralConvexShape.h>

#include "conversion.h"
#include "btPolyhedralConvexShape_wrap.h"

const btConvexPolyhedron* btPolyhedralConvexShape_getConvexPolyhedron(btPolyhedralConvexShape* obj)
{
	return obj->getConvexPolyhedron();
}

void btPolyhedralConvexShape_getEdge(btPolyhedralConvexShape* obj, int i, btScalar* pa, btScalar* pb)
{
	VECTOR3_DEF(pa);
	VECTOR3_DEF(pb);
	obj->getEdge(i, VECTOR3_USE(pa), VECTOR3_USE(pb));
	VECTOR3_DEF_OUT(pa);
	VECTOR3_DEF_OUT(pb);
}

int btPolyhedralConvexShape_getNumEdges(btPolyhedralConvexShape* obj)
{
	return obj->getNumEdges();
}

int btPolyhedralConvexShape_getNumPlanes(btPolyhedralConvexShape* obj)
{
	return obj->getNumPlanes();
}

int btPolyhedralConvexShape_getNumVertices(btPolyhedralConvexShape* obj)
{
	return obj->getNumVertices();
}

void btPolyhedralConvexShape_getPlane(btPolyhedralConvexShape* obj, btScalar* planeNormal, btScalar* planeSupport, int i)
{
	VECTOR3_DEF(planeNormal);
	VECTOR3_DEF(planeSupport);
	obj->getPlane(VECTOR3_USE(planeNormal), VECTOR3_USE(planeSupport), i);
	VECTOR3_DEF_OUT(planeNormal);
	VECTOR3_DEF_OUT(planeSupport);
}

void btPolyhedralConvexShape_getVertex(btPolyhedralConvexShape* obj, int i, btScalar* vtx)
{
	VECTOR3_DEF(vtx);
	obj->getVertex(i, VECTOR3_USE(vtx));
	VECTOR3_DEF_OUT(vtx);
}

bool btPolyhedralConvexShape_initializePolyhedralFeatures(btPolyhedralConvexShape* obj)
{
	return obj->initializePolyhedralFeatures();
}

bool btPolyhedralConvexShape_initializePolyhedralFeatures2(btPolyhedralConvexShape* obj, int shiftVerticesByMargin)
{
	return obj->initializePolyhedralFeatures(shiftVerticesByMargin);
}

bool btPolyhedralConvexShape_isInside(btPolyhedralConvexShape* obj, const btScalar* pt, btScalar tolerance)
{
	VECTOR3_CONV(pt);
	return obj->isInside(VECTOR3_USE(pt), tolerance);
}


void btPolyhedralConvexAabbCachingShape_getNonvirtualAabb(btPolyhedralConvexAabbCachingShape* obj, const btScalar* trans, btScalar* aabbMin, btScalar* aabbMax, btScalar margin)
{
	TRANSFORM_CONV(trans);
	VECTOR3_DEF(aabbMin);
	VECTOR3_DEF(aabbMax);
	obj->getNonvirtualAabb(TRANSFORM_USE(trans), VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax), margin);
	VECTOR3_DEF_OUT(aabbMin);
	VECTOR3_DEF_OUT(aabbMax);
}

void btPolyhedralConvexAabbCachingShape_recalcLocalAabb(btPolyhedralConvexAabbCachingShape* obj)
{
	obj->recalcLocalAabb();
}
