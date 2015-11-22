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

        public static void Write(WrapperProject project)
        {
            using (var writer = XmlWriter.Create(project.ProjectPath))
            {
                writer.WriteStartElement("Project");
                foreach (string sourceRootFolder in project.SourceRootFolders)
                {
                    writer.WriteStartElement("NamespaceName");
                    writer.WriteString("BulletSharp");
                    writer.WriteEndElement();

                    foreach (var @class in project.ClassDefinitions.Where(c => c.Value.IsExcluded))
                    {
                        writer.WriteStartElement(@class.Value.GetType().Name);
                        writer.WriteAttributeString("Name", @class.Key);
                        writer.WriteAttributeString("IsExcluded", @class.Value.IsExcluded.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteStartElement("SourceRootFolder");
                    writer.WriteAttributeString("Name", MakeRelativePath(project.ProjectPath, sourceRootFolder));
                }
            }
        }
    }
}
