#include <BulletCollision/NarrowPhaseCollision/btConvexCast.h>

#include "conversion.h"
#include "btConvexCast_wrap.h"

btConvexCast::CastResult* btConvexCast_CastResult_new()
{
	return new btConvexCast::CastResult();
}

void btConvexCast_CastResult_DebugDraw(btConvexCast::CastResult* obj, btScalar fraction)
{
	obj->DebugDraw(fraction);
}

void btConvexCast_CastResult_drawCoordSystem(btConvexCast::CastResult* obj, const btScalar* trans)
{
	TRANSFORM_CONV(trans);
	obj->drawCoordSystem(TRANSFORM_USE(trans));
}

btScalar btConvexCast_CastResult_getAllowedPenetration(btConvexCast::CastResult* obj)
{
	return obj->m_allowedPenetration;
}

btIDebugDraw* btConvexCast_CastResult_getDebugDrawer(btConvexCast::CastResult* obj)
{
	return obj->m_debugDrawer;
}

btScalar btConvexCast_CastResult_getFraction(btConvexCast::CastResult* obj)
{
	return obj->m_fraction;
}

void btConvexCast_CastResult_getHitPoint(btConvexCast::CastResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitPoint, value);
}

void btConvexCast_CastResult_getHitTransformA(btConvexCast::CastResult* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->m_hitTransformA, value);
}

void btConvexCast_CastResult_getHitTransformB(btConvexCast::CastResult* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->m_hitTransformB, value);
}

void btConvexCast_CastResult_getNormal(btConvexCast::CastResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_normal, value);
}

void btConvexCast_CastResult_reportFailure(btConvexCast::CastResult* obj, int errNo, int numIterations)
{
	obj->reportFailure(errNo, numIterations);
}

void btConvexCast_CastResult_setAllowedPenetration(btConvexCast::CastResult* obj, btScalar value)
{
	obj->m_allowedPenetration = value;
}

void btConvexCast_CastResult_setDebugDrawer(btConvexCast::CastResult* obj, btIDebugDraw* value)
{
	obj->m_debugDrawer = value;
}

void btConvexCast_CastResult_setFraction(btConvexCast::CastResult* obj, btScalar value)
{
	obj->m_fraction = value;
}

void btConvexCast_CastResult_setHitPoint(btConvexCast::CastResult* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitPoint);
}

void btConvexCast_CastResult_setHitTransformA(btConvexCast::CastResult* obj, const btScalar* value)
{
	TRANSFORM_IN(value, &obj->m_hitTransformA);
}

void btConvexCast_CastResult_setHitTransformB(btConvexCast::CastResult* obj, const btScalar* value)
{
	TRANSFORM_IN(value, &obj->m_hitTransformB);
}

void btConvexCast_CastResult_setNormal(btConvexCast::CastResult* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_normal);
}

void btConvexCast_CastResult_delete(btConvexCast::CastResult* obj)
{
	delete obj;
}


bool btConvexCast_calcTimeOfImpact(btConvexCast* obj, const btScalar* fromA, const btScalar* toA, const btScalar* fromB, const btScalar* toB, btConvexCast::CastResult* result)
{
	TRANSFORM_CONV(fromA);
	TRANSFORM_CONV(toA);
	TRANSFORM_CONV(fromB);
	TRANSFORM_CONV(toB);
	return obj->calcTimeOfImpact(TRANSFORM_USE(fromA), TRANSFORM_USE(toA), TRANSFORM_USE(fromB), TRANSFORM_USE(toB), *result);
}

void btConvexCast_delete(btConvexCast* obj)
{
	delete obj;
}
