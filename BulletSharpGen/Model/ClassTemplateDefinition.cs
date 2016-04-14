using System.Collections.Generic;

namespace BulletSharpGen
{
    class ClassTemplateDefinition : ClassDefinition
    {
        public List<string> TemplateParameters { get; } = new List<string>();

        public ClassTemplateDefinition(string name, HeaderDefinition header = null, ClassDefinition parent = null)
            : base(name, header, parent)
        {
        }

        public override string FullyQualifiedName
        {
            get
            {
                string parameters = string.Join(", ", TemplateParameters);
                return $"{base.FullyQualifiedName}<{parameters}>";
            }
        }

        public override string ToString()
        {
            string parameters = string.Join(", ", TemplateParameters);
            return $"{Name}<{parameters}>";
        }
    }
}
