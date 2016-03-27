using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    class ExtensionsWriter : WrapperWriter
    {
        Dictionary<string, string> _extensionClassesInternal = new Dictionary<string, string>();
        Dictionary<string, string> _extensionClassesExternal = new Dictionary<string, string>();
        Dictionary<string, string> _returnTypeConversion = new Dictionary<string, string>();

        List<KeyValuePair<string, string>> _extensionMethods = new List<KeyValuePair<string, string>>();

        public ExtensionsWriter(WrapperProject project)
            : base(project)
        {
            _extensionClassesInternal.Add("Matrix3x3", "BulletSharp.Math.Matrix");
            _extensionClassesInternal.Add("Quaternion", "BulletSharp.Math.Quaternion");
            _extensionClassesInternal.Add("Transform", "BulletSharp.Math.Matrix");
            _extensionClassesInternal.Add("Vector3", "BulletSharp.Math.Vector3");

            _extensionClassesExternal.Add("Matrix3x3", "OpenTK.Matrix4");
            _extensionClassesExternal.Add("Quaternion", "OpenTK.Quaternion");
            _extensionClassesExternal.Add("Transform", "OpenTK.Matrix4");
            _extensionClassesExternal.Add("Vector3", "OpenTK.Vector3");

            _returnTypeConversion.Add("Matrix3x3", ".ToOpenTK()");
            _returnTypeConversion.Add("Quaternion", ".ToOpenTK()");
            _returnTypeConversion.Add("Transform", ".ToOpenTK()");
            _returnTypeConversion.Add("Vector3", ".ToOpenTK()");

            OpenFile(null, WriteTo.Buffer);
        }

        bool MethodNeedsExtensions(MethodDefinition method)
        {
            // Extension constructors & static extension methods not supported
            if (method.IsConstructor || method.IsStatic)
            {
                return false;
            }

            foreach (var param in method.Parameters)
            {
                if (_extensionClassesInternal.ContainsKey(param.Type.ManagedName))
                {
                    return true;
                }
            }
            return false;
        }

        bool ClassNeedsExtensions(ClassDefinition c)
        {
            foreach (var prop in c.Properties)
            {
                if (_extensionClassesInternal.ContainsKey(prop.Type.ManagedName))
                {
                    return true;
                }
            }

            return c.Methods.Any(MethodNeedsExtensions);
        }

        public override void Output()
        {
            string outDirectory = Project.NamespaceName + "_extensions";

            foreach (HeaderDefinition header in Project.HeaderDefinitions.Values)
            {
                if (!header.Classes.Any(ClassNeedsExtensions)) continue;

                Directory.CreateDirectory(outDirectory);

                // C# extensions file
                OpenFile(Path.Combine(outDirectory, header.ManagedName + "Extensions.cs"), WriteTo.CS);
                WriteLine("using System.ComponentModel;");
                WriteLine();
                WriteLine($"namespace {Project.NamespaceName}");
                WriteLine('{');
                hasCSWhiteSpace = true;

                foreach (var @class in header.Classes)
                {
                    WriteClass(@class);
                }

                WriteLine("}");

                CloseFile(WriteTo.CS);
            }
        }

        private void WriteClass(ClassDefinition c)
        {
            foreach (var child in c.NestedClasses)
            {
                WriteClass(child);
            }

            if (!ClassNeedsExtensions(c))
            {
                return;
            }

            _extensionMethods.Clear();

            To = WriteTo.CS;
            EnsureWhiteSpace();
            Write(1, "[EditorBrowsable(EditorBrowsableState.Never)]");
            WriteLine(1, $"public static class {c.ManagedName}Extensions");
            WriteLine(1, "{");

            To = WriteTo.Buffer;
            foreach (var prop in c.Properties)
            {
                if (_extensionClassesInternal.ContainsKey(prop.Type.ManagedName))
                {
                    string typeName = _extensionClassesExternal[prop.Type.ManagedName];

                    // Getter with out parameter
                    ClearBuffer();
                    WriteLine(2, string.Format("public unsafe static void Get{0}(this {1} obj, out {2} value)",
                        prop.Name, c.ManagedName, typeName));
                    WriteLine(2, "{");

                    WriteLine(3, $"fixed ({typeName}* valuePtr = &value)");
                    WriteLine(3, "{");
                    Write(4, $"*({_extensionClassesInternal[prop.Type.ManagedName]}");
                    WriteLine(string.Format("*({0}*)valuePtr = obj.{1};",
                        _extensionClassesInternal[prop.Type.ManagedName], prop.Name));
                    WriteLine(3, "}");

                    WriteLine(2, "}");

                    _extensionMethods.Add(new KeyValuePair<string, string>("Get" + prop.Name, GetBufferString()));

                    // Getter with return value
                    ClearBuffer();
                    WriteLine(2, string.Format("public static {0} Get{1}(this {1} obj)",
                        typeName, prop.Name, c.ManagedName));
                    WriteLine(2, "{");

                    WriteLine(3, $"{typeName} value;");
                    WriteLine(3, $"Get{prop.Name}(obj, out value)");
                    WriteLine(3, "return value;");

                    WriteLine(2, "}");

                    _extensionMethods.Add(new KeyValuePair<string, string>("Get" + prop.Name, GetBufferString()));

                    if (prop.Setter == null)
                    {
                        continue;
                    }

                    // Setter with ref parameter
                    ClearBuffer();
                    WriteLine(2, string.Format("public unsafe static void Set{0}(this {1} obj, ref {2} value)",
                        prop.Name, c.ManagedName, typeName));
                    WriteLine(2, "{");

                    WriteLine(3, $"fixed ({typeName}* valuePtr = &value)");
                    WriteLine(3, "{");
                    WriteLine(4, string.Format("obj.{0} = *({1}*)valuePtr;",
                        prop.Name, _extensionClassesInternal[prop.Type.ManagedName]));
                    WriteLine(3, "}");

                    WriteLine(2, "}");

                    _extensionMethods.Add(new KeyValuePair<string, string>("Set" + prop.Name, GetBufferString()));

                    // Setter with non-ref parameter
                    ClearBuffer();
                    WriteLine(2, string.Format("public static void Set{0}(this {1} obj, {2} value)",
                        prop.Name, c.ManagedName, typeName));
                    WriteLine(2, "{");
                    WriteLine(3, $"Set{prop.Name}(obj, ref value);");
                    WriteLine(2, "}");

                    _extensionMethods.Add(new KeyValuePair<string, string>("Set" + prop.Name, GetBufferString()));
                }
            }

            foreach (var method in c.Methods)
            {
                if (!MethodNeedsExtensions(method))
                {
                    continue;
                }

                WriteMethod(method);
            }

            foreach (var method in _extensionMethods.OrderBy(key => key.Key))
            {
                EnsureWhiteSpace(WriteTo.CS);
                Write(method.Value, WriteTo.CS);
                hasCSWhiteSpace = false;
            }

            WriteLine(1, "}", WriteTo.CS);
            hasCSWhiteSpace = false;
        }

        private void WriteMethod(MethodDefinition method, int numOptionalParams = 0)
        {
            bool convertReturnType = _extensionClassesInternal.ContainsKey(method.ReturnType.ManagedName);

            ClearBuffer();
            Write(2, "public unsafe static ");
            if (convertReturnType)
            {
                Write(_extensionClassesExternal[method.ReturnType.ManagedName]);
            }
            else
            {
                Write(method.ReturnType.ManagedName);
            }
            Write($" {method.ManagedName}(this {method.Parent.ManagedName} obj");

            var extendedParams = new List<ParameterDefinition>();
            int numParameters = method.Parameters.Length - numOptionalParams;
            for (int i = 0; i < numParameters; i++)
            {
                Write(", ");

                var param = method.Parameters[i];
                if (_extensionClassesInternal.ContainsKey(param.Type.ManagedName))
                {
                    Write($"ref {_extensionClassesExternal[param.Type.ManagedName]} {param.Name}");
                    extendedParams.Add(param);
                }
                else
                {
                    Write($"{param.Type.ManagedName} {param.Name}");
                }
            }
            WriteLine(')');

            WriteLine(2, "{");

            // Fix parameter pointers
            int tabs = 3;
            foreach (var param in extendedParams)
            {
                WriteLine(tabs, string.Format("fixed ({0}* {1}Ptr = &{2})",
                    _extensionClassesExternal[param.Type.ManagedName], param.Name, param.Name));
                WriteLine(tabs, "{");
                tabs++;
            }

            WriteTabs(tabs);
            if (method.ReturnType.Name != "void")
            {
                Write("return ");
            }
            Write($"obj.{method.ManagedName}(");
            bool hasOptionalParam = false;
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                if (_extensionClassesInternal.ContainsKey(param.Type.ManagedName))
                {
                    Write(string.Format("ref *({0}*){1}Ptr",
                        _extensionClassesInternal[param.Type.ManagedName], param.Name));
                }
                else
                {
                    Write(param.Name);
                }

                if (param.IsOptional)
                {
                    hasOptionalParam = true;
                }

                if (i != numParameters - 1)
                {
                    Write(", ");
                }
            }
            Write(')');
            if (convertReturnType)
            {
                Write(_returnTypeConversion[method.ReturnType.ManagedName]);
            }
            WriteLine(';');

            // Close fixed blocks
            while (tabs != 2)
            {
                tabs--;
                WriteLine(tabs, "}");
            }

            _extensionMethods.Add(new KeyValuePair<string, string>(method.Name, GetBufferString()));

            if (hasOptionalParam)
            {
                WriteMethod(method, numOptionalParams + 1);
            }
        }
    }
}
