#include <BulletCollision/CollisionShapes/btConcaveShape.h>

#include "conversion.h"
#include "btConcaveShape_wrap.h"

void btConcaveShape_processAllTriangles(btConcaveShape* obj, btTriangleCallback* callback, const btScalar* aabbMin, const btScalar* aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->processAllTriangles(callback, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}
