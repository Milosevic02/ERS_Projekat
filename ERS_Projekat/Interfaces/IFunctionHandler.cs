using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal interface IFunctionHandler
    {
        bool InitializeRegulator(bool testing);
        Device InitializeDevice();
        bool InitializeHeater();

        bool SelectTime();
        bool OpenLog();
        bool ClearLogFile();
        bool ChangeFuel(Heater h, double newConst);

        bool ChangeIntervals(int t1,int t2);
        void Regulate();

        void StopRegulation();
    }
}
