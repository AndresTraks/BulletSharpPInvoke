#include <BulletSoftBody/btDeformableMultiBodyConstraintSolver.h>

#include "btDeformableMultiBodyConstraintSolver_wrap.h"

btDeformableMultiBodyConstraintSolver* btDeformableMultiBodyConstraintSolver_new()
{
	return new btDeformableMultiBodyConstraintSolver();
}

void btDeformableMultiBodyConstraintSolver_setDeformableSolver(btDeformableMultiBodyConstraintSolver* obj, btDeformableBodySolver* deformableSolver)
{
	obj->setDeformableSolver(deformableSolver);
}
