#include <BulletCollision/CollisionShapes/btBvhTriangleMeshShape.h>
#include <BulletCollision/CollisionShapes/btOptimizedBvh.h>
#include <BulletCollision/CollisionShapes/btStridingMeshInterface.h>
#include <BulletDynamics/Dynamics/btDynamicsWorld.h>
#include <BulletDynamics/Dynamics/btRigidBody.h>
#include <../Extras/Serialize/BulletWorldImporter/btWorldImporter.h>

#include "conversion.h"
#include "btWorldImporter_wrap.h"

#ifndef BULLETC_DISABLE_WORLD_IMPORTERS

btWorldImporter* btWorldImporter_new(btDynamicsWorld* world)
{
	return new btWorldImporter(world);
}

btCollisionShape* btWorldImporter_createBoxShape(btWorldImporter* obj, const btScalar* halfExtents)
{
	VECTOR3_CONV(halfExtents);
	return obj->createBoxShape(VECTOR3_USE(halfExtents));
}

btBvhTriangleMeshShape* btWorldImporter_createBvhTriangleMeshShape(btWorldImporter* obj, btStridingMeshInterface* trimesh, btOptimizedBvh* bvh)
{
	return obj->createBvhTriangleMeshShape(trimesh, bvh);
}

btCollisionShape* btWorldImporter_createCapsuleShapeZ(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createCapsuleShapeZ(radius, height);
}

btCollisionShape* btWorldImporter_createCapsuleShapeX(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createCapsuleShapeX(radius, height);
}

btCollisionShape* btWorldImporter_createCapsuleShapeY(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createCapsuleShapeY(radius, height);
}

btCollisionObject* btWorldImporter_createCollisionObject(btWorldImporter* obj, const btScalar* startTransform, btCollisionShape* shape, const char* bodyName)
{
	TRANSFORM_CONV(startTransform);
	return obj->createCollisionObject(TRANSFORM_USE(startTransform), shape, bodyName);
}

btCompoundShape* btWorldImporter_createCompoundShape(btWorldImporter* obj)
{
	return obj->createCompoundShape();
}

btCollisionShape* btWorldImporter_createConeShapeZ(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createConeShapeZ(radius, height);
}

btCollisionShape* btWorldImporter_createConeShapeX(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createConeShapeX(radius, height);
}

btCollisionShape* btWorldImporter_createConeShapeY(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createConeShapeY(radius, height);
}

btConeTwistConstraint* btWorldImporter_createConeTwistConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame)
{
	TRANSFORM_CONV(rbAFrame);
	TRANSFORM_CONV(rbBFrame);
	return obj->createConeTwistConstraint(*rbA, *rbB, TRANSFORM_USE(rbAFrame), TRANSFORM_USE(rbBFrame));
}

btConeTwistConstraint* btWorldImporter_createConeTwistConstraint2(btWorldImporter* obj, btRigidBody* rbA, const btScalar* rbAFrame)
{
	TRANSFORM_CONV(rbAFrame);
	return obj->createConeTwistConstraint(*rbA, TRANSFORM_USE(rbAFrame));
}

btConvexHullShape* btWorldImporter_createConvexHullShape(btWorldImporter* obj)
{
	return obj->createConvexHullShape();
}

btCollisionShape* btWorldImporter_createConvexTriangleMeshShape(btWorldImporter* obj, btStridingMeshInterface* trimesh)
{
	return obj->createConvexTriangleMeshShape(trimesh);
}

btCollisionShape* btWorldImporter_createCylinderShapeZ(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createCylinderShapeZ(radius, height);
}

btCollisionShape* btWorldImporter_createCylinderShapeX(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createCylinderShapeX(radius, height);
}

btCollisionShape* btWorldImporter_createCylinderShapeY(btWorldImporter* obj, btScalar radius, btScalar height)
{
	return obj->createCylinderShapeY(radius, height);
}

btGearConstraint* btWorldImporter_createGearConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* axisInA, const btScalar* axisInB, btScalar ratio)
{
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return obj->createGearConstraint(*rbA, *rbB, VECTOR3_USE(axisInA), VECTOR3_USE(axisInB), ratio);
}

btGeneric6DofConstraint* btWorldImporter_createGeneric6DofConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return obj->createGeneric6DofConstraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

