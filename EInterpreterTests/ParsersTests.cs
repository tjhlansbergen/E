using System;
using EInterpreter.EElements;
using EInterpreter.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreterTests
{
    [TestClass]
    public class ParsersTests
    {
        [TestMethod]
        public void ParsersShouldSucceed_ParseConstant()
        {
            // act
            var result = Parsers.ParseConstant("Constant boolean Test = true");

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EConstant));
        }

        [TestMethod]
        public void ParsersShouldFail_ParseConstant()
        {
            // assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => Parsers.ParseConstant("test"));
        }

        // TODO all Parsers (x2)
    }
}
