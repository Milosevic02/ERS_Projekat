using NUnit.Framework;
using Moq;
using System;
using System.IO;
using System.Reflection;

namespace ERS_Projekat.Tests
{
    [TestFixture]
    public class FunctionHandlerTests
    {
        [Test]
        public void InitializeHeater_HeaterNotNull()
        {
            // Arrange
            var functionHandler = new FunctionHandler();

            // Act
            var heater = functionHandler.Heater;

            // Assert
            Assert.That(heater, Is.Not.Null);
        }

        [Test]
        public void InitializeRegulator_RegulatorNotNull()
        {
            // Arrange
            FunctionHandler functionHandler = new FunctionHandler();

            // Act
            Regulator regulator = functionHandler.Regulator;

            // Assert
            Assert.That(regulator, Is.Not.Null);
        }

        [Test]
        public void ChangeFuel_ValidInput_ReturnsTrue()
        {
            // Arrange
            var functionHandler = new FunctionHandler();
            var heaterMock = new Mock<Heater>();
            var newConstant = 2.0;

            // Act
            var result = functionHandler.ChangeFuel(heaterMock.Object, newConstant);

            // Assert
            Assert.That(result, Is.True);
            heaterMock.VerifySet(h => h.FuelConstant = newConstant, Times.Once);
        }

        [Test]
        public void ClearLogFile_ValidInput_ReturnsTrue()
        {
            // Arrange
            var functionHandler = new FunctionHandler();
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
            var functionHandler = new FunctionHandler();

            // Act
            var result = functionHandler.InitializeDevices();

            // Assert
            Assert.That(result, Is.True);
            Assert.That(functionHandler.Regulator.Devices.Count, Is.EqualTo(4));
        }

        [Test]
        public void InitializeDevice_DeviceInitialized()
        {
            // Arrange
            Console.WriteLine("Starting test");
            var functionHandler = new FunctionHandler();

            // Act
            Console.WriteLine("Before InitializeDevice");
            var device = functionHandler.InitializeDevice();
            Console.WriteLine("After InitializeDevice");

            // Assert
            Assert.That(device, Is.Not.Null);
            Assert.That(device.Id, Is.EqualTo(1));

            // Reset devId for the next test
            typeof(FunctionHandler).GetField("devId", BindingFlags.Static | BindingFlags.NonPublic)?.SetValue(null, 0);

            Console.WriteLine("Test completed");
        }


        // Add more tests for OpenLog, SelectTime, Regulate, StopRegulation as needed
    }
}
