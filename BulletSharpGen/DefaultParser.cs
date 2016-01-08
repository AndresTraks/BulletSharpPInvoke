using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    class DefaultParser
    {
        public WrapperProject Project { get; private set; }

        public DefaultParser(WrapperProject project)
        {
            Project = project;
        }

        public virtual void Parse()
        {
            ResolveReferences();

            MarkAbstractClasses();

            ParseEnums();
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
            int left = 0, right;

            while (left < text.Length)
            {
                right = text.IndexOf('_', left);
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

        void MarkAbstractClasses()
        {
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                @class.IsAbstract = @class.AbstractMethods.Any();
            }
        }

        void ParseEnums()
        {
            // For enums, remove any common prefix and check for flags
            foreach (ClassDefinition @class in Project.ClassDefinitions.Values.Where(c => c is EnumDefinition))
            {
                var @enum = @class as EnumDefinition;

                int prefixLength = @enum.GetCommonPrefix().Length;
                @enum.GetCommonSuffix();
                for (int i = 0; i < @enum.EnumConstants.Count; i++)
                {
                    string enumConstant = @enum.EnumConstants[i];
                    enumConstant = enumConstant.Substring(prefixLength);
                    @enum.EnumConstants[i] = ToCamelCase(enumConstant, true);
                }

                if (@enum.Name.EndsWith("Flags"))
                {
                    @enum.IsFlags = true;
                }
                else
                {
                    // If all values are powers of 2, then it is considered a Flags enum.
                    @enum.IsFlags = @enum.EnumConstantValues.All(value =>
                    {
                        int x;
                        if (int.TryParse(value, out x))
                        {
                            return (x != 0) && ((x & (~x + 1)) == x);
                        }
                        return false;
                    });
                }

                if (@enum.IsFlags)
                {
                    if (!@enum.EnumConstantValues.Any(value => value.Equals("0")))
                    {
                        @enum.EnumConstants.Insert(0, "None");
                        @enum.EnumConstantValues.Insert(0, "0");
                    }
                }
            }
        }

        void ResolveReferences()
        {
            // Resolve references (match TypeRefDefinitions to ClassDefinitions)
            // List might be modified with template specialization classes, so make a copy
            var classDefinitionsList = new List<ClassDefinition>(Project.ClassDefinitions.Values);
            foreach (ClassDefinition c in classDefinitionsList)
            {
                // Include header for the base if necessary
                if (c.BaseClass != null && c.Header != c.BaseClass.Header)
                {
                    c.Header.Includes.Add(c.BaseClass.Header);
                }

                // Resolve typedef
                if (c.TypedefUnderlyingType != null)
                {
                    ResolveTypeRef(c.TypedefUnderlyingType);
                }

                // Resolve method return type and parameter types
                foreach (MethodDefinition method in c.Methods)
                {
                    ResolveTypeRef(method.ReturnType);
                    foreach (ParameterDefinition param in method.Parameters)
                    {
                        ResolveTypeRef(param.Type);
                    }
                }

                // Resolve field types
                foreach (FieldDefinition field in c.Fields)
                {
                    ResolveTypeRef(field.Type);
                }
            }
        }

        void ResolveTypeRef(TypeRefDefinition typeRef)
        {
            if (typeRef.IsBasic || typeRef.HasTemplateTypeParameter)
            {
                return;
            }
            if (typeRef.IsPointer || typeRef.IsReference || typeRef.IsConstantArray)
            {
                ResolveTypeRef(typeRef.Referenced);
            }
            else if (!Project.ClassDefinitions.ContainsKey(typeRef.Name))
            {
                // Search for unscoped enums
                bool resolvedEnum = false;
                foreach (var c in Project.ClassDefinitions.Values.Where(c => c is EnumDefinition))
                {
                    if (typeRef.Name.Equals(c.FullyQualifiedName + "::" + c.Name))
                    {
                        typeRef.Target = c;
                        resolvedEnum = true;
                    }
                }
                if (!resolvedEnum)
                {
                    Console.WriteLine("Class " + typeRef.Name + " not found!");
                }
            }
            else
            {
                typeRef.Target = Project.ClassDefinitions[typeRef.Name];
            }

            if (typeRef.SpecializedTemplateType != null)
            {
                ResolveTypeRef(typeRef.SpecializedTemplateType);

                // Create template specialization class
                string name = string.Format("{0}<{1}>", typeRef.Name, typeRef.SpecializedTemplateType.Name);
                if (!Project.ClassDefinitions.ContainsKey(name))
                {
                    var templateClass = typeRef.Target;
                    if (templateClass != null && !templateClass.IsExcluded)
                    {
                        var header = templateClass.Header;
                        var specializedClass = new ClassDefinition(name, header);
                        specializedClass.BaseClass = templateClass;
                        header.Classes.Add(specializedClass);
                        Project.ClassDefinitions.Add(name, specializedClass);
                    }
                }
            }
        }
    }
}
