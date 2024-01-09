using NUnit.Framework;
using Moq;
using System;
using System.IO;

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
        

        [Test]
        public void GetCommand_InvalidInput_Retries()
        {
            //// Arrange

            //Console.WriteLine("Creating ui handler");
            //UIHandler uiHandler = new UIHandler(true);
            //Console.WriteLine("Created ui handler");

            //var mockReader = new Mock<TextReader>();
            //var userInput = "invalid\n";
            //mockReader.SetupSequence(r => r.ReadLine()).Returns(userInput);
            //Console.SetIn(mockReader.Object);

            //// Act
            //uiHandler.GetCommand();

            //// Assert
            //Assert.That(uiHandler.CommandEnd,Is.False);

            throw new NotImplementedException();
            

        }

        [Test]
        public void GetCommand_SelectTime_CallsSelectTime()
        {
            //// Arrange

            //Console.WriteLine("Creating ui handler");
            //UIHandler uiHandler = new UIHandler(true);
            //Console.WriteLine("Created ui handler");

            //var mockReader = new Mock<TextReader>();
            //var userInput = "1\n";
            //mockReader.SetupSequence(r => r.ReadLine()).Returns(userInput);
            //Console.SetIn(mockReader.Object);

            //// Act
            //uiHandler.GetCommand();

            //// Assert
            //Assert.That(uiHandler.CommandEnd, Is.True);

            throw new NotImplementedException();
        }

    }
}
