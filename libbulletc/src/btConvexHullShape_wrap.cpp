#include <BulletCollision/CollisionShapes/btConvexHullShape.h>

#include "conversion.h"
#include "btConvexHullShape_wrap.h"

btConvexHullShape* btConvexHullShape_new()
{
	return new btConvexHullShape();
}

btConvexHullShape* btConvexHullShape_new2(const btScalar* points)
{
	return new btConvexHullShape(points);
}

btConvexHullShape* btConvexHullShape_new3(const btScalar* points, int numPoints)
{
	return new btConvexHullShape(points, numPoints);
}

btConvexHullShape* btConvexHullShape_new4(const btScalar* points, int numPoints, int stride)
{
	return new btConvexHullShape(points, numPoints, stride);
}

void btConvexHullShape_addPoint(btConvexHullShape* obj, const btScalar* point)
{
	VECTOR3_CONV(point);
	obj->addPoint(VECTOR3_USE(point));
}

void btConvexHullShape_addPoint2(btConvexHullShape* obj, const btScalar* point, bool recalculateLocalAabb)
{
	VECTOR3_CONV(point);
	obj->addPoint(VECTOR3_USE(point), recalculateLocalAabb);
}

int btConvexHullShape_getNumPoints(btConvexHullShape* obj)
{
	return obj->getNumPoints();
}

const btVector3* btConvexHullShape_getPoints(btConvexHullShape* obj)
{
	return obj->getPoints();
}

void btConvexHullShape_getScaledPoint(btConvexHullShape* obj, int i, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getScaledPoint(i), value);
}

const btVector3* btConvexHullShape_getUnscaledPoints(btConvexHullShape* obj)
{
	return obj->getUnscaledPoints();
}

void btConvexHullShape_project(btConvexHullShape* obj, const btScalar* trans, const btScalar* dir, btScalar* minProj, btScalar* maxProj, btScalar* witnesPtMin, btScalar* witnesPtMax)
{
	TRANSFORM_CONV(trans);
	VECTOR3_CONV(dir);
	VECTOR3_DEF(witnesPtMin);
	VECTOR3_DEF(witnesPtMax);
	obj->project(TRANSFORM_USE(trans), VECTOR3_USE(dir), *minProj, *maxProj, VECTOR3_USE(witnesPtMin), VECTOR3_USE(witnesPtMax));
	VECTOR3_DEF_OUT(witnesPtMin);
	VECTOR3_DEF_OUT(witnesPtMax);
}
