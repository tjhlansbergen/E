using EInterpreter.EObjects;
using System.Collections.Generic;

namespace EInterpreter.EElements
{
    public class EStatement : EElement, IRunnableBlock
    {
        public List<EElement> Elements { get; }

        public EStatement(string name) : base(name)
        {
            Elements = new List<EElement>();
        }
    }
}
