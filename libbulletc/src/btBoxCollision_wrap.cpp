#include <BulletCollision/Gimpact/btBoxCollision.h>

#include "conversion.h"
#include "btBoxCollision_wrap.h"

BT_BOX_BOX_TRANSFORM_CACHE* BT_BOX_BOX_TRANSFORM_CACHE_new()
{
	return new BT_BOX_BOX_TRANSFORM_CACHE();
}

void BT_BOX_BOX_TRANSFORM_CACHE_calc_absolute_matrix(BT_BOX_BOX_TRANSFORM_CACHE* obj)
{
	obj->calc_absolute_matrix();
}

void BT_BOX_BOX_TRANSFORM_CACHE_calc_from_full_invert(BT_BOX_BOX_TRANSFORM_CACHE* obj, const btScalar* trans0, const btScalar* trans1)
{
	TRANSFORM_CONV(trans0);
	TRANSFORM_CONV(trans1);
	obj->calc_from_full_invert(TRANSFORM_USE(trans0), TRANSFORM_USE(trans1));
}

void BT_BOX_BOX_TRANSFORM_CACHE_calc_from_homogenic(BT_BOX_BOX_TRANSFORM_CACHE* obj, const btScalar* trans0, const btScalar* trans1)
{
	TRANSFORM_CONV(trans0);
	TRANSFORM_CONV(trans1);
	obj->calc_from_homogenic(TRANSFORM_USE(trans0), TRANSFORM_USE(trans1));
}

void BT_BOX_BOX_TRANSFORM_CACHE_getAR(BT_BOX_BOX_TRANSFORM_CACHE* obj, btScalar* value)
{
	MATRIX3X3_OUT(&obj->m_AR, value);
}

void BT_BOX_BOX_TRANSFORM_CACHE_getR1to0(BT_BOX_BOX_TRANSFORM_CACHE* obj, btScalar* value)
{
	MATRIX3X3_OUT(&obj->m_R1to0, value);
}

void BT_BOX_BOX_TRANSFORM_CACHE_getT1to0(BT_BOX_BOX_TRANSFORM_CACHE* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_T1to0, value);
}

void BT_BOX_BOX_TRANSFORM_CACHE_setAR(BT_BOX_BOX_TRANSFORM_CACHE* obj, const btScalar* value)
{
	MATRIX3X3_IN(value, &obj->m_AR);
}

void BT_BOX_BOX_TRANSFORM_CACHE_setR1to0(BT_BOX_BOX_TRANSFORM_CACHE* obj, const btScalar* value)
{
	MATRIX3X3_IN(value, &obj->m_R1to0);
}

void BT_BOX_BOX_TRANSFORM_CACHE_setT1to0(BT_BOX_BOX_TRANSFORM_CACHE* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_T1to0);
}

void BT_BOX_BOX_TRANSFORM_CACHE_transform(BT_BOX_BOX_TRANSFORM_CACHE* obj, const btScalar* point, btScalar* value)
{
	VECTOR3_CONV(point);
	VECTOR3_OUT_VAL(obj->transform(VECTOR3_USE(point)), value);
}

void BT_BOX_BOX_TRANSFORM_CACHE_delete(BT_BOX_BOX_TRANSFORM_CACHE* obj)
{
	delete obj;
}


btAABB* btAABB_new()
{
	return new btAABB();
}

btAABB* btAABB_new2(const btScalar* V1, const btScalar* V2, const btScalar* V3)
{
	VECTOR3_CONV(V1);
	VECTOR3_CONV(V2);
	VECTOR3_CONV(V3);
	return new btAABB(VECTOR3_USE(V1), VECTOR3_USE(V2), VECTOR3_USE(V3));
}

btAABB* btAABB_new3(const btScalar* V1, const btScalar* V2, const btScalar* V3, btScalar margin)
{
	VECTOR3_CONV(V1);
	VECTOR3_CONV(V2);
	VECTOR3_CONV(V3);
	return new btAABB(VECTOR3_USE(V1), VECTOR3_USE(V2), VECTOR3_USE(V3), margin);
}

btAABB* btAABB_new4(const btAABB* other)
{
	return new btAABB(*other);
}

btAABB* btAABB_new5(const btAABB* other, btScalar margin)
{
	return new btAABB(*other, margin);
}

