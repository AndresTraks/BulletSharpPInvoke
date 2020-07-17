#include <BulletSoftBody/btDeformableNeoHookeanForce.h>

#include "conversion.h"
#include "btDeformableNeoHookeanForce_wrap.h"

btDeformableNeoHookeanForce* btDeformableNeoHookeanForce_new(btScalar mu, btScalar lambda, btScalar damping)
{
	return new btDeformableNeoHookeanForce(mu, lambda, damping);
}
