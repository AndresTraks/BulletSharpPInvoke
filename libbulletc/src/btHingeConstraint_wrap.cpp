#include <BulletDynamics/ConstraintSolver/btHingeConstraint.h>

#include "conversion.h"
#include "btHingeConstraint_wrap.h"

btHingeConstraint* btHingeConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB, const btScalar* axisInA, const btScalar* axisInB)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(pivotInB);
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return new btHingeConstraint(*rbA, *rbB, VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB), VECTOR3_USE(axisInA), VECTOR3_USE(axisInB));
}

btHingeConstraint* btHingeConstraint_new2(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB, const btScalar* axisInA, const btScalar* axisInB, bool useReferenceFrameA)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(pivotInB);
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return new btHingeConstraint(*rbA, *rbB, VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB), VECTOR3_USE(axisInA), VECTOR3_USE(axisInB), useReferenceFrameA);
}

btHingeConstraint* btHingeConstraint_new3(btRigidBody* rbA, const btScalar* pivotInA, const btScalar* axisInA)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(axisInA);
	return new btHingeConstraint(*rbA, VECTOR3_USE(pivotInA), VECTOR3_USE(axisInA));
}

btHingeConstraint* btHingeConstraint_new4(btRigidBody* rbA, const btScalar* pivotInA, const btScalar* axisInA, bool useReferenceFrameA)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(axisInA);
	return new btHingeConstraint(*rbA, VECTOR3_USE(pivotInA), VECTOR3_USE(axisInA), useReferenceFrameA);
}

btHingeConstraint* btHingeConstraint_new5(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame)
{
	TRANSFORM_CONV(rbAFrame);
	TRANSFORM_CONV(rbBFrame);
	return new btHingeConstraint(*rbA, *rbB, TRANSFORM_USE(rbAFrame), TRANSFORM_USE(rbBFrame));
}

btHingeConstraint* btHingeConstraint_new6(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame, bool useReferenceFrameA)
{
	TRANSFORM_CONV(rbAFrame);
	TRANSFORM_CONV(rbBFrame);
	return new btHingeConstraint(*rbA, *rbB, TRANSFORM_USE(rbAFrame), TRANSFORM_USE(rbBFrame), useReferenceFrameA);
}

btHingeConstraint* btHingeConstraint_new7(btRigidBody* rbA, const btScalar* rbAFrame)
{
	TRANSFORM_CONV(rbAFrame);
	return new btHingeConstraint(*rbA, TRANSFORM_USE(rbAFrame));
}

btHingeConstraint* btHingeConstraint_new8(btRigidBody* rbA, const btScalar* rbAFrame, bool useReferenceFrameA)
{
	TRANSFORM_CONV(rbAFrame);
	return new btHingeConstraint(*rbA, TRANSFORM_USE(rbAFrame), useReferenceFrameA);
}

void btHingeConstraint_enableAngularMotor(btHingeConstraint* obj, bool enableMotor, btScalar targetVelocity, btScalar maxMotorImpulse)
{
	obj->enableAngularMotor(enableMotor, targetVelocity, maxMotorImpulse);
}

void btHingeConstraint_enableMotor(btHingeConstraint* obj, bool enableMotor)
{
	obj->enableMotor(enableMotor);
}

void btHingeConstraint_getAFrame(btHingeConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getAFrame(), value);
}

bool btHingeConstraint_getAngularOnly(btHingeConstraint* obj)
{
	return obj->getAngularOnly();
}

void btHingeConstraint_getBFrame(btHingeConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getBFrame(), value);
}

bool btHingeConstraint_getEnableAngularMotor(btHingeConstraint* obj)
{
	return obj->getEnableAngularMotor();
}

int btHingeConstraint_getFlags(btHingeConstraint* obj)
{
	return obj->getFlags();
}

void btHingeConstraint_getFrameOffsetA(btHingeConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetA(), value);
}

void btHingeConstraint_getFrameOffsetB(btHingeConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetB(), value);
}

btScalar btHingeConstraint_getHingeAngle(btHingeConstraint* obj, const btScalar* transA, const btScalar* transB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	return obj->getHingeAngle(TRANSFORM_USE(transA), TRANSFORM_USE(transB));
}

btScalar btHingeConstraint_getHingeAngle2(btHingeConstraint* obj)
{
	return obj->getHingeAngle();
}

void btHingeConstraint_getInfo1NonVirtual(btHingeConstraint* obj, btTypedConstraint::btConstraintInfo1* info)
{
	obj->getInfo1NonVirtual(info);
}

