using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.EElements;
using E.EObjects;
using E.Validation;

namespace E.EObjects
{
    public class ETree
    {
        public List<EVariable> Constants { get; set; } = new List<EVariable>();
        public List<EObject> Objects { get; set; } = new List<EObject>();
        public List<EUtility> Utilities { get; set; } = new List<EUtility>();

        public string Summarize()
        {
            var result = new StringBuilder();

            // constants
            result.AppendLine($"Constants: {Constants.Count}");

            // objects
            result.AppendLine($"Objects: {Objects.Count}");

            // utilities
            result.AppendLine($"Utilities: {Utilities.Count}");

            // total
            result.AppendLine($"Total number of root elements: {_countAllElements()}");

            result.AppendLine();

            return result.ToString();
        }

        private int _countAllElements()
        {
            return Constants.Count +
                   Objects.Count + 
                   Utilities.Count;
        }
    }
}
