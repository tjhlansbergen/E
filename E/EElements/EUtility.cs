﻿using System.Collections.Generic;
using EInterpreter.EObjects;

namespace EInterpreter.EElements
{
    public class EUtility : EElement
    {
        public List<EFunction> Functions { get; set; } = new List<EFunction>();

        public EUtility(string name) : base(name) { }
    }
}
