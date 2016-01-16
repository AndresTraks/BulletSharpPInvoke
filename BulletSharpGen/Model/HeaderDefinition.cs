using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    public class HeaderDefinition
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public List<ClassDefinition> Classes { get; set; }
        public List<HeaderDefinition> Includes { get; set; }
        public bool IsExcluded { get; set; }

        public string ManagedName { get; set; }

        public IEnumerable<ClassDefinition> AllClasses
        {
            get { return Classes.Concat(Classes.SelectMany(c => c.AllSubClasses)); }
        }

        public HeaderDefinition(string filename)
        {
            Name = Path.GetFileNameWithoutExtension(filename);
            Filename = filename;
            Classes = new List<ClassDefinition>();
            Includes = new List<HeaderDefinition>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
