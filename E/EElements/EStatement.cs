using EInterpreter.EObjects;
using System.Collections.Generic;

namespace EInterpreter.EElements
{
    public class EStatement : EElement
    {
        public List<EElement> Elements { get; } = new List<EElement>();

        public EStatement(string name) : base(name) { }
    }
}
