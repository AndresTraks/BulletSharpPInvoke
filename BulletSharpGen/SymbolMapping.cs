using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;

namespace BulletSharpGen
{
    interface ISymbolMapping
    {
        string Map(string symbol);
    }

    class ScriptedMapping : ISymbolMapping
    {
        object _mappingObject;
        MethodInfo _method;
        PropertyInfo _headerProperty;
        PropertyInfo _nameProperty;

        public HeaderDefinition Header
        {
            get { return _headerProperty.GetValue(_mappingObject) as HeaderDefinition; }
            set { _headerProperty.SetValue(_mappingObject, value); }
        }

        public ScriptedMapping(string source)
        {
            // Compile script
            string programCode =
            @"using BulletSharpGen;
            namespace GenMappings{{
                public class Mapping {{
                    public HeaderDefinition Header {{ get; set; }}
                    public string Name {{ get; set; }}
                    public string Map() {{
                        {0}
                    }}
                }}
            }}";
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var cp = new CompilerParameters();
            cp.GenerateInMemory = true;
            cp.ReferencedAssemblies.Add(Assembly.GetCallingAssembly().Location);
            var results = provider.CompileAssemblyFromSource(cp, string.Format(programCode, source));

            // Get script members
            var assembly = results.CompiledAssembly;
            var mapClass = assembly.DefinedTypes.Single(t => t.Name.Equals("Mapping"));
            var constructor = mapClass.DeclaredConstructors.First();
            _headerProperty = mapClass.DeclaredProperties.Single(p => p.Name.Equals("Header"));
            _nameProperty = mapClass.DeclaredProperties.Single(p => p.Name.Equals("Name"));
            _method = mapClass.DeclaredMethods.Single(m => m.Name == "Map");

            // Instantiate mapping class
            _mappingObject = constructor.Invoke(new object[] { });
        }

        public string Map(string symbol)
        {
            _nameProperty.SetValue(_mappingObject, symbol);
            return _method.Invoke(_mappingObject, new object[] { }) as string;
        }
    }
}
