using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    class CachedProperty
    {
        public PropertyDefinition Property { get; private set; }
        public string CacheFieldName { get; private set; }
        public RefAccessSpecifier Access { get; set; }

        public CachedProperty(PropertyDefinition property, string cacheFieldName = null)
        {
            Property = property;

            if (cacheFieldName != null)
            {
                CacheFieldName = cacheFieldName;
            }
            else
            {
                string name = property.Name;
                CacheFieldName = "_" + name.Substring(0, 1).ToLower() + name.Substring(1);
            }

            Access = RefAccessSpecifier.Private;
        }
    }

    class ClassDefinition
    {
        public string Name { get; private set; }
        public List<ClassDefinition> Classes { get; private set; }
        public ClassDefinition BaseClass { get; set; }
        public ClassDefinition Parent { get; private set; }
        public HeaderDefinition Header { get; private set; }
        public List<MethodDefinition> Methods { get; private set; }
        public List<PropertyDefinition> Properties { get; private set; }
        public List<FieldDefinition> Fields { get; private set; }
        public bool IsAbstract { get; set; }
        public bool IsStruct { get; set; }

        public bool HidePublicConstructors { get; set; }
        public bool NoInternalConstructor { get; set; }
        public bool IsTrackingDisposable { get; set; }
        public bool HasPreventDelete { get; set; }
        public bool IsExcluded { get; set; }

        // For function prototypes IsTypeDef == true, but TypedefUnderlyingType == null
        public bool IsTypedef { get; set; }
        public TypeRefDefinition TypedefUnderlyingType { get; set; }

        // Pure enum = enum wrapped in a struct
        public EnumDefinition Enum { get; set; }
        public bool IsPureEnum
        {
            get { return Enum != null && Methods.Count == 0; }
        }

        // static class contains only static methods
        public bool IsStaticClass
        {
            get { return Methods.Count != 0 && Methods.All(x => x.IsStatic); }
        }

        public bool HasCppDefaultConstructor
        {
            get { return !HidePublicConstructors && Methods.Count(m => m.IsConstructor) == 0; }
        }

        public string ManagedName { get; set; }

        public IEnumerable<ClassDefinition> AllSubClasses
        {
            get
            {
                List<ClassDefinition> subClasses = new List<ClassDefinition>();
                foreach (ClassDefinition cl in Classes)
                {
                    subClasses.AddRange(cl.AllSubClasses);
                    subClasses.Add(cl);
                }
                return subClasses;
            }
        }

        public string FullName
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.FullName + "::" + Name;
                }
                return Name;
            }
        }

        public string FullNameCS
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.FullNameCS + '_' + Name;
                }
                return Name;
            }
        }

        public string FullNameManaged
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.FullNameManaged + "::" + ManagedName;
                }
                return ManagedName;
            }
        }

        public Dictionary<string, CachedProperty> CachedProperties { get; private set; }

        public ClassDefinition(string name, HeaderDefinition header, ClassDefinition parent = null)
        {
            Name = name;
            Header = header;
            Parent = parent;

            Classes = new List<ClassDefinition>();
            Methods = new List<MethodDefinition>();
            Properties = new List<PropertyDefinition>();
            Fields = new List<FieldDefinition>();
            CachedProperties = new Dictionary<string, CachedProperty>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
