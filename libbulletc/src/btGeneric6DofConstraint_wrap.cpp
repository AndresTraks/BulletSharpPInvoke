#include <BulletDynamics/ConstraintSolver/btGeneric6DofConstraint.h>

#include "conversion.h"
#include "btGeneric6DofConstraint_wrap.h"

btRotationalLimitMotor* btRotationalLimitMotor_new()
{
	return new btRotationalLimitMotor();
}

btRotationalLimitMotor* btRotationalLimitMotor_new2(const btRotationalLimitMotor* limot)
{
	return new btRotationalLimitMotor(*limot);
}

btScalar btRotationalLimitMotor_getAccumulatedImpulse(btRotationalLimitMotor* obj)
{
	return obj->m_accumulatedImpulse;
}

btScalar btRotationalLimitMotor_getBounce(btRotationalLimitMotor* obj)
{
	return obj->m_bounce;
}

int btRotationalLimitMotor_getCurrentLimit(btRotationalLimitMotor* obj)
{
	return obj->m_currentLimit;
}

btScalar btRotationalLimitMotor_getCurrentLimitError(btRotationalLimitMotor* obj)
{
	return obj->m_currentLimitError;
}

btScalar btRotationalLimitMotor_getCurrentPosition(btRotationalLimitMotor* obj)
{
	return obj->m_currentPosition;
}

btScalar btRotationalLimitMotor_getDamping(btRotationalLimitMotor* obj)
{
	return obj->m_damping;
}

bool btRotationalLimitMotor_getEnableMotor(btRotationalLimitMotor* obj)
{
	return obj->m_enableMotor;
}

btScalar btRotationalLimitMotor_getHiLimit(btRotationalLimitMotor* obj)
{
	return obj->m_hiLimit;
}

btScalar btRotationalLimitMotor_getLimitSoftness(btRotationalLimitMotor* obj)
{
	return obj->m_limitSoftness;
}

btScalar btRotationalLimitMotor_getLoLimit(btRotationalLimitMotor* obj)
{
	return obj->m_loLimit;
}

btScalar btRotationalLimitMotor_getMaxLimitForce(btRotationalLimitMotor* obj)
{
	return obj->m_maxLimitForce;
}

btScalar btRotationalLimitMotor_getMaxMotorForce(btRotationalLimitMotor* obj)
{
	return obj->m_maxMotorForce;
}

btScalar btRotationalLimitMotor_getNormalCFM(btRotationalLimitMotor* obj)
{
	return obj->m_normalCFM;
}

btScalar btRotationalLimitMotor_getStopCFM(btRotationalLimitMotor* obj)
{
	return obj->m_stopCFM;
}

btScalar btRotationalLimitMotor_getStopERP(btRotationalLimitMotor* obj)
{
	return obj->m_stopERP;
}

btScalar btRotationalLimitMotor_getTargetVelocity(btRotationalLimitMotor* obj)
{
	return obj->m_targetVelocity;
}

bool btRotationalLimitMotor_isLimited(btRotationalLimitMotor* obj)
{
	return obj->isLimited();
}

bool btRotationalLimitMotor_needApplyTorques(btRotationalLimitMotor* obj)
{
	return obj->needApplyTorques();
}

void btRotationalLimitMotor_setAccumulatedImpulse(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_accumulatedImpulse = value;
}

void btRotationalLimitMotor_setBounce(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_bounce = value;
}

void btRotationalLimitMotor_setCurrentLimit(btRotationalLimitMotor* obj, int value)
{
	obj->m_currentLimit = value;
}

void btRotationalLimitMotor_setCurrentLimitError(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_currentLimitError = value;
}

void btRotationalLimitMotor_setCurrentPosition(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_currentPosition = value;
}

void btRotationalLimitMotor_setDamping(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_damping = value;
}

void btRotationalLimitMotor_setEnableMotor(btRotationalLimitMotor* obj, bool value)
{
	obj->m_enableMotor = value;
}

void btRotationalLimitMotor_setHiLimit(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_hiLimit = value;
}

void btRotationalLimitMotor_setLimitSoftness(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_limitSoftness = value;
}

