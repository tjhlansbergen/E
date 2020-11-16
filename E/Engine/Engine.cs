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
            // check if we have a build-in function
            if (EBuildIn.Modules.Find(call.Parent))
            {
                EBuildIn.Modules.Run(call.Parent, call.Name, call.Parameters.ToArray());
            }

            // not a build-in function
            //_runFunction(the parsed function);
        }
    }
}
