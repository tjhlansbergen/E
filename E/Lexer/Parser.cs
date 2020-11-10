using System;
using E.EObjects;
using System.Collections.Generic;
using System.Linq;


namespace E.Lexer
{

    /// <summary>
    /// Responsible for parsing a list of tokens into an Abstract Source Tree
    /// </summary>
    class Parser
    {
        private string curNamespace = string.Empty;
        private ELevel curLevel = ELevel.ROOT;

        /// <summary>
        /// Build a ETree out of a list of ETokens
        /// </summary>
        /// <param name="tokens">Unordered list of ETokens</param>
        /// <returns></returns>
        public ETree Parse(IEnumerable<EToken> tokens)
        {
            var tree = new ETree();

            var orderedTokens = tokens.OrderBy(t => t.LineNumber).ToList();


            foreach (var token in orderedTokens)
            {
                _processToken(token, tree);
            }

            return tree;
        }

        private void _processToken(EToken token, ETree tree)
        {
            switch (token.Type)
            {
                case ETokenType.CONSTANT:
                    if (_parseVariable(token.Line.Remove(0, 9), out var result))
                    {
                        tree.Constants.Add(result);
                    }
                    else
                    {
                        // WARNING
                    }

                    break;
                default:
                    // WARNING
                    return;
            }
        }

        private bool _parseVariable(string line, out EVariable result)
        {
            try
            {
                var left = line.SplitClean('=')[0];
                var right = line.SplitClean('=')[1];
                result = EVariable.New(left.SplitClean(' ')[0], left.SplitClean(' ')[1], right.Split(';')[0]);
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result != null;
        }
    }
}
