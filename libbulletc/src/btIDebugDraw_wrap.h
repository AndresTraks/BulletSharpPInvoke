#include "main.h"

#ifndef BT_IDEBUG_DRAW__H
#define pDrawAabb void*
#define pDrawArc void*
#define pDrawBox void*
#define pDrawCapsule void*
#define pDrawCone void*
#define pDrawContactPoint void*
#define pDrawCylinder void*
#define pDrawLine void*
#define pDrawPlane void*
#define pDrawSphere void*
#define pDrawSpherePatch void*
#define pDrawTransform void*
#define pDrawTriangle void*
#define pGetDebugMode void*
#define pSimpleCallback void*

#define btIDebugDrawWrapper void
#else
typedef void (*pDrawAabb)(const btScalar* from, const btScalar* to, const btScalar* color);
typedef void (*pDrawArc)(const btScalar* center, const btScalar* normal, const btScalar* axis,
	btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle, const btScalar* color, bool drawSect, btScalar stepDegrees);
typedef void (*pDrawBox)(const btScalar* bbMin, const btScalar* bbMax, const btScalar* trans, const btScalar* color);
typedef void (*pDrawCapsule)(btScalar radius, btScalar halfHeight, int upAxis, const btScalar* transform, const btScalar* color);
typedef void (*pDrawCone)(btScalar radius, btScalar height, int upAxis, const btScalar* transform, const btScalar* color);
typedef void (*pDrawContactPoint)(const btScalar* PointOnB, const btScalar* normalOnB, btScalar distance, int lifeTime, const btScalar* color);
typedef void (*pDrawCylinder)(btScalar radius, btScalar halfHeight, int upAxis, const btScalar* transform, const btScalar* color);
typedef void (*pDrawLine)(const btScalar* from, const btScalar* to, const btScalar* color);
typedef void (*pDrawPlane)(const btScalar* planeNormal, btScalar planeConst, const btScalar* transform, const btScalar* color);
typedef void (*pDrawSphere)(btScalar radius, const btScalar* transform, const btScalar* color);
typedef void (*pDrawSpherePatch)(const btScalar* center, const btScalar* up, const btScalar* axis, btScalar radius,
	btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btScalar* color, btScalar stepDegrees);
typedef void (*pDrawTransform)(const btScalar* transform, btScalar orthoLen);
typedef void (*pDrawTriangle)(const btScalar* v0, const btScalar* v1, const btScalar* v2, const btScalar* color, btScalar);
typedef int(*pGetDebugMode)();
typedef void (*pSimpleCallback)(int x);

class btIDebugDrawWrapper : public btIDebugDraw
{
private:
	pDrawAabb _drawAabbCallback;
	pDrawArc _drawArcCallback;
	pDrawBox _drawBoxCallback;
	pDrawCapsule _drawCapsuleCallback;
	pDrawCone _drawConeCallback;
	pDrawContactPoint _drawContactPointCallback;
	pDrawCylinder _drawCylinderCallback;
	pDrawLine _drawLineCallback;
	pDrawPlane _drawPlaneCallback;
	pDrawSphere _drawSphereCallback;
	pDrawSpherePatch _drawSpherePatchCallback;
	pDrawTransform _drawTransformCallback;
	pDrawTriangle _drawTriangleCallback;
	pGetDebugMode _getDebugModeCallback;

public:
	void* _debugDrawGCHandle;
	void* getGCHandle();
	
	pSimpleCallback _cb;

	btIDebugDrawWrapper(void* debugDrawGCHandle, pDrawAabb drawAabbCallback, pDrawArc drawArcCallback, pDrawBox drawBoxCallback,
		pDrawCapsule drawCapsuleCallback, pDrawCone drawConeCallback, pDrawContactPoint drawContactPointCallback, pDrawCylinder drawCylinderCallback, pDrawLine drawLineCallback,
		pDrawPlane drawPlaneCallback, pDrawSphere drawSphereCallback, pDrawSpherePatch drawSpherePatch, pDrawTransform drawTransformCallback,
		pDrawTriangle drawTriangleCallback, pGetDebugMode getDebugModeCallback, pSimpleCallback cb);

	virtual void draw3dText(const btVector3& location, const char* textString);
	virtual void drawAabb(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect, btScalar stepDegrees);
	virtual void drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect);
	virtual void drawBox(const btVector3& bbMin, const btVector3& bbMax, const btTransform& trans, const btVector3& color);
	virtual void drawBox(const btVector3& bbMin, const btVector3& bbMax, const btVector3& color);
	virtual void drawCapsule(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawContactPoint(const btVector3& PointOnB, const btVector3& normalOnB, btScalar distance, int lifeTime, const btVector3& color);
	virtual void drawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawLine(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void drawPlane(const btVector3& planeNormal, btScalar planeConst, const btTransform& transform, const btVector3& color);
	virtual void drawSphere(const btVector3& p, btScalar radius, const btVector3& color);
	virtual void drawSphere(btScalar radius, const btTransform& transform, const btVector3& color);
	virtual void drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color, btScalar stepDegrees);
	virtual void drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color);
	virtual void drawTransform(const btTransform& transform, btScalar orthoLen);
	virtual void drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar);
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
	//virtual void drawLine(const btVector3& from, const btVector3& to, const btVector3& fromColor, const btVector3& toColor);
};
#endif

extern "C"
{
	EXPORT btIDebugDrawWrapper* btIDebugDrawWrapper_new(void* debugDrawGCHandle, pDrawAabb drawAabbCallback,
		pDrawArc drawArcCallback, pDrawBox drawBoxCallback, pDrawCapsule drawCapsule, pDrawCone drawCone, pDrawContactPoint drawContactPointCallback,
		pDrawCylinder drawCylinderCallback, pDrawLine drawLineCallback, pDrawPlane drawPlaneCallback, pDrawSphere drawSphereCallback,
		pDrawSpherePatch drawSpherePatchCallback, pDrawTransform drawTransformCallback, pDrawTriangle drawTriangleCallback, pGetDebugMode getDebugModeCallback, pSimpleCallback cb);
	EXPORT void* btIDebugDrawWrapper_getGCHandle(btIDebugDrawWrapper* obj);

	EXPORT void btIDebugDraw_delete(btIDebugDraw* obj);
}
