#include <../Extras/Serialize/BulletFileLoader/btBulletFile.h>

#include "btBulletFile_wrap.h"

#ifndef BULLETC_DISABLE_WORLD_IMPORTERS

bParse_btBulletFile* btBulletFile_new()
{
	return new bParse_btBulletFile();
}

bParse_btBulletFile* btBulletFile_new2(const char* fileName)
{
	return new bParse_btBulletFile(fileName);
}

bParse_btBulletFile* btBulletFile_new3(char* memoryBuffer, int len)
{
	return new bParse_btBulletFile(memoryBuffer, len);
}

void btBulletFile_addStruct(bParse_btBulletFile* obj, const char* structType, void* data, int len, void* oldPtr, int code)
{
	obj->addStruct(structType, data, len, oldPtr, code);
}

btAlignedStructHandleArray* btBulletFile_getBvhs(bParse_btBulletFile* obj)
{
	return &obj->m_bvhs;
}

btAlignedStructHandleArray* btBulletFile_getCollisionObjects(bParse_btBulletFile* obj)
{
	return &obj->m_collisionObjects;
}

btAlignedStructHandleArray* btBulletFile_getCollisionShapes(bParse_btBulletFile* obj)
{
	return &obj->m_collisionShapes;
}

btAlignedStructHandleArray* btBulletFile_getConstraints(bParse_btBulletFile* obj)
{
	return &obj->m_constraints;
}

btAligendCharPtrArray* btBulletFile_getDataBlocks(bParse_btBulletFile* obj)
{
	return &obj->m_dataBlocks;
}

btAlignedStructHandleArray* btBulletFile_getDynamicsWorldInfo(bParse_btBulletFile* obj)
{
	return &obj->m_dynamicsWorldInfo;
}

btAlignedStructHandleArray* btBulletFile_getRigidBodies(bParse_btBulletFile* obj)
{
	return &obj->m_rigidBodies;
}

btAlignedStructHandleArray* btBulletFile_getSoftBodies(bParse_btBulletFile* obj)
{
	return &obj->m_softBodies;
}

btAlignedStructHandleArray* btBulletFile_getTriangleInfoMaps(bParse_btBulletFile* obj)
{
	return &obj->m_triangleInfoMaps;
}

void btBulletFile_parseData(bParse_btBulletFile* obj)
{
	obj->parseData();
}

#endif
