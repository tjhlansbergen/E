using System;
using System.CodeDom;
using E.EObjects;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using E.EElements;


namespace E.Lexer
{
    /// <summary>
    /// Responsible for parsing a list of tokens into an Abstract Source Tree
    /// </summary>
    class Parser
    {
        private Stack<EElement> _callStack = new Stack<EElement>();

        /// <summary>
        /// Build a ETree out of a list of ETokens
        /// </summary>
        /// <param name="tokens">Unordered list of ETokens</param>
        /// <returns></returns>
        public ETree Parse(IEnumerable<EToken> tokens)
        {
            var tree = new ETree();

            var orderedTokens = tokens.OrderBy(t => t.LineNumber).ToList();

            try
            {
                foreach (var token in orderedTokens)
                {
                    _processToken(token, tree);
                }
            }
            catch (ParserException pex)
            {
                Helpers.WriteColoredLine(pex.Message, false);
                return null;
            }

            return tree;
        }

        private string _getNamespac()
        {
            return _callStack.Any() ? string.Join(".", _callStack.Select(s => s.Name)) + "." : string.Empty;
        }

        private void _processToken(EToken token, ETree tree)
        {
            switch (token.Type)
            {
                case ETokenType.WHITESPACE:
                case ETokenType.COMMENT:
                case ETokenType.OPEN:
                    // do nothing
                    break;
                case ETokenType.CLOSE:
                    _handleClose();
                    break;
                case ETokenType.CONSTANT:
                    _handleConstant(token, tree);
                    break;
                case ETokenType.OBJECT:
                    _handleObject(token, tree);
                    break;
                case ETokenType.PROPERTY:
                    _handleProperty(token);
                    break;
                case ETokenType.UTILITY:
                    _handleUtility(token, tree);
                    break;
                case ETokenType.FUNCTION:
                    _handleFunction(token, tree);
                    break;
                case ETokenType.FUNCTION_STATEMENT:
                    _handleStatement(token);
                    break;
                case ETokenType.INITIALIZATION:
                    _handleInitialization(token);
                    break;
                case ETokenType.FUNCTION_CALL:
                    _handleFunctionCall(token);
                    break;
                case ETokenType.FUNCTION_RETURN:
                    _handleFunctionReturn(token);
                    break;
                default:    // REENABLE!
                    throw new ParserException($"Unhandled token type {token.Type} at line: {token.LineNumber}");
                    break;
            }
        }

        private void _handleClose()
        {
            if(_callStack.Any()) _callStack.Pop();
        }

        private void _handleConstant(EToken token, ETree tree)
        {
            if (Parsers.ParseVariable(token.Line, out var result, true))
            {
                tree.Constants.Add(result);
            }
            else
            {
                throw new ParserException(_unparsebleMessage("constant", token.LineNumber));
            }
        }

        private void _handleObject(EToken token, ETree tree)
        {
            if (Parsers.ParseObject(_getNamespac(), token.Line, out var result))
            {
                tree.Objects.Add(result);
                _callStack.Push(result);
            }
            else
            {
                throw new ParserException(_unparsebleMessage("object", token.LineNumber));
            }
        }

        private void _handleUtility(EToken token, ETree tree)
        {
            if (Parsers.ParseUtility(_getNamespac(), token.Line, out var result))
            {
                tree.Utilities.Add(result);
                _callStack.Push(result);
            }
            else
            {
                throw new ParserException(_unparsebleMessage("utility", token.LineNumber));
            }
        }

        private void _handleFunction(EToken token, ETree tree)
        {
            if (_callStack.Any() && _callStack.Peek() is EUtility util)
            {
                if (Parsers.ParseFunction(_getNamespac(), token.Line, out var result))
                {
                    util.Functions.Add(result);
                    _callStack.Push(result);
                }
                else
                {
                    throw new ParserException(_unparsebleMessage("function", token.LineNumber));
                }
            }
            else
            {
                throw new ParserException(_unexpectedMessage("function", token.LineNumber));
            }
        }

        private void _handleStatement(EToken token)
        {
            if (_callStack.Any() && _callStack.Peek() is EFunction func)
            {
                if (Parsers.ParseStatement(token.Line, out var result))
                {
                    func.Statements.Add(result);
                    _callStack.Push(result);
                }
                else
                {
                    throw new ParserException(_unparsebleMessage("statement", token.LineNumber));
                }
            }
            else
            {
                throw new ParserException(_unexpectedMessage("statement", token.LineNumber));
            }
        }

        private void _handleInitialization(EToken token)
        {
            if (_callStack.Any() && _callStack.Peek() is EFunction func)
            {
                if (Parsers.ParseNew(token.Line, out var result))
                {
                    func.Inits.Add(result);
                }
                else
                {
                    throw new ParserException(_unparsebleMessage("object initialization", token.LineNumber));
                }
            }
            else
            {
                throw new ParserException(_unexpectedMessage("object initialization", token.LineNumber));
            }
        }

        private void _handleFunctionCall(EToken token)
        {
            // TODO
        }

        private void _handleFunctionReturn(EToken token)
        {
            // TODO
        }

        private void _handleProperty(EToken token)
        {
            if (_callStack.Any() && _callStack.Peek() is EObject obj)
            {
                if (Parsers.ParseProperty(token.Line, out var result))
                {
                    obj.Properties.Add(result);
                }
                else
                {
                    throw new ParserException(_unparsebleMessage("property", token.LineNumber));
                }
            }
            else
            {
                throw new ParserException(_unexpectedMessage("poperty", token.LineNumber));
            }
        }

        private static string _unparsebleMessage(string name, int linenr)
        {
            return $"Parse Error: Unparsable {name} at line: {linenr}";
        }

        private static string _unexpectedMessage(string name, int linenr)
        {
            return $"Parse Error: Unexpected {name} at line: {linenr}";
        }

    }
}
