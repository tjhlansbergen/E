namespace E.EObjects
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

    public class EVariable : EElement
    {
        public bool IsConstant { get; }

        public EVariable(string name, bool constant = false) : base(name)
        {
            IsConstant = constant;
        }


        public static EVariable New(string type, string name, string value, bool constant)
        {
            return type.ToLowerInvariant() switch
            {
                // TODO make this more generic, need to support Constant Objects as well
                "boolean" => bool.TryParse(value, out var val) ? new EVariable<bool>(name, val, constant) : null,
                "text" => new EVariable<string>(name, value, constant),
                "number" => double.TryParse(value, out var val) ? new EVariable<double>(name, val, constant) : null,
                "list" => null,     // TODO
                _ => null
            };
        }
    }
}
