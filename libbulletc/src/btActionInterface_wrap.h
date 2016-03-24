#include "main.h"

#ifndef _BT_ACTION_INTERFACE_H
#define p_btActionInterface_DebugDraw void*
#define p_btActionInterface_UpdateAction void*
#define btActionInterfaceWrapper void
#else
typedef void (*p_btActionInterface_DebugDraw)(btIDebugDraw* debugDrawer);
typedef void (*p_btActionInterface_UpdateAction)(btCollisionWorld* collisionWorld,
	btScalar deltaTimeStep);

class btActionInterfaceWrapper : public btActionInterface
{
private:
	p_btActionInterface_DebugDraw _debugDrawCallback;
	p_btActionInterface_UpdateAction _updateActionCallback;

public:
	btActionInterfaceWrapper(p_btActionInterface_DebugDraw debugDrawCallback, p_btActionInterface_UpdateAction updateActionCallback);

	virtual void debugDraw(btIDebugDraw* debugDrawer);
	virtual void updateAction(btCollisionWorld* collisionWorld, btScalar deltaTimeStep);
};
#endif

extern "C"
{
	EXPORT btActionInterfaceWrapper* btActionInterfaceWrapper_new(p_btActionInterface_DebugDraw debugDrawCallback,
		p_btActionInterface_UpdateAction updateActionCallback);

	EXPORT void btActionInterface_debugDraw(btActionInterface* obj, btIDebugDraw* debugDrawer);
	EXPORT void btActionInterface_updateAction(btActionInterface* obj, btCollisionWorld* collisionWorld, btScalar deltaTimeStep);
	EXPORT void btActionInterface_delete(btActionInterface* obj);
}