void btRotationalLimitMotor_setLoLimit(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_loLimit = value;
}

void btRotationalLimitMotor_setMaxLimitForce(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_maxLimitForce = value;
}

void btRotationalLimitMotor_setMaxMotorForce(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_maxMotorForce = value;
}

void btRotationalLimitMotor_setNormalCFM(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_normalCFM = value;
}

void btRotationalLimitMotor_setStopCFM(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_stopCFM = value;
}

void btRotationalLimitMotor_setStopERP(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_stopERP = value;
}

void btRotationalLimitMotor_setTargetVelocity(btRotationalLimitMotor* obj, btScalar value)
{
	obj->m_targetVelocity = value;
}

btScalar btRotationalLimitMotor_solveAngularLimits(btRotationalLimitMotor* obj, btScalar timeStep, btScalar* axis, btScalar jacDiagABInv, btRigidBody* body0, btRigidBody* body1)
{
	VECTOR3_CONV(axis);
	return obj->solveAngularLimits(timeStep, VECTOR3_USE(axis), jacDiagABInv, body0, body1);
}

int btRotationalLimitMotor_testLimitValue(btRotationalLimitMotor* obj, btScalar test_value)
{
	return obj->testLimitValue(test_value);
}

void btRotationalLimitMotor_delete(btRotationalLimitMotor* obj)
{
	delete obj;
}


btTranslationalLimitMotor* btTranslationalLimitMotor_new()
{
	return new btTranslationalLimitMotor();
}

btTranslationalLimitMotor* btTranslationalLimitMotor_new2(const btTranslationalLimitMotor* other)
{
	return new btTranslationalLimitMotor(*other);
}

void btTranslationalLimitMotor_getAccumulatedImpulse(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_accumulatedImpulse, value);
}

int* btTranslationalLimitMotor_getCurrentLimit(btTranslationalLimitMotor* obj)
{
	return obj->m_currentLimit;
}

void btTranslationalLimitMotor_getCurrentLimitError(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_currentLimitError, value);
}

void btTranslationalLimitMotor_getCurrentLinearDiff(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_currentLinearDiff, value);
}

btScalar btTranslationalLimitMotor_getDamping(btTranslationalLimitMotor* obj)
{
	return obj->m_damping;
}

bool* btTranslationalLimitMotor_getEnableMotor(btTranslationalLimitMotor* obj)
{
	return obj->m_enableMotor;
}

btScalar btTranslationalLimitMotor_getLimitSoftness(btTranslationalLimitMotor* obj)
{
	return obj->m_limitSoftness;
}

void btTranslationalLimitMotor_getLowerLimit(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_lowerLimit, value);
}

void btTranslationalLimitMotor_getMaxMotorForce(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_maxMotorForce, value);
}

void btTranslationalLimitMotor_getNormalCFM(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_normalCFM, value);
}

btScalar btTranslationalLimitMotor_getRestitution(btTranslationalLimitMotor* obj)
{
	return obj->m_restitution;
}

void btTranslationalLimitMotor_getStopCFM(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_stopCFM, value);
}

void btTranslationalLimitMotor_getStopERP(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_stopERP, value);
}

void btTranslationalLimitMotor_getTargetVelocity(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_targetVelocity, value);
}

void btTranslationalLimitMotor_getUpperLimit(btTranslationalLimitMotor* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_upperLimit, value);
}

bool btTranslationalLimitMotor_isLimited(btTranslationalLimitMotor* obj, int limitIndex)
{
	return obj->isLimited(limitIndex);
}

bool btTranslationalLimitMotor_needApplyForce(btTranslationalLimitMotor* obj, int limitIndex)
{
	return obj->needApplyForce(limitIndex);
}

void btTranslationalLimitMotor_setAccumulatedImpulse(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_accumulatedImpulse);
}

void btTranslationalLimitMotor_setCurrentLimitError(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_currentLimitError);
}

void btTranslationalLimitMotor_setCurrentLinearDiff(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_currentLinearDiff);
}

void btTranslationalLimitMotor_setDamping(btTranslationalLimitMotor* obj, btScalar value)
{
	obj->m_damping = value;
}

