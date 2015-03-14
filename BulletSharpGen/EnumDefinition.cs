using System.Collections.Generic;

namespace BulletSharpGen
{
    class EnumDefinition
    {
        public string Name { get; private set; }
        public List<string> EnumConstants { get; private set; }
        public List<string> EnumConstantValues { get; private set; }

        public EnumDefinition(string name)
        {
            Name = name;
            EnumConstants = new List<string>();
            EnumConstantValues = new List<string>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
