using FileOperations;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using System.IO;

namespace FunctionApp.Test
{
    public class FileOperationsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReadFileLinesTest()
        {
            //Arrange
            FileOperation _fileOperations = new FileOperation();
            //Act
            var response = _fileOperations.ReadFileLines();
            //Assert
            Assert.IsNotNull(response);
        }
    }
}