namespace BulletSharpGen
{
    public class FieldDefinition
    {
        public string Name { get; set; }
        public TypeRefDefinition Type { get; private set; }

        public MethodDefinition Getter { get; set; }
        public MethodDefinition Setter { get; set; }

        public FieldDefinition(string name, TypeRefDefinition type, ClassDefinition parent)
        {
            Name = name;
            Type = type;

            parent.Fields.Add(this);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
