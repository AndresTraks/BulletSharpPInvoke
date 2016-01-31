using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    enum TargetVS
    {
        VS2008,
        VS2010,
        VS2012,
        VS2013,
        VS2015
    }

    class SlnWriter
    {
        FilterWriter filterWriter;
        string namespaceName;
        
        StreamWriter solutionWriter, projectWriter;
        TargetVS targetVS;
        FilterType filterType = FilterType.Structured;

        public string IncludeDirectories { get; set; }
        public string LibraryDirectoriesDebug { get; set; }
        public string LibraryDirectoriesRelease { get; set; }
        public bool X64 { get; set; }
        public FilterWriter FilterWriter { get; set; }

        public SlnWriter(FilterWriter filterWriter, string namespaceName)
        {
            this.filterWriter = filterWriter;
            this.namespaceName = namespaceName;
            
            X64 = true;
        }

        void Write(char value)
        {
            projectWriter.Write(value);
        }

        void Write(string value)
        {
            projectWriter.Write(value);
        }

        void WriteLine(string value)
        {
            projectWriter.WriteLine(value);
        }

        void WriteLine()
        {
            projectWriter.WriteLine();
        }

        void WriteSln(string value)
        {
            solutionWriter.Write(value);
        }

        void WriteLineSln(string value)
        {
            solutionWriter.WriteLine(value);
        }

        void WriteLineSln()
        {
            solutionWriter.WriteLine();
        }

        void WriteProjectConfigurationNameSln(ProjectConfiguration conf)
        {
            WriteSln(conf.IsDebug ? "Debug " : "Release ");
            WriteSln(conf.Name);
        }

        void WriteProjectConfigurationName(ProjectConfiguration conf)
        {
            Write(conf.IsDebug ? "Debug " : "Release ");
            Write(conf.Name);
        }

        void OutputProjectConfiguration(ProjectConfiguration conf, bool x64)
        {
            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("\t\t<Configuration");
                Write("\t\t\tName=\"");
                WriteProjectConfigurationName(conf);
                WriteLine(x64 ? "|x64\"" : "|Win32\"");
                WriteLine("\t\t\tOutputDirectory=\"$(SolutionDir)$(ConfigurationName)\"");
                WriteLine("\t\t\tIntermediateDirectory=\"$(ConfigurationName)\"");
                WriteLine("\t\t\tConfigurationType=\"2\"");
                WriteLine("\t\t\tCharacterSet=\"1\"");
                WriteLine("\t\t\tManagedExtensions=\"1\"");
                if (!conf.IsDebug)
                {
                    WriteLine("\t\t\tWholeProgramOptimization=\"1\"");
                }
                WriteLine("\t\t\t>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCPreBuildEventTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCCustomBuildTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCXMLDataGeneratorTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCWebServiceProxyGeneratorTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCMIDLTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCCLCompilerTool\"");
                if (conf.IsDebug)
                {
                    WriteLine("\t\t\t\tOptimization=\"0\"");
                }
                else
                {
                    WriteLine("\t\t\t\tInlineFunctionExpansion=\"2\"");
                    //WriteLine("\t\t\t\tFavorSizeOrSpeed=\"2\"");
                    WriteLine("\t\t\t\tFavorSizeOrSpeed=\"1\"");
                }
                if (!string.IsNullOrEmpty(IncludeDirectories))
                {
                    Write("\t\t\t\tAdditionalIncludeDirectories=\"");
                    Write(IncludeDirectories);
                    WriteLine("\"");
                }
                Write("\t\t\t\tAdditionalUsingDirectories=\"");
                Write(conf.UsingDirectories);
                WriteLine("\"");
                Write("\t\t\t\tPreprocessorDefinitions=\"");
                Write(conf.Definitions);
                if (!string.IsNullOrEmpty(conf.Definitions) && !conf.Definitions.EndsWith(";"))
                {
                    Write(';');
                }
                //if (!x64)
                {
                    Write("WIN32;");
                }
                Write(conf.IsDebug ? "_DEBUG;" : "NDEBUG;");
                WriteLine("\"");
                if (conf.IsDebug)
                {
                    WriteLine("\t\t\t\tRuntimeLibrary=\"3\"");
                }
                else
                {
                    WriteLine("\t\t\t\tRuntimeLibrary=\"2\"");
                }
                WriteLine("\t\t\t\tFloatingPointModel=\"0\"");
                //WriteLine("\t\t\t\tEnableEnhancedInstructionSet=\"0\"");
                WriteLine("\t\t\t\tUsePrecompiledHeader=\"2\"");
                if (conf.IsDebug)
                {
                    WriteLine("\t\t\t\tWarningLevel=\"3\"");
                    WriteLine("\t\t\t\tDebugInformationFormat=\"3\"");
                    WriteLine("\t\t\t\tDisableSpecificWarnings=\"4793\"");
                }
                else
                {
                    WriteLine("\t\t\t\tWarningLevel=\"1\"");
                }
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCManagedResourceCompilerTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCResourceCompilerTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCPreLinkEventTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCLinkerTool\"");
                Write("\t\t\t\tAdditionalDependencies=\"");
                if (conf.IsDebug)
                {
                    WriteLine("LinearMath_Debug.lib BulletCollision_Debug.lib BulletDynamics_Debug.lib\"");
                }
                else
                {
                    WriteLine("LinearMath_MinSizeRel.lib BulletCollision_MinsizeRel.lib BulletDynamics_MinsizeRel.lib\"");
                }
                WriteLine("\t\t\t\tLinkIncremental=\"1\"");
                if (conf.IsDebug)
                {
                    if (!string.IsNullOrEmpty(LibraryDirectoriesDebug))
                    {
                        Write("\t\t\t\tAdditionalLibraryDirectories=\"");
                        Write(LibraryDirectoriesDebug);
                        WriteLine("\"");
                    }
                    WriteLine("\t\t\t\tGenerateDebugInformation=\"true\"");
                    WriteLine("\t\t\t\tAssemblyDebug=\"1\"");
                }
                else
                {
                    if (!string.IsNullOrEmpty(LibraryDirectoriesRelease))
                    {
                        Write("\t\t\t\tAdditionalLibraryDirectories=\"");
                        Write(LibraryDirectoriesRelease);
                        WriteLine("\"");
                    }
                    WriteLine("\t\t\t\tGenerateDebugInformation=\"false\"");
                }
                WriteLine("\t\t\t\tTargetMachine=\"1\"");
                //WriteLine("\t\t\t\tCLRUnmanagedCodeCheck=\"true\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCALinkTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCManifestTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCXDCMakeTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCBscMakeTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCFxCopTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCAppVerifierTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t\t<Tool");
                WriteLine("\t\t\t\tName=\"VCPostBuildEventTool\"");
                WriteLine("\t\t\t/>");
                WriteLine("\t\t</Configuration>");
            }
            else
            {
                Write("    <ProjectConfiguration Include=\"");
                WriteProjectConfigurationName(conf);
                WriteLine(x64 ? "|x64\">" : "|Win32\">");
                Write("      <Configuration>");
                WriteProjectConfigurationName(conf);
                WriteLine("</Configuration>");
                Write("      <Platform>");
                Write(x64 ? "x64" : "Win32");
                WriteLine("</Platform>");
                WriteLine("    </ProjectConfiguration>");
            }
        }

        void OutputPropertyGroupConfiguration(ProjectConfiguration conf, bool x64)
        {
            Write("  <PropertyGroup Condition=\"\'$(Configuration)|$(Platform)'=='");
            WriteProjectConfigurationName(conf);
            Write(x64 ? "|x64" : "|Win32");
            WriteLine("'\" Label=\"Configuration\">");
            WriteLine("    <ConfigurationType>DynamicLibrary</ConfigurationType>");
            WriteLine("    <CharacterSet>Unicode</CharacterSet>");
            WriteLine("    <CLRSupport>true</CLRSupport>");
            if (conf.IsDebug)
            {
                if (targetVS == TargetVS.VS2012 || targetVS == TargetVS.VS2013 || targetVS == TargetVS.VS2015)
                {
                    WriteLine("    <UseDebugLibraries>true</UseDebugLibraries>");
                }
            }
            else
            {
                WriteLine("    <WholeProgramOptimization>true</WholeProgramOptimization>");
            }
            if (targetVS == TargetVS.VS2012)
            {
                WriteLine("    <PlatformToolset>v110</PlatformToolset>");
            }
            else if (targetVS == TargetVS.VS2013)
            {
                WriteLine("    <PlatformToolset>v120</PlatformToolset>");
            }
            else if (targetVS == TargetVS.VS2015)
            {
                WriteLine("    <PlatformToolset>v140</PlatformToolset>");
            }
            WriteLine("  </PropertyGroup>");
        }

        void OutputImportGroupPropertySheets(ProjectConfiguration conf)
        {
            Write("  <ImportGroup Condition=\"'$(Configuration)|$(Platform)'=='");
            WriteProjectConfigurationName(conf);
            Write(X64 ? "|x64" : "|Win32");
            WriteLine("'\" Label=\"PropertySheets\">");
            WriteLine("    <Import Project=\"$(UserRootDir)\\Microsoft.Cpp.$(Platform).user.props\" Condition=\"exists('$(UserRootDir)\\Microsoft.Cpp.$(Platform).user.props')\" Label=\"LocalAppDataPlatform\" />");
            WriteLine("  </ImportGroup>");
        }

        void OutputPropertyGroupConfiguration2(ProjectConfiguration conf, bool x64)
        {
            Write("    <OutDir Condition=\"'$(Configuration)|$(Platform)'=='");
            WriteProjectConfigurationName(conf);
            Write(x64 ? "|x64" : "|Win32");
            WriteLine("'\">$(SolutionDir)$(Configuration)\\</OutDir>");
            Write("    <IntDir Condition=\"'$(Configuration)|$(Platform)'=='");
            WriteProjectConfigurationName(conf);
            Write(x64 ? "|x64" : "|Win32");
            WriteLine("'\">$(Configuration)\\</IntDir>");
            Write("    <LinkIncremental Condition=\"'$(Configuration)|$(Platform)'=='");
            WriteProjectConfigurationName(conf);
            Write(x64 ? "|x64" : "|Win32");
            WriteLine("'\">false</LinkIncremental>");
        }

        void OutputItemDefinitionGroup(ProjectConfiguration conf, bool x64)
        {
            Write("  <ItemDefinitionGroup Condition=\"'$(Configuration)|$(Platform)'=='");
            WriteProjectConfigurationName(conf);
            Write(x64 ? "|x64" : "|Win32");
            WriteLine("'\">");
            WriteLine("    <ClCompile>");
            if (conf.IsDebug)
            {
                WriteLine("      <Optimization>Disabled</Optimization>");
            }
            else
            {
                WriteLine("      <InlineFunctionExpansion>AnySuitable</InlineFunctionExpansion>");
                WriteLine("      <FavorSizeOrSpeed>Speed</FavorSizeOrSpeed>");
            }
            if (!string.IsNullOrEmpty(IncludeDirectories))
            {
                Write("      <AdditionalIncludeDirectories>");
                Write(IncludeDirectories);
                WriteLine("%(AdditionalIncludeDirectories)</AdditionalIncludeDirectories>");
            }
            Write("      <AdditionalUsingDirectories>");
            Write(conf.UsingDirectories);
            if (!string.IsNullOrEmpty(conf.UsingDirectories) && !conf.UsingDirectories.EndsWith(";"))
            {
                Write(';');
            }
            WriteLine("%(AdditionalUsingDirectories)</AdditionalUsingDirectories>");
            Write("      <PreprocessorDefinitions>");
            Write(conf.Definitions);
            if (!string.IsNullOrEmpty(conf.Definitions) && !conf.Definitions.EndsWith(";"))
            {
                Write(';');
            }
            //if (!x64)
            {
                Write("WIN32;");
            }
            Write(conf.IsDebug ? "_DEBUG;" : "NDEBUG;");
            WriteLine("%(PreprocessorDefinitions)</PreprocessorDefinitions>");
            if (conf.IsDebug)
            {
                WriteLine("      <RuntimeLibrary>MultiThreadedDebugDLL</RuntimeLibrary>");
            }
            else
            {
                WriteLine("      <RuntimeLibrary>MultiThreadedDLL</RuntimeLibrary>");
                WriteLine("      <BufferSecurityCheck>false</BufferSecurityCheck>");
            }
            WriteLine("      <FloatingPointModel>Precise</FloatingPointModel>");
            WriteLine("      <PrecompiledHeader>Use</PrecompiledHeader>");
            if (conf.IsDebug)
            {
                WriteLine("      <WarningLevel>Level3</WarningLevel>");
                WriteLine("      <DebugInformationFormat>ProgramDatabase</DebugInformationFormat>");
                WriteLine("      <DisableSpecificWarnings>4793;%(DisableSpecificWarnings)</DisableSpecificWarnings>");
            }
            else
            {
                WriteLine("      <WarningLevel>Level1</WarningLevel>");
            }
            WriteLine("    </ClCompile>");
            WriteLine("    <Link>");
            if (conf.IsDebug)
            {
                WriteLine("      <AdditionalDependencies>LinearMath_Debug.lib;BulletCollision_Debug.lib;BulletDynamics_Debug.lib</AdditionalDependencies>");
                if (!string.IsNullOrEmpty(LibraryDirectoriesDebug))
                {
                    Write("      <AdditionalLibraryDirectories>");
                    Write(LibraryDirectoriesDebug);
                    WriteLine("%(AdditionalLibraryDirectories)</AdditionalLibraryDirectories>");
                }
                WriteLine("      <GenerateDebugInformation>true</GenerateDebugInformation>");
                WriteLine("      <AssemblyDebug>true</AssemblyDebug>");
            }
            else
            {
                WriteLine("      <AdditionalDependencies>LinearMath_MinSizeRel.lib;BulletCollision_MinsizeRel.lib;BulletDynamics_MinsizeRel.lib</AdditionalDependencies>");
                if (!string.IsNullOrEmpty(LibraryDirectoriesRelease))
                {
                    Write("      <AdditionalLibraryDirectories>");
                    Write(LibraryDirectoriesRelease);
                    WriteLine("%(AdditionalLibraryDirectories)</AdditionalLibraryDirectories>");
                }
            }
            if (!x64)
            {
                WriteLine("      <TargetMachine>MachineX86</TargetMachine>");
            }
            //WriteLine("      <CLRUnmanagedCodeCheck>true</CLRUnmanagedCodeCheck>");
            WriteLine("    </Link>");
            WriteLine("  </ItemDefinitionGroup>");
        }

        void OutputItemGroupReference(string referenceName, string assemblyName)
        {
            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("\t\t<AssemblyReference");
                Write("\t\t\tRelativePath=\"");
                Write(referenceName);
                WriteLine(".dll\"");
                Write("\t\t\tAssemblyName=\"");
                Write(assemblyName);
                WriteLine("\"");
                WriteLine("\t\t\tMinFrameworkVersion=\"131072\"");
                WriteLine("\t\t/>");
            }
            else
            {
                Write("    <Reference Include=\"");
                Write(referenceName);
                WriteLine("\">");
                WriteLine("      <CopyLocalSatelliteAssemblies>true</CopyLocalSatelliteAssemblies>");
                WriteLine("      <ReferenceOutputAssembly>true</ReferenceOutputAssembly>");
                WriteLine("    </Reference>");
            }
        }

        void OutputItemGroupReferenceConditional(string condition, string referenceName, string assemblyName)
        {
            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("\t<References>");
            }
            else
            {
                WriteLine("  <ItemGroup>");
            }
            OutputItemGroupReference(referenceName, assemblyName);
            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("\t</References>");
            }
            else
            {
                WriteLine("  </ItemGroup>");
            }
        }

        public void Output(TargetVS targetVS, IList<ProjectConfiguration> confs, string outDirectory)
        {
            Directory.CreateDirectory(outDirectory);
            var solutionFile = new FileStream(outDirectory + "\\" + namespaceName + ".sln", FileMode.Create, FileAccess.Write);
            solutionWriter = new StreamWriter(solutionFile, Encoding.UTF8);

            this.targetVS = targetVS;
            confs = confs.OrderBy(c => !c.IsDebug).ThenBy(c => c.Name).ToList();

            var projectGuid = new Guid("5A0DEF7E-B7E3-45E9-A511-0F03CECFF8C0");
            string projectGuidString = projectGuid.ToString().ToUpper();

            WriteLineSln();
            WriteSln("Microsoft Visual Studio Solution File, Format Version ");
            switch (targetVS)
            {
                case TargetVS.VS2008:
                    WriteLineSln("10.00");
                    WriteLineSln("# Visual C++ Express 2008");
                    break;
                case TargetVS.VS2010:
                    WriteLineSln("11.00");
                    WriteLineSln("# Visual Studio 2010");
                    break;
                case TargetVS.VS2012:
                    WriteLineSln("12.00");
                    WriteLineSln("# Visual Studio 11");
                    break;
                case TargetVS.VS2013:
                    WriteLineSln("12.00");
                    WriteLineSln("# Visual Studio 2013");
                    WriteLineSln("VisualStudioVersion = 12.0.21005.1");
                    //WriteLineSln("MinimumVisualStudioVersion = 10.0.40219.1");
                    break;
                case TargetVS.VS2015:
                    WriteLineSln("12.00");
                    WriteLineSln("# Visual Studio 14");
                    WriteLineSln("VisualStudioVersion = 14.0.23107.0");
                    //WriteLineSln("MinimumVisualStudioVersion = 10.0.40219.1");
                    break;
                default:
                    throw new NotImplementedException();
            }

            Guid vcppProjectType = new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942");
            string vcppProjectTypeString = vcppProjectType.ToString().ToUpper();
            WriteSln("Project(\"{");
            WriteSln(vcppProjectTypeString);
            WriteSln("}\") = \"");
            WriteSln(namespaceName);
            WriteSln("\", \"");
            WriteSln(namespaceName);
            if (targetVS == TargetVS.VS2008)
            {
                WriteSln(".vcproj\", \"{");
            }
            else
            {
                WriteSln(".vcxproj\", \"{");
            }
            WriteSln(projectGuidString);
            WriteLineSln("}\"");
            WriteLineSln("EndProject");

            WriteLineSln("Global");

            WriteLineSln("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            foreach (var conf in confs)
            {
                WriteSln("\t\t");
                WriteProjectConfigurationNameSln(conf);
                WriteSln("|Win32 = ");
                WriteProjectConfigurationNameSln(conf);
                WriteLineSln("|Win32");
                if (X64)
                {
                    WriteSln("\t\t");
                    WriteProjectConfigurationNameSln(conf);
                    WriteSln("|x64 = ");
                    WriteProjectConfigurationNameSln(conf);
                    WriteLineSln("|x64");
                }
            }
            WriteLineSln("\tEndGlobalSection");

            WriteLineSln("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            foreach (var conf in confs)
            {
                WriteSln("\t\t{");
                WriteSln(projectGuidString);
                WriteSln("}.");
                WriteProjectConfigurationNameSln(conf);
                WriteSln("|Win32.ActiveCfg = ");
                WriteProjectConfigurationNameSln(conf);
                WriteLineSln("|Win32");

                WriteSln("\t\t{");
                WriteSln(projectGuidString);
                WriteSln("}.");
                WriteProjectConfigurationNameSln(conf);
                WriteSln("|Win32.Build.0 = ");
                WriteProjectConfigurationNameSln(conf);
                WriteLineSln("|Win32");

                if (X64)
                {
                    WriteSln("\t\t{");
                    WriteSln(projectGuidString);
                    WriteSln("}.");
                    WriteProjectConfigurationNameSln(conf);
                    WriteSln("|x64.ActiveCfg = ");
                    WriteProjectConfigurationNameSln(conf);
                    WriteLineSln("|x64");

                    WriteSln("\t\t{");
                    WriteSln(projectGuidString);
                    WriteSln("}.");
                    WriteProjectConfigurationNameSln(conf);
                    WriteSln("|x64.Build.0 = ");
                    WriteProjectConfigurationNameSln(conf);
                    WriteLineSln("|x64");
                }
            }
            WriteLineSln("\tEndGlobalSection");

            WriteLineSln("\tGlobalSection(SolutionProperties) = preSolution");
            WriteLineSln("\t\tHideSolutionNode = FALSE");
            WriteLineSln("\tEndGlobalSection");
            WriteLineSln("EndGlobal");

            solutionWriter.Dispose();
            solutionFile.Dispose();


            string projectFilename = namespaceName + (targetVS == TargetVS.VS2008 ? ".vcproj" : ".vcxproj");
            var projectFile = new FileStream(outDirectory + "\\" + projectFilename, FileMode.Create, FileAccess.Write);
            projectWriter = new StreamWriter(projectFile, Encoding.UTF8);

            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("<?xml version=\"1.0\" encoding=\"Windows-1252\"?>");
                WriteLine("<VisualStudioProject");
                WriteLine("\tProjectType=\"Visual C++\"");
                WriteLine("\tVersion=\"9.00\"");
                Write("\tName=\"");
                Write(namespaceName);
                WriteLine("\"");
                Write("\tProjectGUID=\"{");
                Write(projectGuidString);
                WriteLine("}\"");
                Write("\tRootNamespace=\"");
                Write(namespaceName);
                WriteLine("\"");
                WriteLine("\tKeyword=\"ManagedCProj\"");
                WriteLine("	TargetFrameworkVersion=\"131072\"");
                WriteLine("\t>");
                WriteLine("\t<Platforms>");
                WriteLine("\t\t<Platform");
                WriteLine("\t\t\tName=\"Win32\"");
                WriteLine("\t\t/>");
                WriteLine("\t</Platforms>");
                WriteLine("\t<ToolFiles>");
                WriteLine("\t</ToolFiles>");
            }
            else
            {
                WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                Write("<Project DefaultTargets=\"Build\" ToolsVersion=\"");
                switch (targetVS)
                {
                    case TargetVS.VS2010:
                    case TargetVS.VS2012:
                        Write("4.0");
                        break;
                    case TargetVS.VS2013:
                        Write("12.0");
                        break;
                    case TargetVS.VS2015:
                        Write("14.0");
                        break;
                    default:
                        throw new NotImplementedException();
                }
                WriteLine("\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
            }

            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("\t<Configurations>");
            }
            else
            {
                WriteLine("  <ItemGroup Label=\"ProjectConfigurations\">");
            }
            foreach (var conf in confs)
            {
                OutputProjectConfiguration(conf, false);
                if (X64)
                {
                    OutputProjectConfiguration(conf, true);
                }
            }
            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("\t</Configurations>");
            }
            else
            {
                WriteLine("  </ItemGroup>");

                if (targetVS != TargetVS.VS2010)
                {
                    WriteLine("  <PropertyGroup Label=\"Globals\">");
                    WriteLine("    <VCTargetsPath Condition=\"\'$(VCTargetsPath11)\' != \'\' and \'$(VSVersion)\' == \'\' and \'$(VisualStudioVersion)\' == \'\'\">$(VCTargetsPath11)</VCTargetsPath>");
                    WriteLine("  </PropertyGroup>");
                }
                WriteLine("  <PropertyGroup Label=\"Globals\">");
                Write("    <ProjectGuid>{");
                Write(projectGuidString);
                WriteLine("}</ProjectGuid>");
                if (targetVS != TargetVS.VS2010)
                {
                    //WriteLine("    <TargetFrameworkVersion Condition=\"\'$(Configuration)\'==\'Debug XNA 3.1\' OR \'$(Configuration)\'==\'Release XNA 3.1\'\">v2.0</TargetFrameworkVersion>");
                    //WriteLine("    <TargetFrameworkVersion Condition=\"\'$(Configuration)\'==\'Debug XNA 4.0\' OR \'$(Configuration)\'==\'Release XNA 4.0\'\">v4.0</TargetFrameworkVersion>");
                    if (targetVS == TargetVS.VS2012)
                    {
                        WriteLine("    <TargetFrameworkVersion Condition=\"\'$(TargetFrameworkVersion)\'==\'\'\">v4.5</TargetFrameworkVersion>");
                    }
                    else if (targetVS == TargetVS.VS2013)
                    {
                        WriteLine("    <TargetFrameworkVersion Condition=\"\'$(TargetFrameworkVersion)\'==\'\'\">v4.5.1</TargetFrameworkVersion>");
                    }
                    else // if (targetVS == TargetVS.VS2015)
                    {
                        WriteLine("    <TargetFrameworkVersion Condition=\"\'$(Configuration)\'==\'Debug Numerics\' OR \'$(Configuration)\'==\'Release Numerics\'\">v4.6</TargetFrameworkVersion>");
                        WriteLine("    <TargetFrameworkVersion Condition=\"\'$(TargetFrameworkVersion)\'==\'\'\">v4.5.2</TargetFrameworkVersion>");
                    }
                }
                Write("    <RootNamespace>");
                Write(namespaceName);
                WriteLine("</RootNamespace>");
                WriteLine("    <Keyword>ManagedCProj</Keyword>");
                WriteLine("  </PropertyGroup>");


                WriteLine("  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.Default.props\" />");


                foreach (var conf in confs)
                {
                    OutputPropertyGroupConfiguration(conf, false);
                    if (X64)
                    {
                        OutputPropertyGroupConfiguration(conf, true);
                    }
                }

                WriteLine("  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.props\" />");
                WriteLine("  <ImportGroup Label=\"ExtensionSettings\">");
                WriteLine("  </ImportGroup>");


                foreach (var conf in confs)
                {
                    OutputImportGroupPropertySheets(conf);
                }


                WriteLine("  <PropertyGroup Label=\"UserMacros\" />");
                WriteLine("  <PropertyGroup>");
                WriteLine("    <_ProjectFileVersion>10.0.30319.1</_ProjectFileVersion>");
                foreach (var conf in confs)
                {
                    OutputPropertyGroupConfiguration2(conf, false);
                    if (X64)
                    {
                        OutputPropertyGroupConfiguration2(conf, true);
                    }
                }
                WriteLine("  </PropertyGroup>");


                foreach (var conf in confs)
                {
                    OutputItemDefinitionGroup(conf, false);
                    if (X64)
                    {
                        OutputItemDefinitionGroup(conf, true);
                    }
                }
            }

            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("\t<References>");
            }
            else
            {
                WriteLine("  <ItemGroup>");
            }
            OutputItemGroupReference("System", "System, Version=2.0.0.0, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL");
            OutputItemGroupReference("System.Drawing", "System.Drawing, Version=2.0.0.0, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL");
            if (targetVS == TargetVS.VS2008)
            {
                WriteLine("\t</References>");
            }
            else
            {
                WriteLine("  </ItemGroup>");
            }

            foreach (var conf in confs)
            {
                if (conf.ConditionalRef != null && conf.ConditionalRefAssembly != null)
                {
                    if (targetVS != TargetVS.VS2008)
                    {
                        WriteLine(string.Format("  <ItemGroup Condition=\"'$(Configuration)'=='{0} {1}'\">", conf.IsDebug ? "Debug" : "Release", conf.Name));
                        WriteLine(string.Format("    <Reference Include=\"{0}\">", conf.ConditionalRefAssembly));
                        WriteLine("    </Reference>");
                        WriteLine("  </ItemGroup>");
                    }
                }
            }


            if (targetVS == TargetVS.VS2008)
            {
                filterWriter.Output2008(projectWriter, confs, outDirectory);

                WriteLine("\t<Globals>");
                WriteLine("\t</Globals>");
                WriteLine("</VisualStudioProject>");
            }
            else
            {
                WriteLine("  <ItemGroup>");
                var sourceFiles = filterWriter.RootFilter.GetChild("Source Files").GetFileList();
                foreach (var sourceFile in sourceFiles)
                {
                    Write("    <ClCompile Include=\"");
                    Write(sourceFile);
                    if (sourceFile.EndsWith("Stdafx", StringComparison.InvariantCultureIgnoreCase))
                    {
                        WriteLine(".cpp\">");
                        foreach (var conf in confs)
                        {
                            Write("      <PrecompiledHeader Condition=\"'$(Configuration)|$(Platform)'=='");
                            WriteProjectConfigurationName(conf);
                            WriteLine("|Win32'\">Create</PrecompiledHeader>");
                            if (X64)
                            {
                                Write("      <PrecompiledHeader Condition=\"'$(Configuration)|$(Platform)'=='");
                                WriteProjectConfigurationName(conf);
                                WriteLine("|x64'\">Create</PrecompiledHeader>");
                            }
                        }
                        WriteLine("    </ClCompile>");
                    }
                    else
                    {
                        WriteLine(".cpp\" />");
                    }
                }
                WriteLine("  </ItemGroup>");

                WriteLine("  <ItemGroup>");
                var resourceFiles = filterWriter.RootFilter.GetChild("Resource Files").GetFileList();
                foreach (var resourceFile in resourceFiles)
                {
                    Write("    <ResourceCompile Include=\"");
                    Write(resourceFile);
                    WriteLine("\" />");
                }
                WriteLine("  </ItemGroup>");

                WriteLine("  <ItemGroup>");
                var headerFiles = filterWriter.RootFilter.GetChild("Header Files").GetFileList();
                foreach (var headerFile in headerFiles)
                {
                    Write("    <ClInclude Include=\"");
                    Write(headerFile);
                    WriteLine(".h\" />");
                }
                WriteLine("  </ItemGroup>");

                WriteLine("  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.targets\" />");
                WriteLine("  <ImportGroup Label=\"ExtensionTargets\">");
                WriteLine("  </ImportGroup>");

                Write("</Project>");
            }

            projectWriter.Dispose();
            projectFile.Dispose();


            if (FilterWriter != null && targetVS != TargetVS.VS2008)
            {
                FilterWriter.Output(targetVS, outDirectory);
            }
        }
    }
}
