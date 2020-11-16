using System;
using System.IO;
using EInterpreter.EObjects;

namespace EInterpreter.Lexer
{

    /// <summary>
    /// The lexer is responsible for tokenizing and parsing E source code
    /// </summary>
    class Lexer
    {

        /// <summary>
        /// Create a Abstract Source Tree out lines of E source code
        /// </summary>
        /// <param name="lines">Order array of lines of E source code</param>
        /// <param name="verbose">Sets whether or not to print output</param>
        /// <param name="outputChannel">optional, set a custom output channel, when not set, this default to Console</param>
        /// <returns></returns>
        public ETree GetTree(string[] lines, bool verbose)
        {
            // tokenize
            var tokens = new Tokenizer().Tokenize(lines);

            if (verbose)
            {
                tokens.ForEach(x => Console.WriteLine(x.ToString()));
                Console.WriteLine();
            }

            // parse
            var parser = new Parser();
            var tree = parser.Parse(tokens);

            Console.WriteLine();

            if (tree != null && verbose)
            {
                Console.WriteLine(tree.Summarize());
                Console.WriteLine();
            }

            return tree;
        }
    }
}
