using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInterpreter.EObjects;

namespace EInterpreter.Validation
{
    public interface IPostValidationStep
    {
        ValidationStepResult Execute(ETree tree);
    }

    public class HasUniqueIdentifiers : IPostValidationStep
    {
        public ValidationStepResult Execute(ETree tree)
        {
            var valid = true;   // TODO
            return new ValidationStepResult(valid, valid ? "All identifiers have unique names" : "Not all identifiers have unique names");
        }
    }

    // TODO
}
