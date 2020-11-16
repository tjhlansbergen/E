using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInterpreter.EObjects;

namespace EInterpreter.EElements
{
    public class EObject : EElement
    {
        public List<EProperty> Properties { get; set; } = new List<EProperty>();

        public EObject(string name) : base(name) { }
    }
}
