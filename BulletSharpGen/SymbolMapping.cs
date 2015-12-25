using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BulletSharpGen
{
    // Used to map e.g. C++ class names into C# class names
    interface ISymbolMapping
    {
        string Name { get; }
        string Map(string symbol);
    }

    // Maps names according to the Replacements property
    class ReplaceMapping : ISymbolMapping
    {
        public Dictionary<string, string> Replacements { get; private set; }

        string _name;
        public string Name
        {
            get { return _name; }
        }

        public ReplaceMapping(string name)
        {
            _name = name;

            Replacements = new Dictionary<string, string>();
        }

        public virtual string Map(string symbol)
        {
            string mapping;
            if (Replacements.TryGetValue(symbol, out mapping))
            {
                return mapping;
            }
            return symbol;
        }
    }

    // Maps names according to the Replacements property or using a given C# script
    class ScriptedMapping : ReplaceMapping
    {
        object _mappingObject;
        MethodInfo _method;
        PropertyInfo _headerProperty;
        PropertyInfo _nameProperty;

        public string ScriptBody { get; private set; }

        public HeaderDefinition Header
        {
            get { return _headerProperty.GetValue(_mappingObject) as HeaderDefinition; }
            set { _headerProperty.SetValue(_mappingObject, value); }
        }

        public ScriptedMapping(string name, string scriptBody)
            : base(name)
        {
            ScriptBody = scriptBody;

            // Construct script
            string scriptTemplate =
@"namespace GenMappings {{
public class Mapping {{
    public HeaderDefinition Header {{ get; set; }}
    public string Name {{ get; set; }}
    public string Map() {{
        {0}
    }}
}}
}}";
            var thisAssembly = Assembly.GetCallingAssembly();
            scriptTemplate = string.Format("using {0};\r\n", thisAssembly.GetName().Name) + scriptTemplate;
            string scriptFull = string.Format(scriptTemplate, scriptBody);

            // Compile script
            var provider = CodeDomProvider.CreateProvider("CSharp");
            var cp = new CompilerParameters();
            cp.GenerateInMemory = true;
            cp.ReferencedAssemblies.Add(thisAssembly.Location);
            var results = provider.CompileAssemblyFromSource(cp, scriptFull);

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

        public override string Map(string symbol)
        {
            string mapping;
            if (Replacements.TryGetValue(symbol, out mapping))
            {
                return mapping;
            }
            _nameProperty.SetValue(_mappingObject, symbol);
            return _method.Invoke(_mappingObject, new object[] { }) as string;
        }
    }
}
