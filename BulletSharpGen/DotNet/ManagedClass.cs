using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    public class CachedProperty
    {
        public ManagedProperty Property { get; private set; }
        public string CacheFieldName { get; private set; }
        public RefAccessSpecifier Access { get; set; }

        public CachedProperty(ManagedProperty property, string cacheFieldName = null)
        {
            Property = property;

            if (cacheFieldName != null)
            {
                CacheFieldName = cacheFieldName;
            }
            else
            {
                string name = property.Name;
                CacheFieldName = "_" + char.ToLower(name[0]) + name.Substring(1);
            }

            Access = RefAccessSpecifier.Private;
        }
    }

    public class ManagedClass
    {
        public ClassDefinition Native { get; }

        public string Name { get; set; }

        public ManagedClass BaseClass { get; set; }
        public ManagedClass Parent { get; }
        public ManagedHeader Header { get; set; }

        // members
        public List<ManagedMethod> Methods { get; } = new List<ManagedMethod>();
        public List<ManagedClass> NestedClasses { get; } = new List<ManagedClass>();
        public List<ManagedProperty> Properties { get; } = new List<ManagedProperty>();

        public Dictionary<string, CachedProperty> CachedProperties { get; private set; } = new Dictionary<string, CachedProperty>();

        public IEnumerable<ManagedClass> AllNestedClasses
        {
            get { return NestedClasses.Concat(NestedClasses.SelectMany(c => c.AllNestedClasses)); }
        }

        public ManagedClass(ClassDefinition nativeClass, string managedName, ManagedClass parent)
        {
            Native = nativeClass;
            Name = managedName;
            Parent = parent;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
