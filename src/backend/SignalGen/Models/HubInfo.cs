using System;
using System.Collections.Generic;

namespace SignalGen.Models
{
    public class HubInfo
    {
        public string Name { get; set; }
        public List<MethodInfo> Methods { get; set; }
    }
}