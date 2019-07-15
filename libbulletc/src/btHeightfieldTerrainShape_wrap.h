#include "main.h"

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btHeightfieldTerrainShape* btHeightfieldTerrainShape_new(int heightStickWidth, int heightStickLength, const void* heightfieldData, btScalar heightScale, btScalar minHeight, btScalar maxHeight, int upAxis, PHY_ScalarType heightDataType, bool flipQuadEdges);
	EXPORT btHeightfieldTerrainShape* btHeightfieldTerrainShape_new2(int heightStickWidth, int heightStickLength, const void* heightfieldData, btScalar maxHeight, int upAxis, bool useFloatData, bool flipQuadEdges);
	EXPORT void btHeightfieldTerrainShape_performRaycast(btHeightfieldTerrainShape* obj, btTriangleCallback* callback, const btVector3* raySource, const btVector3* rayTarget);
	EXPORT void btHeightfieldTerrainShape_buildAccelerator(btHeightfieldTerrainShape* obj, int chunkSize);
	EXPORT void btHeightfieldTerrainShape_clearAccelerator(btHeightfieldTerrainShape* obj);
	EXPORT int btHeightfieldTerrainShape_getUpAxis(btHeightfieldTerrainShape* obj);
	EXPORT void btHeightfieldTerrainShape_setFlipTriangleWinding(btHeightfieldTerrainShape* obj, bool flipTriangleWinding);
	EXPORT void btHeightfieldTerrainShape_setUseDiamondSubdivision(btHeightfieldTerrainShape* obj);
	EXPORT void btHeightfieldTerrainShape_setUseDiamondSubdivision2(btHeightfieldTerrainShape* obj, bool useDiamondSubdivision);
	EXPORT void btHeightfieldTerrainShape_setUseZigzagSubdivision(btHeightfieldTerrainShape* obj);
	EXPORT void btHeightfieldTerrainShape_setUseZigzagSubdivision2(btHeightfieldTerrainShape* obj, bool useZigzagSubdivision);
#ifdef __cplusplus
}
#endif
