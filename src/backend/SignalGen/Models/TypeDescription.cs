using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGen.Models
{
    public class TypeDescription
    {
        public string PropertyName { get; set; }
        public string TypeName { get; set; }
        public List<TypeDescription> Fields { get; set; } = new List<TypeDescription>();
        public bool IsCollection { get; set; }
    }
}
