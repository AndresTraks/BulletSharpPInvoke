#include <BulletCollision/CollisionShapes/btTriangleMesh.h>

#include "conversion.h"
#include "btTriangleMesh_wrap.h"

btTriangleMesh* btTriangleMesh_new()
{
	return new btTriangleMesh();
}

btTriangleMesh* btTriangleMesh_new2(bool use32bitIndices)
{
	return new btTriangleMesh(use32bitIndices);
}

btTriangleMesh* btTriangleMesh_new3(bool use32bitIndices, bool use4componentVertices)
{
	return new btTriangleMesh(use32bitIndices, use4componentVertices);
}

void btTriangleMesh_addIndex(btTriangleMesh* obj, int index)
{
	obj->addIndex(index);
}

void btTriangleMesh_addTriangle(btTriangleMesh* obj, const btScalar* vertex0, const btScalar* vertex1, const btScalar* vertex2)
{
	VECTOR3_CONV(vertex0);
	VECTOR3_CONV(vertex1);
	VECTOR3_CONV(vertex2);
	obj->addTriangle(VECTOR3_USE(vertex0), VECTOR3_USE(vertex1), VECTOR3_USE(vertex2));
}

void btTriangleMesh_addTriangle2(btTriangleMesh* obj, const btScalar* vertex0, const btScalar* vertex1, const btScalar* vertex2, bool removeDuplicateVertices)
{
	VECTOR3_CONV(vertex0);
	VECTOR3_CONV(vertex1);
	VECTOR3_CONV(vertex2);
	obj->addTriangle(VECTOR3_USE(vertex0), VECTOR3_USE(vertex1), VECTOR3_USE(vertex2), removeDuplicateVertices);
}

void btTriangleMesh_addTriangleIndices(btTriangleMesh* obj, int index1, int index2, int index3)
{
	obj->addTriangleIndices(index1, index2, index3);
}

int btTriangleMesh_findOrAddVertex(btTriangleMesh* obj, const btScalar* vertex, bool removeDuplicateVertices)
{
	VECTOR3_CONV(vertex);
	return obj->findOrAddVertex(VECTOR3_USE(vertex), removeDuplicateVertices);
}

int btTriangleMesh_getNumTriangles(btTriangleMesh* obj)
{
	return obj->getNumTriangles();
}

bool btTriangleMesh_getUse32bitIndices(btTriangleMesh* obj)
{
	return obj->getUse32bitIndices();
}

bool btTriangleMesh_getUse4componentVertices(btTriangleMesh* obj)
{
	return obj->getUse4componentVertices();
}

btScalar btTriangleMesh_getWeldingThreshold(btTriangleMesh* obj)
{
	return obj->m_weldingThreshold;
}

void btTriangleMesh_setWeldingThreshold(btTriangleMesh* obj, btScalar value)
{
	obj->m_weldingThreshold = value;
}
