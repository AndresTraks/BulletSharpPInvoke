namespace BulletSharpGen
{
    public abstract class DotNetWriter : WrapperWriter
    {
        protected DotNetWriter(WrapperProject project) : base(project)
        {
        }

        protected bool IsExcludedClass(ClassDefinition c)
        {
            return c.IsPureEnum || c.IsExcluded ||
                c is ClassTemplateDefinition || c is EnumDefinition;
        }
    }
}
