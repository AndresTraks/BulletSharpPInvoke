using System.IO;
using System.Xml;

namespace BulletSharpGen.Project
{
    class WrapperProjectXmlReader
    {
        public static void Read(WrapperProject project)
        {
            using (var reader = XmlReader.Create(project.ProjectPath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("Project"))
                    {
                        ReadProject(project, reader);
                    }
                }
            }
        }

        static void ReadClassDefinition(WrapperProject project, ClassDefinition @class, XmlReader reader)
        {
            @class.IsExcluded = bool.Parse(reader.GetAttribute("IsExcluded"));
            project.ClassDefinitions.Add(@class.Name, @class);
        }

        static void ReadProject(WrapperProject project, XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch (reader.Name)
                {
                    case "ClassDefinition":
                        {
                            string className = reader.GetAttribute("Name");
                            var @class = new ClassDefinition(className);
                            ReadClassDefinition(project, @class, reader);
                        }
                        break;

                    case "ClassTemplateDefinition":
                        {
                            string className = reader.GetAttribute("Name");
                            var @class = new ClassTemplateDefinition(className);
                            ReadClassDefinition(project, @class, reader);
                        }
                        break;

                    case "NamespaceName":
                        {
                            reader.Read();
                            project.NamespaceName = reader.Value;
                        }
                        break;

                    case "SourceRootFolder":
                        {
                            string sourceRootFolder = reader.GetAttribute("Name");
                            sourceRootFolder = Path.Combine(Path.GetDirectoryName(project.ProjectPath), sourceRootFolder);
                            sourceRootFolder = Path.GetFullPath(sourceRootFolder);
                            project.SourceRootFolders.Add(sourceRootFolder);
                        }
                        break;
                }
            }
        }
    }
}
