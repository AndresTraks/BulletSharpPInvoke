#include <BulletCollision/CollisionShapes/btCompoundShape.h>
#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "collections.h"

btCompoundShapeChild* btCompoundShapeChild_array_at(btCompoundShapeChild* a, int n)
{
	return &a[n];
}

btSoftBody::Node* btSoftBodyNodePtrArray_at(btSoftBody::Node** obj, int n)
{
	return obj[n];
}

void btSoftBodyNodePtrArray_set(btSoftBodyNodePtrArray* obj, btSoftBody_Node* value, int index)
{
	obj[index] = value;
}

void btVector3_array_at(const btVector3* a, int n, btScalar* value)
{
	VECTOR3_OUT(&a[n], value);
}

void btVector3_array_set(btVector3* obj, int n, const btScalar* value)
{
	VECTOR3_IN(value, &obj[n]);
}

btAlignedVector3Array* btAlignedVector3Array_new()
{
	return new btAlignedVector3Array();
}

void btAlignedVector3Array_at(btAlignedVector3Array* obj, int n, btScalar* value)
{
	VECTOR3_OUT(&obj->at(n), value);
}

void btAlignedVector3Array_push_back(btAlignedVector3Array* obj, const btScalar* value)
{
	VECTOR3_CONV(value);
	obj->push_back(VECTOR3_USE(value));
}

void btAlignedVector3Array_push_back2(btAlignedVector3Array* obj, const btScalar* value) // btVector4
{
	VECTOR4_CONV(value);
	obj->push_back(VECTOR4_USE(value));
}

void btAlignedVector3Array_set(btAlignedVector3Array* obj, int n, const btScalar* value)
{
	VECTOR3_IN(value, &obj->at(n));
}

int btAlignedVector3Array_size(btAlignedVector3Array* obj)
{
	return obj->size();
}

void btAlignedVector3Array_delete(btAlignedVector3Array* obj)
{
	delete obj;
}
