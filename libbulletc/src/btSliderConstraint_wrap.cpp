#include <BulletDynamics/ConstraintSolver/btSliderConstraint.h>

#include "conversion.h"
#include "btSliderConstraint_wrap.h"

btSliderConstraint* btSliderConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return new btSliderConstraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

btSliderConstraint* btSliderConstraint_new2(btRigidBody* rbB, const btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInB);
	return new btSliderConstraint(*rbB, TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

void btSliderConstraint_calculateTransforms(btSliderConstraint* obj, const btScalar* transA, const btScalar* transB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	obj->calculateTransforms(TRANSFORM_USE(transA), TRANSFORM_USE(transB));
}

void btSliderConstraint_getAncorInA(btSliderConstraint* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getAncorInA(), value);
}

void btSliderConstraint_getAncorInB(btSliderConstraint* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getAncorInB(), value);
}

btScalar btSliderConstraint_getAngDepth(btSliderConstraint* obj)
{
	return obj->getAngDepth();
}

btScalar btSliderConstraint_getAngularPos(btSliderConstraint* obj)
{
	return obj->getAngularPos();
}

void btSliderConstraint_getCalculatedTransformA(btSliderConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getCalculatedTransformA(), value);
}

void btSliderConstraint_getCalculatedTransformB(btSliderConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getCalculatedTransformB(), value);
}

btScalar btSliderConstraint_getDampingDirAng(btSliderConstraint* obj)
{
	return obj->getDampingDirAng();
}

btScalar btSliderConstraint_getDampingDirLin(btSliderConstraint* obj)
{
	return obj->getDampingDirLin();
}

btScalar btSliderConstraint_getDampingLimAng(btSliderConstraint* obj)
{
	return obj->getDampingLimAng();
}

btScalar btSliderConstraint_getDampingLimLin(btSliderConstraint* obj)
{
	return obj->getDampingLimLin();
}

btScalar btSliderConstraint_getDampingOrthoAng(btSliderConstraint* obj)
{
	return obj->getDampingOrthoAng();
}

btScalar btSliderConstraint_getDampingOrthoLin(btSliderConstraint* obj)
{
	return obj->getDampingOrthoLin();
}

int btSliderConstraint_getFlags(btSliderConstraint* obj)
{
	return obj->getFlags();
}

void btSliderConstraint_getFrameOffsetA(btSliderConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetA(), value);
}

void btSliderConstraint_getFrameOffsetB(btSliderConstraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetB(), value);
}

void btSliderConstraint_getInfo1NonVirtual(btSliderConstraint* obj, btTypedConstraint::btConstraintInfo1* info)
{
	obj->getInfo1NonVirtual(info);
}

void btSliderConstraint_getInfo2NonVirtual(btSliderConstraint* obj, btTypedConstraint::btConstraintInfo2* info, const btScalar* transA, const btScalar* transB, const btScalar* linVelA, const btScalar* linVelB, btScalar rbAinvMass, btScalar rbBinvMass)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	VECTOR3_CONV(linVelA);
	VECTOR3_CONV(linVelB);
	obj->getInfo2NonVirtual(info, TRANSFORM_USE(transA), TRANSFORM_USE(transB), VECTOR3_USE(linVelA), VECTOR3_USE(linVelB), rbAinvMass, rbBinvMass);
}

btScalar btSliderConstraint_getLinDepth(btSliderConstraint* obj)
{
	return obj->getLinDepth();
}

btScalar btSliderConstraint_getLinearPos(btSliderConstraint* obj)
{
	return obj->getLinearPos();
}

btScalar btSliderConstraint_getLowerAngLimit(btSliderConstraint* obj)
{
	return obj->getLowerAngLimit();
}

btScalar btSliderConstraint_getLowerLinLimit(btSliderConstraint* obj)
{
	return obj->getLowerLinLimit();
}

btScalar btSliderConstraint_getMaxAngMotorForce(btSliderConstraint* obj)
{
	return obj->getMaxAngMotorForce();
}

btScalar btSliderConstraint_getMaxLinMotorForce(btSliderConstraint* obj)
{
	return obj->getMaxLinMotorForce();
}

bool btSliderConstraint_getPoweredAngMotor(btSliderConstraint* obj)
{
	return obj->getPoweredAngMotor();
}

bool btSliderConstraint_getPoweredLinMotor(btSliderConstraint* obj)
{
	return obj->getPoweredLinMotor();
}

