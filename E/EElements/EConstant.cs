using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInterpreter.EObjects;

namespace EInterpreter.EElements
{
    public class EConstant : EElement
    {
        public EProperty Prop { get; }

        public string Value { get; }

        public EConstant(string type, string name, string value) : base(name)
        {
            Prop = new EProperty(type, name);
            Value = value;
        }
    }
}
