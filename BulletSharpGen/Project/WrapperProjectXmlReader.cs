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

        static void ReadHeaderDefinition(WrapperProject project, XmlReader reader, string sourceRootFolder)
        {
            string headerPath = sourceRootFolder + reader.GetAttribute("Path");
            headerPath = headerPath.Replace('\\', '/');
            HeaderDefinition header = new HeaderDefinition(headerPath);
            project.HeaderDefinitions.Add(headerPath, header);

            if (reader.IsEmptyElement)
            {
                return;
            }

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "ClassDefinition":
                        case "ClassTemplateDefinition":
                        case "EnumDefinition":
                            {
                                ReadClassDefinition(project, reader, header);
                            }
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }
            }
        }

        static void ReadClassDefinition(WrapperProject project, XmlReader reader, HeaderDefinition header, ClassDefinition parent = null)
        {
            string className = reader.GetAttribute("Name");

            ClassDefinition @class;
            string classType = reader.Name;
            switch (classType)
            {
                case "ClassDefinition":
                    @class = new ClassDefinition(className, header, parent);
                    break;
                case "ClassTemplateDefinition":
                    @class = new ClassTemplateDefinition(className, header, parent);
                    break;
                case "EnumDefinition":
                    @class = new EnumDefinition(className, header, parent);
                    break;
                default:
                    throw new NotImplementedException();
            }

            string namespaceName = reader.GetAttribute("Namespace");
            if (namespaceName != null)
            {
                @class.NamespaceName = namespaceName;
            }

            string isExcluded = reader.GetAttribute("IsExcluded");
            if ("True".Equals(isExcluded))
            {
                @class.IsExcluded = true;
            }
            project.ClassDefinitions.Add(@class.FullyQualifiedName, @class);

            if (reader.IsEmptyElement)
            {
                return;
            }

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "ClassDefinition":
                        case "ClassTemplateDefinition":
                        case "EnumDefinition":
                            {
                                ReadClassDefinition(project, reader, header, @class);
                            }
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
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
                    case "ClassTemplateDefinition":
                    case "EnumDefinition":
                        {
                            ReadClassDefinition(project, reader, currentHeader);
                        }
                        break;

                    case "HeaderDefinition":
                        {
                            ReadHeaderDefinition(project, reader, sourceRootFolder);
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
