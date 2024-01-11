using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal interface IRegulator
    {
        bool Settings(); ///UI za podesavanje
        bool TemperatureControl(Heater h); ///Promena temperature u centralnoj peci

        void SendHeaterIsOn(Heater h); ///Informacija da je pec pocela sa radom

        void SendHeaterIsOff(Heater h); ///Informacija da je pec prestala sa radom

        bool SaveEvent(double avgTemp,bool on); // Cuvanje svih dogadjaja u txt fajl

        bool AddDevice(Device d);

        bool RemoveDevice(Device d);




    }
}
