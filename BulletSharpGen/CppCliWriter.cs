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

        // These do no need forward references
        public List<string> PrecompiledHeaderReferences =
            new List<string>(new[] {"Matrix3x3", "Quaternion", "Transform", "Vector4"});

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

        private void WriteMethodMarshal(MethodDefinition method, int numParameters)
        {
            string methodName = method.IsConstructor ?
                $"{method.Parent.FullyQualifiedName}" : $"{method.Name}";

            var currentParams = method.Parameters.Take(numParameters);
            var paramStrings = currentParams.Select(p =>
            {
                string marshal = BulletParser.GetTypeMarshalCppCli(p);
                if (!string.IsNullOrEmpty(marshal)) { return marshal; }

                if (p.Type.IsBasic) { return p.ManagedName; }

                string paramString = "";
                switch (p.Type.Kind)
                {
                    case TypeKind.Pointer:
                    case TypeKind.LValueReference:
                        if (p.Type.Kind == TypeKind.LValueReference)
                        {
                            // Dereference
                            paramString = "*";
                        }

                        if (p.Type.Referenced.Target?.BaseClass != null)
                        {
                            // Cast native pointer from base class
                            paramString += $"({p.Type.Referenced.FullName}*)";
                        }
                        break;
                }

                paramString += p.ManagedName;
                if (p.Type.Kind == TypeKind.Pointer && p.Type.Referenced.Kind == TypeKind.Void)
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
                        switch (param.Type.Kind)
                        {
                            case TypeKind.Pointer:
                            case TypeKind.LValueReference:
                                if (param.Type.Kind == TypeKind.LValueReference)
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
                                break;
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
                        WriteLine(1, epilogue);
                    }
                }
                if (!method.IsVoid)
                {
                    WriteLine(1, "return ret;");
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

            // #ifndef DISABLE_FEATURE
            bool hasConditional = false;
            if (method.Property == null)
            {
                foreach (var param in method.Parameters)
                {
                    string typeConditional = GetTypeConditional(param.Type, parentClass.Header);
                    if (typeConditional != null)
                    {
                        WriteLine($"#ifndef {typeConditional}", WriteTo.Header | WriteTo.Source);
                        hasSourceWhiteSpace = true;
                        hasConditional = true;
                    }
                }
            }

            // Declaration
            WriteTabs(level + 1, WriteTo.Header);

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

                Write($"{BulletParser.GetTypeRefName(returnType)} ", WriteTo.Header | WriteTo.Source);
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
            Write($"{GetFullNameManaged(parentClass)}::{sourceMethodName}(", WriteTo.Source);

            var prevTo = To;

            // Definition: parameters
            int numParameters = method.Parameters.Length - numOptionalParams;
            // Getters with parameter for return value
            if (numParameters == 1 && method.Property != null && method.Equals(method.Property.Getter))
            {
                numParameters = 0;
            }
            var currentParams = method.Parameters.Take(numParameters).ToList();
            var paramStrings = currentParams
                .Select(p => $"{BulletParser.GetTypeRefName(p.Type)} {p.ManagedName}").ToList();

            string parameters = ListToLines(paramStrings, WriteTo.Header, level + 1);
            WriteLine($"{parameters});", WriteTo.Header);

            parameters = ListToLines(paramStrings, WriteTo.Source);
            WriteLine($"{parameters})", WriteTo.Source);


            // Definition: constructor chaining
            To = WriteTo.Source;
            bool doConstructorChaining = false;
            if (method.IsConstructor && parentClass.BaseClass != null)
            {
                // If there is no need for marshalling code, we can chain constructors
                doConstructorChaining = currentParams.All(p => !DefaultParser.TypeRequiresMarshal(p.Type));

                Write(1, $": {parentClass.BaseClass.ManagedName}(");

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
                        foreach (var param in currentParams)
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
                foreach (var param in currentParams)
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
            if (currentParams.Any() && currentParams.Last().IsOptional)
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

        void OutputClass(ClassDefinition @class, int level)
        {
            EnsureHeaderWhiteSpace();
            EnsureSourceWhiteSpace();

            var prevTo = To;

            To = WriteTo.Header;
            WriteTabs(level);

            // Access modifier
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
            WriteLine(level, "{");

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
                WriteLine(level + 1, $"{@class.ManagedName}() {{}}");
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
                    WriteLine(level + 1, "virtual event EventHandler^ OnDisposing;");
                    WriteLine(level + 1, "virtual event EventHandler^ OnDisposed;");
                    hasHeaderWhiteSpace = false;
                }
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                Write(level + 1, @class.FullyQualifiedName);
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
                WriteLine(level + 1, "bool _isDisposed;");
                hasHeaderWhiteSpace = false;
            }
            // _preventDelete flag
            if (@class.HasPreventDelete)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteLine(level + 1, "bool _preventDelete;");
                hasHeaderWhiteSpace = false;
            }

            // Cached property fields
            foreach (var cachedProperty in @class.CachedProperties.OrderBy(p => p.Key))
            {
                EnsureAccess(level, ref currentAccess, cachedProperty.Value.Access);
                string typename = BulletParser.GetTypeRefName(cachedProperty.Value.Property.Type);
                string fieldName = cachedProperty.Key;
                fieldName = char.ToLower(fieldName[0]) + fieldName.Substring(1);
                WriteLine(level + 1, $"{typename} _{fieldName};");
                hasHeaderWhiteSpace = false;
            }

            // Constructors and destructors
            if (!@class.IsStaticClass)
            {
                // Write unmanaged constructor
                // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.
                if (!@class.NoInternalConstructor)
                {
                    // Declaration
                    To = WriteTo.Header;
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);
                    WriteLine(level + 1,
                        $"{@class.ManagedName}({@class.FullyQualifiedName}* native);");

                    // Definition
                    To = WriteTo.Source;
                    // TODO: use full name once class hierarchy flattening is implemented
                    //WriteLine($"{@class.FullNameCppCli}::{@class.ManagedName}({@class.FullyQualifiedName}* native)");
                    WriteLine($"{@class.ManagedName}::{@class.ManagedName}({@class.FullyQualifiedName}* native)");
                    if (@class.BaseClass != null)
                    {
                        WriteLine(1, $": {@class.BaseClass.ManagedName}(native)");
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
                    WriteLine(level + 1, $"~{@class.ManagedName}();");
                    WriteLine(level + 1, $"!{@class.ManagedName}();");
                    hasHeaderWhiteSpace = false;

                    To = WriteTo.Source;
                    EnsureSourceWhiteSpace();
                    WriteLine($"{GetFullNameManaged(@class)}::~{@class.ManagedName}()");
                    WriteLine('{');
                    WriteLine(1, $"this->!{@class.ManagedName}();");
                    WriteLine('}');
                    WriteLine();

                    WriteLine($"{GetFullNameManaged(@class)}::!{@class.ManagedName}()");
                    WriteLine('{');
                    if (@class.IsTrackingDisposable)
                    {
                        WriteLine(1, "if (this->IsDisposed)");
                        WriteLine(2, "return;");
                        WriteLine();
                        WriteLine(1, "OnDisposing(this, nullptr);");
                        WriteLine();
                    }
                    if (@class.HasPreventDelete)
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
                    if (@class.IsTrackingDisposable)
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
                string typeName = BulletParser.GetTypeRefName(prop.Type);
                WriteLine(level + 1, $"property {typeName} {prop.Name}");
                WriteLine(level + 1, "{");

                // Getter/Setter
                OutputMethod(prop.Getter, level + 1);
                if (prop.Setter != null)
                {
                    OutputMethod(prop.Setter, level + 1);
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
            var headers = Project.HeaderDefinitions.Values
                .Where(h => h.Classes.Any(c => !IsExcludedClass(c))).ToList();

            if (headers.Count != 0)
            {
                Directory.CreateDirectory(Project.CppCliProjectPathFull);
            }

            foreach (var header in headers)
            {
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
                foreach (var c in header.Classes)
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
                    WriteLine(1, $"ref class {c.ManagedName};");
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
            if (type.IsBasic) return;

            switch (type.Kind)
            {
                case TypeKind.Pointer:
                case TypeKind.LValueReference:
                    AddForwardReference(forwardRefs, type.Referenced, header);
                    return;
            }

            if (type.Target == null) return;

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

        private static string GetFullNameManaged(ClassDefinition @class)
        {
            if (@class.Parent != null)
            {
                return $"{GetFullNameManaged(@class.Parent)}::{@class.ManagedName}";
            }
            return @class.ManagedName;
        }
    }
}