btScalar btSliderConstraint_getRestitutionDirAng(btSliderConstraint* obj)
{
	return obj->getRestitutionDirAng();
}

btScalar btSliderConstraint_getRestitutionDirLin(btSliderConstraint* obj)
{
	return obj->getRestitutionDirLin();
}

btScalar btSliderConstraint_getRestitutionLimAng(btSliderConstraint* obj)
{
	return obj->getRestitutionLimAng();
}

btScalar btSliderConstraint_getRestitutionLimLin(btSliderConstraint* obj)
{
	return obj->getRestitutionLimLin();
}

btScalar btSliderConstraint_getRestitutionOrthoAng(btSliderConstraint* obj)
{
	return obj->getRestitutionOrthoAng();
}

btScalar btSliderConstraint_getRestitutionOrthoLin(btSliderConstraint* obj)
{
	return obj->getRestitutionOrthoLin();
}

btScalar btSliderConstraint_getSoftnessDirAng(btSliderConstraint* obj)
{
	return obj->getSoftnessDirAng();
}

btScalar btSliderConstraint_getSoftnessDirLin(btSliderConstraint* obj)
{
	return obj->getSoftnessDirLin();
}

btScalar btSliderConstraint_getSoftnessLimAng(btSliderConstraint* obj)
{
	return obj->getSoftnessLimAng();
}

btScalar btSliderConstraint_getSoftnessLimLin(btSliderConstraint* obj)
{
	return obj->getSoftnessLimLin();
}

btScalar btSliderConstraint_getSoftnessOrthoAng(btSliderConstraint* obj)
{
	return obj->getSoftnessOrthoAng();
}

btScalar btSliderConstraint_getSoftnessOrthoLin(btSliderConstraint* obj)
{
	return obj->getSoftnessOrthoLin();
}

bool btSliderConstraint_getSolveAngLimit(btSliderConstraint* obj)
{
	return obj->getSolveAngLimit();
}

bool btSliderConstraint_getSolveLinLimit(btSliderConstraint* obj)
{
	return obj->getSolveLinLimit();
}

btScalar btSliderConstraint_getTargetAngMotorVelocity(btSliderConstraint* obj)
{
	return obj->getTargetAngMotorVelocity();
}

btScalar btSliderConstraint_getTargetLinMotorVelocity(btSliderConstraint* obj)
{
	return obj->getTargetLinMotorVelocity();
}

btScalar btSliderConstraint_getUpperAngLimit(btSliderConstraint* obj)
{
	return obj->getUpperAngLimit();
}

btScalar btSliderConstraint_getUpperLinLimit(btSliderConstraint* obj)
{
	return obj->getUpperLinLimit();
}

bool btSliderConstraint_getUseFrameOffset(btSliderConstraint* obj)
{
	return obj->getUseFrameOffset();
}

bool btSliderConstraint_getUseLinearReferenceFrameA(btSliderConstraint* obj)
{
	return obj->getUseLinearReferenceFrameA();
}

void btSliderConstraint_setDampingDirAng(btSliderConstraint* obj, btScalar dampingDirAng)
{
	obj->setDampingDirAng(dampingDirAng);
}

void btSliderConstraint_setDampingDirLin(btSliderConstraint* obj, btScalar dampingDirLin)
{
	obj->setDampingDirLin(dampingDirLin);
}

void btSliderConstraint_setDampingLimAng(btSliderConstraint* obj, btScalar dampingLimAng)
{
	obj->setDampingLimAng(dampingLimAng);
}

void btSliderConstraint_setDampingLimLin(btSliderConstraint* obj, btScalar dampingLimLin)
{
	obj->setDampingLimLin(dampingLimLin);
}

void btSliderConstraint_setDampingOrthoAng(btSliderConstraint* obj, btScalar dampingOrthoAng)
{
	obj->setDampingOrthoAng(dampingOrthoAng);
}

void btSliderConstraint_setDampingOrthoLin(btSliderConstraint* obj, btScalar dampingOrthoLin)
{
	obj->setDampingOrthoLin(dampingOrthoLin);
}

void btSliderConstraint_setFrames(btSliderConstraint* obj, const btScalar* frameA, const btScalar* frameB)
{
	TRANSFORM_CONV(frameA);
	TRANSFORM_CONV(frameB);
	obj->setFrames(TRANSFORM_USE(frameA), TRANSFORM_USE(frameB));
}

void btSliderConstraint_setLowerAngLimit(btSliderConstraint* obj, btScalar lowerLimit)
{
	obj->setLowerAngLimit(lowerLimit);
}

