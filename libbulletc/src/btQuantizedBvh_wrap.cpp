#include <BulletCollision/BroadphaseCollision/btQuantizedBvh.h>
#include <LinearMath/btSerializer.h>

#include "conversion.h"
#include "btQuantizedBvh_wrap.h"

btQuantizedBvhNode* btQuantizedBvhNode_new()
{
	return new btQuantizedBvhNode();
}

int btQuantizedBvhNode_getEscapeIndex(btQuantizedBvhNode* obj)
{
	return obj->getEscapeIndex();
}

int btQuantizedBvhNode_getEscapeIndexOrTriangleIndex(btQuantizedBvhNode* obj)
{
	return obj->m_escapeIndexOrTriangleIndex;
}

int btQuantizedBvhNode_getPartId(btQuantizedBvhNode* obj)
{
	return obj->getPartId();
}

unsigned short* btQuantizedBvhNode_getQuantizedAabbMax(btQuantizedBvhNode* obj)
{
	return obj->m_quantizedAabbMax;
}

unsigned short* btQuantizedBvhNode_getQuantizedAabbMin(btQuantizedBvhNode* obj)
{
	return obj->m_quantizedAabbMin;
}

int btQuantizedBvhNode_getTriangleIndex(btQuantizedBvhNode* obj)
{
	return obj->getTriangleIndex();
}

bool btQuantizedBvhNode_isLeafNode(btQuantizedBvhNode* obj)
{
	return obj->isLeafNode();
}

void btQuantizedBvhNode_setEscapeIndexOrTriangleIndex(btQuantizedBvhNode* obj, int value)
{
	obj->m_escapeIndexOrTriangleIndex = value;
}

void btQuantizedBvhNode_delete(btQuantizedBvhNode* obj)
{
	delete obj;
}


btOptimizedBvhNode* btOptimizedBvhNode_new()
{
	return new btOptimizedBvhNode();
}

void btOptimizedBvhNode_getAabbMaxOrg(btOptimizedBvhNode* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_aabbMaxOrg, value);
}

void btOptimizedBvhNode_getAabbMinOrg(btOptimizedBvhNode* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_aabbMinOrg, value);
}

int btOptimizedBvhNode_getEscapeIndex(btOptimizedBvhNode* obj)
{
	return obj->m_escapeIndex;
}

int btOptimizedBvhNode_getSubPart(btOptimizedBvhNode* obj)
{
	return obj->m_subPart;
}

int btOptimizedBvhNode_getTriangleIndex(btOptimizedBvhNode* obj)
{
	return obj->m_triangleIndex;
}

void btOptimizedBvhNode_setAabbMaxOrg(btOptimizedBvhNode* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_aabbMaxOrg);
}

void btOptimizedBvhNode_setAabbMinOrg(btOptimizedBvhNode* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_aabbMinOrg);
}

void btOptimizedBvhNode_setEscapeIndex(btOptimizedBvhNode* obj, int value)
{
	obj->m_escapeIndex = value;
}

void btOptimizedBvhNode_setSubPart(btOptimizedBvhNode* obj, int value)
{
	obj->m_subPart = value;
}

void btOptimizedBvhNode_setTriangleIndex(btOptimizedBvhNode* obj, int value)
{
	obj->m_triangleIndex = value;
}

void btOptimizedBvhNode_delete(btOptimizedBvhNode* obj)
{
	delete obj;
}


void btNodeOverlapCallback_processNode(btNodeOverlapCallback* obj, int subPart, int triangleIndex)
{
	obj->processNode(subPart, triangleIndex);
}

void btNodeOverlapCallback_delete(btNodeOverlapCallback* obj)
{
	delete obj;
}


btQuantizedBvh* btQuantizedBvh_new()
{
	return new btQuantizedBvh();
}

void btQuantizedBvh_buildInternal(btQuantizedBvh* obj)
{
	obj->buildInternal();
}

unsigned int btQuantizedBvh_calculateSerializeBufferSize(btQuantizedBvh* obj)
{
	return obj->calculateSerializeBufferSize();
}

int btQuantizedBvh_calculateSerializeBufferSizeNew(btQuantizedBvh* obj)
{
	return obj->calculateSerializeBufferSizeNew();
}

void btQuantizedBvh_deSerializeDouble(btQuantizedBvh* obj, btQuantizedBvhDoubleData* quantizedBvhDoubleData)
{
	obj->deSerializeDouble(*quantizedBvhDoubleData);
}

