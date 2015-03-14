#include <BulletCollision/CollisionShapes/btConeShape.h>

#include "btConeShape_wrap.h"

btConeShape* btConeShape_new(btScalar radius, btScalar height)
{
	return new btConeShape(radius, height);
}

int btConeShape_getConeUpIndex(btConeShape* obj)
{
	return obj->getConeUpIndex();
}

btScalar btConeShape_getHeight(btConeShape* obj)
{
	return obj->getHeight();
}

btScalar btConeShape_getRadius(btConeShape* obj)
{
	return obj->getRadius();
}

void btConeShape_setConeUpIndex(btConeShape* obj, int upIndex)
{
	obj->setConeUpIndex(upIndex);
}


btConeShapeX* btConeShapeX_new(btScalar radius, btScalar height)
{
	return new btConeShapeX(radius, height);
}


btConeShapeZ* btConeShapeZ_new(btScalar radius, btScalar height)
{
	return new btConeShapeZ(radius, height);
}
