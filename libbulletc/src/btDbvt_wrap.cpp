#include <BulletCollision/BroadphaseCollision/btDbvt.h>

#include "conversion.h"
#include "btDbvt_wrap.h"

btDbvtAabbMm* btDbvtAabbMm_new()
{
	return new btDbvtAabbMm();
}

void btDbvtAabbMm_Center(btDbvtAabbMm* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->Center(), value);
}

int btDbvtAabbMm_Classify(btDbvtAabbMm* obj, const btScalar* n, btScalar o, int s)
{
	VECTOR3_CONV(n);
	return obj->Classify(VECTOR3_USE(n), o, s);
}

bool btDbvtAabbMm_Contain(btDbvtAabbMm* obj, const btDbvtAabbMm* a)
{
	return obj->Contain(*a);
}

void btDbvtAabbMm_Expand(btDbvtAabbMm* obj, const btScalar* e)
{
	VECTOR3_CONV(e);
	obj->Expand(VECTOR3_USE(e));
}

void btDbvtAabbMm_Extents(btDbvtAabbMm* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->Extents(), value);
}

btDbvtAabbMm* btDbvtAabbMm_FromCE(const btScalar* c, const btScalar* e)
{
	btDbvtAabbMm* ret = new btDbvtAabbMm;
	VECTOR3_CONV(c);
	VECTOR3_CONV(e);
	*ret = btDbvtAabbMm::FromCE(VECTOR3_USE(c), VECTOR3_USE(e));
	return ret;
}

btDbvtAabbMm* btDbvtAabbMm_FromCR(const btScalar* c, btScalar r)
{
	btDbvtAabbMm* ret = new btDbvtAabbMm;
	VECTOR3_CONV(c);
	*ret = btDbvtAabbMm::FromCR(VECTOR3_USE(c), r);
	return ret;
}

btDbvtAabbMm* btDbvtAabbMm_FromMM(const btScalar* mi, const btScalar* mx)
{
	btDbvtAabbMm* ret = new btDbvtAabbMm;
	VECTOR3_CONV(mi);
	VECTOR3_CONV(mx);
	*ret = btDbvtAabbMm::FromMM(VECTOR3_USE(mi), VECTOR3_USE(mx));
	return ret;
}

btDbvtAabbMm* btDbvtAabbMm_FromPoints(const btVector3** ppts, int n)
{
	btDbvtAabbMm* ret = new btDbvtAabbMm;
	*ret = btDbvtAabbMm::FromPoints(ppts, n);
	return ret;
}

btDbvtAabbMm* btDbvtAabbMm_FromPoints2(const btVector3* pts, int n)
{
	btDbvtAabbMm* ret = new btDbvtAabbMm;
	*ret = btDbvtAabbMm::FromPoints(pts, n);
	return ret;
}

void btDbvtAabbMm_Lengths(btDbvtAabbMm* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->Lengths(), value);
}

void btDbvtAabbMm_Maxs(btDbvtAabbMm* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->Maxs(), value);
}

void btDbvtAabbMm_Mins(btDbvtAabbMm* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->Mins(), value);
}

btScalar btDbvtAabbMm_ProjectMinimum(btDbvtAabbMm* obj, const btScalar* v, unsigned int signs)
{
	VECTOR3_CONV(v);
	return obj->ProjectMinimum(VECTOR3_USE(v), signs);
}

void btDbvtAabbMm_SignedExpand(btDbvtAabbMm* obj, const btScalar* e)
{
	VECTOR3_CONV(e);
	obj->SignedExpand(VECTOR3_USE(e));
}

void btDbvtAabbMm_tMaxs(btDbvtAabbMm* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->tMaxs(), value);
}

void btDbvtAabbMm_tMins(btDbvtAabbMm* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->tMins(), value);
}

void btDbvtAabbMm_delete(btDbvtAabbMm* obj)
{
	delete obj;
}


btDbvtNode* btDbvtNode_new()
{
	return new btDbvtNode();
}

btDbvtNode** btDbvtNode_getChilds(btDbvtNode* obj)
{
	return obj->childs;
}

void* btDbvtNode_getData(btDbvtNode* obj)
{
	return obj->data;
}

int btDbvtNode_getDataAsInt(btDbvtNode* obj)
{
	return obj->dataAsInt;
}

btDbvtNode* btDbvtNode_getParent(btDbvtNode* obj)
{
	return obj->parent;
}

btDbvtVolume* btDbvtNode_getVolume(btDbvtNode* obj)
{
	return &obj->volume;
}

bool btDbvtNode_isinternal(btDbvtNode* obj)
{
	return obj->isinternal();
}

bool btDbvtNode_isleaf(btDbvtNode* obj)
{
	return obj->isleaf();
}

void btDbvtNode_setData(btDbvtNode* obj, void* value)
{
	obj->data = value;
}

