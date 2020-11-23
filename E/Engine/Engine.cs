using EInterpreter.EElements;
using EInterpreter.EObjects;
using System;
using System.Diagnostics;
using System.Linq;

namespace EInterpreter.Engine
{
    class Engine
    {
        public TimeSpan Duration { get; private set; }
        public bool Result { get; private set; }

        public void Run(ETree tree)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Restart();

            _run(tree);

            stopwatch.Stop();
            Duration = stopwatch.Elapsed;
        }

        private void _run(ETree tree)
        {
            // find and run the singular Start Function, in the singular Program Utility
            Result = _runFunction(tree.Utilities.Single(u => u.Name == "Program").Functions.Single(f => f.Name == "Program.Start"));
        }

        private bool _runFunction(EFunction function)
        {
            // TODO handle parameters

            foreach (var element in function.Elements)
            {
                switch (element)
                {
                    case EFunctionCall functionCall:
                        _handleFunctionCall(functionCall);
                        break;
                }
            }


            return true; // TODO
        }

        private void _handleFunctionCall(EFunctionCall call)
        {
            // TODO try as non-build-in function first, this way the user can hide build-in functions if desired
            // TODO check if parameters match
            //_runFunction(the parsed function);

            // check if we have a build-in function
            if (EBuildIn.Modules.Find(call.Parent))
            {
                // TODO check if the module has such a function

                // TODO check if parameters match

                // run the build-in
                EBuildIn.Modules.Run(call.Parent, call.Name, call.Parameters.ToArray());    // TODO parameters as EBuild-in Types (legal since objects are not allowed here)
            }

            // TODO we should raise an error if we didn't find anything suitable to run
        }
    }
}
