using E.EObjects;
using System.Collections.Generic;

namespace E.EElements
{
    public class EUtility : EElement
    {
        public List<EFunction> Functions { get; set; } = new List<EFunction>();

        public EUtility(string name) : base(name) { }
    }
}
