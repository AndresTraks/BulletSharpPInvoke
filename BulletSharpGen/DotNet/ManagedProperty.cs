namespace BulletSharpGen
{
    public class ManagedProperty
    {
        public ManagedMethod Getter { get; }
        public ManagedMethod Setter { get; set; }
        public ManagedClass Parent { get; }
        public string Name { get; }

        public TypeRefDefinition Type
        {
            get
            {
                if (Getter.Native.IsVoid)
                {
                    return Getter.Native.Parameters[0].Type;
                }
                return Getter.Native.ReturnType;
            }
        }

        // Property from getter method
        public ManagedProperty(ManagedMethod getter, string name)
        {
            Getter = getter;
            Name = name;

            Parent = getter.Parent;
            Parent.Properties.Add(this);

            getter.Property = this;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
