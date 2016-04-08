#include "main.h"

extern "C"
{
	EXPORT btFixedConstraint* btFixedConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btTransform* frameInA, const btTransform* frameInB);
}
