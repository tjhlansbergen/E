using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using E.Validation;

namespace E
{
    class Program
    {
        static void Main(string[] args)
        {
            //check if there are args
            if(args.Length == 0)
            {
                Console.Write("Please provide the path of the E# file to run: ");
                Array.Resize(ref args, 1);
                args[0] = Console.ReadLine();
            }

            var path = args[0].Replace("\"", "");

            var interpreter = new EInterpreter();

            do
            {
                interpreter.Go(path);
            }while (Continue());
        }

        private static bool Continue()
        {
            ConsoleKeyInfo key;

            do
            {
                Console.WriteLine("Press Enter to run again, or Esc to exit.");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter);

            return key.Key == ConsoleKey.Enter;
        }
    }
}
