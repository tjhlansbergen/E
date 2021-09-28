using System.Collections.Generic;
using EBuildIn;

namespace EInterpreter.EElements
{
    public class EStatement : EElement, IRunnableBlock
    {
        public EStatementType Type { get; set; }
        public string EvaluableVariableName { get; }

        public List<EElement> Elements { get; }

        public EStatement(string name, EStatementType type, string evaluableVariable) : base(name)
        {
            Type = type;
            EvaluableVariableName = evaluableVariable;
            Elements = new List<EElement>();
        }
    }
}
