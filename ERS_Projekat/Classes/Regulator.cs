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

        double dayTemperature;
        double nightTemperature;
        DateTime dayStart;
        DateTime dayEnd;
        List<Device> devices = new List<Device>();

        public Regulator(double dayTemperature, double nightTemperature, DateTime dayStart, DateTime dayEnd)
        {
            this.dayTemperature = dayTemperature;
            this.nightTemperature = nightTemperature;
            this.DayStart = dayStart;
            this.DayEnd = dayEnd;
        }

        public double DayTemperature { get => dayTemperature; set => dayTemperature = value; }
        public double NightTemperature { get => nightTemperature; set => nightTemperature = value; }
        public DateTime DayEnd { get => dayEnd; set => dayEnd = value; }
        public DateTime DayStart { get => dayStart; set => dayStart = value; }
        internal List<Device> Devices { get => devices; set => devices = value; }

        public bool Settings()
        {
            bool good = false;
            do
            {

                try
                {
                    Console.WriteLine("Unesi od kada ti zapocinje dan (format - hh:mm):");
                    DayStart = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Unesi do kada ti traje dan (format - hh:mm):");
                    DayEnd = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Unesi koju temperaturu zelis preko dana:");
                    dayTemperature = Double.Parse(Console.ReadLine());
                    Console.WriteLine("Unesi koju temperaturu zelis preko noci:");
                    nightTemperature = Double.Parse(Console.ReadLine());
                    good = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Greska prilikom unosa, pokusajte opet");
                    //Console.WriteLine("Parsing error: {0}",e.Message);
                }
            } while (!good);
            
            return true;
        }

        public bool AddDevice(Device d)
        {
            foreach (Device device in Devices)
            {
                if (device.Id == d.Id)
                {
                    return false;
                }
            }
            Devices.Add(d);
            return true;
        }


        public bool RemoveDevice(Device d)
        {
            foreach (Device device in Devices)
            {
                if (device.Id == d.Id)
                {
                    Devices.Remove(d);
                    return true;
                }
            }
            return false;
        }


        public bool TemperatureControl(Heater h)
        {
            double avgtemp = 0;
            int i = 0;

            foreach (Device d in Devices)
            {
                avgtemp += d.CheckTemperature();
                i++;
            }

            avgtemp = avgtemp / i;

            DateTime currentTime = DateTime.Now;

            if (currentTime >= dayStart && currentTime <= dayEnd)
            {
                // Day mode
                if (avgtemp > dayTemperature)
                {
                    if (h.Flag)
                    {
                        h.TurnOff();
                    }
                    SaveEvent(avgtemp, false);
                    return true;
                }
                else if (avgtemp < dayTemperature)
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
                // Night mode
                if (avgtemp > nightTemperature)
                {
                    h.TurnOff();
                    SaveEvent(avgtemp, false);
                    return true;
                }
                else if (avgtemp < nightTemperature)
                {
                    h.TurnOn();
                    SaveEvent(avgtemp, true);
                    return true;
                }
            }

            return true;
        }



        public bool SaveEvent(double avgTemp, bool on)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("RegulatorDetail.txt", true))
                {
                    writer.WriteLine("**********Devices send information**********\n");

                    foreach (Device d in Devices)
                    {
                        writer.WriteLine("Device with id " + d.Id + " has temperature: " + d.CheckTemperature());
                    }
                    DateTime currentTime = DateTime.Now;
                    if (currentTime >= dayStart && currentTime <= dayEnd)
                        writer.WriteLine("Day temp = " + dayTemperature);
                    else
                        writer.WriteLine("Night temp = " + nightTemperature);
                    writer.WriteLine("Avg Temperature is " + Math.Round(avgTemp, 3));
                    if (on)
                    {
                        writer.WriteLine("Heater is turned On");
                    }
                    else { writer.WriteLine("Heater is turned Off"); }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending to the file: {ex.Message}");
                return false;
            }
            return true;
        }


        public void SendHeaterIsOn(Heater h)
        {

            foreach (Device d in Devices)
            {
                d.Temperature = d.Temperature + 0.01;
            }

        }

        public void SendHeaterIsOff(Heater h)
        {
            foreach (Device d in Devices)
            {
                d.Temperature = d.Temperature - 0.01;
            }
        }
    }
}