void btTranslationalLimitMotor_setLimitSoftness(btTranslationalLimitMotor* obj, btScalar value)
{
	obj->m_limitSoftness = value;
}

void btTranslationalLimitMotor_setLowerLimit(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_lowerLimit);
}

void btTranslationalLimitMotor_setMaxMotorForce(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_maxMotorForce);
}

void btTranslationalLimitMotor_setNormalCFM(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_normalCFM);
}

void btTranslationalLimitMotor_setRestitution(btTranslationalLimitMotor* obj, btScalar value)
{
	obj->m_restitution = value;
}

void btTranslationalLimitMotor_setStopCFM(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_stopCFM);
}

void btTranslationalLimitMotor_setStopERP(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_stopERP);
}

void btTranslationalLimitMotor_setTargetVelocity(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_targetVelocity);
}

void btTranslationalLimitMotor_setUpperLimit(btTranslationalLimitMotor* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_upperLimit);
}

btScalar btTranslationalLimitMotor_solveLinearAxis(btTranslationalLimitMotor* obj, btScalar timeStep, btScalar jacDiagABInv, btRigidBody* body1, const btScalar* pointInA, btRigidBody* body2, const btScalar* pointInB, int limit_index, const btScalar* axis_normal_on_a, const btScalar* anchorPos)
{
	VECTOR3_CONV(pointInA);
	VECTOR3_CONV(pointInB);
	VECTOR3_CONV(axis_normal_on_a);
	VECTOR3_CONV(anchorPos);
	return obj->solveLinearAxis(timeStep, jacDiagABInv, *body1, VECTOR3_USE(pointInA), *body2, VECTOR3_USE(pointInB), limit_index, VECTOR3_USE(axis_normal_on_a), VECTOR3_USE(anchorPos));
}

int btTranslationalLimitMotor_testLimitValue(btTranslationalLimitMotor* obj, int limitIndex, btScalar test_value)
{
	return obj->testLimitValue(limitIndex, test_value);
}

void btTranslationalLimitMotor_delete(btTranslationalLimitMotor* obj)
{
	delete obj;
}


btGeneric6DofConstraint* btGeneric6DofConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofConstraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

btGeneric6DofConstraint* btGeneric6DofConstraint_new2(btRigidBody* rbB, const btScalar* frameInB, bool useLinearReferenceFrameB)
{
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofConstraint(*rbB, TRANSFORM_USE(frameInB), useLinearReferenceFrameB);
}

void btGeneric6DofConstraint_calcAnchorPos(btGeneric6DofConstraint* obj)
{
	obj->calcAnchorPos();
}

void btGeneric6DofConstraint_calculateTransforms(btGeneric6DofConstraint* obj, const btScalar* transA, const btScalar* transB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	obj->calculateTransforms(TRANSFORM_USE(transA), TRANSFORM_USE(transB));
}

void btGeneric6DofConstraint_calculateTransforms2(btGeneric6DofConstraint* obj)
{
	obj->calculateTransforms();
}

int btGeneric6DofConstraint_get_limit_motor_info2(btGeneric6DofConstraint* obj, btRotationalLimitMotor* limot, const btScalar* transA, const btScalar* transB, const btScalar* linVelA, const btScalar* linVelB, const btScalar* angVelA, const btScalar* angVelB, btTypedConstraint::btConstraintInfo2* info, int row, btScalar* ax1, int rotational)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	VECTOR3_CONV(linVelA);
	VECTOR3_CONV(linVelB);
	VECTOR3_CONV(angVelA);
	VECTOR3_CONV(angVelB);
	VECTOR3_CONV(ax1);
	int ret = obj->get_limit_motor_info2(limot, TRANSFORM_USE(transA), TRANSFORM_USE(transB), VECTOR3_USE(linVelA), VECTOR3_USE(linVelB), VECTOR3_USE(angVelA), VECTOR3_USE(angVelB), info, row, VECTOR3_USE(ax1), rotational);
	VECTOR3_DEF_OUT(ax1);
	return ret;
}

