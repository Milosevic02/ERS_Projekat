using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal interface IHeater
    {
        bool TurnOn();
        bool TurnOff();
        string GetHeaterDetails();
        int CheckTemperature();

        bool WriteToFile(string filePath);
    }
}
