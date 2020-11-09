using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.EObjects.Variables;

namespace E.EObjects
{
    public class ETree
    {
        public List<EVariable> Constants { get; set; }
        public List<EVariable> Variables { get; set; }

        public string Summarize()
        {
            var result = new StringBuilder();

            result.AppendLine($"Constants: {Constants.Count} ({string.Join(", ", Constants.Take(3).Select(c => c.Name))}, [...])");

            // TODO
            return result.ToString();
        }
    }
}
