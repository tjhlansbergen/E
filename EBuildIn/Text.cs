using System.Collections.Generic;

namespace EBuildIn
{
    public static class Text
    {
        public static List<string> SetParameters => new List<string> { Types.Text.ToString(), Types.Text.ToString() };

        public static Variable Set(Variable var, Variable text)
        {
            var.Value = text.Value;
            return new Variable(Types.Boolean.ToString(), true);
        }
    }
}
