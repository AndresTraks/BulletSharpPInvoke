#include <BulletDynamics/Dynamics/btRigidBody.h>
#include <BulletDynamics/Vehicle/btWheelInfo.h>

#include "conversion.h"
#include "btWheelInfo_wrap.h"

#ifndef BULLETC_DISABLE_IACTION_CLASSES

btWheelInfoConstructionInfo* btWheelInfoConstructionInfo_new()
{
	return new btWheelInfoConstructionInfo();
}

bool btWheelInfoConstructionInfo_getBIsFrontWheel(btWheelInfoConstructionInfo* obj)
{
	return obj->m_bIsFrontWheel;
}

void btWheelInfoConstructionInfo_getChassisConnectionCS(btWheelInfoConstructionInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_chassisConnectionCS, value);
}

btScalar btWheelInfoConstructionInfo_getFrictionSlip(btWheelInfoConstructionInfo* obj)
{
	return obj->m_frictionSlip;
}

btScalar btWheelInfoConstructionInfo_getMaxSuspensionForce(btWheelInfoConstructionInfo* obj)
{
	return obj->m_maxSuspensionForce;
}

btScalar btWheelInfoConstructionInfo_getMaxSuspensionTravelCm(btWheelInfoConstructionInfo* obj)
{
	return obj->m_maxSuspensionTravelCm;
}

btScalar btWheelInfoConstructionInfo_getSuspensionRestLength(btWheelInfoConstructionInfo* obj)
{
	return obj->m_suspensionRestLength;
}

btScalar btWheelInfoConstructionInfo_getSuspensionStiffness(btWheelInfoConstructionInfo* obj)
{
	return obj->m_suspensionStiffness;
}

void btWheelInfoConstructionInfo_getWheelAxleCS(btWheelInfoConstructionInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_wheelAxleCS, value);
}

void btWheelInfoConstructionInfo_getWheelDirectionCS(btWheelInfoConstructionInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_wheelDirectionCS, value);
}

btScalar btWheelInfoConstructionInfo_getWheelRadius(btWheelInfoConstructionInfo* obj)
{
	return obj->m_wheelRadius;
}

btScalar btWheelInfoConstructionInfo_getWheelsDampingCompression(btWheelInfoConstructionInfo* obj)
{
	return obj->m_wheelsDampingCompression;
}

btScalar btWheelInfoConstructionInfo_getWheelsDampingRelaxation(btWheelInfoConstructionInfo* obj)
{
	return obj->m_wheelsDampingRelaxation;
}

void btWheelInfoConstructionInfo_setBIsFrontWheel(btWheelInfoConstructionInfo* obj, bool value)
{
	obj->m_bIsFrontWheel = value;
}

void btWheelInfoConstructionInfo_setChassisConnectionCS(btWheelInfoConstructionInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_chassisConnectionCS);
}

void btWheelInfoConstructionInfo_setFrictionSlip(btWheelInfoConstructionInfo* obj, btScalar value)
{
	obj->m_frictionSlip = value;
}

void btWheelInfoConstructionInfo_setMaxSuspensionForce(btWheelInfoConstructionInfo* obj, btScalar value)
{
	obj->m_maxSuspensionForce = value;
}

void btWheelInfoConstructionInfo_setMaxSuspensionTravelCm(btWheelInfoConstructionInfo* obj, btScalar value)
{
	obj->m_maxSuspensionTravelCm = value;
}

void btWheelInfoConstructionInfo_setSuspensionRestLength(btWheelInfoConstructionInfo* obj, btScalar value)
{
	obj->m_suspensionRestLength = value;
}

void btWheelInfoConstructionInfo_setSuspensionStiffness(btWheelInfoConstructionInfo* obj, btScalar value)
{
	obj->m_suspensionStiffness = value;
}

void btWheelInfoConstructionInfo_setWheelAxleCS(btWheelInfoConstructionInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_wheelAxleCS);
}

void btWheelInfoConstructionInfo_setWheelDirectionCS(btWheelInfoConstructionInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_wheelDirectionCS);
}

void btWheelInfoConstructionInfo_setWheelRadius(btWheelInfoConstructionInfo* obj, btScalar value)
{
	obj->m_wheelRadius = value;
}

void btWheelInfoConstructionInfo_setWheelsDampingCompression(btWheelInfoConstructionInfo* obj, btScalar value)
{
	obj->m_wheelsDampingCompression = value;
}

void btWheelInfoConstructionInfo_setWheelsDampingRelaxation(btWheelInfoConstructionInfo* obj, btScalar value)
{
	obj->m_wheelsDampingRelaxation = value;
}

void btWheelInfoConstructionInfo_delete(btWheelInfoConstructionInfo* obj)
{
	delete obj;
}


btWheelInfo::RaycastInfo* btWheelInfo_RaycastInfo_new()
{
	return new btWheelInfo::RaycastInfo();
}

