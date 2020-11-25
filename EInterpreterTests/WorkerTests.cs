﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

        [TestMethod]
        [DataRow(new[] { "Hello World!" }, "hello_world.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_constant.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_function.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_parameter.e")]
        public void TestWorkerFullScripts(string[] shouldContain, string name)
        {
            // arrange
            var stringWriter = new StringWriter();
            var worker = new Worker(stringWriter);

            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"WorkerTestScripts\\{name}");
            var lines = File.ReadAllLines(path);

            var shouldContainComplete = shouldContain.Concat(new[] { $"Pre-validation for `{name}` successful", $"Post-validation for `{name}` successful", "ran for", "returned"});

            // act
            worker.Go(lines, name);

            // assert
            Assert.IsFalse(stringWriter.ToString().Contains("Runtime error"), $"Runtime error while running script {name}");

            foreach (var s in shouldContainComplete)
            {
                Assert.IsTrue(stringWriter.ToString().Contains(s), $"Result for {name} does not contain: {s}");
            }
        }
    }
}
