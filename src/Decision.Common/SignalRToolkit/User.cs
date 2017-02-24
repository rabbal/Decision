using System.Collections.Generic;

namespace NTierMvcFramework.Common.SignalRToolkit
{
    public class User
    {
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}