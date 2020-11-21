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
            var result = EBuildIn.Modules.Find("Console");

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFindNotFound()
        {
            // act
            var result = EBuildIn.Modules.Find("bla");

            // assert
            Assert.IsFalse(result);
        }
    }
}
