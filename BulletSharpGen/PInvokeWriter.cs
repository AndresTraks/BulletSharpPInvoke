using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    public class PInvokeWriter : DotNetWriter
    {
        bool hasCppClassSeparatingWhitespace;
        private Dictionary<string, string> _wrapperHeaderGuards;

        public PInvokeWriter(WrapperProject project)
            : base(project)
        {
            _wrapperHeaderGuards = new Dictionary<string, string>
            {
                {"btActionInterface", "_BT_ACTION_INTERFACE_H"},
                {"btBroadphaseAabbCallback", "BT_BROADPHASE_INTERFACE_H"},
                {"btBroadphaseRayCallback", "BT_BROADPHASE_INTERFACE_H"},
                {"ContactResultCallback", "BT_COLLISION_WORLD_H"},
                {"ConvexResultCallback", "BT_COLLISION_WORLD_H"},
                {"RayResultCallback", "BT_COLLISION_WORLD_H"},
                {"btIDebugDraw", "BT_IDEBUG_DRAW__H"},
                {"btMotionState", "BT_MOTIONSTATE_H"},
                {"btSerializer", "BT_SERIALIZER_H"},
                {"btInternalTriangleIndexCallback", "BT_TRIANGLE_CALLBACK_H"},
                {"btTriangleCallback", "BT_TRIANGLE_CALLBACK_H"},
                {"IControl", "_BT_SOFT_BODY_H"},
                {"ImplicitFn", "_BT_SOFT_BODY_H"}
            };
        }

        private static string GetTypeName(TypeRefDefinition type)
        {
            return BulletParser.GetTypeName(type).Replace("::", "_");
        }

        private static string GetTypeNameCS(TypeRefDefinition type)
        {
            if (type.IsBasic) return type.ManagedNameCS;
            if (type.HasTemplateTypeParameter) return type.ManagedNameCS;

            if (type.IsPointer && type.Referenced.ManagedNameCS.Equals("void")) // void*
            {
                return "IntPtr";
            }

            return BulletParser.GetTypeNameCS(type);
        }

        void WriteDeleteMethodCS(MethodDefinition method, int level)
        {
            To = WriteTo.CS;

            WriteLine("Dispose()");
            WriteLine(level + 1, "{");
            WriteLine(level + 2, "Dispose(true);");
            WriteLine(level + 2, "GC.SuppressFinalize(this);");
            WriteLine(level + 1, "}");
            WriteLine();

            WriteLine(level + 1, "protected virtual void Dispose(bool disposing)");
            WriteLine(level + 1, "{");
            WriteLine(level + 2, "if (_native != IntPtr.Zero)");
            WriteLine(level + 2, "{");
            if (method.Parent.HasPreventDelete)
            {
                WriteLine(level + 3, "if (!_preventDelete)");
                WriteLine(level + 3, "{");
                WriteLine(level + 4, $"{GetFullNameC(method.Parent)}_delete(_native);");
                WriteLine(level + 3, "}");
            }
            else
            {
                WriteLine(level + 3, $"{GetFullNameC(method.Parent)}_delete(_native);");
            }
            WriteLine(level + 3, "_native = IntPtr.Zero;");
            WriteLine(level + 2, "}");
            WriteLine(level + 1, "}");

            // C# Destructor
            WriteLine();
            WriteLine(level + 1, $"~{method.Parent.ManagedName}()");
            WriteLine(level + 1, "{");
            WriteLine(level + 2, "Dispose(false);");
            WriteLine(level + 1, "}");
        }

        private void WriteMethodDeclaration(MethodDefinition method, int numParameters, int level, int overloadIndex,
            ParameterDefinition outValueParameter = null)
        {
            // Do not write accessor methods of C# properties
            WriteTo cs = (method.Property == null) ? WriteTo.CS : WriteTo.None;

            // Cached properties that are initialized only once do not need a DllImport for the get method
            WriteTo dllImport;
            if (method.Property != null && method.Property.Setter == null &&
                method.Parent.CachedProperties.ContainsKey(method.Property.Name))
            {
                dllImport = WriteTo.None;
            }
            else
            {
                dllImport = WriteTo.Buffer;
            }

            // Skip delete methods in classes that can't be constructed (including all subclasses).
            if (method.Name.Equals("delete"))
            {
                if (method.Parent.HidePublicConstructors)
                {
                    // TODO: Check all subclasses
                    //return;
                }
            }

            Write(1, "EXPORT ", WriteTo.Header);

            Write(level + 1, "public ", cs);

            // DllImport clause
            if (dllImport == WriteTo.Buffer)
            {
                To = WriteTo.Buffer;
                WriteLine(level + 1,
                    "[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]");
                if (method.ReturnType != null && method.ReturnType.ManagedName.Equals("bool"))
                {
                    WriteLine(level + 1, "[return: MarshalAs(UnmanagedType.I1)]");
                }
                Write(level + 1, "static extern ");
            }

            // Return type
            if (method.IsConstructor)
            {
                Write($"{GetFullNameC(method.Parent)}* ", WriteTo.Header);
                Write($"{method.Parent.FullyQualifiedName}* ", WriteTo.Source);
                Write("IntPtr ", dllImport);
            }
            else
            {
                var returnType = method.ReturnType;

                if (method.IsStatic) Write("static ", cs);
                Write($"{GetTypeNameCS(returnType)} ", cs);

                if (outValueParameter != null)
                {
                    Write("void ", WriteTo.Header | WriteTo.Source | dllImport);
                }
                else
                {
                    Write($"{GetTypeName(returnType)} ", WriteTo.Header | WriteTo.Source);

                    if (returnType.IsBasic)
                    {
                        Write(returnType.ManagedNameCS, dllImport);
                    }
                    else if (returnType.HasTemplateTypeParameter)
                    {
                        Write(returnType.ManagedNameCS, dllImport);
                    }
                    else if (returnType.Referenced != null)
                    {
                        Write("IntPtr", dllImport);
                    }
                    else
                    {
                        // Return structures to an additional out parameter, not immediately
                        Write("void", dllImport);
                    }
                    Write(' ', dllImport);
                }
            }

            // Name
            string methodName = method.IsConstructor ? "new" : method.Name;
            if (overloadIndex != 0)
            {
                methodName += (overloadIndex + 1).ToString();
            }
            Write($"{GetFullNameC(method.Parent)}_{methodName}",
                WriteTo.Header | WriteTo.Source | dllImport);

            if (methodName.Equals("delete"))
            {
                WriteDeleteMethodCS(method, level);
            }
            else
            {
                Write($"{method.ManagedName}(", cs);
            }


            // Parameters
            Write('(', WriteTo.Header | WriteTo.Source | dllImport);

            var parameters = method.Parameters.Take(numParameters);
            var parametersCs = parameters.Select(p => $"{GetTypeNameCS(p.Type)} {p.ManagedName}");
            Write(ListToLines(parametersCs, WriteTo.CS, level + 1), cs);
            if (!methodName.Equals("delete"))
            {
                WriteLine(')', cs);
            }

            if (outValueParameter != null) parameters = parameters.Concat(new[] { outValueParameter });

            var parametersCpp = parameters.Select(p => $"{GetTypeName(p.Type)} {p.Name}");
            var parametersHeader = parametersCpp;
            var parametersSource = parametersCpp;
            var parametersDllImport = parameters.Select(p => $"{BulletParser.GetTypeDllImport(p.Type)} {p.Name}");

            // The first parameter is the instance pointer (if not constructor or static method)
            if (!method.IsConstructor && !method.IsStatic)
            {
                parametersHeader =
                    new[] { $"{GetFullNameC(method.Parent)}* obj" }.Concat(parametersHeader);
                parametersSource =
                    new[] { $"{method.Parent.FullyQualifiedName}* obj" }.Concat(parametersSource);
                parametersDllImport =
                    new[] { "IntPtr obj" }.Concat(parametersDllImport);
            }

            WriteLine($"{string.Join(", ", parametersHeader)});", WriteTo.Header);
            WriteLine($"{ListToLines(parametersSource, WriteTo.Source)})", WriteTo.Source);
            WriteLine($"{string.Join(", ", parametersDllImport)});", dllImport);
        }

        void WriteMethodDefinitionC(MethodDefinition method, int numParameters, int overloadIndex, int level,
            ParameterDefinition outValueParameter)
        {
            To = WriteTo.Source;
            string qualifier = method.IsStatic ? $"{method.Parent.Name}::" : "obj->";

            // Struct field marshalling
            var field = method.Field;
            if (field != null)
            {
                string fieldName = $"{qualifier}{field.Name}";

                if (field.Type.Target != null && field.Type.Target.MarshalAsStruct)
                {
                    string macroPrefix = field.Type.Name.ToUpper();
                    string paramName = (outValueParameter != null ? outValueParameter : method.Parameters[0]).Name;
                    if (method.Name.Equals(method.Property.Getter.Name))
                    {
                        WriteLine(1, $"{macroPrefix}_SET({paramName}, {fieldName});");
                    }
                    else
                    {
                        WriteLine(1, $"{macroPrefix}_COPY(&{fieldName}, {paramName});");
                    }
                }
                else if (method.Name.Equals(method.Property.Getter.Name))
                {
                    WriteLine(1, $"return {qualifier}{field.Name};");
                }
                else
                {
                    WriteLine(1, $"{qualifier}{field.Name} = value;");
                }
                return;
            }

            // Parameter marshalling prologues
            bool needTypeMarshalEpilogue = false;
            foreach (var param in method.Parameters.Take(numParameters))
            {
                if (param.Type.IsReference && param.Type.Referenced.Target != null &&
                    param.Type.Referenced.Target.MarshalAsStruct)
                {
                    string macroPrefix = param.Type.Referenced.Target.Name.ToUpper();
                    if (param.MarshalDirection == MarshalDirection.Out)
                    {
                        WriteLine(1, $"{macroPrefix}_DEF({param.Name});");
                        needTypeMarshalEpilogue = true;
                    }
                    else
                    {
                        WriteLine(1, $"{macroPrefix}_IN({param.Name});");
                    }
                }
            }

            WriteTabs(1);

            if (method.IsConstructor)
            {
                Write($"return new {method.Parent.FullyQualifiedName}");
            }
            else
            {
                if (!method.IsVoid)
                {
                    var returnType = method.ReturnType;

                    if (outValueParameter != null)
                    {
                        string macroPrefix = outValueParameter.Type.Referenced.Name.ToUpper();
                        if (returnType.IsReference)
                        {
                            Write($"{macroPrefix}_COPY({outValueParameter.Name}, ");
                        }
                        else
                        {
                            Write($"{macroPrefix}_SET({outValueParameter.Name}, ");
                        }
                    }
                    else if (needTypeMarshalEpilogue)
                    {
                        // Save the return value and return it after the epilogues
                        Write($"{BulletParser.GetTypeRefName(returnType)} ret = ");
                    }
                    else
                    {
                        // Return immediately
                        Write("return ");
                    }

                    if (!returnType.IsBasic && !returnType.IsPointer && !returnType.IsConstantArray)
                    {
                        if (!(returnType.Target != null && returnType.Target is EnumDefinition))
                        {
                            Write('&');
                        }
                    }
                }

                Write($"{qualifier}{method.Name}");
            }

            // Call parameters
            var originalParams = method.Parameters.Take(numParameters)
                .Select(p =>
                {
                    if (p.Type.Target != null && p.Type.Target.MarshalAsStruct)
                    {
                        string macroPrefix = p.Type.Target.Name.ToUpper();
                        return $"{macroPrefix}_USE({p.Name})";
                    }

                    if (p.Type.IsReference && p.Type.Referenced.Target != null && p.Type.Referenced.Target.MarshalAsStruct)
                    {
                        string macroPrefix = p.Type.Referenced.Target.Name.ToUpper();
                        return $"{macroPrefix}_USE({p.Name})";
                    }

                    if (p.Type.IsBasic || p.Type.IsPointer || p.Type.IsConstantArray) return p.Name;

                    return $"*{p.Name}";
                });
            Write("(");
            Write($"{ListToLines(originalParams, WriteTo.Source, 1)})");
            if (outValueParameter != null)
            {
                Write(")");
            }
            WriteLine(";");

            // Write type marshalling epilogue
            if (needTypeMarshalEpilogue)
            {
                foreach (var param in method.Parameters.Take(numParameters)
                    .Where(p => p.MarshalDirection == MarshalDirection.Out ||
                        p.MarshalDirection == MarshalDirection.InOut))
                {
                    if (param.Type.IsReference && param.Type.Referenced.Target != null &&
                        param.Type.Referenced.Target.MarshalAsStruct)
                    {
                        string macroPrefix = param.Type.Referenced.Target.Name.ToUpper();
                        WriteLine(1, $"{macroPrefix}_DEF_OUT({param.Name});");
                    }
                }

                if (!method.IsVoid)
                {
                    WriteLine(1, "return ret;");
                }
            }
        }

        void WriteMethodDefinitionCS(MethodDefinition method, int numParameters, int overloadIndex, int level,
            ParameterDefinition outValueParameter)
        {
            To = WriteTo.CS;
            if (method.IsConstructor)
            {
                if (method.Parent.BaseClass == null)
                {
                    Write("_native = ");
                }
                Write($"{GetFullNameC(method.Parent)}_new");
            }
            else
            {
                if (!method.IsVoid)
                {
                    if (outValueParameter != null)
                    {
                        // Temporary variable
                        WriteLine(string.Format("{0} {1};",
                            BulletParser.GetTypeNameCS(method.ReturnType),
                            outValueParameter.ManagedName));
                        WriteTabs(level + 2);
                    }

                    Write($"return {BulletParser.GetTypeMarshalConstructorStartCS(method)}");
                }

                Write($"{GetFullNameC(method.Parent)}_{method.Name}");
            }

            if (overloadIndex != 0)
            {
                Write((overloadIndex + 1).ToString());
            }

            Write('(');

            var parameters = method.Parameters.Take(numParameters)
                .Select(p => BulletParser.GetTypeCSMarshal(p));

            // The first parameter is the instance pointer (if not constructor or static method)
            if (!method.IsConstructor && !method.IsStatic)
            {
                parameters = new[] { "_native" }.Concat(parameters);
            }

            Write(ListToLines(parameters, WriteTo.CS, level + 2));

            if (method.IsConstructor && method.Parent.BaseClass != null)
            {
                Write(")");
                if (method.Parent.BaseClass.HasPreventDelete)
                {
                    Write(", false");
                }
                WriteLine(")");
                WriteLine(level + 1, "{");
            }
            else
            {
                if (!method.IsConstructor && !method.IsVoid)
                {
                    Write(BulletParser.GetTypeMarshalConstructorEndCS(method));
                }
                WriteLine(");");
            }

            // Cache property values
            if (method.IsConstructor)
            {
                var methodParent = method.Parent;
                while (methodParent != null)
                {
                    foreach (var cachedProperty in methodParent.CachedProperties.OrderBy(p => p.Key))
                    {
                        foreach (var param in method.Parameters)
                        {
                            if (param.ManagedName.ToLower().Equals(cachedProperty.Key.ToLower())
                                && param.Type.ManagedName.Equals(cachedProperty.Value.Property.Type.ManagedName))
                            {
                                WriteLine(level + 2,
                                    $"{cachedProperty.Value.CacheFieldName} = {param.ManagedName};");
                            }
                        }
                    }
                    methodParent = methodParent.BaseClass;
                }
            }

            // Return temporary variable
            if (outValueParameter != null)
            {
                WriteLine(level + 2, $"return {outValueParameter.ManagedName};");
            }
        }
        
        void WriteMethod(MethodDefinition method, int level, ref int overloadIndex, int numOptionalParams = 0)
        {
            EnsureWhiteSpace(WriteTo.Source);
            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                hasCppClassSeparatingWhitespace = true;
            }

            // Can't return whole structures, so use an output parameter
            // referencing the struct that will hold the return value.
            // "v method()" -> "void method(v* value)"
            // "v& method()" -> "void method(v* value)"
            ParameterDefinition outValueParameter = null;
            var returnType = method.ReturnType;
            if ((returnType.Target != null && returnType.Target.MarshalAsStruct) ||
                (returnType.IsReference && returnType.Referenced.Target != null && returnType.Referenced.Target.MarshalAsStruct))
            {
                var valueType = new TypeRefDefinition
                {
                    IsPointer = true,
                    Referenced = (returnType.IsReference ? returnType.Referenced : returnType).Copy()
                };
                valueType.Referenced.IsConst = false;

                string paramName;
                if (method.Property != null && method.Property.Setter != null)
                {
                    // Borrow out parameter name from setter
                    paramName = method.Property.Setter.Parameters[0].Name;
                }
                else
                {
                    paramName = "value";
                }

                outValueParameter = new ParameterDefinition(paramName, valueType) { ManagedName = paramName };
            }

            EnsureWhiteSpace(WriteTo.Source | WriteTo.CS);

            // Skip methods wrapped by C# properties
            WriteTo propertyTo = WriteTo.Header | WriteTo.Source | ((method.Property == null) ? WriteTo.CS : 0);

            int numOptionalParamsTotal = method.NumOptionalParameters;
            int numParameters = method.Parameters.Length - numOptionalParamsTotal + numOptionalParams;

            WriteMethodDeclaration(method, numParameters, level, overloadIndex, outValueParameter);

            if (method.Name.Equals("delete"))
            {
                WriteLine('{', WriteTo.Source);
                WriteLine(1, "delete obj;", WriteTo.Source);
                WriteLine('}', WriteTo.Source);
                hasSourceWhiteSpace = false;
                hasCSWhiteSpace = false;
                return;
            }

            // Constructor base call
            if (method.IsConstructor && method.Parent.BaseClass != null)
            {
                Write(level + 2, ": base(", WriteTo.CS & propertyTo);
            }
            else
            {
                WriteLine(level + 1, "{", WriteTo.CS & propertyTo);
                WriteTabs(level + 2, WriteTo.CS & propertyTo);
            }

            // Method body
            WriteLine('{', WriteTo.Source);
            WriteMethodDefinitionC(method, numParameters, overloadIndex, level, outValueParameter);
            WriteLine('}', WriteTo.Source);
            hasSourceWhiteSpace = false;

            if ((propertyTo & WriteTo.CS) != 0)
            {
                WriteMethodDefinitionCS(method, numParameters, overloadIndex, level, outValueParameter);
                WriteLine(level + 1, "}", WriteTo.CS);
                hasCSWhiteSpace = false;
            }

            // If there are optional parameters, then output all possible combinations of calls
            overloadIndex++;
            if (numOptionalParams < numOptionalParamsTotal)
            {
                WriteMethod(method, level, ref overloadIndex, numOptionalParams + 1);
            }
        }

        void WriteProperty(PropertyDefinition prop, int level)
        {
            To = WriteTo.CS;
            EnsureWhiteSpace();

            WriteLine(level + 1, $"public {GetTypeNameCS(prop.Type)} {prop.Name}");
            WriteLine(level + 1, "{");

            if (prop.Parent.CachedProperties.Keys.Contains(prop.Name))
            {
                var cachedProperty = prop.Parent.CachedProperties[prop.Name];
                WriteLine(level + 2, $"get {{ return {cachedProperty.CacheFieldName}; }}");

                if (prop.Setter != null)
                {
                    WriteLine(level + 2, "set");
                    WriteLine(level + 2, "{");
                    string marshal = BulletParser.GetTypeSetterCSMarshal(prop.Type);
                    WriteLine(level + 3,
                        $"{GetFullNameC(prop.Parent)}_{prop.Setter.Name}(_native, {marshal});");
                    WriteLine(level + 3, $"{cachedProperty.CacheFieldName} = value;");
                    WriteLine(level + 2, "}");
                }
            }
            else
            {
                Write(BulletParser.GetTypeGetterCSMarshal(prop, level));

                if (prop.Setter != null)
                {
                    string marshal = BulletParser.GetTypeSetterCSMarshal(prop.Type);
                    WriteLine(level + 2,
                        $"set {{ {GetFullNameC(prop.Parent)}_{prop.Setter.Name}(_native, {marshal}); }}");
                }
            }

            WriteLine(level + 1, "}");

            hasCSWhiteSpace = false;
        }

        IEnumerable<EnumDefinition> GetEnums(IEnumerable<ClassDefinition> classes)
        {
            foreach (var @class in classes)
            {
                var @enum = @class as EnumDefinition;
                if (@enum != null)
                {
                    yield return @enum;
                }
                else
                {
                    foreach (var childEnum in GetEnums(@class.NestedClasses))
                    {
                        yield return childEnum;
                    }
                }
            }
        }

        // Accepts a ClassDefinition for recursion
        void WriteEnumClass(ClassDefinition @class, int level)
        {
            var @enum = @class as EnumDefinition;
            if (@enum == null)
            {
                foreach (var childClass in @class.NestedClasses)
                {
                    WriteEnumClass(childClass, level);
                }
                return;
            }

            EnsureWhiteSpace();

            if (@enum.IsFlags) WriteLine(level, "[Flags]");

            WriteLine(level, $"public enum {@enum.ManagedName}");
            WriteLine(level, "{");
            for (int i = 0; i < @enum.EnumConstants.Count; i++)
            {
                WriteTabs(level + 1);
                if (@enum.EnumConstantValues[i].Equals(""))
                {
                    Write(@enum.EnumConstants[i]);
                }
                else
                {
                    Write($"{@enum.EnumConstants[i]} = {@enum.EnumConstantValues[i]}");
                }
                if (i < @enum.EnumConstants.Count - 1)
                {
                    Write(',');
                }
                WriteLine();
            }
            WriteLine(level, "}");
            hasCSWhiteSpace = false;
        }

        void WriteClass(ClassDefinition @class, int level)
        {
            if (_wrapperHeaderGuards.ContainsKey(@class.Name))
            {
                WriteWrapperClassConstructor(@class);
            }

            // Write class definition
            To = WriteTo.CS;
            EnsureWhiteSpace();
            Write(level, "public ");
            if (@class.IsAbstract) Write("abstract ");
            Write($"class {@class.ManagedName}");
            if (@class.BaseClass != null)
            {
                string baseClassName = GetFullNameManaged(@class.BaseClass);
                WriteLine($" : {baseClassName}");
            }
            else if (@class.IsStaticClass)
            {
                WriteLine();
            }
            else
            {
                WriteLine(" : IDisposable");
            }
            WriteLine(level, "{");
            hasCSWhiteSpace = true;

            // Write child classes
            foreach (var c in @class.NestedClasses
                .Where(c => !IsExcludedClass(c))
                .OrderBy(GetFullNameManaged))
            {
                WriteClass(c, level + 1);
            }

            // Write the native pointer to the base class
            To = WriteTo.CS;
            if (@class.BaseClass == null && !@class.IsStaticClass)
            {
                EnsureWhiteSpace();
                WriteLine(level + 1, "internal IntPtr _native;");
                if (@class.HasPreventDelete)
                {
                    WriteLine(level + 1, "bool _preventDelete;");
                }
                hasCSWhiteSpace = false;
            }

            // Write cached property fields
            if (@class.CachedProperties.Any())
            {
                EnsureWhiteSpace();
                foreach (var cachedProperty in @class.CachedProperties.OrderBy(p => p.Key))
                {
                    string fieldName = cachedProperty.Key;
                    fieldName = char.ToLower(fieldName[0]) + fieldName.Substring(1);
                    WriteLine(level + 1, string.Format("{0} {1} _{2};",
                        cachedProperty.Value.Access.ToString().ToLower(),
                        cachedProperty.Value.Property.Type.ManagedNameCS,
                        fieldName));
                }
                hasCSWhiteSpace = false;
            }

            // Write methods
            ClearBuffer();

            // Write constructors
            if (!@class.IsStaticClass)
            {
                // Write C# internal constructor
                if (!@class.NoInternalConstructor)
                {
                    EnsureWhiteSpace();
                    Write(level + 1, $"internal {@class.ManagedName}(IntPtr native");
                    if (@class.HasPreventDelete)
                    {
                        Write(", bool preventDelete");
                    }
                    WriteLine(')');
                    if (@class.BaseClass != null)
                    {
                        Write(level + 2, ": base(native");
                        if (@class.HasPreventDelete)
                        {
                            if (!@class.BaseClass.HasPreventDelete)
                            {
                                // Base class should also have preventDelete
                                //throw new NotImplementedException();
                            }
                            Write(", preventDelete");
                        }
                        else if (@class.BaseClass.HasPreventDelete)
                        {
                            Write(", true");
                        }
                        WriteLine(')');
                    }
                    WriteLine(level + 1, "{");
                    if (@class.BaseClass == null)
                    {
                        WriteLine(level + 2, "_native = native;");
                        if (@class.HasPreventDelete)
                        {
                            WriteLine(level + 2, "_preventDelete = preventDelete;");
                        }
                    }
                    WriteLine(level + 1, "}");
                    hasCSWhiteSpace = false;
                }

                // Write public constructors
                if (!@class.HidePublicConstructors && !@class.IsAbstract)
                {
                    int overloadIndex = 0;
                    var constructors = @class.Methods.Where(m => m.IsConstructor && !m.IsExcluded);
                    if (constructors.Any())
                    {
                        foreach (var constructor in constructors)
                        {
                            WriteMethod(constructor, level, ref overloadIndex);
                        }
                    }
                }
            }

            // Write methods
            var methods = @class.Methods.Where(m => !m.IsConstructor && !m.IsExcluded).OrderBy(m => m.Name);
            var methodsOverloads = methods.GroupBy(m => m.Name);
            foreach (var groupByName in methodsOverloads)
            {
                int overloadIndex = 0;
                foreach (var method in groupByName)
                {
                    WriteMethod(method, level, ref overloadIndex);
                }
            }

            // Write properties
            foreach (var prop in @class.Properties)
            {
                WriteProperty(prop, level);
            }

            // Write delete method
            if (@class.BaseClass == null && !@class.IsStaticClass)
            {
                int overloadIndex = 0;
                var del = new MethodDefinition("delete", @class, 0);
                del.ReturnType = new TypeRefDefinition("void");
                WriteMethod(del, level, ref overloadIndex);
                @class.Methods.Remove(del);
            }

            // Write DllImport clauses
            if (GetBufferString().Length != 0)
            {
                EnsureWhiteSpace(WriteTo.CS);
                Write(GetBufferString(), WriteTo.CS);
            }

            WriteLine(level, "}", WriteTo.CS);
            hasCSWhiteSpace = false;
            hasCppClassSeparatingWhitespace = false;
        }

        public void WriteWrapperClass(ClassDefinition @class)
        {
            List<MethodDefinition> baseVirtualMethods;
            var thisVirtualMethods = @class.Methods.Where(x => x.IsVirtual).ToList();
            var virtualMethods = thisVirtualMethods.ToList();
            if (@class.BaseClass != null)
            {
                baseVirtualMethods = @class.BaseClass.Methods.Where(x => x.IsVirtual).ToList();
                virtualMethods.AddRange(baseVirtualMethods);
            }
            else
            {
                baseVirtualMethods = new List<MethodDefinition>();
            }
            var methodCallbacks = virtualMethods.Select(m =>
            {
                string className = baseVirtualMethods.Contains(m) ?
                    GetFullNameC(@class.BaseClass) : GetFullNameC(@class);
                return $"p_{className}_{m.ManagedName} {m.Name}Callback";
            }).ToList();

            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine();
                hasCppClassSeparatingWhitespace = true;
            }

            // TODO: string headerGuard = wrapperHeaderGuards[@class.Name];
            string parameters;
            if (thisVirtualMethods.Count != 0)
            {
                foreach (var method in thisVirtualMethods)
                {
                    string methodPtr = $"p_{GetFullNameC(@class)}_{method.ManagedName}";
                    Write($"typedef {method.ReturnType.Name} (*{methodPtr})(", WriteTo.Header);
                    parameters = ListToLines(method.Parameters
                        .Select(p => $"{GetTypeName(p.Type)} {p.Name}"), WriteTo.Header);
                    WriteLine($"{parameters});", WriteTo.Header);
                }
                WriteLine();
            }

            // Wrapper class
            string wrapperClassName = $"{GetFullNameC(@class)}Wrapper";
            WriteLine($"class {wrapperClassName} : public {GetFullNameC(@class)}");
            WriteLine("{");
            WriteLine("private:");
            foreach (var m in virtualMethods)
            {
                string className = baseVirtualMethods.Contains(m) ?
                    GetFullNameC(@class.BaseClass) : GetFullNameC(@class);
                WriteLine(1, $"p_{className}_{m.ManagedName} _{m.Name}Callback;");
            }
            WriteLine();
            WriteLine("public:");

            // Wrapper constructor
            Write(1, $"{wrapperClassName}(");
            string constructorParams = ListToLines(methodCallbacks, WriteTo.Header, 1);
            WriteLine($"{constructorParams});");
            WriteLine();

            // Wrapper methods
            foreach (var m in virtualMethods)
            {
                Write(1, $"virtual {m.ReturnType.Name} {m.Name}(");
                string methodParams = ListToLines(
                    m.Parameters.Select(p => $"{GetTypeName(p.Type)} {p.Name}"),
                    WriteTo.Header, 1);
                WriteLine(methodParams + ");");
            }

            WriteLine("};");
            hasCppClassSeparatingWhitespace = false;


            var prevTo = To;
            To = WriteTo.Source;
            EnsureWhiteSpace();

            // Wrapper C++ Constructor
            Write($"{wrapperClassName}::{wrapperClassName}(");
            parameters = ListToLines(methodCallbacks, WriteTo.Source);
            WriteLine($"{parameters})");
            WriteLine('{');
            foreach (var method in virtualMethods)
            {
                WriteLine(1, string.Format("_{0}Callback = {0}Callback;", method.Name));
            }
            WriteLine('}');
            WriteLine();

            // Wrapper C++ methods
            foreach (var method in virtualMethods)
            {
                Write($"{method.ReturnType.Name} {wrapperClassName}::{method.Name}(");
                parameters = ListToLines(method.Parameters
                    .Select(p => $"{GetTypeName(p.Type)} {p.Name}"), WriteTo.Source);
                WriteLine($"{parameters})");

                WriteLine('{');
                WriteTabs(1);
                if (!method.IsVoid)
                {
                    Write("return ");
                }
                Write($"_{method.Name}Callback(");
                parameters = ListToLines(method.Parameters
                    .Select(p => p.Name), WriteTo.Source, 1);
                WriteLine($"{parameters});");
                WriteLine('}');
                WriteLine();
            }
            WriteLine();

            To = prevTo;
        }

        public void WriteWrapperClassConstructor(ClassDefinition @class)
        {
            List<MethodDefinition> baseVirtualMethods;
            var thisVirtualMethods = @class.Methods.Where(x => x.IsVirtual).ToList();
            var virtualMethods = thisVirtualMethods.ToList();
            if (@class.BaseClass != null)
            {
                baseVirtualMethods = @class.BaseClass.Methods.Where(x => x.IsVirtual).ToList();
                virtualMethods.AddRange(baseVirtualMethods);
            }
            else
            {
                baseVirtualMethods = new List<MethodDefinition>();
            }
            var methodCallbacks = virtualMethods.Select(m =>
            {
                string className = baseVirtualMethods.Contains(m) ?
                    GetFullNameC(@class.BaseClass) : GetFullNameC(@class);
                return $"p_{className}_{m.ManagedName} {m.Name}Callback";
            }).ToList();

            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                hasCppClassSeparatingWhitespace = true;
            }
            EnsureWhiteSpace(WriteTo.Source);

            // Wrapper C Constructor
            string wrapperClassName = $"{GetFullNameC(@class)}Wrapper";
            Write(1, "EXPORT ", WriteTo.Header);
            Write($"{wrapperClassName}* {wrapperClassName}_new(", WriteTo.Header | WriteTo.Source);
            WriteLine(ListToLines(methodCallbacks, WriteTo.Header, 1) + ");", WriteTo.Header);
            WriteLine(ListToLines(methodCallbacks, WriteTo.Source) + ")", WriteTo.Source);
            WriteLine('{', WriteTo.Source);
            Write(1, $"return new {wrapperClassName}(", WriteTo.Source);
            WriteLine(
                ListToLines(virtualMethods.Select(m => $"{m.Name}Callback"), WriteTo.Source, 1)
                 + ");", WriteTo.Source);
            WriteLine('}', WriteTo.Source);
            hasCppClassSeparatingWhitespace = false;
            hasSourceWhiteSpace = false;
        }

        void WriteHeader(HeaderDefinition header, string sourceRootFolder)
        {
            // Entirely skip headers that have no classes
            // TODO: parse C++ methods outside of classes
            if (header.AllClasses.All(@class =>
            {
                if (@class.IsExcluded || @class.IsTypedef) return true;
                return false;
            }))
            {
                return;
            }

            // Some headers only need a C# wrapper class, skip C++ part
            bool hasCppWrapper = header.AllClasses.Any(@class =>
                    !@class.IsExcluded &&
                    @class.Methods.Any(m => !m.IsExcluded) &&
                    !(@class is ClassTemplateDefinition));

            if (hasCppWrapper)
            {
                // C++ header file
                string headerPath = header.Name + "_wrap.h";
                string headerFullPath = Path.Combine(Project.CProjectPathFull, headerPath);
                OpenFile(headerFullPath, WriteTo.Header);
                WriteLine("#include \"main.h\"");
                WriteLine();

                // C++ source file
                string sourcePath = header.Name + "_wrap.cpp";
                string sourceFullPath = Path.Combine(Project.CProjectPathFull, sourcePath);
                OpenFile(sourceFullPath, WriteTo.Source);

                // C++ #includes
                var includes = new List<string>();
                foreach (var includeHeader in header.Includes.Concat(new[] {header}))
                {
                    // No need to include the base class header,
                    // it will already be included by the header of this class.
                    if (includeHeader != header &&
                        header.AllClasses.Any(c => c.BaseClass != null && c.BaseClass.Header == includeHeader))
                    {
                        continue;
                    }

                    string includePath =
                        WrapperProject.MakeRelativePath(sourceRootFolder, includeHeader.Filename).Replace('\\', '/');
                    includes.Add(includePath);
                }
                includes.Sort();
                foreach (var include in includes)
                {
                    WriteLine($"#include <{include}>");
                }
                WriteLine();
                if (RequiresConversionHeader(header))
                {
                    WriteLine("#include \"conversion.h\"");
                }
                WriteLine($"#include \"{headerPath}\"");
            }

            // C# source file
            string csPath = header.ManagedName + ".cs";
            string csFullPath = Path.Combine(Project.CsProjectPathFull, csPath);
            OpenFile(csFullPath, WriteTo.CS);
            WriteLine("using System;");
            if (RequiresInterop(header))
            {
                WriteLine("using System.Runtime.InteropServices;");
                WriteLine("using System.Security;");
            }
            if (RequiresMathNamespace(header))
            {
                WriteLine("using BulletSharp.Math;");
            }
            WriteLine();

            if (hasCppWrapper)
            {
                // Write wrapper class headers
                To = WriteTo.Header;
                hasCppClassSeparatingWhitespace = true;
                var wrappedClasses = header.AllClasses
                    .Where(x => _wrapperHeaderGuards.ContainsKey(x.Name))
                    .OrderBy(GetFullNameC).ToList();
                if (wrappedClasses.Count != 0)
                {
                    WriteLine($"#ifndef {_wrapperHeaderGuards[wrappedClasses[0].Name]}");
                    foreach (var @class in wrappedClasses)
                    {
                        foreach (var method in @class.Methods.Where(m => m.IsVirtual))
                        {
                            WriteLine($"#define p_{GetFullNameC(@class)}_{method.ManagedName} void*");
                        }
                    }
                    foreach (var @class in wrappedClasses)
                    {
                        WriteLine($"#define {GetFullNameC(@class)}Wrapper void");
                    }
                    WriteLine("#else");
                    foreach (var @class in wrappedClasses)
                    {
                        WriteWrapperClass(@class);
                    }
                    WriteLine("#endif");
                    WriteLine();
                }

                // Write classes
                WriteLine("extern \"C\"");
                WriteLine("{");
                hasCppClassSeparatingWhitespace = true;
            }

            To = WriteTo.CS;
            WriteLine($"namespace {Project.NamespaceName}");
            WriteLine("{");
            hasCSWhiteSpace = true;

            var enums = GetEnums(header.Classes)
                .OrderBy(e => e.ManagedName).ToList();
            foreach (var @enum in enums)
            {
                WriteEnumClass(@enum, 1);
            }

            foreach (var @class in header.Classes
                .Where(c => !IsExcludedClass(c)))
            {
                WriteClass(@class, 1);
            }

            if (hasCppWrapper)
            {
                WriteLine('}', WriteTo.Header);

                CloseFile(WriteTo.Header);
                CloseFile(WriteTo.Source);
            }

            WriteLine('}', WriteTo.CS);

            CloseFile(WriteTo.CS);
        }

        public override void Output()
        {
            Directory.CreateDirectory(Project.CsProjectPathFull);
            Directory.CreateDirectory(Project.CProjectPathFull);

            OpenFile(null, WriteTo.Buffer);

            // C++ header file (includes all other headers)
            string includeFilename = Project.NamespaceName + ".h";
            var includeFile = new FileStream(Path.Combine(Project.CProjectPathFull, includeFilename), FileMode.Create, FileAccess.Write);
            var includeWriter = new StreamWriter(includeFile);

            var sourceRootFolders = Project.SourceRootFoldersFull.Select(s => s.Replace('\\', '/'));
            var headers = Project.HeaderDefinitions.Values.Where(h => !h.IsExcluded && !h.Classes.All(c => c.IsExcluded));
            var headersByRoot = headers.GroupBy(h => sourceRootFolders.First(s => h.Filename.StartsWith(s)));
            foreach (var headerGroup in headersByRoot)
            {
                string sourceRootFolder = headerGroup.Key;
                foreach (var header in headerGroup.OrderBy(h => h.Name))
                {
                    WriteHeader(header, sourceRootFolder);

                    // Include header
                    string headerPath = header.Name + "_wrap.h";
                    includeWriter.WriteLine("#include \"{0}\"", headerPath);
                }
            }

            includeWriter.Dispose();
            includeFile.Dispose();

            Console.WriteLine("Write complete");
        }

        private static bool RequiresConversionHeader(HeaderDefinition header)
        {
            return header.Classes.Any(RequiresConversionHeader);
        }

        private static bool RequiresConversionHeader(ClassDefinition cl)
        {
            if (cl.NestedClasses.Any(RequiresConversionHeader))
            {
                return true;
            }

            foreach (var method in cl.Methods.Where(m => !m.IsExcluded))
            {
                if (DefaultParser.TypeRequiresMarshal(method.ReturnType))
                {
                    return true;
                }

                if (method.Parameters.Any(param => DefaultParser.TypeRequiresMarshal(param.Type)))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool RequiresInterop(HeaderDefinition header)
        {
            return header.Classes.Any(RequiresInterop);
        }

        private static bool RequiresInterop(ClassDefinition @class)
        {
            if (@class.Methods.Any(m => !m.IsConstructor && !m.IsExcluded)) return true;

            if (!@class.NoInternalConstructor && @class.BaseClass == null) return true;

            if (!@class.HidePublicConstructors && !@class.IsAbstract) return true;

            return false;
        }

        private static string[] _mathClasses = { "Quaternion", "Transform", "Vector4" };

        private static bool RequiresMathNamespace(HeaderDefinition header)
        {
            return header.Classes.Any(RequiresMathNamespace);
        }

        private static bool RequiresMathNamespace(ClassDefinition @class)
        {
            if (@class.NestedClasses.Any(RequiresMathNamespace)) return true;
            if (@class.IsExcluded) return false;

            foreach (var method in @class.Methods.Where(m => !m.IsExcluded))
            {
                if (@class.HidePublicConstructors && method.IsConstructor) continue;

                if (_mathClasses.Contains(method.ReturnType.ManagedName)) return true;

                if (method.Parameters.Any(param => _mathClasses.Contains(param.Type.ManagedName)))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetFullNameC(ClassDefinition @class)
        {
            string className;
            ClassTemplateDefinition template = @class as ClassTemplateDefinition;
            if (template != null)
            {
                className = @class.Name + string.Join("_", template.TemplateTypeParameters);
            }
            else
            {
                className = @class.Name;
            }

            if (@class.Parent != null)
            {
                return $"{GetFullNameC(@class.Parent)}_{@class.Name}";
            }
            if (@class.NamespaceName != "")
            {
                return $"{@class.NamespaceName}_{@class.Name}";
            }
            return @class.Name;
        }

        private static string GetFullNameManaged(ClassDefinition @class)
        {
            if (@class.Parent != null)
            {
                return $"{GetFullNameManaged(@class.Parent)}.{@class.ManagedName}";
            }
            return @class.ManagedName;
        }
    }
}
