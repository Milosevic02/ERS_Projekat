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
            double minTemperature = 19.0;
            double maxTemperature = 23.0;

            // Act
            var device = new Device(1);
            double generatedTemperature = device.Temperature;

            // Assert
            Assert.That(generatedTemperature, Is.GreaterThanOrEqualTo(minTemperature));
            Assert.That(generatedTemperature, Is.LessThanOrEqualTo(maxTemperature));
        }

        [Test]
        public void CheckTemperature_ReturnsRoundedTemperature()
        {
            // Arrange
            var device = new Device(1);
            double originalTemperature = device.Temperature;

            // Act
            double checkedTemperature = device.CheckTemperature();

            // Assert
            Assert.That(GetDecimalPlaces(checkedTemperature), Is.EqualTo(3));
        }

        private int GetDecimalPlaces(double number)
        {
            // Helper method to get the number of decimal places in a double
            string str = number.ToString(System.Globalization.CultureInfo.InvariantCulture);
            int decimalPlaces = str.Length - str.LastIndexOf('.') - 1;
            return decimalPlaces;
        }
    }
}
