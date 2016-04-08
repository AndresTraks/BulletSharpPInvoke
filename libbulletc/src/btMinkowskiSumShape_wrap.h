#include "main.h"

extern "C"
{
	EXPORT btMinkowskiSumShape* btMinkowskiSumShape_new(const btConvexShape* shapeA, const btConvexShape* shapeB);
	EXPORT const btConvexShape* btMinkowskiSumShape_getShapeA(btMinkowskiSumShape* obj);
	EXPORT const btConvexShape* btMinkowskiSumShape_getShapeB(btMinkowskiSumShape* obj);
	EXPORT void btMinkowskiSumShape_getTransformA(btMinkowskiSumShape* obj, btTransform* transA);
	EXPORT void btMinkowskiSumShape_GetTransformB(btMinkowskiSumShape* obj, btTransform* transB);
	EXPORT void btMinkowskiSumShape_setTransformA(btMinkowskiSumShape* obj, const btTransform* transA);
	EXPORT void btMinkowskiSumShape_setTransformB(btMinkowskiSumShape* obj, const btTransform* transB);
}
