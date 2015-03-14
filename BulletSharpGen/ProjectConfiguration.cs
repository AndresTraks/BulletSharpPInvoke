namespace BulletSharpGen
{
    class ProjectConfiguration
    {
        public ProjectConfiguration(string name, bool isDebug, string definitions, string usingDirectories = null)
        {
            Name = name;
            IsDebug = isDebug;
            Definitions = definitions;
            UsingDirectories = usingDirectories;
        }

        public string Name { get; set; }
        public bool IsDebug { get; set; }
        public string Definitions { get; set; }
        public string UsingDirectories { get; set; }
        public string ConditionalRef { get; set; }
        public string ConditionalRefAssembly { get; set; }
    }
}
