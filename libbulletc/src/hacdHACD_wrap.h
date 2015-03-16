#include "main.h"

extern "C"
{
	EXPORT HACD_HACD* HACD_new();
	EXPORT bool HACD_Compute(HACD_HACD* obj);
	EXPORT bool HACD_Compute2(HACD_HACD* obj, bool fullCH);
	EXPORT bool HACD_Compute3(HACD_HACD* obj, bool fullCH, bool exportDistPoints);
	EXPORT void HACD_DenormalizeData(HACD_HACD* obj);
	EXPORT bool HACD_GetAddExtraDistPoints(HACD_HACD* obj);
	EXPORT bool HACD_GetAddFacesPoints(HACD_HACD* obj);
	EXPORT bool HACD_GetAddNeighboursDistPoints(HACD_HACD* obj);
	//EXPORT const * HACD_GetCallBack(HACD_HACD* obj);
	//EXPORT bool HACD_GetCH(HACD_HACD* obj, int numCH, HACD_Vec3* points, HACD_Vec3* triangles);
	EXPORT double HACD_GetCompacityWeight(HACD_HACD* obj);
	EXPORT double HACD_GetConcavity(HACD_HACD* obj);
	EXPORT double HACD_GetConnectDist(HACD_HACD* obj);
	EXPORT int HACD_GetNClusters(HACD_HACD* obj);
	EXPORT int HACD_GetNPoints(HACD_HACD* obj);
	EXPORT int HACD_GetNPointsCH(HACD_HACD* obj, int numCH);
	EXPORT int HACD_GetNTriangles(HACD_HACD* obj);
	EXPORT int HACD_GetNTrianglesCH(HACD_HACD* obj, int numCH);
	EXPORT int HACD_GetNVerticesPerCH(HACD_HACD* obj);
	EXPORT const long* HACD_GetPartition(HACD_HACD* obj);
	//EXPORT const HACD_Vec3* HACD_GetPoints(HACD_HACD* obj);
	EXPORT double HACD_GetScaleFactor(HACD_HACD* obj);
	//EXPORT const HACD_Vec3* HACD_GetTriangles(HACD_HACD* obj);
	EXPORT double HACD_GetVolumeWeight(HACD_HACD* obj);
	EXPORT void HACD_NormalizeData(HACD_HACD* obj);
	EXPORT bool HACD_Save(HACD_HACD* obj, const char* fileName, bool uniColor);
	EXPORT bool HACD_Save2(HACD_HACD* obj, const char* fileName, bool uniColor, long numCluster);
	EXPORT void HACD_SetAddExtraDistPoints(HACD_HACD* obj, bool addExtraDistPoints);
	EXPORT void HACD_SetAddFacesPoints(HACD_HACD* obj, bool addFacesPoints);
	EXPORT void HACD_SetAddNeighboursDistPoints(HACD_HACD* obj, bool addNeighboursDistPoints);
	//EXPORT void HACD_SetCallBack(HACD_HACD* obj, * callBack);
	EXPORT void HACD_SetCompacityWeight(HACD_HACD* obj, double alpha);
	EXPORT void HACD_SetConcavity(HACD_HACD* obj, double concavity);
	EXPORT void HACD_SetConnectDist(HACD_HACD* obj, double ccConnectDist);
	EXPORT void HACD_SetNClusters(HACD_HACD* obj, int nClusters);
	EXPORT void HACD_SetNPoints(HACD_HACD* obj, int nPoints);
	EXPORT void HACD_SetNTriangles(HACD_HACD* obj, int nTriangles);
	EXPORT void HACD_SetNVerticesPerCH(HACD_HACD* obj, int nVerticesPerCH);
	//EXPORT void HACD_SetPoints(HACD_HACD* obj, HACD_Vec3* points);
	EXPORT void HACD_SetScaleFactor(HACD_HACD* obj, double scale);
	//EXPORT void HACD_SetTriangles(HACD_HACD* obj, HACD_Vec3* triangles);
	EXPORT void HACD_SetVolumeWeight(HACD_HACD* obj, double beta);
	EXPORT void HACD_delete(HACD_HACD* obj);
}
