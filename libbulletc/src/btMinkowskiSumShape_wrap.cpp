#include <BulletCollision/CollisionShapes/btMinkowskiSumShape.h>

#include "conversion.h"
#include "btMinkowskiSumShape_wrap.h"

btMinkowskiSumShape* btMinkowskiSumShape_new(const btConvexShape* shapeA, const btConvexShape* shapeB)
{
	return new btMinkowskiSumShape(shapeA, shapeB);
}

const btConvexShape* btMinkowskiSumShape_getShapeA(btMinkowskiSumShape* obj)
{
	return obj->getShapeA();
}

const btConvexShape* btMinkowskiSumShape_getShapeB(btMinkowskiSumShape* obj)
{
	return obj->getShapeB();
}

void btMinkowskiSumShape_getTransformA(btMinkowskiSumShape* obj, btScalar* transA)
{
	TRANSFORM_OUT(&obj->getTransformA(), transA);
}

void btMinkowskiSumShape_GetTransformB(btMinkowskiSumShape* obj, btScalar* transB)
{
	TRANSFORM_OUT(&obj->GetTransformB(), transB);
}

void btMinkowskiSumShape_setTransformA(btMinkowskiSumShape* obj, const btScalar* transA)
{
	TRANSFORM_CONV(transA);
	obj->setTransformA(TRANSFORM_USE(transA));
}

void btMinkowskiSumShape_setTransformB(btMinkowskiSumShape* obj, const btScalar* transB)
{
	TRANSFORM_CONV(transB);
	obj->setTransformB(TRANSFORM_USE(transB));
}
