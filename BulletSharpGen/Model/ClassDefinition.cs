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
        public string Name { get; set; }
        public string NamespaceName { get; set; } = "";

        public ClassDefinition BaseClass { get; set; }
        public ClassDefinition Parent { get; set; }
        public HeaderDefinition Header { get; }

        // members
        public List<ClassDefinition> NestedClasses { get; } = new List<ClassDefinition>();
        public List<MethodDefinition> Methods { get; } = new List<MethodDefinition>();
        public List<FieldDefinition> Fields { get; } = new List<FieldDefinition>();
        public List<PropertyDefinition> Properties { get; } = new List<PropertyDefinition>();

        public bool IsAbstract { get; set; }
        public bool IsStruct { get; set; }
        public bool IsFunctionProto { get; set; }

        public bool IsParsed { get; set; }

        public bool HidePublicConstructors { get; set; }
        public bool NoInternalConstructor { get; set; }
        public bool IsTrackingDisposable { get; set; }

        /// <summary>
        /// If true, the native memory allocated for this class
        /// may or may not be freed by the wrapper class depending
        /// on the value of the additional _preventDelete variable.
        /// If false, the native memory is always freed (default).
        /// </summary>
        public bool HasPreventDelete { get; set; }

        public bool IsExcluded { get; set; }

        public string ManagedName { get; set; }

        /// <summary>
        /// If true, the wrapper class is itself a struct instead
        /// of being a class with a pointer to a native struct.
        /// </summary>
        public bool MarshalAsStruct { get; set; }

        public Dictionary<string, CachedProperty> CachedProperties { get; private set; } = new Dictionary<string, CachedProperty>();

        public IEnumerable<MethodDefinition> AbstractMethods
        {
            get
            {
                var abstractMethods = Methods.Where(m => m.IsAbstract);
                if (BaseClass == null) return abstractMethods;

                // Abstract methods from base classes that aren't implemented in this class
                var baseAbstractMethods = BaseClass.AbstractMethods.Where(am => !Methods.Any(m => m.Equals(am)));

                return abstractMethods.Concat(baseAbstractMethods);
            }
        }

        // Pure enum = enum wrapped in a struct
        public bool IsPureEnum
        {
            get
            {
                return Methods.Count == 0 &&
                    Fields.Count == 0 &&
                    NestedClasses.Count == 1 &&
                    NestedClasses.First() is EnumDefinition;
            }
        }

        // static class contains only static methods
        public bool IsStaticClass
        {
            get { return Methods.Count != 0 && Methods.All(x => x.IsStatic); }
        }

        public IEnumerable<ClassDefinition> AllNestedClasses
        {
            get { return NestedClasses.Concat(NestedClasses.SelectMany(c => c.AllNestedClasses)); }
        }

        public virtual string FullyQualifiedName
        {
            get
            {
                if (Parent != null)
                {
                    return $"{Parent.FullyQualifiedName}::{Name}";
                }
                if (NamespaceName != "")
                {
                    return $"{NamespaceName}::{Name}";
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
                    return $"{Parent.FullName}::{Name}";
                }
                return Name;
            }
        }

        public ClassDefinition(string name, HeaderDefinition header = null, ClassDefinition parent = null)
        {
            Name = name;
            Header = header;
            Parent = parent;
        }

        public override string ToString()
        {
            return ManagedName ?? FullyQualifiedName;
        }
    }
}
