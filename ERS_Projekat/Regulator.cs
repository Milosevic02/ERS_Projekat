using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Regulator : IRegulator
    {
        bool mode; //0 - day ; 1 - night
        double dayTemperature;
        double nightTemperature;
        double dayStart;
        double dayEnd;



        public Regulator(bool mode, double dayTemperature, double nightTemperature, double dayStart, double dayEnd)
        {
            this.mode = mode;
            this.dayTemperature = dayTemperature;
            this.nightTemperature = nightTemperature;
            this.dayStart = dayStart;
            this.dayEnd = dayEnd;
        }

        public bool Mode { get => mode; set => mode = value; }
        public double DayTemperature { get => dayTemperature; set => dayTemperature = value; }
        public double NightTemperature { get => nightTemperature; set => nightTemperature = value; }
        public double DayStart { get => dayStart; set => dayStart = value; }
        public double DayEnd { get => dayEnd; set => dayEnd = value; }

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
