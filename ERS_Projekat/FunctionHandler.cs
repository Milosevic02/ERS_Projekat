using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class FunctionHandler : IFunctionHandler
    {
        static int devId = 0;
        static int devNum = 4;

        static int checkTime = 3 * 60;
        static int tempChangeTime = 2 * 60;

        Heater heater = null;
        Regulator regulator = null;

        public FunctionHandler()
        {
            InitializeHeater();
            InitializeRegulator();
            InitializeDevices();
        }

        readonly string logFilePath = "log.txt";

        internal Heater Heater { get => heater; set => heater = value; }

        public bool ChangeFuel(Heater h, double newConstant)
        {
            h.FuelConstant = newConstant; 
            return true;
        }

        public bool ClearLogFile()
        {
            try
            {
                File.WriteAllText(logFilePath, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing log file: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool InitializeDevices()
        {
            for(int i = 0; i < devNum; i++)
            {
                Device d = InitializeDevice();
                regulator.AddDevice(d);
            }
            return true;
        }

        public Device InitializeDevice()
        {
            Device d = new Device(++devId);
            return d;
        }

        public bool InitializeHeater()
        {
            Heater = new Heater(1);
            return true;
        }

        public bool InitializeRegulator()
        {
            regulator = new Regulator(false,0,0,0,0);
            regulator.Settings();
            return true;
        }

        public bool OpenLog()
        {
            try
            {
                Process.Start("notepad.exe", logFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening file: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool SelectTime()
        {
            return regulator.Settings();
        }

        public void Regulate()
        {
            int i = 0;
            while (true)
            {
                
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                    if ((key.Modifiers & ConsoleModifiers.Control) != 0 && key.Key == ConsoleKey.C)
                    {
                        Console.WriteLine("Interrupted!");
                        break;
                    }
                }
                
                Thread.Sleep(1000);
                i++;
                if (checkTime <= i)
                {
                    regulator.TemperatureControl(heater);
                }
                if(tempChangeTime <= i)
                {
                    regulator.SendHeaterIsOn();
                }
                if (checkTime > tempChangeTime && i >= checkTime)
                    i = 0;
                else if (checkTime < tempChangeTime && i >= checkTime)
                    i = 0;
            }
            
        }
    }
}
