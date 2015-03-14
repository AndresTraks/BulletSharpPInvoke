#include <BulletDynamics/Vehicle/btVehicleRaycaster.h>

#include "conversion.h"
#include "btVehicleRaycaster_wrap.h"

#ifndef BULLETC_DISABLE_IACTION_CLASSES

btVehicleRaycaster::btVehicleRaycasterResult* btVehicleRaycaster_btVehicleRaycasterResult_new()
{
	return new btVehicleRaycaster::btVehicleRaycasterResult();
}

btScalar btVehicleRaycaster_btVehicleRaycasterResult_getDistFraction(btVehicleRaycaster::btVehicleRaycasterResult* obj)
{
	return obj->m_distFraction;
}

void btVehicleRaycaster_btVehicleRaycasterResult_getHitNormalInWorld(btVehicleRaycaster::btVehicleRaycasterResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitNormalInWorld, value);
}

void btVehicleRaycaster_btVehicleRaycasterResult_getHitPointInWorld(btVehicleRaycaster::btVehicleRaycasterResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitPointInWorld, value);
}

void btVehicleRaycaster_btVehicleRaycasterResult_setDistFraction(btVehicleRaycaster::btVehicleRaycasterResult* obj, btScalar value)
{
	obj->m_distFraction = value;
}

void btVehicleRaycaster_btVehicleRaycasterResult_setHitNormalInWorld(btVehicleRaycaster::btVehicleRaycasterResult* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitNormalInWorld);
}

void btVehicleRaycaster_btVehicleRaycasterResult_setHitPointInWorld(btVehicleRaycaster::btVehicleRaycasterResult* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitPointInWorld);
}

void btVehicleRaycaster_btVehicleRaycasterResult_delete(btVehicleRaycaster::btVehicleRaycasterResult* obj)
{
	delete obj;
}


void* btVehicleRaycaster_castRay(btVehicleRaycaster* obj, const btScalar* from, const btScalar* to, btVehicleRaycaster::btVehicleRaycasterResult* result)
{
	VECTOR3_CONV(from);
	VECTOR3_CONV(to);
	return obj->castRay(VECTOR3_USE(from), VECTOR3_USE(to), *result);
}

void btVehicleRaycaster_delete(btVehicleRaycaster* obj)
{
	delete obj;
}

#endif
