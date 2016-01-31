#include <BulletCollision/Gimpact/btTriangleShapeEx.h>

#include "conversion.h"
#include "btTriangleShapeEx_wrap.h"

GIM_TRIANGLE_CONTACT* GIM_TRIANGLE_CONTACT_new()
{
	return new GIM_TRIANGLE_CONTACT();
}

GIM_TRIANGLE_CONTACT* GIM_TRIANGLE_CONTACT_new2(const GIM_TRIANGLE_CONTACT* other)
{
	return new GIM_TRIANGLE_CONTACT(*other);
}

void GIM_TRIANGLE_CONTACT_copy_from(GIM_TRIANGLE_CONTACT* obj, const GIM_TRIANGLE_CONTACT* other)
{
	obj->copy_from(*other);
}

btScalar GIM_TRIANGLE_CONTACT_getPenetration_depth(GIM_TRIANGLE_CONTACT* obj)
{
	return obj->m_penetration_depth;
}

int GIM_TRIANGLE_CONTACT_getPoint_count(GIM_TRIANGLE_CONTACT* obj)
{
	return obj->m_point_count;
}

btVector3* GIM_TRIANGLE_CONTACT_getPoints(GIM_TRIANGLE_CONTACT* obj)
{
	return obj->m_points;
}

void GIM_TRIANGLE_CONTACT_getSeparating_normal(GIM_TRIANGLE_CONTACT* obj, btScalar* value)
{
	VECTOR4_OUT(&obj->m_separating_normal, value);
}

void GIM_TRIANGLE_CONTACT_merge_points(GIM_TRIANGLE_CONTACT* obj, const btScalar* plane, btScalar margin, const btScalar* points, int point_count)
{
	VECTOR4_CONV(plane);
	VECTOR3_CONV(points);
	obj->merge_points(VECTOR4_USE(plane), margin, &VECTOR3_USE(points), point_count);
}

void GIM_TRIANGLE_CONTACT_setPenetration_depth(GIM_TRIANGLE_CONTACT* obj, btScalar value)
{
	obj->m_penetration_depth = value;
}

void GIM_TRIANGLE_CONTACT_setPoint_count(GIM_TRIANGLE_CONTACT* obj, int value)
{
	obj->m_point_count = value;
}

void GIM_TRIANGLE_CONTACT_setSeparating_normal(GIM_TRIANGLE_CONTACT* obj, const btScalar* value)
{
	VECTOR4_IN(value, &obj->m_separating_normal);
}

void GIM_TRIANGLE_CONTACT_delete(GIM_TRIANGLE_CONTACT* obj)
{
	delete obj;
}


btPrimitiveTriangle* btPrimitiveTriangle_new()
{
	return new btPrimitiveTriangle();
}

void btPrimitiveTriangle_applyTransform(btPrimitiveTriangle* obj, const btScalar* t)
{
	TRANSFORM_CONV(t);
	obj->applyTransform(TRANSFORM_USE(t));
}

void btPrimitiveTriangle_buildTriPlane(btPrimitiveTriangle* obj)
{
	obj->buildTriPlane();
}

int btPrimitiveTriangle_clip_triangle(btPrimitiveTriangle* obj, btPrimitiveTriangle* other, btScalar* clipped_points)
{
	VECTOR3_CONV(clipped_points);
	int ret = obj->clip_triangle(*other, &VECTOR3_USE(clipped_points));
	VECTOR3_DEF_OUT(clipped_points);
	return ret;
}

bool btPrimitiveTriangle_find_triangle_collision_clip_method(btPrimitiveTriangle* obj, btPrimitiveTriangle* other, GIM_TRIANGLE_CONTACT* contacts)
{
	return obj->find_triangle_collision_clip_method(*other, *contacts);
}

void btPrimitiveTriangle_get_edge_plane(btPrimitiveTriangle* obj, int edge_index, btScalar* plane)
{
	VECTOR4_CONV(plane);
	obj->get_edge_plane(edge_index, VECTOR4_USE(plane));
	VECTOR4_DEF_OUT(plane);
}

btScalar btPrimitiveTriangle_getDummy(btPrimitiveTriangle* obj)
{
	return obj->m_dummy;
}

btScalar btPrimitiveTriangle_getMargin(btPrimitiveTriangle* obj)
{
	return obj->m_margin;
}

void btPrimitiveTriangle_getPlane(btPrimitiveTriangle* obj, btScalar* value)
{
	VECTOR4_OUT(&obj->m_plane, value);
}

btVector3* btPrimitiveTriangle_getVertices(btPrimitiveTriangle* obj)
{
	return obj->m_vertices;
}

bool btPrimitiveTriangle_overlap_test_conservative(btPrimitiveTriangle* obj, const btPrimitiveTriangle* other)
{
	return obj->overlap_test_conservative(*other);
}

void btPrimitiveTriangle_setDummy(btPrimitiveTriangle* obj, btScalar value)
{
	obj->m_dummy = value;
}

void btPrimitiveTriangle_setMargin(btPrimitiveTriangle* obj, btScalar value)
{
	obj->m_margin = value;
}

void btPrimitiveTriangle_setPlane(btPrimitiveTriangle* obj, const btScalar* value)
{
	VECTOR4_IN(value, &obj->m_plane);
}

void btPrimitiveTriangle_delete(btPrimitiveTriangle* obj)
{
	delete obj;
}


btTriangleShapeEx* btTriangleShapeEx_new()
{
	return new btTriangleShapeEx();
}

btTriangleShapeEx* btTriangleShapeEx_new2(const btScalar* p0, const btScalar* p1, const btScalar* p2)
{
	VECTOR3_CONV(p0);
	VECTOR3_CONV(p1);
	VECTOR3_CONV(p2);
	return new btTriangleShapeEx(VECTOR3_USE(p0), VECTOR3_USE(p1), VECTOR3_USE(p2));
}

btTriangleShapeEx* btTriangleShapeEx_new3(const btTriangleShapeEx* other)
{
	return new btTriangleShapeEx(*other);
}

void btTriangleShapeEx_applyTransform(btTriangleShapeEx* obj, const btScalar* t)
{
	TRANSFORM_CONV(t);
	obj->applyTransform(TRANSFORM_USE(t));
}

void btTriangleShapeEx_buildTriPlane(btTriangleShapeEx* obj, btScalar* plane)
{
	VECTOR4_CONV(plane);
	obj->buildTriPlane(VECTOR4_USE(plane));
	VECTOR4_DEF_OUT(plane);
}

bool btTriangleShapeEx_overlap_test_conservative(btTriangleShapeEx* obj, const btTriangleShapeEx* other)
{
	return obj->overlap_test_conservative(*other);
}
