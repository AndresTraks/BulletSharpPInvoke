#include <BulletCollision/CollisionDispatch/btSphereBoxCollisionAlgorithm.h>

#include "conversion.h"
#include "btSphereBoxCollisionAlgorithm_wrap.h"

btSphereBoxCollisionAlgorithm::CreateFunc* btSphereBoxCollisionAlgorithm_CreateFunc_new()
{
	return new btSphereBoxCollisionAlgorithm::CreateFunc();
}


btSphereBoxCollisionAlgorithm* btSphereBoxCollisionAlgorithm_new(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped)
{
	return new btSphereBoxCollisionAlgorithm(mf, *ci, body0Wrap, body1Wrap, isSwapped);
}

bool btSphereBoxCollisionAlgorithm_getSphereDistance(btSphereBoxCollisionAlgorithm* obj, const btCollisionObjectWrapper* boxObjWrap, btScalar* v3PointOnBox, btScalar* normal, btScalar* penetrationDepth, const btScalar* v3SphereCenter, btScalar fRadius, btScalar maxContactDistance)
{
	VECTOR3_CONV(v3PointOnBox);
	VECTOR3_DEF(normal);
	VECTOR3_DEF(v3SphereCenter);
	bool ret = obj->getSphereDistance(boxObjWrap, VECTOR3_USE(v3PointOnBox), VECTOR3_USE(normal), *penetrationDepth, VECTOR3_USE(v3SphereCenter), fRadius, maxContactDistance);
	VECTOR3_DEF_OUT(v3PointOnBox);
	VECTOR3_DEF_OUT(normal);
	return ret;
}

btScalar btSphereBoxCollisionAlgorithm_getSpherePenetration(btSphereBoxCollisionAlgorithm* obj, const btScalar* boxHalfExtent, const btScalar* sphereRelPos, btScalar* closestPoint, btScalar* normal)
{
	VECTOR3_CONV(boxHalfExtent);
	VECTOR3_CONV(sphereRelPos);
	VECTOR3_DEF(closestPoint);
	VECTOR3_DEF(normal);
	btScalar ret = obj->getSpherePenetration(VECTOR3_USE(boxHalfExtent), VECTOR3_USE(sphereRelPos), VECTOR3_USE(closestPoint), VECTOR3_USE(normal));
	VECTOR3_DEF_OUT(closestPoint);
	VECTOR3_DEF_OUT(normal);
	return ret;
}
