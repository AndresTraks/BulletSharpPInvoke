using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    class EnumDefinition : ClassDefinition
    {
        public List<string> EnumConstants { get; private set; }
        public List<string> EnumConstantValues { get; private set; }

        public bool IsFlags { get; set; }

        public EnumDefinition(string name, HeaderDefinition header = null, ClassDefinition parent = null)
            : base(name, header, parent)
        {
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

        public string GetCommonSuffix()
        {
            if (EnumConstants.Count < 2)
            {
                return "";
            }

            string suffix = "";
            int i = 1;
            while (true)
            {
                string enumConstant = EnumConstants[0];
                char c = enumConstant[enumConstant.Length - i];
                if (!EnumConstants.All(e => e[e.Length - i] == c))
                {
                    break;
                }
                i++;
                suffix = c + suffix;
            }
            return suffix;
        }

        public override string ToString()
        {
            return FullyQualifiedName;
        }
    }
}
