#include "main.h"

#ifndef BT_TRIANGLE_CALLBACK_H
#define p_btInternalTriangleIndexCallback_InternalProcessTriangleIndex void*
#define p_btTriangleCallback_ProcessTriangle void*
#define btInternalTriangleIndexCallbackWrapper void
#define btTriangleCallbackWrapper void
#else
typedef void (*p_btInternalTriangleIndexCallback_InternalProcessTriangleIndex)(btVector3* triangle,
	int partId, int triangleIndex);

class btInternalTriangleIndexCallbackWrapper : public btInternalTriangleIndexCallback
{
private:
	p_btInternalTriangleIndexCallback_InternalProcessTriangleIndex _internalProcessTriangleIndexCallback;

public:
	btInternalTriangleIndexCallbackWrapper(p_btInternalTriangleIndexCallback_InternalProcessTriangleIndex internalProcessTriangleIndexCallback);

	virtual void internalProcessTriangleIndex(btVector3* triangle, int partId, int triangleIndex);
};

typedef void (*p_btTriangleCallback_ProcessTriangle)(btVector3* triangle, int partId,
	int triangleIndex);

class btTriangleCallbackWrapper : public btTriangleCallback
{
private:
	p_btTriangleCallback_ProcessTriangle _processTriangleCallback;

public:
	btTriangleCallbackWrapper(p_btTriangleCallback_ProcessTriangle processTriangleCallback);

	virtual void processTriangle(btVector3* triangle, int partId, int triangleIndex);
};
#endif

extern "C"
{
	EXPORT btTriangleCallbackWrapper* btTriangleCallbackWrapper_new(p_btTriangleCallback_ProcessTriangle processTriangleCallback);

	EXPORT void btTriangleCallback_delete(btTriangleCallback* obj);

	EXPORT btInternalTriangleIndexCallbackWrapper* btInternalTriangleIndexCallbackWrapper_new(
		p_btInternalTriangleIndexCallback_InternalProcessTriangleIndex internalProcessTriangleIndexCallback);

	EXPORT void btInternalTriangleIndexCallback_delete(btInternalTriangleIndexCallback* obj);
}
