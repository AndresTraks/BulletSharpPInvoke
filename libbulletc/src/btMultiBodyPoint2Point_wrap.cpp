#include <BulletDynamics/Dynamics/btRigidBody.h>
#include <BulletDynamics/Featherstone/btMultiBodyPoint2Point.h>

#include "conversion.h"
#include "btMultiBodyPoint2Point_wrap.h"

btMultiBodyPoint2Point* btMultiBodyPoint2Point_new(btMultiBody* body, int link, btRigidBody* bodyB, const btScalar* pivotInA, const btScalar* pivotInB)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(pivotInB);
	return new btMultiBodyPoint2Point(body, link, bodyB, VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB));
}

btMultiBodyPoint2Point* btMultiBodyPoint2Point_new2(btMultiBody* bodyA, int linkA, btMultiBody* bodyB, int linkB, const btScalar* pivotInA, const btScalar* pivotInB)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(pivotInB);
	return new btMultiBodyPoint2Point(bodyA, linkA, bodyB, linkB, VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB));
}

void btMultiBodyPoint2Point_getPivotInB(btMultiBodyPoint2Point* obj, btScalar* pivotInB)
{
	VECTOR3_OUT(&obj->getPivotInB(), pivotInB);
}

void btMultiBodyPoint2Point_setPivotInB(btMultiBodyPoint2Point* obj, const btScalar* pivotInB)
{
	VECTOR3_CONV(pivotInB);
	obj->setPivotInB(VECTOR3_USE(pivotInB));
}
