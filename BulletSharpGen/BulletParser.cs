using ClangSharp;
using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    class BulletParser : DefaultParser
    {
        public BulletParser(WrapperProject project)
            : base(project)
        {
            // Classes that shouldn't be instantiated by users
            var hidePublicConstructors = new HashSet<string> {
                "btActivatingCollisionAlgorithm", "btContactConstraint",
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

        public override void Parse()
        {
            base.Parse();

            // Check if any property values can be cached in
            // in constructors or property setters
            foreach (var @class in Project.ClassDefinitions.Values)
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
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                // Order by name, then fix inheritance, parent classes must appear first
                @class.NestedClasses.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                var classesOrdered = @class.NestedClasses;
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
            if (t.IsBasic) return false;
            if (t.Target != null)
            {
                if (t.Target is EnumDefinition) return false;
                if (t.Target.MarshalAsStruct) return false;
            }
            if (t.Referenced != null)
            {
                return IsCacheableType(t.Referenced);
            }
            return true;
        }

        public static string GetTypeRefName(TypeRefDefinition type)
        {
            if (!string.IsNullOrEmpty(type.Name) && type.Name.Equals("btAlignedObjectArray"))
            {
                if (type.TemplateParams != null)
                {
                    return "Aligned" + type.TemplateParams.First().ManagedName + "Array^";
                }
            }

            switch (type.ManagedName)
            {
                case "Matrix3x3":
                    return "Matrix";
                case "Transform":
                    return "Matrix";
                case "Quaternion":
                case "Vector4":
                    return type.ManagedName;
            }
            
            if (type.ManagedName.Equals("float") && "btScalar".Equals(type.Name))
            {
                return "btScalar";
            }
            switch (type.Kind)
            {
                case TypeKind.Pointer:
                case TypeKind.LValueReference:
                case TypeKind.ConstantArray:
                    if (type.Referenced.Kind == TypeKind.Void)
                    {
                        return "IntPtr";
                    }
                    switch (type.ManagedName)
                    {
                        case "char":
                            return "String^";
                        case "float":
                            return string.Format("array<{0}>^", type.Referenced.Name);
                    }
                    return type.ManagedName + '^';
            }
            return type.ManagedName;
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
                case "Vector4":
                    return "VECTOR4_CONV(" + parameter.ManagedName + ");";
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
                case "Vector4":
                    return "VECTOR4_DEL(" + parameter.ManagedName + ");";
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
                    return $"{method.ReturnType.ManagedName}.GetManaged(";
                case "IDebugDraw":
                    return "DebugDraw.GetManaged(";
                case "CollisionObjectWrapper":
                    return $"new {method.ReturnType.ManagedName}(";
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
                case "Vector4":
                    return "Math::Vector4ToBtVector4(" + parameter.ManagedName + ", &" + nativePointer + "->" + field.Name + ')';
                default:
                    return null;
            }
        }

        public static string GetTypeCSMarshal(ParameterDefinition param)
        {
            var type = param.Type.Canonical;

            if ((type.Target != null && type.Target.MarshalAsStruct) ||
                (type.Kind == TypeKind.LValueReference && type.Referenced.Target != null && type.Referenced.Target.MarshalAsStruct))
            {
                if (param.MarshalDirection == MarshalDirection.Out)
                {
                    return $"out {param.ManagedName}";
                }
                return $"ref {param.ManagedName}";
            }

            if (type.Kind == TypeKind.LValueReference && type.Referenced.Canonical.IsBasic)
            {
                if (param.MarshalDirection == MarshalDirection.Out)
                {
                    return $"out {param.ManagedName}";
                }
                if (param.MarshalDirection == MarshalDirection.InOut)
                {
                    "".ToString();
                }
                return $"ref {param.ManagedName}";
            }

            if (type.Referenced != null)
            {
                switch (type.ManagedName)
                {
                    case "IDebugDraw":
                        return "DebugDraw.GetUnmanaged(" + param.ManagedName + ')';
                }
            }

            if (!type.IsBasic)
            {
                if (!(type.Kind == TypeKind.Pointer && type.Referenced.Kind == TypeKind.Void))
                {
                    return param.ManagedName + "._native";
                }
            }
            return param.ManagedName;
        }

        public static string GetTypeSetterCSMarshal(TypeRefDefinition type)
        {
            if ((type.Target != null && type.Target.MarshalAsStruct) ||
                (type.Kind == TypeKind.LValueReference && type.Referenced.Target != null && type.Referenced.Target.MarshalAsStruct))
            {
                return "ref value";
            }
            if (!type.IsBasic)
            {
                if (type.Kind == TypeKind.Pointer && type.Referenced.Kind == TypeKind.Void)
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
