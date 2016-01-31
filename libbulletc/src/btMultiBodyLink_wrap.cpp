#include <BulletDynamics/Featherstone/btMultiBodyLink.h>
#include <BulletDynamics/Featherstone/btMultiBodyLinkCollider.h>

#include "conversion.h"
#include "btMultiBodyLink_wrap.h"

btSpatialMotionVector* btMultibodyLink_getAbsFrameLocVelocity(btMultibodyLink* obj)
{
	return &obj->m_absFrameLocVelocity;
}

btSpatialMotionVector* btMultibodyLink_getAbsFrameTotVelocity(btMultibodyLink* obj)
{
	return &obj->m_absFrameTotVelocity;
}

void btMultibodyLink_getAppliedConstraintForce(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_appliedConstraintForce, value);
}

void btMultibodyLink_getAppliedConstraintTorque(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_appliedConstraintTorque, value);
}

void btMultibodyLink_getAppliedForce(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_appliedForce, value);
}

void btMultibodyLink_getAppliedTorque(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_appliedTorque, value);
}

btSpatialMotionVector* btMultibodyLink_getAxes(btMultibodyLink* obj)
{
	return obj->m_axes;
}

void btMultibodyLink_getAxisBottom(btMultibodyLink* obj, int dof, btScalar* value)
{
	VECTOR3_OUT(&obj->getAxisBottom(dof), value);
}

void btMultibodyLink_getAxisTop(btMultibodyLink* obj, int dof, btScalar* value)
{
	VECTOR3_OUT(&obj->getAxisTop(dof), value);
}

void btMultibodyLink_getCachedRotParentToThis(btMultibodyLink* obj, btScalar* value)
{
	QUATERNION_OUT(&obj->m_cachedRotParentToThis, value);
}

void btMultibodyLink_getCachedRVector(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_cachedRVector, value);
}

void btMultibodyLink_getCachedWorldTransform(btMultibodyLink* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->m_cachedWorldTransform, value);
}

int btMultibodyLink_getCfgOffset(btMultibodyLink* obj)
{
	return obj->m_cfgOffset;
}

btMultiBodyLinkCollider* btMultibodyLink_getCollider(btMultibodyLink* obj)
{
	return obj->m_collider;
}

int btMultibodyLink_getDofCount(btMultibodyLink* obj)
{
	return obj->m_dofCount;
}

int btMultibodyLink_getDofOffset(btMultibodyLink* obj)
{
	return obj->m_dofOffset;
}

void btMultibodyLink_getDVector(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_dVector, value);
}

void btMultibodyLink_getEVector(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_eVector, value);
}

int btMultibodyLink_getFlags(btMultibodyLink* obj)
{
	return obj->m_flags;
}

void btMultibodyLink_getInertiaLocal(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_inertiaLocal, value);
}

btMultiBodyJointFeedback* btMultibodyLink_getJointFeedback(btMultibodyLink* obj)
{
	return obj->m_jointFeedback;
}

const char* btMultibodyLink_getJointName(btMultibodyLink* obj)
{
	return obj->m_jointName;
}

btScalar* btMultibodyLink_getJointPos(btMultibodyLink* obj)
{
	return obj->m_jointPos;
}

btScalar* btMultibodyLink_getJointTorque(btMultibodyLink* obj)
{
	return obj->m_jointTorque;
}

btMultibodyLink::eFeatherstoneJointType btMultibodyLink_getJointType(btMultibodyLink* obj)
{
	return obj->m_jointType;
}

const char* btMultibodyLink_getLinkName(btMultibodyLink* obj)
{
	return obj->m_linkName;
}

btScalar btMultibodyLink_getMass(btMultibodyLink* obj)
{
	return obj->m_mass;
}

int btMultibodyLink_getParent(btMultibodyLink* obj)
{
	return obj->m_parent;
}

int btMultibodyLink_getPosVarCount(btMultibodyLink* obj)
{
	return obj->m_posVarCount;
}

void btMultibodyLink_getZeroRotParentToThis(btMultibodyLink* obj, btScalar* value)
{
	QUATERNION_OUT(&obj->m_zeroRotParentToThis, value);
}
/*
void btMultibodyLink_setAbsFrameLocVelocity(btMultibodyLink* obj, const btSpatialMotionVector* value)
{
	obj->m_absFrameLocVelocity = value;
}

void btMultibodyLink_setAbsFrameTotVelocity(btMultibodyLink* obj, const btSpatialMotionVector* value)
{
	obj->m_absFrameTotVelocity = value;
}
*/
void btMultibodyLink_setAppliedConstraintForce(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_appliedConstraintForce);
}

