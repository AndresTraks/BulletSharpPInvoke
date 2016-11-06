#include <BulletCollision/CollisionDispatch/btCollisionConfiguration.h>
#include <BulletCollision/CollisionDispatch/btSimulationIslandManager.h>
#include <BulletDynamics/ConstraintSolver/btConstraintSolver.h>
#include <BulletDynamics/Dynamics/btDiscreteDynamicsWorldMt.h>

#include "btDiscreteDynamicsWorldMt_wrap.h"

btDiscreteDynamicsWorldMt* btDiscreteDynamicsWorldMt_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache,
	btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration)
{
	return new btDiscreteDynamicsWorldMt(dispatcher, pairCache, constraintSolver, collisionConfiguration);
}
