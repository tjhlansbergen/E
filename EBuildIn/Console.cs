using System;
using System.Collections.Generic;

namespace EBuildIn
{
    public static class Console
    {
        public static List<string> WriteLineParameters => new List<string> { Types.Text.ToString() };

        public static Variable WriteLine(Variable line)
        {
            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine("| " + line.Value);
            System.Console.ForegroundColor = currentColor;

            

            return new Variable(Types.Boolean.ToString(), true);
        }
    }
}
