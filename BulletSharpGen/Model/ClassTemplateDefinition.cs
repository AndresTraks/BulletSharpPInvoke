using System.Collections.Generic;

namespace BulletSharpGen
{
    class ClassTemplateDefinition : ClassDefinition
    {
        public List<string> TemplateTypeParameters { get; } = new List<string>();

        public ClassTemplateDefinition(string name, HeaderDefinition header = null, ClassDefinition parent = null)
            : base(name, header, parent)
        {
        }

        public override string ToString()
        {
            string parameters = string.Join(", ", TemplateTypeParameters);
            return $"{Name}<{parameters}>";
        }
    }
}
