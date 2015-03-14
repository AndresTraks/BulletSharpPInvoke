#include <BulletDynamics/MLCPSolvers/btMLCPSolver.h>

#include "btMLCPSolver_wrap.h"

btMLCPSolver* btMLCPSolver_new(btMLCPSolverInterface* solver)
{
	return new btMLCPSolver(solver);
}

btScalar btMLCPSolver_getCfm(btMLCPSolver* obj)
{
	return obj->getCfm();
}

int btMLCPSolver_getNumFallbacks(btMLCPSolver* obj)
{
	return obj->getNumFallbacks();
}

void btMLCPSolver_setCfm(btMLCPSolver* obj, btScalar cfm)
{
	obj->setCfm(cfm);
}

void btMLCPSolver_setMLCPSolver(btMLCPSolver* obj, btMLCPSolverInterface* solver)
{
	obj->setMLCPSolver(solver);
}

void btMLCPSolver_setNumFallbacks(btMLCPSolver* obj, int num)
{
	obj->setNumFallbacks(num);
}