void btDbvtNode_setDataAsInt(btDbvtNode* obj, int value)
{
	obj->dataAsInt = value;
}

void btDbvtNode_setParent(btDbvtNode* obj, btDbvtNode* value)
{
	obj->parent = value;
}

void btDbvtNode_delete(btDbvtNode* obj)
{
	delete obj;
}


btDbvt::IClone* btDbvt_IClone_new()
{
	return new btDbvt::IClone();
}

void btDbvt_IClone_CloneLeaf(btDbvt::IClone* obj, btDbvtNode* __unnamed0)
{
	obj->CloneLeaf(__unnamed0);
}

void btDbvt_IClone_delete(btDbvt::IClone* obj)
{
	delete obj;
}


btDbvt::ICollide* btDbvt_ICollide_new()
{
	return new btDbvt::ICollide();
}

bool btDbvt_ICollide_AllLeaves(btDbvt::ICollide* obj, const btDbvtNode* __unnamed0)
{
	return obj->AllLeaves(__unnamed0);
}

bool btDbvt_ICollide_Descent(btDbvt::ICollide* obj, const btDbvtNode* __unnamed0)
{
	return obj->Descent(__unnamed0);
}

void btDbvt_ICollide_Process(btDbvt::ICollide* obj, const btDbvtNode* __unnamed0, const btDbvtNode* __unnamed1)
{
	obj->Process(__unnamed0, __unnamed1);
}

void btDbvt_ICollide_Process2(btDbvt::ICollide* obj, const btDbvtNode* __unnamed0)
{
	obj->Process(__unnamed0);
}

void btDbvt_ICollide_Process3(btDbvt::ICollide* obj, const btDbvtNode* n, btScalar __unnamed1)
{
	obj->Process(n, __unnamed1);
}

void btDbvt_ICollide_delete(btDbvt::ICollide* obj)
{
	delete obj;
}


void btDbvt_IWriter_Prepare(btDbvt::IWriter* obj, const btDbvtNode* root, int numnodes)
{
	obj->Prepare(root, numnodes);
}

void btDbvt_IWriter_WriteLeaf(btDbvt::IWriter* obj, const btDbvtNode* __unnamed0, int index, int parent)
{
	obj->WriteLeaf(__unnamed0, index, parent);
}

void btDbvt_IWriter_WriteNode(btDbvt::IWriter* obj, const btDbvtNode* __unnamed0, int index, int parent, int child0, int child1)
{
	obj->WriteNode(__unnamed0, index, parent, child0, child1);
}

void btDbvt_IWriter_delete(btDbvt::IWriter* obj)
{
	delete obj;
}


btDbvt::sStkCLN* btDbvt_sStkCLN_new(const btDbvtNode* n, btDbvtNode* p)
{
	return new btDbvt::sStkCLN(n, p);
}

const btDbvtNode* btDbvt_sStkCLN_getNode(btDbvt::sStkCLN* obj)
{
	return obj->node;
}

btDbvtNode* btDbvt_sStkCLN_getParent(btDbvt::sStkCLN* obj)
{
	return obj->parent;
}

void btDbvt_sStkCLN_setNode(btDbvt::sStkCLN* obj, const btDbvtNode* value)
{
	obj->node = value;
}

void btDbvt_sStkCLN_setParent(btDbvt::sStkCLN* obj, btDbvtNode* value)
{
	obj->parent = value;
}

void btDbvt_sStkCLN_delete(btDbvt::sStkCLN* obj)
{
	delete obj;
}


btDbvt::sStkNN* btDbvt_sStkNN_new()
{
	return new btDbvt::sStkNN();
}

btDbvt::sStkNN* btDbvt_sStkNN_new2(const btDbvtNode* na, const btDbvtNode* nb)
{
	return new btDbvt::sStkNN(na, nb);
}

const btDbvtNode* btDbvt_sStkNN_getA(btDbvt::sStkNN* obj)
{
	return obj->a;
}

const btDbvtNode* btDbvt_sStkNN_getB(btDbvt::sStkNN* obj)
{
	return obj->b;
}

void btDbvt_sStkNN_setA(btDbvt::sStkNN* obj, const btDbvtNode* value)
{
	obj->a = value;
}

void btDbvt_sStkNN_setB(btDbvt::sStkNN* obj, const btDbvtNode* value)
{
	obj->b = value;
}

void btDbvt_sStkNN_delete(btDbvt::sStkNN* obj)
{
	delete obj;
}


btDbvt::sStkNP* btDbvt_sStkNP_new(const btDbvtNode* n, unsigned int m)
{
	return new btDbvt::sStkNP(n, m);
}

int btDbvt_sStkNP_getMask(btDbvt::sStkNP* obj)
{
	return obj->mask;
}

