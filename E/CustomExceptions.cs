using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInterpreter.Lexer
{
    class ParserException : Exception
    {
        public ParserException(string message) : base(message) { }
    }

    class PreValidationException : Exception
    {
        public PreValidationException(string message) : base(message) { }
    }
    class LexerException : Exception
    {
        public LexerException(string message) : base(message) { }
    }
    class PostValidationException : Exception
    {
        public PostValidationException(string message) : base(message) { }
    }
    class EngineException : Exception
    {
        public EngineException(string message) : base(message) { }
    }
}
