using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    class DefaultParser
    {
        public WrapperProject Project { get; private set; }

        public DefaultParser(WrapperProject project)
        {
            Project = project;

            MarkAbstractClasses();
        }

        // n = 2 -> "\t\t"
        protected static string GetTabs(int n)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                builder.Append('\t');
            }
            return builder.ToString();
        }

        // one_two_three -> oneTwoThree
        // ONE_TWO_THREE -> OneTwoThree
        protected static string RemoveUnderscores(string text)
        {
            if (text.Length == 0)
            {
                return text;
            }

            string outText = text;
            while (outText.Contains("_"))
            {
                int pos = outText.IndexOf('_');
                string left = outText.Substring(0, pos).ToLower();
                char firstLetterOfRight = char.ToUpper(outText[pos + 1]);
                string right = outText.Substring(pos + 2);
                outText = left + firstLetterOfRight + right;
            }

            if (char.IsUpper(text, 0))
            {
                outText = char.ToUpper(outText[0]) + outText.Substring(1);
            }

            return outText;
        }

        // ONE_TWO_THREE -> OneTwoThree
        protected static string RemoveUnderscoresFromUpper(string text)
        {
            if (text.Length == 0)
            {
                return text;
            }

            StringBuilder outText = new StringBuilder();
            int left = 0, right;

            while (true)
            {
                right = text.IndexOf('_', left);
                if (right == -1)
                {
                    outText.Append(char.ToUpper(text[left]));
                    outText.Append(text.Substring(left + 1).ToLower());
                    break;
                }
                outText.Append(char.ToUpper(text[left]));
                left++;
                outText.Append(text.Substring(left, right - left).ToLower());
                left = right + 1;
            }

            return outText.ToString();
        }

        void MarkAbstractClasses()
        {
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                @class.IsAbstract = @class.AbstractMethods.Count() != 0;
            }
        }
    }
}
