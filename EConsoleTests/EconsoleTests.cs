using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EConsoleTests
{
    [TestClass]
    public class EconsoleTests
    {
        [TestMethod]
        public void TestIsValidTextFile()
        {
            // arrange
            var path = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent?.Parent?.Parent?.FullName, "TestScripts");

            // assert
            foreach (var file in Directory.GetFiles(path))
            {
                Assert.IsTrue(EConsole.Helpers._isValidTextFileAsync(file));
            }
        }
    }
}
