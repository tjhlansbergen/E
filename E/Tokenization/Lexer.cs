using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using E.EObjects;
using E.EObjects.Variables;

namespace E.Tokenization
{
    class Lexer
    {
        public ETree GetTree(string[] lines)
        {
            var tree = new ETree();

            tree.Constants = getConstants(lines);

            // TODO
            return tree;
        }

        private List<EVariable> getConstants(string[] lines)
        {
            var results = new List<EVariable>();

            foreach (var line in lines.Where(ln => ln.StartsWith("Constant ")))
            {
                var left = line.SplitClean('=')[0];
                var right = line.SplitClean('=')[1];
                var cst = EVariable.New(left.SplitClean(' ')[1], left.SplitClean(' ')[2], right.Split(';')[0]);

                if (cst != null)
                {
                    results.Add(cst);
                }
                
            }

            return results;
        }


    }
}
