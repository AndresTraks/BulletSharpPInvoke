#include <BulletCollision/BroadphaseCollision/btDispatcher.h>
#include <BulletCollision/CollisionDispatch/btCollisionWorld.h>
#include <BulletCollision/CollisionDispatch/btSimulationIslandManager.h>

#include "btSimulationIslandManager_wrap.h"
/*
void btSimulationIslandManager_IslandCallback_processIsland(btSimulationIslandManager::IslandCallback* obj, * bodies, int numBodies, * manifolds, int numManifolds, int islandId)
{
	obj->processIsland(bodies, numBodies, manifolds, numManifolds, islandId);
}
*/
void btSimulationIslandManager_IslandCallback_delete(btSimulationIslandManager::IslandCallback* obj)
{
	delete obj;
}


btSimulationIslandManager* btSimulationIslandManager_new()
{
	return new btSimulationIslandManager();
}

void btSimulationIslandManager_buildAndProcessIslands(btSimulationIslandManager* obj, btDispatcher* dispatcher, btCollisionWorld* collisionWorld, btSimulationIslandManager::IslandCallback* callback)
{
	obj->buildAndProcessIslands(dispatcher, collisionWorld, callback);
}

void btSimulationIslandManager_buildIslands(btSimulationIslandManager* obj, btDispatcher* dispatcher, btCollisionWorld* colWorld)
{
	obj->buildIslands(dispatcher, colWorld);
}

void btSimulationIslandManager_findUnions(btSimulationIslandManager* obj, btDispatcher* dispatcher, btCollisionWorld* colWorld)
{
	obj->findUnions(dispatcher, colWorld);
}

bool btSimulationIslandManager_getSplitIslands(btSimulationIslandManager* obj)
{
	return obj->getSplitIslands();
}

btUnionFind* btSimulationIslandManager_getUnionFind(btSimulationIslandManager* obj)
{
	return &obj->getUnionFind();
}

void btSimulationIslandManager_initUnionFind(btSimulationIslandManager* obj, int n)
{
	obj->initUnionFind(n);
}

void btSimulationIslandManager_setSplitIslands(btSimulationIslandManager* obj, bool doSplitIslands)
{
	obj->setSplitIslands(doSplitIslands);
}

void btSimulationIslandManager_storeIslandActivationState(btSimulationIslandManager* obj, btCollisionWorld* world)
{
	obj->storeIslandActivationState(world);
}

void btSimulationIslandManager_updateActivationState(btSimulationIslandManager* obj, btCollisionWorld* colWorld, btDispatcher* dispatcher)
{
	obj->updateActivationState(colWorld, dispatcher);
}

void btSimulationIslandManager_delete(btSimulationIslandManager* obj)
{
	delete obj;
}
