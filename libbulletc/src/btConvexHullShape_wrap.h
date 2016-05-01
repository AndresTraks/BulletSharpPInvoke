#include "main.h"

extern "C"
{
	EXPORT btConvexHullShape* btConvexHullShape_new();
	EXPORT btConvexHullShape* btConvexHullShape_new2(const btScalar* points);
	EXPORT btConvexHullShape* btConvexHullShape_new3(const btScalar* points, int numPoints);
	EXPORT btConvexHullShape* btConvexHullShape_new4(const btScalar* points, int numPoints, int stride);
	EXPORT void btConvexHullShape_addPoint(btConvexHullShape* obj, const btVector3* point);
	EXPORT void btConvexHullShape_addPoint2(btConvexHullShape* obj, const btVector3* point, bool recalculateLocalAabb);
	EXPORT int btConvexHullShape_getNumPoints(btConvexHullShape* obj);
	EXPORT void btConvexHullShape_getScaledPoint(btConvexHullShape* obj, int i, btVector3* value);
	EXPORT btVector3* btConvexHullShape_getUnscaledPoints(btConvexHullShape* obj);
}
