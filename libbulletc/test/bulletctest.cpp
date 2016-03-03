#include <iostream>

#include <bulletc.h>
#include "hacd_data.h"

using namespace std;

btScalar addSingleResult(btCollisionWorld_LocalConvexResult* rayResult, bool normalInWorldSpace)
{
	return 1.0f;
}

bool needsCollision(btBroadphaseProxy* proxy0)
{
	return true;
}

void test_hacd()
{
	cout << "Calculating HACD clusters..." << endl;

	// Truncate to float precision
	for (int i = 0; i < sizeof(points) / sizeof(points[0]); i++)
	{
		points[i] = float(points[i]);
	}

	HACD_HACD* myHACD = HACD_HACD_new();
	HACD_HACD_SetPoints(myHACD, points);
	HACD_HACD_SetNPoints(myHACD, sizeof(points) / (sizeof(points[0]) * 3));
	HACD_HACD_SetTriangles(myHACD, triangles);
	HACD_HACD_SetNTriangles(myHACD, sizeof(triangles) / (sizeof(triangles[0]) * 3));
	HACD_HACD_SetCompacityWeight(myHACD, 0.1);
	HACD_HACD_SetVolumeWeight(myHACD, 0.0);

	HACD_HACD_SetNClusters(myHACD, 2);                     // minimum number of clusters
	HACD_HACD_SetNVerticesPerCH(myHACD, 100);                      // max of 100 vertices per convex-hull
	HACD_HACD_SetConcavity(myHACD, 100);                     // maximum concavity
	HACD_HACD_SetAddExtraDistPoints(myHACD, false);
	HACD_HACD_SetAddNeighboursDistPoints(myHACD, false);
	HACD_HACD_SetAddFacesPoints(myHACD, false);

	HACD_HACD_Compute(myHACD);
	size_t nClusters = HACD_HACD_GetNClusters(myHACD);

	cout << "HACD clusters: " << nClusters << endl;

	HACD_HACD_Save(myHACD, "output.wrl", false);
}

int main(int argc, char* argv[])
{
	btDbvtBroadphase* broadphase = btDbvtBroadphase_new();
	btDefaultCollisionConfiguration* collisionConfiguration = btDefaultCollisionConfiguration_new();
	btCollisionDispatcher* dispatcher = btCollisionDispatcher_new(collisionConfiguration);
	btSequentialImpulseConstraintSolver* solver = btSequentialImpulseConstraintSolver_new();
	btDiscreteDynamicsWorld* world = btDiscreteDynamicsWorld_new(dispatcher,broadphase,solver,collisionConfiguration);
	btSphereShape* shape = btSphereShape_new(1);
	btDefaultMotionState* ms = btDefaultMotionState_new();
	btRigidBody_btRigidBodyConstructionInfo* ci = btRigidBody_btRigidBodyConstructionInfo_new(0,ms,shape);
	btRigidBody* body = btRigidBody_new(ci);
	btDynamicsWorld_addRigidBody(world,body);
	btDynamicsWorld_removeRigidBody(world,body);

	btCollisionWorld_ConvexResultCallbackWrapper* convexCallback = btCollisionWorld_ConvexResultCallbackWrapper_new(addSingleResult, needsCollision);
	bool hit = btCollisionWorld_ConvexResultCallback_hasHit(convexCallback);
	if (hit)
	{
		cout << "Boolean marshalling bug" << endl;
	}
	btCollisionWorld_ConvexResultCallback_delete(convexCallback);

	btBroadphaseInterface_delete(broadphase);
	btCollisionConfiguration_delete(collisionConfiguration);
	btDispatcher_delete(dispatcher);
	btConstraintSolver_delete(solver);
	btCollisionWorld_delete(world);
	btCollisionShape_delete(shape);
	btMotionState_delete(ms);
	btRigidBody_btRigidBodyConstructionInfo_delete(ci);
	btCollisionObject_delete(body);

	test_hacd();

	cout << "Done. Press enter to continue.";
	cin.get();
	return 0;
}
