#include "main.h"

extern "C"
{
	EXPORT btDiscreteDynamicsWorldMt* btDiscreteDynamicsWorldMt_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration);
}
