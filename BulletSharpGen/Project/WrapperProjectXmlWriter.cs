using System.Linq;
using System.Xml;

namespace BulletSharpGen.Project
{
    class WrapperProjectXmlWriter
    {
        public static void WriteClassDefinition(XmlWriter writer, ClassDefinition @class)
        {
            string name = @class.GetType().Name;
            if (name.EndsWith("Definition"))
            {
                name = name.Substring(0, name.Length - "Definition".Length);
            }

            writer.WriteStartElement(name);
            writer.WriteAttributeString("Name", @class.Name);
            if (@class.NamespaceName != "")
            {
                writer.WriteAttributeString("Namespace", @class.NamespaceName);
            }
            if (@class.IsExcluded)
            {
                writer.WriteAttributeString("IsExcluded", "true");
            }
            if (@class.HasPreventDelete)
            {
                writer.WriteAttributeString("HasPreventDelete", "true");
            }

            foreach (var childClass in @class.Classes)
            {
                WriteClassDefinition(writer, childClass);
            }

            foreach (var method in @class.Methods)
            {
                // Write out only methods that have non-default options
                if (method.Parameters.Any(p => p.MarshalDirection == MarshalDirection.Out) ||
                    method.BodyText != null || method.IsExcluded)
                {
                    WriteMethodDefinition(writer, method);
                }
            }

            writer.WriteEndElement();
        }

        public static void WriteMethodDefinition(XmlWriter writer, MethodDefinition method)
        {
            writer.WriteStartElement("Method");
            writer.WriteAttributeString("Name", method.Name);
            if (method.IsExcluded)
            {
                writer.WriteAttributeString("IsExcluded", "true");
            }
            foreach (var param in method.Parameters)
            {
                writer.WriteStartElement("Parameter");
                writer.WriteAttributeString("Name", param.Name);
                writer.WriteAttributeString("MarshalDirection", param.MarshalDirection.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public static void WriteMapping(XmlWriter writer, ISymbolMapping mapping)
        {
            writer.WriteStartElement(mapping.GetType().Name);
            writer.WriteAttributeString("Name", mapping.Name);

            var replaceMapping = mapping as ReplaceMapping;
            if (replaceMapping != null)
            {
                foreach (var replacement in replaceMapping.Replacements.OrderBy(kv => kv.Key))
                {
                    writer.WriteStartElement("Replacement");
                    writer.WriteAttributeString("Replace", replacement.Key);
                    writer.WriteAttributeString("With", replacement.Value);
                    writer.WriteEndElement();
                }
                var scriptedMapping = replaceMapping as ScriptedMapping;
                if (scriptedMapping != null)
                {
                    writer.WriteStartElement("ScriptBody");
                    writer.WriteString(scriptedMapping.ScriptBody);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }

        public static void Write(WrapperProject project)
        {
            using (var writer = XmlWriter.Create(project.ProjectFilePath, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartElement("Project", "urn:dotnetwrappergen");

                WriteStringElementIfNotNull(writer, "CppProjectPath", project.CppProjectPath);
                WriteStringElementIfNotNull(writer, "CProjectPath", project.CProjectPath);
                WriteStringElementIfNotNull(writer, "CsProjectPath", project.CsProjectPath);
                WriteStringElementIfNotNull(writer, "CppCliProjectPath", project.CppCliProjectPath);

                if (project.ClassNameMapping != null) WriteMapping(writer, project.ClassNameMapping);
                if (project.MethodNameMapping != null) WriteMapping(writer, project.MethodNameMapping);
                if (project.ParameterNameMapping != null) WriteMapping(writer, project.ParameterNameMapping);

                writer.WriteStartElement("NamespaceName");
                writer.WriteString(project.NamespaceName);
                writer.WriteEndElement();

                for (int i = 0; i < project.SourceRootFolders.Count; i++)
                {
                    string sourceRootFolder = project.SourceRootFolders[i];
                    string sourceRootFolderFull = project.SourceRootFoldersFull.ElementAt(i);
                    string sourceRootFolderCanonical = sourceRootFolderFull.Replace('\\', '/');

                    writer.WriteStartElement("SourceRootFolder");
                    writer.WriteAttributeString("Path", sourceRootFolder);

                    foreach (var header in project.HeaderDefinitions)
                    {
                        if (!header.Key.StartsWith(sourceRootFolderCanonical)) continue;

                        writer.WriteStartElement("Header");
                        string headerRelativePath = WrapperProject.MakeRelativePath(sourceRootFolderFull, header.Key);
                        headerRelativePath = headerRelativePath.Replace('\\', '/');
                        writer.WriteAttributeString("Path", headerRelativePath);
                        if (header.Value.IsExcluded)
                        {
                            writer.WriteAttributeString("IsExcluded", "true");
                        }
                        else
                        {
                            foreach (var @class in header.Value.Classes)
                            {
                                WriteClassDefinition(writer, @class);
                            }
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
            }
        }

        static void WriteStringElementIfNotNull(XmlWriter writer, string elementName, string value)
        {
            if (value == null) return;

            writer.WriteStartElement(elementName);
            writer.WriteString(value);
            writer.WriteEndElement();
        }
    }
}
