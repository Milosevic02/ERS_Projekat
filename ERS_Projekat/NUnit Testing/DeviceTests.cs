using Moq;
using NUnit.Framework;

namespace ERS_Projekat.Tests
{
    [TestFixture]
    public class DeviceTests
    {
        [Test]
        public void Constructor_InitializesDeviceWithValidId()
        {
            // Arrange
            int expectedId = 42;

            // Act
            var device = new Device(expectedId);

            // Assert
            Assert.That(device.Id, Is.EqualTo(expectedId));
        }

        [Test]
        public void Constructor_GeneratesRandomTemperatureInRange()
        {
            // Arrange
            double minTemperature = 15.0;
            double maxTemperature = 19.0;

            // Act
            var device = new Device(1);
            double generatedTemperature = device.Temperature;

            // Assert
            Assert.That(generatedTemperature, Is.GreaterThanOrEqualTo(minTemperature));
            Assert.That(generatedTemperature, Is.LessThanOrEqualTo(maxTemperature));
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
