using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ERS_Projekat
{
    internal class Program
    {
        static void Main()
        {
            UIHandler handler = new UIHandler();
            while(true)
            {
                handler.Menu();
                handler.GetCommand();
            }

        }
    }
}
