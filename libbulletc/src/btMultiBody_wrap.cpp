#include <BulletDynamics/Featherstone/btMultiBody.h>
#include <BulletDynamics/Featherstone/btMultiBodyLinkCollider.h>
#include <LinearMath/btSerializer.h>

#include "conversion.h"
#include "btMultiBody_wrap.h"

btMultiBody* btMultiBody_new(int n_links, btScalar mass, const btScalar* inertia, bool fixedBase, bool canSleep)
{
	VECTOR3_CONV(inertia);
	return new btMultiBody(n_links, mass, VECTOR3_USE(inertia), fixedBase, canSleep);
}

void btMultiBody_addBaseConstraintForce(btMultiBody* obj, const btScalar* f)
{
	VECTOR3_CONV(f);
	obj->addBaseConstraintForce(VECTOR3_USE(f));
}

void btMultiBody_addBaseConstraintTorque(btMultiBody* obj, const btScalar* t)
{
	VECTOR3_CONV(t);
	obj->addBaseConstraintTorque(VECTOR3_USE(t));
}

void btMultiBody_addBaseForce(btMultiBody* obj, const btScalar* f)
{
	VECTOR3_CONV(f);
	obj->addBaseForce(VECTOR3_USE(f));
}

void btMultiBody_addBaseTorque(btMultiBody* obj, const btScalar* t)
{
	VECTOR3_CONV(t);
	obj->addBaseTorque(VECTOR3_USE(t));
}

void btMultiBody_addJointTorque(btMultiBody* obj, int i, btScalar Q)
{
	obj->addJointTorque(i, Q);
}

void btMultiBody_addJointTorqueMultiDof(btMultiBody* obj, int i, const btScalar* Q)
{
	obj->addJointTorqueMultiDof(i, Q);
}

void btMultiBody_addJointTorqueMultiDof2(btMultiBody* obj, int i, int dof, btScalar Q)
{
	obj->addJointTorqueMultiDof(i, dof, Q);
}

void btMultiBody_addLinkConstraintForce(btMultiBody* obj, int i, const btScalar* f)
{
	VECTOR3_CONV(f);
	obj->addLinkConstraintForce(i, VECTOR3_USE(f));
}

void btMultiBody_addLinkConstraintTorque(btMultiBody* obj, int i, const btScalar* t)
{
	VECTOR3_CONV(t);
	obj->addLinkConstraintTorque(i, VECTOR3_USE(t));
}

void btMultiBody_addLinkForce(btMultiBody* obj, int i, const btScalar* f)
{
	VECTOR3_CONV(f);
	obj->addLinkForce(i, VECTOR3_USE(f));
}

void btMultiBody_addLinkTorque(btMultiBody* obj, int i, const btScalar* t)
{
	VECTOR3_CONV(t);
	obj->addLinkTorque(i, VECTOR3_USE(t));
}

void btMultiBody_applyDeltaVeeMultiDof(btMultiBody* obj, const btScalar* delta_vee, btScalar multiplier)
{
	obj->applyDeltaVeeMultiDof(delta_vee, multiplier);
}

void btMultiBody_applyDeltaVeeMultiDof2(btMultiBody* obj, const btScalar* delta_vee, btScalar multiplier)
{
	obj->applyDeltaVeeMultiDof2(delta_vee, multiplier);
}

void btMultiBody_calcAccelerationDeltasMultiDof(btMultiBody* obj, const btScalar* force, btScalar* output, btAlignedScalarArray* scratch_r, btAlignedVector3Array* scratch_v)
{
	obj->calcAccelerationDeltasMultiDof(force, output, *scratch_r, *scratch_v);
}

int btMultiBody_calculateSerializeBufferSize(btMultiBody* obj)
{
	return obj->calculateSerializeBufferSize();
}

void btMultiBody_checkMotionAndSleepIfRequired(btMultiBody* obj, btScalar timestep)
{
	obj->checkMotionAndSleepIfRequired(timestep);
}

void btMultiBody_clearConstraintForces(btMultiBody* obj)
{
	obj->clearConstraintForces();
}