const btDbvtNode* btDbvt_sStkNP_getNode(btDbvt::sStkNP* obj)
{
	return obj->node;
}

void btDbvt_sStkNP_setMask(btDbvt::sStkNP* obj, int value)
{
	obj->mask = value;
}

void btDbvt_sStkNP_setNode(btDbvt::sStkNP* obj, const btDbvtNode* value)
{
	obj->node = value;
}

void btDbvt_sStkNP_delete(btDbvt::sStkNP* obj)
{
	delete obj;
}


btDbvt::sStkNPS* btDbvt_sStkNPS_new()
{
	return new btDbvt::sStkNPS();
}

btDbvt::sStkNPS* btDbvt_sStkNPS_new2(const btDbvtNode* n, unsigned int m, btScalar v)
{
	return new btDbvt::sStkNPS(n, m, v);
}

int btDbvt_sStkNPS_getMask(btDbvt::sStkNPS* obj)
{
	return obj->mask;
}

const btDbvtNode* btDbvt_sStkNPS_getNode(btDbvt::sStkNPS* obj)
{
	return obj->node;
}

btScalar btDbvt_sStkNPS_getValue(btDbvt::sStkNPS* obj)
{
	return obj->value;
}

void btDbvt_sStkNPS_setMask(btDbvt::sStkNPS* obj, int value)
{
	obj->mask = value;
}

void btDbvt_sStkNPS_setNode(btDbvt::sStkNPS* obj, const btDbvtNode* value)
{
	obj->node = value;
}

void btDbvt_sStkNPS_setValue(btDbvt::sStkNPS* obj, btScalar value)
{
	obj->value = value;
}

void btDbvt_sStkNPS_delete(btDbvt::sStkNPS* obj)
{
	delete obj;
}


btDbvt* btDbvt_new()
{
	return new btDbvt();
}

int btDbvt_allocate(btAlignedIntArray* ifree, btAlignedStkNpsArray* stock, const btDbvt::sStkNPS* value)
{
	return btDbvt::allocate(*ifree, *stock, *value);
}

void btDbvt_benchmark()
{
	btDbvt::benchmark();
}

void btDbvt_clear(btDbvt* obj)
{
	obj->clear();
}

void btDbvt_clone(btDbvt* obj, btDbvt* dest)
{
	obj->clone(*dest);
}

void btDbvt_clone2(btDbvt* obj, btDbvt* dest, btDbvt::IClone* iclone)
{
	obj->clone(*dest, iclone);
}
/*
void btDbvt_collideKDOP(const btDbvtNode* root, const btVector3* normals, const btScalar* offsets, int count, const btDbvt::ICollide* policy)
{
	btDbvt::collideKDOP(root, normals, offsets, count, *policy);
}

void btDbvt_collideOCL(const btDbvtNode* root, const btVector3* normals, const btScalar* offsets, const btScalar* sortaxis, int count, const btDbvt::ICollide* policy)
{
	VECTOR3_CONV(sortaxis);
	btDbvt::collideOCL(root, normals, offsets, VECTOR3_USE(sortaxis), count, *policy);
}

void btDbvt_collideOCL2(const btDbvtNode* root, const btVector3* normals, const btScalar* offsets, const btScalar* sortaxis, int count, const btDbvt::ICollide* policy, bool fullsort)
{
	VECTOR3_CONV(sortaxis);
	btDbvt::collideOCL(root, normals, offsets, VECTOR3_USE(sortaxis), count, *policy, fullsort);
}

void btDbvt_collideTT(btDbvt* obj, const btDbvtNode* root0, const btDbvtNode* root1, const btDbvt::ICollide* policy)
{
	obj->collideTT(root0, root1, *policy);
}

void btDbvt_collideTTpersistentStack(btDbvt* obj, const btDbvtNode* root0, const btDbvtNode* root1, const btDbvt::ICollide* policy)
{
	obj->collideTTpersistentStack(root0, root1, *policy);
}

void btDbvt_collideTU(const btDbvtNode* root, const btDbvt::ICollide* policy)
{
	btDbvt::collideTU(root, *policy);
}

void btDbvt_collideTV(btDbvt* obj, const btDbvtNode* root, const btDbvtVolume* volume, const btDbvt::ICollide* policy)
{
	obj->collideTV(root, *volume, *policy);
}
*/
int btDbvt_countLeaves(const btDbvtNode* node)
{
	return btDbvt::countLeaves(node);
}

bool btDbvt_empty(btDbvt* obj)
{
	return obj->empty();
}
/*
void btDbvt_enumLeaves(const btDbvtNode* root, const btDbvt::ICollide* policy)
{
	btDbvt::enumLeaves(root, *policy);
}

void btDbvt_enumNodes(const btDbvtNode* root, const btDbvt::ICollide* policy)
{
	btDbvt::enumNodes(root, *policy);
}
*/
void btDbvt_extractLeaves(const btDbvtNode* node, btAlignedDbvtNodeArray* leaves)
{
	btDbvt::extractLeaves(node, *leaves);
}

