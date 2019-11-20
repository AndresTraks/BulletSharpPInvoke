#include "main.h"

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btDiscreteDynamicsWorld* btDiscreteDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration);
	EXPORT void btDiscreteDynamicsWorld_applyGravity(btDiscreteDynamicsWorld* obj);
	EXPORT void btDiscreteDynamicsWorld_debugDrawConstraint(btDiscreteDynamicsWorld* obj, btTypedConstraint* constraint);
	EXPORT bool btDiscreteDynamicsWorld_getApplySpeculativeContactRestitution(btDiscreteDynamicsWorld* obj);
	EXPORT btCollisionWorld* btDiscreteDynamicsWorld_getCollisionWorld(btDiscreteDynamicsWorld* obj);
	EXPORT bool btDiscreteDynamicsWorld_getLatencyMotionStateInterpolation(btDiscreteDynamicsWorld* obj);
	EXPORT btAlignedObjectArray_btRigidBodyPtr* btDiscreteDynamicsWorld_getNonStaticRigidBodies(btDiscreteDynamicsWorld* obj);
	EXPORT btSimulationIslandManager* btDiscreteDynamicsWorld_getSimulationIslandManager(btDiscreteDynamicsWorld* obj);
	EXPORT bool btDiscreteDynamicsWorld_getSynchronizeAllMotionStates(btDiscreteDynamicsWorld* obj);
	EXPORT void btDiscreteDynamicsWorld_setApplySpeculativeContactRestitution(btDiscreteDynamicsWorld* obj, bool enable);
	EXPORT void btDiscreteDynamicsWorld_setLatencyMotionStateInterpolation(btDiscreteDynamicsWorld* obj, bool latencyInterpolation);
	EXPORT void btDiscreteDynamicsWorld_setNumTasks(btDiscreteDynamicsWorld* obj, int numTasks);
	EXPORT void btDiscreteDynamicsWorld_setSynchronizeAllMotionStates(btDiscreteDynamicsWorld* obj, bool synchronizeAll);
	EXPORT void btDiscreteDynamicsWorld_solveConstraints(btDiscreteDynamicsWorld* obj, btContactSolverInfo* solverInfo);
	EXPORT void btDiscreteDynamicsWorld_synchronizeSingleMotionState(btDiscreteDynamicsWorld* obj, btRigidBody* body);
	EXPORT void btDiscreteDynamicsWorld_updateVehicles(btDiscreteDynamicsWorld* obj, btScalar timeStep);
#ifdef __cplusplus
}
#endif
