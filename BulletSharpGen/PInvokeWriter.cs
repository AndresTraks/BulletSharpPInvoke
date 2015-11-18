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

        public PInvokeWriter(IEnumerable<HeaderDefinition> headerDefinitions, string namespaceName)
            : base(headerDefinitions, namespaceName)
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
                    Write(typeName + "::" + type.Target.Enum.Name, writeTo & WriteTo.Source);
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

        void WriteDeleteMethod(MethodDefinition method, int level)
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

        void WriteMethodDeclaration(MethodDefinition method, int numParameters, int level, int overloadIndex, MethodDefinition returnParamMethod = null)
        {
            // Skip methods wrapped by C# properties
            WriteTo cs = (method.Property == null) ? WriteTo.CS : WriteTo.None;

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
            WriteTabs(level + 1, WriteTo.Buffer);
            WriteLine("[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]", WriteTo.Buffer);
            if (method.ReturnType != null && method.ReturnType.ManagedName.Equals("bool"))
            {
                WriteTabs(level + 1, WriteTo.Buffer);
                WriteLine("[return: MarshalAs(UnmanagedType.I1)]", WriteTo.Buffer);
            }
            WriteTabs(level + 1, WriteTo.Buffer);
            Write("static extern ", WriteTo.Buffer);

            // Return type
            if (method.IsConstructor)
            {
                Write(method.Parent.FullNameCS, WriteTo.Header);
                Write(method.Parent.FullName, WriteTo.Source);
                Write("* ", WriteTo.Header | WriteTo.Source);
                Write("IntPtr ", WriteTo.Buffer);
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
                    Write(method.ReturnType.ManagedNameCS, WriteTo.Buffer);
                }
                else if (method.ReturnType.HasTemplateTypeParameter)
                {
                    Write(method.ReturnType.ManagedNameCS, WriteTo.Buffer);
                }
                else if (method.ReturnType.Referenced != null)
                {
                    Write("IntPtr", WriteTo.Buffer);
                }
                else
                {
                    // Return structures to an additional out parameter, not immediately
                    Write("void", WriteTo.Buffer);
                }
                Write(' ', WriteTo.Buffer);
            }

            // Name
            Write(method.Parent.FullNameCS, WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
            Write('_', WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
            if (method.IsConstructor)
            {
                Write("new", WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                Write(method.Parent.ManagedName, WriteTo.CS);
            }
            else
            {
                Write(method.Name, WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                if (method.Name.Equals("delete"))
                {
                    WriteDeleteMethod(method, level);
                }
                else
                {
                    Write(method.ManagedName, cs);
                }
            }

            // Index number for overloaded methods
            if (overloadIndex != 0)
            {
                Write((overloadIndex + 1).ToString(), WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
            }


            // Parameters
            if (!method.Name.Equals("delete"))
            {
                Write('(', cs);
            }
            Write('(', WriteTo.Header | WriteTo.Source | WriteTo.Buffer);

            // The first parameter is the instance pointer (if not constructor or static method)
            if (!method.IsConstructor && !method.IsStatic)
            {
                Write(method.Parent.FullNameCS, WriteTo.Header);
                Write(method.Parent.FullName, WriteTo.Source);
                Write("* obj", WriteTo.Header | WriteTo.Source);
                Write("IntPtr obj", WriteTo.Buffer);

                if (numParameters != 0)
                {
                    Write(", ", WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
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
                Write(BulletParser.GetTypeDllImport(param.Type), WriteTo.Buffer);

                // Parameter name
                if (!isCsFinalParameter)
                {
                    Write(' ', cs);
                    Write(param.ManagedName, cs);
                }
                Write(' ', WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                Write(param.Name, WriteTo.Header | WriteTo.Source | WriteTo.Buffer);

                if (!isFinalParameter)
                {
                    Write(", ", WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                }
                if (!(isFinalParameter || (returnParamMethod != null && i == numParameters - 2)))
                {
                    Write(", ", WriteTo.CS);
                }
            }
            WriteLine(");", WriteTo.Header | WriteTo.Buffer);
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
            if (method.Field != null && method.Field.Type.Referenced == null && BulletParser.MarshalStructByValue(method.Field.Type))
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
            if (!(method.Property != null && BulletParser.MarshalStructByValue(method.Property.Type) && method.Property.Getter.Name == method.Name))
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
                Write(BulletParser.GetTypeNameCS(returnParamMethod.ReturnType), cs);
                Write(' ', cs);
                Write(method.Parameters[numParametersOriginal].ManagedName, cs);
                WriteLine(';', cs);
                WriteTabs(level + 2, cs);

                Write(BulletParser.GetReturnValueMarshalStart(returnParamMethod.ReturnType), WriteTo.Source);
            }

            if (method.IsConstructor)
            {
                Write("return new ", WriteTo.Source);
                Write(method.Parent.FullName, WriteTo.Source);
                if (method.Parent.BaseClass == null)
                {
                    Write("_native = ", WriteTo.CS);
                }
                Write(method.Parent.FullNameCS, WriteTo.CS);
                Write("_new", WriteTo.CS);
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
                    if (method.Parent.BaseClass.Target.HasPreventDelete)
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
            }

            // Check if any parameters should be assigned to properties
            // in constructors (this can't always work)
            // Move this to BulletParser?
            if (method.IsConstructor)
            {
                foreach (var param in method.Parameters)
                {
                    foreach (var property in method.Parent.Properties)
                    {
                        if (param.ManagedName.ToLower() == property.Name.ToLower()
                            && !param.Type.IsBasic)
                        {
                            // if (IsCacheableType) {
                            //Console.WriteLine("caching");
                            //Console.WriteLine(method.Parent.ManagedName);
                            //Console.WriteLine(param.Type.ToString() + " " + param.ManagedName);
                        }
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

        void WriteMethod(MethodDefinition method, int level, ref int overloadIndex, int numOptionalParams = 0, MethodDefinition returnParamMethod = null)
        {
            // Can't return whole structures, so append an output parameter
            // referencing the struct that will hold the return value.
            // convert "v method(param)" to "void method(param, &v)"
            if (!method.IsConstructor && !method.ReturnType.IsBasic &&
                !method.ReturnType.IsPointer && BulletParser.MarshalStructByValue(method.ReturnType))
            {
                var method2 = method.Copy();
                var paras = method2.Parameters;
                Array.Resize<ParameterDefinition>(ref paras, paras.Length + 1);
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
                var valueType = new TypeRefDefinition()
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
            Write(' ', WriteTo.CS);
            WriteLine(prop.Name, WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('{', WriteTo.CS);

            Write(BulletParser.GetTypeGetterCSMarshal(prop, level), WriteTo.CS);

            if (prop.Setter != null)
            {
                WriteTabs(level + 2, WriteTo.CS);
                Write("set { ", WriteTo.CS);
                Write(prop.Parent.FullNameCS, WriteTo.CS);
                Write('_', WriteTo.CS);
                Write(prop.Setter.Name, WriteTo.CS);
                Write("(_native, ", WriteTo.CS);
                Write(BulletParser.GetTypeSetterCSMarshal(prop.Type), WriteTo.CS);
                WriteLine("); }", WriteTo.CS);
            }

            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);

            hasCSWhiteSpace = false;
        }

        void WriteEnumClass(EnumDefinition e, int level)
        {
            EnsureWhiteSpace(WriteTo.CS);

            if (e.Name.EndsWith("Flags"))
            {
                WriteTabs(level, WriteTo.CS);
                WriteLine("[Flags]", WriteTo.CS);
            }

            WriteTabs(level, WriteTo.CS);
            WriteLine(string.Format("public enum {0}", e.Name), WriteTo.CS);
            WriteTabs(level, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            for (int i = 0; i < e.EnumConstants.Count; i++)
            {
                WriteTabs(level + 1, WriteTo.CS);
                Write(e.EnumConstants[i], WriteTo.CS);
                Write(" = ", WriteTo.CS);
                Write(e.EnumConstantValues[i], WriteTo.CS);
                if (i < e.EnumConstants.Count - 1)
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
            Write("public class ", WriteTo.CS);
            Write(c.ManagedName, WriteTo.CS);
            if (c.BaseClass != null)
            {
                Write(" : ", WriteTo.CS);
                WriteLine(c.BaseClass.Target.FullNameManaged.Replace("::", "."), WriteTo.CS);
                //WriteLine(c.BaseClass.ManagedNameCS, WriteTo.CS);
            }
            else
            {
                WriteLine(" : IDisposable", WriteTo.CS);
            }
            WriteTabs(level, WriteTo.CS);
            WriteLine("{", WriteTo.CS);
            hasCSWhiteSpace = true;

            // Write child classes
            foreach (ClassDefinition cl in c.Classes.OrderBy(x => x.FullNameManaged))
            {
                WriteClass(cl, level + 1);
            }

            if (!hasCppClassSeparatingWhitespace)
            {
                WriteLine(WriteTo.Header | WriteTo.Source);
                hasCppClassSeparatingWhitespace = true;
            }

            // Write native pointer
            if (c.BaseClass == null)
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

            // Write methods
            int overloadIndex = 0;
            bufferBuilder.Clear();

            // Write C# internal constructor
            if (!c.NoInternalConstructor)
            {
                EnsureWhiteSpace(WriteTo.CS);
                WriteTabs(level + 1, WriteTo.CS);
                Write("internal ", WriteTo.CS);
                Write(c.ManagedName, WriteTo.CS);
                Write("(IntPtr native", WriteTo.CS);
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
                        if (!c.BaseClass.Target.HasPreventDelete)
                        {
                            // Base class should also have preventDelete
                            //throw new NotImplementedException();
                        }
                        Write(", preventDelete", WriteTo.CS);
                    }
                    else
                    {
                        if (c.BaseClass.Target.HasPreventDelete)
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

            // Write constructors
            bool hasConstructors = false;
            if (!c.IsAbstract)
            {
                foreach (MethodDefinition method in c.Methods.Where(m => m.IsConstructor))
                {
                    if (!c.HidePublicConstructors)
                    {
                        WriteMethod(method, level, ref overloadIndex);
                    }
                    hasConstructors = true;
                }

                // Write default constructor
                if (!hasConstructors && !c.IsAbstract && !c.HidePublicConstructors)
                {
                    var constructor = new MethodDefinition(c.Name, c, 0);
                    constructor.IsConstructor = true;
                    WriteMethod(constructor, level, ref overloadIndex);
                }
                overloadIndex = 0;
            }

            // Write methods
            MethodDefinition previousMethod = null;
            foreach (MethodDefinition method in c.Methods.Where(m => !m.IsConstructor).OrderBy(m => m.Name))
            {
                if (previousMethod != null && previousMethod.Name != method.Name)
                {
                    overloadIndex = 0;
                }

                WriteMethod(method, level, ref overloadIndex);
                previousMethod = method;
            }
            overloadIndex = 0;

            // Write properties
            foreach (PropertyDefinition prop in c.Properties)
            {
                WriteProperty(prop, level);
            }

            // Write delete method
            if (c.BaseClass == null)
            {
                var del = new MethodDefinition("delete", c, 0);
                del.ReturnType = new TypeRefDefinition();
                WriteMethod(del, level, ref overloadIndex);
                c.Methods.Remove(del);
                overloadIndex = 0;
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
            List<MethodDefinition> thisAbstractMethods = cl.Methods.Where(x => x.IsVirtual).ToList();
            List<MethodDefinition> abstractMethods = thisAbstractMethods.ToList();
            if (cl.BaseClass != null)
            {
                baseAbstractMethods = cl.BaseClass.Target.Methods.Where(x => x.IsVirtual).ToList();
                abstractMethods.AddRange(baseAbstractMethods);
            }
            else
            {
                baseAbstractMethods = new List<MethodDefinition>();
            }

            foreach (MethodDefinition method in thisAbstractMethods)
            {
                WriteLine(string.Format("#define p{0}_{1} void*", cl.ManagedName, method.ManagedName), WriteTo.Header);
            }
        }

        public void WriteClassWrapperMethodDeclarations(ClassDefinition cl)
        {
            List<MethodDefinition> baseAbstractMethods;
            List<MethodDefinition> thisAbstractMethods = cl.Methods.Where(x => x.IsVirtual).ToList();
            List<MethodDefinition> abstractMethods = thisAbstractMethods.ToList();
            if (cl.BaseClass != null)
            {
                baseAbstractMethods = cl.BaseClass.Target.Methods.Where(x => x.IsVirtual).ToList();
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
            foreach (MethodDefinition method in thisAbstractMethods)
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
            foreach (MethodDefinition method in abstractMethods)
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

            foreach (MethodDefinition method in abstractMethods)
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
            List<MethodDefinition> thisAbstractMethods = cl.Methods.Where(x => x.IsVirtual).ToList();
            List<MethodDefinition> abstractMethods = thisAbstractMethods.ToList();
            if (cl.BaseClass != null)
            {
                baseAbstractMethods = cl.BaseClass.Target.Methods.Where(x => x.IsVirtual).ToList();
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
            foreach (MethodDefinition method in abstractMethods)
            {
                WriteLine(string.Format("\t_{0}Callback = {0}Callback;", method.Name), WriteTo.Source);
            }
            WriteLine('}', WriteTo.Source);
            WriteLine(WriteTo.Source);

            // Wrapper C++ methods
            foreach (MethodDefinition method in abstractMethods)
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
            List<MethodDefinition> thisAbstractMethods = cl.Methods.Where(x => x.IsVirtual).ToList();
            List<MethodDefinition> abstractMethods = thisAbstractMethods.ToList();
            if (cl.BaseClass != null)
            {
                baseAbstractMethods = cl.BaseClass.Target.Methods.Where(x => x.IsVirtual).ToList();
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

        public void Output()
        {
            string outDirectoryPInvoke = NamespaceName + "_pinvoke";
            string outDirectoryC = NamespaceName + "_c";

            Directory.CreateDirectory(outDirectoryPInvoke);
            Directory.CreateDirectory(outDirectoryC);

            // C++ header file (includes all other headers)
            string includeFilename = NamespaceName + ".h";
            var includeFile = new FileStream(outDirectoryC + "\\" + includeFilename, FileMode.Create, FileAccess.Write);
            var includeWriter = new StreamWriter(includeFile);

            foreach (var header in headerDefinitions.OrderBy(p => p.Name))
            {
                if (header.Classes.Count == 0)
                {
                    continue;
                }

                // C++ header file
                string headerFilename = header.Name + "_wrap.h";
                var headerFile = new FileStream(outDirectoryC + "\\" + headerFilename, FileMode.Create, FileAccess.Write);
                headerWriter = new StreamWriter(headerFile);
                headerWriter.WriteLine("#include \"main.h\"");
                headerWriter.WriteLine();

                // C++ source file
                var sourceFile = new FileStream(outDirectoryC + "\\" + header.Name + "_wrap.cpp", FileMode.Create, FileAccess.Write);
                sourceWriter = new StreamWriter(sourceFile);
                sourceWriter.Write("#include <");
                sourceWriter.Write(header.Filename);
                sourceWriter.WriteLine('>');
                sourceWriter.WriteLine();
                if (RequiresConversionHeader(header))
                {
                    sourceWriter.WriteLine("#include \"conversion.h\"");
                }
                sourceWriter.Write("#include \"");
                sourceWriter.Write(headerFilename);
                sourceWriter.WriteLine("\"");

                // C# source file
                var csFile = new FileStream(outDirectoryPInvoke + "\\" + header.ManagedName + ".cs", FileMode.Create, FileAccess.Write);
                csWriter = new StreamWriter(csFile);
                csWriter.WriteLine("using System;");
                csWriter.WriteLine("using System.Runtime.InteropServices;");
                csWriter.WriteLine("using System.Security;");
                if (RequiresMathNamespace(header))
                {
                    csWriter.WriteLine("using BulletSharp.Math;");
                }
                csWriter.WriteLine();

                // Write wrapper class headers
                hasCppClassSeparatingWhitespace = true;
                var wrappedClasses = header.AllSubClasses.Where(x => wrapperHeaderGuards.ContainsKey(x.Name)).OrderBy(x => x.FullNameCS).ToList();
                if (wrappedClasses.Count != 0)
                {
                    string headerGuard = wrapperHeaderGuards[wrappedClasses[0].Name];
                    WriteLine("#ifndef " + headerGuard, WriteTo.Header);
                    foreach (ClassDefinition c in wrappedClasses)
                    {
                        WriteClassWrapperMethodPointers(c);
                    }
                    foreach (ClassDefinition c in wrappedClasses)
                    {
                        WriteLine(string.Format("#define {0}Wrapper void", c.FullNameCS), WriteTo.Header);
                    }
                    WriteLine("#else", WriteTo.Header);
                    foreach (ClassDefinition c in wrappedClasses)
                    {
                        WriteClassWrapperMethodDeclarations(c);
                        WriteClassWrapperDefinition(c);
                    }
                    WriteLine("#endif", WriteTo.Header);
                    WriteLine(WriteTo.Header);
                }

                // Write classes
                headerWriter.Write("extern \"C\"");
                headerWriter.WriteLine();
                headerWriter.WriteLine("{");
                csWriter.Write("namespace ");
                csWriter.WriteLine(NamespaceName);
                csWriter.WriteLine("{");
                hasCSWhiteSpace = true;
                hasCppClassSeparatingWhitespace = true;

                foreach (EnumDefinition e in header.Enums.OrderBy(x => x.Name))
                {
                    WriteEnumClass(e, 1);
                }

                foreach (ClassDefinition c in header.Classes)
                {
                    WriteClass(c, 1);
                }

                headerWriter.WriteLine('}');
                csWriter.WriteLine('}');

                // Include header
                includeWriter.Write("#include \"");
                includeWriter.Write(headerFilename);
                includeWriter.WriteLine("\"");

                headerWriter.Dispose();
                headerFile.Dispose();
                sourceWriter.Dispose();
                sourceFile.Dispose();
                csWriter.Dispose();
                csFile.Dispose();
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

        private static bool RequiresMathNamespace(HeaderDefinition header)
        {
            return header.Classes.Any(RequiresMathNamespace);
        }

        private static bool RequiresMathNamespace(ClassDefinition cl)
        {
            if (cl.Classes.Any(RequiresMathNamespace))
            {
                return true;
            }

            foreach (var method in cl.Methods)
            {
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
