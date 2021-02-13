using FileOperations;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using System.IO;

namespace FunctionApp.Test
{
    public class FunctionsTest
    {
        private ILogger logger;
        private Mock<IFileOperation> _fileOperation;
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _fileOperation = new Mock<IFileOperation>();
            _calculator = new Calculator(_fileOperation.Object);
            logger = TestFactory.CreateLogger();
        }

        [TestCase("\\Instructions\\CalculatorTestScenario7.txt", 15)]
        [TestCase("\\Instructions\\CalculatorTestScenario0.txt", 45)]       
        [TestCase("\\Instructions\\CalculatorTestScenario2.txt", 15)]
        [TestCase("\\Instructions\\CalculatorTestScenario6.txt", 349)]
        public void CalculatorTest(string fileName, long expectedResponse)
        {
            //Arrange
            string[] readFileLinesResponse = File.ReadAllLines(string.Concat(Environment.CurrentDirectory, fileName));
            var headers = new Dictionary<string, string>();
            var request = TestFactory.CreateHttpRequest(string.Empty, headers);

            _fileOperation.Setup(a => a.ReadFileLines()).Returns(readFileLinesResponse);

            //Act
            var response = _calculator.Run(request, logger);
            //Assert
            Assert.AreEqual(response, expectedResponse);
        }

        [TestCase("\\Instructions\\CalculatorTestScenario1.txt", "Invalid File Format")]
        [TestCase("\\Instructions\\CalculatorTestScenario3.txt", "Invalid File Format")]
        [TestCase("\\Instructions\\CalculatorTestScenario4.txt", "Invalid File Format")]
        [TestCase("\\Instructions\\CalculatorTestScenario5.txt", "Invalid File Format")]
        public void CalculatorExceptionTest(string fileName, string expectedResponse)
        {
            //Arrange
            string[] readFileLinesResponse = File.ReadAllLines(string.Concat(Environment.CurrentDirectory, fileName));
            var headers = new Dictionary<string, string>();
            var request = TestFactory.CreateHttpRequest(string.Empty, headers);

            _fileOperation.Setup(a => a.ReadFileLines()).Returns(readFileLinesResponse);

            //Act
            var ex = Assert.Throws<Exception>(() => _calculator.Run(request, logger));
            //Assert
            Assert.AreEqual(ex.Message, expectedResponse);
        }
    }
}