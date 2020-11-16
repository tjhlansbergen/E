using System.Collections.Generic;
using System.IO;

namespace EInterpreter.Validation
{
    public interface IValidator<T>
    {
        bool Validate(string[] lines,  bool verbose);
    }
}