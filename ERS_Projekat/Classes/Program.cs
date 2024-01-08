using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Program
    {
        
        
        static void Main(string[] args)
        {
            UIHandler ui_handler = new UIHandler();

            while (true)
            {
                ui_handler.Menu();
                ui_handler.GetCommand();
            }
            

        }
    }
}
