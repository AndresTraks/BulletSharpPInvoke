using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    class BulletParser : DefaultParser
    {
        Dictionary<string, ClassDefinition> classDefinitions = new Dictionary<string, ClassDefinition>();

        public BulletParser(WrapperProject project)
            : base(project)
        {
            classDefinitions = project.ClassDefinitions;

            // Classes that shouldn't be instantiated by users
            HashSet<string> hidePublicConstructors = new HashSet<string>() {
                "btActivatingCollisionAlgorithm", "btContactConstraint", "btConvexInternalShape",
                "btConvexInternalAabbCachingShape", "btPolyhedralConvexAabbCachingShape", "btTypedObject",
                "btDbvtProxy", "btSimpleBroadphaseProxy", "btDispatcherInfo", "btTriangleMeshShape",
                "btUsageBitfield", "btSoftBody::Anchor", "btSoftBody::Config", "btSoftBody::Cluster",
                "btSoftBody::Face", "btSoftBody::Tetra", "btSoftBody::Element", "btSoftBody::Feature",
                "btSoftBody::Link", "btSoftBody::Material", "btSoftBody::Node", "btSoftBody::Note",
                "btSoftBody::Pose", "btSoftBody::SolverState", "btSoftBody::Joint::Specs",
                "btSoftBody::AJoint", "btSoftBody::CJoint", "btSoftBody::LJoint", "btSparseSdf",
                "btCompoundShapeChild", "btMultibodyLink"
            };

            // Classes for which no internal constructor is needed
            HashSet<string> hideInternalConstructor = new HashSet<string>() {
                "btBox2dBox2dCollisionAlgorithm", "btBoxBoxCollisionAlgorithm",
                "btBoxBoxDetector", "btBroadphaseRayCallback", "btCollisionAlgorithmConstructionInfo", "btDefaultCollisionConstructionInfo",
                "btCompoundCompoundCollisionAlgorithm", "btContinuousConvexCollision", "btConvex2dConvex2dAlgorithm",
                "btConvexConcaveCollisionAlgorithm", "btConvexConvexAlgorithm",
                "btDefaultMotionState", "btRigidBody", "btDiscreteCollisionDetectorInterface::ClosestPointInput",
                "btEmptyAlgorithm", "btGjkConvexCast", "btGjkEpaPenetrationDepthSolver",
                "btMinkowskiPenetrationDepthSolver", "btPointCollector",
                "btMultiBodyDynamicsWorld",
                "btDefaultVehicleRaycaster", "btRaycastVehicle", "btDefaultSerializer",
                "btSoftBodyRigidBodyCollisionConfiguration", "btCPUVertexBufferDescriptor",
                "btSoftRigidDynamicsWorld",
                "btSphereBoxCollisionAlgorithm",
                "btSphereTriangleCollisionAlgorithm",
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
                "btDbvtBroadphase", "btSimpleBroadphase",
                "btShapeHull", "btSoftBody::sRayCast", "btSoftBody::AJoint::Specs", "btSoftBody::LJoint::Specs",
                "btCompoundShape" // constructor needed for CompoundFromGImpact in C++/CLI, but not C#
            };

            // Classes that might be cleaned up by Bullet and not us (use preventDelete to indicate this)
            HashSet<string> preventDelete = new HashSet<string>() {
                "btAABB", "btCollisionAlgorithmCreateFunc",
                "btCollisionObject", "btCollisionObjectWrapper", "btCollisionShape",
                "btCollisionWorld::LocalConvexResult", "btCollisionWorld::LocalRayResult",
                "btConstraintSolver", "btContactSolverInfoData", "btDbvt",
                "btOverlappingPairCache", "btPoolAllocator",
                "btRotationalLimitMotor", "btTranslationalLimitMotor",
                "btRotationalLimitMotor2", "btTranslationalLimitMotor2",
                "btConstraintSetting", "btSimulationIslandManager",
                "btSolve2LinearConstraint", "btIndexedMesh", "btTriangleInfoMap",
                "btAngularLimit", "btContactSolverInfo",
                "btWheelInfo", "btManifoldPoint", "btCollisionWorld::LocalShapeInfo"};

            // Classes that have OnDisposing/OnDisposed events
            HashSet<string> trackingDisposable = new HashSet<string>() {
                "btCollisionObject", "btCollisionShape",
                "btDbvt", "btRaycastVehicle", "btTypedConstraint"};

            // Apply class properties
            foreach (ClassDefinition @class in classDefinitions.Values)
            {
                if (hidePublicConstructors.Contains(@class.FullyQualifiedName))
                {
                    @class.HidePublicConstructors = true;
                }

                if (hideInternalConstructor.Contains(@class.FullyQualifiedName))
                {
                    @class.NoInternalConstructor = true;
                }
                if (preventDelete.Contains(@class.FullyQualifiedName))
                {
                    @class.HasPreventDelete = true;
                }
                if (trackingDisposable.Contains(@class.FullyQualifiedName))
                {
                    @class.IsTrackingDisposable = true;
                }
            }
        }

        public override void Parse()
        {
            base.Parse();

            // Check if any property values can be cached in
            // in constructors or property setters
            foreach (var @class in classDefinitions.Values)
            {
                foreach (var constructor in @class.Methods.Where(m => m.IsConstructor))
                {
                    foreach (var param in constructor.Parameters)
                    {
                        var methodParent = constructor.Parent;
                        while (methodParent != null)
                        {
                            foreach (var property in methodParent.Properties)
                            {
                                if (param.ManagedName.ToLower() == property.Name.ToLower()
                                    && IsCacheableType(param.Type)
                                    && param.Type.ManagedName == property.Type.ManagedName)
                                {
                                    CachedProperty cachedProperty;
                                    if (methodParent.CachedProperties.TryGetValue(property.Name, out cachedProperty))
                                    {
                                        if (methodParent != constructor.Parent)
                                        {
                                            cachedProperty.Access = RefAccessSpecifier.Protected;
                                        }
                                    }
                                    else
                                    {
                                        cachedProperty = new CachedProperty(property);
                                        methodParent.CachedProperties.Add(property.Name, cachedProperty);
                                    }
                                }
                            }
                            methodParent = methodParent.BaseClass;
                        }
                    }
                }

                foreach (var property in @class.Properties.Where(p => p.Setter != null))
                {
                    if (IsCacheableType(property.Type))
                    {
                        if (!@class.CachedProperties.ContainsKey(property.Name))
                        {
                            @class.CachedProperties.Add(property.Name, new CachedProperty(property));
                        }
                    }
                }
            }

            // Sort methods and properties alphabetically
            foreach (var @class in classDefinitions.Values)
            {
                // Order by name, then fix inheritance, parent classes must appear first
                @class.Classes.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                var classesOrdered = @class.Classes;
                for (int i = 0; i < classesOrdered.Count; i++)
                {
                    var thisClass = classesOrdered[i];
                    var baseClass = thisClass.BaseClass;
                    if (baseClass != null && classesOrdered.Contains(baseClass))
                    {
                        int thisIndex = classesOrdered.IndexOf(thisClass);
                        if (thisIndex < classesOrdered.IndexOf(baseClass))
                        {
                            classesOrdered.Remove(baseClass);
                            classesOrdered.Insert(thisIndex, baseClass);
                        }
                    }
                }

                @class.Methods.Sort((m1, m2) => m1.Name.CompareTo(m2.Name));
                @class.Properties.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
            }
        }

        bool IsCacheableType(TypeRefDefinition t)
        {
            if (t.IsBasic)
            {
                return false;
            }
            if (t.Target != null)
            {
                if (t.Target.IsPureEnum)
                {
                    return false;
                }
            }
            if (t.Referenced != null)
            {
                return IsCacheableType(t.Referenced);
            }
            switch (t.Name)
            {
                case "btTransform":
                case "btQuaternion":
                case "btVector3":
                case "btVector4":
                    return false;
            }
            return true;
        }

        public static string GetTypeName(TypeRefDefinition type)
        {
            switch (type.ManagedName)
            {
                case "Matrix3x3":
                case "Quaternion":
                case "Transform":
                case "Vector3":
                case "Vector4":
                    return "btScalar";
                default:
                    return type.FullName;
            }
        }

        public static string GetTypeNameCS(TypeRefDefinition type)
        {
            switch (type.ManagedName)
            {
                case "Matrix3x3":
                case "Transform":
                    return "Matrix";
            }

            if (type.IsConstantArray)
            {
                switch (type.Referenced.Name)
                {
                    case "bool":
                        return "BoolArray";
                    case "int":
                        return "IntArray";
                    case "unsigned int":
                        return "UIntArray";
                    case "unsigned short":
                        return "UShortArray";
                    case "btScalar":
                        return "FloatArray";
                    case "btVector3":
                        return "Vector3Array";
                    case "btDbvt":
                        return "DbvtArray";
                    case "btSoftBody::Body":
                        return "BodyArray";
                }

                if (type.Referenced.Referenced != null)
                {
                    switch (type.Referenced.Referenced.Name)
                    {
                        case "btDbvtNode":
                            return "DbvtNodePtrArray";
                        case "btDbvtProxy":
                            return "DbvtProxyPtrArray";
                        case "btSoftBody::Node":
                            return "NodePtrArray";
                    }
                }

                return GetTypeNameCS(type.Referenced) + "[]";
            }

            return type.ManagedNameCS;
        }

        public static string GetTypeRefName(TypeRefDefinition type)
        {
            if (!string.IsNullOrEmpty(type.Name) && type.Name.Equals("btAlignedObjectArray"))
            {
                if (type.SpecializedTemplateType != null)
                {
                    return "Aligned" + type.SpecializedTemplateType.ManagedName + "Array^";
                }
            }

            switch (type.ManagedName)
            {
                case "Matrix3x3":
                    return "Matrix";
                case "Transform":
                    return "Matrix";
                case "Quaternion":
                case "Vector3":
                case "Vector4":
                    return type.ManagedName;
            }
            
            if (type.ManagedName.Equals("float") && "btScalar".Equals(type.Name))
            {
                return "btScalar";
            }
            return type.ManagedTypeRefName;
        }

        // Is the struct passed by value or by reference?
        public static bool MarshalStructByValue(TypeRefDefinition type)
        {
            if (type.IsPointer)
            {
                return false;
            }

            switch (type.ManagedName)
            {
                case "Matrix3x3":
                case "Quaternion":
                case "Transform":
                case "Vector3":
                case "Vector4":
                    return true;
                default:
                    return false;
            }
        }

        // Does the type require additional lines of code to marshal?
        public static bool TypeRequiresMarshal(TypeRefDefinition type)
        {
            switch (type.ManagedName)
            {
                case "Matrix3x3":
                case "Quaternion":
                case "Transform":
                case "Vector3":
                case "Vector4":
                    return true;
                default:
                    return false;
            }
        }

        public static string GetReturnValueMarshalStart(TypeRefDefinition type)
        {
            switch (type.ManagedName)
            {
                case "Quaternion":
                case "Matrix3x3":
                case "Transform":
                case "Vector3":
                case "Vector4":
                    if (type.IsPointer)
                    {
                        return type.ManagedName.ToUpper() + "_OUT(";
                    }
                    if (type.IsReference)
                    {
                        return type.ManagedName.ToUpper() + "_OUT(&";
                    }
                    return type.ManagedName.ToUpper() + "_OUT_VAL(";
                default:
                    return null;
            }
        }

        public static string GetReturnValueMarshalEnd(ParameterDefinition param)
        {
            switch (param.Type.ManagedName)
            {
                case "Quaternion":
                case "Matrix3x3":
                case "Transform":
                case "Vector3":
                case "Vector4":
                    return "), " + param.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetFieldGetterMarshal(ParameterDefinition parameter, FieldDefinition field)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Quaternion":
                    return "QUATERNION_OUT(&obj->" + field.Name + ", " + parameter.Name + ");";
                case "Matrix3x3":
                    return "MATRIX3X3_OUT(&obj->" + field.Name + ", " + parameter.Name + ");";
                case "Transform":
                    return "TRANSFORM_OUT(&obj->" + field.Name + ", " + parameter.Name + ");";
                case "Vector3":
                    return "VECTOR3_OUT(&obj->" + field.Name + ", " + parameter.Name + ");";
                case "Vector4":
                    return "VECTOR4_OUT(&obj->" + field.Name + ", " + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetFieldSetterMarshal(ParameterDefinition parameter, FieldDefinition field)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Quaternion":
                    return "QUATERNION_IN(" + parameter.Name + ", &obj->" + field.Name + ");";
                case "Matrix3x3":
                    return "MATRIX3X3_IN(" + parameter.Name + ", &obj->" + field.Name + ");";
                case "Transform":
                    return "TRANSFORM_IN(" + parameter.Name + ", &obj->" + field.Name + ");";
                case "Vector3":
                    return "VECTOR3_IN(" + parameter.Name + ", &obj->" + field.Name + ");";
                case "Vector4":
                    return "VECTOR4_IN(" + parameter.Name + ", &obj->" + field.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalPrologue(ParameterDefinition parameter, MethodDefinition method)
        {
            if (method.Field != null)
            {
                return null;
            }

            switch (parameter.Type.ManagedName)
            {
                case "Quaternion":
                    return "QUATERNION_CONV(" + parameter.Name + ");";
                case "Matrix3x3":
                    return "MATRIX3X3_CONV(" + parameter.Name + ");";
                case "Transform":
                    return "TRANSFORM_CONV(" + parameter.Name + ");";
                case "Vector3":
                    return "VECTOR3_CONV(" + parameter.Name + ");";
                case "Vector4":
                    return "VECTOR4_CONV(" + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalPrologueCppCli(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Matrix3x3":
                    return "MATRIX3X3_CONV(" + parameter.ManagedName + ");";
                case "Quaternion":
                    return "QUATERNION_CONV(" + parameter.ManagedName + ");";
                case "Transform":
                    return "TRANSFORM_CONV(" + parameter.ManagedName + ");";
                case "Vector3":
                    return "VECTOR3_CONV(" + parameter.ManagedName + ");";
                case "Vector4":
                    return "VECTOR4_CONV(" + parameter.ManagedName + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalEpilogue(ParameterDefinition parameter)
        {
            if (parameter.Type.IsConst || (parameter.Type.Referenced != null && parameter.Type.Referenced.IsConst))
            {
                return null;
            }

            switch (parameter.Type.ManagedName)
            {
                case "Quaternion":
                    return "QUATERNION_DEF_OUT(" + parameter.Name + ");";
                case "Matrix3x3":
                    return "MATRIX3X3_DEF_OUT(" + parameter.Name + ");";
                case "Transform":
                    return "TRANSFORM_DEF_OUT(" + parameter.Name + ");";
                case "Vector3":
                    return "VECTOR3_DEF_OUT(" + parameter.Name + ");";
                case "Vector4":
                    return "VECTOR4_DEF_OUT(" + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalEpilogueCppCli(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Quaternion":
                    return "QUATERNION_DEL(" + parameter.ManagedName + ");";
                case "Matrix3x3":
                    return "MATRIX3X3_DEL(" + parameter.ManagedName + ");";
                case "Transform":
                    return "TRANSFORM_DEL(" + parameter.ManagedName + ");";
                case "Vector3":
                    return "VECTOR3_DEL(" + parameter.ManagedName + ");";
                case "Vector4":
                    return "VECTOR4_DEL(" + parameter.ManagedName + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshal(ParameterDefinition parameter)
        {
            string reference = parameter.Type.IsPointer ? "&" : string.Empty;

            switch (parameter.Type.ManagedName)
            {
                case "Quaternion":
                    return reference + "QUATERNION_USE(" + parameter.Name + ")";
                case "Transform":
                    return reference + "TRANSFORM_USE(" + parameter.Name + ")";
                case "Matrix3x3":
                    return reference + "MATRIX3X3_USE(" + parameter.Name + ")";
                case "Vector3":
                    return reference + "VECTOR3_USE(" + parameter.Name + ")";
                case "Vector4":
                    return reference + "VECTOR4_USE(" + parameter.Name + ")";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalCppCli(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "IDebugDraw":
                    return "DebugDraw::GetUnmanaged(" + parameter.ManagedName + ")";
                case "Quaternion":
                    return "QUATERNION_USE(" + parameter.ManagedName + ")";
                case "Transform":
                    return "TRANSFORM_USE(" + parameter.ManagedName + ")";
                case "Matrix3x3":
                    return "MATRIX3X3_USE(" + parameter.ManagedName + ")";
                case "Vector3":
                    return "VECTOR3_USE(" + parameter.ManagedName + ")";
                case "Vector4":
                    return "VECTOR4_USE(" + parameter.ManagedName + ")";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalConstructorStart(MethodDefinition getter)
        {
            switch (getter.ReturnType.ManagedName)
            {
                case "CollisionShape":
                    return "CollisionShape::GetManaged(";
                case "IDebugDraw":
                    return "DebugDraw::GetManaged(";
                case "OverlappingPairCache":
                    return "OverlappingPairCache::GetManaged(";
                case "Quaternion":
                    return "Math::BtQuatToQuaternion(&";
                case "Transform":
                    return "Math::BtTransformToMatrix(&";
                case "Vector3":
                    return "Math::BtVector3ToVector3(&";
                case "Vector4":
                    return "Math::BtVector4ToVector4(&";
                default:
                    return string.Empty;
            }
        }

        public static string GetTypeMarshalConstructorEnd(MethodDefinition getter)
        {
            switch (getter.ReturnType.ManagedName)
            {
                case "CollisionShape":
                case "IDebugDraw":
                case "OverlappingPairCache":
                case "Quaternion":
                case "Transform":
                case "Vector3":
                case "Vector4":
                    return ")";
                default:
                    return string.Empty;
            }
        }

        public static string GetTypeMarshalConstructorStartCS(MethodDefinition method)
        {
            switch (method.ReturnType.ManagedName)
            {
                case "BroadphaseProxy":
                case "CollisionObject":
                case "CollisionShape":
                case "OverlappingPairCache":
                    return string.Format("{0}.GetManaged(", method.ReturnType.ManagedName);
                case "IDebugDraw":
                    return "DebugDraw.GetManaged(";
                case "CollisionObjectWrapper":
                    return string.Format("new {0}(", method.ReturnType.ManagedName);
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

        public static string GetTypeMarshalFieldSetCppCli(FieldDefinition field, ParameterDefinition parameter, string nativePointer)
        {
            switch (field.Type.ManagedName)
            {
                case "Quaternion":
                    return "Math::QuaternionToBtQuat(" + parameter.ManagedName + ", &" + nativePointer + "->" + field.Name + ')';
                case "Transform":
                    return "Math::MatrixToBtTransform(" + parameter.ManagedName + ", &" + nativePointer + "->" + field.Name + ')';
                case "Vector3":
                    return "Math::Vector3ToBtVector3(" + parameter.ManagedName + ", &" + nativePointer + "->" + field.Name + ')';
                case "Vector4":
                    return "Math::Vector4ToBtVector4(" + parameter.ManagedName + ", &" + nativePointer + "->" + field.Name + ')';
                default:
                    return null;
            }
        }

        public static string GetTypeDllImport(TypeRefDefinition type)
        {
            switch (type.ManagedNameCS)
            {
                case "Matrix3x3":
                case "Quaternion":
                case "Transform":
                case "Vector3":
                case "Vector4":
                    {
                        if (type.Referenced != null && !(type.IsConst || type.Referenced.IsConst))
                        {
                            return "[Out] out " + GetTypeNameCS(type);
                        }
                        return "[In] ref " + GetTypeNameCS(type);
                    }
            }

            if (type.Referenced != null && !type.IsBasic)
            {
                if ("btScalar".Equals(type.Referenced.Name))
                {
                    if (type.IsPointer)
                    {
                        return type.ManagedNameCS + "[]";
                    }
                    else
                    {
                        // reference
                        if (!(type.IsConst || type.Referenced.IsConst))
                        {
                            return "[Out] out " + type.ManagedNameCS;
                        }
                    }
                }
                return "IntPtr";
            }

            return type.ManagedNameCS;
        }

        public static string GetTypeCSMarshal(ParameterDefinition parameter)
        {
            var type = parameter.Type;

            if (type.Referenced != null && !type.IsBasic)
            {
                switch (type.ManagedNameCS)
                {
                    case "Matrix3x3":
                    case "Quaternion":
                    case "Transform":
                    case "Vector3":
                    case "Vector4":
                        {
                            if (type.Referenced != null && !(type.IsConst || type.Referenced.IsConst))
                            {
                                return "out " + parameter.ManagedName;
                            }
                            return "ref " + parameter.ManagedName;
                        }
                    case "IDebugDraw":
                        return "DebugDraw.GetUnmanaged(" + parameter.ManagedName + ')';
                }
            }

            if (!type.IsBasic)
            {
                if (!(type.IsPointer && type.ManagedName.Equals("void")))
                {
                    return parameter.ManagedName + "._native";
                }
            }
            return parameter.ManagedName;
        }

        public static string GetTypeGetterCSMarshal(PropertyDefinition prop, int level)
        {
            StringBuilder output = new StringBuilder();
            TypeRefDefinition type = prop.Type;

            // If cached property can only be set in constructor,
            // the getter can simply return the cached value
            // TODO: check if cached value is initialized in all constructors
            CachedProperty cachedProperty;
            if (prop.Parent.CachedProperties.TryGetValue(prop.Name, out cachedProperty))
            {
                if (cachedProperty.Property.Setter == null)
                {
                    output.AppendLine(GetTabs(level + 2) + string.Format("get {{ return {0}; }}", cachedProperty.CacheFieldName));
                    return output.ToString();
                }
            }

            if (!type.IsBasic)
            {
                switch (type.ManagedNameCS)
                {
                    case "Matrix3x3":
                    case "Quaternion":
                    case "Transform":
                    case "Vector3":
                    case "Vector4":
                        output.AppendLine(GetTabs(level + 2) + "get");
                        output.AppendLine(GetTabs(level + 2) + "{");
                        output.AppendLine(GetTabs(level + 3) + GetTypeNameCS(type) + " value;");
                        output.AppendLine(GetTabs(level + 3) + string.Format("{0}_{1}(_native, out value);", prop.Parent.FullNameCS, prop.Getter.Name));
                        output.AppendLine(GetTabs(level + 3) + "return value;");
                        output.AppendLine(GetTabs(level + 2) + '}');
                        return output.ToString();
                }
            }

            output.AppendLine(GetTabs(level + 2) + string.Format("get {{ return {0}{1}_{2}(_native){3}; }}",
                GetTypeMarshalConstructorStartCS(prop.Getter),
                prop.Parent.FullNameCS, prop.Getter.Name,
                GetTypeMarshalConstructorEndCS(prop.Getter)));
            return output.ToString();
        }

        public static string GetTypeSetterCSMarshal(TypeRefDefinition type)
        {
            if (!type.IsBasic)
            {
                switch (type.ManagedNameCS)
                {
                    case "Matrix3x3":
                    case "Quaternion":
                    case "Transform":
                    case "Vector3":
                    case "Vector4":
                        return "ref value";
                }
                if (type.ManagedTypeRefName.Equals("IntPtr"))
                {
                    return "value";
                }
                return "value._native";
            }

            return "value";
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
                && !cl.ManagedName.Equals("ContactSolverInfoData");
        }
    }
}
