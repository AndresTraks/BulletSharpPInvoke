#include "main.h"

extern "C"
{
	EXPORT btSparseSdf_3* btSparseSdf_new();
	EXPORT void btSparseSdf3_GarbageCollect(btSparseSdf_3* obj, int lifetime);
	EXPORT void btSparseSdf3_GarbageCollect2(btSparseSdf_3* obj);
	EXPORT void btSparseSdf3_Initialize(btSparseSdf_3* obj, int hashsize, int clampCells);
	EXPORT void btSparseSdf3_Initialize2(btSparseSdf_3* obj, int hashsize);
	EXPORT void btSparseSdf3_Initialize3(btSparseSdf_3* obj);
	EXPORT int btSparseSdf3_RemoveReferences(btSparseSdf_3* obj, btCollisionShape* pcs);
	EXPORT void btSparseSdf3_Reset(btSparseSdf_3* obj);
	EXPORT void btSparseSdf_delete(btSparseSdf_3* obj);
}
