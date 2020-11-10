using System;
using System.Collections.Generic;

namespace E.Lexer
{
    class Tokenizer
    {

        /// <summary>
        /// Responsible for tokenizing an ordered array of lines of E source code
        /// </summary>
        /// <param name="lines">Ordered array of lines of E source code</param>
        /// <returns>Unordered list of ETokens</returns>
        public List<EToken> Tokenize(string[] lines)
        {
            var result = new List<EToken>();

            for (int i = 0; i < lines.Length; i++)
            {
                var linenr = i + 1;
                var trimmedLine = lines[i].Trim();

                var token = _tokenizeLine(trimmedLine, linenr);
                if (token != null)
                {
                    result.Add(token);
                }
                else
                {
                    // WARNING
                }
            }

            return result;
        }

        private EToken _tokenizeLine(string line, int linenr)
        {
            foreach (var lineType in Enum.GetValues(typeof(ETokenType)))
            {
                switch (lineType)
                {
                    case ETokenType.WHITESPACE:
                        if (line == string.Empty)
                        {
                            return new EToken(linenr, ETokenType.WHITESPACE);
                        }

                        break;
                    case ETokenType.COMMENT:
                        if (line.StartsWith("//"))
                        {
                            return new EToken(linenr, ETokenType.COMMENT);
                        }

                        break;
                    case ETokenType.CONSTANT:
                        if (line.StartsWith("Constant "))
                        {
                            return new EToken(linenr, ETokenType.CONSTANT, line);
                        }

                        break;
                    case ETokenType.OPEN:
                        if (line == "{")
                        {
                            return new EToken(linenr, ETokenType.OPEN);
                        }

                        break;
                    case ETokenType.CLOSE:
                        if (line == "}")
                        {
                            return new EToken(linenr, ETokenType.CLOSE);
                        }

                        break;
                    case ETokenType.OBJECT:
                        if (line.StartsWith("Object"))
                        {
                            return new EToken(linenr, ETokenType.OBJECT, line);
                        }

                        break;
                    case ETokenType.UTILITY:
                        if (line.StartsWith("Utility"))
                        {
                            return new EToken(linenr, ETokenType.UTILITY, line);
                        }

                        break;
                    case ETokenType.FUNCTION:
                        if (line.StartsWith("Function"))
                        {
                            return new EToken(linenr, ETokenType.FUNCTION, line);
                        }

                        break;
                    case ETokenType.DECLARATION:
                        if (_isDeclaration(line))
                        {
                            return new EToken(linenr, ETokenType.DECLARATION, line);
                        }

                        break;
                    case ETokenType.FUNCTION_ASSIGNMENT:
                        if (_isFunctionAssignment(line))
                        {
                            return new EToken(linenr, ETokenType.FUNCTION_ASSIGNMENT, line);
                        }

                        break;
                    case ETokenType.FUNCTION_CALL:
                        if (_isFunctionCall(line))
                        {
                            return new EToken(linenr, ETokenType.FUNCTION_CALL, line);
                        }

                        break;
                    case ETokenType.FUNCTION_STATEMENT:
                        if (_isFunctionStatement(line))
                        {
                            return new EToken(linenr, ETokenType.FUNCTION_STATEMENT, line);
                        }

                        break;
                    case ETokenType.FUNCTION_RETURN:
                        if (line.StartsWith("return"))
                        {
                            return new EToken(linenr, ETokenType.FUNCTION_RETURN, line);
                        }

                        break;
                    default:
                        return null;
                }
            }

            return null;
        }


        private bool _isDeclaration(string line)
        {
            return line.SplitClean(' ').Length == 2;
        }

        private bool _isFunctionAssignment(string line)
        {
            return line.SplitClean('=').Length == 2;
        }

        private bool _isFunctionCall(string line)
        {
            return line.SplitClean(':').Length == 2;
        }

        private bool _isFunctionStatement(string line)
        {
            return line.SplitClean(' ').Length > 1 && Enum.TryParse<EStatement>(line.SplitClean(' ')[0], true, out _);
        }
    }
}
