using System.Collections.Generic;

namespace Decision.Framework.SignalRToolkit
{
    public class SignalRUser
    {
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}