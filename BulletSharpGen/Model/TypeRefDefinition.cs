using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp;

namespace BulletSharpGen
{
    public class TypeRefDefinition
    {
        public string Name { get; set; }

        public TypeKind Kind { get; set; }

        /// <summary>
        /// Type is void, int, float, enum, etc.
        /// or a typedef to a basic type.
        /// </summary>
        public bool IsBasic { get; set; }

        /// <summary>
        /// Class declaration is a forward reference.
        /// </summary>
        public bool IsIncomplete { get; set; }

        public TypeRefDefinition Referenced { get; set; }

        public bool IsConst { get; set; }
        public bool HasTemplateTypeParameter { get; set; }
        public List<TypeRefDefinition> TemplateParams { get; set; }

        private bool _unresolved;
        public ClassDefinition Target { get; set; }

        public TypeRefDefinition Canonical
        {
            get { return Kind == TypeKind.Typedef ? Referenced : this; }
        }

        public string FullName
        {
            get
            {
                if (Target != null)
                {
                    return Target.FullyQualifiedName;
                }
                return Name;
            }
        }

        public string ManagedName
        {
            get
            {
                if (IsBasic)
                {
                    if (Name.Equals("unsigned char"))
                    {
                        return "byte";
                    }
                    return Name;
                }
                if (HasTemplateTypeParameter)
                {
                    return "T";
                }
                switch (Kind)
                {
                    case TypeKind.Pointer:
                    case TypeKind.LValueReference:
                    case TypeKind.ConstantArray:
                        return Referenced.ManagedName;
                }
                if (Target == null)
                {
                    if (!_unresolved)
                    {
                        Console.WriteLine("Unresolved reference to " + Name);
                    }
                    _unresolved = true;
                    return Name;
                }
                return Target.ManagedName;
            }
        }

