using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Device : IDevice
    {
        int id;
        double temperature;

        public Device(int id)
        {
            this.Id = id;
        }

        public int Id { get => id; set => id = value; }
        public double Temperature { get => temperature; set => temperature = value; }

        public double CheckTemperature()
        {
            return Temperature;
        }

    }
}
