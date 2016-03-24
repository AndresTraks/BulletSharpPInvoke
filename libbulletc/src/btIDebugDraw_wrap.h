#include "main.h"

#ifndef BT_IDEBUG_DRAW__H
#define p_btIDebugDraw_DrawAabb void*
#define p_btIDebugDraw_DrawArc void*
#define p_btIDebugDraw_DrawBox void*
#define p_btIDebugDraw_DrawCapsule void*
#define p_btIDebugDraw_DrawCone void*
#define p_btIDebugDraw_DrawContactPoint void*
#define p_btIDebugDraw_DrawCylinder void*
#define p_btIDebugDraw_DrawLine void*
#define p_btIDebugDraw_DrawPlane void*
#define p_btIDebugDraw_DrawSphere void*
#define p_btIDebugDraw_DrawSpherePatch void*
#define p_btIDebugDraw_DrawTransform void*
#define p_btIDebugDraw_DrawTriangle void*
#define p_btIDebugDraw_GetDebugMode void*
#define pSimpleCallback void*

#define btIDebugDrawWrapper void
#else
typedef void (*p_btIDebugDraw_DrawAabb)(const btScalar* from, const btScalar* to,
	const btScalar* color);
typedef void (*p_btIDebugDraw_DrawArc)(const btScalar* center, const btScalar* normal,
	const btScalar* axis, btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
	const btScalar* color, bool drawSect, btScalar stepDegrees);
typedef void (*p_btIDebugDraw_DrawBox)(const btScalar* bbMin, const btScalar* bbMax,
	const btScalar* trans, const btScalar* color);
typedef void (*p_btIDebugDraw_DrawCapsule)(btScalar radius, btScalar halfHeight,
	int upAxis, const btScalar* transform, const btScalar* color);
typedef void (*p_btIDebugDraw_DrawCone)(btScalar radius, btScalar height, int upAxis,
	const btScalar* transform, const btScalar* color);
typedef void (*p_btIDebugDraw_DrawContactPoint)(const btScalar* PointOnB, const btScalar* normalOnB,
	btScalar distance, int lifeTime, const btScalar* color);
typedef void (*p_btIDebugDraw_DrawCylinder)(btScalar radius, btScalar halfHeight,
	int upAxis, const btScalar* transform, const btScalar* color);
typedef void (*p_btIDebugDraw_DrawLine)(const btScalar* from, const btScalar* to,
	const btScalar* color);
typedef void (*p_btIDebugDraw_DrawPlane)(const btScalar* planeNormal, btScalar planeConst,
	const btScalar* transform, const btScalar* color);
typedef void (*p_btIDebugDraw_DrawSphere)(btScalar radius, const btScalar* transform,
	const btScalar* color);
typedef void (*p_btIDebugDraw_DrawSpherePatch)(const btScalar* center, const btScalar* up,
	const btScalar* axis, btScalar radius, btScalar minTh, btScalar maxTh, btScalar minPs,
	btScalar maxPs, const btScalar* color, btScalar stepDegrees);
typedef void (*p_btIDebugDraw_DrawTransform)(const btScalar* transform, btScalar orthoLen);
typedef void (*p_btIDebugDraw_DrawTriangle)(const btScalar* v0, const btScalar* v1,
	const btScalar* v2, const btScalar* color, btScalar __unnamed4);
typedef int (*p_btIDebugDraw_GetDebugMode)();
typedef void (*pSimpleCallback)(int x);

class btIDebugDrawWrapper : public btIDebugDraw
{
private:
	p_btIDebugDraw_DrawAabb _drawAabbCallback;
	p_btIDebugDraw_DrawArc _drawArcCallback;
	p_btIDebugDraw_DrawBox _drawBoxCallback;
	p_btIDebugDraw_DrawCapsule _drawCapsuleCallback;
	p_btIDebugDraw_DrawCone _drawConeCallback;
	p_btIDebugDraw_DrawContactPoint _drawContactPointCallback;
	p_btIDebugDraw_DrawCylinder _drawCylinderCallback;
	p_btIDebugDraw_DrawLine _drawLineCallback;
	p_btIDebugDraw_DrawPlane _drawPlaneCallback;
	p_btIDebugDraw_DrawSphere _drawSphereCallback;
	p_btIDebugDraw_DrawSpherePatch _drawSpherePatchCallback;
	p_btIDebugDraw_DrawTransform _drawTransformCallback;
	p_btIDebugDraw_DrawTriangle _drawTriangleCallback;
	p_btIDebugDraw_GetDebugMode _getDebugModeCallback;

public:
	void* _debugDrawGCHandle;
	void* getGCHandle();

