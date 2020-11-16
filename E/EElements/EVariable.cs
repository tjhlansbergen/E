using System;

namespace EInterpreter.EObjects
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

        

        public EVariable(EType type, string name, T value, bool constant = false) : base(type, name, constant)
        {
            _value = value;
        }
    }

    public class EVariable : EElement
    {
        public bool IsConstant { get; }
        public EType Type { get; }

        public EVariable(EType type, string name, bool constant = false) : base(name)
        {
            IsConstant = constant;
            Type = type;
        }


        public static EVariable New(string type, string name, string value, bool constant)
        {
            var parsedType = Enum.TryParse<EType>(type, true, out var result) ? result : EType.USER_DEFINED;

            return parsedType switch
            {
                // TODO make this more generic, need to support Constant Objects as well
                EType.BOOLEAN => bool.TryParse(value, out var val) ? new EVariable<bool>(parsedType, name, val, constant) : null,
                EType.TEXT => new EVariable<string>(parsedType, name, value, constant),
                EType.NUMBER => double.TryParse(value, out var val) ? new EVariable<double>(parsedType, name, val, constant) : null,
                EType.LIST => null,     // TODO
                EType.USER_DEFINED => new EVariable(parsedType, name, false),
                _ => null
            };
        }
    }
}
