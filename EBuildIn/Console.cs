using System.Collections.Generic;

namespace EBuildIn
{
    public static class Console
    {
        public static List<string> WriteLineParameters => new List<string> { "text" };

        public static void WriteLine(string line)
        {
            System.Console.WriteLine(line);
        }
    }
}
