using FunctionApp;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace FunctionApp.Test
{
    public class FunctionsTest
    {
        private ILogger logger;
        [SetUp]
        public void Setup()
        {
            logger = TestFactory.CreateLogger();
        }

        [TestCase("", 15)]
        [TestCase("CalculatorTestScenario0", 45)]       
        [TestCase("CalculatorTestScenario2", 15)]
        [TestCase("CalculatorTestScenario6", 349)]
        public void CalculatorTest(string body, long expectedResponse)
        {
            var headers = new Dictionary<string, string>();
            var request = TestFactory.CreateHttpRequest(body, headers);
            var response = FunctionApp.Calculator.Run(request, logger);
            Assert.AreEqual(response, expectedResponse);
        }

        [TestCase("CalculatorTestScenario1", "Invalid File Format")]
        [TestCase("CalculatorTestScenario3", "Invalid File Format")]
        [TestCase("CalculatorTestScenario4", "Invalid File Format")]
        [TestCase("CalculatorTestScenario5", "Invalid File Format")]
        public void CalculatorExceptionTest(string body, string expectedResponse)
        {
            var headers = new Dictionary<string, string>();
            var request = TestFactory.CreateHttpRequest(body, headers);
            //act
            var ex = Assert.Throws<Exception>(() => FunctionApp.Calculator.Run(request, logger));

            //assert
            Assert.AreEqual(ex.Message, expectedResponse);
        }
    }
}