int btGeneric6DofConstraint_get_limit_motor_info22(btGeneric6DofConstraint* obj, btRotationalLimitMotor* limot, const btScalar* transA, const btScalar* transB, const btScalar* linVelA, const btScalar* linVelB, const btScalar* angVelA, const btScalar* angVelB, btTypedConstraint::btConstraintInfo2* info, int row, btScalar* ax1, int rotational, int rotAllowed)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	VECTOR3_CONV(linVelA);
	VECTOR3_CONV(linVelB);
	VECTOR3_CONV(angVelA);
	VECTOR3_CONV(angVelB);
	VECTOR3_CONV(ax1);
	int ret = obj->get_limit_motor_info2(limot, TRANSFORM_USE(transA), TRANSFORM_USE(transB), VECTOR3_USE(linVelA), VECTOR3_USE(linVelB), VECTOR3_USE(angVelA), VECTOR3_USE(angVelB), info, row, VECTOR3_USE(ax1), rotational, rotAllowed);
	VECTOR3_DEF_OUT(ax1);
	return ret;
}

btScalar btGeneric6DofConstraint_getAngle(btGeneric6DofConstraint* obj, int axis_index)
{
	return obj->getAngle(axis_index);
}

void btGeneric6DofConstraint_getAngularLowerLimit(btGeneric6DofConstraint* obj, btScalar* angularLower)
{
	VECTOR3_DEF(angularLower);
	obj->getAngularLowerLimit(VECTOR3_USE(angularLower));
	VECTOR3_DEF_OUT(angularLower);
}

void btGeneric6DofConstraint_getAngularUpperLimit(btGeneric6DofConstraint* obj, btScalar* angularUpper)
{
	VECTOR3_DEF(angularUpper);
	obj->getAngularUpperLimit(VECTOR3_USE(angularUpper));
	VECTOR3_DEF_OUT(angularUpper);
}

void btGeneric6DofConstraint_getAxis(btGeneric6DofConstraint* obj, int axis_index, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getAxis(axis_index), value);
}

void btGeneric6DofConstraint_getCalculatedTransformA(btGeneric6DofConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getCalculatedTransformA(), value);
}

void btGeneric6DofConstraint_getCalculatedTransformB(btGeneric6DofConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getCalculatedTransformB(), value);
}

int btGeneric6DofConstraint_getFlags(btGeneric6DofConstraint* obj)
{
	return obj->getFlags();
}

void btGeneric6DofConstraint_getFrameOffsetA(btGeneric6DofConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetA(), value);
}

void btGeneric6DofConstraint_getFrameOffsetB(btGeneric6DofConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetB(), value);
}

void btGeneric6DofConstraint_getInfo1NonVirtual(btGeneric6DofConstraint* obj, btTypedConstraint::btConstraintInfo1* info)
{
	obj->getInfo1NonVirtual(info);
}

void btGeneric6DofConstraint_getInfo2NonVirtual(btGeneric6DofConstraint* obj, btTypedConstraint::btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* linVelA, const btScalar* linVelB, const btScalar* angVelA, const btScalar* angVelB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	VECTOR3_CONV(linVelA);
	VECTOR3_CONV(linVelB);
	VECTOR3_CONV(angVelA);
	VECTOR3_CONV(angVelB);
	obj->getInfo2NonVirtual(info, TRANSFORM_USE(transA), TRANSFORM_USE(transB), VECTOR3_USE(linVelA), VECTOR3_USE(linVelB), VECTOR3_USE(angVelA), VECTOR3_USE(angVelB));
}

void btGeneric6DofConstraint_getLinearLowerLimit(btGeneric6DofConstraint* obj, btScalar* linearLower)
{
	VECTOR3_DEF(linearLower);
	obj->getLinearLowerLimit(VECTOR3_USE(linearLower));
	VECTOR3_DEF_OUT(linearLower);
}

void btGeneric6DofConstraint_getLinearUpperLimit(btGeneric6DofConstraint* obj, btScalar* linearUpper)
{
	VECTOR3_DEF(linearUpper);
	obj->getLinearUpperLimit(VECTOR3_USE(linearUpper));
	VECTOR3_DEF_OUT(linearUpper);
}