void btMultibodyLink_setAppliedConstraintTorque(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_appliedConstraintTorque);
}

void btMultibodyLink_setAppliedForce(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_appliedForce);
}

void btMultibodyLink_setAppliedTorque(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_appliedTorque);
}

void btMultibodyLink_setAxisBottom(btMultibodyLink* obj, int dof, const btScalar* x, const btScalar* y, const btScalar* z)
{
	obj->setAxisBottom(dof, *x, *y, *z);
}

void btMultibodyLink_setAxisBottom2(btMultibodyLink* obj, int dof, const btScalar* axis)
{
	VECTOR3_CONV(axis);
	obj->setAxisBottom(dof, VECTOR3_USE(axis));
}

void btMultibodyLink_setAxisTop(btMultibodyLink* obj, int dof, const btScalar* x, const btScalar* y, const btScalar* z)
{
	obj->setAxisTop(dof, *x, *y, *z);
}

void btMultibodyLink_setAxisTop2(btMultibodyLink* obj, int dof, const btScalar* axis)
{
	VECTOR3_CONV(axis);
	obj->setAxisTop(dof, VECTOR3_USE(axis));
}

void btMultibodyLink_setCachedRotParentToThis(btMultibodyLink* obj, const btScalar* value)
{
	QUATERNION_IN(value, &obj->m_cachedRotParentToThis);
}

void btMultibodyLink_setCachedRVector(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_cachedRVector);
}

void btMultibodyLink_setCachedWorldTransform(btMultibodyLink* obj, const btScalar* value)
{
	TRANSFORM_IN(value, &obj->m_cachedWorldTransform);
}

void btMultibodyLink_setCfgOffset(btMultibodyLink* obj, int value)
{
	obj->m_cfgOffset = value;
}

void btMultibodyLink_setCollider(btMultibodyLink* obj, btMultiBodyLinkCollider* value)
{
	obj->m_collider = value;
}

void btMultibodyLink_setDofCount(btMultibodyLink* obj, int value)
{
	obj->m_dofCount = value;
}

void btMultibodyLink_setDofOffset(btMultibodyLink* obj, int value)
{
	obj->m_dofOffset = value;
}

void btMultibodyLink_setDVector(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_dVector);
}

void btMultibodyLink_setEVector(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_eVector);
}

void btMultibodyLink_setFlags(btMultibodyLink* obj, int value)
{
	obj->m_flags = value;
}

void btMultibodyLink_setInertiaLocal(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_inertiaLocal);
}

void btMultibodyLink_setJointFeedback(btMultibodyLink* obj, btMultiBodyJointFeedback* value)
{
	obj->m_jointFeedback = value;
}

void btMultibodyLink_setJointName(btMultibodyLink* obj, const char* value)
{
	obj->m_jointName = value;
}

void btMultibodyLink_setJointType(btMultibodyLink* obj, btMultibodyLink::eFeatherstoneJointType value)
{
	obj->m_jointType = value;
}

void btMultibodyLink_setLinkName(btMultibodyLink* obj, const char* value)
{
	obj->m_linkName = value;
}

void btMultibodyLink_setMass(btMultibodyLink* obj, btScalar value)
{
	obj->m_mass = value;
}

void btMultibodyLink_setParent(btMultibodyLink* obj, int value)
{
	obj->m_parent = value;
}

void btMultibodyLink_setPosVarCount(btMultibodyLink* obj, int value)
{
	obj->m_posVarCount = value;
}

void btMultibodyLink_setZeroRotParentToThis(btMultibodyLink* obj, const btScalar* value)
{
	QUATERNION_IN(value, &obj->m_zeroRotParentToThis);
}

void btMultibodyLink_updateCacheMultiDof(btMultibodyLink* obj)
{
	obj->updateCacheMultiDof();
}

void btMultibodyLink_updateCacheMultiDof2(btMultibodyLink* obj, btScalar* pq)
{
	obj->updateCacheMultiDof(pq);
}
