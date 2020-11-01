using System.Collections.Generic;
using System.IO;

namespace E.Validation
{
    public interface IValidator<T>
    {
        bool Validate(string[] lines,  bool verbose, TextWriter outputChannel);
    }
}