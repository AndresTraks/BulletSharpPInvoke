#include <BulletCollision/CollisionShapes/btConvexPolyhedron.h>

#include "conversion.h"
#include "btConvexPolyhedron_wrap.h"

btFace* btFace_new()
{
	return new btFace();
}

btAlignedIntArray* btFace_getIndices(btFace* obj)
{
	return &obj->m_indices;
}

btScalar* btFace_getPlane(btFace* obj)
{
	return obj->m_plane;
}

void btFace_delete(btFace* obj)
{
	delete obj;
}


btConvexPolyhedron* btConvexPolyhedron_new()
{
	return new btConvexPolyhedron();
}

void btConvexPolyhedron_getExtents(btConvexPolyhedron* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_extents, value);
}

btAlignedFaceArray* btConvexPolyhedron_getFaces(btConvexPolyhedron* obj)
{
	return &obj->m_faces;
}

void btConvexPolyhedron_getLocalCenter(btConvexPolyhedron* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_localCenter, value);
}

void btConvexPolyhedron_getMC(btConvexPolyhedron* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->mC, value);
}

void btConvexPolyhedron_getME(btConvexPolyhedron* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->mE, value);
}

btScalar btConvexPolyhedron_getRadius(btConvexPolyhedron* obj)
{
	return obj->m_radius;
}

btAlignedVector3Array* btConvexPolyhedron_getUniqueEdges(btConvexPolyhedron* obj)
{
	return &obj->m_uniqueEdges;
}

btAlignedVector3Array* btConvexPolyhedron_getVertices(btConvexPolyhedron* obj)
{
	return &obj->m_vertices;
}

void btConvexPolyhedron_initialize(btConvexPolyhedron* obj)
{
	obj->initialize();
}

void btConvexPolyhedron_project(btConvexPolyhedron* obj, const btScalar* trans, const btScalar* dir, btScalar* minProj, btScalar* maxProj, btScalar* witnesPtMin, btScalar* witnesPtMax)
{
	TRANSFORM_CONV(trans);
	VECTOR3_CONV(dir);
	VECTOR3_DEF(witnesPtMin);
	VECTOR3_DEF(witnesPtMax);
	obj->project(TRANSFORM_USE(trans), VECTOR3_USE(dir), *minProj, *maxProj, VECTOR3_USE(witnesPtMin), VECTOR3_USE(witnesPtMax));
	VECTOR3_DEF_OUT(witnesPtMin);
	VECTOR3_DEF_OUT(witnesPtMax);
}

void btConvexPolyhedron_setExtents(btConvexPolyhedron* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_extents);
}

void btConvexPolyhedron_setLocalCenter(btConvexPolyhedron* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_localCenter);
}

void btConvexPolyhedron_setMC(btConvexPolyhedron* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->mC);
}

void btConvexPolyhedron_setME(btConvexPolyhedron* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->mE);
}

void btConvexPolyhedron_setRadius(btConvexPolyhedron* obj, btScalar value)
{
	obj->m_radius = value;
}

bool btConvexPolyhedron_testContainment(btConvexPolyhedron* obj)
{
	return obj->testContainment();
}

void btConvexPolyhedron_delete(btConvexPolyhedron* obj)
{
	delete obj;
}
