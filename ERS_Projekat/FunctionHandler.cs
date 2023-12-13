using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class FunctionHandler : IFunctionHandler
    {
        static int devId = 0;

        Heater heater = null;

        public FunctionHandler()
        {
            InitializeHeater();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