btScalar btGeneric6DofConstraint_getRelativePivotPosition(btGeneric6DofConstraint* obj, int axis_index)
{
	return obj->getRelativePivotPosition(axis_index);
}

btRotationalLimitMotor* btGeneric6DofConstraint_getRotationalLimitMotor(btGeneric6DofConstraint* obj, int index)
{
	return obj->getRotationalLimitMotor(index);
}

btTranslationalLimitMotor* btGeneric6DofConstraint_getTranslationalLimitMotor(btGeneric6DofConstraint* obj)
{
	return obj->getTranslationalLimitMotor();
}

bool btGeneric6DofConstraint_getUseFrameOffset(btGeneric6DofConstraint* obj)
{
	return obj->getUseFrameOffset();
}

bool btGeneric6DofConstraint_getUseLinearReferenceFrameA(btGeneric6DofConstraint* obj)
{
	return obj->getUseLinearReferenceFrameA();
}

bool btGeneric6DofConstraint_getUseSolveConstraintObsolete(btGeneric6DofConstraint* obj)
{
	return obj->m_useSolveConstraintObsolete;
}

bool btGeneric6DofConstraint_isLimited(btGeneric6DofConstraint* obj, int limitIndex)
{
	return obj->isLimited(limitIndex);
}

void btGeneric6DofConstraint_setAngularLowerLimit(btGeneric6DofConstraint* obj, const btScalar* angularLower)
{
	VECTOR3_CONV(angularLower);
	obj->setAngularLowerLimit(VECTOR3_USE(angularLower));
}

void btGeneric6DofConstraint_setAngularUpperLimit(btGeneric6DofConstraint* obj, const btScalar* angularUpper)
{
	VECTOR3_CONV(angularUpper);
	obj->setAngularUpperLimit(VECTOR3_USE(angularUpper));
}

void btGeneric6DofConstraint_setAxis(btGeneric6DofConstraint* obj, const btScalar* axis1, const btScalar* axis2)
{
	VECTOR3_CONV(axis1);
	VECTOR3_CONV(axis2);
	obj->setAxis(VECTOR3_USE(axis1), VECTOR3_USE(axis2));
}

void btGeneric6DofConstraint_setFrames(btGeneric6DofConstraint* obj, const btScalar* frameA, const btScalar* frameB)
{
	TRANSFORM_CONV(frameA);
	TRANSFORM_CONV(frameB);
	obj->setFrames(TRANSFORM_USE(frameA), TRANSFORM_USE(frameB));
}

void btGeneric6DofConstraint_setLimit(btGeneric6DofConstraint* obj, int axis, btScalar lo, btScalar hi)
{
	obj->setLimit(axis, lo, hi);
}

void btGeneric6DofConstraint_setLinearLowerLimit(btGeneric6DofConstraint* obj, const btScalar* linearLower)
{
	VECTOR3_CONV(linearLower);
	obj->setLinearLowerLimit(VECTOR3_USE(linearLower));
}

void btGeneric6DofConstraint_setLinearUpperLimit(btGeneric6DofConstraint* obj, const btScalar* linearUpper)
{
	VECTOR3_CONV(linearUpper);
	obj->setLinearUpperLimit(VECTOR3_USE(linearUpper));
}

void btGeneric6DofConstraint_setUseFrameOffset(btGeneric6DofConstraint* obj, bool frameOffsetOnOff)
{
	obj->setUseFrameOffset(frameOffsetOnOff);
}

void btGeneric6DofConstraint_setUseLinearReferenceFrameA(btGeneric6DofConstraint* obj, bool linearReferenceFrameA)
{
	obj->setUseLinearReferenceFrameA(linearReferenceFrameA);
}

void btGeneric6DofConstraint_setUseSolveConstraintObsolete(btGeneric6DofConstraint* obj, bool value)
{
	obj->m_useSolveConstraintObsolete = value;
}

bool btGeneric6DofConstraint_testAngularLimitMotor(btGeneric6DofConstraint* obj, int axis_index)
{
	return obj->testAngularLimitMotor(axis_index);
}

void btGeneric6DofConstraint_updateRHS(btGeneric6DofConstraint* obj, btScalar timeStep)
{
	obj->updateRHS(timeStep);
}