btDbvtNode* btDbvt_getFree(btDbvt* obj)
{
	return obj->m_free;
}

int btDbvt_getLeaves(btDbvt* obj)
{
	return obj->m_leaves;
}

int btDbvt_getLkhd(btDbvt* obj)
{
	return obj->m_lkhd;
}

unsigned int btDbvt_getOpath(btDbvt* obj)
{
	return obj->m_opath;
}

btAlignedDbvtNodeArray* btDbvt_getRayTestStack(btDbvt* obj)
{
	return &obj->m_rayTestStack;
}

btDbvtNode* btDbvt_getRoot(btDbvt* obj)
{
	return obj->m_root;
}

btAlignedStkNNArray* btDbvt_getStkStack(btDbvt* obj)
{
	return &obj->m_stkStack;
}

btDbvtNode* btDbvt_insert(btDbvt* obj, const btDbvtVolume* box, void* data)
{
	return obj->insert(*box, data);
}

int btDbvt_maxdepth(const btDbvtNode* node)
{
	return btDbvt::maxdepth(node);
}

int btDbvt_nearest(const int* i, const btDbvt::sStkNPS* a, btScalar v, int l, int h)
{
	return btDbvt::nearest(i, a, v, l, h);
}

void btDbvt_optimizeBottomUp(btDbvt* obj)
{
	obj->optimizeBottomUp();
}

void btDbvt_optimizeIncremental(btDbvt* obj, int passes)
{
	obj->optimizeIncremental(passes);
}

void btDbvt_optimizeTopDown(btDbvt* obj)
{
	obj->optimizeTopDown();
}

void btDbvt_optimizeTopDown2(btDbvt* obj, int bu_treshold)
{
	obj->optimizeTopDown(bu_treshold);
}
/*
void btDbvt_rayTest(const btDbvtNode* root, const btScalar* rayFrom, const btScalar* rayTo, const btDbvt::ICollide* policy)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	btDbvt::rayTest(root, VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), *policy);
}

void btDbvt_rayTestInternal(btDbvt* obj, const btDbvtNode* root, const btScalar* rayFrom, const btScalar* rayTo, const btScalar* rayDirectionInverse, unsigned int* signs, btScalar lambda_max, const btScalar* aabbMin, const btScalar* aabbMax, const btDbvt::ICollide* policy)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	VECTOR3_CONV(rayDirectionInverse);
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->rayTestInternal(root, VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), VECTOR3_USE(rayDirectionInverse), signs, lambda_max, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax), *policy);
}
*/
void btDbvt_remove(btDbvt* obj, btDbvtNode* leaf)
{
	obj->remove(leaf);
}

void btDbvt_setFree(btDbvt* obj, btDbvtNode* value)
{
	obj->m_free = value;
}

void btDbvt_setLeaves(btDbvt* obj, int value)
{
	obj->m_leaves = value;
}

void btDbvt_setLkhd(btDbvt* obj, int value)
{
	obj->m_lkhd = value;
}

void btDbvt_setOpath(btDbvt* obj, unsigned int value)
{
	obj->m_opath = value;
}

void btDbvt_setRoot(btDbvt* obj, btDbvtNode* value)
{
	obj->m_root = value;
}

void btDbvt_update(btDbvt* obj, btDbvtNode* leaf, btDbvtVolume* volume)
{
	obj->update(leaf, *volume);
}

void btDbvt_update2(btDbvt* obj, btDbvtNode* leaf)
{
	obj->update(leaf);
}

void btDbvt_update3(btDbvt* obj, btDbvtNode* leaf, int lookahead)
{
	obj->update(leaf, lookahead);
}

bool btDbvt_update4(btDbvt* obj, btDbvtNode* leaf, btDbvtVolume* volume, btScalar margin)
{
	return obj->update(leaf, *volume, margin);
}

bool btDbvt_update5(btDbvt* obj, btDbvtNode* leaf, btDbvtVolume* volume, const btScalar* velocity)
{
	VECTOR3_CONV(velocity);
	return obj->update(leaf, *volume, VECTOR3_USE(velocity));
}

bool btDbvt_update6(btDbvt* obj, btDbvtNode* leaf, btDbvtVolume* volume, const btScalar* velocity, btScalar margin)
{
	VECTOR3_CONV(velocity);
	return obj->update(leaf, *volume, VECTOR3_USE(velocity), margin);
}

void btDbvt_write(btDbvt* obj, btDbvt::IWriter* iwriter)
{
	obj->write(iwriter);
}

void btDbvt_delete(btDbvt* obj)
{
	delete obj;
}
