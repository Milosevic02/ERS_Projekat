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
    public class FunctionHandler : IFunctionHandler
    {
        static int devId = 0;
        static int devNum = 4;

        static int checkTime = 5 * 60;
        static int tempChangeTime = 2 * 60;

        Heater heater = null;
        Regulator regulator = null;

        public bool testing;


        public FunctionHandler()
        {
            InitializeHeater();
            InitializeRegulator(false);
            InitializeDevices();
        }

        /*for testing purposes, to bypass regulator config*/
        public FunctionHandler(bool testing)
        {
            InitializeHeater();
            InitializeRegulator(testing);
            InitializeDevices();

        }

        readonly string logFilePath = "log.txt";
        readonly string deviceLogFilePath = "RegulatorDetail.txt";

        internal Heater Heater { get => heater; set => heater = value; }
        public bool stopRequested { get; private set; } = false;
        internal Regulator Regulator { get => regulator; set => regulator = value; }
        public static int CheckTime { get => checkTime; set => checkTime = value; }
        public static int TempChangeTime { get => tempChangeTime; set => tempChangeTime = value; }

        public bool ChangeFuel(Heater h, double newConstant)
        {
            if (newConstant <= 0 || newConstant > 100)
                return false;
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

            try
            {
                File.WriteAllText(deviceLogFilePath, string.Empty);
            }
            catch(Exception ex)
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
                Regulator.AddDevice(d);
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

        public bool InitializeRegulator(bool testing)
        {
            Regulator = new Regulator(19.0, 22.0, DateTime.Parse("8:00"), DateTime.Parse("18:00"));
            if (testing) {
                return true;
            }
            
            if (Regulator.Settings())
                return true;

            return false;
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
            return Regulator.Settings();
        }

        public void Regulate()
        {
            int checkCounter = 0;
            int onCounter = 0; // counter starts when heater turns on
            int offCounter = 0;
           
                while (!stopRequested)
                {
                    
                    checkCounter++;

                    if (CheckTime == checkCounter)
                    {
                        Regulator.TemperatureControl(heater);
                        checkCounter = 0;
                    }

                    if (heater.Flag)
                    {
                        offCounter = 0;
                        onCounter++;
                        if (TempChangeTime == onCounter)
                        {
                            Regulator.SendHeaterIsOn(heater);
                            onCounter = 0; // Reset the onCounter only after sending the signal
                        }
                    }
                    else
                    {
                        onCounter = 0; // Reset the onCounter if the heater is off
                        offCounter++;
                        if (TempChangeTime == offCounter)
                        {
                            Regulator.SendHeaterIsOff(heater);
                            offCounter = 0; // Reset the onCounter only after sending the signal
                        }
                    }

                    Thread.Sleep(1000);
                }
            stopRequested = false;
          
                
        }

        public void StopRegulation()
        {
            stopRequested = true;
            heater.TurnOff();
        }

        public bool ChangeIntervals(int ct,int tct)
        {
            if (ct <= 0 || tct <= 0)
                return false;

            CheckTime = ct;
            TempChangeTime = tct;
            return true;
        }
    }
}