void btSliderConstraint_setLowerLinLimit(btSliderConstraint* obj, btScalar lowerLimit)
{
	obj->setLowerLinLimit(lowerLimit);
}

void btSliderConstraint_setMaxAngMotorForce(btSliderConstraint* obj, btScalar maxAngMotorForce)
{
	obj->setMaxAngMotorForce(maxAngMotorForce);
}

void btSliderConstraint_setMaxLinMotorForce(btSliderConstraint* obj, btScalar maxLinMotorForce)
{
	obj->setMaxLinMotorForce(maxLinMotorForce);
}

void btSliderConstraint_setPoweredAngMotor(btSliderConstraint* obj, bool onOff)
{
	obj->setPoweredAngMotor(onOff);
}

void btSliderConstraint_setPoweredLinMotor(btSliderConstraint* obj, bool onOff)
{
	obj->setPoweredLinMotor(onOff);
}

void btSliderConstraint_setRestitutionDirAng(btSliderConstraint* obj, btScalar restitutionDirAng)
{
	obj->setRestitutionDirAng(restitutionDirAng);
}

void btSliderConstraint_setRestitutionDirLin(btSliderConstraint* obj, btScalar restitutionDirLin)
{
	obj->setRestitutionDirLin(restitutionDirLin);
}

void btSliderConstraint_setRestitutionLimAng(btSliderConstraint* obj, btScalar restitutionLimAng)
{
	obj->setRestitutionLimAng(restitutionLimAng);
}

void btSliderConstraint_setRestitutionLimLin(btSliderConstraint* obj, btScalar restitutionLimLin)
{
	obj->setRestitutionLimLin(restitutionLimLin);
}

void btSliderConstraint_setRestitutionOrthoAng(btSliderConstraint* obj, btScalar restitutionOrthoAng)
{
	obj->setRestitutionOrthoAng(restitutionOrthoAng);
}

void btSliderConstraint_setRestitutionOrthoLin(btSliderConstraint* obj, btScalar restitutionOrthoLin)
{
	obj->setRestitutionOrthoLin(restitutionOrthoLin);
}

void btSliderConstraint_setSoftnessDirAng(btSliderConstraint* obj, btScalar softnessDirAng)
{
	obj->setSoftnessDirAng(softnessDirAng);
}

void btSliderConstraint_setSoftnessDirLin(btSliderConstraint* obj, btScalar softnessDirLin)
{
	obj->setSoftnessDirLin(softnessDirLin);
}

void btSliderConstraint_setSoftnessLimAng(btSliderConstraint* obj, btScalar softnessLimAng)
{
	obj->setSoftnessLimAng(softnessLimAng);
}

void btSliderConstraint_setSoftnessLimLin(btSliderConstraint* obj, btScalar softnessLimLin)
{
	obj->setSoftnessLimLin(softnessLimLin);
}

void btSliderConstraint_setSoftnessOrthoAng(btSliderConstraint* obj, btScalar softnessOrthoAng)
{
	obj->setSoftnessOrthoAng(softnessOrthoAng);
}

void btSliderConstraint_setSoftnessOrthoLin(btSliderConstraint* obj, btScalar softnessOrthoLin)
{
	obj->setSoftnessOrthoLin(softnessOrthoLin);
}

void btSliderConstraint_setTargetAngMotorVelocity(btSliderConstraint* obj, btScalar targetAngMotorVelocity)
{
	obj->setTargetAngMotorVelocity(targetAngMotorVelocity);
}

void btSliderConstraint_setTargetLinMotorVelocity(btSliderConstraint* obj, btScalar targetLinMotorVelocity)
{
	obj->setTargetLinMotorVelocity(targetLinMotorVelocity);
}

void btSliderConstraint_setUpperAngLimit(btSliderConstraint* obj, btScalar upperLimit)
{
	obj->setUpperAngLimit(upperLimit);
}

void btSliderConstraint_setUpperLinLimit(btSliderConstraint* obj, btScalar upperLimit)
{
	obj->setUpperLinLimit(upperLimit);
}

void btSliderConstraint_setUseFrameOffset(btSliderConstraint* obj, bool frameOffsetOnOff)
{
	obj->setUseFrameOffset(frameOffsetOnOff);
}

void btSliderConstraint_testAngLimits(btSliderConstraint* obj)
{
	obj->testAngLimits();
}

void btSliderConstraint_testLinLimits(btSliderConstraint* obj)
{
	obj->testLinLimits();
}
