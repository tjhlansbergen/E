﻿using System.Collections.Generic;
using System.Linq;
using EInterpreter;
using EInterpreter.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreterTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestParseEmpty()
        {
            // arrange
            var parser = new Parser();

            // act
            var result = parser.Parse(new List<EToken>());

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestParse()
        {
            // arrange
            var parser = new Parser();

            // act
            var result = parser.Parse(new List<EToken>{ new EToken(1, ETokenType.CONSTANT, "Constant Boolean Test = true") });

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Constants.Any());
            Assert.AreEqual(1, result.Constants.Count);
        }
    }
}
