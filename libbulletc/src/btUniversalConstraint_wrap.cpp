#include <BulletDynamics/ConstraintSolver/btUniversalConstraint.h>

#include "conversion.h"
#include "btUniversalConstraint_wrap.h"

btUniversalConstraint* btUniversalConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* anchor, const btScalar* axis1, const btScalar* axis2)
{
	VECTOR3_CONV(anchor);
	VECTOR3_CONV(axis1);
	VECTOR3_CONV(axis2);
	return new btUniversalConstraint(*rbA, *rbB, VECTOR3_USE(anchor), VECTOR3_USE(axis1), VECTOR3_USE(axis2));
}

void btUniversalConstraint_getAnchor(btUniversalConstraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAnchor(), value);
}

void btUniversalConstraint_getAnchor2(btUniversalConstraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAnchor2(), value);
}

btScalar btUniversalConstraint_getAngle1(btUniversalConstraint* obj)
{
	return obj->getAngle1();
}

btScalar btUniversalConstraint_getAngle2(btUniversalConstraint* obj)
{
	return obj->getAngle2();
}

void btUniversalConstraint_getAxis1(btUniversalConstraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAxis1(), value);
}

void btUniversalConstraint_getAxis2(btUniversalConstraint* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAxis2(), value);
}

void btUniversalConstraint_setLowerLimit(btUniversalConstraint* obj, btScalar ang1min, btScalar ang2min)
{
	obj->setLowerLimit(ang1min, ang2min);
}

void btUniversalConstraint_setUpperLimit(btUniversalConstraint* obj, btScalar ang1max, btScalar ang2max)
{
	obj->setUpperLimit(ang1max, ang2max);
}
