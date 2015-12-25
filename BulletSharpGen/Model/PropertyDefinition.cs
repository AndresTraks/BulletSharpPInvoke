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
        public PropertyDefinition(MethodDefinition getter)
        {
            Getter = getter;
            Parent = getter.Parent;
            Parent.Properties.Add(this);
            getter.Property = this;

            string name = getter.Name;
            
            // one_two_three -> oneTwoThree
            while (name.Contains("_"))
            {
                int pos = name.IndexOf('_');
                name = name.Substring(0, pos) + name.Substring(pos + 1, 1).ToUpper() + name.Substring(pos + 2);
            }

            if (name.StartsWith("get", StringComparison.InvariantCultureIgnoreCase))
            {
                Name = name.Substring(3);
            }
            else if (name.StartsWith("is", StringComparison.InvariantCultureIgnoreCase))
            {
                Name = name[0].ToString().ToUpper() + name.Substring(1);
            }
            else if (name.StartsWith("has", StringComparison.InvariantCultureIgnoreCase))
            {
                Name = name[0].ToString().ToUpper() + name.Substring(1);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
