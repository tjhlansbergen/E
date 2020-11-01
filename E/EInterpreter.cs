using E.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E
{
    internal class EInterpreter
    {
        private string _path;
        private string[] _lines;

        public void Go(string path)
        {
            _path = path;

            try
            {
                // check if the file exists
                if (!File.Exists(_path))
                {
                    Console.WriteLine($"No file found at:\n{_path}");
                    Console.WriteLine();
                    return;
                }

                // check if the file is readable text
                if (!Helpers.IsValidTextFileAsync(_path))
                {
                    Console.WriteLine($"File {_path}");
                    Console.WriteLine("is not recognized as a valid E# file.");
                    Console.WriteLine();
                    return;
                }

                _lines = File.ReadAllLines(_path);

                PreValidate();
                // Tokenize()
                // PostValidate()
                // Run()


            }
            catch (Exception ex)
            {
                //Display error message message
                Console.WriteLine(Environment.NewLine + ex.Message);
            }
        }

        private void PreValidate()
        {
            Console.WriteLine("\nPre-validation:");
            var preValidationResult = new PreValidator(new List<IPreValidationStep>
            {
                new HasValidLineEndsStep(),
                new HasMatchingQuotes(),
                new HasMatchingBraces(),
                new ContainsProgramUtility(),
                new ContainsStartFunction(),
                new BlockOpeningsOk(),
                new BlockDeclarationsOk()
            }).Validate(_lines, true);

            Console.WriteLine();
            Console.WriteLine(preValidationResult ? $"Pre-validation for {_path} successful" : $"Pre-validation for {_path} failed!");
        }
    }
}
