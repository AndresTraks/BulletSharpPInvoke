#include "main.h"

#ifndef BT_MOTIONSTATE_H
#define p_btMotionState_GetWorldTransform void*
#define p_btMotionState_SetWorldTransform void*
#define btMotionStateWrapper void
#else
typedef void (*p_btMotionState_GetWorldTransform)(btScalar* worldTrans);
typedef void (*p_btMotionState_SetWorldTransform)(const btScalar* worldTrans);

class btMotionStateWrapper : public btMotionState
{
private:
	p_btMotionState_GetWorldTransform _getWorldTransformCallback;
	p_btMotionState_SetWorldTransform _setWorldTransformCallback;

public:
	btMotionStateWrapper(p_btMotionState_GetWorldTransform getWorldTransformCallback,
		p_btMotionState_SetWorldTransform setWorldTransformCallback);

	virtual void getWorldTransform(btTransform& worldTrans) const;
	virtual void setWorldTransform(const btTransform& worldTrans);
};
#endif

extern "C"
{
	EXPORT btMotionStateWrapper* btMotionStateWrapper_new(p_btMotionState_GetWorldTransform getWorldTransformCallback,
		p_btMotionState_SetWorldTransform setWorldTransformCallback);

	EXPORT void btMotionState_getWorldTransform(btMotionState* obj, btScalar* worldTrans);
	EXPORT void btMotionState_setWorldTransform(btMotionState* obj, const btScalar* worldTrans);
	EXPORT void btMotionState_delete(btMotionState* obj);
}