        public TypeRefDefinition(ClangSharp.Type type, Cursor cursor = null)
        {
            Kind = (TypeKind)type.TypeKind;

            var firstChild = cursor?.Children.FirstOrDefault();
            if (firstChild != null && firstChild.Kind == CursorKind.TemplateRef)
            {
                if (cursor.Children.Count == 1)
                {
                    string displayName = cursor.Type.Declaration.DisplayName;
                    int typeStart = displayName.IndexOf('<') + 1;
                    int typeEnd = displayName.LastIndexOf('>');
                    displayName = displayName.Substring(typeStart, typeEnd - typeStart);
                    TemplateParams = new List<TypeRefDefinition> { new TypeRefDefinition(displayName) };
                }
                else
                {
                    TemplateParams = cursor.Children.Skip(1)
                        .Select(c => new TypeRefDefinition(c.Type, c)).ToList();
                }
            }

            if (!type.Declaration.IsInvalid &&
                !type.Declaration.IsDefinition &&
                type.Declaration.SpecializedCursorTemplate.IsInvalid)
            {
                // Forward reference
                IsIncomplete = true;
            }

            IsConst = type.IsConstQualifiedType;

            if (type.Pointee.TypeKind != ClangSharp.Type.Kind.Invalid)
            {
                Cursor pointeeCursor = null;
                if (cursor != null)
                {
                    pointeeCursor = cursor.Children.FirstOrDefault(c => c.Type.Equals(type.Pointee));
                }
                Referenced = new TypeRefDefinition(type.Pointee, pointeeCursor);
            }

            switch (Kind)
            {
                case TypeKind.Void:
                case TypeKind.Bool:
                case TypeKind.Char_S:
                case TypeKind.Double:
                case TypeKind.Float:
                case TypeKind.Int:
                case TypeKind.UChar:
                case TypeKind.UInt:
                    Name = type.Spelling;
                    IsBasic = true;
                    break;
                case TypeKind.Long:
                    Name = "long";
                    IsBasic = true;
                    break;
                case TypeKind.LongLong:
                    Name = "long long";
                    IsBasic = true;
                    break;
                case TypeKind.Short:
                    Name = "short";
                    IsBasic = true;
                    break;
                case TypeKind.ULong:
                    Name = "unsigned long";
                    IsBasic = true;
                    break;
                case TypeKind.ULongLong:
                    Name = "unsigned long long";
                    IsBasic = true;
                    break;
                case TypeKind.UShort:
                    Name = "unsigned short";
                    IsBasic = true;
                    break;

                case TypeKind.Typedef:
                    Name = GetFullyQualifiedName(type);
                    Referenced = new TypeRefDefinition(type.Canonical);
                    break;

                case TypeKind.FunctionProto:
                case TypeKind.LValueReference:
                case TypeKind.Pointer:
                    break;
                case TypeKind.ConstantArray:
                    Referenced = new TypeRefDefinition(type.ArrayElementType);
                    break;

                case TypeKind.Enum:
                    Name = type.Canonical.Declaration.Spelling;
                    IsBasic = true;
                    break;
                case TypeKind.Record:
                case TypeKind.Unexposed:
                    Name = GetFullyQualifiedName(type);
                    break;
                case TypeKind.DependentSizedArray:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public TypeRefDefinition(string name)
        {
            switch (name)
            {
                case "bool":
                    Kind = TypeKind.Bool;
                    Name = "bool";
                    IsBasic = true;
                    break;
                case "char":
                    Kind = TypeKind.Char_U;
                    Name = "char";
                    IsBasic = true;
                    break;
                case "double":
                    Kind = TypeKind.Double;
                    Name = "double";
                    IsBasic = true;
                    break;
                case "float":
                    Kind = TypeKind.Float;
                    Name = "float";
                    IsBasic = true;
                    break;
                case "int":
                    Kind = TypeKind.Int;
                    Name = "int";
                    IsBasic = true;
                    break;
                case "unsigned char":
                    Kind = TypeKind.UChar;
                    Name = "unsigned char";
                    IsBasic = true;
                    break;
                case "unsigned int":
                    Kind = TypeKind.UInt;
                    Name = "unsigned int";
                    IsBasic = true;
                    break;
                case "void":
                    Kind = TypeKind.Void;
                    Name = name;
                    IsBasic = true;
                    break;
                case "long int":
                    Kind = TypeKind.Long;
                    Name = "long";
                    IsBasic = true;
                    break;
                case "long long int":
                    Kind = TypeKind.LongLong;
                    Name = "long long";
                    IsBasic = true;
                    break;
                case "short int":
                    Kind = TypeKind.Short;
                    Name = "short";
                    IsBasic = true;
                    break;
                case "unsigned long int":
                    Kind = TypeKind.ULong;
                    Name = "unsigned long";
                    IsBasic = true;
                    break;
                case "unsigned long long int":
                    Kind = TypeKind.ULongLong;
                    Name = "unsigned long long";
                    IsBasic = true;
                    break;
                case "unsigned short int":
                    Kind = TypeKind.UShort;
                    Name = "unsigned short";
                    IsBasic = true;
                    break;
                default:
                    if (name.EndsWith(" *"))
                    {
                        Referenced = new TypeRefDefinition(name.Substring(0, name.Length - 2));
                        Kind = TypeKind.Pointer;
                        break;
                    }
                    Name = name;
                    break;
            }
        }

        public TypeRefDefinition()
        {
        }

        public TypeRefDefinition Copy()
        {
            var t = new TypeRefDefinition
            {
                HasTemplateTypeParameter = HasTemplateTypeParameter,
                IsBasic = IsBasic,
                IsConst = IsConst,
                IsIncomplete = IsIncomplete,
                Name = Name,
                Referenced = Referenced != null ? Referenced.Copy() : null,
                TemplateParams = TemplateParams?.Select(p => p.Copy()).ToList(),
                Target = Target,
                Kind = Kind
            };
            return t;
        }

        public static string GetFullyQualifiedName(ClangSharp.Type type)
        {
            string name;
            var decl = type.Declaration;
            if (decl.IsInvalid)
            {
                name = "[unexposed type]";
            }
            else if (!decl.SpecializedCursorTemplate.IsInvalid)
            {
                name = decl.DisplayName;
            }
            else
            {
                name = decl.Spelling;
                while (decl.SemanticParent.Kind == ClangSharp.CursorKind.ClassDecl ||
                    decl.SemanticParent.Kind == ClangSharp.CursorKind.StructDecl ||
                    decl.SemanticParent.Kind == ClangSharp.CursorKind.ClassTemplate ||
                    decl.SemanticParent.Kind == ClangSharp.CursorKind.Namespace)
                {
                    name = decl.SemanticParent.Spelling + "::" + name;
                    decl = decl.SemanticParent;
                }
            }
            return name;
        }

        public static string GetFullyQualifiedName(ClangSharp.Cursor cursor)
        {
            string name;
            if (cursor.Type.TypeKind != ClangSharp.Type.Kind.Invalid)
            {
                return GetFullyQualifiedName(cursor.Type);
            }
            else
            {
                name = cursor.Spelling;
                while (cursor.SemanticParent.Kind == ClangSharp.CursorKind.ClassDecl ||
                    cursor.SemanticParent.Kind == ClangSharp.CursorKind.StructDecl ||
                    cursor.SemanticParent.Kind == ClangSharp.CursorKind.ClassTemplate ||
                    cursor.SemanticParent.Kind == ClangSharp.CursorKind.Namespace)
                {
                    name = cursor.SemanticParent.Spelling + "::" + name;
                    cursor = cursor.SemanticParent;
                }
            }
            return name;
        }

        public override bool Equals(object obj)
        {
            TypeRefDefinition t = obj as TypeRefDefinition;
            if (t == null)
            {
                return false;
            }

            if (t.Kind != Kind) return false;

            if (t.Referenced != null)
            {
                if (!t.Referenced.Equals(Referenced)) return false;
            }

            return string.Equals(Name, t.Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return ManagedName ?? Name;
        }
    }
}
