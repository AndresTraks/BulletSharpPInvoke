#include <BulletSoftBody/btSparseSDF.h>

#include "btSparseSdf3_wrap.h"

btSparseSdf<3>* btSparseSdf_new()
{
	return new btSparseSdf<3>();
}

void btSparseSdf3_GarbageCollect(btSparseSdf<3>* obj, int lifetime)
{
	obj->GarbageCollect(lifetime);
}

void btSparseSdf3_GarbageCollect2(btSparseSdf<3>* obj)
{
	obj->GarbageCollect();
}

void btSparseSdf3_Initialize(btSparseSdf<3>* obj, int hashsize, int clampCells)
{
	obj->Initialize(hashsize, clampCells);
}

void btSparseSdf3_Initialize2(btSparseSdf<3>* obj, int hashsize)
{
	obj->Initialize(hashsize);
}

void btSparseSdf3_Initialize3(btSparseSdf<3>* obj)
{
	obj->Initialize();
}

int btSparseSdf3_RemoveReferences(btSparseSdf<3>* obj, btCollisionShape* pcs)
{
	return obj->RemoveReferences(pcs);
}

void btSparseSdf3_Reset(btSparseSdf<3>* obj)
{
	obj->Reset();
}

void btSparseSdf_delete(btSparseSdf<3>* obj)
{
	delete obj;
}
