using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Heater : IHeater
    {

        DateTime startTime;
        TimeSpan elapsedTime;
        double fuelUsed;
        double fuelConstant;
        readonly string logFilePath = "log.txt";


        public Heater(double fuelConstant)
        {
            this.fuelConstant = fuelConstant;
        }

        public double FuelConstant { get => fuelConstant; set => fuelConstant = value; }

        public string GetHeaterDetails()
        {
            return "Start time: [" + startTime.ToString() + "]; On-time: [" + elapsedTime + "]; Fuel used: "+ fuelUsed.ToString();
        }

        public bool TurnOff()
        {
            elapsedTime = DateTime.Now - startTime;
            fuelUsed = fuelConstant*elapsedTime.TotalSeconds;
            Console.WriteLine(fuelUsed);
            WriteToFile(logFilePath);
            return true;
        }

        public bool TurnOn()
        {
            startTime = DateTime.Now;
            fuelUsed = 0;
            return true;
        }

        public bool WriteToFile(string logFilePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("log.txt", true))
                {
                    writer.WriteLine(GetHeaterDetails() + "\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending to the file: {ex.Message}");
                return false;
            }
            return true;
        }


    }
}
