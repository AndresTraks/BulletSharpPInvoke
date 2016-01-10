using System;
using System.Collections.Generic;
using System.IO;

namespace BulletSharpGen
{
    class WrapperProject
    {
        public string ProjectPath { get; set; }

        public string NamespaceName { get; set; }
        public Dictionary<string, ClassDefinition> ClassDefinitions = new Dictionary<string, ClassDefinition>();
        public Dictionary<string, HeaderDefinition> HeaderDefinitions = new Dictionary<string, HeaderDefinition>();

        public ISymbolMapping ClassNameMapping { get; set; }
        public ISymbolMapping HeaderNameMapping { get; set; }
        public ISymbolMapping MethodNameMapping { get; set; }
        public ISymbolMapping ParameterNameMapping { get; set; }

        public List<string> SourceRootFolders { get; set; }

        public WrapperProject()
        {
            SourceRootFolders = new List<string>();
        }

        public bool VerifyFiles()
        {
            foreach (string sourceFolder in SourceRootFolders)
            {
                if (!Directory.Exists(sourceFolder))
                {
                    Console.WriteLine("Source folder \"" + sourceFolder + "\" not found");
                    return false;
                }
            }
            return true;
        }

        public static WrapperProject FromFile(string filename)
        {
            var project = new WrapperProject();
            project.ProjectPath = Path.GetFullPath(filename);

            Project.WrapperProjectXmlReader.Read(project);

            return project;
        }

        public void Save()
        {
            Project.WrapperProjectXmlWriter.Write(this);
        }
    }
}
