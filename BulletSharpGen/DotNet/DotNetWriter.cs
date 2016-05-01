namespace BulletSharpGen
{
    public abstract class DotNetWriter : WrapperWriter
    {
        public DotNetParser DotNetParser { get; }

        protected DotNetWriter(DotNetParser parser) : base(parser)
        {
            DotNetParser = parser;
        }

        protected string GetName(TypeRefDefinition type)
        {
            return DotNetParser.GetName(type);
        }

        protected bool IsExcludedClass(ManagedClass @class)
        {
            return IsExcludedClass(@class.Native);
        }
    }
}
