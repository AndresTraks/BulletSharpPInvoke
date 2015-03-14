#include <BulletDynamics/ConstraintSolver/btFixedConstraint.h>

#include "conversion.h"
#include "btFixedConstraint_wrap.h"

btFixedConstraint* btFixedConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return new btFixedConstraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB));
}
