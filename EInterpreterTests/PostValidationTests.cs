﻿using System.Collections.Generic;
using EInterpreter.EElements;
using EInterpreter.EObjects;
using EInterpreter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreterTests
{
    [TestClass]
    public class PostValidationTests
    {
        [TestMethod]
        public void ValidationShouldSucceed_Empty()
        {
            // arrange
            var validator = new PostValidator(new List<IPostValidationStep>());

            // act
            var result = validator.Validate(new ETree());

            // assert
            Assert.IsTrue(result, "Validation should succeed if no steps are specified");
        }

        [TestMethod]
        public void ValidationShouldSucceed_GlobalIdentifiersAreUnique()
        {
            // arrange
            var validator = new PostValidator(new List<IPostValidationStep>{ new GlobalIdentifiersAreUnique()});

            // act
            var result = validator.Validate(new ETree{Constants = new List<EConstant>{new EConstant("test", "Test1", "")}, Utilities = new List<EUtility>{new EUtility("Test2")}});

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidationShouldFail_GlobalIdentifiersAreUnique()
        {
            // arrange
            var validator = new PostValidator(new List<IPostValidationStep> { new GlobalIdentifiersAreUnique() });

            // act
            var result = validator.Validate(new ETree { Constants = new List<EConstant> { new EConstant("test", "Test1", "") }, Utilities = new List<EUtility> { new EUtility("Test1") } });

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationShouldSucceed_ObjectIdentifiersAreUnique()
        {
            // arrange
            var validator = new PostValidator(new List<IPostValidationStep> { new ObjectIdentifiersAreUnique() });

            // act
            var result = validator.Validate(new ETree 
                { 
                    Objects = new List<EObject>
                    {
                        new EObject("Test")
                        {
                            Properties = new List<EProperty>
                            {
                                new EProperty("test", "Test1"),
                                new EProperty("test", "Test2")
                            }
                        }
                    }
                });

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidationShouldFail_ObjectIdentifiersAreUnique()
        {
            // arrange
            var validator = new PostValidator(new List<IPostValidationStep> { new ObjectIdentifiersAreUnique() });

            // act
            var result = validator.Validate(new ETree
            {
                Objects = new List<EObject>
                {
                    new EObject("Test")
                    {
                        Properties = new List<EProperty>
                        {
                            new EProperty("test", "Test1"),
                            new EProperty("test", "Test1")
                        }
                    }
                }
            });

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidationShouldSucceed_UtilityIdentifiersAreUnique()
        {
            // arrange
            var validator = new PostValidator(new List<IPostValidationStep> { new UtilityIdentifiersAreUnique() });

            // act
            var result = validator.Validate(new ETree
            {
                Utilities = new List<EUtility>
                {
                    new EUtility("Test")
                    {
                        Functions = new List<EFunction>
                        {
                            new EFunction("void", "Test1", null),
                            new EFunction("void", "Test2", null)
                        }
                    }
                }
            });

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidationShouldFail_UtilityIdentifiersAreUnique()
        {
            // arrange
            var validator = new PostValidator(new List<IPostValidationStep> { new UtilityIdentifiersAreUnique() });

            // act
            var result = validator.Validate(new ETree
            {
                Utilities = new List<EUtility>
                {
                    new EUtility("Test")
                    {
                        Functions = new List<EFunction>
                        {
                            new EFunction("void", "Test1", null),
                            new EFunction("void", "Test1", null)
                        }
                    }
                }
            });

            // assert
            Assert.IsFalse(result);
        }
    }
}
