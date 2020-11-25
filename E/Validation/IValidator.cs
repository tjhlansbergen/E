using System.Collections.Generic;
using System.IO;

namespace EInterpreter.Validation
{
    public interface IValidator<T>
    {
        List<ValidationStepResult> Results { get; }
        bool Validate(T content);

    }
}