using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    public class PInvokeWriter : WrapperWriter
    {
        bool hasCppClassSeparatingWhitespace;
        Dictionary<string, string> wrapperHeaderGuards = new Dictionary<string, string>();

        public PInvokeWriter(WrapperProject project)
            : base(project)
        {
            wrapperHeaderGuards.Add("btActionInterface", "_BT_ACTION_INTERFACE_H");
            wrapperHeaderGuards.Add("btBroadphaseAabbCallback", "BT_BROADPHASE_INTERFACE_H");
            wrapperHeaderGuards.Add("btBroadphaseRayCallback", "BT_BROADPHASE_INTERFACE_H");
            wrapperHeaderGuards.Add("ContactResultCallback", "BT_COLLISION_WORLD_H");
            wrapperHeaderGuards.Add("ConvexResultCallback", "BT_COLLISION_WORLD_H");
            wrapperHeaderGuards.Add("RayResultCallback", "BT_COLLISION_WORLD_H");
            wrapperHeaderGuards.Add("btIDebugDraw", "BT_IDEBUG_DRAW__H");
            wrapperHeaderGuards.Add("btMotionState", "BT_MOTIONSTATE_H");
            wrapperHeaderGuards.Add("btSerializer", "BT_SERIALIZER_H");
            wrapperHeaderGuards.Add("btInternalTriangleIndexCallback", "BT_TRIANGLE_CALLBACK_H");
            wrapperHeaderGuards.Add("btTriangleCallback", "BT_TRIANGLE_CALLBACK_H");
            wrapperHeaderGuards.Add("IControl", "_BT_SOFT_BODY_H");
            wrapperHeaderGuards.Add("ImplicitFn", "_BT_SOFT_BODY_H");
        }

        string GetTypeName(TypeRefDefinition type)
        {
            return BulletParser.GetTypeName(type).Replace("::", "_");
        }

        void WriteTypeCS(TypeRefDefinition type)
        {
            To = WriteTo.CS;

            if (type.IsBasic)
            {
                Write(type.ManagedNameCS);
            }
            else if (type.HasTemplateTypeParameter)
            {
                Write(type.ManagedNameCS);
            }
            else if (type.Referenced != null)
            {
                if (type.IsPointer && type.Referenced.ManagedNameCS.Equals("void")) // void*
                {
                    Write("IntPtr");
                }
                else
                {
                    Write(BulletParser.GetTypeNameCS(type));
                }
            }
            else
            {
                Write(BulletParser.GetTypeNameCS(type));
            }
        }

        void WriteDeleteMethodCS(MethodDefinition method, int level)
        {
            To = WriteTo.CS;

            // public void Dispose()
            WriteLine("Dispose()");
            WriteTabs(level + 1);
            WriteLine('{');
            WriteTabs(level + 2);
            WriteLine("Dispose(true);");
            WriteTabs(level + 2);
            WriteLine("GC.SuppressFinalize(this);");
            WriteTabs(level + 1);
            WriteLine('}');

            // protected virtual void Dispose(bool disposing)
            WriteLine();
            WriteTabs(level + 1);
            WriteLine("protected virtual void Dispose(bool disposing)");
            WriteTabs(level + 1);
            WriteLine("{");
            WriteTabs(level + 2);
            WriteLine("if (_native != IntPtr.Zero)");
            WriteTabs(level + 2);
            WriteLine('{');
            if (method.Parent.HasPreventDelete)
            {
                WriteTabs(level + 3);
                WriteLine("if (!_preventDelete)");
                WriteTabs(level + 3);
                WriteLine('{');
                WriteTabs(level + 4);
                Write(method.Parent.FullNameC);
                WriteLine("_delete(_native);");
                WriteTabs(level + 3);
                WriteLine('}');
            }
            else
            {
                WriteTabs(level + 3);
                Write(method.Parent.FullNameC);
                WriteLine("_delete(_native);");
            }
            WriteTabs(level + 3);
            WriteLine("_native = IntPtr.Zero;");
            WriteTabs(level + 2);
            WriteLine('}');
            WriteTabs(level + 1);
            WriteLine('}');

            // C# Destructor
            WriteLine();
            WriteTabs(level + 1);
            Write('~');
            Write(method.Parent.ManagedName);
            WriteLine("()");
            WriteTabs(level + 1);
            WriteLine('{');
            WriteTabs(level + 2);
            WriteLine("Dispose(false);");
            WriteTabs(level + 1);
            WriteLine('}');
        }

        private void WriteMethodDeclaration(MethodDefinition method, int numParameters, int level, int overloadIndex,
            MethodDefinition returnParamMethod = null)
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

            WriteTabs(1, WriteTo.Header);
            Write("EXPORT ", WriteTo.Header);

            WriteTabs(level + 1, cs);
            Write("public ", cs);

            // DllImport clause
            if (dllImport == WriteTo.Buffer)
            {
                To = WriteTo.Buffer;
                WriteTabs(level + 1);
                WriteLine("[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]");
                if (method.ReturnType != null && method.ReturnType.ManagedName.Equals("bool"))
                {
                    WriteTabs(level + 1);
                    WriteLine("[return: MarshalAs(UnmanagedType.I1)]");
                }
                WriteTabs(level + 1);
                Write("static extern ");
            }

            // Return type
            if (method.IsConstructor)
            {
                Write(method.Parent.FullNameC, WriteTo.Header);
                Write(method.Parent.FullyQualifiedName, WriteTo.Source);
                Write("* ", WriteTo.Header | WriteTo.Source);
                Write("IntPtr ", dllImport);
            }
            else
            {
                if (method.IsStatic)
                {
                    Write("static ", cs);
                }
                Write(GetTypeName(method.ReturnType), WriteTo.Header | WriteTo.Source);
                if (cs != 0)
                {
                    WriteTypeCS((returnParamMethod != null) ? returnParamMethod.ReturnType : method.ReturnType);
                }
                Write(' ', WriteTo.Header | WriteTo.Source | cs);
                if (method.ReturnType.IsBasic)
                {
                    Write(method.ReturnType.ManagedNameCS, dllImport);
                }
                else if (method.ReturnType.HasTemplateTypeParameter)
                {
                    Write(method.ReturnType.ManagedNameCS, dllImport);
                }
                else if (method.ReturnType.Referenced != null)
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

            // Name
            string methodName = method.IsConstructor ? "new" : method.Name;
            Write($"{method.Parent.FullNameC}_{methodName}",
                WriteTo.Header | WriteTo.Source | dllImport);

            if (methodName.Equals("delete"))
            {
                WriteDeleteMethodCS(method, level);
            }
            else
            {
                string methodNameManaged = method.IsConstructor ? method.Parent.ManagedName : method.ManagedName;
                Write(methodNameManaged, cs);
            }

            // Index number for overloaded methods
            if (overloadIndex != 0)
            {
                Write((overloadIndex + 1).ToString(), WriteTo.Header | WriteTo.Source | dllImport);
            }


            // Parameters
            if (!method.Name.Equals("delete"))
            {
                Write('(', cs);
            }
            Write('(', WriteTo.Header | WriteTo.Source | dllImport);

            // The first parameter is the instance pointer (if not constructor or static method)
            if (!method.IsConstructor && !method.IsStatic)
            {
                Write($"{method.Parent.FullNameC}* obj", WriteTo.Header);
                Write($"{method.Parent.FullyQualifiedName}* obj", WriteTo.Source);
                Write("IntPtr obj", dllImport);

                if (numParameters != 0)
                {
                    Write(", ", WriteTo.Header | WriteTo.Source | dllImport);
                }
            }

            for (int i = 0; i < numParameters; i++)
            {
                bool isFinalParameter = (i == numParameters - 1);
                bool isCsFinalParameter = returnParamMethod != null && isFinalParameter;
                var param = method.Parameters[i];

                // Parameter type
                if (!isCsFinalParameter)
                {
                    if (param.Type.Referenced != null && !(param.Type.IsConst || param.Type.Referenced.IsConst) &&
                        BulletParser.MarshalStructByValue(param.Type))
                    {
                        Write("out ", cs);
                    }
                }
                Write(GetTypeName(param.Type), WriteTo.Header | WriteTo.Source);
                if (cs != 0 && !isCsFinalParameter)
                {
                    WriteTypeCS(param.Type);
                }
                Write(BulletParser.GetTypeDllImport(param.Type), dllImport);

                // Parameter name
                if (!isCsFinalParameter)
                {
                    Write($" {param.ManagedName}", cs);
                }
                Write($" {param.Name}", WriteTo.Header | WriteTo.Source | dllImport);

                if (!isFinalParameter)
                {
                    Write(", ", WriteTo.Header | WriteTo.Source | dllImport);
                }
                if (!(isFinalParameter || (returnParamMethod != null && i == numParameters - 2)))
                {
                    Write(", ", WriteTo.CS);
                }
            }
            WriteLine(");", WriteTo.Header | dllImport);
            if (!method.Name.Equals("delete"))
            {
                WriteLine(')', cs);
            }
            WriteLine(')', WriteTo.Source);
        }

        void WriteMethodDefinition(MethodDefinition method, int numParameters, int overloadIndex, int level, MethodDefinition returnParamMethod)
        {
            // Skip methods wrapped by C# properties
            WriteTo cs = (method.Property == null) ? WriteTo.CS : WriteTo.None;

            // Field marshalling
            if (method.Field != null && method.Field.Type.Referenced == null &&
                BulletParser.MarshalStructByValue(method.Field.Type))
            {
                WriteTabs(1, WriteTo.Source);
                if (method.Property.Getter.Name == method.Name)
                {
                    WriteLine(BulletParser.GetFieldGetterMarshal(method.Parameters[0], method.Field), WriteTo.Source);
                }
                else
                {
                    WriteLine(BulletParser.GetFieldSetterMarshal(method.Parameters[0], method.Field), WriteTo.Source);
                }
                return;
            }

            int numParametersOriginal = numParameters;
            if (returnParamMethod != null)
            {
                numParametersOriginal--;
            }

            bool needTypeMarshalEpilogue = false;
            if (!(method.Property != null && BulletParser.MarshalStructByValue(method.Property.Type) &&
                  method.Property.Getter.Name == method.Name))
            {
                // Type marshalling prologue
                for (int i = 0; i < numParametersOriginal; i++)
                {
                    var param = method.Parameters[i];
                    string prologue = BulletParser.GetTypeMarshalPrologue(param, method);
                    if (!string.IsNullOrEmpty(prologue))
                    {
                        WriteTabs(1, WriteTo.Source);
                        WriteLine(prologue, WriteTo.Source);
                    }

                    // Do we need a type marshalling epilogue?
                    if (!needTypeMarshalEpilogue)
                    {
                        string epilogue = BulletParser.GetTypeMarshalEpilogue(param);
                        if (!string.IsNullOrEmpty(epilogue))
                        {
                            needTypeMarshalEpilogue = true;
                        }
                    }
                }
            }

            WriteTabs(1, WriteTo.Source);

            if (returnParamMethod != null)
            {
                // Temporary variable
                WriteLine(string.Format("{0} {1};",
                    BulletParser.GetTypeNameCS(returnParamMethod.ReturnType),
                    method.Parameters[numParametersOriginal].ManagedName), cs);
                WriteTabs(level + 2, cs);

                Write(BulletParser.GetReturnValueMarshalStart(returnParamMethod.ReturnType), WriteTo.Source);
            }

            if (method.IsConstructor)
            {
                Write($"return new {method.Parent.FullyQualifiedName}", WriteTo.Source);
                if (method.Parent.BaseClass == null)
                {
                    Write("_native = ", WriteTo.CS);
                }
                Write($"{method.Parent.FullNameC}_new", WriteTo.CS);
            }
            else
            {
                if (!method.IsVoid)
                {
                    var returnType = method.ReturnType;
                    if (needTypeMarshalEpilogue)
                    {
                        // Store return value in a temporary variable
                        Write(BulletParser.GetTypeRefName(returnType), WriteTo.Source);
                        Write(" ret = ", WriteTo.Source);
                    }
                    else
                    {
                        // Return immediately
                        Write("return ", WriteTo.Source);
                    }

                    Write("return ", cs);
                    Write(BulletParser.GetTypeMarshalConstructorStartCS(method), cs);

                    if (!returnType.IsBasic && !returnType.IsPointer && !returnType.IsConstantArray)
                    {
                        if (!(returnType.Target != null && returnType.Target is EnumDefinition))
                        {
                            Write('&', WriteTo.Source);
                        }
                    }
                }

                if (method.IsStatic)
                {
                    Write(method.Parent.Name, WriteTo.Source);
                    Write("::", WriteTo.Source);
                }
                else
                {
                    Write("obj->", WriteTo.Source);
                }
                if (method.Field == null)
                {
                    Write(method.Name, WriteTo.Source);
                }

                Write(method.Parent.FullNameC, cs);
                Write('_', cs);
                Write(method.Name, cs);
            }
            if (overloadIndex != 0)
            {
                Write((overloadIndex + 1).ToString(), cs);
            }

            // Call parameters
            if (method.Field != null)
            {
                Write(method.Field.Name, WriteTo.Source);
                if (method.Property.Setter != null && method.Name.Equals(method.Property.Setter.Name))
                {
                    Write(" = value", WriteTo.Source);
                }
                WriteLine(';', WriteTo.Source);
            }
            else
            {
                Write('(', WriteTo.Source);

                for (int i = 0; i < numParametersOriginal; i++)
                {
                    var param = method.Parameters[i];

                    string marshal = BulletParser.GetTypeMarshal(param);
                    if (!string.IsNullOrEmpty(marshal))
                    {
                        Write(marshal, WriteTo.Source);
                    }
                    else
                    {
                        if (!param.Type.IsBasic && !param.Type.IsPointer && !param.Type.IsConstantArray)
                        {
                            Write('*', WriteTo.Source);
                        }
                        Write(param.Name, WriteTo.Source);
                    }

                    if (i != numParametersOriginal - 1)
                    {
                        Write(", ", WriteTo.Source);
                    }
                }

                if (returnParamMethod != null)
                {
                    WriteLine(BulletParser.GetReturnValueMarshalEnd(method.Parameters[numParametersOriginal]), WriteTo.Source);
                }
                else
                {
                    WriteLine(");", WriteTo.Source);
                }
            }

            if (cs != 0)
            {
                Write('(', WriteTo.CS);
                if (!method.IsConstructor && !method.IsStatic)
                {
                    Write("_native", WriteTo.CS);
                    if (numParametersOriginal != 0 || returnParamMethod != null)
                    {
                        Write(", ", WriteTo.CS);
                    }
                }

                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    Write(BulletParser.GetTypeCSMarshal(param), WriteTo.CS);

                    if (i != numParameters - 1)
                    {
                        Write(", ", WriteTo.CS);
                    }
                }

                if (method.IsConstructor && method.Parent.BaseClass != null)
                {
                    Write(")", WriteTo.CS);
                    if (method.Parent.BaseClass.HasPreventDelete)
                    {
                        Write(", false", WriteTo.CS);
                    }
                    WriteLine(")", WriteTo.CS);
                    WriteTabs(level + 1, WriteTo.CS);
                    WriteLine('{', WriteTo.CS);
                }
                else
                {
                    if (!method.IsConstructor && !method.IsVoid)
                    {
                        Write(BulletParser.GetTypeMarshalConstructorEndCS(method), cs);
                    }
                    WriteLine(");", WriteTo.CS);
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
                                if (param.ManagedName.ToLower() == cachedProperty.Key.ToLower()
                                    && param.Type.ManagedName == cachedProperty.Value.Property.Type.ManagedName)
                                {
                                    WriteTabs(level + 2, WriteTo.CS);
                                    WriteLine($"{cachedProperty.Value.CacheFieldName} = {param.ManagedName};", WriteTo.CS);
                                }
                            }
                        }
                        methodParent = methodParent.BaseClass;
                    }
                }
            }

            // Return temporary variable
            if (returnParamMethod != null)
            {
                WriteTabs(level + 2, cs);
                Write("return ", cs);
                Write(method.Parameters[numParametersOriginal].ManagedName, cs);
                WriteLine(';', cs);
            }

            // Write type marshalling epilogue
            if (needTypeMarshalEpilogue)
            {
                for (int i = 0; i < numParametersOriginal; i++)
                {
                    var param = method.Parameters[i];
                    string epilogue = BulletParser.GetTypeMarshalEpilogue(param);
                    if (!string.IsNullOrEmpty(epilogue))
                    {
                        WriteTabs(1, WriteTo.Source);
                        WriteLine(epilogue, WriteTo.Source);
                    }
                }

                if (!method.IsVoid)
                {
                    WriteTabs(1, WriteTo.Source);
                    WriteLine("return ret;", WriteTo.Source);
                }
            }
        }

        void WriteMethod(MethodDefinition method, int level, ref int overloadIndex, int numOptionalParams = 0,
            MethodDefinition returnParamMethod = null)
        {
            EnsureWhiteSpace(WriteTo.Source);
            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                hasCppClassSeparatingWhitespace = true;
            }

            // Can't return whole structures, so append an output parameter
            // referencing the struct that will hold the return value.
            // convert "v method(param)" to "void method(param, &v)"
            if (!method.IsConstructor && !method.ReturnType.IsBasic &&
                !method.ReturnType.IsPointer && BulletParser.MarshalStructByValue(method.ReturnType))
            {
                var method2 = method.Copy();
                var paras = method2.Parameters;
                Array.Resize(ref paras, paras.Length + 1);
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
                var valueType = new TypeRefDefinition
                {
                    IsBasic = false,
                    IsPointer = true,
                    Referenced = method2.ReturnType
                };
                paras[paras.Length - 1] = new ParameterDefinition(paramName, valueType)
                {
                    ManagedName = paramName
                };
                method2.Parameters = paras;
                method2.ReturnType = new TypeRefDefinition();
                WriteMethod(method2, level, ref overloadIndex, numOptionalParams, method);
                return;
            }

            EnsureWhiteSpace(WriteTo.Source | WriteTo.CS);

            // Skip methods wrapped by C# properties
            WriteTo propertyTo = WriteTo.Header | WriteTo.Source | ((method.Property == null) ? WriteTo.CS : 0);

            int numOptionalParamsTotal = method.NumOptionalParameters;
            int numParameters = method.Parameters.Length - numOptionalParamsTotal + numOptionalParams;

            WriteMethodDeclaration(method, numParameters, level, overloadIndex, returnParamMethod);

            if (method.Name.Equals("delete"))
            {
                WriteLine('{', WriteTo.Source);
                WriteTabs(1, WriteTo.Source);
                WriteLine("delete obj;", WriteTo.Source);
                WriteLine('}', WriteTo.Source);
                hasSourceWhiteSpace = false;
                hasCSWhiteSpace = false;
                return;
            }

            // Constructor base call
            if (method.IsConstructor && method.Parent.BaseClass != null)
            {
                WriteTabs(level + 2, WriteTo.CS & propertyTo);
                Write(": base(", WriteTo.CS & propertyTo);
            }
            else
            {
                WriteTabs(level + 1, WriteTo.CS & propertyTo);
                WriteLine('{', WriteTo.CS & propertyTo);
                WriteTabs(level + 2, WriteTo.CS & propertyTo);
            }

            // Method body
            WriteLine('{', WriteTo.Source);

            WriteMethodDefinition(method, numParameters, overloadIndex, level, returnParamMethod);

            WriteTabs(level + 1, WriteTo.CS & propertyTo);
            WriteLine('}', WriteTo.Source | WriteTo.CS & propertyTo);
            hasSourceWhiteSpace = false;
            if (method.Property == null)
            {
                hasCSWhiteSpace = false;
            }

            // If there are optional parameters, then output all possible combinations of calls
            overloadIndex++;
            if (numOptionalParams < numOptionalParamsTotal)
            {
                WriteMethod(method, level, ref overloadIndex, numOptionalParams + 1, returnParamMethod);
            }
        }

        void WriteProperty(PropertyDefinition prop, int level)
        {
            To = WriteTo.CS;
            EnsureWhiteSpace();

            WriteTabs(level + 1);
            Write("public ");
            WriteTypeCS(prop.Type);
            WriteLine($" {prop.Name}");
            WriteTabs(level + 1);
            WriteLine('{');

            if (prop.Parent.CachedProperties.Keys.Contains(prop.Name))
            {
                var cachedProperty = prop.Parent.CachedProperties[prop.Name];
                WriteTabs(level + 2);
                WriteLine($"get {{ return {cachedProperty.CacheFieldName}; }}");

                if (prop.Setter != null)
                {
                    WriteTabs(level + 2);
                    WriteLine("set");
                    WriteTabs(level + 2);
                    WriteLine("{");
                    WriteTabs(level + 3);
                    WriteLine(string.Format("{0}_{1}(_native, {2});",
                        prop.Parent.FullNameC,
                        prop.Setter.Name,
                        BulletParser.GetTypeSetterCSMarshal(prop.Type)));
                    WriteTabs(level + 3);
                    WriteLine($"{cachedProperty.CacheFieldName} = value;");
                    WriteTabs(level + 2);
                    WriteLine("}");
                }
            }
            else
            {
                Write(BulletParser.GetTypeGetterCSMarshal(prop, level));

                if (prop.Setter != null)
                {
                    WriteTabs(level + 2, WriteTo.CS);
                    WriteLine(string.Format("set {{ {0}_{1}(_native, {2}); }}",
                        prop.Parent.FullNameC,
                        prop.Setter.Name,
                        BulletParser.GetTypeSetterCSMarshal(prop.Type)));
                }
            }

            WriteTabs(level + 1);
            WriteLine('}');

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

            if (@enum.IsFlags)
            {
                WriteTabs(level);
                WriteLine("[Flags]");
            }

            WriteTabs(level);
            WriteLine($"public enum {@enum.ManagedName}");
            WriteTabs(level);
            WriteLine('{');
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
            WriteTabs(level);
            WriteLine('}');
            hasCSWhiteSpace = false;
        }

        void WriteClass(ClassDefinition c, int level)
        {
            if (c.IsExcluded || c.IsTypedef || c.IsPureEnum ||
                c is ClassTemplateDefinition || c is EnumDefinition)
            {
                return;
            }

            if (wrapperHeaderGuards.ContainsKey(c.Name))
            {
                WriteWrapperClassConstructor(c);
            }

            // Write class definition
            EnsureWhiteSpace(WriteTo.CS);
            WriteTabs(level, WriteTo.CS);
            Write("public ", WriteTo.CS);
            if (c.IsAbstract)
            {
                Write("abstract ", WriteTo.CS);
            }
            Write($"class {c.ManagedName}", WriteTo.CS);
            if (c.BaseClass != null)
            {
                Write(" : ", WriteTo.CS);
                WriteLine(c.BaseClass.FullNameCppCli.Replace("::", "."), WriteTo.CS);
            }
            else if (c.IsStaticClass)
            {
                WriteLine(WriteTo.CS);
            }
            else
            {
                WriteLine(" : IDisposable", WriteTo.CS);
            }
            WriteTabs(level, WriteTo.CS);
            WriteLine("{", WriteTo.CS);
            hasCSWhiteSpace = true;

            // Write child classes
            foreach (var cl in c.NestedClasses.OrderBy(x => x.FullNameCppCli))
            {
                WriteClass(cl, level + 1);
            }

            // Write the native pointer to the base class
            if (c.BaseClass == null && !c.IsStaticClass)
            {
                EnsureWhiteSpace(WriteTo.CS);
                WriteTabs(level + 1, WriteTo.CS);
                WriteLine("internal IntPtr _native;", WriteTo.CS);
                if (c.HasPreventDelete)
                {
                    WriteTabs(level + 1, WriteTo.CS);
                    WriteLine("bool _preventDelete;", WriteTo.CS);
                    hasCSWhiteSpace = false;
                }
                hasCSWhiteSpace = false;
            }

            // Write cached property fields
            if (c.CachedProperties.Any())
            {
                EnsureWhiteSpace(WriteTo.CS);
                foreach (var cachedProperty in c.CachedProperties.OrderBy(p => p.Key))
                {
                    WriteTabs(level + 1, WriteTo.CS);
                    string name = cachedProperty.Key;
                    name = char.ToLower(name[0]) + name.Substring(1);
                    WriteLine(string.Format("{0} {1} _{2};",
                        cachedProperty.Value.Access.ToString().ToLower(),
                        cachedProperty.Value.Property.Type.ManagedNameCS,
                        name), WriteTo.CS);
                }
                hasCSWhiteSpace = false;
            }

            // Write methods
            ClearBuffer();

            // Write constructors
            if (!c.IsStaticClass)
            {
                // Write C# internal constructor
                if (!c.NoInternalConstructor)
                {
                    EnsureWhiteSpace(WriteTo.CS);
                    WriteTabs(level + 1, WriteTo.CS);
                    Write($"internal {c.ManagedName}(IntPtr native", WriteTo.CS);
                    if (c.HasPreventDelete)
                    {
                        Write(", bool preventDelete", WriteTo.CS);
                    }
                    WriteLine(')', WriteTo.CS);
                    if (c.BaseClass != null)
                    {
                        WriteTabs(level + 2, WriteTo.CS);
                        Write(": base(native", WriteTo.CS);
                        if (c.HasPreventDelete)
                        {
                            if (!c.BaseClass.HasPreventDelete)
                            {
                                // Base class should also have preventDelete
                                //throw new NotImplementedException();
                            }
                            Write(", preventDelete", WriteTo.CS);
                        }
                        else
                        {
                            if (c.BaseClass.HasPreventDelete)
                            {
                                Write(", true", WriteTo.CS);
                            }
                        }
                        WriteLine(')', WriteTo.CS);
                    }
                    WriteTabs(level + 1, WriteTo.CS);
                    WriteLine('{', WriteTo.CS);
                    if (c.BaseClass == null)
                    {
                        WriteTabs(level + 2, WriteTo.CS);
                        WriteLine("_native = native;", WriteTo.CS);
                        if (c.HasPreventDelete)
                        {
                            WriteTabs(level + 2, WriteTo.CS);
                            WriteLine("_preventDelete = preventDelete;", WriteTo.CS);
                        }
                    }
                    WriteTabs(level + 1, WriteTo.CS);
                    WriteLine('}', WriteTo.CS);
                    hasCSWhiteSpace = false;
                }

                // Write public constructors
                if (!c.HidePublicConstructors && !c.IsAbstract)
                {
                    int overloadIndex = 0;
                    var constructors = c.Methods.Where(m => m.IsConstructor && !m.IsExcluded);
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
            var methods = c.Methods.Where(m => !m.IsConstructor && !m.IsExcluded).OrderBy(m => m.Name);
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
            foreach (var prop in c.Properties)
            {
                WriteProperty(prop, level);
            }

            // Write delete method
            if (c.BaseClass == null && !c.IsStaticClass)
            {
                int overloadIndex = 0;
                var del = new MethodDefinition("delete", c, 0);
                del.ReturnType = new TypeRefDefinition();
                WriteMethod(del, level, ref overloadIndex);
                c.Methods.Remove(del);
            }

            // Write DllImport clauses
            if (GetBufferString().Length != 0)
            {
                EnsureWhiteSpace(WriteTo.CS);
                Write(GetBufferString(), WriteTo.CS);
            }

            WriteTabs(level, WriteTo.CS);
            WriteLine("}", WriteTo.CS);
            hasCSWhiteSpace = false;
            hasCppClassSeparatingWhitespace = false;
        }

        public void WriteWrapperClass(ClassDefinition @class)
        {
            var prevTo = To;

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
                string className = baseVirtualMethods.Contains(m) ? @class.BaseClass.FullNameC : @class.FullNameC;
                return $"p_{className}_{m.ManagedName} {m.Name}Callback";
            }).ToList();

            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine();
                hasCppClassSeparatingWhitespace = true;
            }

            // TODO: string headerGuard = wrapperHeaderGuards[@class.Name];
            if (thisVirtualMethods.Count != 0)
            {
                foreach (var method in thisVirtualMethods)
                {
                    Write(string.Format("typedef {0} (*p_{1}_{2})(",
                        method.ReturnType.Name, @class.FullNameC, method.ManagedName), WriteTo.Header);

                    string parameters = ListToLines(
                        method.Parameters.Select(p => $"{GetTypeName(p.Type)} {p.Name}"),
                        LineLengths[WriteTo.Header]);
                    WriteLine(parameters + ");");
                }
                WriteLine();
            }

            // Wrapper class
            WriteLine(string.Format("class {0}Wrapper : public {0}", @class.FullNameC));
            WriteLine("{");
            WriteLine("private:");
            foreach (var m in virtualMethods)
            {
                string className = baseVirtualMethods.Contains(m) ? @class.BaseClass.FullNameC : @class.FullNameC;
                WriteLine($"\tp_{className}_{m.ManagedName} _{m.Name}Callback;");
            }
            WriteLine();
            WriteLine("public:");

            // Wrapper constructor
            Write($"\t{@class.FullNameC}Wrapper(");
            string constructorParams = ListToLines(methodCallbacks, LineLengths[WriteTo.Header], 1);
            WriteLine(constructorParams + ");");
            WriteLine();

            // Wrapper methods
            foreach (var m in virtualMethods)
            {
                Write($"\tvirtual {m.ReturnType.Name} {m.Name}(");
                string methodParams = ListToLines(
                    m.Parameters.Select(p => $"{GetTypeName(p.Type)} {p.Name}"),
                    LineLengths[WriteTo.Header], 1);
                WriteLine(methodParams + ");");
            }

            WriteLine("};");
            hasCppClassSeparatingWhitespace = false;


            To = WriteTo.Source;
            EnsureWhiteSpace();

            // Wrapper C++ Constructor
            Write(string.Format("{0}Wrapper::{0}Wrapper(", @class.FullNameC));
            WriteLine(ListToLines(
                methodCallbacks, LineLengths[WriteTo.Source]) + ")");
            WriteLine('{');
            foreach (var method in virtualMethods)
            {
                WriteLine(string.Format("\t_{0}Callback = {0}Callback;", method.Name));
            }
            WriteLine('}');
            WriteLine();

            // Wrapper C++ methods
            foreach (var method in virtualMethods)
            {
                Write($"{method.ReturnType.Name} {@class.FullNameC}Wrapper::{method.Name}(");
                string parameters = ListToLines(
                    method.Parameters.Select(p => $"{GetTypeName(p.Type)} {p.Name}"),
                    LineLengths[WriteTo.Source]);
                WriteLine(parameters + ")");

                WriteLine('{');
                Write("\t");
                if (!method.IsVoid)
                {
                    Write("return ");
                }
                Write($"_{method.Name}Callback(");
                parameters = ListToLines(
                    method.Parameters.Select(p => p.Name),
                    LineLengths[WriteTo.Source], 1);
                WriteLine(parameters + ");");
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
                string className = baseVirtualMethods.Contains(m) ? @class.BaseClass.FullNameC : @class.FullNameC;
                return $"p_{className}_{m.ManagedName} {m.Name}Callback";
            });

            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                hasCppClassSeparatingWhitespace = true;
            }
            EnsureWhiteSpace(WriteTo.Source);

            // Wrapper C Constructor
            Write("\tEXPORT ", WriteTo.Header);
            Write(string.Format("{0}Wrapper* {0}Wrapper_new(", @class.FullNameC), WriteTo.Header | WriteTo.Source);
            WriteLine(ListToLines(methodCallbacks, LineLengths[WriteTo.Header], 1) + ");", WriteTo.Header);
            WriteLine(ListToLines(methodCallbacks, LineLengths[WriteTo.Source]) + ")", WriteTo.Source);
            WriteLine('{', WriteTo.Source);
            Write($"\treturn new {@class.FullNameC}Wrapper(", WriteTo.Source);
            WriteLine(ListToLines(
                virtualMethods.Select(m => $"{m.Name}Callback"),
                LineLengths[WriteTo.Source], 1) + ");",
                WriteTo.Source);
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
            bool hasCppWrapper = header.AllClasses.
                Any(@class => @class.AllNestedClasses.Any() || @class.Methods.Count != 0);

            if (hasCppWrapper)
            {
                // C++ header file
                string headerPath = header.Name + "_wrap.h";
                string headerFullPath = Path.Combine(Project.CProjectPathFull, headerPath);
                OpenFile(headerFullPath, WriteTo.Header);
                WriteLine("#include \"main.h\"", WriteTo.Header);
                WriteLine(WriteTo.Header);

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
                    WriteLine($"#include <{include}>", WriteTo.Source);
                }
                WriteLine(WriteTo.Source);
                if (RequiresConversionHeader(header))
                {
                    WriteLine("#include \"conversion.h\"", WriteTo.Source);
                }
                WriteLine($"#include \"{headerPath}\"", WriteTo.Source);
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
                var wrappedClasses =
                    header.AllClasses.Where(x => wrapperHeaderGuards.ContainsKey(x.Name))
                        .OrderBy(x => x.FullNameC)
                        .ToList();
                if (wrappedClasses.Count != 0)
                {
                    WriteLine($"#ifndef {wrapperHeaderGuards[wrappedClasses[0].Name]}");
                    foreach (var @class in wrappedClasses)
                    {
                        foreach (var method in @class.Methods.Where(m => m.IsVirtual))
                        {
                            WriteLine($"#define p_{@class.FullNameC}_{method.ManagedName} void*");
                        }
                    }
                    foreach (var @class in wrappedClasses)
                    {
                        WriteLine($"#define {@class.FullNameC}Wrapper void");
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

            var enums = GetEnums(header.Classes).OrderBy(e => e.ManagedName).ToList();
            foreach (var @enum in enums)
            {
                WriteEnumClass(@enum, 1);
            }

            foreach (var @class in header.Classes)
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
                if (BulletParser.TypeRequiresMarshal(method.ReturnType))
                {
                    return true;
                }

                if (method.Parameters.Any(param => BulletParser.TypeRequiresMarshal(param.Type)))
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

        private static string[] _mathClasses = { "Quaternion", "Transform", "Vector3", "Vector4" };

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
    }
}
