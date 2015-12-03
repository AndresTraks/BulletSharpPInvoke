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
                writer.WriteAttributeString("IsExcluded", "True");
            }

            foreach (var childClass in @class.Classes)
            {
                WriteClassDefinition(writer, childClass);
            }

            writer.WriteEndElement();
        }

        public static void Write(WrapperProject project)
        {
            using (var writer = XmlWriter.Create(project.ProjectPath, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartElement("Project");
                foreach (string sourceRootFolder in project.SourceRootFolders)
                {
                    writer.WriteStartElement("NamespaceName");
                    writer.WriteString("BulletSharp");
                    writer.WriteEndElement();

                    writer.WriteStartElement("SourceRootFolder");
                    string sourceRootFolderRelative = MakeRelativePath(project.ProjectPath, sourceRootFolder);
                    sourceRootFolderRelative = sourceRootFolderRelative.Replace('\\', '/');
                    writer.WriteAttributeString("Path", sourceRootFolderRelative);

                    foreach (var header in project.HeaderDefinitions)
                    {
                        writer.WriteStartElement("HeaderDefinition");
                        string headerRelativePath = MakeRelativePath(sourceRootFolder, header.Key);
                        headerRelativePath = headerRelativePath.Replace('\\', '/');
                        writer.WriteAttributeString("Path", headerRelativePath);
                        if (header.Value.IsExcluded)
                        {
                            writer.WriteAttributeString("IsExcluded", "True");
                        }

                        foreach (var @class in header.Value.Classes)
                        {
                            WriteClassDefinition(writer, @class);
                        }

                        writer.WriteEndElement();
                    }
                }
            }
        }
    }
}
