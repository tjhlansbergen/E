using System;

namespace EInterpreter
{
    public static class ExtensionMethods
    {
        public static string[] SplitClean(this string str, char ch)
        {
            return str.Split(new[] {ch}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void WriteColoredLine(string line, bool ok)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(line);
            Console.ForegroundColor = currentColor;
        }
    }
}
