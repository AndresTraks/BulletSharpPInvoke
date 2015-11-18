using System.Collections.Generic;

namespace BulletSharpGen
{
    class ClassTemplateDefinition : ClassDefinition
    {
        public List<string> TemplateTypeParameters { get; set; }

        public ClassTemplateDefinition(string name, HeaderDefinition header, ClassDefinition parent = null)
            : base(name, header, parent)
        {
        }
    }
}
