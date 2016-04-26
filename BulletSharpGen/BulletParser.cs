using ClangSharp;
using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    class BulletParser : DotNetParser
    {
        public BulletParser(WrapperProject project)
            : base(project)
        {
            // Classes that shouldn't be instantiated by users
            var hidePublicConstructors = new HashSet<string> {
                "btActivatingCollisionAlgorithm", "btBroadphaseProxy", "btContactConstraint",
                "btDbvtProxy", "btDispatcherInfo",
                "btUsageBitfield", "btSoftBody::Anchor", "btSoftBody::Config", "btSoftBody::Cluster",
                "btSoftBody::Face", "btSoftBody::Tetra", "btSoftBody::Element", "btSoftBody::Feature",
                "btSoftBody::Link", "btSoftBody::Material", "btSoftBody::Node", "btSoftBody::Note",
                "btSoftBody::Pose", "btSoftBody::SolverState", "btSoftBody::Joint::Specs",
                "btSoftBody::AJoint", "btSoftBody::CJoint", "btSoftBody::LJoint", "btSparseSdf",
                "btCompoundShapeChild", "btMultibodyLink"
            };

            // Classes for which no internal constructor is needed
            var hideInternalConstructor = new HashSet<string> {
                "btAxisSweep3", "bt32BitAxisSweep3",
                "btBoxBoxDetector", "btBroadphaseRayCallback", "btCollisionAlgorithmConstructionInfo", "btDefaultCollisionConstructionInfo",
                "btContinuousConvexCollision",
                "btDefaultMotionState", "btRigidBody", "btDiscreteCollisionDetectorInterface::ClosestPointInput",
                "btGjkConvexCast", "btGjkEpaPenetrationDepthSolver",
                "btMinkowskiPenetrationDepthSolver", "btPointCollector",
                "btMultiBodyDynamicsWorld",
                "btDefaultVehicleRaycaster", "btRaycastVehicle", "btDefaultSerializer",
                "btSoftBodyRigidBodyCollisionConfiguration", "btCPUVertexBufferDescriptor",
                "btSoftRigidDynamicsWorld",
                "SpuGatheringCollisionDispatcher", "btConvexSeparatingDistanceUtil",
                "btVehicleRaycaster::btVehicleRaycasterResult", "btOverlapCallback",
                "btRaycastVehicle::btVehicleTuning",
                "btBox2dShape", "btBoxShape", "btCapsuleShapeX", "btCapsuleShapeZ",
                "btCylinderShapeX", "btCylinderShapeZ",
                "btConeShapeX", "btConeShapeZ", "btConvex2dShape", "btConvexHullShape",
                "btConvexPointCloudShape", "btEmptyShape", "btHeightfieldTerrainShape", "btMinkowskiSumShape",
                "btMultiSphereShape", "btMultimaterialTriangleMeshShape", "btScaledBvhTriangleMeshShape",
                "btSphereShape", "btStaticPlaneShape", "btUniformScalingShape",
                "btCollisionWorld::ConvexResultCallback", "btCollisionWorld::ClosestConvexResultCallback",
                "HACD", "btRigidBody::btRigidBodyConstructionInfo",
                "btSoftBody::ImplicitFn", "btTriangleBuffer", "btMaterialProperties",
                "btCollisionWorld::AllHitsRayResultCallback", "btCollisionWorld::ContactResultCallback",
                "btCollisionWorld::ClosestRayResultCallback", "btCollisionWorld::RayResultCallback",
                "btJointFeedback", "btTypedConstraint::btConstraintInfo1", "btTypedConstraint::btConstraintInfo2",
                "btConeTwistConstraint", "btFixedConstraint", "btGearConstraint",
                "btGeneric6DofSpringConstraint", "btHinge2Constraint",
                "btHingeAccumulatedAngleConstraint", "btPoint2PointConstraint",
                "btSliderConstraint", "btUniversalConstraint",
                "btMLCPSolver", "btMultiBodyConstraintSolver", "btNNCGConstraintSolver",
                "btPairCachingGhostObject", "btSortedOverlappingPairCache", "btNullPairCache",
                "btDbvtBroadphase",
                "btShapeHull", "btSoftBody::sRayCast", "btSoftBody::AJoint::Specs", "btSoftBody::LJoint::Specs",
                "btCompoundShape" // constructor needed for CompoundFromGImpact in C++/CLI, but not C#
            };

            // Classes that have OnDisposing/OnDisposed events
            var trackingDisposable = new HashSet<string> {
                "btCollisionObject", "btCollisionShape",
                "btDbvt", "btRaycastVehicle", "btTypedConstraint"};

            // Apply class properties
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                if (hidePublicConstructors.Contains(@class.FullyQualifiedName))
                {
                    @class.HidePublicConstructors = true;
                }

                if (hideInternalConstructor.Contains(@class.FullyQualifiedName))
                {
                    @class.NoInternalConstructor = true;
                }
                if (trackingDisposable.Contains(@class.FullyQualifiedName))
                {
                    @class.IsTrackingDisposable = true;
                }
            }
        }

        public static string GetTypeMarshalPrologueCppCli(ManagedParameter parameter)
        {
            switch (parameter.Native.Type.Name)
            {
                case "btMatrix3x3":
                    return "MATRIX3X3_CONV(" + parameter.Name + ");";
                case "btQuaternion":
                    return "QUATERNION_CONV(" + parameter.Name + ");";
                case "btTransform":
                    return "TRANSFORM_CONV(" + parameter.Name + ");";
                case "btVector4":
                    return "VECTOR4_CONV(" + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalEpilogueCppCli(ManagedParameter parameter)
        {
            switch (parameter.Native.Type.Name)
            {
                case "btQuaternion":
                    return "QUATERNION_DEL(" + parameter.Name + ");";
                case "btMatrix3x3":
                    return "MATRIX3X3_DEL(" + parameter.Name + ");";
                case "btTransform":
                    return "TRANSFORM_DEL(" + parameter.Name + ");";
                case "btVector4":
                    return "VECTOR4_DEL(" + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalCppCli(ManagedParameter parameter)
        {
            switch (parameter.Native.Type.Name)
            {
                case "btIDebugDraw":
                    return "DebugDraw::GetUnmanaged(" + parameter.Name + ")";
                case "btQuaternion":
                    return "QUATERNION_USE(" + parameter.Name + ")";
                case "btTransform":
                    return "TRANSFORM_USE(" + parameter.Name + ")";
                case "btMatrix3x3":
                    return "MATRIX3X3_USE(" + parameter.Name + ")";
                case "btVector4":
                    return "VECTOR4_USE(" + parameter.Name + ")";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalConstructorStart(MethodDefinition getter)
        {
            switch (getter.ReturnType.Name)
            {
                case "btCollisionShape":
                    return "CollisionShape::GetManaged(";
                case "btIDebugDraw":
                    return "DebugDraw::GetManaged(";
                case "btOverlappingPairCache":
                    return "OverlappingPairCache::GetManaged(";
                case "btQuaternion":
                    return "Math::BtQuatToQuaternion(&";
                case "btTransform":
                    return "Math::BtTransformToMatrix(&";
                case "btVector4":
                    return "Math::BtVector4ToVector4(&";
                default:
                    return string.Empty;
            }
        }

        public static string GetTypeMarshalConstructorEnd(MethodDefinition getter)
        {
            switch (getter.ReturnType.Name)
            {
                case "btCollisionShape":
                case "btIDebugDraw":
                case "btOverlappingPairCache":
                case "btQuaternion":
                case "btTransform":
                case "btVector4":
                    return ")";
                default:
                    return string.Empty;
            }
        }

        public static string GetTypeMarshalConstructorStartCS(MethodDefinition method)
        {
            switch (method.ReturnType.Name)
            {
                case "btBroadphaseProxy":
                    return "BroadphaseProxy.GetManaged(";
                case "btCollisionObject":
                    return "CollisionObject.GetManaged(";
                case "btCollisionShape":
                    return "CollisionShape.GetManaged(";
                case "btOverlappingPairCache":
                    return "OverlappingPairCache.GetManaged(";
                case "IDebugDraw":
                    return "DebugDraw.GetManaged(";
                case "btCollisionObjectWrapper":
                    return $"new CollisionObjectWrapper(";
                default:
                    return "";
            }
        }

        public static string GetTypeMarshalConstructorEndCS(MethodDefinition method)
        {
            if (GetTypeMarshalConstructorStartCS(method) != "")
            {
                return ")";
            }
            return "";
        }

        public static string GetTypeMarshalFieldSetCppCli(FieldDefinition field, ManagedParameter parameter, string nativePointer)
        {
            switch (field.Type.Name)
            {
                case "btQuaternion":
                    return "Math::QuaternionToBtQuat(" + parameter.Name + ", &" + nativePointer + "->" + field.Name + ')';
                case "btTransform":
                    return "Math::MatrixToBtTransform(" + parameter.Name + ", &" + nativePointer + "->" + field.Name + ')';
                case "btVector4":
                    return "Math::Vector4ToBtVector4(" + parameter.Name + ", &" + nativePointer + "->" + field.Name + ')';
                default:
                    return null;
            }
        }

        protected override bool IsExcludedClass(ClassDefinition cl)
        {
            if (cl.Name.StartsWith("b3"))
            {
                return true;
            }

            // Exclude all "FloatData/DoubleData" serialization classes
            return (cl.Name.EndsWith("Data")
                || cl.Name.EndsWith("Data2"))
                && !cl.Name.Equals("btContactSolverInfoData");
        }
    }
}
