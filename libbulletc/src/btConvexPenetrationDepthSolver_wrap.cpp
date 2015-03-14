#include <BulletCollision/CollisionShapes/btConvexShape.h>
#include <BulletCollision/NarrowPhaseCollision/btConvexPenetrationDepthSolver.h>
#include <LinearMath/btIDebugDraw.h>

#include "conversion.h"
#include "btConvexPenetrationDepthSolver_wrap.h"

bool btConvexPenetrationDepthSolver_calcPenDepth(btConvexPenetrationDepthSolver* obj, btVoronoiSimplexSolver* simplexSolver, const btConvexShape* convexA, const btConvexShape* convexB, const btScalar* transA, const btScalar* transB, btScalar* v, btScalar* pa, btScalar* pb, btIDebugDraw* debugDraw)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	VECTOR3_DEF(v);
	VECTOR3_DEF(pa);
	VECTOR3_DEF(pb);
	bool ret = obj->calcPenDepth(*simplexSolver, convexA, convexB, TRANSFORM_USE(transA), TRANSFORM_USE(transB), VECTOR3_USE(v), VECTOR3_USE(pa), VECTOR3_USE(pb), debugDraw);
	VECTOR3_DEF_OUT(v);
	VECTOR3_DEF_OUT(pa);
	VECTOR3_DEF_OUT(pb);
	return ret;
}

void btConvexPenetrationDepthSolver_delete(btConvexPenetrationDepthSolver* obj)
{
	delete obj;
}
