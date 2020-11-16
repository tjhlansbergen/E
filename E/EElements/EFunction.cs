using System;
using System.Collections.Generic;
using EInterpreter.EObjects;

namespace EInterpreter.EElements
{
    public class EFunction : EElement
    {
        public EProperty ReturnType { get; }

        public List<EProperty> Parameters { get; }

        public List<EElement> Elements = new List<EElement>();

        public EFunction(string returnType, string name, List<EProperty> parameters) : base(name)
        {
            ReturnType = new EProperty(returnType, name);
            Parameters = parameters;
        }
    }
}
