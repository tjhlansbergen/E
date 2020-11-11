using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.EObjects;

namespace E.Lexer
{
    public static class Parsers
    {
        public static bool ParseVariable(string namespac, string line, out EVariable result, bool isConstant = false)
        {
            try
            {
                var left = line.SplitClean('=')[0];
                var right = line.SplitClean('=')[1];
                result = EVariable.New(left.SplitClean(' ')[0], namespac + left.SplitClean(' ')[1], right.Split(';')[0], isConstant);
            }
            catch
            {
                result = null;
            }

            return result != null;
        }
    }
}
