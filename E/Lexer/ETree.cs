using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.EObjects;
using E.Validation;

namespace E.EObjects
{
    public class ETree
    {
        public List<EVariable> Constants { get; set; }
        public List<EVariable> Variables { get; set; }

        public ETree()
        {
            Constants = new List<EVariable>();
            Variables = new List<EVariable>();
        }

        public string Summarize()
        {
            var result = new StringBuilder();

            // constants
            var constNames = Constants.Count > 0 ? "(" + string.Join(", ", Constants.Take(3).Select(c => c.Name)) + ", [...])" : "";
            result.AppendLine($"Constants: {Constants.Count} {constNames}");

            // TODO
            return result.ToString();
        }
    }
}
