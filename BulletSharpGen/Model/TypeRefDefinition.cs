using ClangSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    public class TypeRefDefinition
    {
        public string Name { get; set; }

        public TypeKind Kind { get; set; }

        /// <summary>
        /// Class declaration is a forward reference.
        /// </summary>
        public bool IsIncomplete { get; set; }

        public TypeRefDefinition Referenced { get; set; }

        public bool IsConst { get; set; }
        public bool HasTemplateTypeParameter { get; set; }
        public List<TypeRefDefinition> TemplateParams { get; set; }

        public ClassDefinition Target { get; set; }

        public bool IsBasic
        {
            get
            {
                switch (Kind)
                {
                    case TypeKind.Bool:
                    case TypeKind.Char_S:
                    case TypeKind.Double:
                    case TypeKind.Enum:
                    case TypeKind.Float:
                    case TypeKind.Int:
                    case TypeKind.Long:
                    case TypeKind.LongLong:
                    case TypeKind.SChar:
                    case TypeKind.Short:
                    case TypeKind.UChar:
                    case TypeKind.UInt:
                    case TypeKind.ULong:
                    case TypeKind.ULongLong:
                    case TypeKind.UShort:
                    case TypeKind.Void:
                        return true;
                    case TypeKind.Unexposed:
                        if (Target is EnumDefinition) return true;
                        return false;
                    default:
                        return false;
                }
            }
        }

        public TypeRefDefinition Canonical
        {
            get { return Kind == TypeKind.Typedef ? Referenced : this; }
        }

        public bool ConstCanonical
        {
            get
            {
                if (IsConst) return true;
                return (Referenced != null && Referenced.IsConst);
            }
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

        public TypeRefDefinition(ClangSharp.Type type, Cursor cursor = null)
        {
            Kind = (TypeKind)type.TypeKind;

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
                Cursor pointeeCursor = cursor?.Children.FirstOrDefault(c => c.Type.Equals(type.Pointee));
                if (pointeeCursor == null)
                {
                    pointeeCursor = cursor?.Children.FirstOrDefault(c => c.Kind == CursorKind.TypeRef)?.Referenced;
                }
                Referenced = new TypeRefDefinition(type.Pointee, pointeeCursor);
            }
            else if (Kind != TypeKind.Typedef)
            {
                if (cursor != null && (Kind == TypeKind.Record || Kind == TypeKind.Unexposed))
                {
                    if (cursor.Kind == CursorKind.TypeRef)
                    {
                        if (cursor.Referenced.Kind == CursorKind.TemplateTypeParameter)
                        {
                            HasTemplateTypeParameter = true;
                        }
                    }
                    else if (cursor.Kind == CursorKind.CxxMethod)
                    {
                        var children = cursor.Children.TakeWhile(c => c.Kind != CursorKind.ParmDecl);
                        var typeRef = children.FirstOrDefault();
                        if (typeRef != null && typeRef.Kind == CursorKind.TypeRef)
                        {
                            if (typeRef.Referenced.Kind == CursorKind.TemplateTypeParameter)
                            {
                                HasTemplateTypeParameter = true;
                            }
                        }
                    }
                    else if (cursor.Kind == CursorKind.ParmDecl)
                    {
                        var typeRef = cursor.Children.FirstOrDefault();
                        if (typeRef != null && typeRef.Kind == CursorKind.TypeRef)
                        {
                            if (typeRef.Referenced.Kind == CursorKind.TemplateTypeParameter)
                            {
                                HasTemplateTypeParameter = true;
                            }
                        }
                    }
                }

                // Capture template parameters
                var firstChild = cursor?.Children.FirstOrDefault();
                if (firstChild != null && firstChild.Kind == CursorKind.TemplateRef)
                {
                    if (cursor.Children.Count == 1)
                    {
                        string displayName = GetFullyQualifiedName(type, cursor);
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
                    break;
                case TypeKind.Long:
                    Name = "long";
                    break;
                case TypeKind.LongLong:
                    Name = "long long";
                    break;
                case TypeKind.Short:
                    Name = "short";
                    break;
                case TypeKind.ULong:
                    Name = "unsigned long";
                    break;
                case TypeKind.ULongLong:
                    Name = "unsigned long long";
                    break;
                case TypeKind.UShort:
                    Name = "unsigned short";
                    break;

                case TypeKind.Typedef:
                    Name = GetFullyQualifiedName(type);
                    Referenced = new TypeRefDefinition(type.Canonical, cursor?.Referenced);
                    break;

                case TypeKind.FunctionProto:
                case TypeKind.LValueReference:
                case TypeKind.Pointer:
                    break;
                case TypeKind.ConstantArray:
                    Referenced = new TypeRefDefinition(type.ArrayElementType, cursor);
                    break;

                case TypeKind.Enum:
                    Name = type.Canonical.Declaration.Spelling;
                    break;
                case TypeKind.Record:
                    Name = GetFullyQualifiedName(type, cursor);
                    break;
                case TypeKind.Unexposed:
                    if (type.Canonical.TypeKind != ClangSharp.Type.Kind.Unexposed)
                    {
                        Kind = (TypeKind)type.Canonical.TypeKind;
                        Name = GetFullyQualifiedName(type.Canonical, cursor);
                    }
                    else
                    {
                        Name = GetFullyQualifiedName(type, cursor);
                    }
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
                    break;
                case "char":
                    Kind = TypeKind.Char_U;
                    break;
                case "double":
                    Kind = TypeKind.Double;
                    break;
                case "float":
                    Kind = TypeKind.Float;
                    break;
                case "int":
                    Kind = TypeKind.Int;
                    break;
                case "unsigned char":
                    Kind = TypeKind.UChar;
                    break;
                case "unsigned int":
                    Kind = TypeKind.UInt;
                    break;
                case "void":
                    Kind = TypeKind.Void;
                    break;
                case "long":
                case "long int":
                    Kind = TypeKind.Long;
                    break;
                case "long long":
                case "long long int":
                    Kind = TypeKind.LongLong;
                    break;
                case "short":
                case "short int":
                    Kind = TypeKind.Short;
                    break;
                case "unsigned long":
                case "unsigned long int":
                    Kind = TypeKind.ULong;
                    break;
                case "unsigned long long":
                case "unsigned long long int":
                    Kind = TypeKind.ULongLong;
                    break;
                case "unsigned short":
                case "unsigned short int":
                    Kind = TypeKind.UShort;
                    break;
            }

            if (IsBasic)
            {
                Name = GetBasicName(name);
            }
            else if (name.EndsWith(" *"))
            {
                Referenced = new TypeRefDefinition(name.Substring(0, name.Length - 2));
                Kind = TypeKind.Pointer;
            }
            else
            {
                Name = name;
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

        public static string GetBasicName(string name)
        {
            switch (name)
            {
                case "long int":
                    return "long";
                case "long long int":
                    return "long long";
                case "short int":
                    return "short";
                case "unsigned long int":
                    return "unsigned long";
                case "unsigned long long int":
                    return "unsigned long long";
                case "unsigned short int":
                    return "unsigned short";
                default:
                    return name;
            }
        }

        public static string GetFullyQualifiedName(ClangSharp.Type type, Cursor cursor = null)
        {
            var decl = type.Declaration;
            if (!decl.IsInvalid) return GetFullyQualifiedName(decl);

            return GetFullyQualifiedName(cursor);
        }

        public static string GetFullyQualifiedName(Cursor cursor)
        {
            string name;
            switch (cursor.Kind)
            {
                case CursorKind.CxxMethod:
                case CursorKind.FieldDecl:
                case CursorKind.ParmDecl:
                    var typeRef = cursor.Children.FirstOrDefault(c => c.Kind == CursorKind.TypeRef);
                    if (typeRef != null) return GetFullyQualifiedName(typeRef);
                    return cursor.DisplayName;
                case CursorKind.TemplateTypeParameter:
                case CursorKind.TypeRef:
                    return cursor.DisplayName;
                default:
                    name = cursor.DisplayName;
                    break;
            }

            switch (cursor.SemanticParent.Kind)
            {
                case CursorKind.ClassDecl:
                case CursorKind.StructDecl:
                case CursorKind.ClassTemplate:
                case CursorKind.Namespace:
                    return GetFullyQualifiedName(cursor.SemanticParent) + "::" + name;
            }

            return name;
        }

        public MarshalDirection GetDefaultMarshalDirection()
        {
            if (ConstCanonical) return MarshalDirection.In;
            return MarshalDirection.InOut;
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
            if (Name == null)
            {
                switch (Kind)
                {
                    case TypeKind.LValueReference:
                        return $"{Referenced}&";
                    case TypeKind.Pointer:
                        return $"{Referenced}*";
                }
            }
            return Name;
        }
    }
}
