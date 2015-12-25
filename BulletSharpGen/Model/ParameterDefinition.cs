namespace BulletSharpGen
{
    public class ParameterDefinition
    {
        public string Name { get; private set; }
        public TypeRefDefinition Type { get; private set; }
        public bool IsOptional { get; set; }
        public string ManagedName { get; set; }

        public ParameterDefinition(string name, TypeRefDefinition type, bool isOptional = false)
        {
            Name = name;
            Type = type;
            IsOptional = isOptional;
        }

        internal ParameterDefinition Copy()
        {
            ParameterDefinition p = new ParameterDefinition(Name, Type, IsOptional);
            p.ManagedName = ManagedName;
            return p;
        }

        public override string ToString()
        {
            return Type.ToString() + ' ' + Name;
        }
    }
}
