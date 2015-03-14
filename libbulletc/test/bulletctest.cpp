#include <iostream>

#include <bulletc.h>

using namespace std;

btScalar addSingleResult(btCollisionWorld_LocalConvexResult* rayResult, bool normalInWorldSpace)
{
	return 1.0f;
}

bool needsCollision(btBroadphaseProxy* proxy0)
{
	return true;
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

	cout << "OK";
	cin.get();
	return 0;
}
