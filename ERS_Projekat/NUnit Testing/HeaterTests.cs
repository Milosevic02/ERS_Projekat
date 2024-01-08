using NUnit.Framework;
using System;
using System.IO;
using System.Threading;

namespace ERS_Projekat.Tests
{
    [TestFixture]
    public class HeaterTests
    {
        [Test]
        public void Constructor_InitializesHeaterWithValidFuelConstant()
        {
            // Arrange
            double expectedFuelConstant = 0.5;

            // Act
            var heater = new Heater(expectedFuelConstant);

            // Assert
            Assert.That(heater.FuelConstant, Is.EqualTo(expectedFuelConstant));
        }

        [Test]
        public void TurnOn_SetsFlagToTrue()
        {
            // Arrange
            var heater = new Heater(0.5);

            // Act
            heater.TurnOn();

            // Assert
            Assert.That(heater.Flag, Is.True);
        }

        [Test]
        public void TurnOff_SetsFlagToFalse()
        {
            // Arrange
            var heater = new Heater(0.5);

            // Act
            heater.TurnOn(); // Turn it on first
            heater.TurnOff();

            // Assert
            Assert.That(heater.Flag, Is.False);
        }

        [Test]
        public void TurnOff_CalculatesElapsedTimeAndResetsFuelUsed()
        {
            // Arrange
            var heater = new Heater(0.5);
            heater.TurnOn(); // Turn it on first

            // Act
            heater.TurnOff();

            // Assert
            Assert.That(heater.FuelUsed, Is.EqualTo(0)); //reset fuel consumption after writing to file
            Assert.That(heater.ElapsedTime, Is.EqualTo(TimeSpan.Zero)); //because it should be 0 after running for such a short time
        }

        [Test]
        public void WriteToFile_AppendsHeaterDetailsToLogFile()
        {
            // Arrange
            var heater = new Heater(0.5);

            // Act
            heater.TurnOn();
            heater.TurnOff();

            // Assert
            Assert.That(File.Exists("log.txt"), Is.True);
        }
    }
}
