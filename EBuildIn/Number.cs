using System;
using System.Collections.Generic;

namespace EBuildIn
{
    class Number
    {
        public static List<string> AddParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> SubtractParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> MultiplyParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> DivideParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> RemainderParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };

        public static Variable Add(Variable var, Variable value)
        {
            return Operater(var, value, (x, y) => x + y);
        }

        public static Variable Subtract(Variable var, Variable value)
        {
            return Operater(var, value, (x, y) => x - y);
        }

        public static Variable Divide(Variable var, Variable value)
        {
            return Operater(var, value, (x, y) => x / y);
        }

        public static Variable Multiply(Variable var, Variable value)
        {
            return Operater(var, value, (x, y) => x * y);
        }

        public static Variable Remainder(Variable var, Variable value)
        {
            return Operater(var, value, (x, y) => x % y);
        }

        private static Variable Operater(Variable var, Variable value, Func<double, double, double> operate)
        {
            if (TryConvertValue(value, out var result))
            {
                var.Value = operate((double)var.Value, result);
                return new Variable(Types.Boolean.ToString(), true);
            }
            else
            {
                return new Variable(Types.Boolean.ToString(), false);
            }
        }

        private static bool TryConvertValue(Variable value, out double result)
        {
            try
            {
                result = value.Value is double d ? d : Convert.ToDouble(value.Value);
                return true;
            }
            catch
            {
                result = double.NaN;
                return false;
            }
        }
    }
}
