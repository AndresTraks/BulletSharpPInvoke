namespace BulletSharpGen
{
    public enum MarshalDirection
    {
        /// <summary>
        /// The parameter is marshaled to the callee,
        /// but not back to the caller.
        /// Default for a basic type or a const pointer/reference.
        /// </summary>
        In,

        /// <summary>
        /// The parameter is marshaled back to the caller,
        /// but not to the callee.
        /// Can be explicitly specified for a non-const pointer/reference.
        /// </summary>
        Out,

        /// <summary>
        /// The parameter is marshaled to the callee and also back to the caller.
        /// Default for a non-const pointer/reference.
        /// </summary>
        InOut
    }

    public class ParameterDefinition
    {
        public string Name { get; }
        public TypeRefDefinition Type { get; }
        public bool IsOptional { get; set; }
        public string ManagedName { get; set; }
        public MarshalDirection MarshalDirection { get; set; }

        public ParameterDefinition(string name, TypeRefDefinition type, bool isOptional = false)
        {
            Name = name;
            Type = type;
            IsOptional = isOptional;
        }

        internal ParameterDefinition Copy()
        {
            var p = new ParameterDefinition(Name, Type, IsOptional);
            p.ManagedName = ManagedName;
            return p;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Type, Name);
        }
    }
}
