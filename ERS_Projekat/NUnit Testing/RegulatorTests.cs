using NUnit.Framework;
using Moq;
using System;

namespace ERS_Projekat.Tests
{
    [TestFixture]
    public class RegulatorTests
    {
       
        [Test]
        public void TemperatureControl_TemperatureHigh()
        {
            // Arrange
            Heater heater = new Heater(1);
            Regulator regulator = new Regulator(20.0, 20.0, DateTime.Parse("08:00"), DateTime.Parse("17:00"));

            // Use a concrete implementation of Device for testing
            Device concreteDevice = new Device(1);
            concreteDevice.Temperature = 25.0; //above target

            regulator.AddDevice(concreteDevice);

            // Act
            regulator.TemperatureControl(heater);

            // Assert
            Assert.That(heater.Flag, Is.False);
        }
        [Test]
        public void TemperatureControl_TemperatureLow()
        {
            // Arrange
            Heater heater = new Heater(1);
            Regulator regulator = new Regulator(20.0, 20.0, DateTime.Parse("08:00"), DateTime.Parse("17:00"));

            // Use a concrete implementation of Device for testing
            Device concreteDevice = new Device(1);
            concreteDevice.Temperature = 15.0; //below target

            regulator.AddDevice(concreteDevice);

            // Act
            regulator.TemperatureControl(heater);

            // Assert
            Assert.That(heater.Flag, Is.True);
        }

        [Test]
        public void Add_Then_Remove_Devices()
        {
            //Arrange
            Regulator regulator = new Regulator(20.0, 20.0, DateTime.Parse("08:00"), DateTime.Parse("17:00"));
            Device concreteDevice = new Device(1);

            //Act & Assert
            regulator.AddDevice(concreteDevice);
            Assert.That(regulator.Devices,Is.Not.Empty);
            regulator.RemoveDevice(concreteDevice);
            Assert.That(regulator.Devices, Is.Empty);

        }

        [Test]
        public void SaveEvent_WhenCalled_ReturnsTrue()
        {
            // Arrange
            var regulator = new Regulator(25.0, 20.0, DateTime.Parse("06:00"), DateTime.Parse("18:00"));

            // Act
            bool result = regulator.SaveEvent(22.0, true);

            // Assert
            Assert.That(result, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            Mock<FunctionHandler> functionHandlerMock = new Mock<FunctionHandler>(true);

            functionHandlerMock.Object.ClearLogFile();

            functionHandlerMock = null;

        }
    }
}
