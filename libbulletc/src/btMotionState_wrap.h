#include "main.h"

#ifndef BT_MOTIONSTATE_H
#define pMotionState_GetWorldTransform void*
#define pMotionState_SetWorldTransform void*
#define btMotionStateWrapper void
#else
typedef void (*pMotionState_GetWorldTransform)(btScalar* worldTrans);
typedef void (*pMotionState_SetWorldTransform)(const btScalar* worldTrans);

class btMotionStateWrapper : public btMotionState
{
private:
	pMotionState_GetWorldTransform _getWorldTransformCallback;
	pMotionState_SetWorldTransform _setWorldTransformCallback;

public:
	btMotionStateWrapper(pMotionState_GetWorldTransform getWorldTransformCallback, pMotionState_SetWorldTransform setWorldTransformCallback);

	virtual void getWorldTransform(btTransform& worldTrans) const;
	virtual void setWorldTransform(const btTransform& worldTrans);
};
#endif

extern "C"
{
	EXPORT btMotionStateWrapper* btMotionStateWrapper_new(pMotionState_GetWorldTransform getWorldTransformCallback, pMotionState_SetWorldTransform setWorldTransformCallback);

	EXPORT void btMotionState_getWorldTransform(btMotionState* obj, btScalar* worldTrans);
	EXPORT void btMotionState_setWorldTransform(btMotionState* obj, const btScalar* worldTrans);
	EXPORT void btMotionState_delete(btMotionState* obj);
}
