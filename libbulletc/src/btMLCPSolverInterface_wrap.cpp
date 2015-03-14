#include <BulletDynamics/MLCPSolvers/btMLCPSolverInterface.h>

#include "btMLCPSolverInterface_wrap.h"
/*
bool btMLCPSolverInterface_solveMLCP(btMLCPSolverInterface* obj, const btMatrixXf* A, const btVectorXf* b, btVectorXf* x, const btVectorXf* lo, const btVectorXf* hi, const btAlignedIntArray* limitDependency, int numIterations)
{
	return obj->solveMLCP(*A, *b, *x, *lo, *hi, *limitDependency, numIterations);
}

bool btMLCPSolverInterface_solveMLCP2(btMLCPSolverInterface* obj, const btMatrixXf* A, const btVectorXf* b, btVectorXf* x, const btVectorXf* lo, const btVectorXf* hi, const btAlignedIntArray* limitDependency, int numIterations, bool useSparsity)
{
	return obj->solveMLCP(*A, *b, *x, *lo, *hi, *limitDependency, numIterations, useSparsity);
}
*/
void btMLCPSolverInterface_delete(btMLCPSolverInterface* obj)
{
	delete obj;
}
