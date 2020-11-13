using System;
using E.EObjects;
using System.Collections.Generic;

namespace E.EElements
{
    public class EFunction : EElement
    {
        public EProperty ReturnType { get; }

        public List<EProperty> Parameters { get; }

        public List<EVariable> Variables { get; set; } = new List<EVariable>();

        public List<EStatement> Statements { get; } = new List<EStatement>();

        public List<EInitialization> Inits { get; } = new List<EInitialization>();

        public EFunction(string returnType, string name, List<EProperty> parameters) : base(name)
        {
            ReturnType = new EProperty(returnType, name);
            Parameters = parameters;
        }
    }
}
