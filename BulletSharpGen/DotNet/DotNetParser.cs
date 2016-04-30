using ClangSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    public class DotNetParser : DefaultParser
    {
        // Fully qualified C++ method name -> managed class
        public Dictionary<string, ManagedClass> Classes = new Dictionary<string, ManagedClass>();
        public Dictionary<string, ManagedHeader> Headers = new Dictionary<string, ManagedHeader>();

        public DotNetParser(WrapperProject project)
            : base(project)
        {
        }

        public override void Parse()
        {
            base.Parse();

            CreateManagedHeaders();
            CreateProperties();
            CreateCachedProperties();
        }

        public ManagedHeader GetManaged(HeaderDefinition header)
        {
            return Headers[header.Filename];
        }

        public ManagedClass GetManaged(ClassDefinition @class)
        {
            return Classes[@class.FullyQualifiedName];
        }

        public string GetName(TypeRefDefinition type)
        {
            switch (type.Kind)
            {
                case TypeKind.Typedef:
                    if (type.Referenced.Kind == TypeKind.Pointer &&
                        type.Referenced.Referenced.Kind == TypeKind.FunctionProto)
                    {
                        if (type.Target != null && !type.Target.IsExcluded)
                        {
                            return GetManaged(type.Target).Name;
                        }
                        break;
                    }
                    else if (type.Referenced.IsBasic)
                    {
                        // TODO: C++/CLI: typedef to basic type can return typedef
                        // TODO: Pinvoke: typedef to basic type returns basic type
                        return GetName(type.Referenced);
                    }
                    else
                    {
                        return GetName(type.Referenced);
                    }

                case TypeKind.ConstantArray:
                case TypeKind.LValueReference:
                case TypeKind.Pointer:
                    return GetName(type.Referenced);

                case TypeKind.Enum:
                case TypeKind.Record:
                case TypeKind.Unexposed:
                    if (type.Target != null && !type.Target.IsExcluded)
                    {
                        return GetManaged(type.Target).Name;
                    }
                    break;
            }

            if (type.IsBasic)
            {
                if (type.Name.Equals("unsigned char"))
                {
                    return "byte";
                }
                return type.Name;
            }

            if (type.HasTemplateTypeParameter) return type.Name;

            Console.WriteLine("Unresolved reference to " + type.Name);

            return type.Name;
        }

        string GetManagedClassName(ClassDefinition @class)
        {
            string mapping = Project.ClassNameMapping?.Map(@class.Name);
            if (mapping != null) return mapping;

            return @class.Name;
        }

        string GetManagedHeaderName(HeaderDefinition header)
        {
            string mapping = Project.HeaderNameMapping?.Map(header.Name);
            if (mapping != null) return mapping;

            return header.Name;
        }

        string GetManagedMethodName(MethodDefinition method)
        {
            if (method.IsConstructor)
            {
                var managedParent = GetManagedClass(method.Parent);
                return managedParent.Name;
            }

            string mapping = Project.MethodNameMapping?.Map(method.Name);
            if (mapping != null) return mapping;

            if (method.Name.StartsWith("operator"))
            {
                return method.Name;
            }

            return ToCamelCase(method.Name, true);
        }

        string GetManagedParameterName(ParameterDefinition param)
        {
            string mapping = Project.ParameterNameMapping?.Map(param.Name);
            if (mapping != null) return mapping;

            return ToCamelCase(param.Name, false);
        }

        private ManagedClass GetManagedClass(ClassDefinition @class)
        {
            if (@class == null) return null;

            ManagedClass managedClass;
            if (Classes.TryGetValue(@class.FullyQualifiedName, out managedClass))
            {
                return managedClass;
            }

            if (@class.IsExcluded) return null;

            string managedName = GetManagedClassName(@class);
            var parent = GetManagedClass(@class.Parent);
            managedClass = new ManagedClass(@class, managedName, parent);
            managedClass.BaseClass = GetManagedClass(@class.BaseClass);
            Classes[@class.FullyQualifiedName] = managedClass;

            // Set parent header or class
            managedClass.Header = GetManagedHeader(@class.Header);
            if (parent != null)
            {
                parent.NestedClasses.Add(managedClass);
            }
            else
            {
                managedClass.Header.Classes.Add(managedClass);
            }

            // Managed methods
            foreach (var method in @class.Methods)
            {
                managedName = GetManagedMethodName(method);
                var managedMethod = new ManagedMethod(method, managedClass, managedName);
                foreach (var param in managedMethod.Parameters)
                {
                    param.Name = GetManagedParameterName(param.Native);
                }
            }

            return managedClass;
        }

        private ManagedHeader GetManagedHeader(HeaderDefinition header)
        {
            ManagedHeader managedHeader;
            if (!Headers.TryGetValue(header.Filename, out managedHeader))
            {
                managedHeader = new ManagedHeader(header, GetManagedHeaderName(header));
                Headers[header.Filename] = managedHeader;
            }
            return managedHeader;
        }

        private void CreateManagedHeaders()
        {
            // Create headers only when managed classes are created
            foreach (var @class in Project.ClassDefinitions.Values)
            {
                GetManagedClass(@class);
            }
        }

        private void CreateCachedProperties()
        {
            // Check if any property values can be cached in
            // in constructors or property setters
            foreach (var @class in Classes.Values)
            {
                foreach (var constructor in @class.Methods.Where(m => m.Native.IsConstructor))
                {
                    foreach (var param in constructor.Parameters)
                    {
                        var methodParent = constructor.Parent;
                        while (methodParent != null)
                        {
                            foreach (var property in methodParent.Properties)
                            {
                                if (param.Name.ToLower() == property.Name.ToLower()
                                    && IsCacheableType(param.Native.Type)
                                    && GetName(param.Native.Type).Equals(GetName(property.Type)))
                                {
                                    CachedProperty cachedProperty;
                                    if (methodParent.CachedProperties.TryGetValue(property.Name, out cachedProperty))
                                    {
                                        if (methodParent != constructor.Parent)
                                        {
                                            cachedProperty.Access = RefAccessSpecifier.Protected;
                                        }
                                    }
                                    else
                                    {
                                        cachedProperty = new CachedProperty(property);
                                        methodParent.CachedProperties.Add(property.Name, cachedProperty);
                                    }
                                }
                            }
                            methodParent = methodParent.BaseClass;
                        }
                    }
                }

                foreach (var property in @class.Properties.Where(p => p.Setter != null))
                {
                    if (IsCacheableType(property.Type))
                    {
                        if (!@class.CachedProperties.ContainsKey(property.Name))
                        {
                            @class.CachedProperties.Add(property.Name, new CachedProperty(property));
                        }
                    }
                }
            }
        }

        // Turn getters and setters into properties,
        // managed method names have been resolved at this point
        void CreateProperties()
        {
            foreach (var @class in Classes.Values)
            {
                // Getters with return type and 0 arguments
                var getterMethods = @class.Methods.Where(m => !m.Native.IsConstructor && !m.Native.IsVoid && m.Parameters.Length == 0);
                foreach (var method in getterMethods)
                {
                    if (method.Name.StartsWith("Get") ||
                        (method.Native.ReturnType.Kind == TypeKind.Bool &&
                        _booleanVerbs.Any(v => method.Name.StartsWith(v))))
                    {
                        if (method.Property != null) continue;
                        new ManagedProperty(method, GetPropertyName(method));
                    }
                }

                // Getters with void type and 1 pointer argument for the return value.
                // Can only be done for types with value semantics
                foreach (var method in @class.Methods.Where(m => m.Native.IsVoid && m.Parameters.Length == 1))
                {
                    if (method.Name.StartsWith("Get"))
                    {
                        if (method.Property != null) continue;

                        var paramType = method.Parameters[0].Native.Type.Canonical;
                        if (paramType.IsConst) continue;

                        switch (paramType.Kind)
                        {
                            case TypeKind.Pointer:
                            case TypeKind.LValueReference:
                                var referenced = paramType.Referenced.Canonical;
                                if (referenced.Target != null && referenced.Target.MarshalAsStruct)
                                {
                                    new ManagedProperty(method, GetPropertyName(method));
                                }
                                break;
                        }
                    }
                }

                // Setters
                foreach (var method in @class.Methods)
                {
                    if (method.Parameters.Length == 1 &&
                        method.Name.StartsWith("set", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string name = method.Name.Substring(3);
                        // Find the property with the matching getter
                        foreach (var prop in @class.Properties)
                        {
                            if (prop.Setter != null)
                            {
                                continue;
                            }

                            if (prop.Name.Equals(name))
                            {
                                prop.Setter = method;
                                method.Property = prop;
                                break;
                            }
                        }
                    }
                }
            }
        }

        string[] _booleanVerbs = { "Has", "Is", "Needs" };

        string GetPropertyName(ManagedMethod getter)
        {
            string name = getter.Name;

            var propertyType = getter.Native.IsVoid ? getter.Parameters[0].Native.Type : getter.Native.ReturnType;
            if ("bool".Equals(propertyType.Name) && _booleanVerbs.Any(v => name.StartsWith(v)))
            {
                return name;
            }

            if (name.StartsWith("Get"))
            {
                return name.Substring(3);
            }

            throw new NotImplementedException();
        }

        bool IsCacheableType(TypeRefDefinition t)
        {
            if (t.IsBasic) return false;
            if (t.Target != null)
            {
                if (t.Target is EnumDefinition) return false;
                if (t.Target.MarshalAsStruct) return false;
            }
            if (t.Referenced != null)
            {
                return IsCacheableType(t.Referenced);
            }
            return true;
        }

        protected override void SortMembers()
        {
            base.SortMembers();

            // Sort properties alphabetically
            foreach (var @class in Classes.Values)
            {
                @class.Properties.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
            }
        }
    }
}
