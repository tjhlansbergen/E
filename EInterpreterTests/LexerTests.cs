using EInterpreter.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EInterpreterTests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void TestGetTreeEmpty()
        {
            // arrange
            var lexer = new Lexer();

            // act
            var result = lexer.GetTree(new[] { "// comment" }, false);

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetTree()
        {
            // arrange
            var lexer = new Lexer();

            // act
            var result = lexer.GetTree(new[] { "// comment", "Constant boolean Test = true" }, false);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Constants.Any());
        }
    }
}
