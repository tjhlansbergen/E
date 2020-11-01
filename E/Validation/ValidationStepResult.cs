using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Validation
{
    public readonly struct ValidationStepResult
    {
        public readonly bool Valid;
        public readonly string Output;

        public ValidationStepResult(bool valid, string output)
        {
            Valid = valid;
            Output = output;
        }
    }
}
