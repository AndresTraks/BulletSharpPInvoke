#include <BulletCollision/CollisionShapes/btConvexPointCloudShape.h>

#include "conversion.h"
#include "btConvexPointCloudShape_wrap.h"

btConvexPointCloudShape* btConvexPointCloudShape_new()
{
	return new btConvexPointCloudShape();
}

btConvexPointCloudShape* btConvexPointCloudShape_new2(btVector3* points, int numPoints, const btScalar* localScaling)
{
	VECTOR3_CONV(localScaling);
	return new btConvexPointCloudShape(points, numPoints, VECTOR3_USE(localScaling));
}

btConvexPointCloudShape* btConvexPointCloudShape_new3(btVector3* points, int numPoints, const btScalar* localScaling, bool computeAabb)
{
	VECTOR3_CONV(localScaling);
	return new btConvexPointCloudShape(points, numPoints, VECTOR3_USE(localScaling), computeAabb);
}

int btConvexPointCloudShape_getNumPoints(btConvexPointCloudShape* obj)
{
	return obj->getNumPoints();
}

void btConvexPointCloudShape_getScaledPoint(btConvexPointCloudShape* obj, int index, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getScaledPoint(index), value);
}

btVector3* btConvexPointCloudShape_getUnscaledPoints(btConvexPointCloudShape* obj)
{
	return obj->getUnscaledPoints();
}

void btConvexPointCloudShape_setPoints(btConvexPointCloudShape* obj, btVector3* points, int numPoints)
{
	obj->setPoints(points, numPoints);
}

void btConvexPointCloudShape_setPoints2(btConvexPointCloudShape* obj, btVector3* points, int numPoints, bool computeAabb)
{
	obj->setPoints(points, numPoints, computeAabb);
}

void btConvexPointCloudShape_setPoints3(btConvexPointCloudShape* obj, btVector3* points, int numPoints, bool computeAabb, const btScalar* localScaling)
{
	VECTOR3_CONV(localScaling);
	obj->setPoints(points, numPoints, computeAabb, VECTOR3_USE(localScaling));
}
