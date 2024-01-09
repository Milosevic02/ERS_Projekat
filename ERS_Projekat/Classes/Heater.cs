using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    public class Heater : IHeater
    {

        bool flag;
        DateTime startTime;
        TimeSpan elapsedTime;
        double fuelUsed;
        double fuelConstant;
        readonly string logFilePath = "log.txt";


        public Heater(double fuelConstant)
        {
            this.fuelConstant = fuelConstant;
            flag = false;
            startTime = DateTime.Now;
            elapsedTime = DateTime.Now-startTime;
        }

        public double FuelConstant { get => fuelConstant; set => fuelConstant = value; }
        public bool Flag { get => flag; set => flag = value; }
        public double FuelUsed { get; internal set; }
        public TimeSpan ElapsedTime { get; internal set; }

        public string GetHeaterDetails()
        {
            return "Start time: [" + startTime.ToString() + "]; On-time: [" + elapsedTime + "]; Fuel used: "+ fuelUsed.ToString();
        }

        public bool TurnOff()
        {
            elapsedTime = DateTime.Now - startTime;
            if(Flag)
                fuelUsed = fuelConstant*elapsedTime.TotalSeconds;
            else
                fuelUsed = 0;

            WriteToFile(logFilePath);

            startTime = DateTime.Now;
            elapsedTime = TimeSpan.Zero;
            Flag = false;
            return true;
        }

        public bool TurnOn()
        {
            startTime = DateTime.Now;
            fuelUsed = 0;
            Flag = true;
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

        public int CheckTemperature()
        {
            throw new NotImplementedException();
        }
    }
}