void btWheelInfo_RaycastInfo_getContactNormalWS(btWheelInfo::RaycastInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_contactNormalWS, value);
}

void btWheelInfo_RaycastInfo_getContactPointWS(btWheelInfo::RaycastInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_contactPointWS, value);
}

void* btWheelInfo_RaycastInfo_getGroundObject(btWheelInfo::RaycastInfo* obj)
{
	return obj->m_groundObject;
}

void btWheelInfo_RaycastInfo_getHardPointWS(btWheelInfo::RaycastInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hardPointWS, value);
}

bool btWheelInfo_RaycastInfo_getIsInContact(btWheelInfo::RaycastInfo* obj)
{
	return obj->m_isInContact;
}

btScalar btWheelInfo_RaycastInfo_getSuspensionLength(btWheelInfo::RaycastInfo* obj)
{
	return obj->m_suspensionLength;
}

void btWheelInfo_RaycastInfo_getWheelAxleWS(btWheelInfo::RaycastInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_wheelAxleWS, value);
}

void btWheelInfo_RaycastInfo_getWheelDirectionWS(btWheelInfo::RaycastInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_wheelDirectionWS, value);
}

void btWheelInfo_RaycastInfo_setContactNormalWS(btWheelInfo::RaycastInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_contactNormalWS);
}

void btWheelInfo_RaycastInfo_setContactPointWS(btWheelInfo::RaycastInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_contactPointWS);
}

void btWheelInfo_RaycastInfo_setGroundObject(btWheelInfo::RaycastInfo* obj, void* value)
{
	obj->m_groundObject = value;
}

void btWheelInfo_RaycastInfo_setHardPointWS(btWheelInfo::RaycastInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hardPointWS);
}

void btWheelInfo_RaycastInfo_setIsInContact(btWheelInfo::RaycastInfo* obj, bool value)
{
	obj->m_isInContact = value;
}

void btWheelInfo_RaycastInfo_setSuspensionLength(btWheelInfo::RaycastInfo* obj, btScalar value)
{
	obj->m_suspensionLength = value;
}

void btWheelInfo_RaycastInfo_setWheelAxleWS(btWheelInfo::RaycastInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_wheelAxleWS);
}

void btWheelInfo_RaycastInfo_setWheelDirectionWS(btWheelInfo::RaycastInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_wheelDirectionWS);
}

void btWheelInfo_RaycastInfo_delete(btWheelInfo::RaycastInfo* obj)
{
	delete obj;
}


btWheelInfo* btWheelInfo_new(btWheelInfoConstructionInfo* ci)
{
	return new btWheelInfo(*ci);
}

bool btWheelInfo_getBIsFrontWheel(btWheelInfo* obj)
{
	return obj->m_bIsFrontWheel;
}

btScalar btWheelInfo_getBrake(btWheelInfo* obj)
{
	return obj->m_brake;
}

void btWheelInfo_getChassisConnectionPointCS(btWheelInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_chassisConnectionPointCS, value);
}

void* btWheelInfo_getClientInfo(btWheelInfo* obj)
{
	return obj->m_clientInfo;
}

btScalar btWheelInfo_getClippedInvContactDotSuspension(btWheelInfo* obj)
{
	return obj->m_clippedInvContactDotSuspension;
}

btScalar btWheelInfo_getDeltaRotation(btWheelInfo* obj)
{
	return obj->m_deltaRotation;
}

btScalar btWheelInfo_getEngineForce(btWheelInfo* obj)
{
	return obj->m_engineForce;
}

btScalar btWheelInfo_getFrictionSlip(btWheelInfo* obj)
{
	return obj->m_frictionSlip;
}

btScalar btWheelInfo_getMaxSuspensionForce(btWheelInfo* obj)
{
	return obj->m_maxSuspensionForce;
}

btScalar btWheelInfo_getMaxSuspensionTravelCm(btWheelInfo* obj)
{
	return obj->m_maxSuspensionTravelCm;
}

btWheelInfo::RaycastInfo* btWheelInfo_getRaycastInfo(btWheelInfo* obj)
{
	return &obj->m_raycastInfo;
}

btScalar btWheelInfo_getRollInfluence(btWheelInfo* obj)
{
	return obj->m_rollInfluence;
}

btScalar btWheelInfo_getRotation(btWheelInfo* obj)
{
	return obj->m_rotation;
}

btScalar btWheelInfo_getSkidInfo(btWheelInfo* obj)
{
	return obj->m_skidInfo;
}

btScalar btWheelInfo_getSteering(btWheelInfo* obj)
{
	return obj->m_steering;
}

btScalar btWheelInfo_getSuspensionRelativeVelocity(btWheelInfo* obj)
{
	return obj->m_suspensionRelativeVelocity;
}

btScalar btWheelInfo_getSuspensionRestLength(btWheelInfo* obj)
{
	return obj->getSuspensionRestLength();
}

