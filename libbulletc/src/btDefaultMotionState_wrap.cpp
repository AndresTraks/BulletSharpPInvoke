#include <LinearMath/btDefaultMotionState.h>

#include "conversion.h"
#include "btDefaultMotionState_wrap.h"

btDefaultMotionState* btDefaultMotionState_new()
{
	return ALIGNED_NEW(btDefaultMotionState)();
}

btDefaultMotionState* btDefaultMotionState_new2(const btScalar* startTrans)
{
	TRANSFORM_CONV(startTrans);
	return ALIGNED_NEW(btDefaultMotionState)(TRANSFORM_USE(startTrans));
}

btDefaultMotionState* btDefaultMotionState_new3(const btScalar* startTrans, const btScalar* centerOfMassOffset)
{
	TRANSFORM_CONV(startTrans);
	TRANSFORM_CONV(centerOfMassOffset);
	return ALIGNED_NEW(btDefaultMotionState)(TRANSFORM_USE(startTrans), TRANSFORM_USE(centerOfMassOffset));
}

void btDefaultMotionState_getCenterOfMassOffset(btDefaultMotionState* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->m_centerOfMassOffset, value);
}

void btDefaultMotionState_getGraphicsWorldTrans(btDefaultMotionState* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->m_graphicsWorldTrans, value);
}

void btDefaultMotionState_getStartWorldTrans(btDefaultMotionState* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->m_startWorldTrans, value);
}

void* btDefaultMotionState_getUserPointer(btDefaultMotionState* obj)
{
	return obj->m_userPointer;
}

void btDefaultMotionState_setCenterOfMassOffset(btDefaultMotionState* obj, const btScalar* value)
{
	TRANSFORM_IN(value, &obj->m_centerOfMassOffset);
}

void btDefaultMotionState_setGraphicsWorldTrans(btDefaultMotionState* obj, const btScalar* value)
{
	TRANSFORM_IN(value, &obj->m_graphicsWorldTrans);
}

void btDefaultMotionState_setStartWorldTrans(btDefaultMotionState* obj, const btScalar* value)
{
	TRANSFORM_IN(value, &obj->m_startWorldTrans);
}

void btDefaultMotionState_setUserPointer(btDefaultMotionState* obj, void* value)
{
	obj->m_userPointer = value;
}
