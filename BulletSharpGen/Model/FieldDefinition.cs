namespace BulletSharpGen
{
    public class FieldDefinition
    {
        public string Name { get; private set; }
        public TypeRefDefinition Type { get; private set; }
        public ClassDefinition Parent { get; private set; }

        public FieldDefinition(string name, TypeRefDefinition type, ClassDefinition parent)
        {
            Name = name;
            Type = type;
            Parent = parent;

            parent.Fields.Add(this);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
