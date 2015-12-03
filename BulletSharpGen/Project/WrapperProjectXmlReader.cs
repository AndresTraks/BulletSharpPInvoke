using System;
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
            string isExcluded = reader.GetAttribute("IsExcluded");

            string namespaceName = reader.GetAttribute("Namespace");
            if (namespaceName != null)
            {
                @class.NamespaceName = namespaceName;
            }

            if ("True".Equals(isExcluded))
            {
                @class.IsExcluded = true;
            }
            project.ClassDefinitions.Add(@class.FullyQualifiedName, @class);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }
            }

        }

        static void ReadProject(WrapperProject project, XmlReader reader)
        {
            string sourceRootFolder = null;
            HeaderDefinition currentHeader = null;

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
                            var @class = new ClassDefinition(className, currentHeader);
                            ReadClassDefinition(project, @class, reader);
                        }
                        break;

                    case "ClassTemplateDefinition":
                        {
                            string className = reader.GetAttribute("Name");
                            var @class = new ClassTemplateDefinition(className, currentHeader);
                            ReadClassDefinition(project, @class, reader);
                        }
                        break;

                    case "HeaderDefinition":
                        {
                            string headerPath = sourceRootFolder + reader.GetAttribute("Path");
                            headerPath = headerPath.Replace('\\', '/');
                            currentHeader = new HeaderDefinition(headerPath);
                            project.HeaderDefinitions.Add(headerPath, currentHeader);
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
                            sourceRootFolder = reader.GetAttribute("Path");
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
