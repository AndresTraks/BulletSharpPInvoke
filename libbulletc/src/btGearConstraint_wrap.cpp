#include <BulletDynamics/ConstraintSolver/btGearConstraint.h>

#include "conversion.h"
#include "btGearConstraint_wrap.h"

btGearConstraint* btGearConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* axisInA, const btScalar* axisInB)
{
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return new btGearConstraint(*rbA, *rbB, VECTOR3_USE(axisInA), VECTOR3_USE(axisInB));
}

btGearConstraint* btGearConstraint_new2(btRigidBody* rbA, btRigidBody* rbB, const btScalar* axisInA, const btScalar* axisInB, btScalar ratio)
{
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return new btGearConstraint(*rbA, *rbB, VECTOR3_USE(axisInA), VECTOR3_USE(axisInB), ratio);
}

void btGearConstraint_getAxisA(btGearConstraint* obj, btScalar* axisA)
{
	VECTOR3_OUT(&obj->getAxisA(), axisA);
}

void btGearConstraint_getAxisB(btGearConstraint* obj, btScalar* axisB)
{
	VECTOR3_OUT(&obj->getAxisB(), axisB);
}

btScalar btGearConstraint_getRatio(btGearConstraint* obj)
{
	return obj->getRatio();
}

void btGearConstraint_setAxisA(btGearConstraint* obj, btScalar* axisA)
{
	VECTOR3_CONV(axisA);
	obj->setAxisA(VECTOR3_USE(axisA));
}

void btGearConstraint_setAxisB(btGearConstraint* obj, btScalar* axisB)
{
	VECTOR3_CONV(axisB);
	obj->setAxisB(VECTOR3_USE(axisB));
}

void btGearConstraint_setRatio(btGearConstraint* obj, btScalar ratio)
{
	obj->setRatio(ratio);
}
