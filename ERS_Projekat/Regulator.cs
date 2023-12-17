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
        List<Device> devices;

        public bool Settings()
        {
            Console.WriteLine("Izaberi trenutan rezim rada (0 - dnevni rezim 1 - nocni rezim)");
            int pom = int.Parse(Console.ReadLine());
            if (pom == 1)
            {
                mode = true;
            }
            else if (pom == 0)
            {
                mode = false;
            }
            else
            {
                return false;
            }
            Console.WriteLine("Unesi od kada ti zapocinje dan:");
            dayStart = Double.Parse(Console.ReadLine());
            Console.WriteLine("Unesi do kada ti traje dan:");
            dayEnd = Double.Parse(Console.ReadLine());
            Console.WriteLine("Unesi koju temperaturu zelis preko dana:");
            dayTemperature = Double.Parse(Console.ReadLine());
            Console.WriteLine("Unesi koju temperaturu zelis preko noci:");
            nightTemperature = Double.Parse(Console.ReadLine());
            return true;
        }



        public bool TemperatureControl()
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