void btHingeConstraint_getInfo2Internal(btHingeConstraint* obj, btTypedConstraint::btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* angVelA, const btScalar* angVelB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	VECTOR3_CONV(angVelA);
	VECTOR3_CONV(angVelB);
	obj->getInfo2Internal(info, TRANSFORM_USE(transA), TRANSFORM_USE(transB), VECTOR3_USE(angVelA), VECTOR3_USE(angVelB));
}

void btHingeConstraint_getInfo2InternalUsingFrameOffset(btHingeConstraint* obj, btTypedConstraint::btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* angVelA, const btScalar* angVelB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	VECTOR3_CONV(angVelA);
	VECTOR3_CONV(angVelB);
	obj->getInfo2InternalUsingFrameOffset(info, TRANSFORM_USE(transA), TRANSFORM_USE(transB), VECTOR3_USE(angVelA), VECTOR3_USE(angVelB));
}

void btHingeConstraint_getInfo2NonVirtual(btHingeConstraint* obj, btTypedConstraint::btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* angVelA, const btScalar* angVelB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	VECTOR3_CONV(angVelA);
	VECTOR3_CONV(angVelB);
	obj->getInfo2NonVirtual(info, TRANSFORM_USE(transA), TRANSFORM_USE(transB), VECTOR3_USE(angVelA), VECTOR3_USE(angVelB));
}

btScalar btHingeConstraint_getLimitBiasFactor(btHingeConstraint* obj)
{
	return obj->getLimitBiasFactor();
}

btScalar btHingeConstraint_getLimitRelaxationFactor(btHingeConstraint* obj)
{
	return obj->getLimitRelaxationFactor();
}

btScalar btHingeConstraint_getLimitSign(btHingeConstraint* obj)
{
	return obj->getLimitSign();
}

btScalar btHingeConstraint_getLimitSoftness(btHingeConstraint* obj)
{
	return obj->getLimitSoftness();
}

btScalar btHingeConstraint_getLowerLimit(btHingeConstraint* obj)
{
	return obj->getLowerLimit();
}

btScalar btHingeConstraint_getMaxMotorImpulse(btHingeConstraint* obj)
{
	return obj->getMaxMotorImpulse();
}

btScalar btHingeConstraint_getMotorTargetVelosity(btHingeConstraint* obj)
{
	return obj->getMotorTargetVelosity();
}

int btHingeConstraint_getSolveLimit(btHingeConstraint* obj)
{
	return obj->getSolveLimit();
}

btScalar btHingeConstraint_getUpperLimit(btHingeConstraint* obj)
{
	return obj->getUpperLimit();
}

bool btHingeConstraint_getUseFrameOffset(btHingeConstraint* obj)
{
	return obj->getUseFrameOffset();
}

bool btHingeConstraint_getUseReferenceFrameA(btHingeConstraint* obj)
{
	return obj->getUseReferenceFrameA();
}

bool btHingeConstraint_hasLimit(btHingeConstraint* obj)
{
	return obj->hasLimit();
}

void btHingeConstraint_setAngularOnly(btHingeConstraint* obj, bool angularOnly)
{
	obj->setAngularOnly(angularOnly);
}

void btHingeConstraint_setAxis(btHingeConstraint* obj, btScalar* axisInA)
{
	VECTOR3_CONV(axisInA);
	obj->setAxis(VECTOR3_USE(axisInA));
}

void btHingeConstraint_setFrames(btHingeConstraint* obj, const btScalar* frameA, const btScalar* frameB)
{
	TRANSFORM_CONV(frameA);
	TRANSFORM_CONV(frameB);
	obj->setFrames(TRANSFORM_USE(frameA), TRANSFORM_USE(frameB));
}

void btHingeConstraint_setLimit(btHingeConstraint* obj, btScalar low, btScalar high)
{
	obj->setLimit(low, high);
}

void btHingeConstraint_setLimit2(btHingeConstraint* obj, btScalar low, btScalar high, btScalar _softness)
{
	obj->setLimit(low, high, _softness);
}

void btHingeConstraint_setLimit3(btHingeConstraint* obj, btScalar low, btScalar high, btScalar _softness, btScalar _biasFactor)
{
	obj->setLimit(low, high, _softness, _biasFactor);
}

void btHingeConstraint_setLimit4(btHingeConstraint* obj, btScalar low, btScalar high, btScalar _softness, btScalar _biasFactor, btScalar _relaxationFactor)
{
	obj->setLimit(low, high, _softness, _biasFactor, _relaxationFactor);
}

