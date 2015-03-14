#include <BulletCollision/CollisionShapes/btScaledBvhTriangleMeshShape.h>

#include "conversion.h"
#include "btScaledBvhTriangleMeshShape_wrap.h"

btScaledBvhTriangleMeshShape* btScaledBvhTriangleMeshShape_new(btBvhTriangleMeshShape* childShape, const btScalar* localScaling)
{
	VECTOR3_CONV(localScaling);
	return new btScaledBvhTriangleMeshShape(childShape, VECTOR3_USE(localScaling));
}

btBvhTriangleMeshShape* btScaledBvhTriangleMeshShape_getChildShape(btScaledBvhTriangleMeshShape* obj)
{
	return obj->getChildShape();
}
