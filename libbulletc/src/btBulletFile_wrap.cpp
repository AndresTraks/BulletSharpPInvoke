#include <../BulletFileLoader/btBulletFile.h>

#include "btBulletFile_wrap.h"

#ifndef BULLETC_DISABLE_WORLD_IMPORTERS

bParse_btBulletFile* bParse_btBulletFile_new()
{
	return new bParse_btBulletFile();
}

bParse_btBulletFile* bParse_btBulletFile_new2(const char* fileName)
{
	return new bParse_btBulletFile(fileName);
}

bParse_btBulletFile* bParse_btBulletFile_new3(char* memoryBuffer, int len)
{
	return new bParse_btBulletFile(memoryBuffer, len);
}

void bParse_btBulletFile_addDataBlock(bParse_btBulletFile* obj, char* dataBlock)
{
	obj->addDataBlock(dataBlock);
}

void bParse_btBulletFile_addStruct(bParse_btBulletFile* obj, const char* structType, void* data, int len, void* oldPtr, int code)
{
	obj->addStruct(structType, data, len, oldPtr, code);
}

btAlignedStructHandleArray* bParse_btBulletFile_getBvhs(bParse_btBulletFile* obj)
{
	return &obj->m_bvhs;
}

btAlignedStructHandleArray* bParse_btBulletFile_getCollisionObjects(bParse_btBulletFile* obj)
{
	return &obj->m_collisionObjects;
}

btAlignedStructHandleArray* bParse_btBulletFile_getCollisionShapes(bParse_btBulletFile* obj)
{
	return &obj->m_collisionShapes;
}

btAlignedStructHandleArray* bParse_btBulletFile_getConstraints(bParse_btBulletFile* obj)
{
	return &obj->m_constraints;
}

btAligendCharPtrArray* bParse_btBulletFile_getDataBlocks(bParse_btBulletFile* obj)
{
	return &obj->m_dataBlocks;
}

btAlignedStructHandleArray* bParse_btBulletFile_getDynamicsWorldInfo(bParse_btBulletFile* obj)
{
	return &obj->m_dynamicsWorldInfo;
}

btAlignedStructHandleArray* bParse_btBulletFile_getMultiBodies(bParse_btBulletFile* obj)
{
	return &obj->m_multiBodies;
}

btAlignedStructHandleArray* bParse_btBulletFile_getRigidBodies(bParse_btBulletFile* obj)
{
	return &obj->m_rigidBodies;
}

btAlignedStructHandleArray* bParse_btBulletFile_getSoftBodies(bParse_btBulletFile* obj)
{
	return &obj->m_softBodies;
}

btAlignedStructHandleArray* bParse_btBulletFile_getTriangleInfoMaps(bParse_btBulletFile* obj)
{
	return &obj->m_triangleInfoMaps;
}

void bParse_btBulletFile_parse(bParse_btBulletFile* obj, int verboseMode)
{
	obj->parse(verboseMode);
}

void bParse_btBulletFile_parseData(bParse_btBulletFile* obj)
{
	obj->parseData();
}

int bParse_btBulletFile_write(bParse_btBulletFile* obj, const char* fileName)
{
	return obj->write(fileName);
}

int bParse_btBulletFile_write2(bParse_btBulletFile* obj, const char* fileName, bool fixupPointers)
{
	return obj->write(fileName, fixupPointers);
}

void bParse_btBulletFile_writeDNA(bParse_btBulletFile* obj, FILE* fp)
{
	obj->writeDNA(fp);
}

void bParse_btBulletFile_delete(bParse_btBulletFile* obj)
{
	delete obj;
}

#endif