void btHingeConstraint_setMaxMotorImpulse(btHingeConstraint* obj, btScalar maxMotorImpulse)
{
	obj->setMaxMotorImpulse(maxMotorImpulse);
}

void btHingeConstraint_setMotorTarget(btHingeConstraint* obj, btScalar targetAngle, btScalar dt)
{
	obj->setMotorTarget(targetAngle, dt);
}

void btHingeConstraint_setMotorTarget2(btHingeConstraint* obj, const btScalar* qAinB, btScalar dt)
{
	QUATERNION_CONV(qAinB);
	obj->setMotorTarget(QUATERNION_USE(qAinB), dt);
}

void btHingeConstraint_setMotorTargetVelocity(btHingeConstraint* obj, btScalar motorTargetVelocity)
{
	obj->setMotorTargetVelocity(motorTargetVelocity);
}

void btHingeConstraint_setUseFrameOffset(btHingeConstraint* obj, bool frameOffsetOnOff)
{
	obj->setUseFrameOffset(frameOffsetOnOff);
}

void btHingeConstraint_setUseReferenceFrameA(btHingeConstraint* obj, bool useReferenceFrameA)
{
	obj->setUseReferenceFrameA(useReferenceFrameA);
}

void btHingeConstraint_testLimit(btHingeConstraint* obj, const btScalar* transA, const btScalar* transB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	obj->testLimit(TRANSFORM_USE(transA), TRANSFORM_USE(transB));
}

void btHingeConstraint_updateRHS(btHingeConstraint* obj, btScalar timeStep)
{
	obj->updateRHS(timeStep);
}


btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB, const btScalar* axisInA, const btScalar* axisInB)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(pivotInB);
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return new btHingeAccumulatedAngleConstraint(*rbA, *rbB, VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB), VECTOR3_USE(axisInA), VECTOR3_USE(axisInB));
}

btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new2(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB, const btScalar* axisInA, const btScalar* axisInB, bool useReferenceFrameA)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(pivotInB);
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return new btHingeAccumulatedAngleConstraint(*rbA, *rbB, VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB), VECTOR3_USE(axisInA), VECTOR3_USE(axisInB), useReferenceFrameA);
}

btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new3(btRigidBody* rbA, const btScalar* pivotInA, const btScalar* axisInA)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(axisInA);
	return new btHingeAccumulatedAngleConstraint(*rbA, VECTOR3_USE(pivotInA), VECTOR3_USE(axisInA));
}

btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new4(btRigidBody* rbA, const btScalar* pivotInA, const btScalar* axisInA, bool useReferenceFrameA)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(axisInA);
	return new btHingeAccumulatedAngleConstraint(*rbA, VECTOR3_USE(pivotInA), VECTOR3_USE(axisInA), useReferenceFrameA);
}

btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new5(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame)
{
	TRANSFORM_CONV(rbAFrame);
	TRANSFORM_CONV(rbBFrame);
	return new btHingeAccumulatedAngleConstraint(*rbA, *rbB, TRANSFORM_USE(rbAFrame), TRANSFORM_USE(rbBFrame));
}

btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new6(btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame, bool useReferenceFrameA)
{
	TRANSFORM_CONV(rbAFrame);
	TRANSFORM_CONV(rbBFrame);
	return new btHingeAccumulatedAngleConstraint(*rbA, *rbB, TRANSFORM_USE(rbAFrame), TRANSFORM_USE(rbBFrame), useReferenceFrameA);
}

btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new7(btRigidBody* rbA, const btScalar* rbAFrame)
{
	TRANSFORM_CONV(rbAFrame);
	return new btHingeAccumulatedAngleConstraint(*rbA, TRANSFORM_USE(rbAFrame));
}

btHingeAccumulatedAngleConstraint* btHingeAccumulatedAngleConstraint_new8(btRigidBody* rbA, const btScalar* rbAFrame, bool useReferenceFrameA)
{
	TRANSFORM_CONV(rbAFrame);
	return new btHingeAccumulatedAngleConstraint(*rbA, TRANSFORM_USE(rbAFrame), useReferenceFrameA);
}

btScalar btHingeAccumulatedAngleConstraint_getAccumulatedHingeAngle(btHingeAccumulatedAngleConstraint* obj)
{
	return obj->getAccumulatedHingeAngle();
}

void btHingeAccumulatedAngleConstraint_setAccumulatedHingeAngle(btHingeAccumulatedAngleConstraint* obj, btScalar accAngle)
{
	obj->setAccumulatedHingeAngle(accAngle);
}
