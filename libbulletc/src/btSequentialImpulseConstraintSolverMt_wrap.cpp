#include <BulletDynamics/ConstraintSolver/btSequentialImpulseConstraintSolverMt.h>

#include "btSequentialImpulseConstraintSolverMt_wrap.h"

btSequentialImpulseConstraintSolverMt* btSequentialImpulseConstraintSolverMt_new()
{
	return ALIGNED_NEW(btSequentialImpulseConstraintSolverMt) ();
}
