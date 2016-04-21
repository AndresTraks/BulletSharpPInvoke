using ClangSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    class DefaultNameMapping : ISymbolMapping
    {
        public string Name => "NameMapping";

        public virtual string Map(string symbol)
        {
            return symbol;
        }
    }

    public class DefaultParser
    {
        public WrapperProject Project { get; }

        public DefaultParser(WrapperProject project)
        {
            Project = project;
            if (project.ClassNameMapping == null) project.ClassNameMapping = new DefaultNameMapping();
            if (project.HeaderNameMapping == null) project.HeaderNameMapping = new DefaultNameMapping();
        }

        public virtual void Parse()
        {
            ResolveReferences();
            MapSymbols();
            ParseEnums();
            SetClassProperties();
            RemoveRedundantMethods();
            FlattenClassHierarchy();
            ResolveTemplateSpecializations();
            CreateDefaultConstructors();
            CreateFieldAccessors();
            CreateProperties();
            ResolveIncludes();
        }

        // n = 2 -> "\t\t"
        protected static string GetTabs(int n)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                builder.Append('\t');
            }
            return builder.ToString();
        }

        // one_two_three -> oneTwoThree
        // one_twoThree -> oneTwoThree
        // ONE_TWO_THREE -> oneTwoThree
        // one_two_THREE -> oneTwoThree
        protected static string ToCamelCase(string text, bool upper)
        {
            if (text.Length == 0)
            {
                return text;
            }

            StringBuilder outText = new StringBuilder();
            int left = 0;

            while (left < text.Length)
            {
                int right = text.IndexOf('_', left);
                if (right == -1)
                {
                    right = text.Length;
                }
                else if (right == left)
                {
                    left++;
                    continue;
                }

                char first = text[left];
                if (outText.Length == 0)
                {
                    first = upper ? char.ToUpper(first) : char.ToLower(first);
                }
                else
                {
                    first = char.ToUpper(first);
                }
                outText.Append(first);
                left++;

                string rest = text.Substring(left, right - left);
                if (rest.All(c => char.IsDigit(c) || char.IsUpper(c)))
                {
                    // Two-letter acronyms are preserved as capitalized
                    // https://msdn.microsoft.com/en-us/library/ms229043%28v=vs.110%29.aspx
                    if (rest.Length > 1 ||
                        (first + rest).Equals("NO") ||
                        (first + rest).Equals("OF") ||
                        (first + rest).Equals("IS"))
                    {
                        rest = rest.ToLower();
                    }
                }
                outText.Append(rest);

                left = right + 1;
            }

            return outText.ToString();
        }

        protected virtual bool IsExcludedClass(ClassDefinition cl)
        {
            return false;
        }

        // Does the type require additional lines of code to marshal?
        public static bool TypeRequiresMarshal(TypeRefDefinition type)
        {
            if (type.Target != null && type.Target.MarshalAsStruct) return true;
            if (type.Kind == TypeKind.LValueReference && TypeRequiresMarshal(type.Referenced)) return true;
            return false;
        }

        void SetClassProperties()
        {
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                @class.IsAbstract = @class.AbstractMethods.Any();
                if (IsExcludedClass(@class))
                {
                    @class.IsExcluded = true;
                }
            }
        }

        void ParseEnums()
        {
            // Remove any common prefix and check for flags
            foreach (var @enum in Project.ClassDefinitions.Values.
                Where(c => c is EnumDefinition).Cast<EnumDefinition>())
            {
                int prefixLength = @enum.GetCommonPrefix().Length;
                @enum.GetCommonSuffix();
                foreach (var constant in @enum.EnumConstants)
                {
                    string newConstant = constant.Constant.Substring(prefixLength);
                    newConstant = ToCamelCase(newConstant, true);

                    // Replace any values referring to this constant
                    foreach (var value in @enum.EnumConstants)
                    {
                        if (value.Value.Equals(constant.Constant))
                        {
                            value.Value = newConstant;
                        }
                    }

                    constant.Constant = newConstant;
                }

                if (@enum.Name.EndsWith("Flags"))
                {
                    @enum.IsFlags = true;
                }
                else
                {
                    // If all values are powers of 2, then it is considered a Flags enum.
                    @enum.IsFlags = @enum.EnumConstants.All(constant =>
                    {
                        int x;
                        if (int.TryParse(constant.Value, out x))
                        {
                            return (x != 0) && ((x & (~x + 1)) == x);
                        }
                        return false;
                    });
                }

                // If none of the values are 0, insert a None constant
                if (@enum.IsFlags)
                {
                    if (!@enum.EnumConstants.Any(c => c.Value.Equals("0")))
                    {
                        @enum.EnumConstants.Insert(0, new EnumConstant("None", "0"));
                    }
                }
            }
        }

        void ResolveReferences()
        {
            // Resolve references (match TypeRefDefinitions to ClassDefinitions)
            // List might be modified with template specialization classes, so make a copy
            var classDefinitionsList = new List<ClassDefinition>(Project.ClassDefinitions.Values);
            foreach (var @class in classDefinitionsList)
            {
                // Resolve method return type and parameter types
                foreach (var method in @class.Methods)
                {
                    ResolveTypeRef(method.ReturnType);
                    Array.ForEach(method.Parameters, p => ResolveTypeRef(p.Type));
                }

                // Resolve field types
                @class.Fields.ForEach(f => ResolveTypeRef(f.Type));
            }
        }

        void ResolveTypeRef(TypeRefDefinition typeRef)
        {
            switch (typeRef.Kind)
            {
                case TypeKind.Enum:
                case TypeKind.Record:
                case TypeKind.Unexposed:
                    if (Project.ClassDefinitions.ContainsKey(typeRef.Name))
                    {
                        typeRef.Target = Project.ClassDefinitions[typeRef.Name];
                        return;
                    }

                    Console.WriteLine($"Unresolved reference: {typeRef.Name}");

                    if (typeRef.TemplateParams != null)
                    {
                        typeRef.TemplateParams.ForEach(ResolveTypeRef);

                        // Create template specialization class
                        string templateName = typeRef.Name.Substring(0, typeRef.Name.IndexOf('<'));

                        ClassDefinition template;
                        if (Project.ClassDefinitions.TryGetValue(templateName, out template))
                        {
                            if (!template.IsExcluded)
                            {
                                //string name = $"{typeRef.Name}<{typeRef.TemplateParams.First().Name}>";
                                string name = typeRef.Name;

                                var classTemplate = template as ClassTemplateDefinition;
                                var header = classTemplate.Header;
                                var specializedClass = new ClassDefinition(name, header);
                                specializedClass.BaseClass = template;
                                header.Classes.Add(specializedClass);
                                Project.ClassDefinitions.Add(name, specializedClass);
                            }
                        }
                    }
                    break;
                case TypeKind.Typedef:
                    var underlying = typeRef.Canonical;
                    if (underlying.Kind == TypeKind.Pointer &&
                        underlying.Referenced.Kind == TypeKind.FunctionProto)
                    {
                        typeRef.Target = Project.ClassDefinitions[typeRef.Name];
                        return;
                    }
                    break;
            }

            if (typeRef.Referenced != null)
            {
                ResolveTypeRef(typeRef.Referenced);
            }
        }

        // Remove overridden methods, methods that differ by const/non-const return values
        // and abstract class constructors
        void RemoveRedundantMethods()
        {
            // Remove by index, not by reference, otherwise the wrong method could be removed.
            // MethodDefinition.Equals compares methods from the POV of C#, not C++,
            // so const/non-const methods will be equal.
            var removedMethodsIndices = new SortedSet<int>();

            foreach (var @class in Project.ClassDefinitions.Values)
            {
                for (int i = 0; i < @class.Methods.Count; i++)
                {
                    var method = @class.Methods[i];

                    if (method.IsConstructor)
                    {
                        if (@class.IsAbstract) removedMethodsIndices.Add(i);
                        continue;
                    }

                    // Check if the method already exists in a base class
                    var baseClass = @class.BaseClass;
                    while (baseClass != null)
                    {
                        var baseMethod = baseClass.Methods.FirstOrDefault(m => m.Equals(method));
                        if (baseMethod != null)
                        {
                            if (baseMethod.IsExcluded)
                            {
                                method.IsExcluded = true;
                            }
                            else
                            {
                                removedMethodsIndices.Add(i);
                            }
                            break;
                        }
                        baseClass = baseClass.BaseClass;
                    }

                    for (int j = i + 1; j < @class.Methods.Count; j++)
                    {
                        var method2 = @class.Methods[j];
                        if (!method.Equals(method2)) continue;

                        var type1 = method.ReturnType;
                        var type2 = method2.ReturnType;
                        bool const1 = type1.IsConst || (type1.Referenced != null && type1.Referenced.IsConst);
                        bool const2 = type2.IsConst || (type2.Referenced != null && type2.Referenced.IsConst);

                        // Prefer non-const return value
                        if (const1)
                        {
                            if (!const2)
                            {
                                removedMethodsIndices.Add(i);
                                break;
                            }
                        }
                        else if (const2)
                        {
                            removedMethodsIndices.Add(j);
                            break;
                        }

                        // Couldn't see the difference
                        //throw new NotImplementedException();
                    }
                }

                foreach (int i in removedMethodsIndices.Reverse())
                {
                    @class.Methods.RemoveAt(i);
                }
                removedMethodsIndices.Clear();
            }
        }

        // Move all public nested classes out into the namespace scope
        // unless there is a name conflict.
        // https://msdn.microsoft.com/en-us/library/s9f3ty7f%28v=vs.71%29.aspx
        private void FlattenClassHierarchy()
        {
            var nonNestedClasses = Project.HeaderDefinitions.SelectMany(h => h.Value.Classes);
            var nestedClasses = nonNestedClasses.SelectMany(c => c.NestedClasses).ToList();
            foreach (var @class in nestedClasses)
            {
                // TODO
            }
        }

        private void CopyTemplateMethods(ClassDefinition thisClass, ClassTemplateDefinition template,
            Dictionary<string, string> templateParams = null)
        {
            if (templateParams == null)
            {
                templateParams = new Dictionary<string, string>();
                var templateBase = template.BaseClass as ClassTemplateDefinition;

                for (int i = 0; i < template.TemplateParameters.Count; i++)
                {
                    string param = templateBase.TemplateParameters[i];
                    string paramValue = template.TemplateParameters[i];
                    templateParams[param] = paramValue;
                }

                CopyTemplateMethods(thisClass, templateBase, templateParams);
                return;
            }

            thisClass.BaseClass = template.BaseClass;

            var scriptedNameMapping = Project.ClassNameMapping as ScriptedMapping;
            if (scriptedNameMapping != null)
            {
                scriptedNameMapping.Globals.Header = thisClass.Header;
            }
            template.ManagedName = Project.ClassNameMapping.Map(template.Name);

            foreach (var templateClass in template.NestedClasses)
            {
                var classSpec = new ClassDefinition(templateClass.Name, thisClass.Header, thisClass);
                Project.ClassDefinitions.Add(classSpec.FullyQualifiedName, classSpec);

                foreach (var templateMethod in templateClass.Methods)
                {
                    // Replace template parameters with concrete types
                    var methodSpec = templateMethod.Copy(classSpec);
                    if (methodSpec.ReturnType.HasTemplateTypeParameter)
                    {
                        methodSpec.ReturnType = methodSpec.ReturnType.CopyTemplated(templateParams);
                    }

                    foreach (var param in methodSpec.Parameters
                        .Where(p => p.Type.HasTemplateTypeParameter))
                    {
                        param.Type = param.Type.CopyTemplated(templateParams);
                    }
                }
            }

            foreach (var templateMethod in template.Methods.Where(m => !m.IsConstructor))
            {
                // Replace template parameters with concrete types
                var methodSpec = templateMethod.Copy(thisClass);
                if (methodSpec.ReturnType.HasTemplateTypeParameter)
                {
                    methodSpec.ReturnType = methodSpec.ReturnType.CopyTemplated(templateParams);
                }

                foreach (var param in methodSpec.Parameters
                    .Where(p => p.Type.HasTemplateTypeParameter))
                {
                    param.Type = param.Type.CopyTemplated(templateParams);
                }
            }
        }

        private void ResolveTemplateSpecializations()
        {
            // Class list may be modified with template specializations,
            // so make a copy
            var classes = Project.ClassDefinitions.Values.ToList();

            // If base class is a template, copy template methods to this class
            foreach (var @class in classes.Where(c => !(c is ClassTemplateDefinition)))
            {
                var classTemplate = @class.BaseClass as ClassTemplateDefinition;
                if (classTemplate != null)
                {
                    // TODO: if the base template is a full template specialization,
                    // decide whether it needs to be exposed as a separate class
                    // or just have the methods copied from it.
                    // For now, just copy the methods.

                    CopyTemplateMethods(@class, classTemplate);
                }
            }
        }

        // Give managed names to headers, classes and methods
        void MapSymbols()
        {
            // Get managed header and enum names
            var nameMapping = Project.HeaderNameMapping as ScriptedMapping;
            foreach (var header in Project.HeaderDefinitions.Values)
            {
                if (nameMapping != null)
                {
                    nameMapping.Globals.Header = header;
                }
                header.ManagedName = Project.HeaderNameMapping.Map(header.Name);
            }

            // Apply class properties
            nameMapping = Project.ClassNameMapping as ScriptedMapping;
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                if (nameMapping != null) nameMapping.Globals.Header = @class.Header;
                @class.ManagedName = Project.ClassNameMapping.Map(@class.Name);

                var @enum = @class as EnumDefinition;
                if (@enum != null)
                {
                    if (@enum.Parent != null &&
                        @enum.Parent.Methods.Count == 0 &&
                        @enum.Parent.Fields.Count == 0 &&
                        @enum.Parent.NestedClasses.Count == 1)
                    {
                        if (nameMapping != null) nameMapping.Globals.Header = @enum.Parent.Header;
                        @enum.ManagedName = Project.ClassNameMapping.Map(@enum.Parent.Name);
                    }
                }
            }

            // Set managed method/parameter names
            foreach (var method in Project.ClassDefinitions.Values.SelectMany(c => c.Methods))
            {
                method.ManagedName = GetManagedMethodName(method);

                for (int i = 0; i < method.Parameters.Length; i++)
                {
                    var param = method.Parameters[i];
                    if (param.Name == "")
                    {
                        param.Name = $"__unnamed{i}";
                        param.ManagedName = param.Name;
                    }
                    else
                    {
                        param.ManagedName = GetManagedParameterName(param);
                    }
                }
            }
        }

        string GetManagedMethodName(MethodDefinition method)
        {
            if (method.IsConstructor)
            {
                return method.Parent.ManagedName;
            }

            string mapping = Project.MethodNameMapping?.Map(method.Name);
            if (mapping != null) return mapping;

            if (method.Name.StartsWith("operator"))
            {
                return method.Name;
            }

            return ToCamelCase(method.Name, true);
        }

        string GetManagedParameterName(ParameterDefinition param)
        {
            string mapping = Project.ParameterNameMapping?.Map(param.Name);
            if (mapping != null) return mapping;

            return ToCamelCase(param.Name, false);
        }

        // Create default constructor if no explicit C++ constructor exists.
        void CreateDefaultConstructors()
        {
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                if (@class.HidePublicConstructors) continue;
                if (@class.IsStatic) continue;
                if (@class is EnumDefinition) continue;
                if (@class.IsPureEnum) continue;

                var constructors = @class.Methods.Where(m => m.IsConstructor);
                if (!constructors.Any())
                {
                    // Only possible if base class has a default constructor
                    if (@class.BaseClass == null || (@class.BaseClass != null &&
                        @class.BaseClass.Methods.Any(m => m.IsConstructor && !m.Parameters.Any())))
                    {
                        var constructor = new MethodDefinition(@class.Name, @class, 0)
                        {
                            IsConstructor = true,
                            ReturnType = new TypeRefDefinition("void")
                        };
                        constructor.ManagedName = GetManagedMethodName(constructor);
                    }
                }
            }
        }

        string[] _booleanVerbs = { "Has", "Is", "Needs" };

        // Create getters and setters for fields
        void CreateFieldAccessors()
        {
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                foreach (var field in @class.Fields)
                {
                    string name = field.Name;
                    if (name.StartsWith("m_"))
                    {
                        name = name.Substring(2);
                    }
                    name = char.ToUpper(name[0]) + name.Substring(1); // capitalize
                    string managedName = ToCamelCase(name, true);

                    // Generate getter/setter methods
                    string getterName, setterName;
                    string managedGetterName, managedSetterName;
                    string verb = _booleanVerbs.FirstOrDefault(v => name.StartsWith(v));
                    if (verb != null && "bool".Equals(field.Type.Name))
                    {
                        getterName = name;
                        setterName = "set" + name.Substring(verb.Length);
                        managedGetterName = managedName;
                        managedSetterName = "Set" + managedName.Substring(verb.Length);
                    }
                    else
                    {
                        getterName = "get" + name;
                        setterName = "set" + name;
                        managedGetterName = "Get" + managedName;
                        managedSetterName = "Set" + managedName;
                    }

                    // See if there are already accessor methods for this field
                    MethodDefinition getter = null, setter = null;
                    foreach (var method in @class.Methods)
                    {
                        if (managedGetterName.Equals(method.ManagedName) && method.Parameters.Length == 0)
                        {
                            getter = method;
                            continue;
                        }

                        if (managedSetterName.Equals(method.ManagedName) && method.Parameters.Length == 1)
                        {
                            setter = method;
                        }
                    }

                    if (getter == null)
                    {
                        getter = new MethodDefinition(getterName, @class, 0);
                        getter.ManagedName = managedGetterName;
                        getter.ReturnType = field.Type;
                        getter.Field = field;
                    }
                    var type = field.Type.Canonical;
                    if (!type.IsBasic && type.Referenced == null)
                    {
                        if (!(type.Target != null && type.Target.MarshalAsStruct))
                        {
                            getter.ReturnType = new TypeRefDefinition(field.Type.Name)
                            {
                                Kind = TypeKind.Pointer,
                                Referenced = field.Type.Copy()
                            };
                        }
                    }

                    var prop = new PropertyDefinition(getter, GetPropertyName(getter));

                    if (setter == null)
                    {
                        CreateFieldSetter(prop, setterName, managedSetterName);
                    }
                }
            }
        }

        void CreateFieldSetter(PropertyDefinition prop, string setterName, string managedSetterName)
        {
            var type = prop.Type;
            var typeCanonical = type.Canonical;

            // Can't assign value to reference or constant array
            switch (typeCanonical.Kind)
            {
                case TypeKind.LValueReference:
                case TypeKind.ConstantArray:
                    return;
            }

            if (typeCanonical.Name != null && typeCanonical.Name.StartsWith("btAlignedObjectArray")) return;

            var setter = new MethodDefinition(setterName, prop.Parent, 1);
            setter.ManagedName = managedSetterName;
            setter.ReturnType = new TypeRefDefinition("void");
            setter.Field = prop.Getter.Field;
            if (typeCanonical.Target != null && typeCanonical.Target.MarshalAsStruct)
            {
                type = new TypeRefDefinition
                {
                    IsConst = true,
                    Kind = TypeKind.Pointer,
                    Referenced = type.Copy()
                };
            }
            else if (!typeCanonical.IsBasic && typeCanonical.Kind != TypeKind.Pointer)
            {
                type = type.Copy();
                type.IsConst = true;
            }
            setter.Parameters[0] = new ParameterDefinition("value", type)
            {
                MarshalDirection = MarshalDirection.In,
                ManagedName = "value"
            };

            prop.Setter = setter;
            prop.Setter.Property = prop;
        }

        string GetPropertyName(MethodDefinition getter)
        {
            string name = getter.ManagedName;

            var propertyType = getter.IsVoid ? getter.Parameters[0].Type : getter.ReturnType;
            if ("bool".Equals(propertyType.Name) && _booleanVerbs.Any(v => name.StartsWith(v)))
            {
                return name;
            }

            if (name.StartsWith("Get"))
            {
                return name.Substring(3);
            }

            throw new NotImplementedException();
        }

        // Turn getters and setters into properties,
        // managed method names have been resolved at this point
        void CreateProperties()
        {
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                // Getters with return type and 0 arguments
                var getterMethods = @class.Methods.Where(m => !m.IsConstructor && !m.IsVoid && m.Parameters.Length == 0);
                foreach (var method in getterMethods)
                {
                    if (method.ManagedName.StartsWith("Get") ||
                        (method.ReturnType.Kind == TypeKind.Bool &&
                        _booleanVerbs.Any(v => method.ManagedName.StartsWith(v))))
                    {
                        if (method.Property != null) continue;
                        new PropertyDefinition(method, GetPropertyName(method));
                    }
                }

                // Getters with void type and 1 pointer argument for the return value.
                // Can only be done for types with value semantics
                foreach (var method in @class.Methods.Where(m => m.IsVoid && m.Parameters.Length == 1))
                {
                    if (method.ManagedName.StartsWith("Get"))
                    {
                        if (method.Property != null) continue;

                        var paramType = method.Parameters[0].Type.Canonical;
                        if (paramType.IsConst) continue;

                        switch (paramType.Kind)
                        {
                            case TypeKind.Pointer:
                            case TypeKind.LValueReference:
                                var referenced = paramType.Referenced.Canonical;
                                if (referenced.Target != null && referenced.Target.MarshalAsStruct)
                                {
                                    new PropertyDefinition(method, GetPropertyName(method));
                                }
                                break;
                        }
                    }
                }

                // Setters
                foreach (var method in @class.Methods)
                {
                    if (method.Parameters.Length == 1 &&
                        method.Name.StartsWith("set", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string name = method.ManagedName.Substring(3);
                        // Find the property with the matching getter
                        foreach (var prop in @class.Properties)
                        {
                            if (prop.Setter != null)
                            {
                                continue;
                            }

                            if (prop.Name.Equals(name))
                            {
                                prop.Setter = method;
                                method.Property = prop;
                                break;
                            }
                        }
                    }
                }
            }
        }

        void ResolveInclude(TypeRefDefinition type, HeaderDefinition parentHeader)
        {
            switch (type.Kind)
            {
                case TypeKind.Pointer:
                case TypeKind.LValueReference:
                case TypeKind.ConstantArray:
                    ResolveInclude(type.Referenced, parentHeader);
                    return;
            }
            if (type.TemplateParams != null)
            {
                type.TemplateParams.ForEach(p => ResolveInclude(p, parentHeader));
            }
            else if (type.IsIncomplete && type.Target != null)
            {
                parentHeader.Includes.Add(type.Target.Header);
            }
        }

        // Add includes for incomplete types (forward references)
        // Should be done after removing redundant methods.
        void ResolveIncludes()
        {
            var classDefinitionsList = new List<ClassDefinition>(Project.ClassDefinitions.Values);
            foreach (var @class in classDefinitionsList.Where(c => !c.IsExcluded))
            {
                var header = @class.Header;

                // Include header for the base if necessary
                if (@class.BaseClass != null && header != @class.BaseClass.Header)
                {
                    header.Includes.Add(@class.BaseClass.Header);
                }

                // Resolve method return type and parameter types
                foreach (var method in @class.Methods)
                {
                    ResolveInclude(method.ReturnType, header);
                    foreach (ParameterDefinition param in method.Parameters)
                    {
                        ResolveInclude(param.Type, header);
                    }
                }

                // Resolve field types
                foreach (var field in @class.Fields)
                {
                    ResolveInclude(field.Type, header);
                }
            }
        }
    }
}
