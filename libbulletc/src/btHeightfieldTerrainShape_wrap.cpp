#include <BulletCollision/CollisionShapes/btHeightfieldTerrainShape.h>

#include "conversion.h"
#include "btHeightfieldTerrainShape_wrap.h"

btHeightfieldTerrainShape* btHeightfieldTerrainShape_new(int heightStickWidth, int heightStickLength,
	const void* heightfieldData, btScalar heightScale, btScalar minHeight, btScalar maxHeight,
	int upAxis, PHY_ScalarType heightDataType, bool flipQuadEdges)
{
	return new btHeightfieldTerrainShape(heightStickWidth, heightStickLength, heightfieldData,
		heightScale, minHeight, maxHeight, upAxis, heightDataType, flipQuadEdges);
}

btHeightfieldTerrainShape* btHeightfieldTerrainShape_new2(int heightStickWidth, int heightStickLength,
	const void* heightfieldData, btScalar maxHeight, int upAxis, bool useFloatData,
	bool flipQuadEdges)
{
	return new btHeightfieldTerrainShape(heightStickWidth, heightStickLength, heightfieldData,
		maxHeight, upAxis, useFloatData, flipQuadEdges);
}

void btHeightfieldTerrainShape_performRaycast(btHeightfieldTerrainShape* obj, btTriangleCallback* callback, const btVector3* raySource, const btVector3* rayTarget)
{
	BTVECTOR3_IN(raySource);
	BTVECTOR3_IN(rayTarget);
	obj->performRaycast(callback, BTVECTOR3_USE(raySource), BTVECTOR3_USE(rayTarget));
}

void btHeightfieldTerrainShape_buildAccelerator(btHeightfieldTerrainShape* obj, int chunkSize)
{
	obj->buildAccelerator(chunkSize);
}

void btHeightfieldTerrainShape_clearAccelerator(btHeightfieldTerrainShape* obj)
{
	obj->clearAccelerator();
}

int btHeightfieldTerrainShape_getUpAxis(btHeightfieldTerrainShape* obj)
{
	return obj->getUpAxis();
}

void btHeightfieldTerrainShape_setFlipTriangleWinding(btHeightfieldTerrainShape* obj, bool flipTriangleWinding)
{
	obj->setFlipTriangleWinding(flipTriangleWinding);
}

void btHeightfieldTerrainShape_setUseDiamondSubdivision(btHeightfieldTerrainShape* obj)
{
	obj->setUseDiamondSubdivision();
}

void btHeightfieldTerrainShape_setUseDiamondSubdivision2(btHeightfieldTerrainShape* obj,
	bool useDiamondSubdivision)
{
	obj->setUseDiamondSubdivision(useDiamondSubdivision);
}

void btHeightfieldTerrainShape_setUseZigzagSubdivision(btHeightfieldTerrainShape* obj)
{
	obj->setUseZigzagSubdivision();
}

void btHeightfieldTerrainShape_setUseZigzagSubdivision2(btHeightfieldTerrainShape* obj,
	bool useZigzagSubdivision)
{
	obj->setUseZigzagSubdivision(useZigzagSubdivision);
}
