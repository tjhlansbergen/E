﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInterpreter.EObjects;

namespace EInterpreter.EElements
{
    public class EFunctionCall : EElement
    {
        public string Parent { get; }

        public List<string> Parameters { get; }

        public EFunctionCall(string parent, string name, List<string> parameters) : base(name)
        {
            Parent = parent;
            Parameters = parameters;
        }
    }
}
