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

        public List<string> Warnings { get; set; } = new List<string>();

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
                    _handleConstant(token, tree);
                    break;
                default:
                    Warnings.Add($"Unhandled token type {token.Type} at line: {token.LineNumber}, this line will be ignored!");
                    return;
            }
        }

        private void _handleConstant(EToken token, ETree tree)
        {
            if (Parsers.ParseVariable(curNamespace, token.Line.Remove(0, 9), out var result, true))
            {
                tree.Constants.Add(result);
            }
            else
            {
                Warnings.Add($"Unparsable constant at line: {token.LineNumber}, this constant will be ignored!");
            }
        }
    }
}
