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
            var functionHandler = new FunctionHandler(true);

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
            var functionHandler = new FunctionHandler(true);

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
