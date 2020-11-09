using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.EObjects.Variables
{
    public class EVariable<T> : EVariable
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (!IsConstant)
                {
                    _value = value;
                }
            }
        }

        public EVariable(string name, T value, bool constant = false) : base(name, constant)
        {
            _value = value;
            
        }
    }

    public class EVariable
    {
        public bool IsConstant { get; }

        public string Name { get; }

        public EVariable(string name, bool constant = false)
        {
            Name = name;
            IsConstant = constant;
        }


        public static EVariable New(string type, string name, string value, bool constant = false)
        {
            return type.ToLowerInvariant() switch
            {
                "boolean" => bool.TryParse(value, out var val) ? new EVariable<bool>(name, val, constant) : null,
                "text" => new EVariable<string>(name, value, constant),
                "number" => double.TryParse(value, out var val) ? new EVariable<double>(name, val, constant) : null,
                "list" => null,     // TODO
                _ => null
            };
        }
    }
}
