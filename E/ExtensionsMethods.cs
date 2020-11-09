using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E
{
    public static class ExtensionsMethods
    {
        public static string[] SplitClean(this string str, char ch)
        {
            return str.Split(new[] {ch}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
