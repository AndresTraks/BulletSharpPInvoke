#include <BulletSoftBody/btDeformableGravityForce.h>

#include "conversion.h"
#include "btDeformableGravityForce_wrap.h"

btDeformableGravityForce* btDeformableGravityForce_new(const btVector3* gravity)
{
	BTVECTOR3_IN(gravity);
	return new btDeformableGravityForce(BTVECTOR3_USE(gravity));
}