void btQuantizedBvh_deSerializeFloat(btQuantizedBvh* obj, btQuantizedBvhFloatData* quantizedBvhFloatData)
{
	obj->deSerializeFloat(*quantizedBvhFloatData);
}

btQuantizedBvh* btQuantizedBvh_deSerializeInPlace(void* i_alignedDataBuffer, unsigned int i_dataBufferSize, bool i_swapEndian)
{
	return btQuantizedBvh::deSerializeInPlace(i_alignedDataBuffer, i_dataBufferSize, i_swapEndian);
}

unsigned int btQuantizedBvh_getAlignmentSerializationPadding()
{
	return btQuantizedBvh::getAlignmentSerializationPadding();
}

QuantizedNodeArray* btQuantizedBvh_getLeafNodeArray(btQuantizedBvh* obj)
{
	return &obj->getLeafNodeArray();
}

QuantizedNodeArray* btQuantizedBvh_getQuantizedNodeArray(btQuantizedBvh* obj)
{
	return &obj->getQuantizedNodeArray();
}

BvhSubtreeInfoArray* btQuantizedBvh_getSubtreeInfoArray(btQuantizedBvh* obj)
{
	return &obj->getSubtreeInfoArray();
}

bool btQuantizedBvh_isQuantized(btQuantizedBvh* obj)
{
	return obj->isQuantized();
}

void btQuantizedBvh_quantize(btQuantizedBvh* obj, unsigned short* out, const btScalar* point, int isMax)
{
	VECTOR3_CONV(point);
	obj->quantize(out, VECTOR3_USE(point), isMax);
}

void btQuantizedBvh_quantizeWithClamp(btQuantizedBvh* obj, unsigned short* out, const btScalar* point2, int isMax)
{
	VECTOR3_CONV(point2);
	obj->quantizeWithClamp(out, VECTOR3_USE(point2), isMax);
}

void btQuantizedBvh_reportAabbOverlappingNodex(btQuantizedBvh* obj, btNodeOverlapCallback* nodeCallback, const btScalar* aabbMin, const btScalar* aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->reportAabbOverlappingNodex(nodeCallback, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}

void btQuantizedBvh_reportBoxCastOverlappingNodex(btQuantizedBvh* obj, btNodeOverlapCallback* nodeCallback, const btScalar* raySource, const btScalar* rayTarget, const btScalar* aabbMin, const btScalar* aabbMax)
{
	VECTOR3_CONV(raySource);
	VECTOR3_CONV(rayTarget);
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->reportBoxCastOverlappingNodex(nodeCallback, VECTOR3_USE(raySource), VECTOR3_USE(rayTarget), VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}

void btQuantizedBvh_reportRayOverlappingNodex(btQuantizedBvh* obj, btNodeOverlapCallback* nodeCallback, const btScalar* raySource, const btScalar* rayTarget)
{
	VECTOR3_CONV(raySource);
	VECTOR3_CONV(rayTarget);
	obj->reportRayOverlappingNodex(nodeCallback, VECTOR3_USE(raySource), VECTOR3_USE(rayTarget));
}

bool btQuantizedBvh_serialize(btQuantizedBvh* obj, void* o_alignedDataBuffer, unsigned int i_dataBufferSize, bool i_swapEndian)
{
	return obj->serialize(o_alignedDataBuffer, i_dataBufferSize, i_swapEndian);
}

const char* btQuantizedBvh_serialize2(btQuantizedBvh* obj, void* dataBuffer, btSerializer* serializer)
{
	return obj->serialize(dataBuffer, serializer);
}

void btQuantizedBvh_setQuantizationValues(btQuantizedBvh* obj, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	obj->setQuantizationValues(VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax));
}

void btQuantizedBvh_setQuantizationValues2(btQuantizedBvh* obj, const btScalar* bvhAabbMin, const btScalar* bvhAabbMax, btScalar quantizationMargin)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	obj->setQuantizationValues(VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax), quantizationMargin);
}

void btQuantizedBvh_setTraversalMode(btQuantizedBvh* obj, btQuantizedBvh::btTraversalMode traversalMode)
{
	obj->setTraversalMode(traversalMode);
}

void btQuantizedBvh_unQuantize(btQuantizedBvh* obj, const unsigned short* vecIn, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->unQuantize(vecIn), value);
}

void btQuantizedBvh_delete(btQuantizedBvh* obj)
{
	delete obj;
}
