using System;
using System.Collections.Generic;
using System.Linq;

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

        public static bool ListHasDistinctItemsOnly<T>(IEnumerable<T> list)
        {
            var diffChecker = new HashSet<T>();
            return list.All(diffChecker.Add);
        }
    }
}
