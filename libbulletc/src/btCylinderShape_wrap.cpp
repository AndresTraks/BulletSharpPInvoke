#include <BulletCollision/CollisionShapes/btCylinderShape.h>

#include "conversion.h"
#include "btCylinderShape_wrap.h"

btCylinderShape* btCylinderShape_new(const btScalar* halfExtents)
{
	VECTOR3_CONV(halfExtents);
	return new btCylinderShape(VECTOR3_USE(halfExtents));
}

btCylinderShape* btCylinderShape_new2(btScalar halfExtentX, btScalar halfExtentY, btScalar halfExtentZ)
{
	return new btCylinderShape(btVector3(halfExtentX, halfExtentY, halfExtentZ));
}

void btCylinderShape_getHalfExtentsWithMargin(btCylinderShape* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getHalfExtentsWithMargin(), value);
}

void btCylinderShape_getHalfExtentsWithoutMargin(btCylinderShape* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getHalfExtentsWithoutMargin(), value);
}

btScalar btCylinderShape_getRadius(btCylinderShape* obj)
{
	return obj->getRadius();
}

int btCylinderShape_getUpAxis(btCylinderShape* obj)
{
	return obj->getUpAxis();
}


btCylinderShapeX* btCylinderShapeX_new(const btScalar* halfExtents)
{
	VECTOR3_CONV(halfExtents);
	return new btCylinderShapeX(VECTOR3_USE(halfExtents));
}

btCylinderShapeX* btCylinderShapeX_new2(btScalar halfExtentX, btScalar halfExtentY, btScalar halfExtentZ)
{
	return new btCylinderShapeX(btVector3(halfExtentX, halfExtentY, halfExtentZ));
}


btCylinderShapeZ* btCylinderShapeZ_new(const btScalar* halfExtents)
{
	VECTOR3_CONV(halfExtents);
	return new btCylinderShapeZ(VECTOR3_USE(halfExtents));
}

btCylinderShapeZ* btCylinderShapeZ_new2(btScalar halfExtentX, btScalar halfExtentY, btScalar halfExtentZ)
{
	return new btCylinderShapeZ(btVector3(halfExtentX, halfExtentY, halfExtentZ));
}
