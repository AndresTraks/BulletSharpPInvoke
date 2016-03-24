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
            WriteTabs(1);
            Write("[EditorBrowsable(EditorBrowsableState.Never)]");
            WriteTabs(1);
            WriteLine($"public static class {c.ManagedName}Extensions");
            WriteTabs(1);
            WriteLine('{');

            To = WriteTo.Buffer;
            foreach (var prop in c.Properties)
            {
                if (_extensionClassesInternal.ContainsKey(prop.Type.ManagedName))
                {
                    // Getter with out parameter
                    ClearBuffer();
                    WriteTabs(2);
                    WriteLine(string.Format("public unsafe static void Get{0}(this {1} obj, out {2} value)",
                        prop.Name, c.ManagedName, _extensionClassesExternal[prop.Type.ManagedName]));
                    WriteTabs(2);
                    WriteLine('{');

                    WriteTabs(3);
                    WriteLine(string.Format("fixed ({0}* valuePtr = &value)",
                        _extensionClassesExternal[prop.Type.ManagedName]));
                    WriteTabs(3);
                    WriteLine('{');
                    WriteTabs(4);
                    Write("*(");
                    Write(_extensionClassesInternal[prop.Type.ManagedName]);
                    WriteLine(string.Format("*({0}*)valuePtr = obj.{1};",
                        _extensionClassesInternal[prop.Type.ManagedName], prop.Name));
                    WriteTabs(3);
                    WriteLine('}');

                    WriteTabs(2);
                    WriteLine('}');

                    _extensionMethods.Add(new KeyValuePair<string, string>("Get" + prop.Name, GetBufferString()));

                    // Getter with return value
                    ClearBuffer();
                    WriteTabs(2);
                    WriteLine(string.Format("public static {0} Get{1}(this {1} obj)",
                        _extensionClassesExternal[prop.Type.ManagedName], prop.Name, c.ManagedName));
                    WriteTabs(2);
                    WriteLine('{');

                    WriteTabs(3);
                    WriteLine(string.Format("{0} value;", _extensionClassesExternal[prop.Type.ManagedName]));
                    WriteTabs(3);
                    WriteLine(string.Format("Get{0}(obj, out value)", prop.Name));
                    WriteTabs(3);
                    WriteLine("return value;");

                    WriteTabs(2);
                    WriteLine('}');

                    _extensionMethods.Add(new KeyValuePair<string, string>("Get" + prop.Name, GetBufferString()));

                    if (prop.Setter == null)
                    {
                        continue;
                    }

                    // Setter with ref parameter
                    ClearBuffer();
                    WriteTabs(2);
                    WriteLine(string.Format("public unsafe static void Set{0}(this {1} obj, ref {2} value)",
                        prop.Name, c.ManagedName, _extensionClassesExternal[prop.Type.ManagedName]));
                    WriteTabs(2);
                    WriteLine('{');

                    WriteTabs(3);
                    WriteLine(string.Format("fixed ({0}* valuePtr = &value)",
                        _extensionClassesExternal[prop.Type.ManagedName]));
                    WriteTabs(3);
                    WriteLine('{');
                    WriteTabs(4);
                    WriteLine(string.Format("obj.{0} = *({1}*)valuePtr;",
                        prop.Name, _extensionClassesInternal[prop.Type.ManagedName]));
                    WriteTabs(3);
                    WriteLine('}');

                    WriteTabs(2);
                    WriteLine('}');

                    _extensionMethods.Add(new KeyValuePair<string, string>("Set" + prop.Name, GetBufferString()));

                    // Setter with non-ref parameter
                    ClearBuffer();
                    WriteTabs(2);
                    WriteLine(string.Format("public static void Set{0}(this {1} obj, {2} value)",
                        prop.Name, c.ManagedName, _extensionClassesExternal[prop.Type.ManagedName]));
                    WriteTabs(2);
                    WriteLine('{');

                    WriteTabs(3);
                    WriteLine($"Set{prop.Name}(obj, ref value);");

                    WriteTabs(2);
                    WriteLine('}');

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

            WriteTabs(1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);
            hasCSWhiteSpace = false;
        }

        private void WriteMethod(MethodDefinition method, int numOptionalParams = 0)
        {
            bool convertReturnType = _extensionClassesInternal.ContainsKey(method.ReturnType.ManagedName);

            ClearBuffer();
            WriteTabs(2);
            Write("public unsafe static ");
            if (convertReturnType)
            {
                Write(_extensionClassesExternal[method.ReturnType.ManagedName]);
            }
            else
            {
                Write(method.ReturnType.ManagedName);
            }
            Write(string.Format(" {0}(this {1} obj",
                method.ManagedName, method.Parent.ManagedName));

            var extendedParams = new List<ParameterDefinition>();
            int numParameters = method.Parameters.Length - numOptionalParams;
            for (int i = 0; i < numParameters; i++)
            {
                Write(", ");

                var param = method.Parameters[i];
                if (_extensionClassesInternal.ContainsKey(param.Type.ManagedName))
                {
                    Write(string.Format("ref {0} {1}",
                        _extensionClassesExternal[param.Type.ManagedName], param.Name));
                    extendedParams.Add(param);
                }
                else
                {
                    Write($"{param.Type.ManagedName} {param.Name}");
                }
            }
            WriteLine(')');

            WriteTabs(2);
            WriteLine('{');

            // Fix parameter pointers
            int tabs = 3;
            foreach (var param in extendedParams)
            {
                WriteTabs(tabs);
                WriteLine(string.Format("fixed ({0}* {1}Ptr = &{2})",
                    _extensionClassesExternal[param.Type.ManagedName], param.Name, param.Name));
                WriteTabs(tabs);
                WriteLine('{');
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
                WriteTabs(tabs);
                WriteLine('}');
            }

            _extensionMethods.Add(new KeyValuePair<string, string>(method.Name, GetBufferString()));

            if (hasOptionalParam)
            {
                WriteMethod(method, numOptionalParams + 1);
            }
        }
    }
}
