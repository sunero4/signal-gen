using System.Collections.Generic;

namespace SignalGen.Models
{
    public class MethodInfo
    {
        public string Name { get; set; }
        public List<ArgumentInfo> Arguments { get; set; }
    }
}