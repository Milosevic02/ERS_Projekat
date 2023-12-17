using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Device : IDevice
    {
        readonly int id;

        public Device(int id)
        {
            this.id = id;
        }

        public double CheckTemperature(Regulator r)
        {
            if (r.Mode) // Ako je true noc je
            {
                return r.NightTemperature;
            }
            return r.DayTemperature;
        }

        public int SendTemperature()
        {
            throw new NotImplementedException();
        }
    }
}
