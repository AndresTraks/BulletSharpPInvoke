#include <LinearMath/btIDebugDraw.h>

#include "conversion.h"
#include "btIDebugDraw_wrap.h"

btIDebugDrawWrapper::btIDebugDrawWrapper(void* debugDrawGCHandle, pIDebugDraw_DrawAabb drawAabbCallback, pIDebugDraw_DrawArc drawArcCallback, pIDebugDraw_DrawBox drawBoxCallback,
	pIDebugDraw_DrawCapsule drawCapsuleCallback, pIDebugDraw_DrawCone drawConeCallback, pIDebugDraw_DrawContactPoint drawContactPointCallback, pIDebugDraw_DrawCylinder drawCylinderCallback, pIDebugDraw_DrawLine drawLineCallback, pIDebugDraw_DrawPlane drawPlaneCallback,
	pIDebugDraw_DrawSphere drawSphereCallback, pIDebugDraw_DrawSpherePatch drawSpherePatchCallback, pIDebugDraw_DrawTransform drawTransformCallback, pIDebugDraw_DrawTriangle drawTriangleCallback, pIDebugDraw_GetDebugMode getDebugModeCallback, pSimpleCallback cb)
{
	_debugDrawGCHandle = debugDrawGCHandle;
	_drawAabbCallback = drawAabbCallback;
	_drawArcCallback = drawArcCallback;
	_drawBoxCallback = drawBoxCallback;
	_drawCapsuleCallback = drawCapsuleCallback;
	_drawConeCallback = drawConeCallback;
	_drawContactPointCallback = drawContactPointCallback;
	_drawCylinderCallback = drawCylinderCallback;
	_drawLineCallback = drawLineCallback;
	_drawPlaneCallback = drawPlaneCallback;
	_drawSphereCallback = drawSphereCallback;
	_drawSpherePatchCallback = drawSpherePatchCallback;
	_drawTransformCallback = drawTransformCallback;
	_drawTriangleCallback = drawTriangleCallback;
	_getDebugModeCallback = getDebugModeCallback;
	_cb = cb;
}

void btIDebugDrawWrapper::draw3dText(const btVector3& location, const char* textString)
{
	_cb(0);
	//_debugDraw->Draw3dText(Math::BtVector3ToVector3(&location), StringConv::UnmanagedToManaged(textString));
}

void btIDebugDrawWrapper::drawAabb(const btVector3& from, const btVector3& to, const btVector3& color)
{
	_drawAabbCallback(from, to, color);
}

void btIDebugDrawWrapper::drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
	btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
	const btVector3& color, bool drawSect, btScalar stepDegrees)
{
	_drawArcCallback(center, normal, axis, radiusA, radiusB, minAngle, maxAngle, color, drawSect, stepDegrees);
}

void btIDebugDrawWrapper::drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
	btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
	const btVector3& color, bool drawSect)
{
	_cb(1);
	//_debugDraw->DrawArc(Math::BtVector3ToVector3(&center), Math::BtVector3ToVector3(&normal), Math::BtVector3ToVector3(&axis),
		//radiusA, radiusB, minAngle, maxAngle, BtVectorToBtColor(color), drawSect);
}

void btIDebugDrawWrapper::drawBox(const btVector3& bbMin, const btVector3& bbMax, const btTransform& trans, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transTemp[16];
	btTransformToMatrix(&trans, transTemp);
	_drawBoxCallback(bbMin, bbMax, transTemp, color);
}

void btIDebugDrawWrapper::drawBox(const btVector3& bbMin, const btVector3& bbMax, const btVector3& color)
{
	_cb(2);
	//_debugDraw->DrawBox(
		//Math::BtVector3ToVector3(&bbMin), Math::BtVector3ToVector3(&bbMax),	BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawCapsule(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawCapsuleCallback(radius, halfHeight, upAxis, transformTemp, color);
}

void btIDebugDrawWrapper::drawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawConeCallback(radius, height, upAxis, transformTemp, color);
}

void btIDebugDrawWrapper::drawContactPoint(const btVector3& PointOnB, const btVector3& normalOnB, btScalar distance, int lifeTime, const btVector3& color)
{
	_drawContactPointCallback(PointOnB, normalOnB, distance, lifeTime, color);
}

void btIDebugDrawWrapper::drawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawCylinderCallback(radius, halfHeight, upAxis, transformTemp, color);
}

void btIDebugDrawWrapper::drawLine(const btVector3& from, const btVector3& to, const btVector3& color)
{
	_drawLineCallback(from, to, color);
}

void btIDebugDrawWrapper::drawPlane(const btVector3& planeNormal, btScalar planeConst, const btTransform& transform, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawPlaneCallback(planeNormal, planeConst, transformTemp, color);
}

