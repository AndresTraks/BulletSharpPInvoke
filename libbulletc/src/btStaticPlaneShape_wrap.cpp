#include <BulletCollision/CollisionShapes/btStaticPlaneShape.h>

#include "conversion.h"
#include "btStaticPlaneShape_wrap.h"

btStaticPlaneShape* btStaticPlaneShape_new(const btScalar* planeNormal, btScalar planeConstant)
{
	VECTOR3_CONV(planeNormal);
	return new btStaticPlaneShape(VECTOR3_USE(planeNormal), planeConstant);
}

btScalar btStaticPlaneShape_getPlaneConstant(btStaticPlaneShape* obj)
{
	return obj->getPlaneConstant();
}

void btStaticPlaneShape_getPlaneNormal(btStaticPlaneShape* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getPlaneNormal(), value);
}
