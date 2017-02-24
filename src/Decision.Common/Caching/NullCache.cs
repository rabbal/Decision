using System;

namespace Decision.Common.Caching
{
    public class NullCache : ICache
    {
        public static NullCache Instance => new NullCache();

        public void SetString(string key, string objectToCache, TimeSpan? expiry = null)
        {
        }

        public void Set<T>(string key, T objectToCache, TimeSpan? expiry = null) where T : class
        {
        }

        public string GetString(string key)
        {
            return string.Empty;
        }

        public T Get<T>(string key) where T : class
        {
            return default(T);
        }

        public void Delete(string key)
        {
        }

        public void FlushAll()
        {
        }

    }
}