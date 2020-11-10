using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Lexer
{
    class EToken
    {
        public int LineNumber { get; }
        public ETokenType Type { get; }
        public string Line { get; set; }

        public EToken(int linenr, ETokenType type, string line = "")
        {
            LineNumber = linenr;
            Type = type;
            Line = line;
        }

        public override string ToString()
        {
            return $"{LineNumber}\t{Type}\t{Line}";
        }
    }
}
