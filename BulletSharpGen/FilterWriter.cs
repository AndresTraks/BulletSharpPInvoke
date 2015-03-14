using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    enum FilterType
    {
        None,
        Simple,
        Structured
    }

    class Filter : IComparable<Filter>
    {
        public string Name { get; set; }
        public Filter Parent { get; set; }
        public List<Filter> SubFilters { get; private set; }
        public List<string> Files { get; private set; }
        public string Guid { get; set; }
        public string Extensions { get; set; }

        public string FullName
        {
            get
            {
                if (Parent != null)
                {
                    if ("<root>".Equals(Parent.Name))
                    {
                        return Name;
                    }
                    return Parent.FullName + '\\' + Name;
                }
                return Name;
            }
        }

        public Filter() : this("<root>", null)
        {
        }

        int GetStringHash(string value)
        {
            // return name.GetHashCode(); // Hash might change between .NET versions

            int hash = 0x5d5d5d5d;
            char[] chars = value.ToCharArray();
            foreach (var c in chars)
            {
                hash += c;
                hash -= c << 16;
            }
            return hash;
        }

        public Filter(string name, string guid, string extensions = null)
        {
            Name = name;
            SubFilters = new List<Filter>();
            Files = new List<string>();
            if (string.IsNullOrEmpty(guid))
            {
                //System.Guid newGuid = System.Guid.NewGuid();
                //Guid = newGuid.ToString();

                // GUIDs should stay the same, so use filter name as seed instead
                Random random = new Random(GetStringHash(name));
                var guidBuilder = new StringBuilder();
                guidBuilder.Append(random.Next(0xFFFF).ToString("X4"));
                guidBuilder.Append(random.Next(0xFFFF).ToString("X4"));
                guidBuilder.Append('-');
                guidBuilder.Append(random.Next(0xFFFF).ToString("X4"));
                guidBuilder.Append('-');
                guidBuilder.Append(random.Next(0xFFFF).ToString("X4"));
                guidBuilder.Append('-');
                guidBuilder.Append(random.Next(0xFFFF).ToString("X4"));
                guidBuilder.Append('-');
                guidBuilder.Append(random.Next(0xFFFF).ToString("X4"));
                guidBuilder.Append(random.Next(0xFFFF).ToString("X4"));
                guidBuilder.Append(random.Next(0xFFFF).ToString("X4"));
                Guid = guidBuilder.ToString().ToLower();
            }
            else
            {
                Guid = guid;
            }
            Extensions = extensions;
        }

        public void Add(Filter filter)
        {
            SubFilters.Add(filter);
            filter.Parent = this;
        }

        public void AddFile(string path, string managedFilename)
        {
            int rootFolderIndex = path.IndexOf('/');
            if (rootFolderIndex == -1)
            {
                Files.Add(managedFilename);
                return;
            }
            string rootFolder = path.Substring(0, rootFolderIndex);
            if (rootFolder.Equals(".."))
            {
                AddFile(path.Substring(rootFolderIndex + 1), managedFilename);
                return;
            }
            Filter subFilter = GetChild(rootFolder);
            if (subFilter == null)
            {
                subFilter = new Filter(rootFolder, null);
                Add(subFilter);
            }
            subFilter.AddFile(path.Substring(rootFolderIndex + 1), managedFilename);
        }

        public Filter GetChild(string index)
        {
            foreach (var filter in SubFilters)
            {
                if (filter.Name.Equals(index))
                {
                    return filter;
                }
            }
            return null;
        }

        public Filter GetFilter(string filename)
        {
            foreach (var file in Files)
            {
                if (file.Equals(filename))
                {
                    return this;
                }
            }
            foreach (var filter in SubFilters)
            {
                var result = filter.GetFilter(filename);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return Name;
        }

        void GetFileListRecursive(List<string> list)
        {
            foreach (string name in Files)
            {
                list.Add(name);
            }
            foreach (var filter in SubFilters)
            {
                filter.GetFileListRecursive(list);
            }
        }

        public List<string> GetFileList(bool sorted = false)
        {
            var list = new List<string>();
            GetFileListRecursive(list);
            if (sorted)
            {
                list.Sort();
            }
            return list;
        }

        public void Sort()
        {
            Files.Sort();
            SubFilters.Sort();
        }

        public int CompareTo(Filter other)
        {
            return Name.CompareTo(other.Name);
        }
    }

    class FilterWriter
    {
        public Filter RootFilter { get; private set; }

        FilterType filterType = FilterType.Structured;
        string namespaceName;
        StreamWriter filterWriter;

        public FilterWriter(string namespaceName)
        {
            this.namespaceName = namespaceName;
            RootFilter = new Filter();
        }

        void Write(string value)
        {
            filterWriter.Write(value);
        }

        void WriteLine(string value)
        {
            filterWriter.WriteLine(value);
        }

        void OutputTabs(int n)
        {
            for (int i = 0; i < n; i++)
            {
                filterWriter.Write("\t");
            }
        }

        void OutputFilter(Filter filter)
        {
            Write("    <Filter Include=\"");
            Write(filter.FullName);
            WriteLine("\">");
            Write("      <UniqueIdentifier>{");
            Write(filter.Guid);
            WriteLine("}</UniqueIdentifier>");
            if (!string.IsNullOrEmpty(filter.Extensions))
            {
                Write("      <Extensions>");
                Write(filter.Extensions);
                WriteLine("</Extensions>");
            }
            WriteLine("    </Filter>");
            foreach (var subFilter in filter.SubFilters)
            {
                OutputFilter(subFilter);
            }
        }

        void OutputFilter2008(Filter filter, int level, string extension, IList<ProjectConfiguration> confs = null)
        {
            OutputTabs(level);
            WriteLine("<Filter");
            OutputTabs(level + 1);
            Write("Name=\"");
            Write(filter.Name);
            WriteLine("\"");
            if (!string.IsNullOrEmpty(filter.Extensions))
            {
                OutputTabs(level + 1);
                Write("Filter=\"");
                Write(filter.Extensions);
                WriteLine("\"");
            }
            OutputTabs(level + 1);
            WriteLine(">");
            foreach (string filename in filter.Files.OrderBy(f => f))
            {
                OutputTabs(level + 1);
                WriteLine("<File");
                OutputTabs(level + 2);
                Write("RelativePath=\"");
                Write(filename);
                Write(extension);
                WriteLine("\"");
                OutputTabs(level + 2);
                WriteLine(">");
                if (filename.EndsWith("Stdafx", StringComparison.InvariantCultureIgnoreCase) &&
                    ".cpp".Equals(extension, StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var conf in confs)
                    {
                        OutputTabs(level + 2);
                        WriteLine("<FileConfiguration");
                        OutputTabs(level + 3);
                        Write("Name=\"");
                        Write(conf.IsDebug ? "Debug " : "Release ");
                        Write(conf.Name);
                        WriteLine("|Win32\"");
                        OutputTabs(level + 3);
                        WriteLine(">");
                        OutputTabs(level + 3);
                        WriteLine("<Tool");
                        OutputTabs(level + 4);
                        WriteLine("Name=\"VCCLCompilerTool\"");
                        OutputTabs(level + 4);
                        WriteLine("UsePrecompiledHeader=\"1\"");
                        OutputTabs(level + 3);
                        WriteLine("/>");
                        OutputTabs(level + 2);
                        WriteLine("</FileConfiguration>");
                    }
                }
                OutputTabs(level + 1);
                WriteLine("</File>");
            }
            foreach (var subFilter in filter.SubFilters)
            {
                OutputFilter2008(subFilter, level + 1, extension, confs);
            }
            OutputTabs(level);
            WriteLine("</Filter>");
        }

        void OutputFiles(Filter root, string action, string extension)
        {
            WriteLine("  <ItemGroup>");
            foreach (var filename in root.GetFileList(true))
            {
                Write("    <");
                Write(action);
                Write(" Include=\"");
                Write(filename);
                Write(extension);
                WriteLine("\">");
                Write("      <Filter>");
                Write(root.GetFilter(filename).FullName);
                WriteLine("</Filter>");
                Write("    </");
                Write(action);
                WriteLine(">");
            }
            WriteLine("  </ItemGroup>");
        }

        public void Output(TargetVS targetVS, string outDirectory)
        {
            string filterFilename = namespaceName + ".vcxproj.filters";
            var filterFile = new FileStream(outDirectory + "\\" + filterFilename, FileMode.Create, FileAccess.Write);
            filterWriter = new StreamWriter(filterFile, Encoding.UTF8);

            WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            WriteLine("<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");

            WriteLine("  <ItemGroup>");
            foreach (var filter in RootFilter.SubFilters)
            {
                OutputFilter(filter);
            }
            WriteLine("  </ItemGroup>");

            OutputFiles(RootFilter.GetChild("Source Files"), "ClCompile", ".cpp");
            OutputFiles(RootFilter.GetChild("Resource Files"), "ResourceCompile", "");
            OutputFiles(RootFilter.GetChild("Header Files"), "ClInclude", ".h");

            Write("</Project>");

            filterWriter.Dispose();
            filterFile.Dispose();
        }

        public void Output2008(StreamWriter projectWriter, IList<ProjectConfiguration> confs, string outDirectory)
        {
            filterWriter = projectWriter;

            WriteLine("\t<Files>");
            OutputFilter2008(RootFilter.GetChild("Source Files"), 2, ".cpp", confs);
            OutputFilter2008(RootFilter.GetChild("Header Files"), 2, ".h");
            OutputFilter2008(RootFilter.GetChild("Resource Files"), 2, "");
            WriteLine("\t</Files>");
        }

        public void Output2008_2(StreamWriter projectWriter, IList<ProjectConfiguration> confs, string outDirectory)
        {
            filterWriter = projectWriter;

            WriteLine("\t<Files>");

            WriteLine("\t\t<Filter");
            WriteLine("\t\t\tName=\"Source Files\"");
            WriteLine("\t\t\tFilter=\"cpp;c;cc;cxx;def;odl;idl;hpj;bat;asm;asmx\"");
            WriteLine("\t\t\tUniqueIdentifier=\"{4FC737F1-C7A5-4376-A066-2A32D752A2FF}\"");
            WriteLine("\t\t\t>");
            var sourceFiles = RootFilter.GetChild("Source Files").GetFileList();
            foreach (var sourceFile in sourceFiles)
            {
                WriteLine("\t\t\t<File");
                Write("\t\t\t\tRelativePath=\"");
                Write(sourceFile);
                WriteLine(".cpp\"");
                WriteLine("\t\t\t\t>");
                if (sourceFile.EndsWith("Stdafx", StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var conf in confs)
                    {
                        WriteLine("\t\t\t\t<FileConfiguration");
                        Write("\t\t\t\t\tName=\"");
                        Write(conf.IsDebug ? "Debug " : "Release ");
                        Write(conf.Name);
                        WriteLine("|Win32\"");
                        WriteLine("\t\t\t\t\t>");
                        WriteLine("\t\t\t\t\t<Tool");
                        WriteLine("\t\t\t\t\t\tName=\"VCCLCompilerTool\"");
                        WriteLine("\t\t\t\t\t\tUsePrecompiledHeader=\"1\"");
                        WriteLine("\t\t\t\t\t/>");
                        WriteLine("\t\t\t\t</FileConfiguration>");
                    }
                }
                WriteLine("\t\t\t</File>");
            }
            WriteLine("\t\t</Filter>");

            WriteLine("\t\t<Filter");
            WriteLine("\t\t\tName=\"Header Files\"");
            WriteLine("\t\t\tFilter=\"h;hpp;hxx;hm;inl;inc;xsd\"");
            WriteLine("\t\t\tUniqueIdentifier=\"{93995380-89BD-4b04-88EB-625FBE52EBFB}\"");
            WriteLine("\t\t\t>");
            var headerFiles = RootFilter.GetChild("Header Files").GetFileList();
            foreach (var headerFile in headerFiles)
            {
                WriteLine("\t\t\t<File");
                Write("\t\t\t\tRelativePath=\"");
                Write(headerFile);
                WriteLine(".h\"");
                WriteLine("\t\t\t\t>");
                WriteLine("\t\t\t</File>");
            }
            WriteLine("\t\t</Filter>");

            WriteLine("\t</Files>");
        }
    }
}
