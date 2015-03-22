using System.Collections.Generic;

namespace BulletSharpGen
{
    class EnumDefinition
    {
        public string FullName { get; private set; }
        public string Name { get; private set; }
        public List<string> EnumConstants { get; private set; }
        public List<string> EnumConstantValues { get; private set; }

        public EnumDefinition(string fullName, string name)
        {
            FullName = fullName;
            Name = name;
            EnumConstants = new List<string>();
            EnumConstantValues = new List<string>();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
