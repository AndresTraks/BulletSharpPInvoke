using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BulletSharpGen.Project
{
    class WrapperProjectXmlReader
    {
        public static void Read(WrapperProject project)
        {
            var xmlSettings = new XmlReaderSettings();
            xmlSettings.Schemas.Add("urn:dotnetwrappergen", "dotnetwrappergen.xsd");
            xmlSettings.ValidationType = ValidationType.Schema;
            xmlSettings.ValidationEventHandler += (sender, args) =>
            {
                Console.WriteLine(args.Message);
            };

            using (var reader = XmlReader.Create(project.ProjectPath, xmlSettings))
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

        static void ReadSymbolMapping(WrapperProject project, XmlReader reader)
        {
            string mappingType = reader.Name;
            string mappingName = reader.GetAttribute("Name");
            var replacements = new Dictionary<string, string>();
            string scriptBody = null;

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
                        case "Replacement":
                            {
                                string replace = reader.GetAttribute("Replace");
                                string with = reader.GetAttribute("With");
                                replacements.Add(replace, with);
                            }
                            break;
                        case "ScriptBody":
                            {
                                scriptBody = reader.ReadElementContentAsString();
                            }
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }
            }

            ISymbolMapping mapping;

            switch (mappingType)
            {
                case "ScriptedMapping":
                    {
                        mapping = new ScriptedMapping(mappingName, scriptBody);
                    }
                    break;
                case "ReplaceMapping":
                    {
                        mapping = new ReplaceMapping(mappingName);
                    }
                    break;
                default:
                    Console.WriteLine("Unknown mapping!");
                    return;
            }

            if (mapping is ReplaceMapping)
            {
                var replaceMapping = mapping as ReplaceMapping;
                foreach (var replacement in replacements)
                {
                    replaceMapping.Replacements.Add(replacement.Key, replacement.Value);
                }
            }

            switch (mapping.Name)
            {
                case "NameMapping":
                    project.ClassNameMapping = mapping;
                    project.HeaderNameMapping = mapping;
                    break;
                case "MethodNameMapping":
                    project.MethodNameMapping = mapping;
                    break;
                case "ParameterNameMapping":
                    project.ParameterNameMapping = mapping;
                    break;
            }
        }

        static void ReadHeaderDefinition(WrapperProject project, XmlReader reader, string sourceRootFolder)
        {
            string headerPath = Path.Combine(sourceRootFolder, reader.GetAttribute("Path"));
            headerPath = headerPath.Replace('\\', '/');
            HeaderDefinition header = new HeaderDefinition(headerPath);
            string isExcluded = reader.GetAttribute("IsExcluded");
            if ("true".Equals(isExcluded))
            {
                header.IsExcluded = true;
            }

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
            if ("true".Equals(isExcluded))
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

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch (reader.Name)
                {
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
                    case "ReplaceMapping":
                    case "ScriptedMapping":
                        {
                            ReadSymbolMapping(project, reader);
                        }
                        break;
                }
            }
        }
    }
}
