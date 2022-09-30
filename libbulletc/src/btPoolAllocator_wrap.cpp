#include <LinearMath/btPoolAllocator.h>

#include "conversion.h"
#include "btPoolAllocator_wrap.h"

btPoolAllocator* btPoolAllocator_new(int elemSize, int maxElements)
{
	return new btPoolAllocator(elemSize, maxElements);
}

void* btPoolAllocator_allocate(btPoolAllocator* obj, int size)
{
	return obj->allocate(size);
}

void btPoolAllocator_freeMemory(btPoolAllocator* obj, void* ptr)
{
	obj->freeMemory(ptr);
}

int btPoolAllocator_getElementSize(btPoolAllocator* obj)
{
	return obj->getElementSize();
}

int btPoolAllocator_getFreeCount(btPoolAllocator* obj)
{
	return obj->getFreeCount();
}

int btPoolAllocator_getMaxCount(btPoolAllocator* obj)
{
	return obj->getMaxCount();
}

unsigned char* btPoolAllocator_getPoolAddress(btPoolAllocator* obj)
{
	return obj->getPoolAddress();
}

int btPoolAllocator_getUsedCount(btPoolAllocator* obj)
{
	return obj->getUsedCount();
}

bool btPoolAllocator_validPtr(btPoolAllocator* obj, void* ptr)
{
	return obj->validPtr(ptr);
}

void btPoolAllocator_delete(btPoolAllocator* obj)
{
	delete obj;
}
