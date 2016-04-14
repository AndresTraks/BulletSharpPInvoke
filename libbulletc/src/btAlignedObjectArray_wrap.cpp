#include <BulletCollision/BroadphaseCollision/btBroadphaseProxy.h>
#include <BulletCollision/CollisionShapes/btTriangleIndexVertexArray.h>
#include <BulletCollision/NarrowPhaseCollision/btPersistentManifold.h>
#include <BulletSoftBody/btSoftBody.h>
#include <LinearMath/btAlignedObjectArray.h>

#include "btAlignedObjectArray_wrap.h"

btBroadphasePair* btAlignedObjectArray_btBroadphasePair_at(btAlignedObjectArray_btBroadphasePair* obj, int n)
{
	return &obj->at(n);
}

void btAlignedObjectArray_btBroadphasePair_push_back(btAlignedObjectArray_btBroadphasePair* obj, btBroadphasePair* val)
{
	obj->push_back(*val);
}

void btAlignedObjectArray_btBroadphasePair_resizeNoInitialize(btAlignedObjectArray_btBroadphasePair* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btBroadphasePair_size(btAlignedObjectArray_btBroadphasePair* obj)
{
	return obj->size();
}


btCollisionObject* btAlignedObjectArray_btCollisionObjectPtr_at(btAlignedObjectArray_btCollisionObjectPtr* obj, int n)
{
	return obj->at(n);
}

void btAlignedObjectArray_btCollisionObjectPtr_push_back(btAlignedObjectArray_btCollisionObjectPtr* obj, btCollisionObject* val)
{
	obj->push_back(val);
}

void btAlignedObjectArray_btCollisionObjectPtr_resizeNoInitialize(btAlignedObjectArray_btCollisionObjectPtr* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btCollisionObjectPtr_size(btAlignedObjectArray_btCollisionObjectPtr* obj)
{
	return obj->size();
}


btSoftBody* btAlignedObjectArray_btSoftBodyPtr_at(btAlignedObjectArray_btSoftBodyPtr* obj, int n)
{
	return obj->at(n);
}

void btAlignedObjectArray_btSoftBodyPtr_push_back(btAlignedObjectArray_btSoftBodyPtr* obj, btSoftBody* val)
{
	obj->push_back(val);
}

void btAlignedObjectArray_btSoftBodyPtr_resizeNoInitialize(btAlignedObjectArray_btSoftBodyPtr* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btSoftBodyPtr_size(btAlignedObjectArray_btSoftBodyPtr* obj)
{
	return obj->size();
}


btIndexedMesh* btAlignedObjectArray_btIndexedMesh_at(btAlignedObjectArray_btIndexedMesh* obj, int n)
{
	return &obj->at(n);
}

void btAlignedObjectArray_btIndexedMesh_push_back(btAlignedObjectArray_btIndexedMesh* obj, btIndexedMesh* val)
{
	obj->push_back(*val);
}

void btAlignedObjectArray_btIndexedMesh_resizeNoInitialize(btAlignedObjectArray_btIndexedMesh* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btIndexedMesh_size(btAlignedObjectArray_btIndexedMesh* obj)
{
	return obj->size();
}


EXPORT btAlignedObjectArray_btPersistentManifold* btAlignedObjectArray_btPersistentManifold_new()
{
	return new btAlignedObjectArray_btPersistentManifold();
}

btPersistentManifold* btAlignedObjectArray_btPersistentManifold_at(btAlignedObjectArray_btPersistentManifold* obj, int n)
{
	return obj->at(n);
}

void btAlignedObjectArray_btPersistentManifold_push_back(btAlignedObjectArray_btPersistentManifold* obj, btPersistentManifold* val)
{
	obj->push_back(val);
}

void btAlignedObjectArray_btPersistentManifold_resizeNoInitialize(btAlignedObjectArray_btPersistentManifold* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btPersistentManifold_size(btAlignedObjectArray_btPersistentManifold* obj)
{
	return obj->size();
}

void btAlignedObjectArray_btPersistentManifold_delete(btAlignedObjectArray_btPersistentManifold* obj)
{
	delete obj;
}


btSoftBody::Anchor* btAlignedObjectArray_btSoftBody_Anchor_at(btAlignedObjectArray_btSoftBody_Anchor* obj, int n)
{
	return &obj->at(n);
}

void btAlignedObjectArray_btSoftBody_Anchor_push_back(btAlignedObjectArray_btSoftBody_Anchor* obj, btSoftBody::Anchor* val)
{
	obj->push_back(*val);
}

void btAlignedObjectArray_btSoftBody_Anchor_resizeNoInitialize(btAlignedObjectArray_btSoftBody_Anchor* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btSoftBody_Anchor_size(btAlignedObjectArray_btSoftBody_Anchor* obj)
{
	return obj->size();
}


btSoftBody::Cluster* btAlignedObjectArray_btSoftBody_ClusterPtr_at(btAlignedObjectArray_btSoftBody_ClusterPtr* obj, int n)
{
	return obj->at(n);
}

void btAlignedObjectArray_btSoftBody_ClusterPtr_push_back(btAlignedObjectArray_btSoftBody_ClusterPtr* obj, btSoftBody::Cluster* val)
{
	obj->push_back(val);
}

void btAlignedObjectArray_btSoftBody_ClusterPtr_resizeNoInitialize(btAlignedObjectArray_btSoftBody_ClusterPtr* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btSoftBody_ClusterPtr_size(btAlignedObjectArray_btSoftBody_ClusterPtr* obj)
{
	return obj->size();
}


btSoftBody::Face* btAlignedObjectArray_btSoftBody_Face_at(btAlignedObjectArray_btSoftBody_Face* obj, int n)
{
	return &obj->at(n);
}

void btAlignedObjectArray_btSoftBody_Face_push_back(btAlignedObjectArray_btSoftBody_Face* obj, btSoftBody::Face* val)
{
	obj->push_back(*val);
}

void btAlignedObjectArray_btSoftBody_Face_resizeNoInitialize(btAlignedObjectArray_btSoftBody_Face* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btSoftBody_Face_size(btAlignedObjectArray_btSoftBody_Face* obj)
{
	return obj->size();
}


btSoftBody::Joint* btAlignedObjectArray_btSoftBody_JointPtr_at(btSoftBody::tJointArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedObjectArray_btSoftBody_JointPtr_push_back(btAlignedObjectArray_btSoftBody_JointPtr* obj, btSoftBody::Joint* val)
{
	obj->push_back(val);
}

void btAlignedObjectArray_btSoftBody_JointPtr_resizeNoInitialize(btAlignedObjectArray_btSoftBody_JointPtr* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btSoftBody_JointPtr_size(btAlignedObjectArray_btSoftBody_JointPtr* obj)
{
	return obj->size();
}


btSoftBody::Link* btAlignedObjectArray_btSoftBody_Link_at(btAlignedObjectArray_btSoftBody_Link* obj, int n)
{
	return &obj->at(n);
}

void btAlignedObjectArray_btSoftBody_Link_push_back(btAlignedObjectArray_btSoftBody_Link* obj, btSoftBody::Link* val)
{
	obj->push_back(*val);
}

void btAlignedObjectArray_btSoftBody_Link_resizeNoInitialize(btAlignedObjectArray_btSoftBody_Link* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

void btAlignedObjectArray_btSoftBody_Link_set(btAlignedObjectArray_btSoftBody_Link* obj, btSoftBody::Link* val, int index)
{
	obj->at(index) = *val;
}

int btAlignedObjectArray_btSoftBody_Link_size(btAlignedObjectArray_btSoftBody_Link* obj)
{
	return obj->size();
}


btSoftBody::Material* btAlignedObjectArray_btSoftBody_MaterialPtr_at(btAlignedObjectArray_btSoftBody_MaterialPtr* obj, int n)
{
	return obj->at(n);
}

void btAlignedObjectArray_btSoftBody_MaterialPtr_push_back(btAlignedObjectArray_btSoftBody_MaterialPtr* obj, btSoftBody::Material* val)
{
	obj->push_back(val);
}

void btAlignedObjectArray_btSoftBody_MaterialPtr_resizeNoInitialize(btAlignedObjectArray_btSoftBody_MaterialPtr* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btSoftBody_MaterialPtr_size(btAlignedObjectArray_btSoftBody_MaterialPtr* obj)
{
	return obj->size();
}


btSoftBody::Node* btAlignedObjectArray_btSoftBody_Node_at(btAlignedObjectArray_btSoftBody_Node* obj, int n)
{
	return &obj->at(n);
}

int btAlignedObjectArray_btSoftBody_Node_index_of(btAlignedObjectArray_btSoftBody_Node* obj, btSoftBody::Node* val)
{
	if (val < &obj->at(0) || val > &obj->at(obj->size() - 1)) {
		return -1;
	}
	return static_cast<int>(val - &obj->at(0));
}

void btAlignedObjectArray_btSoftBody_Node_push_back(btAlignedObjectArray_btSoftBody_Node* obj, btSoftBody::Node* val)
{
	obj->push_back(*val);
}

void btAlignedObjectArray_btSoftBody_Node_resizeNoInitialize(btAlignedObjectArray_btSoftBody_Node* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btSoftBody_Node_size(btAlignedObjectArray_btSoftBody_Node* obj)
{
	return obj->size();
}


btSoftBody::Tetra* btAlignedObjectArray_btSoftBody_Tetra_at(btAlignedObjectArray_btSoftBody_Tetra* obj, int n)
{
	return &obj->at(n);
}

void btAlignedObjectArray_btSoftBody_Tetra_push_back(btAlignedObjectArray_btSoftBody_Tetra* obj, btSoftBody::Tetra* val)
{
	obj->push_back(*val);
}

void btAlignedObjectArray_btSoftBody_Tetra_resizeNoInitialize(btAlignedObjectArray_btSoftBody_Tetra* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedObjectArray_btSoftBody_Tetra_size(btAlignedObjectArray_btSoftBody_Tetra* obj)
{
	return obj->size();
}
