#include <../Extras/Serialize/BulletXmlWorldImporter/btBulletXmlWorldImporter.h>
#include <BulletDynamics/Dynamics/btDynamicsWorld.h>

#include "btBulletXmlWorldImporter_wrap.h"

#ifndef BULLETC_DISABLE_WORLD_IMPORTERS

btBulletXmlWorldImporter* btBulletXmlWorldImporter_new(btDynamicsWorld* world)
{
	return new btBulletXmlWorldImporter(world);
}

bool btBulletXmlWorldImporter_loadFile(btBulletXmlWorldImporter* obj, const char* fileName)
{
	return obj->loadFile(fileName);
}

#endif
