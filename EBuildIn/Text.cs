﻿using System;
using System.Collections.Generic;

namespace EBuildIn
{
    public static class Text
    {
        public static List<string> SetParameters => new List<string> { Types.Text.ToString(), Types.Text.ToString() };

        public static Variable Set(Variable var, Variable value)
        {
            if (value.Value is string)
            {
                var.Value = value.Value;
                return new Variable(Types.Boolean.ToString(), true);
            }

            try
            {
                var.Value = value.Value.ToString();
                return new Variable(Types.Boolean.ToString(), true);
            }
            catch (Exception)
            {
                return new Variable(Types.Boolean.ToString(), false);
            }
        }
    }
}
