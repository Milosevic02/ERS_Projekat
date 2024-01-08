using NUnit.Framework;
using Moq;
using System;
using System.IO;

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
        public void GetCommand_InvalidInput_Retries()
        {
            // Arrange
            var mockReader = new Mock<TextReader>();
            var userInput = "invalid\n1\n";
            mockReader.SetupSequence(r => r.ReadLine()).Returns(() => userInput).Returns("1");
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

        // Add tests for other commands (2, 3, 4, 5, 6) following similar structures...

        [TearDown]
        public void RestoreConsoleInput()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
    }
}