void btIDebugDrawWrapper::drawSphere(const btVector3& p, btScalar radius, const btVector3& color)
{
	_cb(3);
	//_debugDraw->DrawSphere(Math::BtVector3ToVector3(&p), radius, BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawSphere(btScalar radius, const btTransform& transform, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawSphereCallback(radius, transformTemp, color);
}

void btIDebugDrawWrapper::drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
	btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color, btScalar stepDegrees)
{
	_drawSpherePatchCallback(center, up, axis, radius, minTh, maxTh, minPs, maxPs, color, stepDegrees);
}

void btIDebugDrawWrapper::drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
	btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color)
{
	_cb(4);
	//_debugDraw->DrawSpherePatch(Math::BtVector3ToVector3(&center), Math::BtVector3ToVector3(&up), Math::BtVector3ToVector3(&axis),
		//radius, minTh, maxTh, minPs, maxPs, BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawTransform(const btTransform& transform, btScalar orthoLen)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawTransformCallback(transformTemp, orthoLen);
}

void btIDebugDrawWrapper::drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar)
{
	_drawTriangleCallback(v0, v1, v2, color, 0);
}

void btIDebugDrawWrapper::drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
	const btVector3& n0, const btVector3& n1, const btVector3& n2, const btVector3& color, btScalar alpha)
{
	_cb(5);
	//_debugDraw->DrawTriangle(Math::BtVector3ToVector3(&v0), Math::BtVector3ToVector3(&v1), Math::BtVector3ToVector3(&v2),
		//Math::BtVector3ToVector3(&n0), Math::BtVector3ToVector3(&n1), Math::BtVector3ToVector3(&n2), BtVectorToBtColor(color), alpha);
}

void btIDebugDrawWrapper::baseDrawAabb(const btVector3& from, const btVector3& to, const btVector3& color)
{
	btIDebugDraw::drawAabb(from, to, color);
}

void btIDebugDrawWrapper::baseDrawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color)
{
	btIDebugDraw::drawCone(radius, height, upAxis, transform, color);
}

void btIDebugDrawWrapper::baseDrawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color)
{
	btIDebugDraw::drawCylinder(radius, halfHeight, upAxis, transform, color);
}

void btIDebugDrawWrapper::baseDrawSphere(const btVector3& p, btScalar radius, const btVector3& color)
{
	btIDebugDraw::drawSphere(p, radius, color);
}

void btIDebugDrawWrapper::baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar)
{
	btIDebugDraw::drawTriangle(v0, v1, v2, color, 0);
}

void btIDebugDrawWrapper::baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
	const btVector3& n0, const btVector3& n1, const btVector3& n2, const btVector3& color, btScalar alpha)
{
	btIDebugDraw::drawTriangle(v0, v1, v2, n0, n1, n2, color, alpha);
}

void btIDebugDrawWrapper::reportErrorWarning(const char* warningString)
{
	//_debugDraw->ReportErrorWarning(StringConv::UnmanagedToManaged(warningString));
}

void btIDebugDrawWrapper::setDebugMode(int debugMode)
{
	_cb(6);
	//_debugDraw->DebugMode = (BulletSharp::DebugDrawModes)debugMode;
}
int	btIDebugDrawWrapper::getDebugMode() const
{
	return _getDebugModeCallback();
}

btIDebugDrawWrapper* btIDebugDrawWrapper_new(void* debugDrawGCHandle, pIDebugDraw_DrawAabb drawAabbCallback, pIDebugDraw_DrawArc drawArcCallback, pIDebugDraw_DrawBox drawBoxCallback,
	pIDebugDraw_DrawCapsule drawCapsule, pIDebugDraw_DrawCone drawCone, pIDebugDraw_DrawContactPoint drawContactPointCallback, pIDebugDraw_DrawCylinder drawCylinderCallback, pIDebugDraw_DrawLine drawLineCallback,
	pIDebugDraw_DrawPlane drawPlaneCallback, pIDebugDraw_DrawSphere drawSphereCallback, pIDebugDraw_DrawSpherePatch drawSpherePatchCallback, pIDebugDraw_DrawTransform drawTransformCallback, pIDebugDraw_DrawTriangle drawTriangleCallback, pIDebugDraw_GetDebugMode getDebugModeCallback, pSimpleCallback cb)
{
	return new btIDebugDrawWrapper(debugDrawGCHandle, drawAabbCallback, drawArcCallback, drawBoxCallback, drawCapsule, drawCone, drawContactPointCallback, drawCylinderCallback, drawLineCallback, drawPlaneCallback, drawSphereCallback, drawSpherePatchCallback, drawTransformCallback, drawTriangleCallback, getDebugModeCallback, cb);
}

void* btIDebugDrawWrapper_getGCHandle(btIDebugDrawWrapper* obj)
{
	return obj->_debugDrawGCHandle;
}

void btIDebugDraw_delete(btIDebugDraw* obj)
{
	delete obj;
}
