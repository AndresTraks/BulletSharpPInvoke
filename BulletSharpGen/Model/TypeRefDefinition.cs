using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp;

namespace BulletSharpGen
{
    public class TypeRefDefinition
    {
        public string Name { get; set; }

        /// <summary>
        /// Type is void, int, float, enum, etc.
        /// or a typedef to a basic type.
        /// </summary>
        public bool IsBasic { get; set; }

        /// <summary>
        /// Class declaration is a forward reference.
        /// </summary>
        public bool IsIncomplete { get; set; }

        public bool IsPointer { get; set; }
        public bool IsReference { get; set; }
        public bool IsConstantArray { get; set; }
        public bool IsConst { get; set; }
        public TypeRefDefinition Referenced { get; set; }
        public bool HasTemplateTypeParameter { get; set; }
        public List<TypeRefDefinition> TemplateParams { get; set; }

        private bool _unresolved;
        public ClassDefinition Target { get; set; }

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
                    if (Referenced != null)
                    {
                        return Referenced.ManagedName;
                    }
                    return Name;
                }
                if (HasTemplateTypeParameter)
                {
                    return "T";
                }
                if (IsPointer || IsReference || IsConstantArray)
                {
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
                if (TemplateParams != null)
                {
                    return Target.ManagedName + "<" + TemplateParams.First().ManagedName + ">";
                }
                return Target.ManagedName;
            }
        }

        public string ManagedNameCS
        {
            get
            {
                if (IsBasic)
                {
                    if (Name.Equals("unsigned short"))
                    {
                        return "ushort";
                    }
                    if (Name.Equals("unsigned int"))
                    {
                        return "uint";
                    }
                    if (Name.Equals("unsigned long"))
                    {
                        return "ulong";
                    }
                }
                return ManagedName;
            }
        }

        public string ManagedTypeRefName
        {
            get
            {
                if (IsPointer || IsReference || IsConstantArray)
                {
                    if (IsBasic)
                    {
                        return ManagedName + '*';
                    }
                    switch (ManagedName)
                    {
                        case "void":
                            return "IntPtr";
                        case "char":
                            return "String^";
                        case "float":
                            return string.Format("array<{0}>^", Referenced.Name);
                    }
                    return ManagedName + '^';
                }
                return ManagedName;
            }
        }

        public TypeRefDefinition(ClangSharp.Type type, Cursor cursor = null)
        {
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

            switch (type.TypeKind)
            {
                case ClangSharp.Type.Kind.Void:
                case ClangSharp.Type.Kind.Bool:
                case ClangSharp.Type.Kind.CharS:
                case ClangSharp.Type.Kind.Double:
                case ClangSharp.Type.Kind.Float:
                case ClangSharp.Type.Kind.Int:
                case ClangSharp.Type.Kind.UChar:
                case ClangSharp.Type.Kind.UInt:
                    Name = type.Spelling;
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.Long:
                    Name = "long";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.LongLong:
                    Name = "long long";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.Short:
                    Name = "short";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.ULong:
                    Name = "unsigned long";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.ULongLong:
                    Name = "unsigned long long";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.UShort:
                    Name = "unsigned short";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.Typedef:
                    Name = GetFullyQualifiedName(type);
                    Referenced = new TypeRefDefinition(type.Canonical);
                    IsBasic = Referenced.IsBasic;
                    break;
                case ClangSharp.Type.Kind.Pointer:
                    IsPointer = true;
                    break;
                case ClangSharp.Type.Kind.LValueReference:
                    IsReference = true;
                    break;
                case ClangSharp.Type.Kind.ConstantArray:
                    Referenced = new TypeRefDefinition(type.ArrayElementType);
                    IsConstantArray = true;
                    break;
                case ClangSharp.Type.Kind.FunctionProto:
                    // ??
                    break;
                case ClangSharp.Type.Kind.Enum:
                    Name = type.Canonical.Declaration.Spelling;
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.Record:
                case ClangSharp.Type.Kind.Unexposed:
                    Name = GetFullyQualifiedName(type);
                    break;
                case ClangSharp.Type.Kind.DependentSizedArray:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public TypeRefDefinition(string name)
        {
            if (name.EndsWith(" *"))
            {
                Referenced = new TypeRefDefinition(name.Substring(0, name.Length - 2));
                IsPointer = true;
            }
            else
            {
                switch (name)
                {
                    case "bool":
                    case "char":
                    case "double":
                    case "float":
                    case "int":
                    case "unsigned char":
                    case "unsigned int":
                    case "void":
                        Name = name;
                        IsBasic = true;
                        break;
                    case "long int":
                        Name = "long";
                        IsBasic = true;
                        break;
                    case "long long int":
                        Name = "long long";
                        IsBasic = true;
                        break;
                    case "short int":
                        Name = "short";
                        IsBasic = true;
                        break;
                    case "unsigned long int":
                        Name = "unsigned long";
                        IsBasic = true;
                        break;
                    case "unsigned long long int":
                        Name = "unsigned long long";
                        IsBasic = true;
                        break;
                    case "unsigned short int":
                        Name = "unsigned short";
                        IsBasic = true;
                        break;
                    default:
                        Name = name;
                        break;
                }
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
                IsConstantArray = IsConstantArray,
                IsIncomplete = IsIncomplete,
                IsPointer = IsPointer,
                IsReference = IsReference,
                Name = Name,
                Referenced = Referenced != null ? Referenced.Copy() : null,
                TemplateParams = TemplateParams?.Select(p => p.Copy()).ToList(),
                Target = Target
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

            if (t.IsBasic != IsBasic ||
                t.IsConstantArray != IsConstantArray ||
                t.IsPointer != IsPointer ||
                t.IsReference != IsReference)
            {
                return false;
            }

            if (IsPointer || IsReference || IsConstantArray)
            {
                return t.Referenced.Equals(Referenced);
            }

            if (Name == null)
            {
                return t.Name == null;
            }
            return t.Name.Equals(Name);
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
