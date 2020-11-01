using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace E.Validation
{
    public class PreValidator : IValidator<string[]>
    {
        private readonly List<IPreValidationStep> _steps;
        public static readonly string[] Blocks = { "Utility", "Object", "Function" };

        public PreValidator(List<IPreValidationStep> steps)
        {
            _steps = steps;
        }

        public bool Validate(string[] lines, bool verbose, TextWriter outputChannel = null)
        {
            // divert output if requested
            if(outputChannel != null) Console.SetOut(outputChannel);

            var cleanLines = _clean(lines);
            var results = new ConcurrentBag<ValidationStepResult>();

            // run steps in parallel
            Parallel.ForEach(_steps, (step) =>
            {
                var result = step.Execute(cleanLines);
                results.Add(result);
            });

            if (verbose)
            {
                foreach (var validationStepResult in results)
                {
                    Helpers.WriteColoredLine(" - " + validationStepResult.Output, validationStepResult.Valid);
                }
            }

            return results.All(r => r.Valid);
        }

        private static string[] _clean(string[] lines)
        {
            //strip comments and stuff between double quotes
            var regex = new Regex("\"([^\"]*)\"");

            for (int i = 0; i < lines.Length; i++)
            {
                //remove indentation
                lines[i] = lines[i].Trim();

                //empty line if it is a comment
                if (lines[i].StartsWith("//")) lines[i] = string.Empty;

                //remove text between double quotes
                lines[i] = regex.Replace(lines[i], "\"\"");
            }

            return lines;
        }
    }
}
