using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInterpreter.EElements;
using EInterpreter.EObjects;

namespace EInterpreter.Engine
{
    class TypeChecker
    {
        public bool MatchParameters(EFunctionCall caller, EFunction subject, ETree tree)
        {
            if(caller == null || subject == null || tree == null) { return false; }

            //TODO
            return true;
        }

        
    }
}
