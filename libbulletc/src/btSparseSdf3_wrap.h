#include "main.h"

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btSparseSdf_3* btSparseSdf_new();
	EXPORT void btSparseSdf3_GarbageCollect(btSparseSdf_3* obj, int lifetime);
	EXPORT btScalar btSparseSdf3_getDefaultVoxelsz(btSparseSdf_3* obj);
	EXPORT void btSparseSdf3_Initialize(btSparseSdf_3* obj, int hashsize, int clampCells);
	EXPORT int btSparseSdf3_RemoveReferences(btSparseSdf_3* obj, btCollisionShape* pcs);
	EXPORT void btSparseSdf3_Reset(btSparseSdf_3* obj);
	EXPORT void btSparseSdf3_setDefaultVoxelsz(btSparseSdf_3* obj, btScalar sz);
	EXPORT void btSparseSdf_delete(btSparseSdf_3* obj);
#ifdef __cplusplus
}
#endif
