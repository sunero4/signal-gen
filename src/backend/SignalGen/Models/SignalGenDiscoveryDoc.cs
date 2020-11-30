using System.Collections.Generic;

namespace SignalGen.Models
{
    public class SignalGenDiscoveryDoc
    {
        public List<HubInfo> HubInfo { get; set; }
        public List<TypeDescription> Types { get; set; }
    }
}