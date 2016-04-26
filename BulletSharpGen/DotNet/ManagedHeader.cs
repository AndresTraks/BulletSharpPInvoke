using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    public class ManagedHeader
    {
        public HeaderDefinition Native { get; }

        public string Name { get; set; }
        public List<ManagedClass> Classes { get; } = new List<ManagedClass>();

        public IEnumerable<ManagedClass> AllClasses
        {
            get { return Classes.Concat(Classes.SelectMany(c => c.AllNestedClasses)); }
        }

        public ManagedHeader(HeaderDefinition nativeHeader, string managedName)
        {
            Native = nativeHeader;
            Name = managedName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
