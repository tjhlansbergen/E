using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInterpreter.EObjects;

namespace EInterpreter.EElements
{
    public class EProperty : EElement
    {
        public string Type { get; }

        public EProperty(string type, string name) : base(name)
        {
             Type = type;
        }
    }
}
