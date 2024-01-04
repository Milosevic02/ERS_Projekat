using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Device : IDevice
    {
        int id;
        double temperature;
        readonly Random r = new Random();

        public Device(int id)
        {
            this.Id = id;
            temperature = r.NextDouble() + r.Next(19,23);
            Thread.Sleep(100); //for synchronizing rng seed 
        }

        public int Id { get => id; set => id = value; }
        public double Temperature { get => temperature; set => temperature = value; }

        public double CheckTemperature()
        {
            return Math.Round(Temperature,3);
        }

    }
}