void btMultiBody_clearForcesAndTorques(btMultiBody* obj)
{
	obj->clearForcesAndTorques();
}

void btMultiBody_clearVelocities(btMultiBody* obj)
{
	obj->clearVelocities();
}

void btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof(btMultiBody* obj, btScalar dt, btAlignedScalarArray* scratch_r, btAlignedVector3Array* scratch_v, btAlignedMatrix3x3Array* scratch_m)
{
	obj->computeAccelerationsArticulatedBodyAlgorithmMultiDof(dt, *scratch_r, *scratch_v, *scratch_m);
}

void btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof2(btMultiBody* obj, btScalar dt, btAlignedScalarArray* scratch_r, btAlignedVector3Array* scratch_v, btAlignedMatrix3x3Array* scratch_m, bool isConstraintPass)
{
	obj->computeAccelerationsArticulatedBodyAlgorithmMultiDof(dt, *scratch_r, *scratch_v, *scratch_m, isConstraintPass);
}

void btMultiBody_fillConstraintJacobianMultiDof(btMultiBody* obj, int link, const btScalar* contact_point, const btScalar* normal_ang, const btScalar* normal_lin, btScalar* jac, btAlignedScalarArray* scratch_r, btAlignedVector3Array* scratch_v, btAlignedMatrix3x3Array* scratch_m)
{
	VECTOR3_CONV(contact_point);
	VECTOR3_CONV(normal_ang);
	VECTOR3_CONV(normal_lin);
	obj->fillConstraintJacobianMultiDof(link, VECTOR3_USE(contact_point), VECTOR3_USE(normal_ang), VECTOR3_USE(normal_lin), jac, *scratch_r, *scratch_v, *scratch_m);
}

void btMultiBody_fillContactJacobianMultiDof(btMultiBody* obj, int link, const btScalar* contact_point, const btScalar* normal, btScalar* jac, btAlignedScalarArray* scratch_r, btAlignedVector3Array* scratch_v, btAlignedMatrix3x3Array* scratch_m)
{
	VECTOR3_CONV(contact_point);
	VECTOR3_CONV(normal);
	obj->fillContactJacobianMultiDof(link, VECTOR3_USE(contact_point), VECTOR3_USE(normal), jac, *scratch_r, *scratch_v, *scratch_m);
}

void btMultiBody_finalizeMultiDof(btMultiBody* obj)
{
	obj->finalizeMultiDof();
}

void btMultiBody_forwardKinematics(btMultiBody* obj, btAlignedQuaternionArray* scratch_q, btAlignedVector3Array* scratch_m)
{
	obj->forwardKinematics(*scratch_q, *scratch_m);
}

btScalar btMultiBody_getAngularDamping(btMultiBody* obj)
{
	return obj->getAngularDamping();
}

void btMultiBody_getAngularMomentum(btMultiBody* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getAngularMomentum(), value);
}

btMultiBodyLinkCollider* btMultiBody_getBaseCollider(btMultiBody* obj)
{
	return obj->getBaseCollider();
}

void btMultiBody_getBaseForce(btMultiBody* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getBaseForce(), value);
}

void btMultiBody_getBaseInertia(btMultiBody* obj, btScalar* inertia)
{
	VECTOR3_OUT(&obj->getBaseInertia(), inertia);
}

btScalar btMultiBody_getBaseMass(btMultiBody* obj)
{
	return obj->getBaseMass();
}

const char* btMultiBody_getBaseName(btMultiBody* obj)
{
	return obj->getBaseName();
}

void btMultiBody_getBaseOmega(btMultiBody* obj, btScalar* omega)
{
	VECTOR3_OUT_VAL(obj->getBaseOmega(), omega);
}

void btMultiBody_getBasePos(btMultiBody* obj, btScalar* pos)
{
	VECTOR3_OUT(&obj->getBasePos(), pos);
}

void btMultiBody_getBaseTorque(btMultiBody* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getBaseTorque(), value);
}

void btMultiBody_getBaseVel(btMultiBody* obj, btScalar* vel)
{
	VECTOR3_OUT_VAL(obj->getBaseVel(), vel);
}

