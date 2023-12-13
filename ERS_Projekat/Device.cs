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

        public int CheckTemperature()
        {
            throw new NotImplementedException();
        }
    }
}
