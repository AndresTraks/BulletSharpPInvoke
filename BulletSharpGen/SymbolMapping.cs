using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return null;
        }
    }

    public class ScriptGlobals
    {
        public string Name { get; set; }
        public HeaderDefinition Header { get; set; }
    }

    // Maps names according to the Replacements property or using a given C# script
    class ScriptedMapping : ReplaceMapping
    {
        Script<string> _script;

        public string ScriptBody { get { return _script.Code; } }
        public ScriptGlobals Globals { get; private set; }

        public ScriptedMapping(string name, string scriptBody)
            : base(name)
        {
            _script = CSharpScript.Create<string>(scriptBody, globalsType: typeof(ScriptGlobals));
            var diagnostic = _script.Compile();
            if (diagnostic.Any())
            {
                foreach (var message in diagnostic)
                {
                    Console.WriteLine(message);
                }
            }
            Globals = new ScriptGlobals();
        }

        public override string Map(string symbol)
        {
            string mapping = base.Map(symbol);
            if (mapping != null) return mapping;

            Globals.Name = symbol;
            return _script.RunAsync(Globals).Result.ReturnValue;
        }
    }
}
