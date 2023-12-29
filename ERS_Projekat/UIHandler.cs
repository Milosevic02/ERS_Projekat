﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class UIHandler : IUIHandler
    {
        readonly int commandNum = 5;
        bool commandEnd = false;
        readonly FunctionHandler functionHandler = new FunctionHandler();

        Thread thrRegulator;

        public UIHandler() 
        {
            thrRegulator = new Thread(new ThreadStart(functionHandler.Regulate));
        }

        public void Menu()
        {
            Console.WriteLine("[= = = = SISTEM ZA REGULACIJU TOPLOTE = = = =]");
            Console.WriteLine("Meni:");
            Console.WriteLine("1. Biranje vremena za dnevni/noćni režim rada");
            Console.WriteLine("2. Promena tipa goriva u grejaču");
            Console.WriteLine("3. Pokretanje/gašenje regulatora");
            Console.WriteLine("4. Brisanje log fajlova");
            Console.WriteLine("5. Otvori log fajl");
        }

        public void GetCommand()
        {
            do
            {

                try
                {
                    int x = int.Parse(Console.ReadLine());
                    if (x < 1 || x > commandNum)
                    {
                        Console.WriteLine();
                        throw new Exception();
                    }


                    switch (x)
                    {
                        case 1:
                            functionHandler.SelectTime();
                            commandEnd = true;
                            break;

                        case 2:
                            double c = 0;
                            bool c2Success = false;
                            do
                            {
                                Console.WriteLine("Unesite novu konstantu za tip goriva:");
                                try
                                {
                                    c = Double.Parse(Console.ReadLine());
                                    c2Success = true;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Konstanta nije validna! Pokusajte opet");
                                }

                            }while (c2Success != true);
                            functionHandler.ChangeFuel(functionHandler.Heater,c);
                            commandEnd = true;
                            break;

                        case 3:
                            if (!thrRegulator.IsAlive)
                            {
                                thrRegulator = new Thread(new ThreadStart(functionHandler.Regulate));
                                thrRegulator.Start();
                                Console.WriteLine("\nRegulator je pokrenut...\n");
                            }
                            else
                            {
                                functionHandler.StopRegulation(); // Set the stopRequested flag
                                thrRegulator.Join(); // Wait for the thread to finish
                                Console.WriteLine("\nRegulator je prestao sa radom.\n");
                            }
                            commandEnd = true;
                            break;

                        case 4:
                            functionHandler.ClearLogFile();
                            Console.WriteLine("Cleared log file!");
                            commandEnd = true;
                            break;
                        case 5:
                            functionHandler.OpenLog();
                            commandEnd = true;
                            break;
                        case 6:
                            Console.WriteLine("You selected Case 6");
                            commandEnd = true;
                            break;

                        default:
                            break;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Nepoznata komanda, probajte opet");
                }
            } while (commandEnd != true);
        }
    }
}
