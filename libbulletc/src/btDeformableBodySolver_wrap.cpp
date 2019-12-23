#include <BulletSoftBody/btDeformableBodySolver.h>

#include "btDeformableBodySolver_wrap.h"

btDeformableBodySolver* btDeformableBodySolver_new()
{
	return new btDeformableBodySolver();
}
