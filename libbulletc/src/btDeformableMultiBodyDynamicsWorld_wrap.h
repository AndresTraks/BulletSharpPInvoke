#include "main.h"

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btDeformableMultiBodyDynamicsWorld* btDeformableMultiBodyDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btDeformableMultiBodyConstraintSolver* constraintSolver, btCollisionConfiguration* collisionCOnfiguration, btDeformableBodySolver* deformableBodySolver);
#ifdef __cplusplus
}
#endif
