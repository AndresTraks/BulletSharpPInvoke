#include <LinearMath/btGeometryUtil.h>

#include "conversion.h"
#include "btGeometryUtil_wrap.h"

bool btGeometryUtil_areVerticesBehindPlane(const btScalar* planeNormal, const btAlignedVector3Array* vertices, btScalar margin)
{
	VECTOR3_CONV(planeNormal);
	return btGeometryUtil::areVerticesBehindPlane(VECTOR3_USE(planeNormal), *vertices, margin);
}

void btGeometryUtil_getPlaneEquationsFromVertices(btAlignedVector3Array* vertices, btAlignedVector3Array* planeEquationsOut)
{
	btGeometryUtil::getPlaneEquationsFromVertices(*vertices, *planeEquationsOut);
}

void btGeometryUtil_getVerticesFromPlaneEquations(const btAlignedVector3Array* planeEquations, btAlignedVector3Array* verticesOut)
{
	btGeometryUtil::getVerticesFromPlaneEquations(*planeEquations, *verticesOut);
}
/*
bool btGeometryUtil_isInside(const btAlignedVector3Array* vertices, const btScalar* planeNormal, btScalar margin)
{
	VECTOR3_CONV(planeNormal);
	return btGeometryUtil::isInside(*vertices, VECTOR3_USE(planeNormal), margin);
}
*/
bool btGeometryUtil_isPointInsidePlanes(const btAlignedVector3Array* planeEquations, const btScalar* point, btScalar margin)
{
	VECTOR3_CONV(point);
	return btGeometryUtil::isPointInsidePlanes(*planeEquations, VECTOR3_USE(point), margin);
}
