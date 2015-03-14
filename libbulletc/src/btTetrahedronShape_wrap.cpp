#include <BulletCollision/CollisionShapes/btTetrahedronShape.h>

#include "conversion.h"
#include "btTetrahedronShape_wrap.h"

btBU_Simplex1to4* btBU_Simplex1to4_new()
{
	return new btBU_Simplex1to4();
}

btBU_Simplex1to4* btBU_Simplex1to4_new2(const btScalar* pt0)
{
	VECTOR3_CONV(pt0);
	return new btBU_Simplex1to4(VECTOR3_USE(pt0));
}

btBU_Simplex1to4* btBU_Simplex1to4_new3(const btScalar* pt0, const btScalar* pt1)
{
	VECTOR3_CONV(pt0);
	VECTOR3_CONV(pt1);
	return new btBU_Simplex1to4(VECTOR3_USE(pt0), VECTOR3_USE(pt1));
}

btBU_Simplex1to4* btBU_Simplex1to4_new4(const btScalar* pt0, const btScalar* pt1, const btScalar* pt2)
{
	VECTOR3_CONV(pt0);
	VECTOR3_CONV(pt1);
	VECTOR3_CONV(pt2);
	return new btBU_Simplex1to4(VECTOR3_USE(pt0), VECTOR3_USE(pt1), VECTOR3_USE(pt2));
}

btBU_Simplex1to4* btBU_Simplex1to4_new5(const btScalar* pt0, const btScalar* pt1, const btScalar* pt2, const btScalar* pt3)
{
	VECTOR3_CONV(pt0);
	VECTOR3_CONV(pt1);
	VECTOR3_CONV(pt2);
	VECTOR3_CONV(pt3);
	return new btBU_Simplex1to4(VECTOR3_USE(pt0), VECTOR3_USE(pt1), VECTOR3_USE(pt2), VECTOR3_USE(pt3));
}

void btBU_Simplex1to4_addVertex(btBU_Simplex1to4* obj, const btScalar* pt)
{
	VECTOR3_CONV(pt);
	obj->addVertex(VECTOR3_USE(pt));
}

int btBU_Simplex1to4_getIndex(btBU_Simplex1to4* obj, int i)
{
	return obj->getIndex(i);
}

void btBU_Simplex1to4_reset(btBU_Simplex1to4* obj)
{
	obj->reset();
}
