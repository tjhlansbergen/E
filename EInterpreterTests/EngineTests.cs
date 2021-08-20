using System.Collections.Generic;
using EInterpreter.EElements;
using EInterpreter.EObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreterTests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void RunShouldFail()
        {
            // arrange
            var engine = new EInterpreter.Engine.Engine();
            var tree = new ETree
            {
                Constants = new List<EConstant> {new EConstant("test", "Test1", "")},
                Utilities = new List<EUtility> {new EUtility("Test2")}
            };

            // assert
            Assert.ThrowsException<EInterpreter.EngineException>(() => engine.Run(tree));
        }

        [TestMethod]
        public void RunShouldSucceed()
        {
            // arrange
            var engine = new EInterpreter.Engine.Engine();

            var tree = new ETree
            {
                Utilities = new List<EUtility>
                {
                    new EUtility("Program")
                    {
                        Functions = new List<EFunction>
                        {
                            new EFunction("Boolean", "Program.Start", new List<EProperty> {new EProperty("Text", "arguments")})
                            {
                                Elements = { new EReturn("", "true")}
                            }
                        }
                    }
                }
            };

            // act
            engine.Run(tree);
        }
    }
}
