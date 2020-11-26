using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildInTests
{
    [TestClass]
    public class ModulesTests
    {
        [TestMethod]
        public void TestFindFound()
        {
            // act
            var result = EBuildIn.Modules.FindFunctionAndReturnParameters("Console", "WriteLine");

            // assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("text", result.Single());
        }

        [TestMethod]
        public void TestFindNotFound()
        {
            // act
            var result = EBuildIn.Modules.FindFunctionAndReturnParameters("Test", "Test");

            // assert
            Assert.IsNull(result);
        }
    }
}
