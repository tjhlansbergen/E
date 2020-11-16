using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInterpreter.EObjects;

namespace EInterpreter.Validation
{
    class PostValidator : IValidator<ETree>
    {
        private readonly List<IPostValidationStep> _steps;

        public PostValidator(List<IPostValidationStep> steps)
        {
            _steps = steps;
        }

        public bool Validate(ETree tree, bool verbose)
        {
            var results = new ConcurrentBag<ValidationStepResult>();

            // run steps in parallel
            Parallel.ForEach(_steps, (step) =>
            {
                var result = step.Execute(tree);
                results.Add(result);
            });

            if (verbose)
            {
                foreach (var validationStepResult in results)
                {
                    ExtensionMethods.WriteColoredLine(" - " + validationStepResult.Output, validationStepResult.Valid ? ConsoleColor.Green : ConsoleColor.Red);
                }
            }

            return results.All(r => r.Valid);
        }
    }
}
