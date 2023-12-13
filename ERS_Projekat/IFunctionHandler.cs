using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal interface IFunctionHandler
    {
        bool InitializeRegulator();
        Device InitializeDevice();
        bool InitializeHeater();

        bool SelectTime();
        bool OpenLog();
        bool ClearLogFile();
        bool ChangeFuel(Heater h, double newConst);
    }
}
