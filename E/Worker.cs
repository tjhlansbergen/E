using System;
using System.Collections.Generic;
using System.IO;
using EInterpreter.EObjects;
using EInterpreter.Validation;

namespace EInterpreter
{
    public class Worker
    {
        public bool Verbose { get; set; } = true;

        private string _name;
        private string[] _lines;
        private ETree _tree;

        public Worker(TextWriter outputChannel = null)
        {
            // divert output if requested
            if (outputChannel != null) Console.SetOut(outputChannel);
        }

        public void Go(string[] lines, string name)
        {
            _tree = null;
            _lines = lines;
            _name = name;

            try
            {
                _preValidate();
                _lex();
                _postValidate();

                // Run
            }
            catch (Exception ex)
            {
                //TODO Display error message message
                Console.WriteLine(Environment.NewLine + ex.Message);
            }
        }

        private void _preValidate()
        {
            Console.WriteLine("\nPre-validation");
            var preValidationResult = new PreValidator(new List<IPreValidationStep>
            {
                new HasValidLineEndsStep(),
                new HasMatchingQuotes(),
                new HasMatchingBraces(),
                new ContainsProgramUtility(),
                new ContainsStartFunction(),
                new BlockOpeningsOk(),
                new BlockDeclarationsOk(),
                new ConstantsOk(),
                new PropertiesOk()
            }).Validate(_lines, Verbose);

            Console.WriteLine();
            Console.WriteLine(preValidationResult ? $"Pre-validation for {_name} successful" : $"Pre-validation for {_name} failed!");
        }

        private void _lex()
        {
            Console.WriteLine("\nLexing");
            _tree = new Lexer.Lexer().GetTree(_lines, Verbose);
        }

        private void _postValidate()
        {
            var postValidationResult = true;    // TODO

            Console.WriteLine();
            Console.WriteLine(postValidationResult ? $"Post-validation for {_name} successful" : $"Post-validation for {_name} failed!");
        }
    }
}
