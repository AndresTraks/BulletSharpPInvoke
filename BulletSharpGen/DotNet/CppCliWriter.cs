using ClangSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    public class CppCliWriter : DotNetWriter
    {
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

        public CppCliWriter(DotNetParser parser)
            : base(parser)
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

            WriteLine(level, $"{required.ToString().ToLower()}:", WriteTo.Header);
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

        private void WriteMethodMarshal(ManagedMethod method, int numParameters)
        {
            var nativeMethod = method.Native;
            string methodName = nativeMethod.IsConstructor ?
                $"{nativeMethod.Parent.FullyQualifiedName}" : $"{nativeMethod.Name}";

            var currentParams = method.Parameters.Take(numParameters);
            var paramStrings = currentParams.Select(p =>
            {
                string marshal = BulletParser.GetTypeMarshalCppCli(p);
                if (!string.IsNullOrEmpty(marshal)) { return marshal; }

                var paramType = p.Native.Type;
                if (paramType.IsBasic) { return p.Name; }

                string paramString = "";
                switch (paramType.Kind)
                {
                    case TypeKind.Pointer:
                    case TypeKind.LValueReference:
                        if (paramType.Kind == TypeKind.LValueReference)
                        {
                            // Dereference
                            paramString = "*";
                        }

                        if (paramType.Referenced.Target?.BaseClass != null)
                        {
                            // Cast native pointer from base class
                            paramString += $"({paramType.Referenced.FullName}*)";
                        }
                        break;
                }

                paramString += p.Name;
                if (paramType.Kind == TypeKind.Pointer && paramType.Referenced.Kind == TypeKind.Void)
                {
                    paramString += ".ToPointer()";
                }
                else
                {
                    paramString += "->_native";
                }
                return paramString;
            });

            Write($"{methodName}(");
            string parameters = ListToLines(paramStrings, WriteTo.Source, 1);
            Write($"{parameters})");
        }

        void WriteMethodDefinition(ManagedMethod method, int numParameters)
        {
            var nativeMethod = method.Native;
            var parentClass = method.Parent.Native;

            // Type marshalling prologue
            bool needTypeMarshalEpilogue = false;
            if (nativeMethod.Field == null)
            {
                foreach (var param in method.Parameters)
                {
                    string prologue = BulletParser.GetTypeMarshalPrologueCppCli(param);
                    if (!string.IsNullOrEmpty(prologue))
                    {
                        WriteLine(1, prologue);
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
            if (nativeMethod.IsConstructor)
            {
                Write("_native = new ");
            }
            else if (!nativeMethod.IsVoid)
            {
                //if (method.ReturnType.IsBasic || method.ReturnType.Referenced != null)
                if (needTypeMarshalEpilogue)
                {
                    // Return after epilogue (cleanup)
                    Write($"{GetTypeName(nativeMethod.ReturnType)} ret = ");
                }
                else
                {
                    // Return immediately
                    Write("return ");
                }
                Write(BulletParser.GetTypeMarshalConstructorStart(nativeMethod));
            }
            else if (method.Property != null && method.Equals(method.Property.Getter))
            {
                Write(BulletParser.GetTypeMarshalConstructorStart(nativeMethod));
            }

            // Native is defined as static_cast<className*>(_native)
            string nativePointer = (parentClass.BaseClass != null) ? "Native" : "_native";

            if (nativeMethod.Field != null)
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
                        Write($"{nativePointer}->{nativeMethod.Field.Name}");
                    }
                }
                else if (property.Setter != null && method.Equals(property.Setter))
                {
                    var param = method.Parameters[0];
                    var paramType = param.Native.Type;
                    var fieldSet = BulletParser.GetTypeMarshalFieldSetCppCli(nativeMethod.Field, param, nativePointer);
                    if (!string.IsNullOrEmpty(fieldSet))
                    {
                        Write(fieldSet);
                    }
                    else
                    {
                        Write($"{nativePointer}->{nativeMethod.Field.Name} = ");
                        switch (paramType.Kind)
                        {
                            case TypeKind.Pointer:
                            case TypeKind.LValueReference:
                                if (paramType.Kind == TypeKind.LValueReference)
                                {
                                    // Dereference
                                    Write('*');
                                }

                                if (paramType.Referenced.Target != null &&
                                    paramType.Referenced.Target.BaseClass != null)
                                {
                                    // Cast native pointer from base class
                                    Write($"({paramType.Referenced.FullName}*)");
                                }
                                break;
                        }
                        Write(param.Name);
                        if (!paramType.IsBasic)
                        {
                            Write("->_native");
                        }
                    }
                }
            }
            else
            {
                if (nativeMethod.IsConstructor)
                {
                }
                else if (nativeMethod.IsStatic)
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
            if (!nativeMethod.IsConstructor && !nativeMethod.IsVoid)
            {
                Write(BulletParser.GetTypeMarshalConstructorEnd(nativeMethod));
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
                        WriteLine(1, epilogue);
                    }
                }
                if (!nativeMethod.IsVoid)
                {
                    WriteLine(1, "return ret;");
                }
            }
        }

        private void WriteMethodDeclaration(ManagedMethod method, int level, int numOptionalParams)
        {
            var nativeMethod = method.Native;

            WriteTabs(level + 1, WriteTo.Header);

            if (nativeMethod.IsStatic) Write("static ", WriteTo.Header);

            // Return type
            if (!nativeMethod.IsConstructor)
            {
                var returnType = nativeMethod.ReturnType;

                if (method.Property != null)
                {
                    if (method.Equals(method.Property.Getter))
                    {
                        // If property name matches type name, resolve ambiguity
                        if (method.Property.Name.Equals(GetName(method.Property.Type)))
                        {
                            Write(Project.NamespaceName + "::", WriteTo.Header);
                        }

                        // Getter with parameter for return value
                        if (method.Parameters.Length == 1)
                        {
                            returnType = method.Parameters[0].Native.Type;
                        }
                    }
                }

                Write($"{GetTypeName(returnType)} ", WriteTo.Header | WriteTo.Source);
            }


            // Name
            string headerMethodName;
            string sourceMethodName;
            if (nativeMethod.IsConstructor)
            {
                headerMethodName = method.Parent.Name;
                sourceMethodName = headerMethodName;
            }
            else if (method.Property != null)
            {
                headerMethodName = method.Property.Getter.Equals(method) ? "get" : "set";
                sourceMethodName = $"{method.Property.Name}::{headerMethodName}";
            }
            else
            {
                headerMethodName = method.Name;
                sourceMethodName = headerMethodName;
            }
            Write($"{headerMethodName}(", WriteTo.Header);
            Write($"{GetFullNameManaged(method.Parent)}::{sourceMethodName}(", WriteTo.Source);


            // Parameters
            int numParameters = method.Parameters.Length - numOptionalParams;
            // Getters with parameter for return value
            if (numParameters == 1 && method.Property != null && method.Equals(method.Property.Getter))
            {
                numParameters = 0;
            }
            var currentParams = method.Parameters.Take(numParameters).ToList();
            var paramStrings = currentParams
                .Select(p => $"{GetParameterMarshal(p.Native)} {p.Name}").ToList();

            string parameters = ListToLines(paramStrings, WriteTo.Header, level + 1);
            WriteLine($"{parameters});", WriteTo.Header);

            parameters = ListToLines(paramStrings, WriteTo.Source);
            WriteLine($"{parameters})", WriteTo.Source);
        }

        private void WriteMethod(ManagedMethod method, int level, int numOptionalParams = 0)
        {
            var nativeMethod = method.Native;
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
                    string typeConditional = GetTypeConditional(param.Native.Type, parentClass.Header);
                    if (typeConditional != null)
                    {
                        WriteLine($"#ifndef {typeConditional}", WriteTo.Header | WriteTo.Source);
                        hasSourceWhiteSpace = true;
                        hasConditional = true;
                    }
                }
            }

            WriteMethodDeclaration(method, level, numOptionalParams);

            var prevTo = To;
            To = WriteTo.Source;

            // Constructor chaining
            int numParameters = method.Parameters.Length - numOptionalParams;
            // Getters with parameter for return value
            if (numParameters == 1 && method.Property != null && method.Equals(method.Property.Getter))
            {
                numParameters = 0;
            }
            var currentParams = method.Parameters.Take(numParameters).ToList();

            bool doConstructorChaining = false;
            if (nativeMethod.IsConstructor && parentClass.BaseClass != null)
            {
                // If there is no need for marshalling code, we can chain constructors
                doConstructorChaining = currentParams.All(p => !DefaultParser.TypeRequiresMarshal(p.Native.Type));

                Write(1, $": {parentClass.BaseClass.Name}(");

                if (doConstructorChaining)
                {
                    Write("new ");
                    WriteMethodMarshal(method, numParameters);
                    if (parentClass.BaseClass.Native.HasPreventDelete)
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
            if (nativeMethod.IsConstructor)
            {
                var assignments = new List<string>();
                var methodParent = method.Parent;
                while (methodParent != null)
                {
                    foreach (var cachedProperty in methodParent.CachedProperties.OrderBy(p => p.Key))
                    {
                        foreach (var param in currentParams)
                        {
                            if (!param.Name.ToLower().Equals(cachedProperty.Key.ToLower())) continue;

                            var paramType = GetName(param.Native.Type);
                            var propType = GetName(cachedProperty.Value.Property.Type);
                            if (!paramType.Equals(propType)) return;

                            string assignment = $"\t{cachedProperty.Value.CacheFieldName} = {param.Name};";
                            assignments.Add(assignment);
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
                foreach (var param in currentParams)
                {
                    string typeConditional = GetTypeConditional(param.Native.Type, method.Parent.Header);
                    if (typeConditional != null)
                    {
                        WriteLine("#endif", WriteTo.Header | WriteTo.Source);
                        hasHeaderWhiteSpace = true;
                    }
                }
            }

            // If there are optional parameters, then output all possible combinations of calls
            if (currentParams.Any() && currentParams.Last().Native.IsOptional)
            {
                WriteMethod(method, level, numOptionalParams + 1);
            }

            To = prevTo;
        }

        void WriteClasses(IList<ManagedClass> classes, ref RefAccessSpecifier currentAccess, int level)
        {
            bool insertSeparator = false;
            foreach (var @class in classes.Where(c => !IsExcludedClass(c.Native)))
            {
                if (insertSeparator) WriteLine(WriteTo.Source);

                if (level != 0)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                }
                WriteClass(@class, level + 1);
                insertSeparator = true;
            }
        }

        void WriteClass(ManagedClass @class, int level)
        {
            var nativeClass = @class.Native;

            EnsureHeaderWhiteSpace();
            EnsureSourceWhiteSpace();

            var prevTo = To;

            To = WriteTo.Header;
            WriteTabs(level);

            // Access modifier
            if (level == 1) Write("public ");

            // Class definition
            Write($"ref class {@class.Name}");

            // abstract/sealed keywords
            if (nativeClass.IsAbstract) Write(" abstract");
            else if (nativeClass.IsStatic) Write(" sealed");

            // Inheritance
            if (@class.BaseClass != null)
            {
                WriteLine($" : {@class.BaseClass.Name}");
            }
            else if (nativeClass.IsTrackingDisposable) WriteLine(" : ITrackingDisposable");
            else WriteLine();

            // Class body start
            WriteLine(level, "{");

            // Default access for ref class
            var currentAccess = RefAccessSpecifier.Private;

            // Nested classes
            if (!@class.NestedClasses.All(IsExcludedClass))
            {
                WriteClasses(@class.NestedClasses, ref currentAccess, level);
                currentAccess = RefAccessSpecifier.Public;
                WriteLine(WriteTo.Source);
            }

            // Private constructor for classes that aren't instanced
            if (nativeClass.IsStatic)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteLine(level + 1, $"{@class.Name}() {{}}");
                hasHeaderWhiteSpace = false;
            }

            // Downcast native pointer if any methods in a derived class use it
            if (@class.BaseClass != null && @class.Methods.Select(m => m.Native)
                .Any(m => !m.IsConstructor && !m.IsStatic && !m.IsExcluded &&
                    m.Access == AccessSpecifier.Public))
            {
                EnsureSourceWhiteSpace();
                WriteLine($"#define Native static_cast<{nativeClass.FullyQualifiedName}*>(_native)", WriteTo.Source);
                hasSourceWhiteSpace = false;
            }

            // Write the native pointer to the base class
            if (@class.BaseClass == null && !nativeClass.IsStatic)
            {
                if (@class.NestedClasses.Any(c => !IsExcludedClass(c)))
                {
                    WriteLine();
                }
                if (nativeClass.IsTrackingDisposable)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                    WriteLine(level + 1, "virtual event EventHandler^ OnDisposing;");
                    WriteLine(level + 1, "virtual event EventHandler^ OnDisposed;");
                    hasHeaderWhiteSpace = false;
                }
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                Write(level + 1, nativeClass.FullyQualifiedName);
                WriteLine("* _native;");
                hasHeaderWhiteSpace = false;
            }

            EnsureHeaderWhiteSpace();
            EnsureSourceWhiteSpace();

            // Private fields
            // _isDisposed flag
            if (nativeClass.IsTrackingDisposable)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteLine(level + 1, "bool _isDisposed;");
                hasHeaderWhiteSpace = false;
            }
            // _preventDelete flag
            if (nativeClass.HasPreventDelete)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteLine(level + 1, "bool _preventDelete;");
                hasHeaderWhiteSpace = false;
            }

            // Cached property fields
            foreach (var cachedProperty in @class.CachedProperties.OrderBy(p => p.Key))
            {
                EnsureAccess(level, ref currentAccess, cachedProperty.Value.Access);
                string typename = GetTypeName(cachedProperty.Value.Property.Type);
                string fieldName = cachedProperty.Key;
                fieldName = char.ToLower(fieldName[0]) + fieldName.Substring(1);
                WriteLine(level + 1, $"{typename} _{fieldName};");
                hasHeaderWhiteSpace = false;
            }

            // Constructors and destructors
            if (!nativeClass.IsStatic)
            {
                // Write unmanaged constructor
                // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.
                if (!nativeClass.NoInternalConstructor)
                {
                    // Declaration
                    To = WriteTo.Header;
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);
                    WriteLine(level + 1,
                        $"{@class.Name}({nativeClass.FullyQualifiedName}* native);");

                    // Definition
                    To = WriteTo.Source;
                    // TODO: use full name once class hierarchy flattening is implemented
                    //WriteLine($"{@class.FullNameCppCli}::{@class.ManagedName}({@class.FullyQualifiedName}* native)");
                    WriteLine($"{@class.Name}::{@class.Name}({nativeClass.FullyQualifiedName}* native)");
                    if (@class.BaseClass != null)
                    {
                        WriteLine(1, $": {@class.BaseClass.Name}(native)");
                    }

                    // Body
                    WriteLine('{');
                    if (@class.BaseClass == null) WriteLine(1, "_native = native;");
                    WriteLine('}');

                    hasHeaderWhiteSpace = false;
                    hasSourceWhiteSpace = false;
                }

                // Write destructor & finalizer
                if (@class.BaseClass == null)
                {
                    To = WriteTo.Header;

                    // ECMA-372 19.13.1: "The access-specifier of a destructor in a ref class is ignored."
                    // ECMA-372 19.13.2: "The access-specifier of a finalizer in a ref class is ignored."
                    WriteLine(level + 1, $"~{@class.Name}();");
                    WriteLine(level + 1, $"!{@class.Name}();");
                    hasHeaderWhiteSpace = false;

                    To = WriteTo.Source;
                    EnsureSourceWhiteSpace();
                    WriteLine($"{GetFullNameManaged(@class)}::~{@class.Name}()");
                    WriteLine('{');
                    WriteLine(1, $"this->!{@class.Name}();");
                    WriteLine('}');
                    WriteLine();

                    WriteLine($"{GetFullNameManaged(@class)}::!{@class.Name}()");
                    WriteLine('{');
                    if (nativeClass.IsTrackingDisposable)
                    {
                        WriteLine(1, "if (this->IsDisposed)");
                        WriteLine(2, "return;");
                        WriteLine();
                        WriteLine(1, "OnDisposing(this, nullptr);");
                        WriteLine();
                    }
                    if (nativeClass.HasPreventDelete)
                    {
                        WriteLine(1, "if (!_preventDelete)");
                        WriteLine(1, "{");
                        WriteLine(2, "delete _native;");
                        WriteLine(1, "}");
                    }
                    else
                    {
                        WriteLine(1, "delete _native;");
                    }
                    if (nativeClass.IsTrackingDisposable)
                    {
                        WriteLine(1, "_isDisposed = true;");
                        WriteLine();
                        WriteLine(1, "OnDisposed(this, nullptr);");
                    }
                    else
                    {
                        WriteLine(1, "_native = NULL;");
                    }
                    WriteLine('}');
                    hasSourceWhiteSpace = false;
                }

                // Write public constructors
                if (!nativeClass.HidePublicConstructors && !nativeClass.IsAbstract)
                {
                    var constructors = @class.Methods.Where(m => m.Native.IsConstructor && m.Native.Access == AccessSpecifier.Public);
                    if (constructors.Any())
                    {
                        EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);

                        foreach (var constructor in constructors)
                        {
                            WriteMethod(constructor, level);
                        }
                    }
                }
            }

            // Write non-constructor methods
            var methods = @class.Methods
                .Where(m => !m.Native.IsConstructor && !m.Native.IsExcluded &&
                    m.Native.Access == AccessSpecifier.Public && m.Property == null);
            if (methods.Any())
            {
                EnsureHeaderWhiteSpace();

                foreach (var method in methods)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                    WriteMethod(method, level);
                }
            }

            // Write properties (includes unmanaged fields and getters/setters)
            To = WriteTo.Header;
            foreach (ManagedProperty prop in @class.Properties)
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
                WriteLine(level + 1, $"property {GetTypeName(prop.Type)} {prop.Name}");
                WriteLine(level + 1, "{");

                // Getter/Setter
                WriteMethod(prop.Getter, level + 1);
                if (prop.Setter != null)
                {
                    WriteMethod(prop.Setter, level + 1);
                }

                WriteLine(level + 1, "}");

                if (typeConditional != null)
                {
                    WriteLine("#endif", WriteTo.Header | WriteTo.Source);
                    hasSourceWhiteSpace = false;
                }

                hasHeaderWhiteSpace = false;
            }

            WriteLine(level, "};");
            hasHeaderWhiteSpace = false;

            To = prevTo;
        }

        public override void Output()
        {
            var headers = DotNetParser.Headers.Values;
            if (headers.Count != 0)
            {
                Directory.CreateDirectory(Project.CppCliProjectPathFull);
            }

            foreach (var header in headers)
            {
                string headerFilename = Path.Combine(Project.CppCliProjectPathFull, header.Name + ".h");
                OpenFile(headerFilename, WriteTo.Header);
                WriteLine("#pragma once");
                WriteLine();

                // Write includes
                if (header.Native.Includes.Count != 0)
                {
                    foreach (var include in header.Native.Includes)
                    {
                        string includeName = DotNetParser.GetManaged(include).Name;
                        WriteLine($"#include \"{includeName}.h\"");
                    }
                    WriteLine();
                }

                // Write namespace
                WriteLine($"namespace {Project.NamespaceName}");
                WriteLine("{");
                hasHeaderWhiteSpace = true;

                // Find forward references
                var forwardRefs = new List<ManagedClass>();
                foreach (var c in header.Classes)
                {
                    FindForwardReferences(forwardRefs, c);
                }

                // Remove redundant forward references (header file already included)
                forwardRefs.RemoveAll(fr => header.Native.Includes.Contains(fr.Header.Native));
                forwardRefs.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));

                // Write forward references
                var forwardRefHeaders = new List<ManagedHeader>();
                foreach (ManagedClass c in forwardRefs)
                {
                    WriteLine(1, $"ref class {c.Name};");
                    if (!forwardRefHeaders.Contains(c.Header))
                    {
                        forwardRefHeaders.Add(c.Header);
                    }
                    hasHeaderWhiteSpace = false;
                }
                forwardRefHeaders.Add(header);
                forwardRefHeaders.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));


                string sourceFilename = Path.Combine(Project.CppCliProjectPathFull, header.Name + ".cpp");
                OpenFile(sourceFilename, WriteTo.Source);
                WriteLine("#include \"StdAfx.h\"");
                WriteLine();

                string headerConditional;
                if (headerConditionals.TryGetValue(header.Name, out headerConditional))
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
                        if (headerConditionals.ContainsKey(refHeader.Name))
                        {
                            hasHeaderConditional = true;
                            if (headerConditionals.ContainsKey(header.Name) &&
                                headerConditionals[refHeader.Name] == headerConditionals[header.Name])
                            {
                                hasHeaderConditional = false;
                            }
                        }
                        if (hasHeaderConditional)
                        {
                            Write("#ifndef ");
                            WriteLine(headerConditionals[refHeader.Name]);
                        }
                        WriteLine($"#include \"{refHeader.Name}.h\"");
                        if (hasHeaderConditional)
                        {
                            WriteLine("#endif");
                        }
                    }
                    hasSourceWhiteSpace = false;
                }

                // Write classes
                var currentAccess = RefAccessSpecifier.Public;
                WriteClasses(header.Classes, ref currentAccess, 0);

                if (headerConditionals.ContainsKey(header.Name))
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

        void AddForwardReference(List<ManagedClass> forwardRefs, TypeRefDefinition type, HeaderDefinition header)
        {
            if (type.IsBasic) return;

            switch (type.Kind)
            {
                case TypeKind.Pointer:
                case TypeKind.LValueReference:
                    AddForwardReference(forwardRefs, type.Referenced, header);
                    return;
            }

            var target = type.Target;
            if (target == null) return;
            if (target.IsExcluded || target.MarshalAsStruct || target.Header == header) return;

            var targetManaged = DotNetParser.GetManaged(target);
            if (!forwardRefs.Contains(targetManaged))
            {
                forwardRefs.Add(targetManaged);
            }
        }

        void FindForwardReferences(List<ManagedClass> forwardRefs, ManagedClass @class)
        {
            var header = @class.Header.Native;

            foreach (var prop in @class.Properties)
            {
                AddForwardReference(forwardRefs, prop.Type, header);
            }

            var methods = @class.Methods.Where(m => m.Native.Access == AccessSpecifier.Public && !m.Native.IsExcluded);
            if (@class.Native.HidePublicConstructors) methods = methods.Where(m => !m.Native.IsConstructor);
            foreach (var method in methods)
            {
                AddForwardReference(forwardRefs, method.Native.ReturnType, header);

                foreach (var param in method.Parameters)
                {
                    AddForwardReference(forwardRefs, param.Native.Type, header);
                }
            }

            foreach (var cl in @class.NestedClasses)
            {
                FindForwardReferences(forwardRefs, cl);
            }
        }

        private string GetTypeName(TypeRefDefinition type)
        {
            if (type.Kind == TypeKind.Typedef)
            {
                if (type.Referenced.Kind == TypeKind.Pointer &&
                    type.Referenced.Referenced.Kind == TypeKind.FunctionProto)
                {
                    return GetName(type);
                }
                if (type.Canonical.IsBasic)
                {
                    return type.Name;
                }
                return GetTypeName(type.Referenced);
            }

            if (type.Kind == TypeKind.LValueReference)
            {
                return GetTypeName(type.Referenced);
            }

            if (!string.IsNullOrEmpty(type.Name) && type.Name.Equals("btAlignedObjectArray"))
            {
                if (type.TemplateParams != null)
                {
                    return "Aligned" + GetName(type.TemplateParams[0]) + "Array^";
                }
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
                    switch (GetName(type))
                    {
                        case "char":
                            return "String^";
                        case "float":
                            return string.Format("array<{0}>^", type.Referenced.Name);
                    }
                    return GetName(type) + '^';
            }
            return GetName(type);
        }

        private string GetParameterMarshal(ParameterDefinition param)
        {
            var typeName = GetTypeName(param.Type);
            var type = param.Type.Canonical;

            switch (type.Kind)
            {
                case TypeKind.Pointer:
                case TypeKind.LValueReference:
                    if (type.Referenced.Target != null && type.Referenced.Target.MarshalAsStruct)
                    {
                        // https://msdn.microsoft.com/en-us/library/77e6taeh%28v=vs.100%29.aspx
                        // Default IDL attribute values:
                        // ref - [In, Out]
                        // out - [Out]
                        switch (param.MarshalDirection)
                        {
                            case MarshalDirection.Out:
                                return $"[Out] {typeName}%";
                            case MarshalDirection.InOut:
                                return $"{typeName}%";
                            case MarshalDirection.In:
                                return typeName;
                        }
                    }
                    break;
            }

            if (type.Kind == TypeKind.LValueReference && type.Referenced.Canonical.IsBasic)
            {
                typeName = GetTypeName(type.Referenced);
                switch (param.MarshalDirection)
                {
                    case MarshalDirection.Out:
                        return $"[Out] {typeName}%";
                    case MarshalDirection.InOut:
                        return $"{typeName}%";
                    case MarshalDirection.In:
                        return typeName;
                }
            }

            return typeName;
        }

        // If the type is defined in a conditionally compiled header, return the condition string.
        private string GetTypeConditional(TypeRefDefinition type)
        {
            var target = type.Target;
            if (target == null && type.Referenced != null)
            {
                target = type.Referenced.Target;
            }

            if (target == null) return null;

            string headerConditional;
            string targetHeaderName = DotNetParser.GetManaged(target.Header).Name;
            if (headerConditionals.TryGetValue(targetHeaderName, out headerConditional))
            {
                return headerConditional;
            }

            return null;
        }

        // Return condition unless type is already used under the same condition.
        private string GetTypeConditional(TypeRefDefinition type, ManagedHeader header)
        {
            string typeConditional = GetTypeConditional(type);
            if (typeConditional != null && headerConditionals.ContainsKey(header.Name))
            {
                if (headerConditionals[header.Name].Equals(typeConditional))
                {
                    return null;
                }
            }
            return typeConditional;
        }

        private static string GetFullNameManaged(ManagedClass @class)
        {
            if (@class.Parent != null)
            {
                return $"{GetFullNameManaged(@class.Parent)}::{@class.Name}";
            }
            return @class.Name;
        }
    }
}
