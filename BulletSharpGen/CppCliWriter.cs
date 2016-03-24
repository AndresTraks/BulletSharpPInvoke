using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    class CppCliWriter : WrapperWriter
    {
        private const int LineBreakWidth = 80;

        // Conditional compilation (#ifndef DISABLE_FEATURE)
        Dictionary<string, string> headerConditionals = new Dictionary<string, string>
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

        // These do no need forward references
        public List<string> PrecompiledHeaderReferences =
            new List<string>(new[] {"Vector3", "Matrix3x3", "Quaternion", "Transform", "Vector4"});

        public CppCliWriter(WrapperProject project)
            : base(project)
        {
        }

        void EnsureAccess(int level, ref RefAccessSpecifier current, RefAccessSpecifier required,
            bool withWhiteSpace = true)
        {
            if (current == required) return;

            if (withWhiteSpace)
            {
                EnsureHeaderWhiteSpace();
            }

            WriteTabs(level, WriteTo.Header);
            WriteLine($"{required.ToString().ToLower()}:", WriteTo.Header);
            current = required;
        }

        void EnsureHeaderWhiteSpace()
        {
            if (hasHeaderWhiteSpace) return;

            WriteLine(WriteTo.Header);
            hasHeaderWhiteSpace = true;
        }

        void EnsureSourceWhiteSpace()
        {
            if (hasSourceWhiteSpace) return;

            WriteLine(WriteTo.Source);
            hasSourceWhiteSpace = true;
        }

        private void WriteMethodMarshal(MethodDefinition method, int numParameters)
        {
            if (method.IsConstructor)
            {
                Write(method.Parent.FullyQualifiedName);
            }
            else
            {
                Write(method.Name);
            }
            Write('(');
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                string marshal = BulletParser.GetTypeMarshalCppCli(param);
                if (!string.IsNullOrEmpty(marshal))
                {
                    Write(marshal);
                }
                else if (param.Type.IsBasic)
                {
                    Write(param.ManagedName);
                }
                else
                {
                    if (param.Type.IsPointer || param.Type.IsReference)
                    {
                        if (param.Type.IsReference)
                        {
                            // Dereference
                            Write('*');
                        }

                        if (param.Type.Referenced.Target?.BaseClass != null)
                        {
                            // Cast native pointer from base class
                            Write($"({param.Type.Referenced.FullName}*)");
                        }
                    }
                    Write(param.ManagedName);
                    if (param.Type.IsPointer && param.Type.ManagedName.Equals("void"))
                    {
                        Write(".ToPointer()");
                    }
                    else
                    {
                        Write("->_native");
                    }
                }

                // Insert comma if there are more parameters
                if (i != numParameters - 1)
                {
                    if (LineLengths[WriteTo.Source] >= LineBreakWidth)
                    {
                        WriteLine(",");
                        WriteTabs(2);
                    }
                    else
                    {
                        Write(", ");
                    }
                }
            }
            Write(')');
        }

        void WriteMethodDefinition(MethodDefinition method, int numParameters)
        {
            var parentClass = method.Parent;

            // Type marshalling prologue
            bool needTypeMarshalEpilogue = false;
            if (method.Field == null)
            {
                foreach (var param in method.Parameters)
                {
                    string prologue = BulletParser.GetTypeMarshalPrologueCppCli(param);
                    if (!string.IsNullOrEmpty(prologue))
                    {
                        WriteTabs(1);
                        WriteLine(prologue);
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

            WriteTabs(1);
            if (method.IsConstructor)
            {
                Write("_native = new ");
            }
            else if (!method.IsVoid)
            {
                //if (method.ReturnType.IsBasic || method.ReturnType.Referenced != null)
                if (needTypeMarshalEpilogue)
                {
                    // Return after epilogue (cleanup)
                    Write($"{BulletParser.GetTypeRefName(method.ReturnType)} ret = ");
                }
                else
                {
                    // Return immediately
                    Write("return ");
                }
                Write(BulletParser.GetTypeMarshalConstructorStart(method));
            }
            else if (method.Property != null && method.Equals(method.Property.Getter))
            {
                Write(BulletParser.GetTypeMarshalConstructorStart(method));
            }

            // Native is defined as static_cast<className*>(_native)
            string nativePointer = (parentClass.BaseClass != null) ? "Native" : "_native";

            if (method.Field != null)
            {
                var property = method.Property;
                if (method.Equals(property.Getter))
                {
                    CachedProperty cachedProperty;
                    if (method.Parent.CachedProperties.TryGetValue(property.Name, out cachedProperty))
                    {
                        Write(cachedProperty.CacheFieldName);
                    }
                    else
                    {
                        Write($"{nativePointer}->{method.Field.Name}");
                    }
                }
                else if (property.Setter != null && method.Equals(property.Setter))
                {
                    var param = method.Parameters[0];
                    var fieldSet = BulletParser.GetTypeMarshalFieldSetCppCli(method.Field, param, nativePointer);
                    if (!string.IsNullOrEmpty(fieldSet))
                    {
                        Write(fieldSet);
                    }
                    else
                    {
                        Write($"{nativePointer}->{method.Field.Name} = ");
                        if (param.Type.IsPointer || param.Type.IsReference)
                        {
                            if (param.Type.IsReference)
                            {
                                // Dereference
                                Write('*');
                            }

                            if (param.Type.Referenced.Target != null &&
                                param.Type.Referenced.Target.BaseClass != null)
                            {
                                // Cast native pointer from base class
                                Write($"({param.Type.Referenced.FullName}*)");
                            }
                        }
                        Write(param.ManagedName);
                        if (!param.Type.IsBasic)
                        {
                            Write("->_native");
                        }
                    }
                }
            }
            else
            {
                if (method.IsConstructor)
                {
                }
                else if (method.IsStatic)
                {
                    Write(parentClass.FullyQualifiedName + "::");
                }
                else
                {
                    Write(nativePointer + "->");
                }
                To = WriteTo.Source;
                WriteMethodMarshal(method, numParameters);
            }
            if (!method.IsConstructor && !method.IsVoid)
            {
                Write(BulletParser.GetTypeMarshalConstructorEnd(method));
            }
            WriteLine(';');

            // Write type marshalling epilogue
            if (needTypeMarshalEpilogue)
            {
                foreach (var param in method.Parameters)
                {
                    string epilogue = BulletParser.GetTypeMarshalEpilogueCppCli(param);
                    if (!string.IsNullOrEmpty(epilogue))
                    {
                        WriteTabs(1);
                        WriteLine(epilogue);
                    }
                }
                if (!method.IsVoid)
                {
                    WriteTabs(1);
                    WriteLine("return ret;");
                }
            }
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

            var prevTo = To;
            To = WriteTo.Header | WriteTo.Source;

            // #ifndef DISABLE_FEATURE
            bool hasConditional = false;
            if (method.Property == null)
            {
                foreach (var param in method.Parameters)
                {
                    string typeConditional = GetTypeConditional(param.Type, parentClass.Header);
                    if (typeConditional != null)
                    {
                        WriteLine($"#ifndef {typeConditional}");
                        hasSourceWhiteSpace = true;
                        hasConditional = true;
                    }
                }
            }

            // Declaration
            WriteTabs(level + 1);

            // "static"
            if (method.IsStatic)
            {
                Write("static ", WriteTo.Header);
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
                            Write(Project.NamespaceName + "::", WriteTo.Header);
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
            string headerMethodName;
            string sourceMethodName;
            if (method.IsConstructor)
            {
                headerMethodName = parentClass.ManagedName;
                sourceMethodName = headerMethodName;
            }
            else if (method.Property != null)
            {
                headerMethodName = method.Property.Getter.Equals(method) ? "get" : "set";
                sourceMethodName = $"{method.Property.Name}::{headerMethodName}";
            }
            else
            {
                headerMethodName = method.ManagedName;
                sourceMethodName = headerMethodName;
            }
            Write($"{headerMethodName}(", WriteTo.Header);
            Write($"{parentClass.FullNameCppCli}::{sourceMethodName}(", WriteTo.Source);

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
                Write($"{BulletParser.GetTypeRefName(param.Type)} {param.ManagedName}");

                if (param.IsOptional)
                {
                    hasOptionalParam = true;
                }

                if (i != numParameters - 1)
                {
                    To = WriteTo.Header;
                    if (LineLengths[To] >= LineBreakWidth)
                    {
                        WriteLine(",");
                        WriteTabs(level + 2);
                    }
                    else
                    {
                        Write(", ");
                    }

                    To = WriteTo.Source;
                    if (LineLengths[To] >= LineBreakWidth)
                    {
                        WriteLine(",");
                        WriteTabs(1);
                    }
                    else
                    {
                        Write(", ");
                    }
                }
            }
            WriteLine(");", WriteTo.Header);
            WriteLine(')', WriteTo.Source);


            // Definition: constructor chaining
            To = WriteTo.Source;
            bool doConstructorChaining = false;
            if (method.IsConstructor && parentClass.BaseClass != null)
            {
                // If there is no need for marshalling code, we can chain constructors
                doConstructorChaining = method.Parameters.All(p => !BulletParser.TypeRequiresMarshal(p.Type));

                WriteTabs(1);
                Write($": {parentClass.BaseClass.ManagedName}(");

                if (doConstructorChaining)
                {
                    Write("new ");
                    WriteMethodMarshal(method, numParameters);
                    if (parentClass.BaseClass.HasPreventDelete)
                    {
                        Write(", false");
                    }
                }
                else
                {
                    Write('0');
                }

                WriteLine(')');
            }

            // Method body
            WriteLine('{');
            if (!doConstructorChaining)
            {
                WriteMethodDefinition(method, numParameters);
            }

            // Cache property values
            if (method.IsConstructor)
            {
                var assignments = new List<string>();
                var methodParent = method.Parent;
                while (methodParent != null)
                {
                    foreach (var cachedProperty in methodParent.CachedProperties.OrderBy(p => p.Key))
                    {
                        foreach (var param in method.Parameters)
                        {
                            if (param.ManagedName.ToLower() == cachedProperty.Key.ToLower()
                                && param.Type.ManagedName == cachedProperty.Value.Property.Type.ManagedName)
                            {
                                string assignment = $"\t{cachedProperty.Value.CacheFieldName} = {param.ManagedName};";
                                assignments.Add(assignment);
                            }
                        }
                    }
                    methodParent = methodParent.BaseClass;
                }
                if (assignments.Count != 0)
                {
                    EnsureSourceWhiteSpace();
                    foreach (string assignment in assignments)
                    {
                        WriteLine(assignment);
                    }
                    hasSourceWhiteSpace = false;
                }
            }
            WriteLine('}');
            hasSourceWhiteSpace = false;

            // #endif // DISABLE_FEATURE
            if (hasConditional)
            {
                foreach (var param in method.Parameters)
                {
                    string typeConditional = GetTypeConditional(param.Type, method.Parent.Header);
                    if (typeConditional != null)
                    {
                        WriteLine("#endif", WriteTo.Header | WriteTo.Source);
                        hasHeaderWhiteSpace = true;
                    }
                }
            }

            // If there are optional parameters, then output all possible combinations of calls
            if (hasOptionalParam)
            {
                OutputMethod(method, level, numOptionalParams + 1);
            }

            To = prevTo;
        }

        void OutputClasses(IList<ClassDefinition> classes, ref RefAccessSpecifier currentAccess, int level)
        {
            bool insertSeparator = false;
            foreach (var @class in classes.Where(c => !IsExcludedClass(c)))
            {
                if (insertSeparator)
                {
                    WriteLine(WriteTo.Source);
                }
                if (level != 0)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                }
                OutputClass(@class, level + 1);
                insertSeparator = true;
            }
        }

        bool IsExcludedClass(ClassDefinition c)
        {
            return c.IsTypedef || c.IsPureEnum || c.IsExcluded;
        }

        void OutputClass(ClassDefinition @class, int level)
        {
            EnsureHeaderWhiteSpace();
            EnsureSourceWhiteSpace();

            var prevTo = To;
            To = WriteTo.Header;

            // Access modifier
            WriteTabs(level);
            if (level == 1) Write("public ");

            // Class definition
            Write($"ref class {@class.ManagedName}");

            // abstract/sealed keywords
            if (@class.IsAbstract) Write(" abstract");
            else if (@class.IsStaticClass) Write(" sealed");

            // Inheritance
            if (@class.BaseClass != null) WriteLine($" : {@class.BaseClass.ManagedName}");
            else if (@class.IsTrackingDisposable) WriteLine(" : ITrackingDisposable");
            else WriteLine();

            // Class body start
            WriteTabs(level);
            WriteLine("{");

            // Default access for ref class
            var currentAccess = RefAccessSpecifier.Private;

            // Nested classes
            if (!@class.NestedClasses.All(IsExcludedClass))
            {
                OutputClasses(@class.NestedClasses, ref currentAccess, level);
                currentAccess = RefAccessSpecifier.Public;
                WriteLine(WriteTo.Source);
            }

            // Private constructor for classes that aren't instanced
            if (@class.IsStaticClass)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteTabs(level + 1);
                WriteLine($"{@class.ManagedName}() {{}}");
                hasHeaderWhiteSpace = false;
            }

            // Downcast native pointer if any methods in a derived class use it
            if (@class.BaseClass != null && @class.Methods.Any(m => !m.IsConstructor && !m.IsStatic && !m.IsExcluded))
            {
                EnsureSourceWhiteSpace();
                WriteLine($"#define Native static_cast<{@class.FullyQualifiedName}*>(_native)", WriteTo.Source);
                hasSourceWhiteSpace = false;
            }

            // Write the native pointer to the base class
            if (@class.BaseClass == null && !@class.IsStaticClass)
            {
                if (@class.NestedClasses.Any(c => !IsExcludedClass(c)))
                {
                    WriteLine();
                }
                if (@class.IsTrackingDisposable)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                    WriteTabs(level + 1);
                    WriteLine("virtual event EventHandler^ OnDisposing;");
                    WriteTabs(level + 1);
                    WriteLine("virtual event EventHandler^ OnDisposed;");
                    hasHeaderWhiteSpace = false;
                }
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                WriteTabs(level + 1);
                Write(@class.FullyQualifiedName);
                WriteLine("* _native;");
                hasHeaderWhiteSpace = false;
            }

            EnsureHeaderWhiteSpace();
            EnsureSourceWhiteSpace();

            // Private fields
            // _isDisposed flag
            if (@class.IsTrackingDisposable)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteTabs(level + 1);
                WriteLine("bool _isDisposed;");
                hasHeaderWhiteSpace = false;
            }
            // _preventDelete flag
            if (@class.HasPreventDelete)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteTabs(level + 1);
                WriteLine("bool _preventDelete;");
                hasHeaderWhiteSpace = false;
            }

            // Cached property fields
            foreach (var cachedProperty in @class.CachedProperties.OrderBy(p => p.Key))
            {
                EnsureAccess(level, ref currentAccess, cachedProperty.Value.Access);
                WriteTabs(level + 1);
                string name = cachedProperty.Key;
                name = char.ToLower(name[0]) + name.Substring(1);
                WriteLine($"{BulletParser.GetTypeRefName(cachedProperty.Value.Property.Type)} _{name};");
                hasHeaderWhiteSpace = false;
            }

            // Constructors and destructors
            if (!@class.IsStaticClass)
            {
                // Write unmanaged constructor
                // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.
                if (!@class.NoInternalConstructor)
                {
                    string constructorName = $"{@class.FullNameCppCli}::{@class.ManagedName}";

                    // Declaration
                    To = WriteTo.Header;
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);
                    WriteTabs(level + 1);
                    WriteLine($"{constructorName}::({@class.FullyQualifiedName}* native);");

                    // Definition
                    To = WriteTo.Source;
                    WriteLine($"{constructorName}::({@class.FullyQualifiedName}* native)");
                    if (@class.BaseClass != null)
                    {
                        WriteLine($"\t: {@class.BaseClass.ManagedName}(native)");
                    }

                    // Body
                    WriteLine('{');
                    if (@class.BaseClass == null) WriteLine("\t_native = native;");
                    WriteLine('}');

                    hasHeaderWhiteSpace = false;
                    hasSourceWhiteSpace = false;
                }

                // Write destructor & finalizer
                if (@class.BaseClass == null)
                {
                    To = WriteTo.Header;

                    // ECMA-372 19.13.1: "The access-specifier of a destructor in a ref class is ignored."
                    WriteTabs(level + 1);
                    WriteLine($"~{@class.ManagedName}();");
                    // ECMA-372 19.13.2: "The access-specifier of a finalizer in a ref class is ignored."
                    WriteTabs(level + 1);
                    WriteLine($"!{@class.ManagedName}();");
                    hasHeaderWhiteSpace = false;

                    To = WriteTo.Source;
                    EnsureSourceWhiteSpace();
                    WriteLine($"{@class.FullNameCppCli}::~{@class.ManagedName}()");
                    WriteLine('{');
                    WriteLine($"\tthis->!{@class.ManagedName}();");
                    WriteLine('}');
                    WriteLine();

                    WriteLine($"{@class.FullNameCppCli}::!{@class.ManagedName}()");
                    WriteLine('{');
                    if (@class.IsTrackingDisposable)
                    {
                        WriteLine("\tif (this->IsDisposed)");
                        WriteLine("\t\treturn;");
                        WriteLine();
                        WriteLine("\tOnDisposing(this, nullptr);");
                        WriteLine();
                    }
                    if (@class.HasPreventDelete)
                    {
                        WriteLine("\tif (!_preventDelete)");
                        WriteLine("\t{");
                        WriteLine("\t\tdelete _native;");
                        WriteLine("\t}");
                    }
                    else
                    {
                        WriteLine("\tdelete _native;");
                    }
                    if (@class.IsTrackingDisposable)
                    {
                        WriteLine("\t_isDisposed = true;");
                        WriteLine();
                        WriteLine("\tOnDisposed(this, nullptr);");
                    }
                    else
                    {
                        WriteLine("\t_native = NULL;");
                    }
                    WriteLine('}');
                    hasSourceWhiteSpace = false;
                }

                // Write public constructors
                if (!@class.HidePublicConstructors && !@class.IsAbstract)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);

                    var constructors = @class.Methods.Where(m => m.IsConstructor);
                    if (constructors.Any())
                    {
                        foreach (var constructor in constructors)
                        {
                            OutputMethod(constructor, level);
                        }
                    }
                }
            }

            // Write non-constructor methods
            var methods = @class.Methods.Where(m => !m.IsConstructor && !m.IsExcluded);
            if (methods.Any())
            {
                EnsureHeaderWhiteSpace();

                foreach (var method in methods)
                {
                    if (method.Property != null) continue;

                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                    OutputMethod(method, level);
                }
            }

            // Write properties (includes unmanaged fields and getters/setters)
            To = WriteTo.Header;
            foreach (PropertyDefinition prop in @class.Properties)
            {
                string typeConditional = GetTypeConditional(prop.Type, @class.Header);
                if (typeConditional != null)
                {
                    WriteLine($"#ifndef {typeConditional}", WriteTo.Header | WriteTo.Source);
                    hasSourceWhiteSpace = true;
                }
                else
                {
                    EnsureHeaderWhiteSpace();
                }

                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                WriteTabs(level + 1);
                WriteLine($"property {BulletParser.GetTypeRefName(prop.Type)} {prop.Name}");
                WriteTabs(level + 1);
                WriteLine("{");

                // Getter/Setter
                OutputMethod(prop.Getter, level + 1);
                if (prop.Setter != null)
                {
                    OutputMethod(prop.Setter, level + 1);
                }

                WriteTabs(level + 1);
                WriteLine("}");

                if (typeConditional != null)
                {
                    WriteLine("#endif", WriteTo.Header | WriteTo.Source);
                    hasSourceWhiteSpace = false;
                }

                hasHeaderWhiteSpace = false;
            }

            WriteTabs(level);
            WriteLine("};");
            hasHeaderWhiteSpace = false;

            To = prevTo;
        }

        public override void Output()
        {
            foreach (HeaderDefinition header in Project.HeaderDefinitions.Values)
            {
                if (header.Classes.All(IsExcludedClass)) continue;

                Directory.CreateDirectory(Project.CppCliProjectPathFull);

                string headerFilename = Path.Combine(Project.CppCliProjectPathFull, header.ManagedName + ".h");
                OpenFile(headerFilename, WriteTo.Header);
                WriteLine("#pragma once");
                WriteLine();

                // Write includes
                if (header.Includes.Count != 0)
                {
                    foreach (var include in header.Includes)
                    {
                        WriteLine($"#include \"{include.ManagedName}.h\"");
                    }
                    WriteLine();
                }

                // Write namespace
                WriteLine($"namespace {Project.NamespaceName}");
                WriteLine("{");
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
                    WriteLine($"\tref class {c.ManagedName};");
                    if (!forwardRefHeaders.Contains(c.Header))
                    {
                        forwardRefHeaders.Add(c.Header);
                    }
                    hasHeaderWhiteSpace = false;
                }
                forwardRefHeaders.Add(header);
                forwardRefHeaders.Sort((r1, r2) => r1.ManagedName.CompareTo(r2.ManagedName));


                string sourceFilename = Path.Combine(Project.CppCliProjectPathFull, header.ManagedName + ".cpp");
                OpenFile(sourceFilename, WriteTo.Source);
                WriteLine("#include \"StdAfx.h\"");
                WriteLine();

                string headerConditional;
                if (headerConditionals.TryGetValue(header.ManagedName, out headerConditional))
                {
                    WriteLine($"#ifndef {headerConditional}");
                    WriteLine();
                }

                // Write statements to include forward referenced types
                if (forwardRefHeaders.Count != 0)
                {
                    foreach (var refHeader in forwardRefHeaders)
                    {
                        bool hasHeaderConditional = false;
                        if (headerConditionals.ContainsKey(refHeader.ManagedName))
                        {
                            hasHeaderConditional = true;
                            if (headerConditionals.ContainsKey(header.ManagedName) &&
                                headerConditionals[refHeader.ManagedName] == headerConditionals[header.ManagedName])
                            {
                                hasHeaderConditional = false;
                            }
                        }
                        if (hasHeaderConditional)
                        {
                            Write("#ifndef ");
                            WriteLine(headerConditionals[refHeader.ManagedName]);
                        }
                        WriteLine($"#include \"{refHeader.ManagedName}.h\"");
                        if (hasHeaderConditional)
                        {
                            WriteLine("#endif");
                        }
                    }
                    hasSourceWhiteSpace = false;
                }

                // Write classes
                var currentAccess = RefAccessSpecifier.Public;
                OutputClasses(header.Classes, ref currentAccess, 0);

                if (headerConditionals.ContainsKey(header.ManagedName))
                {
                    WriteLine();
                    WriteLine("#endif");
                }

                WriteLine("};", WriteTo.Header);
                CloseFile(WriteTo.Header);
                CloseFile(WriteTo.Source);
            }

            Console.WriteLine("Write complete");
        }

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

            foreach (ClassDefinition cl in c.NestedClasses)
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

            if (target != null && headerConditionals.ContainsKey(target.Header.ManagedName))
            {
                return headerConditionals[target.Header.ManagedName];
            }

            return null;
        }

        // Return condition unless type is already used under the same condition.
        string GetTypeConditional(TypeRefDefinition type, HeaderDefinition header)
        {
            string typeConditional = GetTypeConditional(type);
            if (typeConditional != null && headerConditionals.ContainsKey(header.ManagedName))
            {
                if (headerConditionals[header.ManagedName].Equals(typeConditional))
                {
                    return null;
                }
            }
            return typeConditional;
        }
    }
}
