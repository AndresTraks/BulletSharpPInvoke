#include <BulletDynamics/ConstraintSolver/btConeTwistConstraint.h>

#include "conversion.h"
#include "btConeTwistConstraint_wrap.h"

btConeTwistConstraint* btConeTwistConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame)
{
	TRANSFORM_CONV(rbAFrame);
	TRANSFORM_CONV(rbBFrame);
	return new btConeTwistConstraint(*rbA, *rbB, TRANSFORM_USE(rbAFrame), TRANSFORM_USE(rbBFrame));
}

btConeTwistConstraint* btConeTwistConstraint_new2(btRigidBody* rbA, const btScalar* rbAFrame)
{
	TRANSFORM_CONV(rbAFrame);
	return new btConeTwistConstraint(*rbA, TRANSFORM_USE(rbAFrame));
}

void btConeTwistConstraint_calcAngleInfo(btConeTwistConstraint* obj)
{
	obj->calcAngleInfo();
}

void btConeTwistConstraint_calcAngleInfo2(btConeTwistConstraint* obj, const btScalar* transA, const btScalar* transB, const btScalar* invInertiaWorldA, const btScalar* invInertiaWorldB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	MATRIX3X3_CONV(invInertiaWorldA);
	MATRIX3X3_CONV(invInertiaWorldB);
	obj->calcAngleInfo2(TRANSFORM_USE(transA), TRANSFORM_USE(transB), MATRIX3X3_USE(invInertiaWorldA), MATRIX3X3_USE(invInertiaWorldB));
}

void btConeTwistConstraint_enableMotor(btConeTwistConstraint* obj, bool b)
{
	obj->enableMotor(b);
}

void btConeTwistConstraint_getAFrame(btConeTwistConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getAFrame(), value);
}

bool btConeTwistConstraint_getAngularOnly(btConeTwistConstraint* obj)
{
	return obj->getAngularOnly();
}

void btConeTwistConstraint_getBFrame(btConeTwistConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getBFrame(), value);
}

btScalar btConeTwistConstraint_getBiasFactor(btConeTwistConstraint* obj)
{
	return obj->getBiasFactor();
}

btScalar btConeTwistConstraint_getDamping(btConeTwistConstraint* obj)
{
	return obj->getDamping();
}

btScalar btConeTwistConstraint_getFixThresh(btConeTwistConstraint* obj)
{
	return obj->getFixThresh();
}

int btConeTwistConstraint_getFlags(btConeTwistConstraint* obj)
{
	return obj->getFlags();
}

void btConeTwistConstraint_getFrameOffsetA(btConeTwistConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetA(), value);
}

void btConeTwistConstraint_getFrameOffsetB(btConeTwistConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetB(), value);
}

void btConeTwistConstraint_getInfo1NonVirtual(btConeTwistConstraint* obj, btTypedConstraint::btConstraintInfo1* info)
{
	obj->getInfo1NonVirtual(info);
}

void btConeTwistConstraint_getInfo2NonVirtual(btConeTwistConstraint* obj, btTypedConstraint::btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* invInertiaWorldA, const btScalar* invInertiaWorldB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	MATRIX3X3_CONV(invInertiaWorldA);
	MATRIX3X3_CONV(invInertiaWorldB);
	obj->getInfo2NonVirtual(info, TRANSFORM_USE(transA), TRANSFORM_USE(transB), MATRIX3X3_USE(invInertiaWorldA), MATRIX3X3_USE(invInertiaWorldB));
}

btScalar btConeTwistConstraint_getLimit(btConeTwistConstraint* obj, int limitIndex)
{
	return obj->getLimit(limitIndex);
}

btScalar btConeTwistConstraint_getLimitSoftness(btConeTwistConstraint* obj)
{
	return obj->getLimitSoftness();
}

btScalar btConeTwistConstraint_getMaxMotorImpulse(btConeTwistConstraint* obj)
{
	return obj->getMaxMotorImpulse();
}

void btConeTwistConstraint_getMotorTarget(btConeTwistConstraint* obj, btScalar* q)
{
	QUATERNION_OUT(&obj->getMotorTarget(), q);
}

void btConeTwistConstraint_GetPointForAngle(btConeTwistConstraint* obj, btScalar fAngleInRadians, btScalar fLength, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->GetPointForAngle(fAngleInRadians, fLength), value);
}

btScalar btConeTwistConstraint_getRelaxationFactor(btConeTwistConstraint* obj)
{
	return obj->getRelaxationFactor();
}

int btConeTwistConstraint_getSolveSwingLimit(btConeTwistConstraint* obj)
{
	return obj->getSolveSwingLimit();
}

