#include "main.h"

extern "C"
{
	EXPORT void btConcaveShape_processAllTriangles(btConcaveShape* obj, btTriangleCallback* callback, const btVector3* aabbMin, const btVector3* aabbMax);
}