void btMultiBody_getBaseWorldTransform(btMultiBody* obj, btScalar* tr)
{
	TRANSFORM_OUT_VAL(obj->getBaseWorldTransform(), tr);
}

bool btMultiBody_getCanSleep(btMultiBody* obj)
{
	return obj->getCanSleep();
}

int btMultiBody_getCompanionId(btMultiBody* obj)
{
	return obj->getCompanionId();
}

btScalar btMultiBody_getJointPos(btMultiBody* obj, int i)
{
	return obj->getJointPos(i);
}

btScalar* btMultiBody_getJointPosMultiDof(btMultiBody* obj, int i)
{
	return obj->getJointPosMultiDof(i);
}

btScalar btMultiBody_getJointTorque(btMultiBody* obj, int i)
{
	return obj->getJointTorque(i);
}

btScalar* btMultiBody_getJointTorqueMultiDof(btMultiBody* obj, int i)
{
	return obj->getJointTorqueMultiDof(i);
}

btScalar btMultiBody_getJointVel(btMultiBody* obj, int i)
{
	return obj->getJointVel(i);
}

btScalar* btMultiBody_getJointVelMultiDof(btMultiBody* obj, int i)
{
	return obj->getJointVelMultiDof(i);
}

btScalar btMultiBody_getKineticEnergy(btMultiBody* obj)
{
	return obj->getKineticEnergy();
}

btScalar btMultiBody_getLinearDamping(btMultiBody* obj)
{
	return obj->getLinearDamping();
}

btMultibodyLink* btMultiBody_getLink(btMultiBody* obj, int index)
{
	return &obj->getLink(index);
}

void btMultiBody_getLinkForce(btMultiBody* obj, int i, btScalar* value)
{
	VECTOR3_OUT(&obj->getLinkForce(i), value);
}

void btMultiBody_getLinkInertia(btMultiBody* obj, int i, btScalar* value)
{
	VECTOR3_OUT(&obj->getLinkInertia(i), value);
}

btScalar btMultiBody_getLinkMass(btMultiBody* obj, int i)
{
	return obj->getLinkMass(i);
}

void btMultiBody_getLinkTorque(btMultiBody* obj, int i, btScalar* value)
{
	VECTOR3_OUT(&obj->getLinkTorque(i), value);
}

btScalar btMultiBody_getMaxAppliedImpulse(btMultiBody* obj)
{
	return obj->getMaxAppliedImpulse();
}

btScalar btMultiBody_getMaxCoordinateVelocity(btMultiBody* obj)
{
	return obj->getMaxCoordinateVelocity();
}

int btMultiBody_getNumDofs(btMultiBody* obj)
{
	return obj->getNumDofs();
}

int btMultiBody_getNumLinks(btMultiBody* obj)
{
	return obj->getNumLinks();
}

int btMultiBody_getNumPosVars(btMultiBody* obj)
{
	return obj->getNumPosVars();
}

int btMultiBody_getParent(btMultiBody* obj, int link_num)
{
	return obj->getParent(link_num);
}

void btMultiBody_getParentToLocalRot(btMultiBody* obj, int i, btScalar* value)
{
	QUATERNION_OUT(&obj->getParentToLocalRot(i), value);
}

void btMultiBody_getRVector(btMultiBody* obj, int i, btScalar* value)
{
	VECTOR3_OUT(&obj->getRVector(i), value);
}

bool btMultiBody_getUseGyroTerm(btMultiBody* obj)
{
	return obj->getUseGyroTerm();
}

const btScalar* btMultiBody_getVelocityVector(btMultiBody* obj)
{
	return obj->getVelocityVector();
}

void btMultiBody_getWorldToBaseRot(btMultiBody* obj, btScalar* rot)
{
	QUATERNION_OUT(&obj->getWorldToBaseRot(), rot);
}

void btMultiBody_goToSleep(btMultiBody* obj)
{
	obj->goToSleep();
}

bool btMultiBody_hasFixedBase(btMultiBody* obj)
{
	return obj->hasFixedBase();
}

