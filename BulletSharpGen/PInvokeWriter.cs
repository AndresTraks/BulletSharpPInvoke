using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    class PInvokeWriter : WrapperWriter
    {
        bool hasCppClassSeparatingWhitespace;
        Dictionary<string, string> wrapperHeaderGuards = new Dictionary<string, string>();
        WrapperProject project;

        string OutDirectoryPInvoke { get { return NamespaceName + "_pinvoke"; } }
        string OutDirectoryC { get { return NamespaceName + "_c"; } }

        public PInvokeWriter(WrapperProject project)
            : base(project.HeaderDefinitions.Values, project.NamespaceName)
        {
            this.project = project;

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

        void WriteType(TypeRefDefinition type, WriteTo writeTo)
        {
            if (type.IsBasic)
            {
                Write(type.Name.Replace("::", "_"), writeTo & WriteTo.Header);
                Write(type.Name, writeTo & WriteTo.Source);
            }
            else if (type.HasTemplateTypeParameter)
            {
                
            }
            else
            {
                string typeName;
                if (type.Referenced != null)
                {
                    if (type.Referenced.IsConst) // || type.IsConst
                    {
                        Write("const ", writeTo);
                    }

                    typeName = BulletParser.GetTypeName(type.Referenced) ?? string.Empty;
                }
                else
                {
                    if (type.IsConst)
                    {
                        Write("const ", writeTo);
                    }

                    typeName = BulletParser.GetTypeName(type);
                }
                if (type.Target != null && type.Target.IsPureEnum)
                {
                    Write(typeName + "::" + type.Target.Name, writeTo & WriteTo.Source);
                    Write(typeName.Replace("::", "_"), writeTo & WriteTo.Header);
                }
                else
                {
                    Write(typeName + '*', writeTo & WriteTo.Source);
                    Write(typeName.Replace("::", "_") + '*', writeTo & WriteTo.Header);
                }
            }
        }

        void WriteTypeCS(TypeRefDefinition type)
        {
            if (type.IsBasic)
            {
                Write(type.ManagedNameCS, WriteTo.CS);
            }
            else if (type.HasTemplateTypeParameter)
            {
                Write(type.ManagedNameCS, WriteTo.CS);
            }
            else if (type.Referenced != null)
            {
                if (type.IsPointer && type.Referenced.ManagedNameCS.Equals("void")) // void*
                {
                    Write("IntPtr", WriteTo.CS);
                }
                else
                {
                    Write(BulletParser.GetTypeNameCS(type), WriteTo.CS);
                }
            }
            else
            {
                Write(BulletParser.GetTypeNameCS(type), WriteTo.CS);
            }
        }

        void WriteDeleteMethodCS(MethodDefinition method, int level)
        {
            // public void Dispose()
            WriteLine("Dispose()", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine("Dispose(true);", WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine("GC.SuppressFinalize(this);", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);

            // protected virtual void Dispose(bool disposing)
            WriteLine(WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine("protected virtual void Dispose(bool disposing)", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine("{", WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine("if (_native != IntPtr.Zero)", WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            if (method.Parent.HasPreventDelete)
            {
                WriteTabs(level + 3, WriteTo.CS);
                WriteLine("if (!_preventDelete)", WriteTo.CS);
                WriteTabs(level + 3, WriteTo.CS);
                WriteLine('{', WriteTo.CS);
                WriteTabs(level + 4, WriteTo.CS);
                Write(method.Parent.FullNameCS, WriteTo.CS);
                WriteLine("_delete(_native);", WriteTo.CS);
                WriteTabs(level + 3, WriteTo.CS);
                WriteLine('}', WriteTo.CS);
            }
            else
            {
                WriteTabs(level + 3, WriteTo.CS);
                Write(method.Parent.FullNameCS, WriteTo.CS);
                WriteLine("_delete(_native);", WriteTo.CS);
            }
            WriteTabs(level + 3, WriteTo.CS);
            WriteLine("_native = IntPtr.Zero;", WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine('}', WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);

            // C# Destructor
            WriteLine(WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            Write('~', WriteTo.CS);
            Write(method.Parent.ManagedName, WriteTo.CS);
            WriteLine("()", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine("Dispose(false);", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);
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

            WriteTabs(1);
            Write("EXPORT ", WriteTo.Header);

            WriteTabs(level + 1, cs);
            Write("public ", cs);

            // DllImport clause
            WriteTabs(level + 1, dllImport);
            WriteLine("[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]", dllImport);
            if (method.ReturnType != null && method.ReturnType.ManagedName.Equals("bool"))
            {
                WriteTabs(level + 1, dllImport);
                WriteLine("[return: MarshalAs(UnmanagedType.I1)]", dllImport);
            }
            WriteTabs(level + 1, dllImport);
            Write("static extern ", dllImport);

            // Return type
            if (method.IsConstructor)
            {
                Write(method.Parent.FullNameCS, WriteTo.Header);
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
                WriteType(method.ReturnType, WriteTo.Header | WriteTo.Source);
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
            Write(string.Format("{0}_{1}", method.Parent.FullNameCS, methodName),
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
                Write(string.Format("{0}* obj", method.Parent.FullNameCS), WriteTo.Header);
                Write(string.Format("{0}* obj", method.Parent.FullyQualifiedName), WriteTo.Source);
                Write("IntPtr obj", dllImport);

                if (numParameters != 0)
                {
                    Write(", ", WriteTo.Header | WriteTo.Source | dllImport);
                }
            }

            for (int i = 0; i < numParameters; i++)
            {
                bool isFinalParameter = (i == numParameters - 1);
                bool isCsFinalParameter = (returnParamMethod != null) ? isFinalParameter : false;
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
                WriteType(param.Type, WriteTo.Header | WriteTo.Source);
                if (cs != 0 && !isCsFinalParameter)
                {
                    WriteTypeCS(param.Type);
                }
                Write(BulletParser.GetTypeDllImport(param.Type), dllImport);

                // Parameter name
                if (!isCsFinalParameter)
                {
                    Write(string.Format(" {0}", param.ManagedName), cs);
                }
                Write(string.Format(" {0}", param.Name), WriteTo.Header | WriteTo.Source | dllImport);

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
                Write(string.Format("return new {0}", method.Parent.FullyQualifiedName), WriteTo.Source);
                if (method.Parent.BaseClass == null)
                {
                    Write("_native = ", WriteTo.CS);
                }
                Write(string.Format("{0}_new", method.Parent.FullNameCS), WriteTo.CS);
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
                        if (!(returnType.Target != null && returnType.Target.IsPureEnum))
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

                Write(method.Parent.FullNameCS, cs);
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
                    if (numParametersOriginal != 0)
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
                                    WriteLine(string.Format("{0} = {1};",
                                        cachedProperty.Value.CacheFieldName, param.ManagedName), WriteTo.CS);
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
                paras[paras.Length - 1] = new ParameterDefinition(paramName, valueType);
                paras[paras.Length - 1].ManagedName = paramName;
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
            EnsureWhiteSpace(WriteTo.CS);

            WriteTabs(level + 1, WriteTo.CS);
            Write("public ", WriteTo.CS);
            WriteTypeCS(prop.Type);
            WriteLine(string.Format(" {0}", prop.Name), WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('{', WriteTo.CS);

            if (prop.Parent.CachedProperties.Keys.Contains(prop.Name))
            {
                var cachedProperty = prop.Parent.CachedProperties[prop.Name];
                WriteTabs(level + 2, WriteTo.CS);
                WriteLine(string.Format("get {{ return {0}; }}", cachedProperty.CacheFieldName), WriteTo.CS);

                if (prop.Setter != null)
                {
                    WriteTabs(level + 2, WriteTo.CS);
                    WriteLine("set", WriteTo.CS);
                    WriteTabs(level + 2, WriteTo.CS);
                    WriteLine("{", WriteTo.CS);
                    WriteTabs(level + 3, WriteTo.CS);
                    WriteLine(string.Format("{0}_{1}(_native, {2});",
                        prop.Parent.FullNameCS,
                        prop.Setter.Name,
                        BulletParser.GetTypeSetterCSMarshal(prop.Type)), WriteTo.CS);
                    WriteTabs(level + 3, WriteTo.CS);
                    WriteLine(string.Format("{0} = value;", cachedProperty.CacheFieldName), WriteTo.CS);
                    WriteTabs(level + 2, WriteTo.CS);
                    WriteLine("}", WriteTo.CS);
                }
            }
            else
            {
                Write(BulletParser.GetTypeGetterCSMarshal(prop, level), WriteTo.CS);

                if (prop.Setter != null)
                {
                    WriteTabs(level + 2, WriteTo.CS);
                    WriteLine(string.Format("set {{ {0}_{1}(_native, {2}); }}",
                        prop.Parent.FullNameCS,
                        prop.Setter.Name,
                        BulletParser.GetTypeSetterCSMarshal(prop.Type)), WriteTo.CS);
                }
            }

            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);

            hasCSWhiteSpace = false;
        }

        // Accepts a ClassDefinition for recursion
        void WriteEnumClass(ClassDefinition @class, int level)
        {
            var @enum = @class as EnumDefinition;
            if (@enum == null)
            {
                foreach (var childClass in @class.Classes)
                {
                    WriteEnumClass(childClass, level);
                }
                return;
            }

            EnsureWhiteSpace(WriteTo.CS);

            if (@enum.IsFlags)
            {
                WriteTabs(level, WriteTo.CS);
                WriteLine("[Flags]", WriteTo.CS);
            }

            WriteTabs(level, WriteTo.CS);
            WriteLine(string.Format("public enum {0}", @enum.ManagedName), WriteTo.CS);
            WriteTabs(level, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            for (int i = 0; i < @enum.EnumConstants.Count; i++)
            {
                WriteTabs(level + 1, WriteTo.CS);
                if (@enum.EnumConstantValues[i].Equals(""))
                {
                    Write(@enum.EnumConstants[i], WriteTo.CS);
                }
                else
                {
                    Write(string.Format("{0} = {1}", @enum.EnumConstants[i], @enum.EnumConstantValues[i]), WriteTo.CS);
                }
                if (i < @enum.EnumConstants.Count - 1)
                {
                    Write(',', WriteTo.CS);
                }
                WriteLine(WriteTo.CS);
            }
            WriteTabs(level, WriteTo.CS);
            WriteLine('}', WriteTo.CS);
            hasCSWhiteSpace = false;
        }

        void WriteClass(ClassDefinition c, int level)
        {
            if (c.IsExcluded || c.IsTypedef || c.IsPureEnum || c is ClassTemplateDefinition)
            {
                return;
            }

            if (wrapperHeaderGuards.ContainsKey(c.Name))
            {
                WriteClassWrapper(c);
            }

            EnsureWhiteSpace(WriteTo.Source | WriteTo.CS);

            // Write class definition
            WriteTabs(level, WriteTo.CS);
            Write("public ", WriteTo.CS);
            if (c.IsAbstract)
            {
                Write("abstract ", WriteTo.CS);
            }
            Write(string.Format("class {0}", c.ManagedName), WriteTo.CS);
            if (c.BaseClass != null)
            {
                Write(" : ", WriteTo.CS);
                WriteLine(c.BaseClass.FullNameManaged.Replace("::", "."), WriteTo.CS);
                //WriteLine(c.BaseClass.ManagedNameCS, WriteTo.CS);
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
            foreach (var cl in c.Classes.OrderBy(x => x.FullNameManaged))
            {
                WriteClass(cl, level + 1);
            }

            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                hasCppClassSeparatingWhitespace = true;
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
            bufferBuilder.Clear();

            // Write constructors
            if (!c.IsStaticClass)
            {
                // Write C# internal constructor
                if (!c.NoInternalConstructor)
                {
                    EnsureWhiteSpace(WriteTo.CS);
                    WriteTabs(level + 1, WriteTo.CS);
                    Write(string.Format("internal {0}(IntPtr native", c.ManagedName), WriteTo.CS);
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
                    var constructors = c.Methods.Where(m => m.IsConstructor);
                    if (constructors.Any())
                    {
                        foreach (var constructor in constructors)
                        {
                            WriteMethod(constructor, level, ref overloadIndex);
                        }
                    }
                    else
                    {
                        // Default constructor
                        var constructor = new MethodDefinition(c.Name, c, 0);
                        constructor.IsConstructor = true;
                        WriteMethod(constructor, level, ref overloadIndex);
                    }
                }
            }

            // Write methods
            foreach (var groupByName in c.Methods.Where(m => !m.IsConstructor).OrderBy(m => m.Name).GroupBy(m => m.Name))
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
            if (bufferBuilder.Length != 0)
            {
                EnsureWhiteSpace(WriteTo.CS);
                Write(bufferBuilder.ToString(), WriteTo.CS);
            }

            WriteTabs(level, WriteTo.CS);
            WriteLine("}", WriteTo.CS);
            hasCSWhiteSpace = false;
            hasCppClassSeparatingWhitespace = false;
        }

        public void WriteClassWrapperMethodPointers(ClassDefinition cl)
        {
            List<MethodDefinition> baseAbstractMethods;
            var thisAbstractMethods = cl.Methods.Where(x => x.IsVirtual).ToList();
            var abstractMethods = thisAbstractMethods.ToList();
            if (cl.BaseClass != null)
            {
                baseAbstractMethods = cl.BaseClass.Methods.Where(x => x.IsVirtual).ToList();
                abstractMethods.AddRange(baseAbstractMethods);
            }
            else
            {
                baseAbstractMethods = new List<MethodDefinition>();
            }

            foreach (var method in thisAbstractMethods)
            {
                WriteLine(string.Format("#define p{0}_{1} void*", cl.ManagedName, method.ManagedName), WriteTo.Header);
            }
        }

        public void WriteClassWrapperMethodDeclarations(ClassDefinition cl)
        {
            List<MethodDefinition> baseAbstractMethods;
            var thisAbstractMethods = cl.Methods.Where(x => x.IsVirtual).ToList();
            var abstractMethods = thisAbstractMethods.ToList();
            if (cl.BaseClass != null)
            {
                baseAbstractMethods = cl.BaseClass.Methods.Where(x => x.IsVirtual).ToList();
                abstractMethods.AddRange(baseAbstractMethods);
            }
            else
            {
                baseAbstractMethods = new List<MethodDefinition>();
            }

            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header);
                hasCppClassSeparatingWhitespace = true;
            }

            string headerGuard = wrapperHeaderGuards[cl.Name];
            foreach (var method in thisAbstractMethods)
            {
                Write(string.Format("typedef {0} (*p{1}_{2})(", method.ReturnType.Name, cl.ManagedName, method.ManagedName), WriteTo.Header);
                int numParameters = method.Parameters.Length;
                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    WriteType(param.Type, WriteTo.Header);
                    Write(" ", WriteTo.Header);
                    Write(param.Name, WriteTo.Header);

                    if (i != numParameters - 1)
                    {
                        Write(", ", WriteTo.Header);
                    }
                }
                WriteLine(");", WriteTo.Header);
            }
            if (thisAbstractMethods.Count != 0)
            {
                WriteLine(WriteTo.Header);
            }
            WriteLine(string.Format("class {0}Wrapper : public {0}", cl.FullNameCS), WriteTo.Header);
            WriteLine("{", WriteTo.Header);
            WriteLine("private:", WriteTo.Header);
            foreach (var method in abstractMethods)
            {
                string className = baseAbstractMethods.Contains(method) ? cl.BaseClass.ManagedName : cl.ManagedName;
                WriteLine(string.Format("\tp{0}_{1} _{2}Callback;", className, method.ManagedName, method.Name), WriteTo.Header);
            }
            WriteLine(WriteTo.Header);
            WriteLine("public:", WriteTo.Header);

            // Wrapper constructor
            Write(string.Format("\t{0}Wrapper(", cl.FullNameCS), WriteTo.Header);
            int numMethods = abstractMethods.Count;
            for (int i = 0; i < numMethods; i++)
            {
                var method = abstractMethods[i];
                string className = baseAbstractMethods.Contains(method) ? cl.BaseClass.ManagedName : cl.ManagedName;
                Write(string.Format("p{0}_{1} {2}Callback", className, method.ManagedName, method.Name), WriteTo.Header);
                if (i != numMethods - 1)
                {
                    Write(", ", WriteTo.Header);
                }
            }
            WriteLine(");", WriteTo.Header);
            WriteLine(WriteTo.Header);

            foreach (var method in abstractMethods)
            {
                Write(string.Format("\tvirtual {0} {1}(", method.ReturnType.Name, method.Name), WriteTo.Header);
                int numParameters = method.Parameters.Length;
                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    WriteType(param.Type, WriteTo.Header);
                    Write(" ", WriteTo.Header);
                    Write(param.Name, WriteTo.Header);

                    if (i != numParameters - 1)
                    {
                        Write(", ", WriteTo.Header);
                    }
                }
                WriteLine(");", WriteTo.Header);
            }

            WriteLine("};", WriteTo.Header);
            hasCppClassSeparatingWhitespace = false;
        }

        public void WriteClassWrapperDefinition(ClassDefinition cl)
        {
            List<MethodDefinition> baseAbstractMethods;
            var thisAbstractMethods = cl.Methods.Where(x => x.IsVirtual).ToList();
            var abstractMethods = thisAbstractMethods.ToList();
            if (cl.BaseClass != null)
            {
                baseAbstractMethods = cl.BaseClass.Methods.Where(x => x.IsVirtual).ToList();
                abstractMethods.AddRange(baseAbstractMethods);
            }
            else
            {
                baseAbstractMethods = new List<MethodDefinition>();
            }

            EnsureWhiteSpace(WriteTo.Source);

            // Wrapper C++ Constructor
            Write(string.Format("{0}Wrapper::{0}Wrapper(", cl.Name), WriteTo.Source);
            int numMethods = abstractMethods.Count;
            for (int i = 0; i < numMethods; i++)
            {
                var method = abstractMethods[i];
                string className = baseAbstractMethods.Contains(method) ? cl.BaseClass.ManagedName : cl.ManagedName;
                Write(string.Format("p{0}_{1} {2}Callback", className, method.ManagedName, method.Name), WriteTo.Source);
                if (i != numMethods - 1)
                {
                    Write(", ", WriteTo.Source);
                }
            }
            WriteLine(')', WriteTo.Source);
            WriteLine('{', WriteTo.Source);
            foreach (var method in abstractMethods)
            {
                WriteLine(string.Format("\t_{0}Callback = {0}Callback;", method.Name), WriteTo.Source);
            }
            WriteLine('}', WriteTo.Source);
            WriteLine(WriteTo.Source);

            // Wrapper C++ methods
            foreach (var method in abstractMethods)
            {
                Write(string.Format("{0} {1}Wrapper::{2}(", method.ReturnType.Name, cl.Name, method.Name), WriteTo.Source);
                int numParameters = method.Parameters.Length;
                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    WriteType(param.Type, WriteTo.Source);
                    Write(" ", WriteTo.Source);
                    Write(param.Name, WriteTo.Source);

                    if (i != numParameters - 1)
                    {
                        Write(", ", WriteTo.Source);
                    }
                }
                WriteLine(')', WriteTo.Source);
                WriteLine('{', WriteTo.Source);
                Write("\t", WriteTo.Source);
                if (!method.IsVoid)
                {
                    Write("return ", WriteTo.Source);
                }
                Write(string.Format("_{0}Callback(", method.Name), WriteTo.Source);
                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    Write(param.Name, WriteTo.Source);

                    if (i != numParameters - 1)
                    {
                        Write(", ", WriteTo.Source);
                    }
                }
                WriteLine(");", WriteTo.Source);
                WriteLine('}', WriteTo.Source);
                WriteLine(WriteTo.Source);
            }
            WriteLine(WriteTo.Source);
        }

        public void WriteClassWrapper(ClassDefinition cl)
        {
            List<MethodDefinition> baseAbstractMethods;
            var thisAbstractMethods = cl.Methods.Where(x => x.IsVirtual).ToList();
            var abstractMethods = thisAbstractMethods.ToList();
            if (cl.BaseClass != null)
            {
                baseAbstractMethods = cl.BaseClass.Methods.Where(x => x.IsVirtual).ToList();
                abstractMethods.AddRange(baseAbstractMethods);
            }
            else
            {
                baseAbstractMethods = new List<MethodDefinition>();
            }

            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                hasCppClassSeparatingWhitespace = true;
            }
            EnsureWhiteSpace(WriteTo.Source);

            // Wrapper C Constructor
            Write("\tEXPORT ", WriteTo.Header);
            Write(string.Format("{0}Wrapper* {0}Wrapper_new(", cl.Name), WriteTo.Header | WriteTo.Source);
            int numMethods = abstractMethods.Count;
            for (int i = 0; i < numMethods; i++)
            {
                var method = abstractMethods[i];
                string className = baseAbstractMethods.Contains(method) ? cl.BaseClass.ManagedName : cl.ManagedName;
                Write(string.Format("p{0}_{1} {2}Callback", className, method.ManagedName, method.Name), WriteTo.Header | WriteTo.Source);
                if (i != numMethods - 1)
                {
                    Write(", ", WriteTo.Header | WriteTo.Source);
                }
            }
            WriteLine(");", WriteTo.Header);
            WriteLine(')', WriteTo.Source);
            WriteLine('{', WriteTo.Source);
            Write(string.Format("\treturn new {0}Wrapper(", cl.Name), WriteTo.Source);
            for (int i = 0; i < numMethods; i++)
            {
                var method = abstractMethods[i];
                Write(string.Format("{0}Callback", method.Name), WriteTo.Source);
                if (i != numMethods - 1)
                {
                    Write(", ", WriteTo.Source);
                }
            }
            WriteLine(");", WriteTo.Source);
            WriteLine('}', WriteTo.Source);
            hasCppClassSeparatingWhitespace = false;
            hasSourceWhiteSpace = false;
        }

        void WriteHeader(HeaderDefinition header, string sourceRootFolder)
        {
            // C++ header file
            string headerPath = header.Name + "_wrap.h";
            string headerFullPath = Path.Combine(OutDirectoryC, headerPath);
            var headerFile = new FileStream(headerFullPath, FileMode.Create, FileAccess.Write);
            headerWriter = new StreamWriter(headerFile);
            headerWriter.WriteLine("#include \"main.h\"");
            headerWriter.WriteLine();

            // C++ source file
            string sourcePath = header.Name + "_wrap.cpp";
            string sourceFullPath = Path.Combine(OutDirectoryC, sourcePath);
            var sourceFile = new FileStream(sourceFullPath, FileMode.Create, FileAccess.Write);
            sourceWriter = new StreamWriter(sourceFile);
            string headerIncludeName = header.Filename.Substring(sourceRootFolder.Length);
            sourceWriter.WriteLine("#include <{0}>", headerIncludeName);
            sourceWriter.WriteLine();
            if (RequiresConversionHeader(header))
            {
                sourceWriter.WriteLine("#include \"conversion.h\"");
            }
            sourceWriter.WriteLine("#include \"{0}\"", headerPath);

            // C# source file
            string csPath = header.ManagedName + ".cs";
            string csFullPath = Path.Combine(OutDirectoryPInvoke, csPath);
            var csFile = new FileStream(csFullPath, FileMode.Create, FileAccess.Write);
            csWriter = new StreamWriter(csFile);
            csWriter.WriteLine("using System;");
            if (RequiresInterop(header))
            {
                csWriter.WriteLine("using System.Runtime.InteropServices;");
                csWriter.WriteLine("using System.Security;");
            }
            if (RequiresMathNamespace(header))
            {
                csWriter.WriteLine("using BulletSharp.Math;");
            }
            csWriter.WriteLine();

            // Write wrapper class headers
            hasCppClassSeparatingWhitespace = true;
            var wrappedClasses = header.AllClasses.Where(x => wrapperHeaderGuards.ContainsKey(x.Name)).OrderBy(x => x.FullNameCS).ToList();
            if (wrappedClasses.Count != 0)
            {
                string headerGuard = wrapperHeaderGuards[wrappedClasses[0].Name];
                WriteLine("#ifndef " + headerGuard, WriteTo.Header);
                foreach (var @class in wrappedClasses)
                {
                    WriteClassWrapperMethodPointers(@class);
                }
                foreach (var @class in wrappedClasses)
                {
                    WriteLine(string.Format("#define {0}Wrapper void", @class.FullNameCS), WriteTo.Header);
                }
                WriteLine("#else", WriteTo.Header);
                foreach (var @class in wrappedClasses)
                {
                    WriteClassWrapperMethodDeclarations(@class);
                    WriteClassWrapperDefinition(@class);
                }
                WriteLine("#endif", WriteTo.Header);
                WriteLine(WriteTo.Header);
            }

            // Write classes
            headerWriter.WriteLine("extern \"C\"");
            headerWriter.WriteLine("{");
            csWriter.WriteLine("namespace {0}", NamespaceName);
            csWriter.WriteLine("{");
            hasCSWhiteSpace = true;
            hasCppClassSeparatingWhitespace = true;

            foreach (var @class in header.Classes)
            {
                WriteEnumClass(@class, 1);
            }

            foreach (var @class in header.Classes)
            {
                WriteClass(@class, 1);
            }

            headerWriter.WriteLine('}');
            csWriter.WriteLine('}');

            headerWriter.Dispose();
            headerFile.Dispose();
            sourceWriter.Dispose();
            sourceFile.Dispose();
            csWriter.Dispose();
            csFile.Dispose();
        }

        public override void Output()
        {
            string outDirectoryPInvoke = NamespaceName + "_pinvoke";
            string outDirectoryC = NamespaceName + "_c";

            Directory.CreateDirectory(outDirectoryPInvoke);
            Directory.CreateDirectory(outDirectoryC);

            // C++ header file (includes all other headers)
            string includeFilename = NamespaceName + ".h";
            var includeFile = new FileStream(outDirectoryC + "\\" + includeFilename, FileMode.Create, FileAccess.Write);
            var includeWriter = new StreamWriter(includeFile);

            var sourceRootFolders = project.SourceRootFolders.Select(s => s.Replace('\\', '/'));
            var headers = headerDefinitions.Where(h => !h.IsExcluded && !h.Classes.All(c => c.IsExcluded));
            var headersByRoot = headers.GroupBy(h => sourceRootFolders.Where(s => h.Filename.StartsWith(s)).First());
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
            if (cl.Classes.Any(RequiresConversionHeader))
            {
                return true;
            }

            foreach (var method in cl.Methods)
            {
                if (BulletParser.TypeRequiresMarshal(method.ReturnType))
                {
                    return true;
                }

                foreach (var param in method.Parameters)
                {
                    if (BulletParser.TypeRequiresMarshal(param.Type))
                    {
                        return true;
                    }
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
            if (@class.Methods.Any(m => !m.IsConstructor)) return true;

            if (!@class.NoInternalConstructor && @class.BaseClass == null) return true;

            if (!@class.HidePublicConstructors && !@class.IsAbstract) return true;

            return false;
        }

        private static bool RequiresMathNamespace(HeaderDefinition header)
        {
            return header.Classes.Any(RequiresMathNamespace);
        }

        private static bool RequiresMathNamespace(ClassDefinition @class)
        {
            if (@class.Classes.Any(RequiresMathNamespace))
            {
                return true;
            }

            if (@class.IsExcluded)
            {
                return false;
            }

            foreach (var method in @class.Methods)
            {
                if (@class.HidePublicConstructors && method.IsConstructor)
                {
                    continue;
                }

                if (method.ReturnType.ManagedName.Equals("Quaternion") ||
                    method.ReturnType.ManagedName.Equals("Transform") ||
                    method.ReturnType.ManagedName.Equals("Vector3"))
                {
                    return true;
                }

                foreach (var param in method.Parameters)
                {
                    if (param.Type.ManagedName.Equals("Quaternion") ||
                        param.Type.ManagedName.Equals("Transform") ||
                        param.Type.ManagedName.Equals("Vector3"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
