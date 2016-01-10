using System;
using System.Collections.Generic;

namespace BulletSharpGen
{
    class Program
    {
        static void Main(string[] args)
        {
            // If true, outputs C++/CLI wrapper,
            // if false, outputs C wrapper with C# code.
            bool cppCliMode = false;

            var project = WrapperProject.FromFile("bullet3.xml");
            if (!project.VerifyFiles())
            {
                Console.ReadKey();
                return;
            }

            var reader = new CppReader(project);
            var parser = new BulletParser(project);
            parser.Parse();
            Console.WriteLine("Parsing complete");

            WrapperWriter writer;
            if (cppCliMode)
            {
                writer = new CppCliWriter(project.HeaderDefinitions.Values, project.NamespaceName);
            }
            else
            {
                writer = new PInvokeWriter(project);

                var extensionWriter = new ExtensionsWriter(project.HeaderDefinitions.Values, project.NamespaceName);
                extensionWriter.Output();
            }
            writer.Output();

            OutputSolution(TargetVS.VS2008, project);
            OutputSolution(TargetVS.VS2010, project);
            OutputSolution(TargetVS.VS2012, project);
            OutputSolution(TargetVS.VS2013, project);
            OutputSolution(TargetVS.VS2015, project);
            //project.Save();

            CMakeWriter cmake = new CMakeWriter(project.HeaderDefinitions, project.NamespaceName);
            cmake.Output();

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static void OutputSolution(TargetVS targetVS, WrapperProject project)
        {
            string targetVersionString;
            switch (targetVS)
            {
                case TargetVS.VS2008:
                    targetVersionString = "2008";
                    break;
                case TargetVS.VS2010:
                    targetVersionString = "2010";
                    break;
                case TargetVS.VS2012:
                    targetVersionString = "2012";
                    break;
                case TargetVS.VS2013:
                    targetVersionString = "2013";
                    break;
                case TargetVS.VS2015:
                    targetVersionString = "2015";
                    break;
                default:
                    throw new NotImplementedException();
            }

            string slnRelDir = (targetVS == TargetVS.VS2010) ? "" : "..\\";
            string rootFolder = slnRelDir + "src\\";

            List<ProjectConfiguration> confs = new List<ProjectConfiguration>();
            confs.Add(new ProjectConfiguration("Axiom", true, "GRAPHICS_AXIOM", slnRelDir + "..\\Axiom-SDK-0.8.3376.12322\\bin\\Net35"));
            confs.Add(new ProjectConfiguration("Axiom", false, "GRAPHICS_AXIOM", slnRelDir + "..\\Axiom-SDK-0.8.3376.12322\\bin\\Net35"));
            confs.Add(new ProjectConfiguration("Generic", true, "GRAPHICS_GENERIC"));
            confs.Add(new ProjectConfiguration("Generic", false, "GRAPHICS_GENERIC"));
            confs.Add(new ProjectConfiguration("Mogre", true, "GRAPHICS_MOGRE", "C:\\MogreSDK\\bin\\Debug"));
            confs.Add(new ProjectConfiguration("Mogre", false, "GRAPHICS_MOGRE", "C:\\MogreSDK\\bin\\Release"));
            if (targetVS != TargetVS.VS2008)
            {
                confs.Add(new ProjectConfiguration("MonoGame", true, "GRAPHICS_MONOGAME", "$(ProgramFiles)\\MonoGame\\v3.0\\Assemblies\\WindowsGL\\;$(ProgramFiles(x86))\\MonoGame\\v3.0\\Assemblies\\WindowsGL\\"));
                confs.Add(new ProjectConfiguration("MonoGame", false, "GRAPHICS_MONOGAME", "$(ProgramFiles)\\MonoGame\\v3.0\\Assemblies\\WindowsGL\\;$(ProgramFiles(x86))\\MonoGame\\v3.0\\Assemblies\\WindowsGL\\"));
            }
            if (targetVS != TargetVS.VS2008 && targetVS != TargetVS.VS2010)
            {
                confs.Add(new ProjectConfiguration("Numerics", true, "GRAPHICS_NUMERICS"));
                confs.Add(new ProjectConfiguration("Numerics", false, "GRAPHICS_NUMERICS"));
            }
            if (targetVS == TargetVS.VS2008)
            {
                confs.Add(new ProjectConfiguration("OpenTK", true, "GRAPHICS_OPENTK", "$(USERPROFILE)\\My Documents\\OpenTK\\1.1\\Binaries\\OpenTK\\Release"));
                confs.Add(new ProjectConfiguration("OpenTK", false, "GRAPHICS_OPENTK", "$(USERPROFILE)\\My Documents\\OpenTK\\1.1\\Binaries\\OpenTK\\Release"));
                confs.Add(new ProjectConfiguration("SharpDX", true, "GRAPHICS_SHARPDX", slnRelDir + "..\\SharpDX\\Bin\\DirectX11-net20"));
                confs.Add(new ProjectConfiguration("SharpDX", false, "GRAPHICS_SHARPDX", slnRelDir + "..\\SharpDX\\Bin\\DirectX11-net20"));
                confs.Add(new ProjectConfiguration("SharpDX Signed", true, "GRAPHICS_SHARPDX", slnRelDir + "..\\SharpDX\\Bin\\DirectX11-Signed-net20"));
                confs.Add(new ProjectConfiguration("SharpDX Signed", false, "GRAPHICS_SHARPDX", slnRelDir + "..\\SharpDX\\Bin\\DirectX11-Signed-net20"));
                confs.Add(new ProjectConfiguration("SlimDX", true, "GRAPHICS_SLIMDX", "$(PROGRAMFILES)\\SlimDX SDK (January 2012)\\Bin\\net20\\;$(PROGRAMFILES(x86))\\SlimDX SDK (June 2010)\\Bin\\net20\\"));
                confs.Add(new ProjectConfiguration("SlimDX", false, "GRAPHICS_SLIMDX", "$(PROGRAMFILES)\\SlimDX SDK (January 2012)\\Bin\\net20\\;$(PROGRAMFILES(x86))\\SlimDX SDK (June 2010)\\Bin\\net20\\"));
            }
            else
            {
                confs.Add(new ProjectConfiguration("OpenTK", true, "GRAPHICS_OPENTK")
                {
                    ConditionalRef = "OpenTK",
                    ConditionalRefAssembly = "OpenTK, Version=1.1.0.0, Culture=neutral, PublicKeyToken=bad199fe84eb3df4"
                });
                confs.Add(new ProjectConfiguration("OpenTK", false, "GRAPHICS_OPENTK")
                {
                    ConditionalRef = "OpenTK",
                    ConditionalRefAssembly = "OpenTK, Version=1.1.0.0, Culture=neutral, PublicKeyToken=bad199fe84eb3df4"
                });
                confs.Add(new ProjectConfiguration("SharpDX", true, "GRAPHICS_SHARPDX", slnRelDir + "..\\SharpDX\\Bin\\DirectX11-net40"));
                confs.Add(new ProjectConfiguration("SharpDX", false, "GRAPHICS_SHARPDX", slnRelDir + "..\\SharpDX\\Bin\\DirectX11-net40"));
                confs.Add(new ProjectConfiguration("SharpDX Signed", true, "GRAPHICS_SHARPDX", slnRelDir + "..\\SharpDX\\Bin\\DirectX11-Signed-net40"));
                confs.Add(new ProjectConfiguration("SharpDX Signed", false, "GRAPHICS_SHARPDX", slnRelDir + "..\\SharpDX\\Bin\\DirectX11-Signed-net40"));
                confs.Add(new ProjectConfiguration("SlimDX", true, "GRAPHICS_SLIMDX", "$(PROGRAMFILES)\\SlimDX SDK (January 2012)\\Bin\\net40\\;$(PROGRAMFILES(x86))\\SlimDX SDK (June 2010)\\Bin\\net40\\"));
                confs.Add(new ProjectConfiguration("SlimDX", false, "GRAPHICS_SLIMDX", "$(PROGRAMFILES)\\SlimDX SDK (January 2012)\\Bin\\net40\\;$(PROGRAMFILES(x86))\\SlimDX SDK (June 2010)\\Bin\\net40\\"));
            }
            //confs.Add(new ProjectConfiguration("SlimMath", true, "GRAPHICS_SLIMMATH", slnRelDir + "..\\SlimMath\\SlimMath\\bin\\Release"));
            //confs.Add(new ProjectConfiguration("SlimMath", false, "GRAPHICS_SLIMMATH", slnRelDir + "..\\SlimMath\\SlimMath\\bin\\Debug"));
            if (targetVS != TargetVS.VS2008)
            {
                confs.Add(new ProjectConfiguration("Windows API Code Pack", true, "GRAPHICS_WAPICODEPACK", slnRelDir + "..\\Windows API Code Pack 1.1\\binaries\\DirectX"));
                confs.Add(new ProjectConfiguration("Windows API Code Pack", false, "GRAPHICS_WAPICODEPACK", slnRelDir + "..\\Windows API Code Pack 1.1\\binaries\\DirectX"));
            }
            /*
            confs.Add(new ProjectConfiguration("XNA 3.1", true, "GRAPHICS_XNA31", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\"));
            confs.Add(new ProjectConfiguration("XNA 3.1", false, "GRAPHICS_XNA31", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\"));
            if (targetVS != TargetVS.VS2008)
            {
                confs.Add(new ProjectConfiguration("XNA 4.0", true, "GRAPHICS_XNA40", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\"));
                confs.Add(new ProjectConfiguration("XNA 4.0", false, "GRAPHICS_XNA40", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\"));
            }
            */

            var filterWriter = new FilterWriter(project.NamespaceName);
            var sourceFilter = new Filter("Source Files", "4FC737F1-C7A5-4376-A066-2A32D752A2FF", "cpp;c;cc;cxx;def;odl;idl;hpj;bat;asm;asmx");
            var headerFilter = new Filter("Header Files", "93995380-89BD-4b04-88EB-625FBE52EBFB", "h;hh;hpp;hxx;hm;inl;inc;xsd");
            var resourceFilter = new Filter("Resource Files", "67DA6AB6-F800-4c08-8B7A-83BB121AAD01", "rc;ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe;resx;tiff;tif;png;wav;mfcribbon-ms");
            filterWriter.RootFilter.Add(sourceFilter);
            filterWriter.RootFilter.Add(headerFilter);
            filterWriter.RootFilter.Add(resourceFilter);

            sourceFilter.AddFile("", rootFolder + "Stdafx");
            sourceFilter.AddFile("", rootFolder + "AssemblyInfo");
            sourceFilter.AddFile("", rootFolder + "Collections");
            sourceFilter.AddFile("", rootFolder + "Math");
            sourceFilter.AddFile("", rootFolder + "StringConv");
            sourceFilter.AddFile("", rootFolder + "DataStream");
            sourceFilter.AddFile("", rootFolder + "Matrix");
            sourceFilter.AddFile("", rootFolder + "Quaternion");
            sourceFilter.AddFile("", rootFolder + "Utilities");
            sourceFilter.AddFile("", rootFolder + "Vector3");
            sourceFilter.AddFile("", rootFolder + "Vector4");

            headerFilter.AddFile("", rootFolder + "Stdafx");
            headerFilter.AddFile("", rootFolder + "Collections");
            headerFilter.AddFile("", rootFolder + "Enums");
            headerFilter.AddFile("", rootFolder + "ITrackingDisposable");
            headerFilter.AddFile("", rootFolder + "Math");
            headerFilter.AddFile("", rootFolder + "StringConv");
            headerFilter.AddFile("", rootFolder + "Version");
            headerFilter.AddFile("", rootFolder + "DataStream");
            headerFilter.AddFile("", rootFolder + "Matrix");
            headerFilter.AddFile("", rootFolder + "Quaternion");
            headerFilter.AddFile("", rootFolder + "Utilities");
            headerFilter.AddFile("", rootFolder + "Vector3");
            headerFilter.AddFile("", rootFolder + "Vector4");

            foreach (HeaderDefinition header in project.HeaderDefinitions.Values)
            {
                if (header.Classes.Count == 0)
                {
                    continue;
                }

                sourceFilter.AddFile(header.Filename, rootFolder + header.ManagedName);
                headerFilter.AddFile(header.Filename, rootFolder + header.ManagedName);
            }

            sourceFilter.AddFile("BulletCollision/CollisionDispatch/", rootFolder + "InternalEdgeUtility");
            sourceFilter.AddFile("BulletCollision/CollisionShapes/", rootFolder + "BulletMaterial");
            sourceFilter.AddFile("BulletCollision/NarrowPhaseCollision/", rootFolder + "SimplexSolverInterface");
            sourceFilter.AddFile("LinearMath/", rootFolder + "DebugDraw");
            sourceFilter.AddFile("SoftBody/", rootFolder + "SoftBodySolver");
            headerFilter.AddFile("BulletCollision/CollisionDispatch/", rootFolder + "InternalEdgeUtility");
            headerFilter.AddFile("BulletCollision/CollisionShapes/", rootFolder + "BulletMaterial");
            headerFilter.AddFile("BulletCollision/NarrowPhaseCollision/", rootFolder + "SimplexSolverInterface");
            headerFilter.AddFile("LinearMath/", rootFolder + "DebugDraw");
            headerFilter.AddFile("BulletDynamics/Character/", rootFolder + "ICharacterController");
            headerFilter.AddFile("LinearMath/", rootFolder + "IDebugDraw");
            headerFilter.AddFile("SoftBody/", rootFolder + "SoftBodySolver");

            resourceFilter.AddFile("", rootFolder + "Resources.rc");

            filterWriter.RootFilter.Sort();

            string bulletRoot = "..\\bullet3";

            var slnWriter = new SlnWriter(filterWriter, project.NamespaceName)
            {
                IncludeDirectories = string.Format("{0}\\src;{0}\\Extras\\HACD;{0}\\Extras\\Serialize\\BulletWorldImporter;", slnRelDir + bulletRoot),
                FilterWriter = filterWriter
            };
            if (targetVS == TargetVS.VS2008)
            {
                slnWriter.LibraryDirectoriesRelease = slnRelDir + bulletRoot + "\\msvc\\2008\\lib\\MinSizeRel";
                slnWriter.LibraryDirectoriesDebug = slnRelDir + bulletRoot + "\\msvc\\2008\\lib\\Debug";
            }
            else
            {
                slnWriter.LibraryDirectoriesRelease = slnRelDir + bulletRoot + "\\msvc\\" + targetVersionString + "\\lib\\MinSizeRel;";
                slnWriter.LibraryDirectoriesDebug = slnRelDir + bulletRoot + "\\msvc\\" + targetVersionString + "\\lib\\Debug;";
            }
            slnWriter.Output(targetVS, confs, "sln" + targetVersionString);
        }
    }
}
