using ClangSharp;
using System.Collections.Generic;

namespace BulletSharpGen
{
    public class MethodDefinition
    {
        public string Name { get; }
        public ClassDefinition Parent { get; }
        public TypeRefDefinition ReturnType { get; set; }
        public ParameterDefinition[] Parameters { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsConstructor { get; set; }
        public bool IsDestructor { get; set; }
        public bool IsVirtual { get; set; }
        public FieldDefinition Field { get; set; } // get/set method target
        public AccessSpecifier Access { get; set; } = AccessSpecifier.Public;

        public bool IsParsed { get; set; }
        public ParameterDefinition OutValueParameter { get; set; }
        public string BodyText { get; set; }

        public bool IsVoid
        {
            get { return ReturnType != null && ReturnType.Kind == TypeKind.Void; }
        }

        public int NumOptionalParameters
        {
            get
            {
                int count = 0;
                for (int i = Parameters.Length - 1; i >= 0; i--)
                {
                    if (!Parameters[i].IsOptional)
                    {
                        break;
                    }
                    count++;
                }
                return count;
            }
        }

        public MethodDefinition(string name, ClassDefinition parent, int numArgs)
        {
            Name = name;
            Parent = parent;
            Parameters = new ParameterDefinition[numArgs];

            if (parent != null)
            {
                parent.Methods.Add(this);
            }
        }

        public MethodDefinition Copy(ClassDefinition parent = null)
        {
            var m = new MethodDefinition(Name, parent ?? Parent, Parameters.Length)
            {
                Access = Access,
                Field = Field,
                IsAbstract = IsAbstract,
                IsConstructor = IsConstructor,
                IsExcluded = IsExcluded,
                IsStatic = IsStatic,
                OutValueParameter = OutValueParameter?.Copy(),
                ReturnType = ReturnType.Copy()
            };
            for (int i = 0; i < Parameters.Length; i++)
            {
                m.Parameters[i] = Parameters[i].Copy();
            }
            return m;
        }

        public override bool Equals(object obj)
        {
            var m = obj as MethodDefinition;
            if (m == null)
            {
                return false;
            }

            if (!m.Name.Equals(Name) || m.Parameters.Length != Parameters.Length)
            {
                return false;
            }

            if (!m.ReturnType.Equals(ReturnType))
            {
                return false;
            }

            for (int i = 0; i < Parameters.Length; i++)
            {
                // Parameter names can vary, but types must match
                if (!m.Parameters[i].Type.Equals(Parameters[i].Type))
                {
                    return false;
                }
            }

            if (m.OutValueParameter != null && OutValueParameter != null &&
                !m.OutValueParameter.Type.Equals(OutValueParameter.Type))
            {
                return false;
            }

            return m.IsStatic == IsStatic;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
