using ClangSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    public class PInvokeWriter : DotNetWriter
    {
        public PInvokeWriter(DotNetParser parser)
            : base(parser)
        {
        }

        void WriteDeleteMethodCS(ManagedMethod method, int level)
        {
            WriteLine(level + 1, "public void Dispose()");
            WriteLine(level + 1, "{");
            WriteLine(level + 2, "Dispose(true);");
            WriteLine(level + 2, "GC.SuppressFinalize(this);");
            WriteLine(level + 1, "}");
            WriteLine();

            WriteLine(level + 1, "protected virtual void Dispose(bool disposing)");
            WriteLine(level + 1, "{");
            WriteLine(level + 2, "if (_native != IntPtr.Zero)");
            WriteLine(level + 2, "{");
            if (method.Parent.Native.HasPreventDelete)
            {
                WriteLine(level + 3, "if (!_preventDelete)");
                WriteLine(level + 3, "{");
                WriteLine(level + 4, $"{GetFullNameC(method.Parent.Native)}_delete(_native);");
                WriteLine(level + 3, "}");
            }
            else
            {
                WriteLine(level + 3, $"{GetFullNameC(method.Parent.Native)}_delete(_native);");
            }
            WriteLine(level + 3, "_native = IntPtr.Zero;");
            WriteLine(level + 2, "}");
            WriteLine(level + 1, "}");

            // C# Destructor
            WriteLine();
            WriteLine(level + 1, $"~{method.Parent.Name}()");
            WriteLine(level + 1, "{");
            WriteLine(level + 2, "Dispose(false);");
            WriteLine(level + 1, "}");
        }


        private void WriteMethodDeclaration(ManagedMethod method, int numParameters, int level, int overloadIndex,
            ManagedParameter outValueParameter = null)
        {
            var nativeMethod = method.Native;

            EnsureWhiteSpace(WriteTo.CS);

            WriteTo cs;
            WriteTo dllImport = WriteTo.Buffer;

            if (method.Property != null)
            {
                // Do not write accessor methods of C# properties here
                cs = WriteTo.None;

                // Cached properties that are initialized only once
                // do not need a DllImport for the get method
                if (method.Parent.CachedProperties.ContainsKey(method.Property.Name) &&
                    method.Equals(method.Property.Getter))
                {
                    dllImport = WriteTo.None;
                }
            }
            else if (method.Name.Equals("delete"))
            {
                WriteDeleteMethodCS(method, level);
                cs = WriteTo.None;
            }
            else
            {
                Write(level + 1, "public ", WriteTo.CS);
                cs = WriteTo.CS;
            }

            // DllImport clause
            WriteLine(level + 1,
                "[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]",
                dllImport);
            if (nativeMethod.ReturnType != null && nativeMethod.ReturnType.Kind == TypeKind.Bool)
            {
                WriteLine(level + 1, "[return: MarshalAs(UnmanagedType.I1)]", dllImport);
            }
            Write(level + 1, "static extern ", dllImport);

            // Return type
            if (nativeMethod.IsConstructor)
            {
                Write("IntPtr ", dllImport);
            }
            else
            {
                var returnType = nativeMethod.ReturnType.Canonical;

                if (nativeMethod.IsStatic) Write("static ", cs);
                Write($"{GetTypeNameCS(returnType)} ", cs);

                if (outValueParameter != null)
                {
                    Write("void ", dllImport);
                }
                else
                {
                    if (returnType.IsBasic)
                    {
                        Write(GetTypeNameCS(returnType), dllImport);
                    }
                    else if (returnType.Referenced != null)
                    {
                        if (returnType.Kind == TypeKind.LValueReference &&
                            returnType.Referenced.IsConst && returnType.Referenced.Canonical.IsBasic)
                        {
                            Write(GetTypeNameCS(returnType.Referenced.Canonical), dllImport);
                        }
                        else
                        {
                            Write("IntPtr", dllImport);
                        }
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
            string methodName = nativeMethod.IsConstructor ? "new" : method.Native.Name;
            if (overloadIndex != 0)
            {
                methodName += (overloadIndex + 1).ToString();
            }
            Write($"{GetFullNameC(method.Parent.Native)}_{methodName}(", dllImport);
            Write($"{method.Name}(", cs);


            // Parameters
            var parameters = method.Parameters.Take(numParameters);
            var parametersCs = parameters.Select(p => $"{GetParameterTypeNameCS(p.Native)} {p.Name}");
            WriteLine($"{ListToLines(parametersCs, WriteTo.CS, level + 1)})", cs);

            if (outValueParameter != null) parameters = parameters.Concat(new[] { outValueParameter });

            var parametersDllImport = parameters.Select(p => $"{GetParameterTypeNameDllImport(p.Native)} {p.Native.Name}");

            // The first parameter is the instance pointer (if not constructor or static method)
            if (!nativeMethod.IsConstructor && !nativeMethod.IsStatic)
            {
                parametersDllImport =
                    new[] { "IntPtr obj" }.Concat(parametersDllImport);
            }

            WriteLine($"{string.Join(", ", parametersDllImport)});", dllImport);
        }

        void WriteMethodDefinition(ManagedMethod method, int numParameters, int overloadIndex, int level,
            ManagedParameter outValueParameter)
        {
            var nativeMethod = method.Native;
            if (nativeMethod.IsConstructor)
            {
                if (method.Parent.BaseClass == null)
                {
                    Write("_native = ");
                }
                Write($"{GetFullNameC(method.Parent.Native)}_new");
            }
            else
            {
                if (!nativeMethod.IsVoid)
                {
                    if (outValueParameter != null)
                    {
                        // Temporary variable
                        WriteLine(string.Format("{0} {1};",
                            DotNetParser.GetManaged(outValueParameter.Native.Type.Referenced.Target).Name,
                            outValueParameter.Name));
                        WriteTabs(level + 2);
                    }
                    else
                    {
                        Write($"return {BulletParser.GetTypeMarshalConstructorStartCS(nativeMethod)}");
                    }
                }

                Write($"{GetFullNameC(method.Parent.Native)}_{method.Native.Name}");
            }

            if (overloadIndex != 0)
            {
                Write((overloadIndex + 1).ToString());
            }

            Write('(');

            var parameters = method.Parameters.Take(numParameters)
                .Select(p => GetParameterMarshal(p));
            if (outValueParameter != null)
            {
                parameters = parameters.Concat(new[] { $"out {outValueParameter.Name }" });
            }

            // The first parameter is the instance pointer (if not constructor or static method)
            if (!nativeMethod.IsConstructor && !nativeMethod.IsStatic)
            {
                parameters = new[] { "_native" }.Concat(parameters);
            }

            Write(ListToLines(parameters, WriteTo.CS, level + 2));

            if (nativeMethod.IsConstructor && method.Parent.BaseClass != null)
            {
                Write(")");
                if (method.Parent.BaseClass.Native.HasPreventDelete)
                {
                    Write(", false");
                }
                WriteLine(")");
                WriteLine(level + 1, "{");
            }
            else
            {
                if (!nativeMethod.IsConstructor && !nativeMethod.IsVoid)
                {
                    Write(BulletParser.GetTypeMarshalConstructorEndCS(nativeMethod));
                }
                WriteLine(");");
            }

            // Cache property values
            if (nativeMethod.IsConstructor)
            {
                var methodParent = method.Parent;
                while (methodParent != null)
                {
                    foreach (var cachedProperty in methodParent.CachedProperties.OrderBy(p => p.Key))
                    {
                        foreach (var param in method.Parameters)
                        {
                            if (param.Name.ToLower().Equals(cachedProperty.Key.ToLower())
                                && GetName(param.Native.Type).Equals(GetName(cachedProperty.Value.Property.Type)))
                            {
                                WriteLine(level + 2,
                                    $"{cachedProperty.Value.CacheFieldName} = {param.Name};");
                            }
                        }
                    }
                    methodParent = methodParent.BaseClass;
                }
            }

            // Return temporary variable
            if (outValueParameter != null)
            {
                WriteLine(level + 2, $"return {outValueParameter.Name};");
            }
        }

        void WriteMethod(ManagedMethod method, int level, ref int overloadIndex, int numOptionalParams = 0)
        {
            var nativeMethod = method.Native;

            int numOptionalParamsTotal = nativeMethod.NumOptionalParameters;
            int numParameters = method.Parameters.Length - numOptionalParamsTotal + numOptionalParams;

            // TODO: outValueParameter
            WriteMethodDeclaration(method, numParameters, level, overloadIndex, null);

            // Skip methods wrapped by C# properties
            if (method.Property == null)
            {
                // Constructor base call
                if (nativeMethod.IsConstructor && method.Parent.BaseClass != null)
                {
                    Write(level + 2, ": base(", WriteTo.CS);
                }
                else
                {
                    WriteLine(level + 1, "{", WriteTo.CS);
                    WriteTabs(level + 2, WriteTo.CS);
                }

                // TODO: outValueParameter
                WriteMethodDefinition(method, numParameters, overloadIndex, level, null);
                WriteLine(level + 1, "}", WriteTo.CS);
                hasCSWhiteSpace = false;
            }

            // If there was an optional parameter,
            // write the method again without it.
            overloadIndex++;
            if (numOptionalParams < numOptionalParamsTotal)
            {
                WriteMethod(method, level, ref overloadIndex, numOptionalParams + 1);
            }
        }

        void WriteProperty(ManagedProperty prop, int level)
        {
            var getterNative = prop.Getter.Native;
            var setterNative = prop.Setter?.Native;

            EnsureWhiteSpace();

            Write(level + 1, $"public ");
            if (getterNative.IsStatic) Write("static ");
            WriteLine($"{GetTypeNameCS(prop.Type)} {prop.Name}");
            WriteLine(level + 1, "{");

            if (prop.Parent.CachedProperties.ContainsKey(prop.Name))
            {
                var cachedProperty = prop.Parent.CachedProperties[prop.Name];
                WriteLine(level + 2, $"get {{ return {cachedProperty.CacheFieldName}; }}");

                if (setterNative != null)
                {
                    WriteLine(level + 2, "set");
                    WriteLine(level + 2, "{");
                    Write(level + 3, $"{GetFullNameC(prop.Parent.Native)}_{setterNative.Name}(");
                    if (!setterNative.IsStatic) Write("_native, ");
                    WriteLine($"{GetTypeSetterCSMarshal(prop.Type)});");
                    WriteLine(level + 3, $"{cachedProperty.CacheFieldName} = value;");
                    WriteLine(level + 2, "}");
                }
            }
            else
            {
                var type = prop.Type;
                if ((type.Target != null && type.Target.MarshalAsStruct) ||
                    (type.Kind == TypeKind.LValueReference && type.Referenced.Target != null && type.Referenced.Target.MarshalAsStruct))
                {
                    WriteLine(level + 2, "get");
                    WriteLine(level + 2, "{");
                    WriteLine(level + 3, $"{GetTypeNameCS(type)} value;");
                    Write(level + 3, $"{GetFullNameC(prop.Parent.Native)}_{getterNative.Name}(");
                    if (!getterNative.IsStatic) Write("_native, ");
                    WriteLine("out value);");
                    WriteLine(level + 3, "return value;");
                    WriteLine(level + 2, "}");
                }
                else
                {
                    string objPtr = getterNative.IsStatic ? "" : "_native";
                    WriteLine(level + 2, string.Format("get {{ return {0}{1}_{2}({3}){4}; }}",
                        BulletParser.GetTypeMarshalConstructorStartCS(getterNative),
                        GetFullNameC(prop.Parent.Native), getterNative.Name,
                        objPtr,
                        BulletParser.GetTypeMarshalConstructorEndCS(getterNative)));
                }

                if (setterNative != null)
                {
                    string marshal = GetTypeSetterCSMarshal(prop.Type);
                    Write(level + 2,
                        $"set {{ {GetFullNameC(prop.Parent.Native)}_{setterNative.Name}(");
                    if (!setterNative.IsStatic) Write("_native, ");
                    WriteLine($"{marshal}); }}");
                }
            }

            WriteLine(level + 1, "}");

            hasCSWhiteSpace = false;
        }

        private static string GetTypeSetterCSMarshal(TypeRefDefinition type)
        {
            type = type.Canonical;

            if ((type.Target != null && type.Target.MarshalAsStruct) ||
                (type.Kind == TypeKind.LValueReference && type.Referenced.Target != null && type.Referenced.Target.MarshalAsStruct))
            {
                return "ref value";
            }
            if (!type.IsBasic)
            {
                if (type.Kind == TypeKind.Pointer && type.Referenced.Kind == TypeKind.Void)
                {
                    return "value";
                }
                return "value._native";
            }

            return "value";
        }

        IEnumerable<ManagedClass> GetEnums(IEnumerable<ManagedClass> classes)
        {
            foreach (var @class in classes)
            {
                if (@class.Native is EnumDefinition)
                {
                    yield return @class;
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
        void WriteEnumClass(ManagedClass @class, int level)
        {
            var @enum = @class.Native as EnumDefinition;
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

            WriteLine(level, $"public enum {@class.Name}");
            WriteLine(level, "{");
            for (int i = 0; i < @enum.EnumConstants.Count; i++)
            {
                var constant = @enum.EnumConstants[i];
                if (constant.Value.Equals(""))
                {
                    Write(level + 1, constant.Constant);
                }
                else
                {
                    Write(level + 1, $"{constant.Constant} = {constant.Value}");
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

        void WriteClass(ManagedClass @class, int level)
        {
            var nativeClass = @class.Native;

            // Write class definition
            EnsureWhiteSpace();
            Write(level, "public ");
            if (nativeClass.IsStatic) Write("static ");
            if (@class.Parent != null && @class.Parent.BaseClass != null)
            {
                if (@class.Parent.BaseClass.NestedClasses
                    .Any(c => c != @class && c.Name.Equals(@class.Name)))
                {
                    Write("new ");
                }
            }
            if (nativeClass.IsAbstract) Write("abstract ");
            Write($"class {@class.Name}");
            if (@class.BaseClass != null)
            {
                WriteLine($" : {GetFullNameManaged(@class.BaseClass)}");
            }
            else if (nativeClass.IsStatic)
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
            if (@class.BaseClass == null && !nativeClass.IsStatic)
            {
                EnsureWhiteSpace();
                WriteLine(level + 1, "internal IntPtr _native;");
                if (nativeClass.HasPreventDelete)
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
                        GetTypeNameCS(cachedProperty.Value.Property.Type),
                        fieldName));
                }
                hasCSWhiteSpace = false;
            }

            // Write methods
            ClearBuffer();

            // Write constructors
            if (!nativeClass.IsStatic)
            {
                // Write C# internal constructor
                if (!nativeClass.NoInternalConstructor)
                {
                    EnsureWhiteSpace();
                    Write(level + 1, $"internal {@class.Name}(IntPtr native");
                    if (nativeClass.HasPreventDelete)
                    {
                        Write(", bool preventDelete");
                    }
                    WriteLine(')');
                    if (@class.BaseClass != null)
                    {
                        Write(level + 2, ": base(native");
                        if (nativeClass.HasPreventDelete)
                        {
                            if (!@class.BaseClass.Native.HasPreventDelete)
                            {
                                // Base class should also have preventDelete
                                //throw new NotImplementedException();
                            }
                            Write(", preventDelete");
                        }
                        else if (@class.BaseClass.Native.HasPreventDelete)
                        {
                            Write(", true");
                        }
                        WriteLine(')');
                    }
                    WriteLine(level + 1, "{");
                    if (@class.BaseClass == null)
                    {
                        WriteLine(level + 2, "_native = native;");
                        if (nativeClass.HasPreventDelete)
                        {
                            WriteLine(level + 2, "_preventDelete = preventDelete;");
                        }
                    }
                    WriteLine(level + 1, "}");
                    hasCSWhiteSpace = false;
                }

                // Write public constructors
                if (!nativeClass.HidePublicConstructors && !nativeClass.IsAbstract)
                {
                    int overloadIndex = 0;
                    var constructors = @class.Methods.Where(m => m.Native.IsConstructor && !m.Native.IsExcluded);
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
            var methods = @class.Methods.Where(m => !m.Native.IsConstructor && !m.Native.IsExcluded).OrderBy(m => m.Name);
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
            // TODO: skip delete methods in classes that can't be constructed.
            /*
            if (@class.BaseClass == null && !nativeClass.IsStatic)
            {
                int overloadIndex = 0;
                var del = new MethodDefinition("delete", @class, 0);
                del.ReturnType = new TypeRefDefinition("void");
                WriteMethod(del, level, ref overloadIndex);
                @class.Methods.Remove(del);
            }
            */
            // Write DllImport clauses
            if (GetBufferString().Length != 0)
            {
                EnsureWhiteSpace(WriteTo.CS);
                Write(GetBufferString(), WriteTo.CS);
            }

            WriteLine(level, "}", WriteTo.CS);
            hasCSWhiteSpace = false;
        }

        void WriteHeader(ManagedHeader header, string sourceRootFolder)
        {
            // Entirely skip headers that have no classes
            // TODO: parse C++ methods outside of classes
            if (header.Native.AllClasses.All(@class => @class.IsExcluded)) return;

            // C# source file
            string csPath = header.Name + ".cs";
            string csFullPath = Path.Combine(Project.CsProjectPathFull, csPath);
            OpenFile(csFullPath, WriteTo.CS);
            WriteLine("using System;");
            if (RequiresInterop(header.Native))
            {
                WriteLine("using System.Runtime.InteropServices;");
                WriteLine("using System.Security;");
            }
            if (RequiresMathNamespace(header.Native))
            {
                WriteLine("using BulletSharp.Math;");
            }
            WriteLine();


            WriteLine($"namespace {Project.NamespaceName}");
            WriteLine("{");
            hasCSWhiteSpace = true;

            var enums = GetEnums(header.Classes)
                .OrderBy(e => e.Name).ToList();
            foreach (var @enum in enums)
            {
                WriteEnumClass(@enum, 1);
            }

            foreach (var @class in header.Classes
                .Where(c => !IsExcludedClass(c)))
            {
                WriteClass(@class, 1);
            }

            WriteLine('}', WriteTo.CS);

            CloseFile(WriteTo.CS);
        }

        public override void Output()
        {
            Directory.CreateDirectory(Project.CsProjectPathFull);

            OpenFile(null, WriteTo.Buffer);

            var sourceRootFolders = Project.SourceRootFoldersFull.Select(s => s.Replace('\\', '/'));
            var headers = DotNetParser.Headers.Values.Where(h => !h.Native.IsExcluded && !h.Classes.All(c => c.Native.IsExcluded));
            var headersByRoot = headers.GroupBy(h => sourceRootFolders.First(s => h.Native.Filename.StartsWith(s)));
            foreach (var headerGroup in headersByRoot)
            {
                string sourceRootFolder = headerGroup.Key;
                foreach (var header in headerGroup.OrderBy(h => h.Name))
                {
                    WriteHeader(header, sourceRootFolder);
                }
            }

            Console.WriteLine("Platform Invoke wrapper completed");
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

                if (DefaultParser.TypeRequiresMarshal(method.ReturnType)) return true;

                if (method.Parameters.Any(param => DefaultParser.TypeRequiresMarshal(param.Type)))
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetFullNameManaged(ManagedClass @class)
        {
            if (@class.Parent != null)
            {
                return $"{GetFullNameManaged(@class.Parent)}.{@class.Name}";
            }
            return @class.Name;
        }

        private string GetTypeNameCS(TypeRefDefinition type)
        {
            if (type.Kind == TypeKind.Typedef)
            {
                if (type.Referenced.Kind == TypeKind.Pointer &&
                    type.Referenced.Referenced.Kind == TypeKind.FunctionProto)
                {
                    return GetName(type);
                }
                return GetTypeNameCS(type.Referenced);
            }

            if (type.Kind == TypeKind.LValueReference)
            {
                return GetTypeNameCS(type.Referenced);
            }

            if (type.IsBasic)
            {
                switch (type.Kind)
                {
                    case TypeKind.UShort:
                        return "ushort";
                    case TypeKind.UInt:
                        return "uint";
                    case TypeKind.ULong:
                        return "ulong";
                }
                return GetName(type);
            }

            if (type.Kind == TypeKind.Pointer && type.Referenced.Kind == TypeKind.Void)
            {
                return "IntPtr";
            }

            if (type.Kind == TypeKind.ConstantArray)
            {
                switch (type.Referenced.Name)
                {
                    case "bool":
                        return "BoolArray";
                    case "int":
                        return "IntArray";
                    case "unsigned int":
                        return "UIntArray";
                    case "unsigned short":
                        return "UShortArray";
                    case "btScalar":
                        return "FloatArray";
                    case "btDbvt":
                        return "DbvtArray";
                    case "btSoftBody::Body":
                        return "BodyArray";
                }

                if (type.Referenced.Referenced != null)
                {
                    switch (type.Referenced.Referenced.Name)
                    {
                        case "btDbvtNode":
                            return "DbvtNodePtrArray";
                        case "btDbvtProxy":
                            return "DbvtProxyPtrArray";
                        case "btSoftBody::Node":
                            return "NodePtrArray";
                    }
                }

                return GetTypeNameCS(type.Referenced) + "[]";
            }

            return GetName(type);
        }

        private static string GetParameterMarshal(ManagedParameter param)
        {
            var type = param.Native.Type.Canonical;

            if ((type.Target != null && type.Target.MarshalAsStruct) ||
                (type.Kind == TypeKind.LValueReference && type.Referenced.Target != null && type.Referenced.Target.MarshalAsStruct))
            {
                if (param.Native.MarshalDirection == MarshalDirection.Out)
                {
                    return $"out {param.Name}";
                }
                return $"ref {param.Name}";
            }

            if (type.Kind == TypeKind.LValueReference && type.Referenced.Canonical.IsBasic)
            {
                switch (param.Native.MarshalDirection)
                {
                    case MarshalDirection.Out:
                        return $"out {param.Name}";
                    case MarshalDirection.InOut:
                        return $"ret {param.Name}";
                    default:
                        return param.Name;
                }
            }

            if (type.Referenced != null)
            {
                switch (type.Name)
                {
                    case "btIDebugDraw":
                        return "DebugDraw.GetUnmanaged(" + param.Name + ')';
                }

                if (!(type.Kind == TypeKind.Pointer && type.Referenced.Kind == TypeKind.Void))
                {
                    return param.Name + "._native";
                }
            }

            return param.Name;
        }

        private string GetParameterTypeNameCS(ParameterDefinition param)
        {
            var type = param.Type;
            string typeName = GetTypeNameCS(type);

            switch (param.MarshalDirection)
            {
                case MarshalDirection.Out:
                    return $"out {typeName}";
                case MarshalDirection.InOut:
                    type = type.Canonical;
                    if (type.Kind == TypeKind.LValueReference)
                    {
                        var referencedType = type.Referenced.Canonical;
                        if (referencedType.IsBasic ||
                            (referencedType.Target != null && referencedType.Target.MarshalAsStruct))
                        {
                            return "ref " + typeName;
                        }
                    }
                    break;
            }

            return typeName;
        }

        private string GetParameterTypeNameDllImport(ParameterDefinition param)
        {
            var type = param.Type.Canonical;
            switch (type.Kind)
            {
                case TypeKind.Pointer:
                case TypeKind.LValueReference:
                    if (type.Referenced.Target != null && type.Referenced.Target.MarshalAsStruct)
                    {
                        var typeName = GetTypeNameCS(type);

                        // https://msdn.microsoft.com/en-us/library/77e6taeh%28v=vs.100%29.aspx
                        // Default IDL attribute values:
                        // ref - [In, Out]
                        // out - [Out]
                        switch (param.MarshalDirection)
                        {
                            case MarshalDirection.In:
                                return $"[In] ref {typeName}";
                            case MarshalDirection.InOut:
                                return $"ref {typeName}";
                            case MarshalDirection.Out:
                                return $"out {typeName}";
                        }
                    }
                    break;
            }

            if (type.Kind == TypeKind.LValueReference && type.Referenced.Canonical.IsBasic)
            {
                switch (param.MarshalDirection)
                {
                    case MarshalDirection.Out:
                        return $"out {GetTypeNameCS(type.Referenced.Canonical)}";
                    case MarshalDirection.InOut:
                        return $"ref {GetTypeNameCS(type.Referenced.Canonical)}";
                    case MarshalDirection.In:
                        return GetTypeNameCS(type.Referenced.Canonical);
                }
            }

            if (type.Referenced != null)
            {
                return "IntPtr";
            }

            return GetTypeNameCS(type);
        }
    }
}
