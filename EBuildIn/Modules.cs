using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;

namespace EBuildIn
{
    public static class Modules
    {
        private static readonly string namespac = "EBuildIn";

        public static List<string> FindFunctionAndReturnParameters(string module, string function)
        {
            function += "Parameters";
            var type = Type.GetType($"{namespac}.{module}");
            var propertyInfo = type?.GetProperty(function, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
            return (List<string>)propertyInfo?.GetValue(null);
        }

        public static Variable Run(string module, string function, object[] parameters)
        {
            // TODO error handling (e.g. function not found etc...)

            var type = Type.GetType($"{namespac}.{module}");
            var method = type?.GetMethod(function, BindingFlags.Static | BindingFlags.Public);

            method?.Invoke(null, parameters);

            // TODO handle return value

            return new Variable("boolean", true);
        }
    }
}
