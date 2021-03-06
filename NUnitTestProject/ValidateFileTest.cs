using FileOperations;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using System.IO;

namespace FunctionApp.Test
{
    public class ValidateFileTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("\\Instructions\\CalculatorTestScenario7.txt")]
        [TestCase("\\Instructions\\CalculatorTestScenario0.txt")]
        [TestCase("\\Instructions\\CalculatorTestScenario2.txt")]
        [TestCase("\\Instructions\\CalculatorTestScenario6.txt")]
        public void ValidateFileTests(string fileName)
        {
            //Arrange
            string[] readFileLinesResponse = File.ReadAllLines(string.Concat(Environment.CurrentDirectory, fileName));
            IValidateFile _validateFile = new ValidateFile();
            //Act
            TestDelegate code = (()=> _validateFile.ValidateFileContent(ref readFileLinesResponse));
            //Assert
            Assert.DoesNotThrow(code);
        }

        [TestCase("\\Instructions\\CalculatorTestScenario1.txt", "Invalid File Format")]
        [TestCase("\\Instructions\\CalculatorTestScenario3.txt", "Invalid File Format")]
        [TestCase("\\Instructions\\CalculatorTestScenario4.txt", "Invalid File Format")]
        [TestCase("\\Instructions\\CalculatorTestScenario5.txt", "Invalid File Format")]
        public void ValidateFileExceptionTests(string fileName, string expectedResponse)
        {
            //Arrange
            string[] readFileLinesResponse = File.ReadAllLines(string.Concat(Environment.CurrentDirectory, fileName));
            IValidateFile _validateFile = new ValidateFile();
            //Act
            var ex = Assert.Throws<Exception>(() => _validateFile.ValidateFileContent(ref readFileLinesResponse));

            //Assert
            Assert.AreEqual(ex.Message, expectedResponse);
        }
    }
}