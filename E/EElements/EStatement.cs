using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInterpreter.EObjects;

namespace EInterpreter.EElements
{
    public class EStatement : EElement
    {
        public List<EFunctionCall> Calls { get; } = new List<EFunctionCall>();

        public EStatement(string name) : base(name)
        {
        }
    }
}
