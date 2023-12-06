﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal interface IRegulator
    {
        bool Settings(); ///UI za podesavanje
        bool ChangeTemperature(); ///Promena temperature u centralnoj peci

        bool SendHeaterIsOn(); ///Informacija da je pec pocela sa radom

        bool SaveEvent(); // Cuvanje svih dogadjaja u txt fajl




    }
}
