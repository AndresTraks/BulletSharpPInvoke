using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    class BulletParser
    {
        Dictionary<string, ClassDefinition> classDefinitions = new Dictionary<string, ClassDefinition>();
        public Dictionary<string, HeaderDefinition> ExternalHeaders = new Dictionary<string, HeaderDefinition>();

        Dictionary<string, string> methodNameMapping = new Dictionary<string, string>();
        Dictionary<string, string> parameterNameMapping = new Dictionary<string, string>();
        Dictionary<string, string> excludedClassNames;

        public BulletParser(Dictionary<string, ClassDefinition> classDefinitions, Dictionary<string, HeaderDefinition> headerDefinitions)
        {
            this.classDefinitions = classDefinitions;
            ExternalHeaders = headerDefinitions;

            // Excluded classes
            string[] excludedClassArray = new string[] { "ActionInterface", "AlignedAllocator", "bChunk", "bCommon",
            "bFile", "Box", "BulletFile", "cl_MiniCL_Defs", "cl_platform", "ContactProcessing", "ConvexHull",
            "ConvexHullComputer", "DantzigLCP", "GenericPoolAllocator",
            "gim_array", "gim_bitset", "gim_box_collision", "gim_box_set", "gim_clip_polygon", "gim_contact",
            "gim_geom_types", "gim_hash_table", "gim_memory", "gim_radixsort", "gim_tri_collision", "GjkEpa2",
            "Gpu3DGridBroadphase", "Gpu3DGridBroadphaseSharedTypes", "GpuDefines", "GrahamScan2dConvexHull",
            "HashedSimplePairCache", "HashMap", "HeapManager", "IDebugDraw", "JacobianEntry", "List", "Material",
            "Matrix3x3", "MatrixX", "MiniCLTask", "MiniCLTaskScheduler", "MultiSapBroadphase", "PlatformDefinitions",
            "PolarDecomposition", "PolyhedralContactClipping", "PosixThreadSupport", "PpuAddressSpace", "QuadWord",
            "RaycastCallback", "SequentialThreadSupport", "SimpleDynamicsWorld", "SoftBodyData", "SoftBodyInternals",
            "SoftBodySolvers", "SoftRigidCollisionAlgorithm", "SoftSoftCollisionAlgorithm", "SolveProjectedGaussSeidel",
            "SolverBody", "SolverConstraint", "SpuCollisionObjectWrapper", "SpuCollisionShapes",
            "SpuCollisionTaskProcess", "SpuContactManifoldCollisionAlgorithm", "SpuContactResult",
            "SpuConvexPenetrationDepthSolver", "SpuDoubleBuffer", "SpuGatheringCollisionTask",
            "SpuMinkowskiPenetrationDepthSolver", "SpuSampleTask", "SpuSampleTaskProcess", "SpuSync", "StackAlloc",
            "SubSimplexConvexCast", "Transform", "TrbDynBody", "TrbStateVec", "vectormath_aos", "vmInclude",
            "HacdCircularList", "HacdGraph", "HacdICHull", "HacdManifoldMesh", "HacdVector",
            "Quaternion", "Vector3", "LemkeAlgorithm", "CharacterControllerInterface" };

            // Managed method names
            methodNameMapping.Add("gimpact_vs_compoundshape", "GImpactVsCompoundshape");
            methodNameMapping.Add("gimpact_vs_concave", "GImpactVsConcave");
            methodNameMapping.Add("gimpact_vs_gimpact", "GImpactVsGImpact");
            methodNameMapping.Add("gimpact_vs_shape", "GImpactVsShape");

            // Managed method parameter names
            parameterNameMapping.Add("i_dataBufferSize", "dataBufferSize");
            parameterNameMapping.Add("i_swapEndian", "swapEndian");
            parameterNameMapping.Add("i_alignedDataBuffer", "alignedDataBuffer");
            parameterNameMapping.Add("o_alignedDataBuffer", "alignedDataBuffer");
            parameterNameMapping.Add("drawflags", "drawFlags");
            parameterNameMapping.Add("idraw", "iDraw");
            parameterNameMapping.Add("indexstride", "indexStride");
            parameterNameMapping.Add("indicestype", "indicesType");
            parameterNameMapping.Add("maxdepth", "maxDepth");
            parameterNameMapping.Add("mindepth", "minDepth");
            parameterNameMapping.Add("limot", "limitMotor");
            parameterNameMapping.Add("nodeindex", "nodeIndex");
            parameterNameMapping.Add("numfaces", "numFaces");
            parameterNameMapping.Add("numindices", "numIndices");
            parameterNameMapping.Add("numverts", "numVerts");
            parameterNameMapping.Add("rbA", "rigidBodyA");
            parameterNameMapping.Add("rbB", "rigidBodyB");
            parameterNameMapping.Add("rbAFrame", "rigidBodyAFrame");
            parameterNameMapping.Add("rbBFrame", "rigidBodyBFrame");
            parameterNameMapping.Add("use32bitIndices", "use32BitIndices");
            parameterNameMapping.Add("use4componentVertices", "use4ComponentVertices");
            parameterNameMapping.Add("vertexbase", "vertexBase");
            parameterNameMapping.Add("bcheckexist", "checkExist");

            // Managed header names
            var headerNameMapping = new Dictionary<string, string>();
            headerNameMapping.Add("btActionInterface", "IAction");
            headerNameMapping.Add("btBox2dBox2dCollisionAlgorithm", "Box2DBox2DCollisionAlgorithm");
            headerNameMapping.Add("btBox2dShape", "Box2DShape");
            headerNameMapping.Add("btCompoundFromGimpact", "CompoundFromGImpact");
            headerNameMapping.Add("btConvex2dConvex2dAlgorithm", "Convex2DConvex2DAlgorithm");
            headerNameMapping.Add("btConvex2dShape", "Convex2DShape");
            headerNameMapping.Add("btMLCPSolver", "MlcpSolver");
            headerNameMapping.Add("btMLCPSolverInterface", "MlcpSolverInterface");
            headerNameMapping.Add("btNNCGConstraintSolver", "NncgConstraintSolver");
            headerNameMapping.Add("btSparseSDF", "SparseSdf");
            headerNameMapping.Add("hacdHACD", "Hacd");

            // Managed class names
            var classNameMapping = new Dictionary<string, string>();
            classNameMapping.Add("btAABB", "Aabb");
            classNameMapping.Add("bt32BitAxisSweep3", "AxisSweep3_32Bit");
            classNameMapping.Add("btActionInterface", "IAction");
            classNameMapping.Add("btBox2dBox2dCollisionAlgorithm", "Box2DBox2DCollisionAlgorithm");
            classNameMapping.Add("btBox2dShape", "Box2DShape");
            classNameMapping.Add("btConvex2dConvex2dAlgorithm", "Convex2DConvex2DAlgorithm");
            classNameMapping.Add("btConvex2dShape", "Convex2DShape");
            classNameMapping.Add("btMLCPSolver", "MlcpSolver");
            classNameMapping.Add("btMLCPSolverInterface", "MlcpSolverInterface");
            classNameMapping.Add("btMultibodyLink", "MultiBodyLink");
            classNameMapping.Add("btNNCGConstraintSolver", "NncgConstraintSolver");
            classNameMapping.Add("HACD", "Hacd");
            classNameMapping.Add("GIM_BVH_DATA", "GimBvhData");
            classNameMapping.Add("GIM_BVH_DATA_ARRAY", "GimBvhDataArray");
            classNameMapping.Add("GIM_BVH_TREE_NODE", "GimBvhTreeNode");
            classNameMapping.Add("GIM_BVH_TREE_NODE_ARRAY", "GimBvhTreeNodeArray");
            classNameMapping.Add("GIM_PAIR", "GimPair");
            classNameMapping.Add("GIM_TRIANGLE_CONTACT", "GimTriangleContact");
            classNameMapping.Add("BT_QUANTIZED_BVH_NODE", "GImpactQuantizedBvhNode");
            classNameMapping.Add("GIM_QUANTIZED_BVH_NODE_ARRAY", "GimGImpactQuantizedBvhNodeArray");
            classNameMapping.Add("btCPUVertexBufferDescriptor", "CpuVertexBufferDescriptor");
            classNameMapping.Add("btBU_Simplex1to4", "BuSimplex1To4");

            // Classes that shouldn't be instantiated by users
            List<string> hidePublicConstructors = new List<string>() {
                "btActivatingCollisionAlgorithm", "btContactConstraint", "btConvexInternalShape",
                "btConvexInternalAabbCachingShape", "btPolyhedralConvexAabbCachingShape", "btTypedObject",
                "btTriangleMeshShape"
            };

            // Classes for which no internal constructor is needed
            List<string> hideInternalConstructor = new List<string>() {
                "btBox2dBox2dCollisionAlgorithm", "btBox2dBox2dCollisionAlgorithm::CreateFunc",
                "btBoxBoxCollisionAlgorithm", "btBoxBoxCollisionAlgorithm::CreateFunc",
                "btBoxBoxDetector", "btCollisionAlgorithmConstructionInfo", "btDefaultCollisionConstructionInfo",
                "btCompoundCollisionAlgorithm::CreateFunc", "btCompoundCollisionAlgorithm::SwappedCreateFunc",
                "btCompoundCompoundCollisionAlgorithm::CreateFunc", "btCompoundCompoundCollisionAlgorithm::SwappedCreateFunc", "btCompoundCompoundCollisionAlgorithm",
                "btContinuousConvexCollision",
                "btConvex2dConvex2dAlgorithm::CreateFunc", "btConvex2dConvex2dAlgorithm",
                "btConvexConcaveCollisionAlgorithm::CreateFunc", "btConvexConcaveCollisionAlgorithm::SwappedCreateFunc", "btConvexConcaveCollisionAlgorithm",
                "btConvexConvexAlgorithm::CreateFunc", "btConvexConvexAlgorithm",
                "btConvexPlaneCollisionAlgorithm::CreateFunc",
                "btDefaultMotionState",
                "btDiscreteCollisionDetectorInterface::ClosestPointInput",
                "btEmptyAlgorithm::CreateFunc", "btEmptyAlgorithm", "btGjkConvexCast",
                "btGjkEpaPenetrationDepthSolver", "btManifoldResult",
                "btMinkowskiPenetrationDepthSolver", "btPointCollector",
                "btDefaultVehicleRaycaster", "btRaycastVehicle", "btDefaultSerializer",
                "btSoftBodyConcaveCollisionAlgorithm::CreateFunc", "btSoftBodyConcaveCollisionAlgorithm::SwappedCreateFunc",
                "btSoftBodyRigidBodyCollisionConfiguration", "btCPUVertexBufferDescriptor",
                "btSoftRigidDynamicsWorld",
                "btSphereBoxCollisionAlgorithm::CreateFunc", "btSphereBoxCollisionAlgorithm",
                "btSphereSphereCollisionAlgorithm::CreateFunc", "btSphereSphereCollisionAlgorithm",
                "btSphereTriangleCollisionAlgorithm::CreateFunc", "btSphereTriangleCollisionAlgorithm",
                "SpuGatheringCollisionDispatcher", "btConvexSeparatingDistanceUtil",
                "btVehicleRaycaster::btVehicleRaycasterResult", "btOverlapCallback",
                "btRaycastVehicle::btVehicleTuning",
                "btBox2dShape", "btBoxShape", "btCapsuleShapeX", "btCapsuleShapeZ",
                "btCylinderShapeX", "btCylinderShapeZ", "btCompoundShape",
                "btConeShapeX", "btConeShapeZ", "btConvex2dShape", "btConvexHullShape",
                "btConvexPointCloudShape", "btEmptyShape", "btHeightfieldTerrainShape", "btMinkowskiSumShape",
                "btMultiSphereShape", "btMultimaterialTriangleMeshShape", "btScaledBvhTriangleMeshShape",
                "btSphereShape", "btStaticPlaneShape", "btUniformScalingShape",
                "btCollisionWorld::ClosestConvexResultCallback",
                "HACD", "btRigidBody::btRigidBodyConstructionInfo",
                "btSoftBody::ImplicitFn", "btTriangleBuffer", "btMaterialProperties",
                "btCollisionWorld::LocalConvexResult", "btCollisionWorld::LocalRayResult",
                "btCollisionWorld::AllHitsRayResultCallback", "btCollisionWorld::ContactResultCallback",
                "btCollisionWorld::ClosestRayResultCallback"};

            // Classes that might be cleaned up by Bullet and not us (use preventDelete to indicate this)
            List<string> preventDelete = new List<string>() {
                "btBroadphaseProxy", "btAABB", "btCollisionAlgorithmCreateFunc",
                "btCollisionObject", "btCollisionObjectWrapper", "btCollisionShape",
                "btConstraintSolver", "btContactSolverInfoData", "btDbvt",
                "btRotationalLimitMotor", "btTranslationalLimitMotor",
                "btRotationalLimitMotor2", "btTranslationalLimitMotor2",
                "btConstraintSetting", "btSimulationIslandManager",
                "btSolve2LinearConstraint", "btIndexedMesh", "btTriangleInfoMap",
                "btJointFeedback", "btTypedConstraint", "btAngularLimit",
                "btTypedConstraint::btConstraintInfo1", "btTypedConstraint::btConstraintInfo2",
                "btWheelInfo", "btManifoldPoint", "btCollisionWorld::LocalShapeInfo"};

            // Classes that have OnDisposing/OnDisposed events
            List<string> trackingDisposable = new List<string>() {
                "btCollisionObject", "btCollisionShape", "btCollisionWorld",
                "btConstraintSolver", "btDbvt", "btDispatcher", "btRaycastVehicle",
                "btTypedConstraint"};

            excludedClassNames = new Dictionary<string, string>();
            foreach (string c in excludedClassArray)
            {
                excludedClassNames.Add(c, c);
            }

            // Resolve references (match TypeRefDefinitions to ClassDefinitions)
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                // Resolve base class type
                if (c.BaseClass != null)
                {
                    ResolveTypeRef(c.BaseClass);
                    if (c.BaseClass.Target == null)
                    {
                        Console.WriteLine("Base class " + c.BaseClass.Name + " not found!");
                    }
                    else
                    {
                        var targetHeader = c.BaseClass.Target.Header;
                        if (c.Header != targetHeader && !c.Header.Includes.Contains(targetHeader))
                        {
                            c.Header.Includes.Add(targetHeader);
                        }
                    }
                }

                // Resolve typedef
                if (c.TypedefUnderlyingType != null)
                {
                    ResolveTypeRef(c.TypedefUnderlyingType);
                }

                // Resolve method return type and parameter types
                foreach (MethodDefinition method in c.Methods)
                {
                    ResolveTypeRef(method.ReturnType);
                    foreach (ParameterDefinition param in method.Parameters)
                    {
                        ResolveTypeRef(param.Type);
                    }
                }

                // Resolve field types
                foreach (FieldDefinition field in c.Fields)
                {
                    ResolveTypeRef(field.Type);
                }
            }

            // Exclude all overridden methods
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                int i;
                for (i = 0; i < c.Methods.Count; )
                {
                    var method = c.Methods[i];
                    // Check if the method already exists in base classes
                    var baseClass = c.BaseClass;
                    while (baseClass != null && baseClass.Target != null)
                    {
                        foreach (MethodDefinition m in baseClass.Target.Methods)
                        {
                            if (method.Equals(m))
                            {
                                c.Methods.Remove(method);
                                method = null;
                                break;
                            }
                        }
                        if (method == null)
                        {
                            break;
                        }
                        baseClass = baseClass.Target.BaseClass;
                    }
                    if (method != null)
                    {
                        i++;
                    }
                }
            }

            // Exclude constructors of abstract classes
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                if (!c.IsAbstract)
                {
                    continue;
                }

                int i;
                for (i = 0; i < c.Methods.Count; )
                {
                    var method = c.Methods[i];
                    if (method.IsConstructor)
                    {
                        c.Methods.Remove(method);
                        continue;
                    }
                    i++;
                }
            }

            // Exclude duplicate methods
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                for (int i = 0; i < c.Methods.Count; i++)
                {
                    for (int j = i + 1; j < c.Methods.Count; j++)
                    {
                        if (!c.Methods[i].Equals(c.Methods[j]))
                        {
                            continue;
                        }

                        var iType = c.Methods[i].ReturnType;
                        var jType = c.Methods[j].ReturnType;
                        bool iConst = iType.IsConst || (iType.Referenced != null && iType.Referenced.IsConst);
                        bool jConst = jType.IsConst || (jType.Referenced != null && jType.Referenced.IsConst);

                        // Prefer non-const return value
                        if (iConst)
                        {
                            if (!jConst)
                            {
                                c.Methods.RemoveAt(i);
                                i--;
                            }
                        }
                        else
                        {
                            if (jConst)
                            {
                                c.Methods.RemoveAt(j);
                                i--;
                            }
                        }
                        break;
                    }
                }
            }

            // Set managed method/parameter names
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                foreach (var method in c.Methods)
                {
                    method.ManagedName = GetManagedMethodName(method);

                    foreach (var param in method.Parameters)
                    {
                        param.ManagedName = GetManagedParameterName(param);
                    }
                }
            }

            // Turn fields into get/set methods
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                foreach (FieldDefinition field in c.Fields)
                {
                    ResolveTypeRef(field.Type);

                    string name = field.Name;
                    if (name.StartsWith("m_"))
                    {
                        name = name.Substring(2);
                    }
                    name = name.Substring(0, 1).ToUpper() + name.Substring(1); // capitalize

                    // one_two_three -> oneTwoThree
                    string managedName = name;
                    while (managedName.Contains("_"))
                    {
                        int pos = managedName.IndexOf('_');
                        managedName = managedName.Substring(0, pos) + managedName.Substring(pos + 1, 1).ToUpper() + managedName.Substring(pos + 2);
                    }

                    // Generate getter/setter methods
                    string getterName, setterName;
                    string managedGetterName, managedSetterName;
                    if (name.StartsWith("has"))
                    {
                        getterName = name;
                        setterName = "set" + name.Substring(3);
                        managedGetterName = managedName;
                        managedSetterName = "Set" + managedName.Substring(3);
                    }
                    else if (name.StartsWith("is"))
                    {
                        getterName = name;
                        setterName = "set" + name.Substring(2);
                        managedGetterName = managedName;
                        managedSetterName = "Set" + managedName.Substring(2);
                    }
                    else
                    {
                        getterName = "get" + name;
                        setterName = "set" + name;
                        managedGetterName = "Get" + managedName;
                        managedSetterName = "Set" + managedName;
                    }

                    // See if there are already accessor methods for this field
                    MethodDefinition getter = null, setter = null;
                    foreach (var m in c.Methods)
                    {
                        if (m.ManagedName.Equals(managedGetterName) && m.Parameters.Length == 0)
                        {
                            getter = m;
                            continue;
                        }

                        if (m.ManagedName.Equals(managedSetterName) && m.Parameters.Length == 1)
                        {
                            setter = m;
                        }
                    }

                    if (getter == null)
                    {
                        getter = new MethodDefinition(getterName, c, 0);
                        getter.ManagedName = managedGetterName;
                        getter.ReturnType = field.Type;
                        getter.Field = field;
                    }

                    var prop = new PropertyDefinition(getter);

                    // Can't assign value to reference or constant array
                    if (setter == null && !field.Type.IsReference && !field.Type.IsConstantArray)
                    {
                        setter = new MethodDefinition(setterName, c, 1);
                        setter.ManagedName = managedSetterName;
                        setter.ReturnType = new TypeRefDefinition();
                        setter.Field = field;
                        TypeRefDefinition constType;
                        if (!field.Type.IsBasic && !field.Type.IsPointer)
                        {
                            constType = field.Type.Copy();
                            constType.IsConst = true;
                        }
                        else
                        {
                            constType = field.Type;
                        }
                        setter.Parameters[0] = new ParameterDefinition("value", constType);
                        setter.Parameters[0].ManagedName = "value";

                        prop.Setter = setter;
                        prop.Setter.Property = prop;
                    }
                }
            }

            // Turn getters/setters into properties
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                foreach (var method in c.Methods)
                {
                    if (method.Parameters.Length == 0 &&
                        (method.Name.StartsWith("get", StringComparison.InvariantCultureIgnoreCase) ||
                        method.Name.StartsWith("has", StringComparison.InvariantCultureIgnoreCase) ||
                        method.Name.StartsWith("is", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        if (method.Property == null)
                        {
                            new PropertyDefinition(method);
                        }
                    }
                }
                foreach (var method in c.Methods)
                {
                    if (method.Parameters.Length == 1 &&
                        method.Name.StartsWith("set", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string name = method.ManagedName.Substring(3);
                        // Find the property with the matching getter
                        foreach (PropertyDefinition prop in c.Properties)
                        {
                            if (prop.Setter != null)
                            {
                                continue;
                            }

                            if (prop.Name.Equals(name))
                            {
                                prop.Setter = method;
                                method.Property = prop;
                                break;
                            }
                        }
                    }
                }
            }

            // Get managed header names
            foreach (HeaderDefinition header in ExternalHeaders.Values)
            {
                string name = header.Name;
                string mapping;
                if (headerNameMapping.TryGetValue(name, out mapping))
                {
                    header.ManagedName = mapping;
                }
                else if (name.StartsWith("bt"))
                {
                    header.ManagedName = name.Substring(2);
                }
                else if (name.StartsWith("hacd"))
                {
                    header.ManagedName = "Hacd" + name.Substring(4);
                }
            }


            // Apply class properties
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                string name = c.Name;
                string mapping;
                if (classNameMapping.TryGetValue(name, out mapping))
                {
                    c.ManagedName = mapping;
                }
                else if (name.StartsWith("bt") && !name.Equals("btScalar"))
                {
                    c.ManagedName = name.Substring(2);
                }
                else
                {
                    c.ManagedName = name;
                }

                if (hidePublicConstructors.Contains(c.FullName))
                {
                    c.HidePublicConstructors = true;
                }

                if (hideInternalConstructor.Contains(c.FullName))
                {
                    c.NoInternalConstructor = true;
                }
                if (preventDelete.Contains(c.FullName))
                {
                    c.HasPreventDelete = true;
                }
                if (trackingDisposable.Contains(c.FullName))
                {
                    c.IsTrackingDisposable = true;
                }
            }

            // Sort methods and properties alphabetically
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                // Order by name, then fix inheritance, parent classes must appear first
                c.Classes.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                var classesOrdered = c.Classes;
                for (int i = 0; i < classesOrdered.Count; i++)
                {
                    var thisClass = classesOrdered[i];
                    var baseClass = thisClass.BaseClass;
                    if (baseClass != null && classesOrdered.Contains(baseClass.Target))
                    {
                        int thisIndex = classesOrdered.IndexOf(thisClass);
                        if (thisIndex < classesOrdered.IndexOf(baseClass.Target))
                        {
                            classesOrdered.Remove(baseClass.Target);
                            classesOrdered.Insert(thisIndex, baseClass.Target);
                        }
                    }
                }

                c.Methods.Sort((m1, m2) => m1.Name.CompareTo(m2.Name));
                c.Properties.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
            }

            // Mark excluded classes
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                if (IsExcludedClass(c))
                {
                    c.IsExcluded = true;
                }
            }

            Console.WriteLine("Parsing complete");
        }

        void ResolveTypeRef(TypeRefDefinition typeRef)
        {
            if (typeRef.IsBasic || typeRef.HasTemplateTypeParameter)
            {
                return;
            }
            if (typeRef.IsPointer || typeRef.IsReference || typeRef.IsConstantArray)
            {
                ResolveTypeRef(typeRef.Referenced);
            }
            else if (!classDefinitions.ContainsKey(typeRef.Name))
            {
                // Search for unscoped enums
                bool resolvedEnum = false;
                foreach (var c in classDefinitions.Values.Where(c => c.Enum != null))
                {
                    if (typeRef.Name.Equals(c.FullName + "::" + c.Enum.Name))
                    {
                        typeRef.Target = c;
                        resolvedEnum = true;
                    }
                }
                if (!resolvedEnum)
                {
                    Console.WriteLine("Class " + typeRef.Name + " not found!");
                }
            }
            else
            {
                typeRef.Target = classDefinitions[typeRef.Name];
            }

            if (typeRef.SpecializedTemplateType != null)
            {
                ResolveTypeRef(typeRef.SpecializedTemplateType);
            }
        }

        static string GetTabs(int n)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                builder.Append('\t');
            }
            return builder.ToString();
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
                default:
                    return type.ManagedName;
            }
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

        public string GetManagedMethodName(MethodDefinition method)
        {
            if (methodNameMapping.ContainsKey(method.Name))
            {
                return methodNameMapping[method.Name];
            }

            string managedName = method.Name;
            while (managedName.Contains("_"))
            {
                int pos = managedName.IndexOf('_');
                managedName = managedName.Substring(0, pos) + managedName.Substring(pos + 1, 1).ToUpper() + managedName.Substring(pos + 2);
            }

            return managedName.Substring(0, 1).ToUpper() + managedName.Substring(1);
        }

        public string GetManagedParameterName(ParameterDefinition param)
        {
            if (parameterNameMapping.ContainsKey(param.Name))
            {
                return parameterNameMapping[param.Name];
            }

            string managedName = param.Name;
            if (managedName.StartsWith("__unnamed"))
            {
                return managedName;
            }

            // Remove underscores
            // one_two_three -> oneTwoThree
            while (managedName.Contains("_"))
            {
                int pos = managedName.IndexOf('_');
                if (pos == 0)
                {
                    managedName = managedName.Substring(1);
                }
                else if (pos >= managedName.Length - 1)
                {
                    managedName = managedName.Substring(0, pos);
                    break;
                }
                else
                {
                    managedName = managedName.Substring(0, pos) + managedName.Substring(pos + 1, 1).ToUpper() + managedName.Substring(pos + 2);
                }
            }
            return managedName;
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
                    return "BroadphaseProxy.GetManaged(";
                case "CollisionObject":
                    return "CollisionObject.GetManaged(";
                case "CollisionObjectWrapper":
                    return "new CollisionObjectWrapper(";
                case "CollisionShape":
                    return "CollisionShape.GetManaged(";
                case "IDebugDraw":
                    return "DebugDraw.GetManaged(";
                case "OverlappingPairCache":
                    return "OverlappingPairCache.GetManaged(";
                default:
                    return string.Empty;
            }
        }

        public static string GetTypeMarshalConstructorEndCS(MethodDefinition method)
        {
            switch (method.ReturnType.ManagedName)
            {
                case "BroadphaseProxy":
                case "CollisionObject":
                case "CollisionObjectWrapper":
                case "CollisionShape":
                case "IDebugDraw":
                case "OverlappingPairCache":
                    return ")";
                default:
                    return string.Empty;
            }
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
                        output.AppendLine(GetTabs(level + 3) + prop.Parent.FullNameCS + '_' + prop.Getter.Name + "(_native, out value);");
                        output.AppendLine(GetTabs(level + 3) + "return value;");
                        output.AppendLine(GetTabs(level + 2) + '}');
                        return output.ToString();
                }
            }

            output.AppendLine(GetTabs(level + 2) + "get { return " +
                BulletParser.GetTypeMarshalConstructorStartCS(prop.Getter) +
                prop.Parent.FullNameCS + '_' + prop.Getter.Name + "(_native)" +
                BulletParser.GetTypeMarshalConstructorEndCS(prop.Getter) + "; }");
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

        private bool IsExcludedClass(ClassDefinition cl)
        {
            if (excludedClassNames.ContainsKey(cl.ManagedName))
            {
                return true;
            }

            if (cl.Name.StartsWith("b3"))
            {
                return true;
            }

            switch (cl.Name)
            {
                case "btBroadphasePairSortPredicate":
                    return true;
            }

            switch (cl.FullName)
            {
                case "btAxisSweep3Internal::Edge":
                case "btAxisSweep3Internal::Handle":
                case "BT_BOX_BOX_TRANSFORM_CACHE":
                case "btChunk":
                case "btPointerUid":
                case "btSoftBodyTriangleCallback":
                case "btTriIndex":
                case "btBarrier":
                case "btCriticalSection":
                case "GraphEdgePriorityQueue":
                case "btConstraintRow":
                case "btSpatialForceVector":
                case "btSpatialMotionVector":
                case "btSymmetricSpatialDyad":
                case "btSpatialTransformationMatrix":
                case "btPositionAndRadius":
                case "PfxParallelBatch":
                case "PfxParallelGroup":
                case "PfxSortData16":
                case "PfxSolverBody":
                case "PfxSetupContactConstraintsIO":
                case "PfxSolveConstraintsIO":
                case "PfxPostSolverIO":
                case "ConstraintSolverIO":
                case "btBvhSubtreeInfo":
                case "btInfMaskConverter":
                case "btRaycastVehicle":
                case "btDefaultVehicleRaycaster":
                    return true;
            }

            // Exclude all "FloatData/DoubleData" serialization classes
            return (cl.Name.EndsWith("Data")
                ||cl.Name.EndsWith("Data2"))
                && !cl.ManagedName.Equals("ContactSolverInfoData");
        }
    }
}
