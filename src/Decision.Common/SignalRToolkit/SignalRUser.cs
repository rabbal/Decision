using System.Collections.Generic;

namespace Decision.Common.SignalRToolkit
{
    public class SignalRUser
    {
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}