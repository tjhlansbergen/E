using System;

namespace EInterpreter
{
    public static class ExtensionMethods
    {
        public static string[] SplitClean(this string str, char ch)
        {
            return str.Split(new[] {ch}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void WriteColoredLine(string line, ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ForegroundColor = currentColor;
        }
    }
}
