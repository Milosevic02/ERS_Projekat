﻿using System;
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

        bool WriteToFile(string filePath);
    }
}