btGeneric6DofConstraint* btWorldImporter_createGeneric6DofConstraint2(btWorldImporter* obj, btRigidBody* rbB, const btScalar* frameInB, bool useLinearReferenceFrameB)
{
	TRANSFORM_CONV(frameInB);
	return obj->createGeneric6DofConstraint(*rbB, TRANSFORM_USE(frameInB), useLinearReferenceFrameB);
}

btGeneric6DofSpring2Constraint* btWorldImporter_createGeneric6DofSpring2Constraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB, int rotateOrder)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return obj->createGeneric6DofSpring2Constraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), rotateOrder);
}

btGeneric6DofSpringConstraint* btWorldImporter_createGeneric6DofSpringConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return obj->createGeneric6DofSpringConstraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

btGImpactMeshShape* btWorldImporter_createGimpactShape(btWorldImporter* obj, btStridingMeshInterface* trimesh)
{
	return obj->createGimpactShape(trimesh);
}

btHingeConstraint* btWorldImporter_createHingeConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame)
{
	TRANSFORM_CONV(rbAFrame);
	TRANSFORM_CONV(rbBFrame);
	return obj->createHingeConstraint(*rbA, *rbB, TRANSFORM_USE(rbAFrame), TRANSFORM_USE(rbBFrame));
}

btHingeConstraint* btWorldImporter_createHingeConstraint2(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* rbAFrame, const btScalar* rbBFrame, bool useReferenceFrameA)
{
	TRANSFORM_CONV(rbAFrame);
	TRANSFORM_CONV(rbBFrame);
	return obj->createHingeConstraint(*rbA, *rbB, TRANSFORM_USE(rbAFrame), TRANSFORM_USE(rbBFrame), useReferenceFrameA);
}

btHingeConstraint* btWorldImporter_createHingeConstraint3(btWorldImporter* obj, btRigidBody* rbA, const btScalar* rbAFrame)
{
	TRANSFORM_CONV(rbAFrame);
	return obj->createHingeConstraint(*rbA, TRANSFORM_USE(rbAFrame));
}

btHingeConstraint* btWorldImporter_createHingeConstraint4(btWorldImporter* obj, btRigidBody* rbA, const btScalar* rbAFrame, bool useReferenceFrameA)
{
	TRANSFORM_CONV(rbAFrame);
	return obj->createHingeConstraint(*rbA, TRANSFORM_USE(rbAFrame), useReferenceFrameA);
}

btTriangleIndexVertexArray* btWorldImporter_createMeshInterface(btWorldImporter* obj, btStridingMeshInterfaceData* meshData)
{
	return obj->createMeshInterface(*meshData);
}

btMultiSphereShape* btWorldImporter_createMultiSphereShape(btWorldImporter* obj, const btScalar* positions, const btScalar* radi, int numSpheres)
{
	VECTOR3_CONV(positions);
	return obj->createMultiSphereShape(&VECTOR3_USE(positions), radi, numSpheres);
}

btOptimizedBvh* btWorldImporter_createOptimizedBvh(btWorldImporter* obj)
{
	return obj->createOptimizedBvh();
}

btCollisionShape* btWorldImporter_createPlaneShape(btWorldImporter* obj, const btScalar* planeNormal, btScalar planeConstant)
{
	VECTOR3_CONV(planeNormal);
	return obj->createPlaneShape(VECTOR3_USE(planeNormal), planeConstant);
}

btPoint2PointConstraint* btWorldImporter_createPoint2PointConstraint(btWorldImporter* obj, btRigidBody* rbA, const btScalar* pivotInA)
{
	VECTOR3_CONV(pivotInA);
	return obj->createPoint2PointConstraint(*rbA, VECTOR3_USE(pivotInA));
}

btPoint2PointConstraint* btWorldImporter_createPoint2PointConstraint2(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* pivotInA, const btScalar* pivotInB)
{
	VECTOR3_CONV(pivotInA);
	VECTOR3_CONV(pivotInB);
	return obj->createPoint2PointConstraint(*rbA, *rbB, VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB));
}

btRigidBody* btWorldImporter_createRigidBody(btWorldImporter* obj, bool isDynamic, btScalar mass, const btScalar* startTransform, btCollisionShape* shape, const char* bodyName)
{
	TRANSFORM_CONV(startTransform);
	return obj->createRigidBody(isDynamic, mass, TRANSFORM_USE(startTransform), shape, bodyName);
}

