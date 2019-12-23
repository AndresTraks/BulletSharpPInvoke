#include "main.h"

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btDeformableMultiBodyConstraintSolver* btDeformableMultiBodyConstraintSolver_new();
	EXPORT void btDeformableMultiBodyConstraintSolver_setDeformableSolver(btDeformableMultiBodyConstraintSolver* obj, btDeformableBodySolver* deformableSolver);
#ifdef __cplusplus
}
#endif
