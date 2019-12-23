#include "main.h"

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btDeformableMultiBodyDynamicsWorld* btDeformableMultiBodyDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btDeformableMultiBodyConstraintSolver* constraintSolver, btCollisionConfiguration* collisionCOnfiguration, btDeformableBodySolver* deformableBodySolver);
	EXPORT void btDeformableMultiBodyDynamicsWorld_addForce(btDeformableMultiBodyDynamicsWorld* obj, btSoftBody* psb, btDeformableLagrangianForce* force);
	EXPORT void btDeformableMultiBodyDynamicsWorld_addSoftBody(btDeformableMultiBodyDynamicsWorld* obj, btSoftBody* body, int collisionFilterGroup, int collisionFilterMask);
	EXPORT btSoftBodyWorldInfo* btDeformableMultiBodyDynamicsWorld_getWorldInfo(btDeformableMultiBodyDynamicsWorld* obj);
#ifdef __cplusplus
}
#endif
