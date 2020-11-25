using System;
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

        // TODO (1) should succeed test (nb full scripts should be tested in WorkerTests
    }
}
