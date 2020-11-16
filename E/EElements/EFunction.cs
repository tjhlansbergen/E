using System;
using System.Collections.Generic;
using EInterpreter.EObjects;

namespace EInterpreter.EElements
{
    public class EFunction : EElement
    {
        public EProperty ReturnType { get; }

        public List<EProperty> Parameters { get; }

        public List<EVariable> Variables { get; } = new List<EVariable>();

        public List<EStatement> Statements { get; } = new List<EStatement>();

        public List<EDeclaration> Declarations { get; } = new List<EDeclaration>();

        public List<EFunctionCall> Calls { get; } = new List<EFunctionCall>();

        public EFunction(string returnType, string name, List<EProperty> parameters) : base(name)
        {
            ReturnType = new EProperty(returnType, name);
            Parameters = parameters;
        }
    }
}
