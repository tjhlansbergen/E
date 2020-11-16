using System;
using System.Diagnostics;
using System.IO;
using EInterpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreterTests
{
    [TestClass]
    public class WorkerTests
    {
        [TestMethod]
        public void TestGo()
        {
            // arrange
            var worker = new Worker();

            // act
            worker.Go(new []{"// test"}, "test");

            // assert
            // nothing to assert, we are just checking if Go eats it's own exceptions at all times
        }

        [TestMethod]
        public void TestWorkerCustomOutputChannel()
        {
            // arrange
            var stringWriter = new StringWriter();
            var worker = new Worker(stringWriter);

            // act
            worker.Go(new[] { "// test" }, "test");

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(stringWriter.ToString()));
        }

        [TestMethod]
        [DataRow("Pre-validation")]
        [DataRow("Lexing")]
        [DataRow("Post-validation")]
        public void TestWorkerOutput(string content)
        {
            // arrange
            var stringWriter = new StringWriter();
            var worker = new Worker(stringWriter);

            // act
            worker.Go(new[] { "// test" }, "test");

            // assert
            Assert.IsTrue(stringWriter.ToString().Contains(content));
        }

    }
}