btScaledBvhTriangleMeshShape* btWorldImporter_createScaledTrangleMeshShape(btWorldImporter* obj, btBvhTriangleMeshShape* meshShape, const btScalar* localScalingbtBvhTriangleMeshShape)
{
	VECTOR3_CONV(localScalingbtBvhTriangleMeshShape);
	return obj->createScaledTrangleMeshShape(meshShape, VECTOR3_USE(localScalingbtBvhTriangleMeshShape));
}

btSliderConstraint* btWorldImporter_createSliderConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return obj->createSliderConstraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

btSliderConstraint* btWorldImporter_createSliderConstraint2(btWorldImporter* obj, btRigidBody* rbB, const btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInB);
	return obj->createSliderConstraint(*rbB, TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

btCollisionShape* btWorldImporter_createSphereShape(btWorldImporter* obj, btScalar radius)
{
	return obj->createSphereShape(radius);
}

btStridingMeshInterfaceData* btWorldImporter_createStridingMeshInterfaceData(btWorldImporter* obj, btStridingMeshInterfaceData* interfaceData)
{
	return obj->createStridingMeshInterfaceData(interfaceData);
}

btTriangleInfoMap* btWorldImporter_createTriangleInfoMap(btWorldImporter* obj)
{
	return obj->createTriangleInfoMap();
}

btTriangleIndexVertexArray* btWorldImporter_createTriangleMeshContainer(btWorldImporter* obj)
{
	return obj->createTriangleMeshContainer();
}

void btWorldImporter_deleteAllData(btWorldImporter* obj)
{
	obj->deleteAllData();
}

btOptimizedBvh* btWorldImporter_getBvhByIndex(btWorldImporter* obj, int index)
{
	return obj->getBvhByIndex(index);
}

btCollisionShape* btWorldImporter_getCollisionShapeByIndex(btWorldImporter* obj, int index)
{
	return obj->getCollisionShapeByIndex(index);
}

btCollisionShape* btWorldImporter_getCollisionShapeByName(btWorldImporter* obj, const char* name)
{
	return obj->getCollisionShapeByName(name);
}

btTypedConstraint* btWorldImporter_getConstraintByIndex(btWorldImporter* obj, int index)
{
	return obj->getConstraintByIndex(index);
}

btTypedConstraint* btWorldImporter_getConstraintByName(btWorldImporter* obj, const char* name)
{
	return obj->getConstraintByName(name);
}

const char* btWorldImporter_getNameForPointer(btWorldImporter* obj, const void* ptr)
{
	return obj->getNameForPointer(ptr);
}

int btWorldImporter_getNumBvhs(btWorldImporter* obj)
{
	return obj->getNumBvhs();
}

int btWorldImporter_getNumCollisionShapes(btWorldImporter* obj)
{
	return obj->getNumCollisionShapes();
}

int btWorldImporter_getNumConstraints(btWorldImporter* obj)
{
	return obj->getNumConstraints();
}

int btWorldImporter_getNumRigidBodies(btWorldImporter* obj)
{
	return obj->getNumRigidBodies();
}

int btWorldImporter_getNumTriangleInfoMaps(btWorldImporter* obj)
{
	return obj->getNumTriangleInfoMaps();
}

btCollisionObject* btWorldImporter_getRigidBodyByIndex(btWorldImporter* obj, int index)
{
	return obj->getRigidBodyByIndex(index);
}

btRigidBody* btWorldImporter_getRigidBodyByName(btWorldImporter* obj, const char* name)
{
	return obj->getRigidBodyByName(name);
}

btTriangleInfoMap* btWorldImporter_getTriangleInfoMapByIndex(btWorldImporter* obj, int index)
{
	return obj->getTriangleInfoMapByIndex(index);
}

int btWorldImporter_getVerboseMode(btWorldImporter* obj)
{
	return obj->getVerboseMode();
}

void btWorldImporter_setDynamicsWorldInfo(btWorldImporter* obj, const btScalar* gravity, const btContactSolverInfo* solverInfo)
{
	VECTOR3_CONV(gravity);
	obj->setDynamicsWorldInfo(VECTOR3_USE(gravity), *solverInfo);
}

void btWorldImporter_setVerboseMode(btWorldImporter* obj, int verboseMode)
{
	obj->setVerboseMode(verboseMode);
}

void btWorldImporter_delete(btWorldImporter* obj)
{
	delete obj;
}

#endif
