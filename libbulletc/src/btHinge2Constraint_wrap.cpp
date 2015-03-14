#include <BulletDynamics/ConstraintSolver/btHinge2Constraint.h>

#include "conversion.h"
#include "btHinge2Constraint_wrap.h"

btHinge2Constraint* btHinge2Constraint_new(btRigidBody* rbA, btRigidBody* rbB, btScalar* anchor, btScalar* axis1, btScalar* axis2)
{
	VECTOR3_CONV(anchor);
	VECTOR3_CONV(axis1);
	VECTOR3_CONV(axis2);
	return new btHinge2Constraint(*rbA, *rbB, VECTOR3_USE(anchor), VECTOR3_USE(axis1), VECTOR3_USE(axis2));
}

void btHinge2Constraint_getAnchor(btHinge2Constraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAnchor(), value);
}

void btHinge2Constraint_getAnchor2(btHinge2Constraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAnchor2(), value);
}

btScalar btHinge2Constraint_getAngle1(btHinge2Constraint* obj)
{
	return obj->getAngle1();
}

btScalar btHinge2Constraint_getAngle2(btHinge2Constraint* obj)
{
	return obj->getAngle2();
}

void btHinge2Constraint_getAxis1(btHinge2Constraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAxis1(), value);
}

void btHinge2Constraint_getAxis2(btHinge2Constraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAxis2(), value);
}

void btHinge2Constraint_setLowerLimit(btHinge2Constraint* obj, btScalar ang1min)
{
	obj->setLowerLimit(ang1min);
}

void btHinge2Constraint_setUpperLimit(btHinge2Constraint* obj, btScalar ang1max)
{
	obj->setUpperLimit(ang1max);
}