void btAABB_appy_transform(btAABB* obj, const btScalar* trans)
{
	TRANSFORM_CONV(trans);
	obj->appy_transform(TRANSFORM_USE(trans));
}

void btAABB_appy_transform_trans_cache(btAABB* obj, const BT_BOX_BOX_TRANSFORM_CACHE* trans)
{
	obj->appy_transform_trans_cache(*trans);
}

bool btAABB_collide_plane(btAABB* obj, const btScalar* plane)
{
	VECTOR4_CONV(plane);
	return obj->collide_plane(VECTOR4_USE(plane));
}

bool btAABB_collide_ray(btAABB* obj, const btScalar* vorigin, const btScalar* vdir)
{
	VECTOR3_CONV(vorigin);
	VECTOR3_CONV(vdir);
	return obj->collide_ray(VECTOR3_USE(vorigin), VECTOR3_USE(vdir));
}

bool btAABB_collide_triangle_exact(btAABB* obj, const btScalar* p1, const btScalar* p2, const btScalar* p3, const btScalar* triangle_plane)
{
	VECTOR3_CONV(p1);
	VECTOR3_CONV(p2);
	VECTOR3_CONV(p3);
	VECTOR4_DEF(triangle_plane);
	return obj->collide_triangle_exact(VECTOR3_USE(p1), VECTOR3_USE(p2), VECTOR3_USE(p3), VECTOR4_USE(triangle_plane));
}

void btAABB_copy_with_margin(btAABB* obj, const btAABB* other, btScalar margin)
{
	obj->copy_with_margin(*other, margin);
}

void btAABB_find_intersection(btAABB* obj, const btAABB* other, btAABB* intersection)
{
	obj->find_intersection(*other, *intersection);
}

void btAABB_get_center_extend(btAABB* obj, btScalar* center, btScalar* extend)
{
	VECTOR3_CONV(center);
	VECTOR3_CONV(extend);
	obj->get_center_extend(VECTOR3_USE(center), VECTOR3_USE(extend));
	VECTOR3_DEF_OUT(center);
	VECTOR3_DEF_OUT(extend);
}

void btAABB_getMax(btAABB* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_max, value);
}

void btAABB_getMin(btAABB* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_min, value);
}

bool btAABB_has_collision(btAABB* obj, const btAABB* other)
{
	return obj->has_collision(*other);
}

void btAABB_increment_margin(btAABB* obj, btScalar margin)
{
	obj->increment_margin(margin);
}

void btAABB_invalidate(btAABB* obj)
{
	obj->invalidate();
}

void btAABB_merge(btAABB* obj, const btAABB* box)
{
	obj->merge(*box);
}

bool btAABB_overlapping_trans_cache(btAABB* obj, const btAABB* box, const BT_BOX_BOX_TRANSFORM_CACHE* transcache, bool fulltest)
{
	return obj->overlapping_trans_cache(*box, *transcache, fulltest);
}

bool btAABB_overlapping_trans_conservative(btAABB* obj, const btAABB* box, btScalar* trans1_to_0)
{
	TRANSFORM_CONV(trans1_to_0);
	bool ret = obj->overlapping_trans_conservative(*box, TRANSFORM_USE(trans1_to_0));
	TRANSFORM_DEF_OUT(trans1_to_0);
	return ret;
}

bool btAABB_overlapping_trans_conservative2(btAABB* obj, const btAABB* box, const BT_BOX_BOX_TRANSFORM_CACHE* trans1_to_0)
{
	return obj->overlapping_trans_conservative2(*box, *trans1_to_0);
}

eBT_PLANE_INTERSECTION_TYPE btAABB_plane_classify(btAABB* obj, const btScalar* plane)
{
	VECTOR4_CONV(plane);
	return obj->plane_classify(VECTOR4_USE(plane));
}

void btAABB_projection_interval(btAABB* obj, const btScalar* direction, btScalar* vmin, btScalar* vmax)
{
	VECTOR3_CONV(direction);
	obj->projection_interval(VECTOR3_USE(direction), *vmin, *vmax);
}

void btAABB_setMax(btAABB* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_max);
}

void btAABB_setMin(btAABB* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_min);
}

void btAABB_delete(btAABB* obj)
{
	delete obj;
}
