﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInterpreter.EObjects
{
    public class EElement
    {
        public string Name { get; }

        public EElement(string name)
        {
            Name = name;
        }
    }
}