bool btMultiBody_hasSelfCollision(btMultiBody* obj)
{
	return obj->hasSelfCollision();
}

bool btMultiBody_internalNeedsJointFeedback(btMultiBody* obj)
{
	return obj->internalNeedsJointFeedback();
}

bool btMultiBody_isAwake(btMultiBody* obj)
{
	return obj->isAwake();
}

bool btMultiBody_isPosUpdated(btMultiBody* obj)
{
	return obj->isPosUpdated();
}

bool btMultiBody_isUsingGlobalVelocities(btMultiBody* obj)
{
	return obj->isUsingGlobalVelocities();
}

bool btMultiBody_isUsingRK4Integration(btMultiBody* obj)
{
	return obj->isUsingRK4Integration();
}

void btMultiBody_localDirToWorld(btMultiBody* obj, int i, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localDirToWorld(i, VECTOR3_USE(vec)), value);
}

void btMultiBody_localPosToWorld(btMultiBody* obj, int i, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localPosToWorld(i, VECTOR3_USE(vec)), value);
}

void btMultiBody_processDeltaVeeMultiDof2(btMultiBody* obj)
{
	obj->processDeltaVeeMultiDof2();
}

const char* btMultiBody_serialize(btMultiBody* obj, void* dataBuffer, btSerializer* serializer)
{
	return obj->serialize(dataBuffer, serializer);
}

void btMultiBody_setAngularDamping(btMultiBody* obj, btScalar damp)
{
	obj->setAngularDamping(damp);
}

void btMultiBody_setBaseCollider(btMultiBody* obj, btMultiBodyLinkCollider* collider)
{
	obj->setBaseCollider(collider);
}

void btMultiBody_setBaseInertia(btMultiBody* obj, const btScalar* inertia)
{
	VECTOR3_CONV(inertia);
	obj->setBaseInertia(VECTOR3_USE(inertia));
}

void btMultiBody_setBaseMass(btMultiBody* obj, btScalar mass)
{
	obj->setBaseMass(mass);
}

void btMultiBody_setBaseName(btMultiBody* obj, const char* name)
{
	obj->setBaseName(name);
}

void btMultiBody_setBaseOmega(btMultiBody* obj, const btScalar* omega)
{
	VECTOR3_CONV(omega);
	obj->setBaseOmega(VECTOR3_USE(omega));
}

void btMultiBody_setBasePos(btMultiBody* obj, const btScalar* pos)
{
	VECTOR3_CONV(pos);
	obj->setBasePos(VECTOR3_USE(pos));
}

void btMultiBody_setBaseVel(btMultiBody* obj, const btScalar* vel)
{
	VECTOR3_CONV(vel);
	obj->setBaseVel(VECTOR3_USE(vel));
}

void btMultiBody_setBaseWorldTransform(btMultiBody* obj, const btScalar* tr)
{
	TRANSFORM_CONV(tr);
	obj->setBaseWorldTransform(TRANSFORM_USE(tr));
}

void btMultiBody_setCanSleep(btMultiBody* obj, bool canSleep)
{
	obj->setCanSleep(canSleep);
}

void btMultiBody_setCompanionId(btMultiBody* obj, int id)
{
	obj->setCompanionId(id);
}

void btMultiBody_setHasSelfCollision(btMultiBody* obj, bool hasSelfCollision)
{
	obj->setHasSelfCollision(hasSelfCollision);
}

void btMultiBody_setJointPos(btMultiBody* obj, int i, btScalar q)
{
	obj->setJointPos(i, q);
}

void btMultiBody_setJointPosMultiDof(btMultiBody* obj, int i, btScalar* q)
{
	obj->setJointPosMultiDof(i, q);
}

void btMultiBody_setJointVel(btMultiBody* obj, int i, btScalar qdot)
{
	obj->setJointVel(i, qdot);
}

void btMultiBody_setJointVelMultiDof(btMultiBody* obj, int i, btScalar* qdot)
{
	obj->setJointVelMultiDof(i, qdot);
}

