using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    enum RefAccessSpecifier
    {
        Public,
        Protected,
        Private,
        Internal
    }

    class CppCliWriter : WrapperWriter
    {
        int _headerLineLength;
        int _sourceLineLength;
        
        const int TabWidth = 4;
        const int LineBreakWidth = 80;

        // Conditional compilation (#ifndef DISABLE_FEATURE)
        Dictionary<string, string> headerConditional = new Dictionary<string, string>
        {
            {"ActivatingCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"Box2DBox2DCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"BoxBoxCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"Box2DShape", "DISABLE_UNCOMMON"},
            {"BoxBoxDetector", "DISABLE_UNCOMMON"},
            {"BoxCollision", "DISABLE_GIMPACT"},
            {"BulletWorldImporter", "DISABLE_SERIALIZE"},
            {"CompoundCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"CompoundCompoundCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"CompoundFromGImpact", "DISABLE_GIMPACT"},
            {"ConeTwistConstraint", "DISABLE_CONSTRAINTS"},
            {"ContactConstraint", "DISABLE_CONSTRAINTS"},
            {"ContactSolverInfo", "DISABLE_CONSTRAINTS"},
            {"ContinuousConvexCollision", "DISABLE_UNCOMMON"},
            {"Convex2DConvex2DAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"Convex2DShape", "DISABLE_UNCOMMON"},
            {"ConvexCast", "DISABLE_UNCOMMON"},
            {"ConvexConcaveCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"ConvexConvexAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"ConvexPlaneCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"ConvexPenetrationDepthSolver", "DISABLE_UNCOMMON"},
            {"ConvexPointCloudShape", "DISABLE_UNCOMMON"},
            {"ConvexPolyhedron", "DISABLE_UNCOMMON"},
            {"DantzigSolver", "DISABLE_MLCP"},
            {"Dbvt", "DISABLE_DBVT"},
            {"DbvtNode", "DISABLE_DBVT"},
            {"DefaultSoftBodySolver", "DISABLE_SOFTBODY"},
            {"EmptyCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"FixedConstraint", "DISABLE_CONSTRAINTS"},
            {"GearConstraint", "DISABLE_CONSTRAINTS"},
            {"Generic6DofConstraint", "DISABLE_CONSTRAINTS"},
            {"Generic6DofSpringConstraint", "DISABLE_CONSTRAINTS"},
            {"Generic6DofSpring2Constraint", "DISABLE_CONSTRAINTS"},
            {"GeometryUtil", "DISABLE_GEOMETRY_UTIL"},
            {"GhostObject", "DISABLE_UNCOMMON"},
            {"GImpactBvh", "DISABLE_GIMPACT"},
            {"GImpactCollisionAlgorithm", "DISABLE_GIMPACT"},
            {"GImpactQuantizedBvh", "DISABLE_GIMPACT"},
            {"GImpactShape", "DISABLE_GIMPACT"},
            {"GjkConvexCast", "DISABLE_UNCOMMON"},
            {"GjkEpaPenetrationDepthSolver", "DISABLE_UNCOMMON"},
            {"GjkPairDetector", "DISABLE_UNCOMMON"},
            {"Hacd", "DISABLE_HACD"},
            {"HeightfieldTerrainShape", "DISABLE_UNCOMMON"},
            {"Hinge2Constraint", "DISABLE_CONSTRAINTS"},
            {"HingeConstraint", "DISABLE_CONSTRAINTS"},
            {"IDebugDraw", "DISABLE_DEBUGDRAW"},
            {"KinematicCharacterController", "DISABLE_UNCOMMON"},
            {"LemkeSolver", "DISABLE_UNCOMMON"},
            {"Material", "DISABLE_UNCOMMON"},
            {"MinkowskiPenetrationDepthSolver", "DISABLE_UNCOMMON"},
            {"MinkowskiSumShape", "DISABLE_UNCOMMON"},
            {"MlcpSolver", "DISABLE_MLCP"},
            {"MlcpSolverInterface", "DISABLE_MLCP"},
            {"MultiBody", "DISABLE_FEATHERSTONE"},
            {"MultiBodyConstraint", "DISABLE_FEATHERSTONE"},
            {"MultiBodyConstraintSolver", "DISABLE_FEATHERSTONE"},
            {"MultiBodyDynamicsWorld", "DISABLE_FEATHERSTONE"},
            {"MultiBodyJointLimitConstraint", "DISABLE_FEATHERSTONE"},
            {"MultiBodyJointMotor", "DISABLE_FEATHERSTONE"},
            {"MultiBodyLink", "DISABLE_FEATHERSTONE"},
            {"MultiBodyLinkCollider", "DISABLE_FEATHERSTONE"},
            {"MultiBodyPoint2Point", "DISABLE_FEATHERSTONE"},
            {"MultiBodySolverConstraint", "DISABLE_FEATHERSTONE"},
            {"MultimaterialTriangleMeshShape", "DISABLE_UNCOMMON"},
            {"NncgConstraintSolver", "DISABLE_CONSTRAINTS"},
            {"OptimizedBvh", "DISABLE_BVH"},
            {"Point2PointConstraint", "DISABLE_CONSTRAINTS"},
            {"PointCollector", "DISABLE_UNCOMMON"},
            {"PoolAllocator", "DISABLE_UNCOMMON"},
            {"QuantizedBvh", "DISABLE_BVH"},
            {"RaycastVehicle", "DISABLE_VEHICLE"},
            {"ScaledBvhTriangleMeshShape", "DISABLE_UNCOMMON"},
            {"Serializer", "DISABLE_SERIALIZE"},
            {"ShapeHull", "DISABLE_UNCOMMON"},
            {"SimulationIslandManager", "DISABLE_UNCOMMON"},
            {"SliderConstraint", "DISABLE_CONSTRAINTS"},
            {"SoftBody", "DISABLE_SOFTBODY"},
            {"SoftBodyConcaveCollisionAlgorithm", "DISABLE_SOFTBODY"},
            {"SoftBodyHelpers", "DISABLE_SOFTBODY"},
            {"SoftBodyRigidBodyCollisionConfiguration", "DISABLE_SOFTBODY"},
            {"SoftBodySolver", "DISABLE_SOFTBODY"},
            {"SoftBodySolverVertexBuffer", "DISABLE_SOFTBODY"},
            {"SoftRigidDynamicsWorld", "DISABLE_SOFTBODY"},
            {"Solve2LinearConstraint", "DISABLE_CONSTRAINTS"},
            {"SparseSdf", "DISABLE_SOFTBODY"},
            {"SphereBoxCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"SphereSphereCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"SphereTriangleCollisionAlgorithm", "DISABLE_COLLISION_ALGORITHMS"},
            {"SphereTriangleDetector", "DISABLE_UNCOMMON"},
            {"TriangleBuffer", "DISABLE_UNCOMMON"},
            {"TriangleIndexVertexMaterialArray", "DISABLE_UNCOMMON"},
            {"TriangleShape", "DISABLE_UNCOMMON"},
            {"TriangleShapeEx", "DISABLE_GIMPACT"},
            {"TypedConstraint", "DISABLE_CONSTRAINTS"},
            {"UnionFind", "DISABLE_UNCOMMON"},
            {"UniversalConstraint", "DISABLE_CONSTRAINTS"},
            {"VehicleRaycaster", "DISABLE_VEHICLE"},
            {"VoronoiSimplexSolver", "DISABLE_UNCOMMON"},
            {"WheelInfo", "DISABLE_VEHICLE"},
            {"WorldImporter", "DISABLE_SERIALIZE"}
        };

        public CppCliWriter(IEnumerable<HeaderDefinition> headerDefinitions, string namespaceName)
            : base(headerDefinitions, namespaceName)
        {
        }

        void WriteTabs(int n, bool source = false)
        {
            for (int i = 0; i < n; i++)
            {
                if (source)
                {
                    sourceWriter.Write('\t');
                    _sourceLineLength += TabWidth;
                }
                else
                {
                    headerWriter.Write('\t');
                    _headerLineLength += TabWidth;
                }
            }
        }

        void HeaderWrite(string s)
        {
            headerWriter.Write(s);
            _headerLineLength += s.Length;
        }

        void HeaderWriteLine(string s = "")
        {
            HeaderWrite(s);
            headerWriter.WriteLine();
            _headerLineLength = 0;
        }

        void HeaderWriteLine(char c)
        {
            headerWriter.WriteLine(c);
            _headerLineLength = 0;
        }

        void SourceWrite(string s)
        {
            sourceWriter.Write(s);
            _sourceLineLength += s.Length;
        }

        void SourceWrite(char c)
        {
            sourceWriter.Write(c);
            _sourceLineLength += c.Equals('\t') ? TabWidth : 1;
        }

        void SourceWriteLine(string s = "")
        {
            sourceWriter.WriteLine(s);
            _sourceLineLength = 0;
        }

        void SourceWriteLine(char c)
        {
            sourceWriter.WriteLine(c);
            _sourceLineLength = 0;
        }

        void Write(string s)
        {
            HeaderWrite(s);
            SourceWrite(s);
        }

        void Write(char c, bool source = true)
        {
            headerWriter.Write(c);
            _headerLineLength += 1;
            if (source)
            {
                sourceWriter.Write(c);
                _sourceLineLength += 1;
            }
        }

        void WriteLine(string s)
        {
            HeaderWriteLine(s);
            SourceWriteLine(s);
        }

        void EnsureAccess(int level, ref RefAccessSpecifier current, RefAccessSpecifier required, bool withWhiteSpace = true)
        {
            if (current != required)
            {
                if (withWhiteSpace)
                {
                    EnsureHeaderWhiteSpace();
                }

                WriteTabs(level);
                if (required == RefAccessSpecifier.Internal)
                {
                    HeaderWriteLine("internal:");
                }
                else if (required == RefAccessSpecifier.Private)
                {
                    HeaderWriteLine("private:");
                }
                else if (required == RefAccessSpecifier.Public)
                {
                    HeaderWriteLine("public:");
                }
                else if (required == RefAccessSpecifier.Protected)
                {
                    HeaderWriteLine("protected:");
                }
                current = required;
            }
        }

        void EnsureHeaderWhiteSpace()
        {
            if (!hasHeaderWhiteSpace)
            {
                HeaderWriteLine();
                hasHeaderWhiteSpace = true;
            }
        }

        void EnsureSourceWhiteSpace()
        {
            if (!hasSourceWhiteSpace)
            {
                SourceWriteLine();
                hasSourceWhiteSpace = true;
            }
        }

        void OutputMethodMarshal(MethodDefinition method, int numParameters)
        {
            if (method.IsConstructor)
            {
                SourceWrite(method.Parent.FullName);
            }
            else
            {
                SourceWrite(method.Name);
            }
            SourceWrite('(');
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                string marshal = BulletParser.GetTypeMarshalCppCli(param);
                if (!string.IsNullOrEmpty(marshal))
                {
                    SourceWrite(marshal);
                }
                else if (param.Type.IsBasic)
                {
                    SourceWrite(param.ManagedName);
                }
                else
                {
                    if (param.Type.IsPointer || param.Type.IsReference)
                    {
                        if (param.Type.IsReference)
                        {
                            // Dereference
                            SourceWrite('*');
                        }

                        if (param.Type.Referenced.Target != null &&
                            param.Type.Referenced.Target.BaseClass != null)
                        {
                            // Cast native pointer from base class
                            SourceWrite(string.Format("({0}*)", param.Type.Referenced.FullName));
                        }
                    }
                    SourceWrite(param.ManagedName);
                    if (param.Type.IsPointer && param.Type.ManagedName.Equals("void"))
                    {
                        SourceWrite(".ToPointer()");
                    }
                    else
                    {
                        SourceWrite("->_native");
                    }
                }

                // Any more parameters?
                if (i != numParameters - 1)
                {
                    if (_sourceLineLength >= LineBreakWidth)
                    {
                        SourceWriteLine(",");
                        WriteTabs(2, true);
                    }
                    else
                    {
                        SourceWrite(", ");
                    }
                }
            }
            SourceWrite(')');
        }

        void OutputMethod(MethodDefinition method, int level, int numOptionalParams = 0)
        {
            var parentClass = method.Parent;

            // No whitespace between get/set methods
            if (!(method.Property != null &&
                method.Equals(method.Property.Setter)))
            {
                EnsureSourceWhiteSpace();
                hasHeaderWhiteSpace = false;
            }

            // #ifndef DISABLE_FEATURE
            bool hasConditional = false;
            if (method.Property == null)
            {
                foreach (var param in method.Parameters)
                {
                    string typeConditional = GetTypeConditional(param.Type, parentClass.Header);
                    if (typeConditional != null)
                    {
                        Write("#ifndef ");
                        WriteLine(typeConditional);
                        hasSourceWhiteSpace = true;
                        hasConditional = true;
                    }
                }
            }

            WriteTabs(level + 1);

            // "static"
            if (method.IsStatic)
            {
                HeaderWrite("static ");
            }

            // Definition: return type
            if (!method.IsConstructor)
            {
                var returnType = method.ReturnType;

                if (method.Property != null)
                {
                    if (method.Equals(method.Property.Getter))
                    {
                        // If property name matches type name, resolve ambiguity
                        if (method.Property.Name.Equals(method.Property.Type.ManagedName))
                        {
                            HeaderWrite(NamespaceName + "::");
                        }

                        // Getter with parameter for return value
                        if (method.Parameters.Length == 1)
                        {
                            returnType = method.Parameters[0].Type;
                        }
                    }
                }

                Write(BulletParser.GetTypeRefName(returnType));
                Write(' ');
            }

            // Definition: name
            SourceWrite(parentClass.FullNameManaged);
            SourceWrite("::");
            if (method.IsConstructor)
            {
                Write(parentClass.ManagedName);
            }
            else
            {
                if (method.Property != null)
                {
                    SourceWrite(method.Property.Name);
                    SourceWrite("::");
                    if (method.Property.Getter.Equals(method))
                    {
                        Write("get");
                    }
                    else
                    {
                        Write("set");
                    }
                }
                else
                {
                    Write(method.ManagedName);
                }
            }
            Write('(');

            // Definition: parameters
            int numParameters = method.Parameters.Length - numOptionalParams;
            // Getters with parameter for return value
            if (numParameters == 1 && method.Property != null && method.Equals(method.Property.Getter))
            {
                numParameters = 0;
            }
            bool hasOptionalParam = false;
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                Write(BulletParser.GetTypeRefName(param.Type));
                Write(' ');
                Write(param.ManagedName);

                if (param.IsOptional)
                {
                    hasOptionalParam = true;
                }

                if (i != numParameters - 1)
                {
                    if (_headerLineLength >= LineBreakWidth)
                    {
                        HeaderWriteLine(",");
                        WriteTabs(level + 2);
                    }
                    else
                    {
                        HeaderWrite(", ");
                    }

                    if (_sourceLineLength >= LineBreakWidth)
                    {
                        SourceWriteLine(",");
                        WriteTabs(1, true);
                    }
                    else
                    {
                        SourceWrite(", ");
                    }
                }
            }
            HeaderWriteLine(");");
            SourceWriteLine(')');


            // Constructor chaining
            bool doConstructorChaining = false;
            if (method.IsConstructor && parentClass.BaseClass != null)
            {
                // If there is no need for marshalling code, we can chain constructors
                doConstructorChaining = true;
                foreach (var param in method.Parameters)
                {
                    if (BulletParser.TypeRequiresMarshal(param.Type))
                    {
                        doConstructorChaining = false;
                        break;
                    }
                }

                WriteTabs(1, true);
                SourceWrite(": ");
                SourceWrite(parentClass.BaseClass.ManagedName);
                SourceWrite('(');

                if (doConstructorChaining)
                {
                    SourceWrite("new ");
                    OutputMethodMarshal(method, numParameters);
                    if (parentClass.BaseClass.HasPreventDelete)
                    {
                        SourceWrite(", false");
                    }
                }
                else
                {
                    SourceWrite('0');
                }

                SourceWriteLine(')');
            }

            // Method definition
            SourceWriteLine('{');

            if (!doConstructorChaining)
            {
                // Type marshalling prologue
                bool needTypeMarshalEpilogue = false;
                if (method.Field == null)
                {
                    for (int i = 0; i < numParameters; i++)
                    {
                        var param = method.Parameters[i];
                        string prologue = BulletParser.GetTypeMarshalPrologueCppCli(param);
                        if (!string.IsNullOrEmpty(prologue))
                        {
                            WriteTabs(1, true);
                            SourceWriteLine(prologue);
                        }

                        // Do we need a type marshalling epilogue?
                        if (!needTypeMarshalEpilogue)
                        {
                            string epilogue = BulletParser.GetTypeMarshalEpilogueCppCli(param);
                            if (!string.IsNullOrEmpty(epilogue))
                            {
                                needTypeMarshalEpilogue = true;
                            }
                        }
                    }
                }

                WriteTabs(1, true);
                if (method.IsConstructor)
                {
                    SourceWrite("_native = new ");
                }
                else
                {
                    if (!method.IsVoid)
                    {
                        //if (method.ReturnType.IsBasic || method.ReturnType.Referenced != null)
                        if (needTypeMarshalEpilogue)
                        {
                            // Return after epilogue (cleanup)
                            SourceWrite(BulletParser.GetTypeRefName(method.ReturnType));
                            SourceWrite(" ret = ");
                        }
                        else
                        {
                            // Return immediately
                            SourceWrite("return ");
                        }
                        SourceWrite(BulletParser.GetTypeMarshalConstructorStart(method));
                    }
                    else
                    {
                        if (method.Property != null && method.Equals(method.Property.Getter))
                        {
                            SourceWrite(BulletParser.GetTypeMarshalConstructorStart(method));
                        }
                    }
                }

                // Native is defined as static_cast<className*>(_native)
                string nativePointer = (parentClass.BaseClass != null) ? "Native" : "_native";

                if (method.Field != null)
                {
                    if (method.Equals(method.Property.Getter))
                    {
                        SourceWrite(nativePointer);
                        SourceWrite("->");
                        SourceWrite(method.Field.Name);
                    }

                    var setter = method.Property.Setter;
                    if (setter != null && method.Equals(setter))
                    {
                        var param = method.Parameters[0];
                        var fieldSet = BulletParser.GetTypeMarshalFieldSetCppCli(method.Field, param, nativePointer);
                        if (!string.IsNullOrEmpty(fieldSet))
                        {
                            SourceWrite(fieldSet);
                        }
                        else
                        {
                            SourceWrite(string.Format("{0}->{1} = ", nativePointer, method.Field.Name));
                            if (param.Type.IsPointer || param.Type.IsReference)
                            {
                                if (param.Type.IsReference)
                                {
                                    // Dereference
                                    SourceWrite('*');
                                }

                                if (param.Type.Referenced.Target != null &&
                                    param.Type.Referenced.Target.BaseClass != null)
                                {
                                    // Cast native pointer from base class
                                    SourceWrite(string.Format("({0}*)", param.Type.Referenced.FullName));
                                }
                            }
                            SourceWrite(param.ManagedName);
                            if (!param.Type.IsBasic)
                            {
                                SourceWrite("->_native");
                            }
                        }
                    }
                }
                else
                {
                    if (!method.IsConstructor)
                    {
                        if (method.IsStatic)
                        {
                            SourceWrite(parentClass.FullName);
                            SourceWrite("::");
                        }
                        else
                        {
                            SourceWrite(nativePointer);
                            SourceWrite("->");
                        }
                    }
                    OutputMethodMarshal(method, numParameters);
                }
                if (!method.IsConstructor && !method.IsVoid)
                {
                    SourceWrite(BulletParser.GetTypeMarshalConstructorEnd(method));
                }
                SourceWriteLine(';');

                // Write type marshalling epilogue
                if (needTypeMarshalEpilogue)
                {
                    for (int i = 0; i < numParameters; i++)
                    {
                        var param = method.Parameters[i];
                        string epilogue = BulletParser.GetTypeMarshalEpilogueCppCli(param);
                        if (!string.IsNullOrEmpty(epilogue))
                        {
                            WriteTabs(1, true);
                            SourceWriteLine(epilogue);
                        }
                    }
                    if (!method.IsVoid)
                    {
                        WriteTabs(1, true);
                        SourceWriteLine("return ret;");
                    }
                }
            }
            SourceWriteLine('}');
            hasSourceWhiteSpace = false;

            // #endif // DISABLE_FEATURE
            if (hasConditional)
            {
                foreach (var param in method.Parameters)
                {
                    string typeConditional = GetTypeConditional(param.Type, method.Parent.Header);
                    if (typeConditional != null)
                    {
                        WriteLine("#endif");
                        hasHeaderWhiteSpace = true;
                    }
                }
            }

            // If there are optional parameters, then output all possible combinations of calls
            if (hasOptionalParam)
            {
                OutputMethod(method, level, numOptionalParams + 1);
            }
        }

        void OutputClasses(IList<ClassDefinition> classes, ref RefAccessSpecifier currentAccess, int level)
        {
            int classCount = 0;
            foreach (var cl in classes)
            {
                if (IsExcludedClass(cl))
                {
                    continue;
                }
                if (classCount != 0)
                {
                    SourceWriteLine();
                }
                if (level != 0)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                }
                OutputClass(cl, level + 1);
                classCount++;
            }
        }

        bool IsExcludedClass(ClassDefinition c)
        {
            return c.IsTypedef || c.IsPureEnum || c.IsExcluded;
        }

        void OutputClass(ClassDefinition c, int level)
        {
            EnsureHeaderWhiteSpace();
            EnsureSourceWhiteSpace();

            // Write access modifier
            WriteTabs(level);
            if (level == 1)
            {
                HeaderWrite("public ");
            }

            // Write class definition
            HeaderWrite("ref class ");
            HeaderWrite(c.ManagedName);
            if (c.IsAbstract)
            {
                HeaderWrite(" abstract");
            }
            if (c.BaseClass != null)
            {
                HeaderWrite(" : ");
                HeaderWriteLine(c.BaseClass.ManagedName);
            }
            else
            {
                if (c.IsTrackingDisposable)
                {
                    HeaderWriteLine(" : ITrackingDisposable");
                }
                else
                {
                    // In C++/CLI, IDisposable is inherited automatically if the destructor and finalizer are defined
                    //HeaderWriteLine(" : IDisposable");
                    if (c.IsStaticClass)
                    {
                        HeaderWrite(" sealed");
                    }
                    HeaderWriteLine();
                }
            }

            WriteTabs(level);
            HeaderWriteLine("{");
            //hasHeaderWhiteSpace = false;

            // Default access for ref class
            var currentAccess = RefAccessSpecifier.Private;

            // Write child classes
            if (c.Classes.Count != 0)
            {
                OutputClasses(c.Classes, ref currentAccess, level);
                currentAccess = RefAccessSpecifier.Public;
                SourceWriteLine();
            }

            // Classes without instances
            if (c.IsStaticClass)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteTabs(level + 1);
                HeaderWriteLine(string.Format("{0}() {{}}", c.ManagedName));
            }

            // Downcast native pointer if any methods in a derived class use it
            if (c.BaseClass != null && c.Methods.Any(m => !m.IsConstructor && !m.IsStatic))
            {
                EnsureSourceWhiteSpace();
                SourceWriteLine(string.Format("#define Native static_cast<{0}*>(_native)", c.FullName));
                hasSourceWhiteSpace = false;
            }

            // Write the unmanaged pointer to the base class
            if (c.BaseClass == null && !c.IsStaticClass)
            {
                if (c.Classes.Count != 0)
                {
                    HeaderWriteLine();
                }
                if (c.IsTrackingDisposable)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                    WriteTabs(level + 1);
                    HeaderWriteLine("virtual event EventHandler^ OnDisposing;");
                    WriteTabs(level + 1);
                    HeaderWriteLine("virtual event EventHandler^ OnDisposed;");
                    hasHeaderWhiteSpace = false;
                }
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                WriteTabs(level + 1);
                HeaderWrite(c.FullName);
                HeaderWriteLine("* _native;");
                hasHeaderWhiteSpace = false;
            }

            EnsureHeaderWhiteSpace();
            EnsureSourceWhiteSpace();

            // Private fields
            // _isDisposed flag
            if (c.IsTrackingDisposable)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteTabs(level + 1);
                HeaderWriteLine("bool _isDisposed;");
                hasHeaderWhiteSpace = false;
            }
            // _preventDelete flag
            if (c.HasPreventDelete)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteTabs(level + 1);
                HeaderWriteLine("bool _preventDelete;");
                hasHeaderWhiteSpace = false;
            }

            // Write unmanaged constructor
            // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.
            if (!c.NoInternalConstructor && !c.IsStaticClass)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                WriteTabs(level + 1);
                SourceWrite(c.FullNameManaged);
                SourceWrite("::");
                Write(c.ManagedName);
                Write('(');
                Write(c.FullName);
                Write("* native)");
                HeaderWriteLine(';');
                SourceWriteLine();
                if (c.BaseClass != null)
                {
                    WriteTabs(1, true);
                    SourceWrite(": ");
                    SourceWrite(c.BaseClass.ManagedName);
                    SourceWriteLine("(native)");
                }
                SourceWriteLine('{');
                if (c.BaseClass == null)
                {
                    WriteTabs(1, true);
                    SourceWriteLine("_native = native;");
                }
                SourceWriteLine('}');
                hasHeaderWhiteSpace = false;
                hasSourceWhiteSpace = false;
            }

            // Write destructor & finalizer
            if (c.BaseClass == null && !c.IsStaticClass)
            {
                // ECMA-372 19.13.2: "The access-specifier of a finalizer in a ref class is ignored."
                WriteTabs(level + 1);
                HeaderWriteLine(string.Format("!{0}();", c.ManagedName));
                // ECMA-372 19.13.1: "The access-specifier of a destructor in a ref class is ignored."
                WriteTabs(level + 1);
                HeaderWriteLine(string.Format("~{0}();", c.ManagedName));
                hasHeaderWhiteSpace = false;

                EnsureSourceWhiteSpace();
                SourceWriteLine(string.Format("{0}::~{1}()", c.FullNameManaged, c.ManagedName));
                SourceWriteLine('{');
                SourceWriteLine(string.Format("\tthis->!{0}();", c.ManagedName));
                SourceWriteLine('}');
                SourceWriteLine();

                SourceWriteLine(string.Format("{0}::!{1}()", c.FullNameManaged, c.ManagedName));
                SourceWriteLine('{');
                if (c.IsTrackingDisposable)
                {
                    SourceWriteLine("\tif (this->IsDisposed)");
                    SourceWriteLine("\t\treturn;");
                    SourceWriteLine();
                    SourceWriteLine("\tOnDisposing(this, nullptr);");
                    SourceWriteLine();
                }
                if (c.HasPreventDelete)
                {
                    SourceWriteLine("\tif (!_preventDelete)");
                    SourceWriteLine("\t{");
                    SourceWriteLine("\t\tdelete _native;");
                    SourceWriteLine("\t}");
                }
                else
                {
                    SourceWriteLine("\tdelete _native;");
                }
                if (c.IsTrackingDisposable)
                {
                    SourceWriteLine("\t_isDisposed = true;");
                    SourceWriteLine();
                    SourceWriteLine("\tOnDisposed(this, nullptr);");
                }
                else
                {
                    SourceWriteLine("\t_native = NULL;");
                }
                SourceWriteLine('}');
                hasSourceWhiteSpace = false;
            }

            // Write constructors
            int constructorCount = 0;
            foreach (MethodDefinition method in c.Methods.Where(m => m.IsConstructor))
            {
                if (!c.HidePublicConstructors)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                    OutputMethod(method, level);
                }
                constructorCount++;
            }

            // Write default constructor
            if (constructorCount == 0 && !c.IsAbstract && !c.HidePublicConstructors && !c.IsStaticClass)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);

                MethodDefinition constructor = new MethodDefinition(c.Name, c, 0);
                constructor.IsConstructor = true;
                OutputMethod(constructor, level);
                constructorCount++;
            }

            // Write methods
            if (c.Methods.Count - constructorCount != 0)
            {
                EnsureHeaderWhiteSpace();

                foreach (MethodDefinition method in c.Methods)
                {
                    if (!method.IsConstructor && method.Property == null)
                    {
                        EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                        OutputMethod(method, level);
                    }
                }
            }

            // Write properties (includes unmanaged fields and getters/setters)
            foreach (PropertyDefinition prop in c.Properties)
            {
                string typeConditional = GetTypeConditional(prop.Type, c.Header);
                if (typeConditional != null)
                {
                    Write("#ifndef ");
                    WriteLine(typeConditional);
                    hasSourceWhiteSpace = true;
                }
                else
                {
                    EnsureHeaderWhiteSpace();
                }

                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);

                string typeRefName = BulletParser.GetTypeRefName(prop.Type);

                WriteTabs(level + 1);
                HeaderWrite("property ");
                HeaderWrite(typeRefName);
                HeaderWrite(" ");
                HeaderWriteLine(prop.Name);
                WriteTabs(level + 1);
                HeaderWriteLine("{");

                // Getter/Setter
                OutputMethod(prop.Getter, level + 1);
                if (prop.Setter != null)
                {
                    OutputMethod(prop.Setter, level + 1);
                }

                WriteTabs(level + 1);
                HeaderWriteLine("}");

                if (typeConditional != null)
                {
                    WriteLine("#endif");
                    hasSourceWhiteSpace = false;
                }

                hasHeaderWhiteSpace = false;
            }

            WriteTabs(level);
            HeaderWriteLine("};");
            hasHeaderWhiteSpace = false;
        }

        public override void Output()
        {
            string outDirectory = NamespaceName + "_cppcli";

            foreach (HeaderDefinition header in headerDefinitions)
            {
                if (header.Classes.All(c => IsExcludedClass(c)))
                {
                    continue;
                }

                Directory.CreateDirectory(outDirectory);
                var headerFile = new FileStream(outDirectory + "\\" + header.ManagedName + ".h", FileMode.Create, FileAccess.Write);
                headerWriter = new StreamWriter(headerFile);
                HeaderWriteLine("#pragma once");
                HeaderWriteLine();

                var sourceFile = new FileStream(outDirectory + "\\" + header.ManagedName + ".cpp", FileMode.Create, FileAccess.Write);
                sourceWriter = new StreamWriter(sourceFile);
                SourceWriteLine("#include \"StdAfx.h\"");
                SourceWriteLine();

                if (headerConditional.ContainsKey(header.ManagedName))
                {
                    SourceWrite("#ifndef ");
                    SourceWriteLine(headerConditional[header.ManagedName]);
                    SourceWriteLine();
                }

                // Write includes
                if (header.Includes.Count != 0)
                {
                    foreach (HeaderDefinition include in header.Includes)
                    {
                        HeaderWriteLine(string.Format("#include \"{0}.h\"", include.ManagedName));
                    }
                    HeaderWriteLine();
                }

                // Write namespace
                HeaderWrite("namespace ");
                HeaderWriteLine(NamespaceName);
                HeaderWriteLine("{");
                hasHeaderWhiteSpace = true;

                // Find forward references
                var forwardRefs = new List<ClassDefinition>();
                foreach (ClassDefinition c in header.Classes)
                {
                    FindForwardReferences(forwardRefs, c);
                }

                // Remove redundant forward references (header file already included)
                forwardRefs.RemoveAll(fr => header.Includes.Contains(fr.Header));
                forwardRefs.Sort((r1, r2) => r1.ManagedName.CompareTo(r2.ManagedName));

                // Write forward references
                var forwardRefHeaders = new List<HeaderDefinition>();
                foreach (ClassDefinition c in forwardRefs)
                {
                    HeaderWriteLine(string.Format("\tref class {0};", c.ManagedName));
                    if (!forwardRefHeaders.Contains(c.Header))
                    {
                        forwardRefHeaders.Add(c.Header);
                    }
                    hasHeaderWhiteSpace = false;
                }
                forwardRefHeaders.Add(header);
                forwardRefHeaders.Sort((r1, r2) => r1.ManagedName.CompareTo(r2.ManagedName));

                // Write statements to include forward referenced types
                if (forwardRefHeaders.Count != 0)
                {
                    foreach (HeaderDefinition refHeader in forwardRefHeaders)
                    {
                        bool hasHeaderConditional = false;
                        if (headerConditional.ContainsKey(refHeader.ManagedName))
                        {
                            hasHeaderConditional = true;
                            if (headerConditional.ContainsKey(header.ManagedName) &&
                                headerConditional[refHeader.ManagedName] == headerConditional[header.ManagedName])
                            {
                                hasHeaderConditional = false;
                            }
                        }
                        if (hasHeaderConditional)
                        {
                            SourceWrite("#ifndef ");
                            SourceWriteLine(headerConditional[refHeader.ManagedName]);
                        }
                        SourceWriteLine(string.Format("#include \"{0}.h\"", refHeader.ManagedName));
                        if (hasHeaderConditional)
                        {
                            SourceWriteLine("#endif");
                        }
                    }
                    hasSourceWhiteSpace = false;
                }

                // Write classes
                var currentAccess = RefAccessSpecifier.Public;
                OutputClasses(header.Classes, ref currentAccess, 0);

                if (headerConditional.ContainsKey(header.ManagedName))
                {
                    SourceWriteLine();
                    SourceWriteLine("#endif");
                }

                HeaderWriteLine("};");
                headerWriter.Dispose();
                headerFile.Dispose();
                sourceWriter.Dispose();
                sourceFile.Dispose();
            }

            Console.WriteLine("Write complete");
        }

        // These do no need forward references
        public List<string> PrecompiledHeaderReferences = new List<string>(new[] { "Vector3", "Matrix3x3", "Quaternion", "Transform", "Vector4" });

        void AddForwardReference(List<ClassDefinition> forwardRefs, TypeRefDefinition type, HeaderDefinition header)
        {
            if (type.IsBasic)
            {
                return;
            }

            if (type.IsPointer || type.IsReference)
            {
                AddForwardReference(forwardRefs, type.Referenced, header);
                return;
            }

            if (type.Target == null)
            {
                return;
            }
            if (type.Target.IsExcluded || 
                forwardRefs.Contains(type.Target) ||
                PrecompiledHeaderReferences.Contains(type.Target.ManagedName))
            {
                return;
            }

            // Forward ref to class in another header
            if (type.Target.Header != header)
            {
                forwardRefs.Add(type.Target);
            }
        }

        void FindForwardReferences(List<ClassDefinition> forwardRefs, ClassDefinition c)
        {
            foreach (PropertyDefinition prop in c.Properties)
            {
                AddForwardReference(forwardRefs, prop.Type, c.Header);
            }

            foreach (MethodDefinition method in c.Methods)
            {
                if (method.IsConstructor && c.HidePublicConstructors)
                {
                    continue;
                }

                AddForwardReference(forwardRefs, method.ReturnType, c.Header);

                foreach (ParameterDefinition param in method.Parameters)
                {
                    AddForwardReference(forwardRefs, param.Type, c.Header);
                }
            }

            foreach (ClassDefinition cl in c.Classes)
            {
                FindForwardReferences(forwardRefs, cl);
            }
        }

        // If the type is defined in a conditionally compiled header, return the condition string.
        string GetTypeConditional(TypeRefDefinition type)
        {
            var target = type.Target;
            if (target == null && type.Referenced != null)
            {
                target = type.Referenced.Target;
            }

            if (target != null && headerConditional.ContainsKey(target.Header.ManagedName))
            {
                return headerConditional[target.Header.ManagedName];
            }

            return null;
        }

        // Return condition unless type is already used under the same condition.
        string GetTypeConditional(TypeRefDefinition type, HeaderDefinition header)
        {
            string typeConditional = GetTypeConditional(type);
            if (typeConditional != null && headerConditional.ContainsKey(header.ManagedName))
            {
                if (headerConditional[header.ManagedName].Equals(typeConditional))
                {
                    return null;
                }
            }
            return typeConditional;
        }
    }
}
