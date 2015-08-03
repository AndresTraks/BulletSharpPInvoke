#include <BulletCollision/BroadphaseCollision/btDbvt.h>
#include <BulletCollision/CollisionShapes/btCompoundShape.h>

#include "conversion.h"
#include "btCompoundShape_wrap.h"

btScalar btCompoundShapeChild_getChildMargin(btCompoundShapeChild* obj)
{
	return obj->m_childMargin;
}

btCollisionShape* btCompoundShapeChild_getChildShape(btCompoundShapeChild* obj)
{
	return obj->m_childShape;
}

int btCompoundShapeChild_getChildShapeType(btCompoundShapeChild* obj)
{
	return obj->m_childShapeType;
}

btDbvtNode* btCompoundShapeChild_getNode(btCompoundShapeChild* obj)
{
	return obj->m_node;
}

void btCompoundShapeChild_getTransform(btCompoundShapeChild* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->m_transform, value);
}

void btCompoundShapeChild_setChildMargin(btCompoundShapeChild* obj, btScalar value)
{
	obj->m_childMargin = value;
}

void btCompoundShapeChild_setChildShape(btCompoundShapeChild* obj, btCollisionShape* value)
{
	obj->m_childShape = value;
}

void btCompoundShapeChild_setChildShapeType(btCompoundShapeChild* obj, int value)
{
	obj->m_childShapeType = value;
}

void btCompoundShapeChild_setNode(btCompoundShapeChild* obj, btDbvtNode* value)
{
	obj->m_node = value;
}

void btCompoundShapeChild_setTransform(btCompoundShapeChild* obj, const btScalar* value)
{
	TRANSFORM_IN(value, &obj->m_transform);
}

void btCompoundShapeChild_delete(btCompoundShapeChild* obj)
{
	delete obj;
}


btCompoundShape* btCompoundShape_new()
{
	return new btCompoundShape();
}

btCompoundShape* btCompoundShape_new2(bool enableDynamicAabbTree)
{
	return new btCompoundShape(enableDynamicAabbTree);
}

btCompoundShape* btCompoundShape_new3(bool enableDynamicAabbTree, int initialChildCapacity)
{
	return new btCompoundShape(enableDynamicAabbTree, initialChildCapacity);
}

void btCompoundShape_addChildShape(btCompoundShape* obj, const btScalar* localTransform, btCollisionShape* shape)
{
	TRANSFORM_CONV(localTransform);
	obj->addChildShape(TRANSFORM_USE(localTransform), shape);
}

void btCompoundShape_calculatePrincipalAxisTransform(btCompoundShape* obj, btScalar* masses, btScalar* principal, btScalar* inertia)
{
	TRANSFORM_CONV(principal);
	VECTOR3_DEF(inertia);
	obj->calculatePrincipalAxisTransform(masses, TRANSFORM_USE(principal), VECTOR3_USE(inertia));
	TRANSFORM_DEF_OUT(principal);
	VECTOR3_DEF_OUT(inertia);
}

void btCompoundShape_createAabbTreeFromChildren(btCompoundShape* obj)
{
	obj->createAabbTreeFromChildren();
}

btCompoundShapeChild* btCompoundShape_getChildList(btCompoundShape* obj)
{
	return obj->getChildList();
}

const btCollisionShape* btCompoundShape_getChildShape(btCompoundShape* obj, int index)
{
	return obj->getChildShape(index);
}

void btCompoundShape_getChildTransform(btCompoundShape* obj, int index, btScalar* value)
{
	TRANSFORM_OUT(&obj->getChildTransform(index), value);
}

btDbvt* btCompoundShape_getDynamicAabbTree(btCompoundShape* obj)
{
	return obj->getDynamicAabbTree();
}

int btCompoundShape_getNumChildShapes(btCompoundShape* obj)
{
	return obj->getNumChildShapes();
}

int btCompoundShape_getUpdateRevision(btCompoundShape* obj)
{
	return obj->getUpdateRevision();
}

void btCompoundShape_recalculateLocalAabb(btCompoundShape* obj)
{
	obj->recalculateLocalAabb();
}

void btCompoundShape_removeChildShape(btCompoundShape* obj, btCollisionShape* shape)
{
	obj->removeChildShape(shape);
}

void btCompoundShape_removeChildShapeByIndex(btCompoundShape* obj, int childShapeindex)
{
	obj->removeChildShapeByIndex(childShapeindex);
}

void btCompoundShape_updateChildTransform(btCompoundShape* obj, int childIndex, const btScalar* newChildTransform)
{
	TRANSFORM_CONV(newChildTransform);
	obj->updateChildTransform(childIndex, TRANSFORM_USE(newChildTransform));
}

void btCompoundShape_updateChildTransform2(btCompoundShape* obj, int childIndex, const btScalar* newChildTransform, bool shouldRecalculateLocalAabb)
{
	TRANSFORM_CONV(newChildTransform);
	obj->updateChildTransform(childIndex, TRANSFORM_USE(newChildTransform), shouldRecalculateLocalAabb);
}
