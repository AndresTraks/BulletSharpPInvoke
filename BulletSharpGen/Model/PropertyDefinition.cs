using System;
namespace BulletSharpGen
{
    public class PropertyDefinition
    {
        public MethodDefinition Getter { get; private set; }
        public MethodDefinition Setter { get; set; }
        public ClassDefinition Parent { get; private set; }
        public string Name { get; private set; }

        public TypeRefDefinition Type
        {
            get
            {
                if (Getter.IsVoid)
                {
                    return Getter.Parameters[0].Type;
                }
                return Getter.ReturnType;
            }
        }

        // Property from getter method
        public PropertyDefinition(MethodDefinition getter, string name)
        {
            Getter = getter;
            Parent = getter.Parent;
            Parent.Properties.Add(this);
            getter.Property = this;

            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
