using System.Linq;

namespace BulletSharpGen
{
    public class ManagedMethod
    {
        public MethodDefinition Native { get; }

        public string Name { get; }
        public ManagedClass Parent { get; }
        public ManagedParameter[] Parameters { get; set; }
        public ManagedProperty Property { get; set; } // property that wraps this get/set method

        public ManagedParameter OutValueParameter { get; set; }

        public ManagedMethod(MethodDefinition nativeMethod, ManagedClass parent, string name)
        {
            Native = nativeMethod;
            Parent = parent;
            Name = name;

            Parameters = nativeMethod.Parameters.Select(p => new ManagedParameter(p)).ToArray();
            if (nativeMethod.OutValueParameter != null)
            {
                OutValueParameter = new ManagedParameter(nativeMethod.OutValueParameter);
            }

            if (parent != null)
            {
                parent.Methods.Add(this);
            }
        }

        public ManagedMethod Copy(ManagedClass parent)
        {
            var m = new ManagedMethod(Native, parent, Name)
            {
                Property = Property
            };
            //TODO: params
            return m;
        }

        public override string ToString()
        {
            string parameters = string.Join(", ", Parameters.Select(p => p.Name));
            return $"{Name}({parameters})";
        }
    }
}
