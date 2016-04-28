#include <BulletCollision/CollisionDispatch/btConvexConvexAlgorithm.h>
#include <BulletCollision/NarrowPhaseCollision/btConvexPenetrationDepthSolver.h>

#include "btConvexConvexAlgorithm_wrap.h"

btConvexConvexAlgorithm_CreateFunc* btConvexConvexAlgorithm_CreateFunc_new(btVoronoiSimplexSolver* simplexSolver,
	btConvexPenetrationDepthSolver* pdSolver)
{
	return new btConvexConvexAlgorithm::CreateFunc(simplexSolver, pdSolver);
}

int btConvexConvexAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(btConvexConvexAlgorithm_CreateFunc* obj)
{
	return obj->m_minimumPointsPerturbationThreshold;
}

int btConvexConvexAlgorithm_CreateFunc_getNumPerturbationIterations(btConvexConvexAlgorithm_CreateFunc* obj)
{
	return obj->m_numPerturbationIterations;
}

btConvexPenetrationDepthSolver* btConvexConvexAlgorithm_CreateFunc_getPdSolver(btConvexConvexAlgorithm_CreateFunc* obj)
{
	return obj->m_pdSolver;
}

btVoronoiSimplexSolver* btConvexConvexAlgorithm_CreateFunc_getSimplexSolver(btConvexConvexAlgorithm_CreateFunc* obj)
{
	return obj->m_simplexSolver;
}

void btConvexConvexAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(btConvexConvexAlgorithm_CreateFunc* obj,
	int value)
{
	obj->m_minimumPointsPerturbationThreshold = value;
}

void btConvexConvexAlgorithm_CreateFunc_setNumPerturbationIterations(btConvexConvexAlgorithm_CreateFunc* obj,
	int value)
{
	obj->m_numPerturbationIterations = value;
}

void btConvexConvexAlgorithm_CreateFunc_setPdSolver(btConvexConvexAlgorithm_CreateFunc* obj,
	btConvexPenetrationDepthSolver* value)
{
	obj->m_pdSolver = value;
}

void btConvexConvexAlgorithm_CreateFunc_setSimplexSolver(btConvexConvexAlgorithm_CreateFunc* obj,
	btVoronoiSimplexSolver* value)
{
	obj->m_simplexSolver = value;
}


btConvexConvexAlgorithm* btConvexConvexAlgorithm_new(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci,
	const btCollisionObjectWrapper* body0Wrap, const btCollisionObjectWrapper* body1Wrap,
	btVoronoiSimplexSolver* simplexSolver, btConvexPenetrationDepthSolver* pdSolver,
	int numPerturbationIterations, int minimumPointsPerturbationThreshold)
{
	return new btConvexConvexAlgorithm(mf, *ci, body0Wrap, body1Wrap, simplexSolver,
		pdSolver, numPerturbationIterations, minimumPointsPerturbationThreshold);
}

const btPersistentManifold* btConvexConvexAlgorithm_getManifold(btConvexConvexAlgorithm* obj)
{
	return obj->getManifold();
}

void btConvexConvexAlgorithm_setLowLevelOfDetail(btConvexConvexAlgorithm* obj, bool useLowLevel)
{
	obj->setLowLevelOfDetail(useLowLevel);
}
