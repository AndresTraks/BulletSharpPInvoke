using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace BulletSharpGen.Project
{
    class WrapperProjectXmlWriter
    {
        public static string MakeRelativePath(string fromPath, string toPath)
        {
            if (string.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (string.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            var fromUri = new Uri(fromPath);
            var toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.ToUpperInvariant() == "FILE")
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }

        public static void WriteClassDefinition(XmlWriter writer, ClassDefinition @class)
        {
            writer.WriteStartElement(@class.GetType().Name);
            writer.WriteAttributeString("Name", @class.Name);
            if (@class.NamespaceName != "")
            {
                writer.WriteAttributeString("Namespace", @class.NamespaceName);
            }
            if (@class.IsExcluded)
            {
                writer.WriteAttributeString("IsExcluded", "true");
            }

            foreach (var method in @class.Methods.Where(m => m.Parameters.Any(p => p.MarshalDirection == MarshalDirection.Out)))
            {
                WriteMethodDefinition(writer, method);
            }

            foreach (var childClass in @class.Classes)
            {
                WriteClassDefinition(writer, childClass);
            }

            writer.WriteEndElement();
        }

        public static void WriteMethodDefinition(XmlWriter writer, MethodDefinition method)
        {
            writer.WriteStartElement("MethodDefinition");
            writer.WriteAttributeString("Name", method.Name);
            /*if (method.IsExcluded)
            {
                writer.WriteAttributeString("IsExcluded", "true");
            }*/
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
            if (mapping is ReplaceMapping)
            {
                var replaceMapping = mapping as ReplaceMapping;
                foreach (var replacement in replaceMapping.Replacements.OrderBy(kv => kv.Key))
                {
                    writer.WriteStartElement("Replacement");
                    writer.WriteAttributeString("Replace", replacement.Key);
                    writer.WriteAttributeString("With", replacement.Value);
                    writer.WriteEndElement();
                }
                if (replaceMapping is ScriptedMapping)
                {
                    var scriptedMapping = replaceMapping as ScriptedMapping;
                    writer.WriteStartElement("ScriptBody");
                    writer.WriteString(scriptedMapping.ScriptBody);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }

        public static void Write(WrapperProject project)
        {
            using (var writer = XmlWriter.Create(project.ProjectPath, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartElement("Project");

                if (project.ClassNameMapping != null) WriteMapping(writer, project.ClassNameMapping);
                if (project.MethodNameMapping != null) WriteMapping(writer, project.MethodNameMapping);
                if (project.ParameterNameMapping != null) WriteMapping(writer, project.ParameterNameMapping);

                writer.WriteStartElement("NamespaceName");
                writer.WriteString(project.NamespaceName);
                writer.WriteEndElement();

                foreach (string sourceRootFolder in project.SourceRootFolders)
                {
                    string sourceRootFolderCanonical = sourceRootFolder.Replace('\\', '/');

                    writer.WriteStartElement("SourceRootFolder");
                    string sourceRootFolderRelative = MakeRelativePath(project.ProjectPath, sourceRootFolder);
                    sourceRootFolderRelative = sourceRootFolderRelative.Replace('\\', '/');
                    writer.WriteAttributeString("Path", sourceRootFolderRelative);

                    foreach (var header in project.HeaderDefinitions)
                    {
                        if (!header.Key.StartsWith(sourceRootFolderCanonical)) continue;

                        writer.WriteStartElement("HeaderDefinition");
                        string headerRelativePath = MakeRelativePath(sourceRootFolder, header.Key);
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
    }
}