int btConeTwistConstraint_getSolveTwistLimit(btConeTwistConstraint* obj)
{
	return obj->getSolveTwistLimit();
}

btScalar btConeTwistConstraint_getSwingSpan1(btConeTwistConstraint* obj)
{
	return obj->getSwingSpan1();
}

btScalar btConeTwistConstraint_getSwingSpan2(btConeTwistConstraint* obj)
{
	return obj->getSwingSpan2();
}

btScalar btConeTwistConstraint_getTwistAngle(btConeTwistConstraint* obj)
{
	return obj->getTwistAngle();
}

btScalar btConeTwistConstraint_getTwistLimitSign(btConeTwistConstraint* obj)
{
	return obj->getTwistLimitSign();
}

btScalar btConeTwistConstraint_getTwistSpan(btConeTwistConstraint* obj)
{
	return obj->getTwistSpan();
}

bool btConeTwistConstraint_isMaxMotorImpulseNormalized(btConeTwistConstraint* obj)
{
	return obj->isMaxMotorImpulseNormalized();
}

bool btConeTwistConstraint_isMotorEnabled(btConeTwistConstraint* obj)
{
	return obj->isMotorEnabled();
}

bool btConeTwistConstraint_isPastSwingLimit(btConeTwistConstraint* obj)
{
	return obj->isPastSwingLimit();
}

void btConeTwistConstraint_setAngularOnly(btConeTwistConstraint* obj, bool angularOnly)
{
	obj->setAngularOnly(angularOnly);
}

void btConeTwistConstraint_setDamping(btConeTwistConstraint* obj, btScalar damping)
{
	obj->setDamping(damping);
}

void btConeTwistConstraint_setFixThresh(btConeTwistConstraint* obj, btScalar fixThresh)
{
	obj->setFixThresh(fixThresh);
}

void btConeTwistConstraint_setFrames(btConeTwistConstraint* obj, const btScalar* frameA, const btScalar* frameB)
{
	TRANSFORM_CONV(frameA);
	TRANSFORM_CONV(frameB);
	obj->setFrames(TRANSFORM_USE(frameA), TRANSFORM_USE(frameB));
}

void btConeTwistConstraint_setLimit(btConeTwistConstraint* obj, int limitIndex, btScalar limitValue)
{
	obj->setLimit(limitIndex, limitValue);
}

void btConeTwistConstraint_setLimit2(btConeTwistConstraint* obj, btScalar _swingSpan1, btScalar _swingSpan2, btScalar _twistSpan)
{
	obj->setLimit(_swingSpan1, _swingSpan2, _twistSpan);
}

void btConeTwistConstraint_setLimit3(btConeTwistConstraint* obj, btScalar _swingSpan1, btScalar _swingSpan2, btScalar _twistSpan, btScalar _softness)
{
	obj->setLimit(_swingSpan1, _swingSpan2, _twistSpan, _softness);
}

void btConeTwistConstraint_setLimit4(btConeTwistConstraint* obj, btScalar _swingSpan1, btScalar _swingSpan2, btScalar _twistSpan, btScalar _softness, btScalar _biasFactor)
{
	obj->setLimit(_swingSpan1, _swingSpan2, _twistSpan, _softness, _biasFactor);
}

void btConeTwistConstraint_setLimit5(btConeTwistConstraint* obj, btScalar _swingSpan1, btScalar _swingSpan2, btScalar _twistSpan, btScalar _softness, btScalar _biasFactor, btScalar _relaxationFactor)
{
	obj->setLimit(_swingSpan1, _swingSpan2, _twistSpan, _softness, _biasFactor, _relaxationFactor);
}

void btConeTwistConstraint_setMaxMotorImpulse(btConeTwistConstraint* obj, btScalar maxMotorImpulse)
{
	obj->setMaxMotorImpulse(maxMotorImpulse);
}

void btConeTwistConstraint_setMaxMotorImpulseNormalized(btConeTwistConstraint* obj, btScalar maxMotorImpulse)
{
	obj->setMaxMotorImpulseNormalized(maxMotorImpulse);
}

void btConeTwistConstraint_setMotorTarget(btConeTwistConstraint* obj, const btScalar* q)
{
	QUATERNION_CONV(q);
	obj->setMotorTarget(QUATERNION_USE(q));
}

void btConeTwistConstraint_setMotorTargetInConstraintSpace(btConeTwistConstraint* obj, const btScalar* q)
{
	QUATERNION_CONV(q);
	obj->setMotorTargetInConstraintSpace(QUATERNION_USE(q));
}

void btConeTwistConstraint_updateRHS(btConeTwistConstraint* obj, btScalar timeStep)
{
	obj->updateRHS(timeStep);
}
