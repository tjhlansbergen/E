using System;
using System.Runtime.Serialization.Formatters;
using EInterpreter;
using EInterpreter.EElements;
using EInterpreter.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreterTests
{
    [TestClass]
    public class ParsersTests
    {
        [TestMethod]
        [DataRow("Constant boolean Test = \"True\"")]
        [DataRow("Constant text Test = \"Some text\"")]
        [DataRow("Constant number Test = 42.2")]
        public void ParsersShouldSucceed_ParseConstant(string line)
        {
            // act
            var result = Parsers.ParseConstant(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EConstant));
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Constant Test")]
        [DataRow("Constant Test = test")]
        [DataRow("Constant boolean Test")]
        public void ParsersShouldFail_ParseConstant(string line)
        {
            // assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => Parsers.ParseConstant(line));
        }

        [TestMethod]
        [DataRow("Property boolean Test")]
        [DataRow("Number 42")]
        public void ParsersShouldSucceed_ParseProperty(string line)
        {
            // act
            var result = Parsers.ParseProperty(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EProperty));
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Property Test = test")]
        public void ParsersShouldFail_ParseProperty(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseProperty(line));
        }

        [TestMethod]
        [DataRow("Object Test")]
        public void ParsersShouldSucceed_ParseObject(string line)
        {
            // act
            var result = Parsers.ParseObject("Test.", line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EObject));
            Assert.AreEqual("Test.Test", result.Name);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Object")]
        [DataRow("Test Object")]
        [DataRow("Utility Test")]
        public void ParsersShouldFail_ParseObject(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseObject("Test.", line));
        }

        [TestMethod]
        [DataRow("Utility Test")]
        public void ParsersShouldSucceed_ParseUtility(string line)
        {
            // act
            var result = Parsers.ParseUtility("Test.", line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EUtility));
            Assert.AreEqual("Test.Test", result.Name);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Utility")]
        [DataRow("Test Utility")]
        [DataRow("Object Test")]
        public void ParsersShouldFail_ParseUtility(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseUtility("Test.", line));
        }

        [TestMethod]
        [DataRow("if(True)")]
        [DataRow("foreach(item in list)")]
        public void ParsersShouldSucceed_ParseStatement(string line)
        {
            // act
            var result = Parsers.ParseStatement(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EStatement));
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("if")]
        [DataRow("foreach")]
        public void ParsersShouldFail_ParseStatement(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseStatement(line));
        }

        [TestMethod]
        [DataRow("Function boolean SomeFunction()", 0)]
        [DataRow("Function number GetAge(text birthdate)", 1)]
        [DataRow("Function number GetAge(text birthdate, boolean inDays)", 2)]
        public void ParsersShouldSucceed_ParseFunction(string line, int numberOfParameters)
        {
            // act
            var result = Parsers.ParseFunction("", line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EFunction));
            Assert.AreEqual(numberOfParameters, result.Parameters.Count);
            Assert.IsTrue(line.SplitClean(' ')[2].StartsWith(result.Name));
        }

        [TestMethod]
        [DataRow("Function Test")]
        [DataRow("Function boolean Test")]
        [DataRow("boolean Test()")]
        public void ParsersShouldFail_ParseFunction(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseFunction("", line));
        }

        [TestMethod]
        [DataRow("Console:WriteLine()")]
        [DataRow("Console:WriteLine(\"some text to write\")")]
        public void ParsersShouldSucceed_ParseFunctionCall(string line)
        {
            // act
            var result = Parsers.ParseFunctionCall(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EFunctionCall));
        }

        [TestMethod]
        [DataRow("Console.WriteLine()")]
        [DataRow("Console:WriteLine")]
        [DataRow("WriteLine()")]
        public void ParsersShouldFail_ParseFunctionCall(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseFunctionCall(line));
        }

        [TestMethod]
        [DataRow("new Message message")]
        [DataRow("new number Age")]
        public void ParsersShouldSucceed_ParseDeclaration(string line)
        {
            // act
            var result = Parsers.ParseDeclaration(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EDeclaration));
        }

        [TestMethod]
        [DataRow("boolean Test")]
        [DataRow("new number")]
        [DataRow("new number Age = 42")]
        public void ParsersShouldFail_ParseDeclaration(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseDeclaration(line));
        }

        // TODO all Parsers (x2)
    }
}
