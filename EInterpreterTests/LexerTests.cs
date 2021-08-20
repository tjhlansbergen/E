using EInterpreter.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var result = lexer.GetTree(new[] { "// comment" });

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, lexer.Tokens.Count);
        }

        [TestMethod]
        public void TestGetTree()
        {
            // arrange
            var lexer = new Lexer();

            // act
            var result = lexer.GetTree(new[] { "// comment", "Constant Boolean Test = true" });

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Constants.Count);
            Assert.AreEqual(2, lexer.Tokens.Count);
        }
    }
}
