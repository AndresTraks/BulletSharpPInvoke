#include <BulletDynamics/Featherstone/btMultiBodyJointMotor.h>

#include "btMultiBodyJointMotor_wrap.h"

btMultiBodyJointMotor* btMultiBodyJointMotor_new(btMultiBody* body, int link, btScalar desiredVelocity, btScalar maxMotorImpulse)
{
	return new btMultiBodyJointMotor(body, link, desiredVelocity, maxMotorImpulse);
}

btMultiBodyJointMotor* btMultiBodyJointMotor_new2(btMultiBody* body, int link, int linkDoF, btScalar desiredVelocity, btScalar maxMotorImpulse)
{
	return new btMultiBodyJointMotor(body, link, linkDoF, desiredVelocity, maxMotorImpulse);
}

void btMultiBodyJointMotor_setVelocityTarget(btMultiBodyJointMotor* obj, btScalar velTarget)
{
	obj->setVelocityTarget(velTarget);
}