	pSimpleCallback _cb;

	btIDebugDrawWrapper(void* debugDrawGCHandle,
		p_btIDebugDraw_DrawAabb drawAabbCallback, p_btIDebugDraw_DrawArc drawArcCallback,
		p_btIDebugDraw_DrawBox drawBoxCallback, p_btIDebugDraw_DrawCapsule drawCapsuleCallback,
		p_btIDebugDraw_DrawCone drawConeCallback, p_btIDebugDraw_DrawContactPoint drawContactPointCallback,
		p_btIDebugDraw_DrawCylinder drawCylinderCallback, p_btIDebugDraw_DrawLine drawLineCallback,
		p_btIDebugDraw_DrawPlane drawPlaneCallback, p_btIDebugDraw_DrawSphere drawSphereCallback,
		p_btIDebugDraw_DrawSpherePatch drawSpherePatchCallback, p_btIDebugDraw_DrawTransform drawTransformCallback,
		p_btIDebugDraw_DrawTriangle drawTriangleCallback, p_btIDebugDraw_GetDebugMode getDebugModeCallback,
		pSimpleCallback cb);

	virtual void draw3dText(const btVector3& location, const char* textString);
	virtual void drawAabb(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle, const btVector3& color,
		bool drawSect, btScalar stepDegrees);
	virtual void drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect);
	virtual void drawBox(const btVector3& bbMin, const btVector3& bbMax, const btVector3& color);
	virtual void drawBox(const btVector3& bbMin, const btVector3& bbMax, const btTransform& trans,
		const btVector3& color);
	virtual void drawCapsule(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform,
		const btVector3& color);
	virtual void drawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform,
		const btVector3& color);
	virtual void drawContactPoint(const btVector3& PointOnB, const btVector3& normalOnB,
		btScalar distance, int lifeTime, const btVector3& color);
	virtual void drawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform,
		const btVector3& color);
	virtual void drawLine(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void drawPlane(const btVector3& planeNormal, btScalar planeConst, const btTransform& transform,
		const btVector3& color);
	virtual void drawSphere(const btVector3& p, btScalar radius, const btVector3& color);
	virtual void drawSphere(btScalar radius, const btTransform& transform, const btVector3& color);
	virtual void drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis,
		btScalar radius, btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs,
		const btVector3& color, btScalar stepDegrees);
	virtual void drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color);
	virtual void drawTransform(const btTransform& transform, btScalar orthoLen);
	virtual void drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
		const btVector3& color, btScalar __unnamed4);
	virtual void drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
		const btVector3&, const btVector3&, const btVector3&, const btVector3& color, btScalar alpha);

	virtual void baseDrawAabb(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void baseDrawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void baseDrawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void baseDrawSphere(const btVector3& p, btScalar radius, const btVector3& color);
	virtual void baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar);
	virtual void baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
		const btVector3&, const btVector3&, const btVector3&, const btVector3& color, btScalar alpha);

	virtual void reportErrorWarning(const char* warningString);

	virtual void setDebugMode(int debugMode);
	virtual int	getDebugMode() const;

	// Never called from Bullet
	//virtual void drawLine(const btVector3& from, const btVector3& to, const btVector3& fromColor,
	//	const btVector3& toColor);
};
#endif

extern "C"
{
	EXPORT btIDebugDrawWrapper* btIDebugDrawWrapper_new(void* debugDrawGCHandle,
		p_btIDebugDraw_DrawAabb drawAabbCallback, p_btIDebugDraw_DrawArc drawArcCallback,
		p_btIDebugDraw_DrawBox drawBoxCallback,
		p_btIDebugDraw_DrawCapsule drawCapsuleCallback, p_btIDebugDraw_DrawCone drawConeCallback,
		p_btIDebugDraw_DrawContactPoint drawContactPointCallback, p_btIDebugDraw_DrawCylinder drawCylinderCallback,
		p_btIDebugDraw_DrawLine drawLineCallback,
		p_btIDebugDraw_DrawPlane drawPlaneCallback, p_btIDebugDraw_DrawSphere drawSphereCallback,
		p_btIDebugDraw_DrawSpherePatch drawSpherePatchCallback,
		p_btIDebugDraw_DrawTransform drawTransformCallback, p_btIDebugDraw_DrawTriangle drawTriangleCallback,
		p_btIDebugDraw_GetDebugMode getDebugModeCallback);
	EXPORT void* btIDebugDrawWrapper_getGCHandle(btIDebugDrawWrapper* obj);

	EXPORT void btIDebugDraw_delete(btIDebugDraw* obj);
}
