using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Heater : IHeater
    {

        int temperature;
        DateTime startTime;
        TimeSpan elapsedTime;
        double fuelUsed;
        
        public int CheckTemperature()
        {
            throw new NotImplementedException();
        }

        public string GetHeaterDetails()
        {
            return "Start time: [" + startTime.ToString() + "]; On-time: [" + elapsedTime + "]; Fuel used: "+ fuelUsed.ToString();
        }

        public bool TurnOff()
        {
            throw new NotImplementedException();
        }

        public bool TurnOn()
        {
            throw new NotImplementedException();
        }


    }
}
