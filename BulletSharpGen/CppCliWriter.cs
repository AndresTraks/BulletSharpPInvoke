using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    class CppCliWriter : WrapperWriter
    {
        int _headerLineLength;
        int _sourceLineLength;
        
        const int TabWidth = 4;
        const int LineBreakWidth = 80;

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
            if (current == required) return;

            if (withWhiteSpace)
            {
                EnsureHeaderWhiteSpace();
            }

            WriteTabs(level);
            HeaderWriteLine(string.Format("{0}:", required.ToString().ToLower()));
            current = required;
        }

        void EnsureHeaderWhiteSpace()
        {
            if (hasHeaderWhiteSpace) return;

            HeaderWriteLine();
            hasHeaderWhiteSpace = true;
        }

        void EnsureSourceWhiteSpace()
        {
            if (hasSourceWhiteSpace) return;

            SourceWriteLine();
            hasSourceWhiteSpace = true;
        }

        void OutputMethodMarshal(MethodDefinition method, int numParameters)
        {
            if (method.IsConstructor)
            {
                SourceWrite(method.Parent.FullyQualifiedName);
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

                // Insert comma if there are more parameters
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
            else if (!method.IsVoid)
            {
                //if (method.ReturnType.IsBasic || method.ReturnType.Referenced != null)
                if (needTypeMarshalEpilogue)
                {
                    // Return after epilogue (cleanup)
                    SourceWrite(string.Format("{0} ret = ",
                        BulletParser.GetTypeRefName(method.ReturnType)));
                }
                else
                {
                    // Return immediately
                    SourceWrite("return ");
                }
                SourceWrite(BulletParser.GetTypeMarshalConstructorStart(method));
            }
            else if (method.Property != null && method.Equals(method.Property.Getter))
            {
                SourceWrite(BulletParser.GetTypeMarshalConstructorStart(method));
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
                        SourceWrite(cachedProperty.CacheFieldName);
                    }
                    else
                    {
                        SourceWrite(string.Format("{0}->{1}", nativePointer, method.Field.Name));
                    }
                }
                else if (property.Setter != null && method.Equals(property.Setter))
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
                if (method.IsConstructor)
                {
                }
                else if (method.IsStatic)
                {
                    SourceWrite(parentClass.FullyQualifiedName + "::");
                }
                else
                {
                    SourceWrite(nativePointer + "->");
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
                foreach (var param in method.Parameters)
                {
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
                        WriteLine(string.Format("#ifndef {0}", typeConditional));
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
                sourceMethodName = string.Format("{0}::{1}", method.Property.Name, headerMethodName);
            }
            else
            {
                headerMethodName = method.ManagedName;
                sourceMethodName = headerMethodName;
            }
            HeaderWrite(string.Format("{0}(", headerMethodName));
            SourceWrite(string.Format("{0}::{1}(", parentClass.FullNameManaged, sourceMethodName));

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
                Write(string.Format("{0} {1}",
                    BulletParser.GetTypeRefName(param.Type), param.ManagedName));

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
                doConstructorChaining = method.Parameters.All(p => !BulletParser.TypeRequiresMarshal(p.Type));

                WriteTabs(1, true);
                SourceWrite(string.Format(": {0}(", parentClass.BaseClass.ManagedName));

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
                                string assignment = string.Format("\t{0} = {1};", cachedProperty.Value.CacheFieldName, param.ManagedName);
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
                        SourceWriteLine(assignment);
                    }
                    hasSourceWhiteSpace = false;
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
            bool insertSeparator = false;
            foreach (var @class in classes.Where(c => !IsExcludedClass(c)))
            {
                if (insertSeparator)
                {
                    SourceWriteLine();
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

            // Write access modifier
            WriteTabs(level);
            if (level == 1)
            {
                HeaderWrite("public ");
            }

            // Write class definition
            HeaderWrite(string.Format("ref class {0}", @class.ManagedName));
            if (@class.IsAbstract)
            {
                HeaderWrite(" abstract");
            }
            else if (@class.IsStaticClass)
            {
                HeaderWrite(" sealed");
            }
            if (@class.BaseClass != null)
            {
                HeaderWriteLine(string.Format(" : {0}", @class.BaseClass.ManagedName));
            }
            else if (@class.IsTrackingDisposable)
            {
                HeaderWriteLine(" : ITrackingDisposable");
            }
            else
            {
                // In C++/CLI, IDisposable is inherited automatically if the destructor and finalizer are defined
                //HeaderWrite(" : IDisposable");
                HeaderWriteLine();
            }

            WriteTabs(level);
            HeaderWriteLine("{");
            //hasHeaderWhiteSpace = true;

            // Default access for ref class
            var currentAccess = RefAccessSpecifier.Private;

            // Write child classes
            if (!@class.Classes.All(cl => IsExcludedClass(cl)))
            {
                OutputClasses(@class.Classes, ref currentAccess, level);
                currentAccess = RefAccessSpecifier.Public;
                SourceWriteLine();
            }

            // Add a private constructor for classes without instances
            if (@class.IsStaticClass)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteTabs(level + 1);
                HeaderWriteLine(string.Format("{0}() {{}}", @class.ManagedName));
                hasHeaderWhiteSpace = false;
            }

            // Downcast native pointer if any methods in a derived class use it
            if (@class.BaseClass != null && @class.Methods.Any(m => !m.IsConstructor && !m.IsStatic))
            {
                EnsureSourceWhiteSpace();
                SourceWriteLine(string.Format("#define Native static_cast<{0}*>(_native)", @class.FullyQualifiedName));
                hasSourceWhiteSpace = false;
            }

            // Write the native pointer to the base class
            if (@class.BaseClass == null && !@class.IsStaticClass)
            {
                if (@class.Classes.Any(c => !IsExcludedClass(c)))
                {
                    HeaderWriteLine();
                }
                if (@class.IsTrackingDisposable)
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
                HeaderWrite(@class.FullyQualifiedName);
                HeaderWriteLine("* _native;");
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
                HeaderWriteLine("bool _isDisposed;");
                hasHeaderWhiteSpace = false;
            }
            // _preventDelete flag
            if (@class.HasPreventDelete)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Private);
                WriteTabs(level + 1);
                HeaderWriteLine("bool _preventDelete;");
                hasHeaderWhiteSpace = false;
            }

            // Write cached property fields
            foreach (var cachedProperty in @class.CachedProperties.OrderBy(p => p.Key))
            {
                EnsureAccess(level, ref currentAccess, cachedProperty.Value.Access);
                WriteTabs(level + 1);
                string name = cachedProperty.Key;
                name = char.ToLower(name[0]) + name.Substring(1);
                HeaderWriteLine(string.Format("{0} _{1};",
                    BulletParser.GetTypeRefName(cachedProperty.Value.Property.Type), name));
                hasHeaderWhiteSpace = false;
            }

            // Write constructors and destructors if not static
            if (!@class.IsStaticClass)
            {
                // Write unmanaged constructor
                // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.
                if (!@class.NoInternalConstructor)
                {
                    EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                    WriteTabs(level + 1);
                    SourceWrite(string.Format("{0}::", @class.FullNameManaged));
                    Write(string.Format("{0}({1}* native)", @class.ManagedName, @class.FullyQualifiedName));
                    HeaderWriteLine(';');
                    SourceWriteLine();
                    if (@class.BaseClass != null)
                    {
                        WriteTabs(1, true);
                        SourceWriteLine(string.Format(": {0}(native)", @class.BaseClass.ManagedName));
                    }
                    SourceWriteLine('{');
                    if (@class.BaseClass == null)
                    {
                        WriteTabs(1, true);
                        SourceWriteLine("_native = native;");
                    }
                    SourceWriteLine('}');
                    hasHeaderWhiteSpace = false;
                    hasSourceWhiteSpace = false;
                }

                // Write destructor & finalizer
                if (@class.BaseClass == null)
                {
                    // ECMA-372 19.13.2: "The access-specifier of a finalizer in a ref class is ignored."
                    WriteTabs(level + 1);
                    HeaderWriteLine(string.Format("!{0}();", @class.ManagedName));
                    // ECMA-372 19.13.1: "The access-specifier of a destructor in a ref class is ignored."
                    WriteTabs(level + 1);
                    HeaderWriteLine(string.Format("~{0}();", @class.ManagedName));
                    hasHeaderWhiteSpace = false;

                    EnsureSourceWhiteSpace();
                    SourceWriteLine(string.Format("{0}::~{1}()", @class.FullNameManaged, @class.ManagedName));
                    SourceWriteLine('{');
                    SourceWriteLine(string.Format("\tthis->!{0}();", @class.ManagedName));
                    SourceWriteLine('}');
                    SourceWriteLine();

                    SourceWriteLine(string.Format("{0}::!{1}()", @class.FullNameManaged, @class.ManagedName));
                    SourceWriteLine('{');
                    if (@class.IsTrackingDisposable)
                    {
                        SourceWriteLine("\tif (this->IsDisposed)");
                        SourceWriteLine("\t\treturn;");
                        SourceWriteLine();
                        SourceWriteLine("\tOnDisposing(this, nullptr);");
                        SourceWriteLine();
                    }
                    if (@class.HasPreventDelete)
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
                    if (@class.IsTrackingDisposable)
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
                    else
                    {
                        // Default constructor
                        MethodDefinition constructor = new MethodDefinition(@class.Name, @class, 0);
                        constructor.IsConstructor = true;
                        OutputMethod(constructor, level);
                    }
                }
            }

            // Write non-constructor methods
            var methods = @class.Methods.Where(m => !m.IsConstructor);
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
            foreach (PropertyDefinition prop in @class.Properties)
            {
                string typeConditional = GetTypeConditional(prop.Type, @class.Header);
                if (typeConditional != null)
                {
                    WriteLine(string.Format("#ifndef {0}", typeConditional));
                    hasSourceWhiteSpace = true;
                }
                else
                {
                    EnsureHeaderWhiteSpace();
                }

                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);

                WriteTabs(level + 1);
                HeaderWriteLine(string.Format("property {0} {1}",
                    BulletParser.GetTypeRefName(prop.Type), prop.Name));
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

                string headerConditional;
                if (headerConditionals.TryGetValue(header.ManagedName, out headerConditional))
                {
                    SourceWriteLine(string.Format("#ifndef {0}", headerConditional));
                    SourceWriteLine();
                }

                // Write includes
                if (header.Includes.Count != 0)
                {
                    foreach (var include in header.Includes)
                    {
                        HeaderWriteLine(string.Format("#include \"{0}.h\"", include.ManagedName));
                    }
                    HeaderWriteLine();
                }

                // Write namespace
                HeaderWriteLine(string.Format("namespace {0}", NamespaceName));
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
                            SourceWrite("#ifndef ");
                            SourceWriteLine(headerConditionals[refHeader.ManagedName]);
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

                if (headerConditionals.ContainsKey(header.ManagedName))
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
