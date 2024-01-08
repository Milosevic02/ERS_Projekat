using NUnit.Framework;
using Moq;
using System;

namespace ERS_Projekat.Tests
{
    [TestFixture]
    public class RegulatorTests
    {
        [Test]
        public void TemperatureControl_DayMode_TurnsOnHeater()
        {
            // Arrange
            var heaterMock = new Mock<IHeater>();
            var regulator = new Regulator(25.0, 20.0, DateTime.Parse("06:00"), DateTime.Parse("18:00"));

            // Use a concrete implementation of Device for testing
            var concreteDevice = new Device(1);
            concreteDevice.Temperature = 22.0;

            regulator.AddDevice(concreteDevice);

            // Act
            regulator.TemperatureControl((Heater)heaterMock.Object);

            // Assert
            Assert.That(() => heaterMock.Verify(h => h.TurnOn(), Times.Once), Throws.Nothing);
        }

        [Test]
        public void TemperatureControl_NightMode_TurnsOffHeater()
        {
            // Arrange
            var heaterMock = new Mock<Heater>(1);
            var regulator = new Regulator(25.0, 20.0, DateTime.Parse("00:00"), DateTime.Parse("00:00"));//for night mode

            // Use a concrete implementation of Device for testing
            var concreteDevice = new Device(1);
            concreteDevice.Temperature = 18.0;

            regulator.AddDevice(concreteDevice);

            // Act
            regulator.TemperatureControl(heaterMock.Object);

            // Assert
            Assert.That(() => heaterMock.Verify(h => h.TurnOff(), Times.Once), Throws.Nothing);
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
    }
}
