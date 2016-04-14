using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    class EnumConstant
    {
        public string Constant { get; set; }
        public string Value { get; set; }

        public EnumConstant(string constant, string value)
        {
            Constant = constant;
            Value = value;
        }

        public override string ToString()
        {
            if (Value == "") return Constant;
            return $"{Constant} = {Value}";
        }
    }

    class EnumDefinition : ClassDefinition
    {
        public List<EnumConstant> EnumConstants { get; } = new List<EnumConstant>();

        public bool IsFlags { get; set; }

        public EnumDefinition(string name, HeaderDefinition header = null, ClassDefinition parent = null)
            : base(name, header, parent)
        {
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
                char c = EnumConstants[0].Constant[i];
                if (EnumConstants.Any(e => e.Constant[i] != c))
                {
                    break;
                }
                if (EnumConstants.Any(e => e.Constant.Length == i))
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
                string enumConstant = EnumConstants[0].Constant;
                char c = enumConstant[enumConstant.Length - i];
                if (EnumConstants.Any(e => e.Constant[e.Constant.Length - i] != c))
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
