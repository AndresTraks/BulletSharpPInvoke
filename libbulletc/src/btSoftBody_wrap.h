#include "main.h"

#ifndef _BT_SOFT_BODY_H
#define p_btSoftBody_AJoint_IControl_Prepare void*
#define p_btSoftBody_AJoint_IControl_Speed void*
#define p_btSoftBody_ImplicitFn_Eval void*
#define btSoftBody_AJoint_IControlWrapper void
#define btSoftBody_ImplicitFnWrapper void
#else
typedef void (*p_btSoftBody_AJoint_IControl_Prepare)(btSoftBody_AJoint* aJoint);
typedef btScalar (*p_btSoftBody_AJoint_IControl_Speed)(btSoftBody_AJoint* aJoint,
	btScalar current);

class btSoftBody_AJoint_IControlWrapper : public btSoftBody_AJoint_IControl
{
private:
	p_btSoftBody_AJoint_IControl_Prepare _PrepareCallback;
	p_btSoftBody_AJoint_IControl_Speed _SpeedCallback;
	void* _wrapperData;

public:
	btSoftBody_AJoint_IControlWrapper(p_btSoftBody_AJoint_IControl_Prepare PrepareCallback,
		p_btSoftBody_AJoint_IControl_Speed SpeedCallback);

	virtual void Prepare(btSoftBody_AJoint* aJoint);
	virtual btScalar Speed(btSoftBody_AJoint* aJoint, btScalar current);
	void* getWrapperData() const;
	void setWrapperData(void* data);
};

typedef btScalar (*p_btSoftBody_ImplicitFn_Eval)(const btVector3* x);

class btSoftBody_ImplicitFnWrapper : public btSoftBody_ImplicitFn
{
private:
	p_btSoftBody_ImplicitFn_Eval _EvalCallback;

public:
	btSoftBody_ImplicitFnWrapper(p_btSoftBody_ImplicitFn_Eval EvalCallback);

