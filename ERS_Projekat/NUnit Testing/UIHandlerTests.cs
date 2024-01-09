using NUnit.Framework;
using Moq;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ERS_Projekat.Tests
{
    
    
    
    /* DISCLAIMER!!!
     * 
     * These tests are not yet 'tested' beacuse of an invisible infinite loop or an unintentional process halt
     * Be advised when trying to fix these tests, and good luck.
     * 
    */
    
    [TestFixture]
    public class UIHandlerTests
    {
        private UIHandler uiHandler;
        private Mock<FunctionHandler> functionHandlerMock;

        [SetUp]
        public void Setup()
        {
            functionHandlerMock = new Mock<FunctionHandler>(true);
            uiHandler = new UIHandler();
            uiHandler.functionHandler = functionHandlerMock.Object;
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void GetCommand_InvalidInput_Retries()
        {
            // Arrange
            var mockReader = new Mock<TextReader>();
            var userInput = "invalid\n";
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
        public void TearDown()
        {
            uiHandler = null;
            functionHandlerMock = null;
        }
    }
}
