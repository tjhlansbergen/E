using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.EElements;
using E.EObjects;

namespace E.Lexer
{
    public static class Parsers
    {
        public static bool ParseVariable(string line, out EVariable result, bool isConstant = false)
        {
            try
            {
                if (line.StartsWith("Constant"))
                {
                    line = line.Remove(0, "Constant".Length);
                }
                var left = line.SplitClean('=')[0];
                var right = line.SplitClean('=')[1];
                result = EVariable.New(left.SplitClean(' ')[0], left.SplitClean(' ')[1], right.Split(';')[0], isConstant);
            }
            catch
            {
                result = null;
            }

            return result != null;
        }

        public static bool ParseObject(string namespac, string line, out EObject result)
        {
            try
            {
                result = new EObject(namespac + line.SplitClean(' ')[1]);
            }
            catch
            {
                result = null;
            }

            return result != null;
        }

        public static bool ParseUtility(string namespac, string line, out EUtility result)
        {
            try
            {
                result = new EUtility(namespac + line.SplitClean(' ')[1]);
            }
            catch
            {
                result = null;
            }

            return result != null;
        }

        public static bool ParseFunction(string namespac, string line, out EFunction result)
        {
            try
            {
                line = line.SplitClean(')')[0];
                var left = line.SplitClean('(')[0].SplitClean(' ');

                var parameters = new List<EProperty>();

                if (line.SplitClean('(').Length > 1)
                {
                    foreach (var p in line.SplitClean('(')[1].SplitClean(','))
                    {
                        if (ParseProperty(p, out var par))
                        {
                            parameters.Add(par);
                        }
                        else
                        {
                            result = null;
                        }
                    }
                }

                result = new EFunction(left[1], namespac + left[2], parameters);
            }
            catch
            {
                result = null;
            }

            return result != null;
        }

        public static bool ParseProperty(string line, out EProperty result)
        {
            try
            {
                if(line.StartsWith("Property"))
                {
                    line = line.Remove(0, "Property".Length);
                }
                result = new EProperty(line.SplitClean(' ')[0], line.SplitClean(' ')[1]);
            }
            catch
            {
                result = null;
            }

            return result != null;
        }

        public static bool ParseStatement(string line, out EStatement result)
        {
            try
            {
                result = new EStatement("some name"); // TODO 
            }
            catch
            {
                result = null;
            }

            return result != null;
        }

        public static bool ParseNew(string line, out EInitialization result)
        {
            try
            {
                result = new EInitialization(line.SplitClean(' ')[1], line.SplitClean(' ')[2]); 
            }
            catch
            {
                result = null;
            }

            return result != null;
        }
    }
}
