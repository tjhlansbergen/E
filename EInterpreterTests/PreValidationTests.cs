using System;
using System.Collections.Generic;
using EInterpreter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreterTests
{
    [TestClass]
    public class PreValidationTests
    {
        [TestMethod]
        public void ValidationShouldSucceed_Empty()
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep>());

            // act
            var result = validator.Validate(new[] {""}, false);

            // assert
            Assert.IsTrue(result, "Validation should succeed if no steps are specified");
        }

        [TestMethod]
        [DataRow("{")]
        [DataRow("}" )]
        [DataRow(";" )]
        [DataRow(")" )]
        [DataRow("// comment" )]
        public void ValidationShouldSucceed_HasValidLineEnds(string line)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasValidLineEndsStep() });

            // act
            var result = validator.Validate(new [] { line }, false);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("Invalid line!")]
        public void ValidationShouldFail_HasValidLineEnds(string line)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasValidLineEndsStep() });

            // act
            var result = validator.Validate(new[] { line }, false);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(new [] { "\"\"" })]
        [DataRow(new[] { "\"hi!\"", "\"there\"" })]
        public void ValidationShouldSucceed_HasMatchingQuotes(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasMatchingQuotes() });

            // act
            var result = validator.Validate(lines, false);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow(new[] { "\"" })]
        [DataRow(new[] { "\"hi!\"", "\"there\"\"" })]
        public void ValidationShouldFail_HasMatchingQuotes(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasMatchingQuotes() });

            // act
            var result = validator.Validate(lines, false);

            // assert
            Assert.IsFalse(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "{}" })]
        [DataRow(new[] { "()" })]
        [DataRow(new[] { "[]" })]
        [DataRow(new[] { "<>" })]
        [DataRow(new[] { "{","}" })]
        [DataRow(new[] { "(","...",")" })]
        [DataRow(new[] { "[","<","]",">" })]
        [DataRow(new[] { "<{()][}>", "><{(][})" })]
        public void ValidationShouldSucceed_HasMatchingBraces(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasMatchingBraces() });

            // act
            var result = validator.Validate(lines, false);

            // assert
            Assert.IsTrue(result, $"Testcase {string.Join(" ",lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "{" })]
        [DataRow(new[] { "((" })]
        [DataRow(new[] { "[][" })]
        [DataRow(new[] { "<>...<>...", "<...<>" })]
        [DataRow(new[] { "<{()]", "[}>>" })]
        public void ValidationShouldFail_HasMatchingBraces(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasMatchingBraces() });

            // act
            var result = validator.Validate(lines, false);

            // assert
            Assert.IsFalse(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "Utility Program" })]
        public void ValidationShouldSucceed_ContainsProgramUtility(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ContainsProgramUtility() });

            // act
            var result = validator.Validate(lines, false);

            // assert
            Assert.IsTrue(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "Utility" })]
        [DataRow(new[] { "Program" })]
        [DataRow(new[] { "UtilityProgram" })]
        [DataRow(new[] { "Utility", "Program" })]
        public void ValidationShouldFail_ContainsProgramUtility(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ContainsProgramUtility() });

            // act
            var result = validator.Validate(lines, false);

            // assert
            Assert.IsFalse(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "Function boolean Start" })]
        public void ValidationShouldSucceed_ContainsStartFunction(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ContainsStartFunction() });

            // act
            var result = validator.Validate(lines, false);

            // assert
            Assert.IsTrue(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "Function Start" })]
        [DataRow(new[] { "boolean Start" })]
        public void ValidationShouldFail_ContainsStartFunction(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ContainsStartFunction() });

            // act
            var result = validator.Validate(lines, false);

            // assert
            Assert.IsFalse(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        public void ValidationShouldSucceed_BlockOpeningsOk()
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new BlockOpeningsOk() });
            var result = false;

            // act
            foreach (var block in PreValidator.Blocks)
            {
                result = validator.Validate(new[] { block, "{" }, false);
            }

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("(")]
        [DataRow("[")]
        [DataRow(" ")]
        [DataRow("\n")]
        [DataRow("\r\n")]
        public void ValidationShouldFail_BlockOpeningsOk(string nextLine)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new BlockOpeningsOk() });
            var result = false;

            // act
            foreach (var block in PreValidator.Blocks)
            {
                result = validator.Validate(new[] { block, nextLine }, false);
            }

            // assert
            Assert.IsFalse(result, $"Testcase {nextLine} failed");
        }

        [TestMethod]
        [DataRow("Utility Parser")]
        [DataRow("Function List<Thing> GetThings()")]
        [DataRow("Object Car")]
        public void ValidationShouldSucceed_BlockDeclarationsOk(string block)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new BlockDeclarationsOk() });

            // act
            var result = validator.Validate(new[] { block }, false);
            
            // assert
            Assert.IsTrue(result, $"Testcase {block} failed");
        }

        [TestMethod]
        [DataRow("Utility String Parser")]
        [DataRow("Function GetThings()")]
        [DataRow("Object bool Car")]
        public void ValidationShouldFail_BlockDeclarationsOk(string block)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new BlockDeclarationsOk() });

            // act
            var result = validator.Validate(new[] { block }, false);

            // assert
            Assert.IsFalse(result, $"Testcase {block} failed");
        }

        [TestMethod]
        [DataRow("Constant boolean PayMore = true")]
        [DataRow("Constant number TheAnswer = 42")]
        public void ValidationShouldSucceed_ConstantsOk(string constant)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ConstantsOk() });

            // act
            var result = validator.Validate(new[] { constant }, false);

            // assert
            Assert.IsTrue(result, $"Testcase {constant} failed");
        }

        [TestMethod]
        [DataRow("Constant boolean PayMore")]
        [DataRow("Constant TheAnswer = 42")]
        [DataRow("Constant text = \"mytext\"")]
        public void ValidationShouldFail_ConstantsOk(string constant)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ConstantsOk() });

            // act
            var result = validator.Validate(new[] { constant }, false);

            // assert
            Assert.IsFalse(result, $"Testcase {constant} failed");
        }

        [TestMethod]
        [DataRow("Property boolean Test")]
        [DataRow("Property number TestNum")]
        public void ValidationShouldSucceed_PropertiesOk(string prop)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new PropertiesOk() });

            // act
            var result = validator.Validate(new[] { prop }, false);

            // assert
            Assert.IsTrue(result, $"Testcase {prop} failed");
        }

        [TestMethod]
        [DataRow("Property boolean")]
        [DataRow("Property number TestNum = 42")]
        public void ValidationShouldFail_PropertiesOk(string prop)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new PropertiesOk() });

            // act
            var result = validator.Validate(new[] { prop }, false);

            // assert
            Assert.IsFalse(result, $"Testcase {prop} failed");
        }
    }
}
