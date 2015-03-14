#include <BulletCollision/CollisionDispatch/btCollisionObjectWrapper.h>
#include <BulletCollision/CollisionDispatch/btManifoldResult.h>
#include <BulletCollision/CollisionShapes/btCollisionShape.h>
#include <BulletSoftBody/btSoftBodyConcaveCollisionAlgorithm.h>

#include "conversion.h"
#include "btSoftBodyConcaveCollisionAlgorithm_wrap.h"

btTriIndex* btTriIndex_new(int partId, int triangleIndex, btCollisionShape* shape)
{
	return new btTriIndex(partId, triangleIndex, shape);
}

btCollisionShape* btTriIndex_getChildShape(btTriIndex* obj)
{
	return obj->m_childShape;
}

int btTriIndex_getPartId(btTriIndex* obj)
{
	return obj->getPartId();
}

int btTriIndex_getPartIdTriangleIndex(btTriIndex* obj)
{
	return obj->m_PartIdTriangleIndex;
}

int btTriIndex_getTriangleIndex(btTriIndex* obj)
{
	return obj->getTriangleIndex();
}

int btTriIndex_getUid(btTriIndex* obj)
{
	return obj->getUid();
}

void btTriIndex_setChildShape(btTriIndex* obj, btCollisionShape* value)
{
	obj->m_childShape = value;
}

void btTriIndex_setPartIdTriangleIndex(btTriIndex* obj, int value)
{
	obj->m_PartIdTriangleIndex = value;
}

void btTriIndex_delete(btTriIndex* obj)
{
	delete obj;
}


btSoftBodyTriangleCallback* btSoftBodyTriangleCallback_new(btDispatcher* dispatcher, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped)
{
	return new btSoftBodyTriangleCallback(dispatcher, body0Wrap, body1Wrap, isSwapped);
}

void btSoftBodyTriangleCallback_clearCache(btSoftBodyTriangleCallback* obj)
{
	obj->clearCache();
}

void btSoftBodyTriangleCallback_getAabbMax(btSoftBodyTriangleCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAabbMax(), value);
}

void btSoftBodyTriangleCallback_getAabbMin(btSoftBodyTriangleCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getAabbMin(), value);
}

int btSoftBodyTriangleCallback_getTriangleCount(btSoftBodyTriangleCallback* obj)
{
	return obj->m_triangleCount;
}

void btSoftBodyTriangleCallback_setTimeStepAndCounters(btSoftBodyTriangleCallback* obj, btScalar collisionMarginTriangle, const btCollisionObjectWrapper* triObjWrap, const btDispatcherInfo* dispatchInfo, btManifoldResult* resultOut)
{
	obj->setTimeStepAndCounters(collisionMarginTriangle, triObjWrap, *dispatchInfo, resultOut);
}

void btSoftBodyTriangleCallback_setTriangleCount(btSoftBodyTriangleCallback* obj, int value)
{
	obj->m_triangleCount = value;
}


btSoftBodyConcaveCollisionAlgorithm::CreateFunc* btSoftBodyConcaveCollisionAlgorithm_CreateFunc_new()
{
	return new btSoftBodyConcaveCollisionAlgorithm::CreateFunc();
}


btSoftBodyConcaveCollisionAlgorithm::SwappedCreateFunc* btSoftBodyConcaveCollisionAlgorithm_SwappedCreateFunc_new()
{
	return new btSoftBodyConcaveCollisionAlgorithm::SwappedCreateFunc();
}


btSoftBodyConcaveCollisionAlgorithm* btSoftBodyConcaveCollisionAlgorithm_new(const btCollisionAlgorithmConstructionInfo* ci, const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap, bool isSwapped)
{
	return new btSoftBodyConcaveCollisionAlgorithm(*ci, body0Wrap, body1Wrap, isSwapped);
}

void btSoftBodyConcaveCollisionAlgorithm_clearCache(btSoftBodyConcaveCollisionAlgorithm* obj)
{
	obj->clearCache();
}
