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
        /// Class declaration is a forward reference.
        /// </summary>
        public bool IsIncomplete { get; set; }

        public TypeRefDefinition Referenced { get; set; }

        public bool IsConst { get; set; }
        public bool HasTemplateTypeParameter { get; set; }
        public List<TypeRefDefinition> TemplateParams { get; set; }

        private bool _unresolved;
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
                switch (Kind)
                {
                    case TypeKind.Typedef:
                        if (Referenced.Kind == TypeKind.Pointer &&
                            Referenced.Referenced.Kind == TypeKind.FunctionProto)
                        {
                            if (Target != null) return Target.ManagedName;
                            break;
                        }
                        else if (Referenced.IsBasic)
                        {
                            // TODO: C++/CLI: typedef to basic type can return typedef
                            // TODO: Pinvoke: typedef to basic type returns basic type
                            return Referenced.ManagedName;
                        }
                        else
                        {
                            return Referenced.ManagedName;
                        }

                    case TypeKind.ConstantArray:
                    case TypeKind.LValueReference:
                    case TypeKind.Pointer:
                        return Referenced.ManagedName;

                    case TypeKind.Enum:
                    case TypeKind.Record:
                    case TypeKind.Unexposed:
                        if (Target != null) return Target.ManagedName;
                        break;
                }

                if (IsBasic)
                {
                    if (Name.Equals("unsigned char"))
                    {
                        return "byte";
                    }
                    return Name;
                }

                if (HasTemplateTypeParameter) return Name;

                if (!_unresolved)
                {
                    Console.WriteLine("Unresolved reference to " + Name);
                }
                _unresolved = true;
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
                case TypeKind.Unexposed:
                    Name = GetFullyQualifiedName(type, cursor);
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
                    break;
                case "char":
                    Kind = TypeKind.Char_U;
                    Name = "char";
                    break;
                case "double":
                    Kind = TypeKind.Double;
                    Name = "double";
                    break;
                case "float":
                    Kind = TypeKind.Float;
                    Name = "float";
                    break;
                case "int":
                    Kind = TypeKind.Int;
                    Name = "int";
                    break;
                case "unsigned char":
                    Kind = TypeKind.UChar;
                    Name = "unsigned char";
                    break;
                case "unsigned int":
                    Kind = TypeKind.UInt;
                    Name = "unsigned int";
                    break;
                case "void":
                    Kind = TypeKind.Void;
                    Name = name;
                    break;
                case "long int":
                    Kind = TypeKind.Long;
                    Name = "long";
                    break;
                case "long long int":
                    Kind = TypeKind.LongLong;
                    Name = "long long";
                    break;
                case "short int":
                    Kind = TypeKind.Short;
                    Name = "short";
                    break;
                case "unsigned long int":
                    Kind = TypeKind.ULong;
                    Name = "unsigned long";
                    break;
                case "unsigned long long int":
                    Kind = TypeKind.ULongLong;
                    Name = "unsigned long long";
                    break;
                case "unsigned short int":
                    Kind = TypeKind.UShort;
                    Name = "unsigned short";
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

        public static string GetFullyQualifiedName(ClangSharp.Type type, Cursor cursor = null)
        {
            var decl = type.Declaration;
            if (decl.IsInvalid)
            {
                if (cursor.Kind == CursorKind.TypeRef) return cursor.DisplayName;
                if (cursor.Kind == CursorKind.TemplateTypeParameter)
                {
                    return cursor.DisplayName;
                }

                var typeRef = cursor.Children.FirstOrDefault(c => c.Kind == CursorKind.TypeRef);
                if (typeRef != null)
                {
                    return typeRef.DisplayName;
                }

                return "[unexposed type]";
            }

            string name = decl.DisplayName;
            while (decl.SemanticParent.Kind == ClangSharp.CursorKind.ClassDecl ||
                decl.SemanticParent.Kind == ClangSharp.CursorKind.StructDecl ||
                decl.SemanticParent.Kind == ClangSharp.CursorKind.ClassTemplate ||
                decl.SemanticParent.Kind == ClangSharp.CursorKind.Namespace)
            {
                name = decl.SemanticParent.Spelling + "::" + name;
                decl = decl.SemanticParent;
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
