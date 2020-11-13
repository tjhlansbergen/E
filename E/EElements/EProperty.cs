using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.EObjects;

namespace E.EElements
{
    public class EProperty : EElement
    {
        public EType Type { get; }
        public string UserType { get; }

        public EProperty(string type, string name) : base(name)
        {
            Type = Enum.TryParse<EType>(type, true, out var result) ? result : EType.USER_DEFINED;
            if (Type == EType.USER_DEFINED)
            {
                UserType = type; 
            }
        }
    }
}
