namespace BulletSharpGen
{
    public class ManagedParameter
    {
        public ParameterDefinition Native { get; }

        public string Name { get; set; }

        public ManagedParameter(ParameterDefinition nativeParam)
        {
            Native = nativeParam;
            Name = nativeParam.Name;
        }
    }
}
