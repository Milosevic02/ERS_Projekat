using NUnit.Framework;
using Moq;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ERS_Projekat.Tests
{
    [TestFixture]
    public class UIHandlerTests
    {
        private UIHandler uiHandler;
        private Mock<FunctionHandler> functionHandlerMock;

        [SetUp]
        public void Setup()
        {
            functionHandlerMock = new Mock<FunctionHandler>();
            uiHandler = new UIHandler();
            uiHandler.functionHandler = functionHandlerMock.Object;
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void GetCommand_InvalidInput_Retries()
        {
            // Arrange
            var mockReader = new Mock<TextReader>();
            var userInput = "invalid";
            mockReader.SetupSequence(r => r.ReadLine()).Returns(userInput);
            Console.SetIn(mockReader.Object);

            // Act
            uiHandler.GetCommand();

            // Assert
            functionHandlerMock.Verify(fh => fh.SelectTime(), Times.Once);
        }

        [Test]
        public void GetCommand_SelectTime_CallsSelectTime()
        {
            // Arrange
            var mockReader = new Mock<TextReader>();
            var userInput = "1\n";
            mockReader.Setup(r => r.ReadLine()).Returns(() => userInput);
            Console.SetIn(mockReader.Object);

            // Act
            uiHandler.GetCommand();

            // Assert
            functionHandlerMock.Verify(fh => fh.SelectTime(), Times.Once);
        }


        [TearDown]
        public void RestoreConsoleInput()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
    }
}
