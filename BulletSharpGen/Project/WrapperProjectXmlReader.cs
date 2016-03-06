using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            using (var reader = XmlReader.Create(project.ProjectFilePath, xmlSettings))
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

            var replaceMapping = mapping as ReplaceMapping;
            if (replaceMapping != null)
            {
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

        static void ReadHeaderDefinition(WrapperProject project, XmlReader reader)
        {
            string sourceRootFolder = project.SourceRootFoldersFull.Last();
            string headerPath = Path.Combine(sourceRootFolder, reader.GetAttribute("Path"));
            headerPath = headerPath.Replace('\\', '/');
            var header = new HeaderDefinition(headerPath);
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
                        case "Class":
                        case "ClassTemplate":
                        case "Enum":
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
                case "Class":
                    @class = new ClassDefinition(className, header, parent);
                    break;
                case "ClassTemplate":
                    @class = new ClassTemplateDefinition(className, header, parent);
                    break;
                case "Enum":
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

            if ("true".Equals(reader.GetAttribute("IsExcluded")))
            {
                @class.IsExcluded = true;
            }
            if ("true".Equals(reader.GetAttribute("HasPreventDelete")))
            {
                @class.HasPreventDelete = true;
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
                        case "Class":
                        case "ClassTemplate":
                        case "Enum":
                            {
                                ReadClassDefinition(project, reader, header, @class);
                            }
                            break;
                        case "Method":
                            {
                                ReadMethodDefinition(reader, @class);
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

        static void ReadMethodDefinition(XmlReader reader, ClassDefinition @class)
        {
            string name = reader.GetAttribute("Name");
            string isExcluded = reader.GetAttribute("IsExcluded");

            var parameters = new List<ParameterDefinition>();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }

                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                switch (reader.Name)
                {
                    case "Parameter":
                        {
                            string parameterName = reader.GetAttribute("Name");
                            var parameter = new ParameterDefinition(parameterName, null);
                            string marshalDirectionString = reader.GetAttribute("MarshalDirection");
                            if (marshalDirectionString != null)
                            {
                                MarshalDirection marshalDirection;
                                if (Enum.TryParse(marshalDirectionString, out marshalDirection))
                                {
                                    parameter.MarshalDirection = marshalDirection;
                                }
                            }
                            parameters.Add(parameter);
                        }
                        continue;
                }
            }

            var method = new MethodDefinition(name, @class, parameters.Count);
            for (int i = 0; i < parameters.Count; i++)
            {
                method.Parameters[i] = parameters[i];
            }
            if ("true".Equals(isExcluded))
            {
                method.IsExcluded = true;
            }
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
                    case "CppProjectPath":
                        {
                            project.CppProjectPath = reader.ReadElementContentAsString();
                        }
                        break;
                    case "CProjectPath":
                        {
                            project.CProjectPath = reader.ReadElementContentAsString();
                        }
                        break;
                    case "CsProjectPath":
                        {
                            project.CsProjectPath = reader.ReadElementContentAsString();
                        }
                        break;
                    case "CppCliProjectPath":
                        {
                            project.CppCliProjectPath = reader.ReadElementContentAsString();
                        }
                        break;
                    case "Header":
                        {
                            ReadHeaderDefinition(project, reader);
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
                            string sourceRootFolder = reader.GetAttribute("Path");
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
