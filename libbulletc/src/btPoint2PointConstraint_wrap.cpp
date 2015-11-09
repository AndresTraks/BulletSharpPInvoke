#include <BulletDynamics/ConstraintSolver/btPoint2PointConstraint.h>

#include "conversion.h"
#include "btPoint2PointConstraint_wrap.h"

btConstraintSetting* btConstraintSetting_new()
{
	return new btConstraintSetting();
}

btScalar btConstraintSetting_getDamping(btConstraintSetting* obj)
{
	return obj->m_damping;
}

btScalar btConstraintSetting_getImpulseClamp(btConstraintSetting* obj)
{
	return obj->m_impulseClamp;
}

btScalar btConstraintSetting_getTau(btConstraintSetting* obj)
{
	return obj->m_tau;
}

void btConstraintSetting_setDamping(btConstraintSetting* obj, btScalar value)
{
	obj->m_damping = value;
}

void btConstraintSetting_setImpulseClamp(btConstraintSetting* obj, btScalar value)
{
	obj->m_impulseClamp = value;
}

void btConstraintSetting_setTau(btConstraintSetting* obj, btScalar value)
{
	obj->m_tau = value;
}

void btConstraintSetting_delete(btConstraintSetting* obj)
{
	delete obj;
}


btPoint2PointConstraint* btPoint2PointConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(pivotInB);
	return new btPoint2PointConstraint(*rbA, *rbB, VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB));
}

btPoint2PointConstraint* btPoint2PointConstraint_new2(btRigidBody* rbA, const btScalar* pivotInA)
{
	VECTOR3_CONV(pivotInA);
	return new btPoint2PointConstraint(*rbA, VECTOR3_USE(pivotInA));
}

int btPoint2PointConstraint_getFlags(btPoint2PointConstraint* obj)
{
	return obj->getFlags();
}

void btPoint2PointConstraint_getInfo1NonVirtual(btPoint2PointConstraint* obj, btTypedConstraint::btConstraintInfo1* info)
{
	obj->getInfo1NonVirtual(info);
}

void btPoint2PointConstraint_getInfo2NonVirtual(btPoint2PointConstraint* obj, btTypedConstraint::btConstraintInfo2* info, const btScalar* body0_trans, const btScalar* body1_trans)
{
	TRANSFORM_CONV(body0_trans);
	TRANSFORM_CONV(body1_trans);
	obj->getInfo2NonVirtual(info, TRANSFORM_USE(body0_trans), TRANSFORM_USE(body1_trans));
}

void btPoint2PointConstraint_getPivotInA(btPoint2PointConstraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getPivotInA(), value);
}

void btPoint2PointConstraint_getPivotInB(btPoint2PointConstraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getPivotInB(), value);
}

btConstraintSetting* btPoint2PointConstraint_getSetting(btPoint2PointConstraint* obj)
{
	return &obj->m_setting;
}

bool btPoint2PointConstraint_getUseSolveConstraintObsolete(btPoint2PointConstraint* obj)
{
	return obj->m_useSolveConstraintObsolete;
}

void btPoint2PointConstraint_setPivotA(btPoint2PointConstraint* obj, const btScalar* pivotA)
{
	VECTOR3_CONV(pivotA);
	obj->setPivotA(VECTOR3_USE(pivotA));
}

void btPoint2PointConstraint_setPivotB(btPoint2PointConstraint* obj, const btScalar* pivotB)
{
	VECTOR3_CONV(pivotB);
	obj->setPivotB(VECTOR3_USE(pivotB));
}

void btPoint2PointConstraint_setUseSolveConstraintObsolete(btPoint2PointConstraint* obj, bool value)
{
	obj->m_useSolveConstraintObsolete = value;
}

void btPoint2PointConstraint_updateRHS(btPoint2PointConstraint* obj, btScalar timeStep)
{
	obj->updateRHS(timeStep);
}
