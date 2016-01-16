using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    public enum RefAccessSpecifier
    {
        Public,
        Protected,
        Private,
        Internal
    }

    public class CachedProperty
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
                CacheFieldName = "_" + char.ToLower(name[0]) + name.Substring(1);
            }

            Access = RefAccessSpecifier.Private;
        }
    }

    public class ClassDefinition
    {
        public string Name { get; private set; }
        public string NamespaceName { get; set; }
        public List<ClassDefinition> Classes { get; private set; }
        public ClassDefinition BaseClass { get; set; }
        public ClassDefinition Parent { get; set; }
        public HeaderDefinition Header { get; private set; }
        public List<MethodDefinition> Methods { get; private set; }
        public List<PropertyDefinition> Properties { get; private set; }
        public List<FieldDefinition> Fields { get; private set; }
        public bool IsAbstract { get; set; }
        public bool IsStruct { get; set; }

        public bool IsParsed { get; set; }

        public bool HidePublicConstructors { get; set; }
        public bool NoInternalConstructor { get; set; }
        public bool IsTrackingDisposable { get; set; }
        public bool HasPreventDelete { get; set; }
        public bool IsExcluded { get; set; }

        // For function prototypes IsTypeDef == true, but TypedefUnderlyingType == null
        public bool IsTypedef { get; set; }
        public TypeRefDefinition TypedefUnderlyingType { get; set; }

        public string ManagedName { get; set; }

        public Dictionary<string, CachedProperty> CachedProperties { get; private set; }

        public IEnumerable<MethodDefinition> AbstractMethods
        {
            get
            {
                var abstractMethods = Methods.Where(m => m.IsAbstract);
                if (BaseClass == null)
                {
                    return abstractMethods;
                }

                // Abstract methods from base classes that aren't implemented in this class
                var baseAbstractMethods = BaseClass.AbstractMethods.Where(am => !Methods.Any(m => m.Equals(am)));

                return abstractMethods.Concat(baseAbstractMethods);
            }
        }

        // Pure enum = enum wrapped in a struct
        public bool IsPureEnum
        {
            get { return this is EnumDefinition && Methods.Count == 0; }
        }

        // static class contains only static methods
        public bool IsStaticClass
        {
            get { return Methods.Count != 0 && Methods.All(x => x.IsStatic); }
        }

        public IEnumerable<ClassDefinition> AllSubClasses
        {
            get { return Classes.Concat(Classes.SelectMany(c => c.AllSubClasses)); }
        }

        public string FullyQualifiedName
        {
            get
            {
                if (Parent != null)
                {
                    return string.Format("{0}::{1}", Parent.FullyQualifiedName, Name);
                }
                if (NamespaceName != "")
                {
                    return string.Format("{0}::{1}", NamespaceName, Name);
                }
                return Name;
            }
        }

        public string FullName
        {
            get
            {
                if (Parent != null)
                {
                    return string.Format("{0}::{1}", Parent.FullName, Name);
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

        public ClassDefinition(string name, HeaderDefinition header = null, ClassDefinition parent = null)
        {
            Name = name;
            Header = header;
            Parent = parent;

            NamespaceName = "";

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
