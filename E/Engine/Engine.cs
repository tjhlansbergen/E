﻿using EInterpreter.EElements;
using EInterpreter.EObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using EBuildIn;
using EInterpreter.Lexer;
using Console = System.Console;

namespace EInterpreter.Engine
{
    public class Engine
    {
        public TimeSpan Duration { get; private set; }
        public bool Result { get; private set; }

        private ETree _tree;
        private List<Variable> _stack = new List<Variable>();


        public void Run(ETree tree)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Restart();

            _tree = tree;
            _run();
            _tree = null;

            stopwatch.Stop();
            Duration = stopwatch.Elapsed;
        }

        private void _run()
        {
            // throw all constants on the stack
            var globals = _tree.Constants.Select(_expandConstant);
            _stack.AddRange(globals);

            //TODO get args
            var args = new List<Variable>();

            // find and run the singular Start Function, in the singular Program Utility
            Result = (bool)_runBlock(_tree.Utilities.Single(u => u.Name == "Program").Functions.Single(f => f.Name == "Program.Start"), args).Value;
        }

        private Variable _runBlock(IRunnableBlock block, List<Variable> variables)
        {
            var scope = block.Name;
            variables.ForEach(v => v.Scope = scope);
            _stack.AddRange(variables);

            for (var i = 0; i < block.Elements.Count; i++)
            {
                var element = block.Elements[i];

                switch (element)
                {
                    case EFunctionCall functionCall:
                        {
                            var result = _handleFunctionCall(functionCall);
                            result.Scope = scope;
                            _stack.Add(result);
                        }
                        break;
                    case EDeclaration declaration:
                        {
                            var var = new Variable(declaration.Prop.Type, null, scope);
                            var.Name = declaration.Name;
                            _stack.Add(var);
                        }
                        break;
                    case EReturn returnStatement:
                        {
                            var result = _expandParameter(returnStatement.Parameter);
                            _stack.RemoveAll(v => v.Scope == scope);
                            return result;
                        }

                    case EStatement statement:
                        {
                            var result = _handleStatement(statement);
                            if (!result.IsEmpty)
                            {
                                _stack.RemoveAll(v => v.Scope == scope);
                                return result;
                            }
                        }
                        break;
                }
            }

            _stack.RemoveAll(v => v.Scope == scope);

            if (block is EStatement)
            {
                // statements can finish without returning something, we return a dummy that's get ignored by the parent/caller
                return Variable.Empty;
            }
            else
            {
                // by now, we should have come across at least one return statement, if not that's an error
                throw new EngineException($"Function {block.Name} exited without return statement.");
            }
        }

        private Variable _handleStatement(EStatement statement)
        {
            // TODO evaluate the statement

            // we recursively call _runBlock, as if running a function, but empty set of parameters
            // the return value will be empty, if the statement completed, signaling we can continue,
            // or has a value, signaling we encountered a return inside the statement.
            var result = _runBlock(statement, new List<Variable>());

            // TODO start again (while)?

            return result;
        }

        private Variable _handleFunctionCall(EFunctionCall call)
        {
            var parameters = call.Parameters.Select(_expandParameter).ToList();

            // try as non-build-in function first, this way the user can hide build-in functions if desired
            var localFunction = _tree.Functions.SingleOrDefault(f => f.Name == call.FullName);


            // if we found a matching function, and it's parameters match, then run it
            if (localFunction != null && EngineHelpers.MatchAndNameParameters(parameters, localFunction))
            {
                return _runBlock(localFunction, parameters.ToList());
            }

            // check if we have a build-in function, and get it's parameters
            var targetParameters = EBuildIn.Modules.FindFunctionAndReturnParameters(call.Parent, call.Name);

            if (targetParameters != null && EngineHelpers.MatchParameters(parameters, targetParameters))
            {
                // we have match for a build in function, run it
                return Modules.Run(call.Parent, call.Name, parameters.ToArray());
            }

            // TODO we should raise an error if we didn't find anything suitable to run
            return new Variable(Types.Text.ToString(), false);
        }



        private Variable _expandParameter(string parameter)
        {
            // determine the type step by step

            // is it true/false?
            if (bool.TryParse(parameter, out bool boolean)) { return new Variable(Types.Boolean.ToString(), boolean); }

            // is it a literal double?
            if (double.TryParse(parameter, out double number)) { return new Variable(Types.Number.ToString(), number); }

            // is it a string-literal
            if (parameter.StartsWith("\"") && parameter.EndsWith("\"")) { return new Variable(Types.Text.ToString(), parameter.Replace("\"", "")); }

            // we don't support inline-lists (yet), no need to check for that

            // not some literal value, it can be a variable
            if (_stack.Exists(v => v.Name == parameter)) { return _stack.Single(v => v.Name == parameter); }

            // also not a variable, a function call then? if so call it inline and return its return value
            EFunctionCall call;
            try { call = Parsers.ParseFunctionCall(parameter); }
            catch { call = null; }
            if (call != null) { return _handleFunctionCall(call); }

            // ran out of options
            throw new EngineException($"Invalid parameter: {parameter}");
        }

        private Variable _expandConstant(EConstant constant)
        {
            var variable = _expandParameter(constant.Value);
            variable.Name = constant.Name;

            if (variable.Type != constant.Prop.Type) { throw new ParserException($"Type mismatch in constant: {constant.Name}"); }

            return variable;
        }
    }
}
