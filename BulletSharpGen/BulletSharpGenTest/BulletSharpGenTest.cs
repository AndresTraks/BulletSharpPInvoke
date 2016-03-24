using System.Linq;
using BulletSharpGen;
using NUnit.Framework;

namespace BulletSharpGenTest
{
    [TestFixture]
    class BulletSharpGenTest
    {
        [Test]
        public void Cpp1()
        {
            var project = new WrapperProject();
            project.NamespaceName = "Cpp1";
            project.ProjectFilePath = "Cpp1/cpp1.xml";
            project.SourceRootFolders.Add(".");
            project.ReadCpp();
            project.Save();

            var parser = new DefaultParser(project);
            parser.Parse();

            project.CProjectPath = "Cpp1_wrap";
            project.CsProjectPath = "Cpp1_wrap";
            var writer = new PInvokeWriter(project);
            writer.Output();

            Assert.AreEqual(1, project.HeaderDefinitions.Count);
            Assert.AreEqual(1, project.ClassDefinitions.Count);
            var cppClass1 = project.ClassDefinitions.First().Value;
            Assert.AreEqual("CppClass1", cppClass1.Name);
            Assert.AreEqual("CppClass1", cppClass1.ManagedName);
            Assert.AreEqual(2, cppClass1.Methods.Count);
            Assert.AreEqual("Empty", cppClass1.Methods[1].ManagedName);
        }

        [Test]
        public void CppTemplate()
        {
            var project = new WrapperProject();
            project.NamespaceName = "CppTemplate";
            project.ProjectFilePath = "CppTemplate/cpp_template.xml";
            project.SourceRootFolders.Add(".");
            project.ReadCpp();
            project.Save();

            var parser = new DefaultParser(project);
            parser.Parse();

            project.CProjectPath = "CppTemplate_wrap";
            project.CsProjectPath = "CppTemplate_wrap";
            var writer = new PInvokeWriter(project);
            writer.Output();

            var cmake = new CMakeWriter(project);
            cmake.Output();

            Assert.AreEqual(1, project.HeaderDefinitions.Count);
            Assert.AreEqual(4, project.ClassDefinitions.Count);
            /*
            var cppClass1 = project.ClassDefinitions.First().Value;
            Assert.AreEqual("CppClass1", cppClass1.Name);
            Assert.AreEqual("CppClass1", cppClass1.ManagedName);
            Assert.AreEqual(2, cppClass1.Methods.Count);
            Assert.AreEqual("Empty", cppClass1.Methods[1].ManagedName);*/
        }
    }
}
