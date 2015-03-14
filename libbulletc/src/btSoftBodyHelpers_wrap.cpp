#include <BulletSoftBody/btSoftBodyHelpers.h>

#include "conversion.h"
#include "btSoftBodyHelpers_wrap.h"

#ifndef BULLETC_DISABLE_SOFTBODY_HELPERS
float btSoftBodyHelpers_CalculateUV(int resx, int resy, int ix, int iy, int id)
{
	return btSoftBodyHelpers::CalculateUV(resx, resy, ix, iy, id);
}

btSoftBody* btSoftBodyHelpers_CreateEllipsoid(btSoftBodyWorldInfo* worldInfo, const btScalar* center, const btScalar* radius, int res)
{
	VECTOR3_CONV(center);
	VECTOR3_CONV(radius);
	return btSoftBodyHelpers::CreateEllipsoid(*worldInfo, VECTOR3_USE(center), VECTOR3_USE(radius), res);
}
#endif
btSoftBody* btSoftBodyHelpers_CreateFromConvexHull(btSoftBodyWorldInfo* worldInfo, const btScalar* vertices, int nvertices)
{
	btVector3* verticesTemp = Vector3ArrayIn(vertices, nvertices);
	btSoftBody* ret = btSoftBodyHelpers::CreateFromConvexHull(*worldInfo, verticesTemp, nvertices);
	delete[] verticesTemp;
	return ret;
}

btSoftBody* btSoftBodyHelpers_CreateFromConvexHull2(btSoftBodyWorldInfo* worldInfo, const btScalar* vertices, int nvertices, bool randomizeConstraints)
{
	btVector3* verticesTemp = Vector3ArrayIn(vertices, nvertices);
	btSoftBody* ret = btSoftBodyHelpers::CreateFromConvexHull(*worldInfo, verticesTemp, nvertices, randomizeConstraints);
	delete[] verticesTemp;
	return ret;
}
#ifndef BULLETC_DISABLE_SOFTBODY_HELPERS
btSoftBody* btSoftBodyHelpers_CreateFromTetGenData(btSoftBodyWorldInfo* worldInfo, const char* ele, const char* face, const char* node, bool bfacelinks, bool btetralinks, bool bfacesfromtetras)
{
	return btSoftBodyHelpers::CreateFromTetGenData(*worldInfo, ele, face, node, bfacelinks, btetralinks, bfacesfromtetras);
}

btSoftBody* btSoftBodyHelpers_CreateFromTriMesh(btSoftBodyWorldInfo* worldInfo, const btScalar* vertices, const int* triangles, int ntriangles)
{
	return btSoftBodyHelpers::CreateFromTriMesh(*worldInfo, vertices, triangles, ntriangles);
}

btSoftBody* btSoftBodyHelpers_CreateFromTriMesh2(btSoftBodyWorldInfo* worldInfo, const btScalar* vertices, const int* triangles, int ntriangles, bool randomizeConstraints)
{
	return btSoftBodyHelpers::CreateFromTriMesh(*worldInfo, vertices, triangles, ntriangles, randomizeConstraints);
}

btSoftBody* btSoftBodyHelpers_CreatePatch(btSoftBodyWorldInfo* worldInfo, const btScalar* corner00, const btScalar* corner10, const btScalar* corner01, const btScalar* corner11, int resx, int resy, int fixeds, bool gendiags)
{
	VECTOR3_CONV(corner00);
	VECTOR3_CONV(corner10);
	VECTOR3_CONV(corner01);
	VECTOR3_CONV(corner11);
	return btSoftBodyHelpers::CreatePatch(*worldInfo, VECTOR3_USE(corner00), VECTOR3_USE(corner10), VECTOR3_USE(corner01), VECTOR3_USE(corner11), resx, resy, fixeds, gendiags);
}
#endif
btSoftBody* btSoftBodyHelpers_CreatePatchUV(btSoftBodyWorldInfo* worldInfo, const btScalar* corner00, const btScalar* corner10, const btScalar* corner01, const btScalar* corner11, int resx, int resy, int fixeds, bool gendiags)
{
	VECTOR3_CONV(corner00);
	VECTOR3_CONV(corner10);
	VECTOR3_CONV(corner01);
	VECTOR3_CONV(corner11);
	return btSoftBodyHelpers::CreatePatchUV(*worldInfo, VECTOR3_USE(corner00), VECTOR3_USE(corner10), VECTOR3_USE(corner01), VECTOR3_USE(corner11), resx, resy, fixeds, gendiags);
}