void btMultiBody_setLinearDamping(btMultiBody* obj, btScalar damp)
{
	obj->setLinearDamping(damp);
}

void btMultiBody_setMaxAppliedImpulse(btMultiBody* obj, btScalar maxImp)
{
	obj->setMaxAppliedImpulse(maxImp);
}

void btMultiBody_setMaxCoordinateVelocity(btMultiBody* obj, btScalar maxVel)
{
	obj->setMaxCoordinateVelocity(maxVel);
}

void btMultiBody_setNumLinks(btMultiBody* obj, int numLinks)
{
	obj->setNumLinks(numLinks);
}

void btMultiBody_setPosUpdated(btMultiBody* obj, bool updated)
{
	obj->setPosUpdated(updated);
}

void btMultiBody_setupFixed(btMultiBody* obj, int linkIndex, btScalar mass, const btScalar* inertia, int parent, const btScalar* rotParentToThis, const btScalar* parentComToThisPivotOffset, const btScalar* thisPivotToThisComOffset)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rotParentToThis);
	VECTOR3_CONV(parentComToThisPivotOffset);
	VECTOR3_CONV(thisPivotToThisComOffset);
	obj->setupFixed(linkIndex, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(rotParentToThis), VECTOR3_USE(parentComToThisPivotOffset), VECTOR3_USE(thisPivotToThisComOffset));
}

void btMultiBody_setupPlanar(btMultiBody* obj, int i, btScalar mass, const btScalar* inertia, int parent, const btScalar* rotParentToThis, const btScalar* rotationAxis, const btScalar* parentComToThisComOffset)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rotParentToThis);
	VECTOR3_CONV(rotationAxis);
	VECTOR3_CONV(parentComToThisComOffset);
	obj->setupPlanar(i, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(rotParentToThis), VECTOR3_USE(rotationAxis), VECTOR3_USE(parentComToThisComOffset));
}

void btMultiBody_setupPlanar2(btMultiBody* obj, int i, btScalar mass, const btScalar* inertia, int parent, const btScalar* rotParentToThis, const btScalar* rotationAxis, const btScalar* parentComToThisComOffset, bool disableParentCollision)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rotParentToThis);
	VECTOR3_CONV(rotationAxis);
	VECTOR3_CONV(parentComToThisComOffset);
	obj->setupPlanar(i, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(rotParentToThis), VECTOR3_USE(rotationAxis), VECTOR3_USE(parentComToThisComOffset), disableParentCollision);
}

void btMultiBody_setupPrismatic(btMultiBody* obj, int i, btScalar mass, const btScalar* inertia, int parent, const btScalar* rotParentToThis, const btScalar* jointAxis, const btScalar* parentComToThisPivotOffset, const btScalar* thisPivotToThisComOffset, bool disableParentCollision)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rotParentToThis);
	VECTOR3_CONV(jointAxis);
	VECTOR3_CONV(parentComToThisPivotOffset);
	VECTOR3_CONV(thisPivotToThisComOffset);
	obj->setupPrismatic(i, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(rotParentToThis), VECTOR3_USE(jointAxis), VECTOR3_USE(parentComToThisPivotOffset), VECTOR3_USE(thisPivotToThisComOffset), disableParentCollision);
}

void btMultiBody_setupRevolute(btMultiBody* obj, int linkIndex, btScalar mass, const btScalar* inertia, int parentIndex, const btScalar* rotParentToThis, const btScalar* jointAxis, const btScalar* parentComToThisPivotOffset, const btScalar* thisPivotToThisComOffset)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rotParentToThis);
	VECTOR3_CONV(jointAxis);
	VECTOR3_CONV(parentComToThisPivotOffset);
	VECTOR3_CONV(thisPivotToThisComOffset);
	obj->setupRevolute(linkIndex, mass, VECTOR3_USE(inertia), parentIndex, QUATERNION_USE(rotParentToThis), VECTOR3_USE(jointAxis), VECTOR3_USE(parentComToThisPivotOffset), VECTOR3_USE(thisPivotToThisComOffset));
}

