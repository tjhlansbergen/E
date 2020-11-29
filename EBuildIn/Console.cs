using System.Collections.Generic;

namespace EBuildIn
{
    public static class Console
    {
        public static List<string> WriteLineParameters => new List<string> { Types.Text.ToString() };

        public static Variable WriteLine(string line)
        {
            System.Console.WriteLine(line);

            return new Variable(Types.Boolean.ToString(), true);
        }
    }
}
