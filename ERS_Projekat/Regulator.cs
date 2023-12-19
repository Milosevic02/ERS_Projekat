using System;
using System.Collections.Generic;
using System.IO;
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

        public bool AddDevice(Device d)
        {
            foreach(Device device in devices)
            {
                if(device.Id == d.Id)
                {
                    return false;
                }
            }
            devices.Add(d);
            return true;
        }


        public bool RemoveDevice(Device d)
        {
            foreach (Device device in devices)
            {
                if (device.Id == d.Id)
                {
                    devices.Remove(d);
                    return true;
                }
            }
            return false;
        }


        public bool TemperatureControl(Heater h)
        {
            double avgtemp = 0;
            int i = 0;
            foreach (Device d in devices)
            {
                avgtemp += d.CheckTemperature();
                i++;
            };
            avgtemp = avgtemp / i;
            if (!mode)
            {
                if(avgtemp > dayTemperature){
                    if (h.Flag)
                    {
                        h.TurnOff();
                    }
                    SaveEvent(avgtemp, false);
                    return true;
                    
                }else if(avgtemp < dayTemperature)
                {
                    if (!h.Flag)
                    {
                        h.TurnOn();
                       

                    }
                    SaveEvent(avgtemp, true);
                    return true;
                }
                
            }
            else
            {
                if (avgtemp > nightTemperature)
                {
                    h.TurnOff() ;
                    SaveEvent(avgtemp, false);
                    return true;

                }
                else if (avgtemp < nightTemperature)
                {
                    h.TurnOn() ;
                    SaveEvent(avgtemp, true);
                    return true;
                }
            }
            return true;
        }

        public void SendHeaterIsOn(Heater h)
        {
           
                foreach (Device d in devices)
                {
                    d.Temperature = d.Temperature + 0.01;
                };
            
        }
        public bool SaveEvent(double avgTemp,bool on)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("RegulatorDetail.txt", true))
                {
                    writer.WriteLine("**********Devices send information**********\n");

                    foreach (Device d in devices)
                    {
                        writer.WriteLine("Device with id " + d.Id + " have temperature: " + d.CheckTemperature());
                    }
                    writer.WriteLine("Day temp = " + dayTemperature);
                    writer.WriteLine("Avg Temperature is " + avgTemp);
                    if (on)
                    {
                        writer.WriteLine("Heater Turn On");
                    }
                    else { writer.WriteLine("Heater Turn Off"); }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending to the file: {ex.Message}");
                return false;
            }
            return true;
        }



    }
}
