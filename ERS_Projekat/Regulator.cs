using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Regulator : IRegulator
    {
        bool mode;
        double dayTemperature;
        double nightTemperature;
        double dayStart;
        double dayEnd;

        public bool Settings()
        {
            throw new NotImplementedException();
        }

        public bool ChangeTemperature()
        {
            throw new NotImplementedException();
        }

        public bool SendHeaterIsOn()
        {
            throw new NotImplementedException();
        }
        public bool SaveEvent()
        {
            throw new NotImplementedException();
        }




    }
}
