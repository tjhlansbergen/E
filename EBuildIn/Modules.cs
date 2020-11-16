using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;

namespace EBuildIn
{
    public class Modules
    {
        private static readonly string namespac = "EBuildIn";

        public static bool Find(string moduleName)
        {
            return _getTypesInThisNamespace()
                .Select(t => t.Name)
                .Contains(moduleName);
        }

        public static void Run(string moduleName, string functionName, object[] parameters)
        {
            // TODO error handling (e.g. function not found etc...)

            Type t = Type.GetType($"{namespac}.{moduleName}");
            MethodInfo method = t?.GetMethod(functionName, BindingFlags.Static | BindingFlags.Public);

            method?.Invoke(null, parameters);
        }

        private static Type[] _getTypesInThisNamespace()
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetTypes()
                    .Where(t => String.Equals(t.Namespace, namespac, StringComparison.Ordinal))
                    .ToArray();
        }
    }
}
