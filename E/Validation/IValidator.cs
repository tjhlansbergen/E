using System.Collections.Generic;
using System.IO;

namespace EInterpreter.Validation
{
    public interface IValidator<T>
    {
        bool Validate(T content,  bool verbose);
    }
}