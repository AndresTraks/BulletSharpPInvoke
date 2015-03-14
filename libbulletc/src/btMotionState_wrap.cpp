#include <LinearMath/btMotionState.h>

#include "conversion.h"
#include "btMotionState_wrap.h"

btMotionStateWrapper::btMotionStateWrapper(pMotionState_GetWorldTransform getWorldTransformCallback, pMotionState_SetWorldTransform setWorldTransformCallback)
{
	_getWorldTransformCallback = getWorldTransformCallback;
	_setWorldTransformCallback = setWorldTransformCallback;
}

void btMotionStateWrapper::getWorldTransform(btTransform& worldTrans) const
{
	ATTRIBUTE_ALIGNED16(btScalar) worldTransTemp[16];
	_getWorldTransformCallback(worldTransTemp);
	MatrixTobtTransform(worldTransTemp, &worldTrans);
}

void btMotionStateWrapper::setWorldTransform(const btTransform& worldTrans)
{
	ATTRIBUTE_ALIGNED16(btScalar) worldTransTemp[16];
	btTransformToMatrix(&worldTrans, worldTransTemp);
	_setWorldTransformCallback(worldTransTemp);
}


btMotionStateWrapper* btMotionStateWrapper_new(pMotionState_GetWorldTransform getWorldTransformCallback, pMotionState_SetWorldTransform setWorldTransformCallback)
{
	return ALIGNED_NEW(btMotionStateWrapper)(getWorldTransformCallback, setWorldTransformCallback);
}


void btMotionState_getWorldTransform(btMotionState* obj, btScalar* worldTrans)
{
	TRANSFORM_DEF(worldTrans);
	obj->getWorldTransform(TRANSFORM_USE(worldTrans));
	TRANSFORM_DEF_OUT(worldTrans);
}

void btMotionState_setWorldTransform(btMotionState* obj, const btScalar* worldTrans)
{
	TRANSFORM_CONV(worldTrans);
	obj->setWorldTransform(TRANSFORM_USE(worldTrans));
}

void btMotionState_delete(btMotionState* obj)
{
	ALIGNED_FREE(obj);
}