btSoftBody* btSoftBodyHelpers_CreatePatchUV2(btSoftBodyWorldInfo* worldInfo, const btScalar* corner00, const btScalar* corner10, const btScalar* corner01, const btScalar* corner11, int resx, int resy, int fixeds, bool gendiags, float* tex_coords)
{
	VECTOR3_CONV(corner00);
	VECTOR3_CONV(corner10);
	VECTOR3_CONV(corner01);
	VECTOR3_CONV(corner11);
	return btSoftBodyHelpers::CreatePatchUV(*worldInfo, VECTOR3_USE(corner00), VECTOR3_USE(corner10), VECTOR3_USE(corner01), VECTOR3_USE(corner11), resx, resy, fixeds, gendiags, tex_coords);
}
#ifndef BULLETC_DISABLE_SOFTBODY_HELPERS
btSoftBody* btSoftBodyHelpers_CreateRope(btSoftBodyWorldInfo* worldInfo, const btScalar* from, const btScalar* to, int res, int fixeds)
{
	VECTOR3_CONV(from);
	VECTOR3_CONV(to);
	return btSoftBodyHelpers::CreateRope(*worldInfo, VECTOR3_USE(from), VECTOR3_USE(to), res, fixeds);
}
#endif
void btSoftBodyHelpers_Draw(btSoftBody* psb, btIDebugDraw* idraw)
{
	btSoftBodyHelpers::Draw(psb, idraw);
}

void btSoftBodyHelpers_Draw2(btSoftBody* psb, btIDebugDraw* idraw, int drawflags)
{
	btSoftBodyHelpers::Draw(psb, idraw, drawflags);
}

void btSoftBodyHelpers_DrawClusterTree(btSoftBody* psb, btIDebugDraw* idraw)
{
	btSoftBodyHelpers::DrawClusterTree(psb, idraw);
}

void btSoftBodyHelpers_DrawClusterTree2(btSoftBody* psb, btIDebugDraw* idraw, int mindepth)
{
	btSoftBodyHelpers::DrawClusterTree(psb, idraw, mindepth);
}

void btSoftBodyHelpers_DrawClusterTree3(btSoftBody* psb, btIDebugDraw* idraw, int mindepth, int maxdepth)
{
	btSoftBodyHelpers::DrawClusterTree(psb, idraw, mindepth, maxdepth);
}

void btSoftBodyHelpers_DrawFaceTree(btSoftBody* psb, btIDebugDraw* idraw)
{
	btSoftBodyHelpers::DrawFaceTree(psb, idraw);
}

void btSoftBodyHelpers_DrawFaceTree2(btSoftBody* psb, btIDebugDraw* idraw, int mindepth)
{
	btSoftBodyHelpers::DrawFaceTree(psb, idraw, mindepth);
}

void btSoftBodyHelpers_DrawFaceTree3(btSoftBody* psb, btIDebugDraw* idraw, int mindepth, int maxdepth)
{
	btSoftBodyHelpers::DrawFaceTree(psb, idraw, mindepth, maxdepth);
}

void btSoftBodyHelpers_DrawFrame(btSoftBody* psb, btIDebugDraw* idraw)
{
	btSoftBodyHelpers::DrawFrame(psb, idraw);
}

void btSoftBodyHelpers_DrawInfos(btSoftBody* psb, btIDebugDraw* idraw, bool masses, bool areas, bool stress)
{
	btSoftBodyHelpers::DrawInfos(psb, idraw, masses, areas, stress);
}

void btSoftBodyHelpers_DrawNodeTree(btSoftBody* psb, btIDebugDraw* idraw)
{
	btSoftBodyHelpers::DrawNodeTree(psb, idraw);
}

void btSoftBodyHelpers_DrawNodeTree2(btSoftBody* psb, btIDebugDraw* idraw, int mindepth)
{
	btSoftBodyHelpers::DrawNodeTree(psb, idraw, mindepth);
}

void btSoftBodyHelpers_DrawNodeTree3(btSoftBody* psb, btIDebugDraw* idraw, int mindepth, int maxdepth)
{
	btSoftBodyHelpers::DrawNodeTree(psb, idraw, mindepth, maxdepth);
}
#ifndef BULLETC_DISABLE_SOFTBODY_HELPERS
void btSoftBodyHelpers_ReoptimizeLinkOrder(btSoftBody* psb)
{
	btSoftBodyHelpers::ReoptimizeLinkOrder(psb);
}
#endif