btScalar btWheelInfo_getSuspensionRestLength1(btWheelInfo* obj)
{
	return obj->m_suspensionRestLength1;
}

btScalar btWheelInfo_getSuspensionStiffness(btWheelInfo* obj)
{
	return obj->m_suspensionStiffness;
}

void btWheelInfo_getWheelAxleCS(btWheelInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_wheelAxleCS, value);
}

void btWheelInfo_getWheelDirectionCS(btWheelInfo* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_wheelDirectionCS, value);
}

btScalar btWheelInfo_getWheelsDampingCompression(btWheelInfo* obj)
{
	return obj->m_wheelsDampingCompression;
}

btScalar btWheelInfo_getWheelsDampingRelaxation(btWheelInfo* obj)
{
	return obj->m_wheelsDampingRelaxation;
}

btScalar btWheelInfo_getWheelsRadius(btWheelInfo* obj)
{
	return obj->m_wheelsRadius;
}

btScalar btWheelInfo_getWheelsSuspensionForce(btWheelInfo* obj)
{
	return obj->m_wheelsSuspensionForce;
}

void btWheelInfo_getWorldTransform(btWheelInfo* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->m_worldTransform, value);
}

void btWheelInfo_setBIsFrontWheel(btWheelInfo* obj, bool value)
{
	obj->m_bIsFrontWheel = value;
}

void btWheelInfo_setBrake(btWheelInfo* obj, btScalar value)
{
	obj->m_brake = value;
}

void btWheelInfo_setChassisConnectionPointCS(btWheelInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_chassisConnectionPointCS);
}

void btWheelInfo_setClientInfo(btWheelInfo* obj, void* value)
{
	obj->m_clientInfo = value;
}

void btWheelInfo_setClippedInvContactDotSuspension(btWheelInfo* obj, btScalar value)
{
	obj->m_clippedInvContactDotSuspension = value;
}

void btWheelInfo_setDeltaRotation(btWheelInfo* obj, btScalar value)
{
	obj->m_deltaRotation = value;
}

void btWheelInfo_setEngineForce(btWheelInfo* obj, btScalar value)
{
	obj->m_engineForce = value;
}

void btWheelInfo_setFrictionSlip(btWheelInfo* obj, btScalar value)
{
	obj->m_frictionSlip = value;
}

void btWheelInfo_setMaxSuspensionForce(btWheelInfo* obj, btScalar value)
{
	obj->m_maxSuspensionForce = value;
}

void btWheelInfo_setMaxSuspensionTravelCm(btWheelInfo* obj, btScalar value)
{
	obj->m_maxSuspensionTravelCm = value;
}

void btWheelInfo_setRollInfluence(btWheelInfo* obj, btScalar value)
{
	obj->m_rollInfluence = value;
}

void btWheelInfo_setRotation(btWheelInfo* obj, btScalar value)
{
	obj->m_rotation = value;
}

void btWheelInfo_setSkidInfo(btWheelInfo* obj, btScalar value)
{
	obj->m_skidInfo = value;
}

void btWheelInfo_setSteering(btWheelInfo* obj, btScalar value)
{
	obj->m_steering = value;
}

void btWheelInfo_setSuspensionRelativeVelocity(btWheelInfo* obj, btScalar value)
{
	obj->m_suspensionRelativeVelocity = value;
}

void btWheelInfo_setSuspensionRestLength1(btWheelInfo* obj, btScalar value)
{
	obj->m_suspensionRestLength1 = value;
}

void btWheelInfo_setSuspensionStiffness(btWheelInfo* obj, btScalar value)
{
	obj->m_suspensionStiffness = value;
}

void btWheelInfo_setWheelAxleCS(btWheelInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_wheelAxleCS);
}

void btWheelInfo_setWheelDirectionCS(btWheelInfo* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_wheelDirectionCS);
}

void btWheelInfo_setWheelsDampingCompression(btWheelInfo* obj, btScalar value)
{
	obj->m_wheelsDampingCompression = value;
}

void btWheelInfo_setWheelsDampingRelaxation(btWheelInfo* obj, btScalar value)
{
	obj->m_wheelsDampingRelaxation = value;
}

void btWheelInfo_setWheelsRadius(btWheelInfo* obj, btScalar value)
{
	obj->m_wheelsRadius = value;
}

void btWheelInfo_setWheelsSuspensionForce(btWheelInfo* obj, btScalar value)
{
	obj->m_wheelsSuspensionForce = value;
}

void btWheelInfo_setWorldTransform(btWheelInfo* obj, const btScalar* value)
{
	TRANSFORM_IN(value, &obj->m_worldTransform);
}

void btWheelInfo_updateWheel(btWheelInfo* obj, const btRigidBody* chassis, btWheelInfo::RaycastInfo* raycastInfo)
{
	obj->updateWheel(*chassis, *raycastInfo);
}

void btWheelInfo_delete(btWheelInfo* obj)
{
	delete obj;
}

#endif
