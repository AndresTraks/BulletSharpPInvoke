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
            CreateFieldAccessors();
            MapSymbols();
            ParseEnums();
            SetClassProperties();
            RemoveRedundantMethods();
            FlattenClassHierarchy();
            ResolveTemplateSpecializations();
            CreateDefaultConstructors();
            CreateDestructors();
            SortMembers();
            ResolveIncludes();
        }

        // n = 2 -> "\t\t"
        protected static string GetTabs(int n)
        {
            return new string('\t', 2);
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
            switch (type.Kind)
            {
                case TypeKind.ConstantArray:
                case TypeKind.LValueReference:
                case TypeKind.Pointer:
                    return TypeRequiresMarshal(type.Referenced);
            }

            return type.Target != null && type.Target.MarshalAsStruct;
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

        protected virtual void SortMembers()
        {
            // Sort methods alphabetically
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                // Order by name, then fix inheritance, parent classes must appear first
                @class.NestedClasses.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                var classesOrdered = @class.NestedClasses;
                for (int i = 0; i < classesOrdered.Count; i++)
                {
                    var thisClass = classesOrdered[i];
                    var baseClass = thisClass.BaseClass;
                    if (baseClass != null && classesOrdered.Contains(baseClass))
                    {
                        int thisIndex = classesOrdered.IndexOf(thisClass);
                        if (thisIndex < classesOrdered.IndexOf(baseClass))
                        {
                            classesOrdered.Remove(baseClass);
                            classesOrdered.Insert(thisIndex, baseClass);
                        }
                    }
                }

                @class.Methods.Sort((m1, m2) => m1.Name.CompareTo(m2.Name));
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
            // TODO:
            //template.ManagedName = Project.ClassNameMapping.Map(template.Name);

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
            /*
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
            */

            // Rename empty parameter names to "__unnamedX"
            foreach (var method in Project.ClassDefinitions.Values.SelectMany(c => c.Methods))
            {
                for (int i = 0; i < method.Parameters.Length; i++)
                {
                    var param = method.Parameters[i];
                    if (param.Name.Equals(""))
                    {
                        param.Name = $"__unnamed{i}";
                    }
                }
            }
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
                        //TODO:
                        //constructor.ManagedName = GetManagedMethodName(constructor);
                    }
                }
            }
        }

        private void CreateDestructors()
        {
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                if (@class.BaseClass != null) continue;
                if (@class.NoInternalConstructor && @class.HidePublicConstructors) continue;
                if (@class.IsStatic) continue;
                if (@class is EnumDefinition) continue;
                if (@class.IsPureEnum) continue;

                var overloadIndex = @class.Methods.Count(m => m.Name.Equals("delete"));
                string name = overloadIndex == 0 ? "delete" : $"delete{overloadIndex + 1}";
                var deleteMethod = new MethodDefinition(name, @class, 0)
                {
                    IsDestructor = true,
                    ReturnType = new TypeRefDefinition("void")
                };
            }
        }

        private void CreateFieldGetter(FieldDefinition field, ClassDefinition @class, string getterName, MethodDefinition setter)
        {
            MethodDefinition getter;

            // Use getter with an out parameter for structs
            if (field.Type.Target != null && field.Type.Target.MarshalAsStruct)
            {
                getter = new MethodDefinition(getterName, @class, 1);
                getter.ReturnType = new TypeRefDefinition("void");

                string paramName = setter != null ? setter.Parameters[0].Name : "value";
                var paramType = new TypeRefDefinition(field.Type.Name)
                {
                    Kind = TypeKind.Pointer,
                    Referenced = field.Type.Copy()
                };
                getter.Parameters[0] = new ParameterDefinition(paramName, paramType)
                {
                    MarshalDirection = MarshalDirection.Out
                };
            }
            else
            {
                TypeRefDefinition type;
                if (field.Type.Canonical.Kind == TypeKind.Record)
                {
                    type = new TypeRefDefinition()
                    {
                        Kind = TypeKind.Pointer,
                        Referenced = field.Type.Copy()
                    };
                }
                else if (field.Type.Canonical.Kind == TypeKind.Unexposed)
                {
                    // TODO:
                    type = field.Type;
                }
                else
                {
                    type = field.Type;
                }

                getter = new MethodDefinition(getterName, @class, 0);
                getter.ReturnType = type;
            }

            getter.Field = field;
            field.Getter = getter;
        }

        private void CreateFieldSetter(FieldDefinition field, ClassDefinition @class, string setterName)
        {
            var type = field.Type;
            var typeCanonical = type.Canonical;

            // Can't assign value to reference or constant array
            switch (typeCanonical.Kind)
            {
                case TypeKind.LValueReference:
                case TypeKind.ConstantArray:
                    return;
                case TypeKind.Record:
                    if (typeCanonical.Target == null) return;
                    if (!typeCanonical.Target.MarshalAsStruct) return;

                    type = new TypeRefDefinition
                    {
                        IsConst = true,
                        Kind = TypeKind.Pointer,
                        Referenced = type.Copy()
                    };
                    break;
                default:
                    type = type.Copy();
                    break;
            }

            var setter = new MethodDefinition(setterName, @class, 1)
            {
                Field = field,
                ReturnType = new TypeRefDefinition("void")
            };
            setter.Parameters[0] = new ParameterDefinition("value", type)
            {
                MarshalDirection = MarshalDirection.In
            };

            field.Setter = setter;
        }

        private void CreateFieldAccessors()
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

                    // Generate getter/setter methods
                    string getterName, setterName;
                    /*
                    string verb = _booleanVerbs.FirstOrDefault(v => name.StartsWith(v));
                    if (verb != null && "bool".Equals(field.Type.Name))
                    {
                        getterName = name;
                        setterName = "set" + name.Substring(verb.Length);
                    }
                    else*/
                    {
                        getterName = "get" + name;
                        setterName = "set" + name;
                    }

                    // See if there are already C++ accessor methods for this field
                    MethodDefinition getter = null, setter = null;

                    foreach (var method in @class.Methods)
                    {
                        if (getterName.Equals(method.Name))
                        {
                            if (method.IsVoid)
                            {
                                if (method.Parameters.Length == 1)
                                {
                                    // TODO: check parameter type
                                    getter = method;
                                    break;
                                }
                            }
                            else
                            {
                                if (method.Parameters.Length == 0)
                                {
                                    // TODO: check return type
                                    getter = method;
                                    break;
                                }
                            }
                        }

                        if (setterName.Equals(method.Name) && method.Parameters.Length == 1)
                        {
                            setter = method;
                        }
                    }

                    if (getter == null)
                    {
                        CreateFieldGetter(field, @class, getterName, setter);
                    }

                    if (setter == null)
                    {
                        CreateFieldSetter(field, @class, setterName);
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
                if (type.Target.MarshalAsStruct) return;

                parentHeader.Includes.Add(type.Target.Header);
            }
        }

        // Add includes for incomplete types (forward references)
        // Should be done after removing redundant methods.
        void ResolveIncludes()
        {
            foreach (var @class in Project.ClassDefinitions.Values.Where(c => !c.IsExcluded))
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
                    foreach (var param in method.Parameters)
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
