using System.Collections.Concurrent;

namespace NTierMvcFramework.Common.WebAPIToolkit
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