using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal interface IDevice
    {
        double CheckTemperature(Regulator r);
        int SendTemperature(); ///Slanje temperature regulatoru
    }
}