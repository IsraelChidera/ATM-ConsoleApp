using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ATM.BLL
{    
    public static class Utility
    {
        private static string? _amount;

        public static string? Amount
        {
            get; set;
        }

        public static void HomeContent()
        {
            Console.WriteLine("============================================================");
            Console.WriteLine("\tAutomated Teller Machine");
            Console.WriteLine("============================================================");

            Console.WriteLine();
            PressEnterToContinue();
        }

        public static void Animation(int timer = 15)
        {
            for (var i = 0; i < timer; i++)
            {                
                Console.Write(".");
                Thread.Sleep(200);
            }
        }

        public static void PressEnterToContinue()
        {
            Console.WriteLine("Press Enter to Continue");

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    PressEnterToContinue();
                }
            }
        }



        public static void SucessfullTransferPrompts(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ErrorPrompts(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}\n");
            Console.ResetColor();
        }

    }
}
