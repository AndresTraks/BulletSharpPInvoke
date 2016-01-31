using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    class CMakeWriter
    {
        private Dictionary<string, HeaderDefinition> headerDefinitions;
        private string namespaceName;

        StreamWriter cmakeWriter;

        public CMakeWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
        {
            this.headerDefinitions = headerDefinitions;
            this.namespaceName = namespaceName;
        }

        void Write(string s)
        {
            cmakeWriter.Write(s);
        }

        void WriteLine(string s)
        {
            cmakeWriter.WriteLine(s);
        }

        void WriteLine()
        {
            cmakeWriter.WriteLine();
        }

        public void Output()
        {
            string outDirectoryC = namespaceName + "_c";

            Directory.CreateDirectory(outDirectoryC);

            // C++ header file (includes all other headers)
            string cmakeFilename = "CMakeLists.txt";
            var cmakeFile = new FileStream(outDirectoryC + "\\" + cmakeFilename, FileMode.Create, FileAccess.Write);
            cmakeWriter = new StreamWriter(cmakeFile);

            WriteLine("CMAKE_MINIMUM_REQUIRED (VERSION 2.6)");
            WriteLine("PROJECT (libbulletc)");
            WriteLine();
            WriteLine("IF (NOT CMAKE_BUILD_TYPE)");
            WriteLine("# SET(CMAKE_BUILD_TYPE \"Debug\")");
            WriteLine(" SET(CMAKE_BUILD_TYPE \"MinSizeRel\")");
            WriteLine("ENDIF (NOT CMAKE_BUILD_TYPE)");
            WriteLine();
            WriteLine("FIND_PATH(BULLET_INCLUDE_DIR NAMES btBulletCollisionCommon.h");
            WriteLine("  PATHS");
            WriteLine("  ${PROJECT_SOURCE_DIR}/../../bullet/src/");
            WriteLine("  ${PROJECT_SOURCE_DIR}/../bullet/src/");
            WriteLine("  ${PROJECT_SOURCE_DIR}/../../bullet3/src/");
            WriteLine("  ${PROJECT_SOURCE_DIR}/../bullet3/src/");
            WriteLine("  ENV CPATH");
            WriteLine("  /usr/include");
            WriteLine("  /usr/local/include");
            WriteLine("  /opt/local/include");
            WriteLine("  NO_DEFAULT_PATH");
            WriteLine(")");
            WriteLine("INCLUDE_DIRECTORIES(${BULLET_INCLUDE_DIR})");
            WriteLine("INCLUDE_DIRECTORIES(${BULLET_INCLUDE_DIR}/../Extras/Serialize/BulletWorldImporter/)");
            WriteLine();

            WriteLine("IF(${CMAKE_GENERATOR} MATCHES \"Unix Makefiles\")");
            WriteLine("    SET(BULLET_LIB_DIR ${BULLET_INCLUDE_DIR}/..)");
            WriteLine("    LINK_DIRECTORIES(${BULLET_LIB_DIR}/build/src/BulletCollision)");
            WriteLine("    LINK_DIRECTORIES(${BULLET_LIB_DIR}/build/src/BulletDynamics)");
            WriteLine("    LINK_DIRECTORIES(${BULLET_LIB_DIR}/build/src/BulletSoftBody)");
            WriteLine("    LINK_DIRECTORIES(${BULLET_LIB_DIR}/build/src/LinearMath)");
            WriteLine("    LINK_DIRECTORIES(${BULLET_LIB_DIR}/build/Extras/Serialize/BulletFileLoader)");
            WriteLine("    LINK_DIRECTORIES(${BULLET_LIB_DIR}/build/Extras/Serialize/BulletWorldImporter)");
            WriteLine("    LINK_DIRECTORIES(${BULLET_LIB_DIR}/build/Extras/Serialize/BulletXmlWorldImporter)");
            WriteLine("    SET(BULLETC_LIB bulletc)");
            WriteLine("ELSE()");
            WriteLine("    IF(${CMAKE_GENERATOR} MATCHES \"Visual Studio 9 2008\")");
            WriteLine("        SET(REL_LIB_DIR msvc/2008)");
            WriteLine("    ELSEIF(${CMAKE_GENERATOR} MATCHES \"Visual Studio 10\")");
            WriteLine("        SET(REL_LIB_DIR msvc/2010)");
            WriteLine("    ELSEIF(${CMAKE_GENERATOR} MATCHES \"Visual Studio 11\")");
            WriteLine("        SET(REL_LIB_DIR msvc/2012)");
            WriteLine("    ELSEIF(${CMAKE_GENERATOR} MATCHES \"Visual Studio 12\")");
            WriteLine("        SET(REL_LIB_DIR msvc/2013)");
            WriteLine("    ELSEIF(${CMAKE_GENERATOR} MATCHES \"Visual Studio 14\")");
            WriteLine("        SET(REL_LIB_DIR msvc/2015)");
            WriteLine("    ENDIF()");
            WriteLine("    LINK_DIRECTORIES(${BULLET_INCLUDE_DIR}/../${REL_LIB_DIR}/lib/${CMAKE_CFG_INTDIR})");
            WriteLine("    SET(BULLETC_LIB libbulletc)");
            WriteLine("    SET(LIB_SUFFIX _${CMAKE_CFG_INTDIR}.lib)");
            WriteLine("ENDIF()");
            WriteLine();

            WriteLine("IF(MSVC)");
            WriteLine("    IF (CMAKE_CL_64)");
            WriteLine("        ADD_DEFINITIONS(-D_WIN64)");
            WriteLine("    ELSE()");
            WriteLine("        OPTION(USE_MSVC_SSE \"Use MSVC /arch:sse option\" ON)");
            WriteLine("        IF (USE_MSVC_SSE)");
            WriteLine("            ADD_DEFINITIONS(/arch:SSE)");
            WriteLine("        ENDIF()");
            WriteLine("    ENDIF()");
            WriteLine("    OPTION(USE_MSVC_FAST_FLOATINGPOINT \"Use MSVC /fp:fast option\" ON)");
            WriteLine("    IF (USE_MSVC_FAST_FLOATINGPOINT)");
            WriteLine("        ADD_DEFINITIONS(/fp:fast)");
            WriteLine("    ENDIF()");
            WriteLine("ENDIF(MSVC)");
            WriteLine();

            AddLibrary();
            WriteLine();

            WriteLine("TARGET_LINK_LIBRARIES(${BULLETC_LIB} BulletCollision${LIB_SUFFIX})");
            WriteLine("TARGET_LINK_LIBRARIES(${BULLETC_LIB} BulletDynamics${LIB_SUFFIX})");
            WriteLine("TARGET_LINK_LIBRARIES(${BULLETC_LIB} LinearMath${LIB_SUFFIX})");
            WriteLine("TARGET_LINK_LIBRARIES(${BULLETC_LIB} BulletFileLoader${LIB_SUFFIX})");
            WriteLine("TARGET_LINK_LIBRARIES(${BULLETC_LIB} BulletSoftBody${LIB_SUFFIX})");
            WriteLine("TARGET_LINK_LIBRARIES(${BULLETC_LIB} BulletWorldImporter${LIB_SUFFIX})");
            WriteLine("TARGET_LINK_LIBRARIES(${BULLETC_LIB} BulletXmlWorldImporter${LIB_SUFFIX})");

            cmakeWriter.Dispose();
            cmakeFile.Dispose();
        }

        void AddLibrary()
        {
            List<string> sources = new List<string>();
            var headers = headerDefinitions.Values.Where(h => h.Classes.Any()).OrderBy(x => x.Name);

            foreach (HeaderDefinition header in headers)
            {
                sources.Add(header.Name + "_wrap.cpp");
                sources.Add(header.Name + "_wrap.h");
            }
            //sources.OrderBy(x => x.Na);

            WriteLine("ADD_LIBRARY(${BULLETC_LIB} SHARED");
            WriteSources(new[]
            {
                "dllmain.cpp"
            });
            WriteSources(new[]
            {
                "conversion.h",
                "main.h",
                "collections.cpp",
                "collections.h"
            }, "src");
            WriteSources(sources, "src");
            WriteLine(")");
        }

        void WriteSources(IEnumerable<string> files, string directory)
        {
            foreach (string file in files)
            {
                WriteLine(string.Format("    {0}/{1}", directory, file));
            }
        }

        void WriteSources(IEnumerable<string> files)
        {
            foreach (string file in files)
            {
                WriteLine(string.Format("    {0}", file));
            }
        }
    }
}
