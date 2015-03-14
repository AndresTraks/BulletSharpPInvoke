#include <LinearMath/btPolarDecomposition.h>

#include "conversion.h"
#include "btPolarDecomposition_wrap.h"

btPolarDecomposition* btPolarDecomposition_new()
{
	return new btPolarDecomposition();
}

btPolarDecomposition* btPolarDecomposition_new2(btScalar tolerance)
{
	return new btPolarDecomposition(tolerance);
}

btPolarDecomposition* btPolarDecomposition_new3(btScalar tolerance, unsigned int maxIterations)
{
	return new btPolarDecomposition(tolerance, maxIterations);
}

unsigned int btPolarDecomposition_decompose(btPolarDecomposition* obj, const btScalar* a, btScalar* u, btScalar* h)
{
	MATRIX3X3_CONV(a);
	MATRIX3X3_DEF(u);
	MATRIX3X3_DEF(h);
	unsigned int ret = obj->decompose(MATRIX3X3_USE(a), MATRIX3X3_USE(u), MATRIX3X3_USE(h));
	MATRIX3X3_DEF_OUT(u);
	MATRIX3X3_DEF_OUT(h);
	return ret;
}

unsigned int btPolarDecomposition_maxIterations(btPolarDecomposition* obj)
{
	return obj->maxIterations();
}

void btPolarDecomposition_delete(btPolarDecomposition* obj)
{
	delete obj;
}
