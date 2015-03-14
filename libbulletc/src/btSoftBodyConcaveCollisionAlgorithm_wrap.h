#include "main.h"

extern "C"
{
	EXPORT btTriIndex* btTriIndex_new(int partId, int triangleIndex, btCollisionShape* shape);
	EXPORT btCollisionShape* btTriIndex_getChildShape(btTriIndex* obj);
	EXPORT int btTriIndex_getPartId(btTriIndex* obj);
	EXPORT int btTriIndex_getPartIdTriangleIndex(btTriIndex* obj);
	EXPORT int btTriIndex_getTriangleIndex(btTriIndex* obj);
	EXPORT int btTriIndex_getUid(btTriIndex* obj);
	EXPORT void btTriIndex_setChildShape(btTriIndex* obj, btCollisionShape* value);
	EXPORT void btTriIndex_setPartIdTriangleIndex(btTriIndex* obj, int value);
	EXPORT void btTriIndex_delete(btTriIndex* obj);

	EXPORT btSoftBodyTriangleCallback* btSoftBodyTriangleCallback_new(btDispatcher* dispatcher, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped);
	EXPORT void btSoftBodyTriangleCallback_clearCache(btSoftBodyTriangleCallback* obj);
	EXPORT void btSoftBodyTriangleCallback_getAabbMax(btSoftBodyTriangleCallback* obj, btScalar* value);
	EXPORT void btSoftBodyTriangleCallback_getAabbMin(btSoftBodyTriangleCallback* obj, btScalar* value);
	EXPORT int btSoftBodyTriangleCallback_getTriangleCount(btSoftBodyTriangleCallback* obj);
	EXPORT void btSoftBodyTriangleCallback_setTimeStepAndCounters(btSoftBodyTriangleCallback* obj, btScalar collisionMarginTriangle, const btCollisionObjectWrapper* triObjWrap, const btDispatcherInfo* dispatchInfo, btManifoldResult* resultOut);
	EXPORT void btSoftBodyTriangleCallback_setTriangleCount(btSoftBodyTriangleCallback* obj, int value);

	EXPORT btSoftBodyConcaveCollisionAlgorithm_CreateFunc* btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new();

	EXPORT btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc* btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new();

	EXPORT btSoftBodyConcaveCollisionAlgorithm* btSoftBodyConcaveCollisionAlgorithm_new(const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped);
	EXPORT void btSoftBodyConcaveCollisionAlgorithm_clearCache(btSoftBodyConcaveCollisionAlgorithm* obj);
}
