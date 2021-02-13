using FileOperations;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using System.IO;

namespace FunctionApp.Test
{
    public class EvaluateExpressionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("\\Instructions\\CalculatorTestScenario7.txt", 15)]
        [TestCase("\\Instructions\\CalculatorTestScenario0.txt", 45)]
        [TestCase("\\Instructions\\CalculatorTestScenario2.txt", 15)]
        [TestCase("\\Instructions\\CalculatorTestScenario6.txt", 349)]
        public void EvaluateTest(string fileName, long expectedResponse)
        {
            //Arrange
            IEvaluateExpression _evaluateExpression = new EvaluateExpression();
            decimal response = 0;
            string[] readFileLinesResponse = File.ReadAllLines(string.Concat(Environment.CurrentDirectory, fileName));

            //Act
            TestDelegate code = (() => _evaluateExpression.Evaluate(ref readFileLinesResponse, ref response));

            //Assert
            Assert.IsNotNull(code);
        }
    }
}