using System.Collections.Generic;

namespace EBuildIn
{
    public static class Console
    {
        public static List<string> WriteLineParameters => new List<string> { Types.Text.ToString() };

        public static Variable WriteLine(Variable line)
        {
            System.Console.WriteLine(line.Value);

            return new Variable(Types.Boolean.ToString(), true);
        }
    }
}
