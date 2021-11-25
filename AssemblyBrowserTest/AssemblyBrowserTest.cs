using NUnit.Framework;
using AssemblyAnalyzer.Containers;
using System.Collections.Generic;
using AssemblyAnalyzer;
using System;
using System.IO;

namespace AssemblyBrowserTest
{
    public class AssemblyBrowserTest
    {
        private const string _testAssemblyPath = "/resource/UIntGeneratorPlugin.dll";
        private AssemblyBrowser _assemblyBrowser;
        private List<Container> _assemblyInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            _assemblyBrowser = new AssemblyBrowser();
            _assemblyInfo = _assemblyBrowser.GetAssemblyInfo(AppDomain.CurrentDomain.BaseDirectory + _testAssemblyPath);
        }

        [Test]
        public void AssemblyIsNotEmptyTest()
        {
            int expected = 0;
            int actual = _assemblyInfo.Count;
            Assert.AreNotEqual(expected, actual);
        } 

        [Test]
        public void AssemblyHasCorrectClassesNumberTest()
        {
            int expected = 1;
            int actual = _assemblyInfo[0].Members.Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AssemblyClassHasCorrectMembersNumberTest()
        {
            int expected = 4;
            Container container = (Container) _assemblyInfo[0].Members[0];
            int actual = container.Members.Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AssemblyClassHasMemberGetObjectTest()
        {
            string memberGetObject = "public virtual Object GetRandomValue ()";
            Container container = (Container)_assemblyInfo[0].Members[0];
            bool hasMemberGetObject = false;
            foreach (MemberInfo member in container.Members)
            {
                if (member.Signature.Equals(memberGetObject))
                {
                    hasMemberGetObject = true;
                    break;
                }
            }
            Assert.IsTrue(hasMemberGetObject);
        }

        [Test]
        public void AssemblyClassHasMemberConstructorTest()
        {
            string memberConstructor = "public .ctor ()";
            Container container = (Container)_assemblyInfo[0].Members[0];
            bool hasMemberConstructor = false;
            foreach (MemberInfo member in container.Members)
            {
                if (member.Signature.Equals(memberConstructor))
                {
                    hasMemberConstructor = true;
                    break;
                }
            }
            Assert.IsTrue(hasMemberConstructor);
        }

        [Test]
        public void AssemblyClassHasMemberGeneratorTypePropTest()
        {
            string memberGeneratorTypeProp = "public Type GeneratorType { public get_GeneratorType; }";
            Container container = (Container)_assemblyInfo[0].Members[0];
            bool hasMemberGeneratorTypeProp = false;
            foreach (MemberInfo member in container.Members)
            {
                if (member.Signature.Equals(memberGeneratorTypeProp))
                {
                    hasMemberGeneratorTypeProp = true;
                    break;
                }
            }
            Assert.IsTrue(hasMemberGeneratorTypeProp);
        }

        [Test]
        public void AssemblyClassHasMemberGetGeneratorTypeTest()
        {
            string memberGetGeneratorType = "public virtual Type get_GeneratorType ()";
            Container container = (Container)_assemblyInfo[0].Members[0];
            bool hasMemberGetGeneratorType = false;
            foreach (MemberInfo member in container.Members)
            {
                if (member.Signature.Equals(memberGetGeneratorType))
                {
                    hasMemberGetGeneratorType = true;
                    break;
                }
            }
            Assert.IsTrue(hasMemberGetGeneratorType);
        }

        [Test]
        public void IncorrectAssemblyPathTest()
        {
            string path = "../qwe//Assembly.dll";
            Assert.Throws<FileNotFoundException>(() => _assemblyBrowser.GetAssemblyInfo(path));
        }
    }
}