void btMultiBody_setupRevolute2(btMultiBody* obj, int linkIndex, btScalar mass, const btScalar* inertia, int parentIndex, const btScalar* rotParentToThis, const btScalar* jointAxis, const btScalar* parentComToThisPivotOffset, const btScalar* thisPivotToThisComOffset, bool disableParentCollision)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rotParentToThis);
	VECTOR3_CONV(jointAxis);
	VECTOR3_CONV(parentComToThisPivotOffset);
	VECTOR3_CONV(thisPivotToThisComOffset);
	obj->setupRevolute(linkIndex, mass, VECTOR3_USE(inertia), parentIndex, QUATERNION_USE(rotParentToThis), VECTOR3_USE(jointAxis), VECTOR3_USE(parentComToThisPivotOffset), VECTOR3_USE(thisPivotToThisComOffset), disableParentCollision);
}

void btMultiBody_setupSpherical(btMultiBody* obj, int linkIndex, btScalar mass, const btScalar* inertia, int parent, const btScalar* rotParentToThis, const btScalar* parentComToThisPivotOffset, const btScalar* thisPivotToThisComOffset)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rotParentToThis);
	VECTOR3_CONV(parentComToThisPivotOffset);
	VECTOR3_CONV(thisPivotToThisComOffset);
	obj->setupSpherical(linkIndex, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(rotParentToThis), VECTOR3_USE(parentComToThisPivotOffset), VECTOR3_USE(thisPivotToThisComOffset));
}

void btMultiBody_setupSpherical2(btMultiBody* obj, int linkIndex, btScalar mass, const btScalar* inertia, int parent, const btScalar* rotParentToThis, const btScalar* parentComToThisPivotOffset, const btScalar* thisPivotToThisComOffset, bool disableParentCollision)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rotParentToThis);
	VECTOR3_CONV(parentComToThisPivotOffset);
	VECTOR3_CONV(thisPivotToThisComOffset);
	obj->setupSpherical(linkIndex, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(rotParentToThis), VECTOR3_USE(parentComToThisPivotOffset), VECTOR3_USE(thisPivotToThisComOffset), disableParentCollision);
}

void btMultiBody_setUseGyroTerm(btMultiBody* obj, bool useGyro)
{
	obj->setUseGyroTerm(useGyro);
}

void btMultiBody_setWorldToBaseRot(btMultiBody* obj, const btScalar* rot)
{
	QUATERNION_CONV(rot);
	obj->setWorldToBaseRot(QUATERNION_USE(rot));
}

void btMultiBody_stepPositionsMultiDof(btMultiBody* obj, btScalar dt)
{
	obj->stepPositionsMultiDof(dt);
}

void btMultiBody_stepPositionsMultiDof2(btMultiBody* obj, btScalar dt, btScalar* pq)
{
	obj->stepPositionsMultiDof(dt, pq);
}

void btMultiBody_stepPositionsMultiDof3(btMultiBody* obj, btScalar dt, btScalar* pq, btScalar* pqd)
{
	obj->stepPositionsMultiDof(dt, pq, pqd);
}

void btMultiBody_updateCollisionObjectWorldTransforms(btMultiBody* obj, btAlignedQuaternionArray* scratch_q, btAlignedVector3Array* scratch_m)
{
	obj->updateCollisionObjectWorldTransforms(*scratch_q, *scratch_m);
}

void btMultiBody_useGlobalVelocities(btMultiBody* obj, bool use)
{
	obj->useGlobalVelocities(use);
}

void btMultiBody_useRK4Integration(btMultiBody* obj, bool use)
{
	obj->useRK4Integration(use);
}

void btMultiBody_wakeUp(btMultiBody* obj)
{
	obj->wakeUp();
}

void btMultiBody_worldDirToLocal(btMultiBody* obj, int i, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->worldDirToLocal(i, VECTOR3_USE(vec)), value);
}

void btMultiBody_worldPosToLocal(btMultiBody* obj, int i, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->worldPosToLocal(i, VECTOR3_USE(vec)), value);
}

void btMultiBody_delete(btMultiBody* obj)
{
	delete obj;
}
