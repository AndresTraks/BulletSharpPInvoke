#include "main.h"

extern "C"
{
	EXPORT btMultiSphereShape* btMultiSphereShape_new(const btScalar* positions, const btScalar* radi, int numSpheres);
	EXPORT btMultiSphereShape* btMultiSphereShape_new2(const btVector3* positions, const btScalar* radi, int numSpheres);
	EXPORT int btMultiSphereShape_getSphereCount(btMultiSphereShape* obj);
	EXPORT void btMultiSphereShape_getSpherePosition(btMultiSphereShape* obj, int index, btScalar* value);
	EXPORT btScalar btMultiSphereShape_getSphereRadius(btMultiSphereShape* obj, int index);
	/*
	EXPORT btPositionAndRadius* btPositionAndRadius_new();
	//EXPORT void btPositionAndRadius_getPos(btPositionAndRadius* obj, btScalar* value);
	EXPORT float btPositionAndRadius_getRadius(btPositionAndRadius* obj);
	//EXPORT void btPositionAndRadius_setPos(btPositionAndRadius* obj, const btScalar* value);
	EXPORT void btPositionAndRadius_setRadius(btPositionAndRadius* obj, float value);
	EXPORT void btPositionAndRadius_delete(btPositionAndRadius* obj);
	*/
}
