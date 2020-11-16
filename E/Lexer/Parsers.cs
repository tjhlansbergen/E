using EInterpreter.EObjects;
using System.Collections.Generic;
using EInterpreter.EElements;

namespace EInterpreter.Lexer
{
    public static class Parsers
    {
        public static EConstant ParseConstant(string line)
        {
            if (line.StartsWith("Constant"))
            {
                line = line.Remove(0, "Constant".Length);
            }
            var left = line.SplitClean('=')[0];
            var right = line.SplitClean('=')[1];
            

            return new EConstant(left.SplitClean(' ')[0], left.SplitClean(' ')[1], right.Split(';')[0]);
        }

        public static EObject ParseObject(string namespac, string line)
        {
            return new EObject(namespac + line.SplitClean(' ')[1]);
        }

        public static EUtility ParseUtility(string namespac, string line)
        {
            return new EUtility(namespac + line.SplitClean(' ')[1]);
        }

        public static EFunction ParseFunction(string namespac, string line)
        {
            line = line.SplitClean(')')[0];
            var left = line.SplitClean('(')[0].SplitClean(' ');

            var parameters = new List<EProperty>();

            if (line.SplitClean('(').Length  > 1)
            {
                foreach (var p in line.SplitClean('(')[1].SplitClean(','))
                {
                    parameters.Add(ParseProperty(p));
                }
            }
            return new EFunction(left[1], namespac + left[2], parameters);
        }

        public static EProperty ParseProperty(string line)
        {
            if (line.StartsWith("Property"))
            {
                line = line.Remove(0, "Property".Length);
            }

            var lineArr = line.SplitClean(' ');
            return new EProperty(lineArr[0], lineArr[1]);
        }

        public static EStatement ParseStatement(string line)
        {
            return new EStatement("some name"); // TODO 
        }

        public static EDeclaration ParseInit(string line)
        {
            var lineArr = line.SplitClean(' ');
            
            return new EDeclaration(lineArr[1], lineArr[2]);
        }

        public static EFunctionCall ParseFunctionCall(string line)
        {
            var lineArr = line.SplitClean(':');
            var left = lineArr[0];
            var right = lineArr[1];

            var rightArr = right.SplitClean(';')[0].SplitClean('(');
            var parameters = new List<string>();

            if (rightArr.Length > 1)
            {
                foreach (var p in rightArr[1].SplitClean(','))
                {
                    parameters.Add(p);
                }
            }

            return new EFunctionCall(left, rightArr[0], parameters);
        }
    }
}
