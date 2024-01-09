using NUnit.Framework;
using Moq;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace ERS_Projekat.Tests
{
    [TestFixture]
    public class FunctionHandlerTests
    {
        Thread t;

        [Test]
        public void InitializeHeater_HeaterNotNull()
        {
            // Arrange
            FunctionHandler functionHandler = new FunctionHandler(true);

            // Act
            Heater heater = functionHandler.Heater;

            // Assert
            Assert.That(heater, Is.Not.Null);
        }

        [Test]
        public void InitializeRegulator_RegulatorNotNull()
        {
            // Arrange
            FunctionHandler functionHandler = new FunctionHandler(true);

            // Act
            Regulator regulator = functionHandler.Regulator;

            // Assert
            Assert.That(regulator, Is.Not.Null);
        }

        [Test]
        public void ChangeFuel_ValidInput_ReturnsTrue()
        {
            // Arrange
            FunctionHandler functionHandler = new FunctionHandler(true);
            Heater heater = new Heater(1);
            var newConstant = 2.0;

            // Act
            var result = functionHandler.ChangeFuel(heater, newConstant);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ChangeFuel_InvalidInput_ReturnsFalse1()
        {
            // Arrange
            FunctionHandler functionHandler = new FunctionHandler(true);
            Heater heater = new Heater(1);
            var newConstant = -1212123;

            // Act
            var result = functionHandler.ChangeFuel(heater, newConstant);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ChangeFuel_InvalidInput_ReturnsFalse2()
        {
            // Arrange
            FunctionHandler functionHandler = new FunctionHandler(true);
            Heater heater = new Heater(1);
            var newConstant = 999999;

            // Act
            var result = functionHandler.ChangeFuel(heater, newConstant);

            // Assert
            Assert.That(result, Is.False);
        }


        [Test]
        public void ClearLogFile_ValidInput_ReturnsTrue()
        {
            // Arrange
            var functionHandler = new FunctionHandler(true);
            File.WriteAllText("log.txt", "Test log content");
            File.WriteAllText("RegulatorDetail.txt", "Test device log content");

            // Act
            var result = functionHandler.ClearLogFile();

            // Assert
            Assert.That(result, Is.True);
            Assert.That(File.ReadAllText("log.txt"), Is.Empty);
            Assert.That(File.ReadAllText("RegulatorDetail.txt"), Is.Empty);
        }

        [Test]
        public void InitializeDevices_DevicesInitialized()
        {
            // Arrange
            FunctionHandler functionHandler = new FunctionHandler(true);

            // Act
            List<Device> devs = functionHandler.Regulator.Devices;

            // Assert
            Assert.That(devs, Is.Not.Null);
        }

        //[Test]
        public void OpenLog_Successful()
        {
            // Arrange
            Mock<FunctionHandler> functionHandlerMock = new Mock<FunctionHandler>(true);

            // Act
            bool result = functionHandlerMock.Object.OpenLog();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Regulate_Start_Stop()
        {
            // Arrange
            Mock<FunctionHandler> functionHandlerMock = new Mock<FunctionHandler>(true);
            t = new Thread(() => functionHandlerMock.Object.Regulate());

            // Act & Assert
            t.Start();
            Console.WriteLine("Started thread...");
            Thread.Sleep(1500); //allow thread to start working
            Assert.That(functionHandlerMock.Object.stopRequested, Is.False);

            functionHandlerMock.Object.StopRegulation();
            Assert.That(functionHandlerMock.Object.stopRequested, Is.True);
            t.Join();
            Console.WriteLine("Stopped thread...");
        }

        [Test]
        public void ChangeIntervals_ValidInput()
        {
            // Arrange
            Mock<FunctionHandler> functionHandlerMock = new Mock<FunctionHandler>(true);

            // Act
            bool result = functionHandlerMock.Object.ChangeIntervals(100,200);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ChangeIntervals_InvalidInput()
        {
            // Arrange
            Mock<FunctionHandler> functionHandlerMock = new Mock<FunctionHandler>(true);

            // Act
            bool result = functionHandlerMock.Object.ChangeIntervals(-232, 0);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
