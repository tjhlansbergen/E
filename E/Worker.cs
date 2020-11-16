using System;
using System.Collections.Generic;
using System.IO;
using EInterpreter.EObjects;
using EInterpreter.Lexer;
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
                _runEngine();
            }
            catch (Exception ex)
            {
                ExtensionMethods.WriteColoredLine(ex.Message, ConsoleColor.Red);
            }
        }

        private void _preValidate()
        {
            ExtensionMethods.WriteColoredLine("Pre-validation: ", ConsoleColor.Blue);
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
            Console.WriteLine(preValidationResult ? $"Pre-validation for `{_name}` successful" : $"Pre-validation for {_name} failed!");
            Console.WriteLine();
            if(preValidationResult == false) { throw new PreValidationException("EInterpreter stopped before execution because of failing Pre-validation");}
        }

        private void _lex()
        {
            ExtensionMethods.WriteColoredLine("Lexing: ", ConsoleColor.Blue);
            _tree = new Lexer.Lexer().GetTree(_lines, false);
            if (_tree != null) return;

            ExtensionMethods.WriteColoredLine("The parser did not return an object tree!", ConsoleColor.Red);
            throw new LexerException("EInterpreter stopped before execution because of a lexer exception");
        }

        private void _postValidate()
        {
            ExtensionMethods.WriteColoredLine("Post-validation: ", ConsoleColor.Blue);
            var postValidationResult = new PostValidator(new List<IPostValidationStep>
            {
                new HasUniqueIdentifiers()
            }).Validate(_tree, Verbose);

            Console.WriteLine();
            Console.WriteLine(postValidationResult ? $"Post-validation for `{_name}` successful" : $"Post-validation for {_name} failed!");
            Console.WriteLine();
            if(postValidationResult == false) { throw new PostValidationException("EInterpreter stopped before execution because of failing Post-validation"); }
        }

        private void _runEngine()
        {
            try
            {
                new Engine.Engine().Run(_tree);
            }
            catch (Exception ex)
            {
                throw new EngineException($"Runtime error: {ex.Message}");
            }
            
        }
    }
}
