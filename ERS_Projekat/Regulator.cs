﻿using System;
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
        List<Device> devices = new List<Device>();



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

        public void AddDevice(Device d)
        {
            devices.Add(d);
        }

        public void RemoveDevice(Device d)
        {
            devices.Remove(d);
        }


        public bool TemperatureControl()
        {
            double avgtemp = 0;
            int i = 0;
            foreach (Device d in devices)
            {
                avgtemp = d.CheckTemperature();
                i++;
            };
            avgtemp = avgtemp / i;
            if (mode)
            {
                if(avgtemp > dayTemperature){
                    //smanjuj temperaturu
                    return true;
                    
                }else if(avgtemp < dayTemperature)
                {
                    //povecaj temperaturu
                    return true;
                }
            }
            else
            {
                if (avgtemp > nightTemperature)
                {
                    //smanjuj temperaturu
                    return true;

                }
                else if (avgtemp < nightTemperature)
                {
                    //povecaj temperaturu
                    return true;
                }
            }
            return true;
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
