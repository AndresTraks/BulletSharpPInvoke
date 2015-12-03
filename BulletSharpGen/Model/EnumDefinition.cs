using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    class EnumDefinition
    {
        public string FullName { get; private set; }
        public string Name { get; private set; }
        public List<string> EnumConstants { get; private set; }
        public List<string> EnumConstantValues { get; private set; }

        public string ManagedName { get; set; }

        public EnumDefinition(string fullName, string name)
        {
            FullName = fullName;
            Name = name;
            EnumConstants = new List<string>();
            EnumConstantValues = new List<string>();
        }

        public string GetCommonPrefix()
        {
            if (EnumConstants.Count < 2)
            {
                return "";
            }

            string prefix = "";
            int i = 0;
            while (true)
            {
                char c = EnumConstants[0][i];
                if (!EnumConstants.All(e => e[i] == c))
                {
                    break;
                }
                if (EnumConstants.Any(e => e.Length == i))
                {
                    // Prefix is already one of the enumerators,
                    // can't use this prefix
                    return "";
                }
                i++;
                prefix += c;
            }
            return prefix;
        }

        public IList<string> GetManagedConstants()
        {
            int prefixLength = GetCommonPrefix().Length;
            List<string> enumConstants = new List<string>(EnumConstants.Count);
            foreach (var c in EnumConstants)
            {
                enumConstants.Add(c.Substring(prefixLength));
            }
            return enumConstants;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
