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

        static int checkTime = 5 /** 60*/;
        static int tempChangeTime = 2 /** 60*/;

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
            int checkCounter = 0;
            int onCounter = 0; // counter starts when heater turns on

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                    if ((key.Modifiers & ConsoleModifiers.Control) != 0 && key.Key == ConsoleKey.C)
                    {
                        Console.WriteLine("Prekod rada regulatora!");
                        break;
                    }
                }

                checkCounter++;

                if (checkTime == checkCounter)
                {
                    regulator.TemperatureControl(heater);
                    checkCounter = 0;
                }

                if (heater.Flag)
                {
                    onCounter++;
                    if (tempChangeTime == onCounter)
                    {
                        regulator.SendHeaterIsOn(heater);
                        onCounter = 0; // Reset the onCounter only after sending the signal
                    }
                }
                else
                {
                    onCounter = 0; // Reset the onCounter if the heater is off
                }

                Thread.Sleep(10);
            }

        }
    }
}
