using System.Collections.Concurrent;

namespace Decision.Common.WebAPIToolkit
{
    public static class Global
    {
        static Global()
        {
            Storage = new ConcurrentDictionary<string, object>();
        }

        public static ConcurrentDictionary<string, object> Storage { get; set; }
    }
}