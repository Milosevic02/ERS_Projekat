using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Program
    {
        private static Thread t = new Thread(() => Loop());
        private static UIHandler ui_handler = new UIHandler();

        static void Main()
        {
            t.Start();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Console.WriteLine("Exiting...Please wait a bit for all threads to terminate.");
                t.Abort();
                Environment.Exit(0);
            };

        }

        //Deamon thread for main process, runs until CTRL+C is pressed (SIGINT system interrupt)
        static void Loop()
        {
            while (true)
            {
                ui_handler.Menu();
                ui_handler.GetCommand();
            }
        }
    }
}