	virtual btScalar Eval(const btVector3& x);
};
#endif

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btSoftBodyWorldInfo* btSoftBodyWorldInfo_new();
	EXPORT btScalar btSoftBodyWorldInfo_getAir_density(btSoftBodyWorldInfo* obj);
	EXPORT btBroadphaseInterface* btSoftBodyWorldInfo_getBroadphase(btSoftBodyWorldInfo* obj);
	EXPORT btDispatcher* btSoftBodyWorldInfo_getDispatcher(btSoftBodyWorldInfo* obj);
	EXPORT void btSoftBodyWorldInfo_getGravity(btSoftBodyWorldInfo* obj, btVector3* value);
	EXPORT btScalar btSoftBodyWorldInfo_getMaxDisplacement(btSoftBodyWorldInfo* obj);
	EXPORT btSparseSdf_3* btSoftBodyWorldInfo_getSparsesdf(btSoftBodyWorldInfo* obj);
	EXPORT btScalar btSoftBodyWorldInfo_getWater_density(btSoftBodyWorldInfo* obj);
	EXPORT void btSoftBodyWorldInfo_getWater_normal(btSoftBodyWorldInfo* obj, btVector3* value);
	EXPORT btScalar btSoftBodyWorldInfo_getWater_offset(btSoftBodyWorldInfo* obj);
	EXPORT void btSoftBodyWorldInfo_setAir_density(btSoftBodyWorldInfo* obj, btScalar value);
	EXPORT void btSoftBodyWorldInfo_setBroadphase(btSoftBodyWorldInfo* obj, btBroadphaseInterface* value);
	EXPORT void btSoftBodyWorldInfo_setDispatcher(btSoftBodyWorldInfo* obj, btDispatcher* value);
	EXPORT void btSoftBodyWorldInfo_setGravity(btSoftBodyWorldInfo* obj, const btVector3* value);
	EXPORT void btSoftBodyWorldInfo_setMaxDisplacement(btSoftBodyWorldInfo* obj, btScalar value);
	EXPORT void btSoftBodyWorldInfo_setWater_density(btSoftBodyWorldInfo* obj, btScalar value);
	EXPORT void btSoftBodyWorldInfo_setWater_normal(btSoftBodyWorldInfo* obj, const btVector3* value);
	EXPORT void btSoftBodyWorldInfo_setWater_offset(btSoftBodyWorldInfo* obj, btScalar value);
	EXPORT void btSoftBodyWorldInfo_delete(btSoftBodyWorldInfo* obj);

	EXPORT btSoftBody_AJoint_IControlWrapper* btSoftBody_AJoint_IControlWrapper_new(
		p_btSoftBody_AJoint_IControl_Prepare PrepareCallback, p_btSoftBody_AJoint_IControl_Speed SpeedCallback);
	EXPORT void* btSoftBody_AJoint_IControlWrapper_getWrapperData(btSoftBody_AJoint_IControlWrapper* obj);
	EXPORT void btSoftBody_AJoint_IControlWrapper_setWrapperData(btSoftBody_AJoint_IControlWrapper* obj, void* data);

	EXPORT btSoftBody_AJoint_IControl* btSoftBody_AJoint_IControl_new();
	EXPORT btSoftBody_AJoint_IControl* btSoftBody_AJoint_IControl_Default();
	EXPORT void btSoftBody_AJoint_IControl_Prepare(btSoftBody_AJoint_IControl* obj, btSoftBody_AJoint* __unnamed0);
	EXPORT btScalar btSoftBody_AJoint_IControl_Speed(btSoftBody_AJoint_IControl* obj, btSoftBody_AJoint* __unnamed0, btScalar current);
	EXPORT void btSoftBody_AJoint_IControl_delete(btSoftBody_AJoint_IControl* obj);

	EXPORT btSoftBody_AJoint_Specs* btSoftBody_AJoint_Specs_new();
	EXPORT void btSoftBody_AJoint_Specs_getAxis(btSoftBody_AJoint_Specs* obj, btVector3* value);
	EXPORT btSoftBody_AJoint_IControl* btSoftBody_AJoint_Specs_getIcontrol(btSoftBody_AJoint_Specs* obj);
	EXPORT void btSoftBody_AJoint_Specs_setAxis(btSoftBody_AJoint_Specs* obj, const btVector3* value);
	EXPORT void btSoftBody_AJoint_Specs_setIcontrol(btSoftBody_AJoint_Specs* obj, btSoftBody_AJoint_IControl* value);

	EXPORT btVector3* btSoftBody_AJoint_getAxis(btSoftBody_AJoint* obj);
	EXPORT btSoftBody_AJoint_IControl* btSoftBody_AJoint_getIcontrol(btSoftBody_AJoint* obj);
	EXPORT void btSoftBody_AJoint_setIcontrol(btSoftBody_AJoint* obj, btSoftBody_AJoint_IControl* value);

	EXPORT btRigidBody* btSoftBody_Anchor_getBody(btSoftBody_Anchor* obj);
	EXPORT void btSoftBody_Anchor_getC0(btSoftBody_Anchor* obj, btMatrix3x3* value);
	EXPORT void btSoftBody_Anchor_getC1(btSoftBody_Anchor* obj, btVector3* value);
	EXPORT btScalar btSoftBody_Anchor_getC2(btSoftBody_Anchor* obj);
	EXPORT btScalar btSoftBody_Anchor_getInfluence(btSoftBody_Anchor* obj);
	EXPORT void btSoftBody_Anchor_getLocal(btSoftBody_Anchor* obj, btVector3* value);
	EXPORT btSoftBody_Node* btSoftBody_Anchor_getNode(btSoftBody_Anchor* obj);
	EXPORT void btSoftBody_Anchor_setBody(btSoftBody_Anchor* obj, btRigidBody* value);
	EXPORT void btSoftBody_Anchor_setC0(btSoftBody_Anchor* obj, const btMatrix3x3* value);
	EXPORT void btSoftBody_Anchor_setC1(btSoftBody_Anchor* obj, const btVector3* value);
	EXPORT void btSoftBody_Anchor_setC2(btSoftBody_Anchor* obj, btScalar value);
	EXPORT void btSoftBody_Anchor_setInfluence(btSoftBody_Anchor* obj, btScalar value);
	EXPORT void btSoftBody_Anchor_setLocal(btSoftBody_Anchor* obj, const btVector3* value);
	EXPORT void btSoftBody_Anchor_setNode(btSoftBody_Anchor* obj, btSoftBody_Node* value);

	EXPORT btSoftBody_Body* btSoftBody_Body_new();
	EXPORT btSoftBody_Body* btSoftBody_Body_new2(const btCollisionObject* colObj);
	EXPORT btSoftBody_Body* btSoftBody_Body_new3(btSoftBody_Cluster* p);
	EXPORT void btSoftBody_Body_activate(btSoftBody_Body* obj);
	EXPORT void btSoftBody_Body_angularVelocity(btSoftBody_Body* obj, const btVector3* rpos, btVector3* value);
	EXPORT void btSoftBody_Body_angularVelocity2(btSoftBody_Body* obj, btVector3* value);
	EXPORT void btSoftBody_Body_applyAImpulse(btSoftBody_Body* obj, const btSoftBody_Impulse* impulse);
	EXPORT void btSoftBody_Body_applyDAImpulse(btSoftBody_Body* obj, const btVector3* impulse);
	EXPORT void btSoftBody_Body_applyDCImpulse(btSoftBody_Body* obj, const btVector3* impulse);
	EXPORT void btSoftBody_Body_applyDImpulse(btSoftBody_Body* obj, const btVector3* impulse, const btVector3* rpos);
	EXPORT void btSoftBody_Body_applyImpulse(btSoftBody_Body* obj, const btSoftBody_Impulse* impulse, const btVector3* rpos);
	EXPORT void btSoftBody_Body_applyVAImpulse(btSoftBody_Body* obj, const btVector3* impulse);
	EXPORT void btSoftBody_Body_applyVImpulse(btSoftBody_Body* obj, const btVector3* impulse, const btVector3* rpos);
	EXPORT const btCollisionObject* btSoftBody_Body_getCollisionObject(btSoftBody_Body* obj);
	EXPORT btRigidBody* btSoftBody_Body_getRigid(btSoftBody_Body* obj);
	EXPORT btSoftBody_Cluster* btSoftBody_Body_getSoft(btSoftBody_Body* obj);
	EXPORT btScalar btSoftBody_Body_invMass(btSoftBody_Body* obj);
	EXPORT void btSoftBody_Body_invWorldInertia(btSoftBody_Body* obj, btMatrix3x3* value);
	EXPORT void btSoftBody_Body_linearVelocity(btSoftBody_Body* obj, btVector3* value);
	EXPORT void btSoftBody_Body_setCollisionObject(btSoftBody_Body* obj, const btCollisionObject* value);
	EXPORT void btSoftBody_Body_setRigid(btSoftBody_Body* obj, btRigidBody* value);
	EXPORT void btSoftBody_Body_setSoft(btSoftBody_Body* obj, btSoftBody_Cluster* value);
	EXPORT void btSoftBody_Body_velocity(btSoftBody_Body* obj, const btVector3* rpos, btVector3* value);
	EXPORT void btSoftBody_Body_xform(btSoftBody_Body* obj, btTransform* value);
	EXPORT void btSoftBody_Body_delete(btSoftBody_Body* obj);

	EXPORT btSoftBody_Body* btSoftBody_Body_array_at(btSoftBody_Body* obj, int index);

	EXPORT btScalar btSoftBody_CJoint_getFriction(btSoftBody_CJoint* obj);
	EXPORT int btSoftBody_CJoint_getLife(btSoftBody_CJoint* obj);
	EXPORT int btSoftBody_CJoint_getMaxlife(btSoftBody_CJoint* obj);
	EXPORT void btSoftBody_CJoint_getNormal(btSoftBody_CJoint* obj, btVector3* value);
	EXPORT btVector3* btSoftBody_CJoint_getRpos(btSoftBody_CJoint* obj);
	EXPORT void btSoftBody_CJoint_setFriction(btSoftBody_CJoint* obj, btScalar value);
	EXPORT void btSoftBody_CJoint_setLife(btSoftBody_CJoint* obj, int value);
	EXPORT void btSoftBody_CJoint_setMaxlife(btSoftBody_CJoint* obj, int value);
	EXPORT void btSoftBody_CJoint_setNormal(btSoftBody_CJoint* obj, const btVector3* value);

	EXPORT btScalar btSoftBody_Cluster_getAdamping(btSoftBody_Cluster* obj);
	EXPORT void btSoftBody_Cluster_getAv(btSoftBody_Cluster* obj, btVector3* value);
	EXPORT int btSoftBody_Cluster_getClusterIndex(btSoftBody_Cluster* obj);
	EXPORT bool btSoftBody_Cluster_getCollide(btSoftBody_Cluster* obj);
	EXPORT void btSoftBody_Cluster_getCom(btSoftBody_Cluster* obj, btVector3* value);
	EXPORT bool btSoftBody_Cluster_getContainsAnchor(btSoftBody_Cluster* obj);
	EXPORT btVector3* btSoftBody_Cluster_getDimpulses(btSoftBody_Cluster* obj);
	EXPORT btAlignedObjectArray_btVector3* btSoftBody_Cluster_getFramerefs(btSoftBody_Cluster* obj);
	EXPORT void btSoftBody_Cluster_getFramexform(btSoftBody_Cluster* obj, btTransform* value);
	EXPORT btScalar btSoftBody_Cluster_getIdmass(btSoftBody_Cluster* obj);
	EXPORT btScalar btSoftBody_Cluster_getImass(btSoftBody_Cluster* obj);
	EXPORT void btSoftBody_Cluster_getInvwi(btSoftBody_Cluster* obj, btMatrix3x3* value);
	EXPORT btScalar btSoftBody_Cluster_getLdamping(btSoftBody_Cluster* obj);
	EXPORT btDbvtNode* btSoftBody_Cluster_getLeaf(btSoftBody_Cluster* obj);
	EXPORT void btSoftBody_Cluster_getLocii(btSoftBody_Cluster* obj, btMatrix3x3* value);
	EXPORT void btSoftBody_Cluster_getLv(btSoftBody_Cluster* obj, btVector3* value);
	EXPORT btAlignedObjectArray_btScalar* btSoftBody_Cluster_getMasses(btSoftBody_Cluster* obj);
	EXPORT btScalar btSoftBody_Cluster_getMatching(btSoftBody_Cluster* obj);
	EXPORT btScalar btSoftBody_Cluster_getMaxSelfCollisionImpulse(btSoftBody_Cluster* obj);
	EXPORT btScalar btSoftBody_Cluster_getNdamping(btSoftBody_Cluster* obj);
	EXPORT int btSoftBody_Cluster_getNdimpulses(btSoftBody_Cluster* obj);
	EXPORT btAlignedObjectArray_btSoftBody_NodePtr* btSoftBody_Cluster_getNodes(btSoftBody_Cluster* obj);
	EXPORT int btSoftBody_Cluster_getNvimpulses(btSoftBody_Cluster* obj);
	EXPORT btScalar btSoftBody_Cluster_getSelfCollisionImpulseFactor(btSoftBody_Cluster* obj);
	EXPORT btVector3* btSoftBody_Cluster_getVimpulses(btSoftBody_Cluster* obj);
	EXPORT void btSoftBody_Cluster_setAdamping(btSoftBody_Cluster* obj, btScalar value);
	EXPORT void btSoftBody_Cluster_setAv(btSoftBody_Cluster* obj, const btVector3* value);
	EXPORT void btSoftBody_Cluster_setClusterIndex(btSoftBody_Cluster* obj, int value);
	EXPORT void btSoftBody_Cluster_setCollide(btSoftBody_Cluster* obj, bool value);
	EXPORT void btSoftBody_Cluster_setCom(btSoftBody_Cluster* obj, const btVector3* value);
	EXPORT void btSoftBody_Cluster_setContainsAnchor(btSoftBody_Cluster* obj, bool value);
	EXPORT void btSoftBody_Cluster_setFramexform(btSoftBody_Cluster* obj, const btTransform* value);
	EXPORT void btSoftBody_Cluster_setIdmass(btSoftBody_Cluster* obj, btScalar value);
	EXPORT void btSoftBody_Cluster_setImass(btSoftBody_Cluster* obj, btScalar value);
	EXPORT void btSoftBody_Cluster_setInvwi(btSoftBody_Cluster* obj, const btMatrix3x3* value);
	EXPORT void btSoftBody_Cluster_setLdamping(btSoftBody_Cluster* obj, btScalar value);
	EXPORT void btSoftBody_Cluster_setLeaf(btSoftBody_Cluster* obj, btDbvtNode* value);
	EXPORT void btSoftBody_Cluster_setLocii(btSoftBody_Cluster* obj, const btMatrix3x3* value);
	EXPORT void btSoftBody_Cluster_setLv(btSoftBody_Cluster* obj, const btVector3* value);
	EXPORT void btSoftBody_Cluster_setMatching(btSoftBody_Cluster* obj, btScalar value);
	EXPORT void btSoftBody_Cluster_setMaxSelfCollisionImpulse(btSoftBody_Cluster* obj, btScalar value);
	EXPORT void btSoftBody_Cluster_setNdamping(btSoftBody_Cluster* obj, btScalar value);
	EXPORT void btSoftBody_Cluster_setNdimpulses(btSoftBody_Cluster* obj, int value);
	EXPORT void btSoftBody_Cluster_setNvimpulses(btSoftBody_Cluster* obj, int value);
	EXPORT void btSoftBody_Cluster_setSelfCollisionImpulseFactor(btSoftBody_Cluster* obj, btScalar value);

	EXPORT btSoftBody_eAeroModel btSoftBody_Config_getAeromodel(btSoftBody_Config* obj);
	EXPORT int btSoftBody_Config_getCiterations(btSoftBody_Config* obj);
	EXPORT int btSoftBody_Config_getCollisions(btSoftBody_Config* obj);
	EXPORT int btSoftBody_Config_getDiterations(btSoftBody_Config* obj);
	EXPORT btAlignedObjectArray_btSoftBody_ePSolver__* btSoftBody_Config_getDsequence(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKAHR(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKCHR(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKDF(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKDG(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKDP(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKKHR(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKLF(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKMT(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKPR(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKSHR(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKSK_SPLT_CL(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKSKHR_CL(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKSR_SPLT_CL(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKSRHR_CL(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKSS_SPLT_CL(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKSSHR_CL(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKVC(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getKVCF(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getMaxvolume(btSoftBody_Config* obj);
	EXPORT int btSoftBody_Config_getPiterations(btSoftBody_Config* obj);
	EXPORT btAlignedObjectArray_btSoftBody_ePSolver__* btSoftBody_Config_getPsequence(btSoftBody_Config* obj);
	EXPORT btScalar btSoftBody_Config_getTimescale(btSoftBody_Config* obj);
	EXPORT int btSoftBody_Config_getViterations(btSoftBody_Config* obj);
	EXPORT btAlignedObjectArray_btSoftBody_eVSolver__* btSoftBody_Config_getVsequence(btSoftBody_Config* obj);
	EXPORT void btSoftBody_Config_setAeromodel(btSoftBody_Config* obj, btSoftBody_eAeroModel value);
	EXPORT void btSoftBody_Config_setCiterations(btSoftBody_Config* obj, int value);
	EXPORT void btSoftBody_Config_setCollisions(btSoftBody_Config* obj, int value);
	EXPORT void btSoftBody_Config_setDiterations(btSoftBody_Config* obj, int value);
	EXPORT void btSoftBody_Config_setKAHR(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKCHR(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKDF(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKDG(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKDP(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKKHR(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKLF(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKMT(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKPR(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKSHR(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKSK_SPLT_CL(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKSKHR_CL(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKSR_SPLT_CL(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKSRHR_CL(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKSS_SPLT_CL(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKSSHR_CL(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKVC(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setKVCF(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setMaxvolume(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setPiterations(btSoftBody_Config* obj, int value);
	EXPORT void btSoftBody_Config_setTimescale(btSoftBody_Config* obj, btScalar value);
	EXPORT void btSoftBody_Config_setViterations(btSoftBody_Config* obj, int value);

	EXPORT void* btSoftBody_Element_getTag(btSoftBody_Element* obj);
	EXPORT void btSoftBody_Element_setTag(btSoftBody_Element* obj, void* value);
	EXPORT void btSoftBody_Element_delete(btSoftBody_Element* obj);

	EXPORT btDbvtNode* btSoftBody_Face_getLeaf(btSoftBody_Face* obj);
	EXPORT btSoftBody_Node** btSoftBody_Face_getN(btSoftBody_Face* obj);
	EXPORT void btSoftBody_Face_getNormal(btSoftBody_Face* obj, btVector3* value);
	EXPORT btScalar btSoftBody_Face_getRa(btSoftBody_Face* obj);
	EXPORT void btSoftBody_Face_setLeaf(btSoftBody_Face* obj, btDbvtNode* value);
	EXPORT void btSoftBody_Face_setNormal(btSoftBody_Face* obj, const btVector3* value);
	EXPORT void btSoftBody_Face_setRa(btSoftBody_Face* obj, btScalar value);

	EXPORT btSoftBody_Material* btSoftBody_Feature_getMaterial(btSoftBody_Feature* obj);
	EXPORT void btSoftBody_Feature_setMaterial(btSoftBody_Feature* obj, btSoftBody_Material* value);

	EXPORT btSoftBody_ImplicitFnWrapper* btSoftBody_ImplicitFnWrapper_new(p_btSoftBody_ImplicitFn_Eval EvalCallback);

	EXPORT btScalar btSoftBody_ImplicitFn_Eval(btSoftBody_ImplicitFn* obj, const btVector3* x);
	EXPORT void btSoftBody_ImplicitFn_delete(btSoftBody_ImplicitFn* obj);

	EXPORT btSoftBody_Impulse* btSoftBody_Impulse_new();
	EXPORT int btSoftBody_Impulse_getAsDrift(btSoftBody_Impulse* obj);
	EXPORT int btSoftBody_Impulse_getAsVelocity(btSoftBody_Impulse* obj);
	EXPORT void btSoftBody_Impulse_getDrift(btSoftBody_Impulse* obj, btVector3* value);
	EXPORT void btSoftBody_Impulse_getVelocity(btSoftBody_Impulse* obj, btVector3* value);
	EXPORT btSoftBody_Impulse* btSoftBody_Impulse_operator_n(btSoftBody_Impulse* obj);
	EXPORT btSoftBody_Impulse* btSoftBody_Impulse_operator_m(btSoftBody_Impulse* obj, btScalar x);
	EXPORT void btSoftBody_Impulse_setAsDrift(btSoftBody_Impulse* obj, int value);
	EXPORT void btSoftBody_Impulse_setAsVelocity(btSoftBody_Impulse* obj, int value);
	EXPORT void btSoftBody_Impulse_setDrift(btSoftBody_Impulse* obj, const btVector3* value);
	EXPORT void btSoftBody_Impulse_setVelocity(btSoftBody_Impulse* obj, const btVector3* value);
	EXPORT void btSoftBody_Impulse_delete(btSoftBody_Impulse* obj);

	EXPORT btSoftBody_Joint_Specs* btSoftBody_Joint_Specs_new();
	EXPORT btScalar btSoftBody_Joint_Specs_getCfm(btSoftBody_Joint_Specs* obj);
	EXPORT btScalar btSoftBody_Joint_Specs_getErp(btSoftBody_Joint_Specs* obj);
	EXPORT btScalar btSoftBody_Joint_Specs_getSplit(btSoftBody_Joint_Specs* obj);
	EXPORT void btSoftBody_Joint_Specs_setCfm(btSoftBody_Joint_Specs* obj, btScalar value);
	EXPORT void btSoftBody_Joint_Specs_setErp(btSoftBody_Joint_Specs* obj, btScalar value);
	EXPORT void btSoftBody_Joint_Specs_setSplit(btSoftBody_Joint_Specs* obj, btScalar value);
	EXPORT void btSoftBody_Joint_Specs_delete(btSoftBody_Joint_Specs* obj);

	EXPORT btSoftBody_Body* btSoftBody_Joint_getBodies(btSoftBody_Joint* obj);
	EXPORT btScalar btSoftBody_Joint_getCfm(btSoftBody_Joint* obj);
	EXPORT bool btSoftBody_Joint_getDelete(btSoftBody_Joint* obj);
	EXPORT void btSoftBody_Joint_getDrift(btSoftBody_Joint* obj, btVector3* value);
	EXPORT btScalar btSoftBody_Joint_getErp(btSoftBody_Joint* obj);
	EXPORT void btSoftBody_Joint_getMassmatrix(btSoftBody_Joint* obj, btMatrix3x3* value);
	EXPORT btVector3* btSoftBody_Joint_getRefs(btSoftBody_Joint* obj);
	EXPORT void btSoftBody_Joint_getSdrift(btSoftBody_Joint* obj, btVector3* value);
	EXPORT btScalar btSoftBody_Joint_getSplit(btSoftBody_Joint* obj);
	EXPORT void btSoftBody_Joint_Prepare(btSoftBody_Joint* obj, btScalar dt, int iterations);
	EXPORT void btSoftBody_Joint_setCfm(btSoftBody_Joint* obj, btScalar value);
	EXPORT void btSoftBody_Joint_setDelete(btSoftBody_Joint* obj, bool value);
	EXPORT void btSoftBody_Joint_setDrift(btSoftBody_Joint* obj, const btVector3* value);
	EXPORT void btSoftBody_Joint_setErp(btSoftBody_Joint* obj, btScalar value);
	EXPORT void btSoftBody_Joint_setMassmatrix(btSoftBody_Joint* obj, const btMatrix3x3* value);
	EXPORT void btSoftBody_Joint_setSdrift(btSoftBody_Joint* obj, const btVector3* value);
	EXPORT void btSoftBody_Joint_setSplit(btSoftBody_Joint* obj, btScalar value);
	EXPORT void btSoftBody_Joint_Solve(btSoftBody_Joint* obj, btScalar dt, btScalar sor);
	EXPORT void btSoftBody_Joint_Terminate(btSoftBody_Joint* obj, btScalar dt);
	EXPORT btSoftBody_Joint_eType btSoftBody_Joint_Type(btSoftBody_Joint* obj);
	EXPORT void btSoftBody_Joint_delete(btSoftBody_Joint* obj);

	EXPORT btSoftBody_Link* btSoftBody_Link_new();
	EXPORT btSoftBody_Link* btSoftBody_Link_new2(btSoftBody_Link* obj);
	EXPORT int btSoftBody_Link_getBbending(btSoftBody_Link* obj);
	EXPORT btScalar btSoftBody_Link_getC0(btSoftBody_Link* obj);
	EXPORT btScalar btSoftBody_Link_getC1(btSoftBody_Link* obj);
	EXPORT btScalar btSoftBody_Link_getC2(btSoftBody_Link* obj);
	EXPORT void btSoftBody_Link_getC3(btSoftBody_Link* obj, btVector3* value);
	EXPORT btSoftBody_Node** btSoftBody_Link_getN(btSoftBody_Link* obj);
	EXPORT btScalar btSoftBody_Link_getRl(btSoftBody_Link* obj);
	EXPORT void btSoftBody_Link_setBbending(btSoftBody_Link* obj, int value);
	EXPORT void btSoftBody_Link_setC0(btSoftBody_Link* obj, btScalar value);
	EXPORT void btSoftBody_Link_setC1(btSoftBody_Link* obj, btScalar value);
	EXPORT void btSoftBody_Link_setC2(btSoftBody_Link* obj, btScalar value);
	EXPORT void btSoftBody_Link_setC3(btSoftBody_Link* obj, const btVector3* value);
	EXPORT void btSoftBody_Link_setRl(btSoftBody_Link* obj, btScalar value);
	EXPORT void btSoftBody_Link_delete(btSoftBody_Link* obj);

	EXPORT btSoftBody_LJoint_Specs* btSoftBody_LJoint_Specs_new();
	EXPORT void btSoftBody_LJoint_Specs_getPosition(btSoftBody_LJoint_Specs* obj, btVector3* value);
	EXPORT void btSoftBody_LJoint_Specs_setPosition(btSoftBody_LJoint_Specs* obj, const btVector3* value);

	EXPORT btVector3* btSoftBody_LJoint_getRpos(btSoftBody_LJoint* obj);

	EXPORT int btSoftBody_Material_getFlags(btSoftBody_Material* obj);
	EXPORT btScalar btSoftBody_Material_getKAST(btSoftBody_Material* obj);
	EXPORT btScalar btSoftBody_Material_getKLST(btSoftBody_Material* obj);
	EXPORT btScalar btSoftBody_Material_getKVST(btSoftBody_Material* obj);
	EXPORT void btSoftBody_Material_setFlags(btSoftBody_Material* obj, int value);
	EXPORT void btSoftBody_Material_setKAST(btSoftBody_Material* obj, btScalar value);
	EXPORT void btSoftBody_Material_setKLST(btSoftBody_Material* obj, btScalar value);
	EXPORT void btSoftBody_Material_setKVST(btSoftBody_Material* obj, btScalar value);

	EXPORT btScalar btSoftBody_Node_getArea(btSoftBody_Node* obj);
	EXPORT int btSoftBody_Node_getBattach(btSoftBody_Node* obj);
	EXPORT void btSoftBody_Node_getF(btSoftBody_Node* obj, btVector3* value);
	EXPORT btScalar btSoftBody_Node_getIm(btSoftBody_Node* obj);
	EXPORT int btSoftBody_Node_getIndex(btSoftBody_Node* obj);
	EXPORT btDbvtNode* btSoftBody_Node_getLeaf(btSoftBody_Node* obj);
	EXPORT void btSoftBody_Node_getN(btSoftBody_Node* obj, btVector3* value);
	EXPORT void btSoftBody_Node_getQ(btSoftBody_Node* obj, btVector3* value);
	EXPORT void btSoftBody_Node_getV(btSoftBody_Node* obj, btVector3* value);
	EXPORT void btSoftBody_Node_getVN(btSoftBody_Node* obj, btVector3* value);
	EXPORT void btSoftBody_Node_getX(btSoftBody_Node* obj, btVector3* value);
	EXPORT void btSoftBody_Node_setArea(btSoftBody_Node* obj, btScalar value);
	EXPORT void btSoftBody_Node_setBattach(btSoftBody_Node* obj, int value);
	EXPORT void btSoftBody_Node_setF(btSoftBody_Node* obj, const btVector3* value);
	EXPORT void btSoftBody_Node_setIm(btSoftBody_Node* obj, btScalar value);
	EXPORT void btSoftBody_Node_setIndex(btSoftBody_Node* obj, int value);
	EXPORT void btSoftBody_Node_setLeaf(btSoftBody_Node* obj, btDbvtNode* value);
	EXPORT void btSoftBody_Node_setN(btSoftBody_Node* obj, const btVector3* value);
	EXPORT void btSoftBody_Node_setQ(btSoftBody_Node* obj, const btVector3* value);
	EXPORT void btSoftBody_Node_setV(btSoftBody_Node* obj, const btVector3* value);
	EXPORT void btSoftBody_Node_setVN(btSoftBody_Node* obj, const btVector3* value);
	EXPORT void btSoftBody_Node_setX(btSoftBody_Node* obj, const btVector3* value);

	EXPORT btScalar* btSoftBody_Note_getCoords(btSoftBody_Note* obj);
	EXPORT btSoftBody_Node** btSoftBody_Note_getNodes(btSoftBody_Note* obj);
	EXPORT void btSoftBody_Note_getOffset(btSoftBody_Note* obj, btVector3* value);
	EXPORT int btSoftBody_Note_getRank(btSoftBody_Note* obj);
	EXPORT const char* btSoftBody_Note_getText(btSoftBody_Note* obj);
	EXPORT void btSoftBody_Note_setOffset(btSoftBody_Note* obj, const btVector3* value);
	EXPORT void btSoftBody_Note_setRank(btSoftBody_Note* obj, int value);
	EXPORT void btSoftBody_Note_setText(btSoftBody_Note* obj, const char* value);

	EXPORT void btSoftBody_Pose_getAqq(btSoftBody_Pose* obj, btMatrix3x3* value);
	EXPORT bool btSoftBody_Pose_getBframe(btSoftBody_Pose* obj);
	EXPORT bool btSoftBody_Pose_getBvolume(btSoftBody_Pose* obj);
	EXPORT void btSoftBody_Pose_getCom(btSoftBody_Pose* obj, btVector3* value);
	EXPORT btAlignedObjectArray_btVector3* btSoftBody_Pose_getPos(btSoftBody_Pose* obj);
	EXPORT void btSoftBody_Pose_getRot(btSoftBody_Pose* obj, btMatrix3x3* value);
	EXPORT void btSoftBody_Pose_getScl(btSoftBody_Pose* obj, btMatrix3x3* value);
	EXPORT btAlignedObjectArray_btScalar* btSoftBody_Pose_getWgh(btSoftBody_Pose* obj);
	EXPORT btScalar btSoftBody_Pose_getVolume(btSoftBody_Pose* obj);
	EXPORT void btSoftBody_Pose_setAqq(btSoftBody_Pose* obj, const btMatrix3x3* value);
	EXPORT void btSoftBody_Pose_setBframe(btSoftBody_Pose* obj, bool value);
	EXPORT void btSoftBody_Pose_setBvolume(btSoftBody_Pose* obj, bool value);
	EXPORT void btSoftBody_Pose_setCom(btSoftBody_Pose* obj, const btVector3* value);
	EXPORT void btSoftBody_Pose_setRot(btSoftBody_Pose* obj, const btMatrix3x3* value);
	EXPORT void btSoftBody_Pose_setScl(btSoftBody_Pose* obj, const btMatrix3x3* value);
	EXPORT void btSoftBody_Pose_setVolume(btSoftBody_Pose* obj, btScalar value);

	EXPORT btSoftBody_RayFromToCaster* btSoftBody_RayFromToCaster_new(const btVector3* rayFrom, const btVector3* rayTo, btScalar mxt);
	EXPORT btSoftBody_Face* btSoftBody_RayFromToCaster_getFace(btSoftBody_RayFromToCaster* obj);
	EXPORT btScalar btSoftBody_RayFromToCaster_getMint(btSoftBody_RayFromToCaster* obj);
	EXPORT void btSoftBody_RayFromToCaster_getRayFrom(btSoftBody_RayFromToCaster* obj, btVector3* value);
	EXPORT void btSoftBody_RayFromToCaster_getRayNormalizedDirection(btSoftBody_RayFromToCaster* obj, btVector3* value);
	EXPORT void btSoftBody_RayFromToCaster_getRayTo(btSoftBody_RayFromToCaster* obj, btVector3* value);
	EXPORT int btSoftBody_RayFromToCaster_getTests(btSoftBody_RayFromToCaster* obj);
	EXPORT btScalar btSoftBody_RayFromToCaster_rayFromToTriangle(const btVector3* rayFrom, const btVector3* rayTo, const btVector3* rayNormalizedDirection, const btVector3* a, const btVector3* b, const btVector3* c);
	EXPORT btScalar btSoftBody_RayFromToCaster_rayFromToTriangle2(const btVector3* rayFrom, const btVector3* rayTo, const btVector3* rayNormalizedDirection, const btVector3* a, const btVector3* b, const btVector3* c, btScalar maxt);
	EXPORT void btSoftBody_RayFromToCaster_setFace(btSoftBody_RayFromToCaster* obj, btSoftBody_Face* value);
	EXPORT void btSoftBody_RayFromToCaster_setMint(btSoftBody_RayFromToCaster* obj, btScalar value);
	EXPORT void btSoftBody_RayFromToCaster_setRayFrom(btSoftBody_RayFromToCaster* obj, const btVector3* value);
	EXPORT void btSoftBody_RayFromToCaster_setRayNormalizedDirection(btSoftBody_RayFromToCaster* obj, const btVector3* value);
	EXPORT void btSoftBody_RayFromToCaster_setRayTo(btSoftBody_RayFromToCaster* obj, const btVector3* value);
	EXPORT void btSoftBody_RayFromToCaster_setTests(btSoftBody_RayFromToCaster* obj, int value);

	EXPORT btSoftBody_RContact* btSoftBody_RContact_new();
	EXPORT void btSoftBody_RContact_getC0(btSoftBody_RContact* obj, btMatrix3x3* value);
	EXPORT void btSoftBody_RContact_getC1(btSoftBody_RContact* obj, btVector3* value);
	EXPORT btScalar btSoftBody_RContact_getC2(btSoftBody_RContact* obj);
	EXPORT btScalar btSoftBody_RContact_getC3(btSoftBody_RContact* obj);
	EXPORT btScalar btSoftBody_RContact_getC4(btSoftBody_RContact* obj);
	EXPORT btSoftBody_sCti* btSoftBody_RContact_getCti(btSoftBody_RContact* obj);
	EXPORT btSoftBody_Node* btSoftBody_RContact_getNode(btSoftBody_RContact* obj);
	EXPORT void btSoftBody_RContact_getT1(btSoftBody_RContact* obj, btVector3* value);
	EXPORT void btSoftBody_RContact_getT2(btSoftBody_RContact* obj, btVector3* value);
	EXPORT void btSoftBody_RContact_setC0(btSoftBody_RContact* obj, const btMatrix3x3* value);
	EXPORT void btSoftBody_RContact_setC1(btSoftBody_RContact* obj, const btVector3* value);
	EXPORT void btSoftBody_RContact_setC2(btSoftBody_RContact* obj, btScalar value);
	EXPORT void btSoftBody_RContact_setC3(btSoftBody_RContact* obj, btScalar value);
	EXPORT void btSoftBody_RContact_setC4(btSoftBody_RContact* obj, btScalar value);
	EXPORT void btSoftBody_RContact_setNode(btSoftBody_RContact* obj, btSoftBody_Node* value);
	EXPORT void btSoftBody_RContact_setT1(btSoftBody_RContact* obj, const btVector3* value);
	EXPORT void btSoftBody_RContact_setT2(btSoftBody_RContact* obj, const btVector3* value);
	EXPORT void btSoftBody_RContact_delete(btSoftBody_RContact* obj);

	EXPORT btSoftBody_SContact* btSoftBody_SContact_new();
	EXPORT btScalar* btSoftBody_SContact_getCfm(btSoftBody_SContact* obj);
	EXPORT btSoftBody_Face* btSoftBody_SContact_getFace(btSoftBody_SContact* obj);
	EXPORT btScalar btSoftBody_SContact_getFriction(btSoftBody_SContact* obj);
	EXPORT btScalar btSoftBody_SContact_getMargin(btSoftBody_SContact* obj);
	EXPORT btSoftBody_Node* btSoftBody_SContact_getNode(btSoftBody_SContact* obj);
	EXPORT void btSoftBody_SContact_getNormal(btSoftBody_SContact* obj, btVector3* value);
	EXPORT void btSoftBody_SContact_getWeights(btSoftBody_SContact* obj, btVector3* value);
	EXPORT void btSoftBody_SContact_setFace(btSoftBody_SContact* obj, btSoftBody_Face* value);
	EXPORT void btSoftBody_SContact_setFriction(btSoftBody_SContact* obj, btScalar value);
	EXPORT void btSoftBody_SContact_setMargin(btSoftBody_SContact* obj, btScalar value);
	EXPORT void btSoftBody_SContact_setNode(btSoftBody_SContact* obj, btSoftBody_Node* value);
	EXPORT void btSoftBody_SContact_setNormal(btSoftBody_SContact* obj, const btVector3* value);
	EXPORT void btSoftBody_SContact_setWeights(btSoftBody_SContact* obj, const btVector3* value);
	EXPORT void btSoftBody_SContact_delete(btSoftBody_SContact* obj);

	EXPORT btSoftBody_sCti* btSoftBody_sCti_new();
	EXPORT const btCollisionObject* btSoftBody_sCti_getColObj(btSoftBody_sCti* obj);
	EXPORT void btSoftBody_sCti_getNormal(btSoftBody_sCti* obj, btVector3* value);
	EXPORT btScalar btSoftBody_sCti_getOffset(btSoftBody_sCti* obj);
	EXPORT void btSoftBody_sCti_setColObj(btSoftBody_sCti* obj, const btCollisionObject* value);
	EXPORT void btSoftBody_sCti_setNormal(btSoftBody_sCti* obj, const btVector3* value);
	EXPORT void btSoftBody_sCti_setOffset(btSoftBody_sCti* obj, btScalar value);
	EXPORT void btSoftBody_sCti_delete(btSoftBody_sCti* obj);

	EXPORT btScalar btSoftBody_SolverState_getIsdt(btSoftBody_SolverState* obj);
	EXPORT btScalar btSoftBody_SolverState_getRadmrg(btSoftBody_SolverState* obj);
	EXPORT btScalar btSoftBody_SolverState_getSdt(btSoftBody_SolverState* obj);
	EXPORT btScalar btSoftBody_SolverState_getUpdmrg(btSoftBody_SolverState* obj);
	EXPORT btScalar btSoftBody_SolverState_getVelmrg(btSoftBody_SolverState* obj);
	EXPORT void btSoftBody_SolverState_setIsdt(btSoftBody_SolverState* obj, btScalar value);
	EXPORT void btSoftBody_SolverState_setRadmrg(btSoftBody_SolverState* obj, btScalar value);
	EXPORT void btSoftBody_SolverState_setSdt(btSoftBody_SolverState* obj, btScalar value);
	EXPORT void btSoftBody_SolverState_setUpdmrg(btSoftBody_SolverState* obj, btScalar value);
	EXPORT void btSoftBody_SolverState_setVelmrg(btSoftBody_SolverState* obj, btScalar value);

	EXPORT btSoftBody_sRayCast* btSoftBody_sRayCast_new();
	EXPORT btSoftBody* btSoftBody_sRayCast_getBody(btSoftBody_sRayCast* obj);
	EXPORT btSoftBody_eFeature btSoftBody_sRayCast_getFeature(btSoftBody_sRayCast* obj);
	EXPORT btScalar btSoftBody_sRayCast_getFraction(btSoftBody_sRayCast* obj);
	EXPORT int btSoftBody_sRayCast_getIndex(btSoftBody_sRayCast* obj);
	EXPORT void btSoftBody_sRayCast_setBody(btSoftBody_sRayCast* obj, btSoftBody* value);
	EXPORT void btSoftBody_sRayCast_setFeature(btSoftBody_sRayCast* obj, btSoftBody_eFeature value);
	EXPORT void btSoftBody_sRayCast_setFraction(btSoftBody_sRayCast* obj, btScalar value);
	EXPORT void btSoftBody_sRayCast_setIndex(btSoftBody_sRayCast* obj, int value);
	EXPORT void btSoftBody_sRayCast_delete(btSoftBody_sRayCast* obj);

	EXPORT btVector3* btSoftBody_Tetra_getC0(btSoftBody_Tetra* obj);
	EXPORT btScalar btSoftBody_Tetra_getC1(btSoftBody_Tetra* obj);
	EXPORT btScalar btSoftBody_Tetra_getC2(btSoftBody_Tetra* obj);
	EXPORT btDbvtNode* btSoftBody_Tetra_getLeaf(btSoftBody_Tetra* obj);
	EXPORT btSoftBody_Node** btSoftBody_Tetra_getN(btSoftBody_Tetra* obj);
	EXPORT btScalar btSoftBody_Tetra_getRv(btSoftBody_Tetra* obj);
	EXPORT void btSoftBody_Tetra_setC1(btSoftBody_Tetra* obj, btScalar value);
	EXPORT void btSoftBody_Tetra_setC2(btSoftBody_Tetra* obj, btScalar value);
	EXPORT void btSoftBody_Tetra_setLeaf(btSoftBody_Tetra* obj, btDbvtNode* value);
	EXPORT void btSoftBody_Tetra_setRv(btSoftBody_Tetra* obj, btScalar value);

	EXPORT btSoftBody* btSoftBody_new(btSoftBodyWorldInfo* worldInfo, int node_count, const btScalar* x, const btScalar* m);
	EXPORT btSoftBody* btSoftBody_new2(btSoftBodyWorldInfo* worldInfo);
	EXPORT void btSoftBody_addAeroForceToFace(btSoftBody* obj, const btVector3* windVelocity, int faceIndex);
	EXPORT void btSoftBody_addAeroForceToNode(btSoftBody* obj, const btVector3* windVelocity, int nodeIndex);
	EXPORT void btSoftBody_addForce(btSoftBody* obj, const btVector3* force);
	EXPORT void btSoftBody_addForce2(btSoftBody* obj, const btVector3* force, int node);
	EXPORT void btSoftBody_addVelocity(btSoftBody* obj, const btVector3* velocity);
	EXPORT void btSoftBody_addVelocity2(btSoftBody* obj, const btVector3* velocity, int node);
	EXPORT void btSoftBody_appendAnchor(btSoftBody* obj, int node, btRigidBody* body, const btVector3* localPivot, bool disableCollisionBetweenLinkedBodies, btScalar influence);
	EXPORT void btSoftBody_appendAnchor2(btSoftBody* obj, int node, btRigidBody* body, bool disableCollisionBetweenLinkedBodies, btScalar influence);
	EXPORT void btSoftBody_appendAngularJoint(btSoftBody* obj, const btSoftBody_AJoint_Specs* specs);
	EXPORT void btSoftBody_appendAngularJoint2(btSoftBody* obj, const btSoftBody_AJoint_Specs* specs, btSoftBody_Body* body);
	EXPORT void btSoftBody_appendAngularJoint3(btSoftBody* obj, const btSoftBody_AJoint_Specs* specs, btSoftBody* body);
	EXPORT void btSoftBody_appendAngularJoint4(btSoftBody* obj, const btSoftBody_AJoint_Specs* specs, btSoftBody_Cluster* body0, btSoftBody_Body* body1);
	EXPORT void btSoftBody_appendFace(btSoftBody* obj, int model, btSoftBody_Material* mat);
	EXPORT void btSoftBody_appendFace2(btSoftBody* obj, int node0, int node1, int node2, btSoftBody_Material* mat);
	EXPORT void btSoftBody_appendLinearJoint(btSoftBody* obj, const btSoftBody_LJoint_Specs* specs, btSoftBody* body);
	EXPORT void btSoftBody_appendLinearJoint2(btSoftBody* obj, const btSoftBody_LJoint_Specs* specs);
	EXPORT void btSoftBody_appendLinearJoint3(btSoftBody* obj, const btSoftBody_LJoint_Specs* specs, btSoftBody_Body* body);
	EXPORT void btSoftBody_appendLinearJoint4(btSoftBody* obj, const btSoftBody_LJoint_Specs* specs, btSoftBody_Cluster* body0, btSoftBody_Body* body1);
	EXPORT void btSoftBody_appendLink(btSoftBody* obj, int node0, int node1, btSoftBody_Material* mat, bool bcheckexist);
	EXPORT void btSoftBody_appendLink2(btSoftBody* obj, int model, btSoftBody_Material* mat);
	EXPORT void btSoftBody_appendLink3(btSoftBody* obj, btSoftBody_Node* node0, btSoftBody_Node* node1, btSoftBody_Material* mat, bool bcheckexist);
	EXPORT btSoftBody_Material* btSoftBody_appendMaterial(btSoftBody* obj);
	EXPORT void btSoftBody_appendNode(btSoftBody* obj, const btVector3* x, btScalar m);
	EXPORT void btSoftBody_appendNote(btSoftBody* obj, const char* text, const btVector3* o, btSoftBody_Face* feature);
	EXPORT void btSoftBody_appendNote2(btSoftBody* obj, const char* text, const btVector3* o, btSoftBody_Link* feature);
	EXPORT void btSoftBody_appendNote3(btSoftBody* obj, const char* text, const btVector3* o, btSoftBody_Node* feature);
	EXPORT void btSoftBody_appendNote4(btSoftBody* obj, const char* text, const btVector3* o);
	EXPORT void btSoftBody_appendNote5(btSoftBody* obj, const char* text, const btVector3* o, const btVector4* c, btSoftBody_Node* n0, btSoftBody_Node* n1, btSoftBody_Node* n2, btSoftBody_Node* n3);
	EXPORT void btSoftBody_appendTetra(btSoftBody* obj, int model, btSoftBody_Material* mat);
	EXPORT void btSoftBody_appendTetra2(btSoftBody* obj, int node0, int node1, int node2, int node3, btSoftBody_Material* mat);
	EXPORT void btSoftBody_applyClusters(btSoftBody* obj, bool drift);
	EXPORT void btSoftBody_applyForces(btSoftBody* obj);
	EXPORT bool btSoftBody_checkContact(btSoftBody* obj, const btCollisionObjectWrapper* colObjWrap, const btVector3* x, btScalar margin, btSoftBody_sCti* cti);
	EXPORT bool btSoftBody_checkDeformableContact(btSoftBody* obj, const btCollisionObjectWrapper* colObjWrap, const btVector3* x, btScalar margin, btSoftBody_sCti* cti, bool predict);
	EXPORT bool btSoftBody_checkFace(btSoftBody* obj, int node0, int node1, int node2);
	EXPORT bool btSoftBody_checkLink(btSoftBody* obj, const btSoftBody_Node* node0, const btSoftBody_Node* node1);
	EXPORT bool btSoftBody_checkLink2(btSoftBody* obj, int node0, int node1);
	EXPORT void btSoftBody_cleanupClusters(btSoftBody* obj);
	EXPORT void btSoftBody_clusterAImpulse(btSoftBody_Cluster* cluster, const btSoftBody_Impulse* impulse);
	EXPORT void btSoftBody_clusterCom(btSoftBody* obj, int cluster, btVector3* value);
	EXPORT void btSoftBody_clusterCom2(const btSoftBody_Cluster* cluster, btVector3* value);
	EXPORT int btSoftBody_clusterCount(btSoftBody* obj);
	EXPORT void btSoftBody_clusterDAImpulse(btSoftBody_Cluster* cluster, const btVector3* impulse);
	EXPORT void btSoftBody_clusterDCImpulse(btSoftBody_Cluster* cluster, const btVector3* impulse);
	EXPORT void btSoftBody_clusterDImpulse(btSoftBody_Cluster* cluster, const btVector3* rpos, const btVector3* impulse);
	EXPORT void btSoftBody_clusterImpulse(btSoftBody_Cluster* cluster, const btVector3* rpos, const btSoftBody_Impulse* impulse);
	EXPORT void btSoftBody_clusterVAImpulse(btSoftBody_Cluster* cluster, const btVector3* impulse);
	EXPORT void btSoftBody_clusterVelocity(const btSoftBody_Cluster* cluster, const btVector3* rpos, btVector3* value);
	EXPORT void btSoftBody_clusterVImpulse(btSoftBody_Cluster* cluster, const btVector3* rpos, const btVector3* impulse);
	EXPORT bool btSoftBody_cutLink(btSoftBody* obj, const btSoftBody_Node* node0, const btSoftBody_Node* node1, btScalar position);
	EXPORT bool btSoftBody_cutLink2(btSoftBody* obj, int node0, int node1, btScalar position);
	EXPORT void btSoftBody_dampClusters(btSoftBody* obj);
	EXPORT void btSoftBody_defaultCollisionHandler(btSoftBody* obj, const btCollisionObjectWrapper* pcoWrap);
	EXPORT void btSoftBody_defaultCollisionHandler2(btSoftBody* obj, btSoftBody* psb);
	EXPORT void btSoftBody_evaluateCom(btSoftBody* obj, btVector3* value);
	EXPORT int btSoftBody_generateBendingConstraints(btSoftBody* obj, int distance, btSoftBody_Material* mat);
	EXPORT int btSoftBody_generateClusters(btSoftBody* obj, int k);
	EXPORT int btSoftBody_generateClusters2(btSoftBody* obj, int k, int maxiterations);
	EXPORT void btSoftBody_getAabb(btSoftBody* obj, btVector3* aabbMin, btVector3* aabbMax);
	EXPORT btAlignedObjectArray_btSoftBody_Anchor* btSoftBody_getAnchors(btSoftBody* obj);
	EXPORT btVector3* btSoftBody_getBounds(btSoftBody* obj);
	EXPORT bool btSoftBody_getBUpdateRtCst(btSoftBody* obj);
	EXPORT btDbvt* btSoftBody_getCdbvt(btSoftBody* obj);
	EXPORT btSoftBody_Config* btSoftBody_getCfg(btSoftBody* obj);
	EXPORT btAlignedObjectArray_bool* btSoftBody_getClusterConnectivity(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_ClusterPtr* btSoftBody_getClusters(btSoftBody* obj);
	EXPORT btAlignedObjectArray_const_btCollisionObjectPtr* btSoftBody_getCollisionDisabledObjects(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_Face* btSoftBody_getFaces(btSoftBody* obj);
	EXPORT btDbvt* btSoftBody_getFdbvt(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_JointPtr* btSoftBody_getJoints(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_Link* btSoftBody_getLinks(btSoftBody* obj);
	EXPORT btScalar btSoftBody_getMass(btSoftBody* obj, int node);
	EXPORT btAlignedObjectArray_btSoftBody_MaterialPtr* btSoftBody_getMaterials(btSoftBody* obj);
	EXPORT btDbvt* btSoftBody_getNdbvt(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_Node* btSoftBody_getNodes(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_Note* btSoftBody_getNotes(btSoftBody* obj);
	EXPORT btSoftBody_Pose* btSoftBody_getPose(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_RContact* btSoftBody_getRcontacts(btSoftBody* obj);
	EXPORT btScalar btSoftBody_getRestLengthScale(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_SContact* btSoftBody_getScontacts(btSoftBody* obj);
	EXPORT btSoftBodySolver* btSoftBody_getSoftBodySolver(btSoftBody* obj);
	//EXPORT btSoftBody_psolver_t btSoftBody_getSolver(btSoftBody_ePSolver solver);
	//EXPORT btSoftBody_vsolver_t btSoftBody_getSolver2(btSoftBody_eVSolver solver);
	EXPORT btSoftBody_SolverState* btSoftBody_getSst(btSoftBody* obj);
	EXPORT void* btSoftBody_getTag(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_Tetra* btSoftBody_getTetras(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_TetraScratch* btSoftBody_getTetraScratches(btSoftBody* obj);
	EXPORT btAlignedObjectArray_btSoftBody_TetraScratch* btSoftBody_getTetraScratchesTn(btSoftBody* obj);
	EXPORT btScalar btSoftBody_getTimeacc(btSoftBody* obj);
	EXPORT btScalar btSoftBody_getTotalMass(btSoftBody* obj);
	EXPORT btAlignedObjectArray_int* btSoftBody_getUserIndexMapping(btSoftBody* obj);
	EXPORT void btSoftBody_getWindVelocity(btSoftBody* obj, btVector3* velocity);
	EXPORT btScalar btSoftBody_getVolume(btSoftBody* obj);
	EXPORT btSoftBodyWorldInfo* btSoftBody_getWorldInfo(btSoftBody* obj);
	EXPORT void btSoftBody_indicesToPointers(btSoftBody* obj, const int* map);
	EXPORT void btSoftBody_initDefaults(btSoftBody* obj);
	EXPORT void btSoftBody_initializeClusters(btSoftBody* obj);
	EXPORT void btSoftBody_initializeFaceTree(btSoftBody* obj);
	EXPORT void btSoftBody_initializeDmInverse(btSoftBody* obj);
	EXPORT void btSoftBody_integrateMotion(btSoftBody* obj);
	EXPORT void btSoftBody_pointersToIndices(btSoftBody* obj);
	EXPORT void btSoftBody_predictMotion(btSoftBody* obj, btScalar dt);
	EXPORT void btSoftBody_prepareClusters(btSoftBody* obj, int iterations);
	EXPORT void btSoftBody_PSolve_Anchors(btSoftBody* psb, btScalar kst, btScalar ti);
	EXPORT void btSoftBody_PSolve_Links(btSoftBody* psb, btScalar kst, btScalar ti);
	EXPORT void btSoftBody_PSolve_RContacts(btSoftBody* psb, btScalar kst, btScalar ti);
	EXPORT void btSoftBody_PSolve_SContacts(btSoftBody* psb, btScalar __unnamed1, btScalar ti);
	EXPORT void btSoftBody_randomizeConstraints(btSoftBody* obj);
	EXPORT bool btSoftBody_rayTest(btSoftBody* obj, const btVector3* rayFrom, const btVector3* rayTo, btSoftBody_sRayCast* results);
	EXPORT int btSoftBody_rayTest2(btSoftBody* obj, const btVector3* rayFrom, const btVector3* rayTo, btScalar* mint, btSoftBody_eFeature* feature, int* index, bool bcountonly);
	EXPORT void btSoftBody_refine(btSoftBody* obj, btSoftBody_ImplicitFn* ifn, btScalar accurary, bool cut);
	EXPORT void btSoftBody_releaseCluster(btSoftBody* obj, int index);
	EXPORT void btSoftBody_releaseClusters(btSoftBody* obj);
	EXPORT void btSoftBody_resetLinkRestLengths(btSoftBody* obj);
	EXPORT void btSoftBody_rotate(btSoftBody* obj, const btQuaternion* rot);
	EXPORT void btSoftBody_scale(btSoftBody* obj, const btVector3* scl);
	EXPORT void btSoftBody_setBUpdateRtCst(btSoftBody* obj, bool value);
	EXPORT void btSoftBody_setMass(btSoftBody* obj, int node, btScalar mass);
	EXPORT void btSoftBody_setPose(btSoftBody* obj, bool bvolume, bool bframe);
	EXPORT void btSoftBody_setRestLengthScale(btSoftBody* obj, btScalar restLength);
	EXPORT void btSoftBody_setSelfCollision(btSoftBody* obj, bool useSelfCollision);
	EXPORT void btSoftBody_setSoftBodySolver(btSoftBody* obj, btSoftBodySolver* softBodySolver);
	EXPORT void btSoftBody_setSolver(btSoftBody* obj, btSoftBody_eSolverPresets preset);
	EXPORT void btSoftBody_setTag(btSoftBody* obj, void* value);
	EXPORT void btSoftBody_setTimeacc(btSoftBody* obj, btScalar value);
	EXPORT void btSoftBody_setTotalDensity(btSoftBody* obj, btScalar density);
	EXPORT void btSoftBody_setTotalMass(btSoftBody* obj, btScalar mass, bool fromfaces);
	EXPORT void btSoftBody_setVelocity(btSoftBody* obj, const btVector3* velocity);
	EXPORT void btSoftBody_setWindVelocity(btSoftBody* obj, const btVector3* velocity);
	EXPORT void btSoftBody_setVolumeDensity(btSoftBody* obj, btScalar density);
	EXPORT void btSoftBody_setVolumeMass(btSoftBody* obj, btScalar mass);
	EXPORT void btSoftBody_setWorldInfo(btSoftBody* obj, btSoftBodyWorldInfo* value);
	EXPORT void btSoftBody_solveClusters(const btAlignedObjectArray_btSoftBodyPtr* bodies);
	EXPORT void btSoftBody_solveClusters2(btSoftBody* obj, btScalar sor);
	EXPORT void btSoftBody_solveCommonConstraints(btSoftBody** bodies, int count, int iterations);
	EXPORT void btSoftBody_solveConstraints(btSoftBody* obj);
	EXPORT void btSoftBody_staticSolve(btSoftBody* obj, int iterations);
	EXPORT void btSoftBody_transform(btSoftBody* obj, const btTransform* trs);
	EXPORT void btSoftBody_translate(btSoftBody* obj, const btVector3* trs);
	EXPORT btSoftBody* btSoftBody_upcast(btCollisionObject* colObj);
	EXPORT void btSoftBody_updateArea(btSoftBody* obj, bool averageArea);
	EXPORT void btSoftBody_updateBounds(btSoftBody* obj);
	EXPORT void btSoftBody_updateClusters(btSoftBody* obj);
	EXPORT void btSoftBody_updateConstants(btSoftBody* obj);
	EXPORT void btSoftBody_updateLinkConstants(btSoftBody* obj);
	EXPORT void btSoftBody_updateNormals(btSoftBody* obj);
	EXPORT void btSoftBody_updatePose(btSoftBody* obj);
	EXPORT bool btSoftBody_useSelfCollision(btSoftBody* obj);
	EXPORT void btSoftBody_VSolve_Links(btSoftBody* psb, btScalar kst);

	EXPORT int btSoftBody_getFaceVertexData(btSoftBody* obj, btScalar* vertices);
	EXPORT int btSoftBody_getFaceVertexNormalData(btSoftBody* obj, btScalar* vertices);
	EXPORT int btSoftBody_getFaceVertexNormalData2(btSoftBody* obj, btScalar* vertices, btScalar* normals);
	EXPORT int btSoftBody_getLinkVertexData(btSoftBody* obj, btScalar* vertices);
	EXPORT int btSoftBody_getLinkVertexNormalData(btSoftBody* obj, btScalar* vertices);
	EXPORT int btSoftBody_getTetraVertexData(btSoftBody* obj, btScalar* vertices);
	EXPORT int btSoftBody_getTetraVertexNormalData(btSoftBody* obj, btScalar* vertices);
	EXPORT int btSoftBody_getTetraVertexNormalData2(btSoftBody* obj, btScalar* vertices, btScalar* normals);
#ifdef __cplusplus
}
#endif
