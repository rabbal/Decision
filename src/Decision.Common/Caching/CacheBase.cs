using System;
using Newtonsoft.Json;

namespace Decision.Common.Caching
{
    public abstract class CacheBase : ICache
    {
        public void Set<T>(string key, T objectToCache, TimeSpan? expiry = null) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                var serializedObjectToCache = JsonConvert.SerializeObject(objectToCache
                     , Formatting.Indented
                     , new JsonSerializerSettings
                     {
                         ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                         PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                         TypeNameHandling = TypeNameHandling.All
                     });

                SetStringProtected(key, serializedObjectToCache, expiry);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot Set {key}", e);
            }

        }

        public T Get<T>(string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            try
            {
                var stringObject = GetStringProtected(key);
                if (stringObject == null)
                {
                    return default(T);
                }

                var obj = JsonConvert.DeserializeObject<T>(stringObject
                    , new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                        TypeNameHandling = TypeNameHandling.All
                    });
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot Get {key}", e);
            }
        }

        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                DeleteProtected(key);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot Delete key {key}", e);
            }

        }

        public void DeleteByPattern(string prefixKey)
        {
            if (string.IsNullOrEmpty(prefixKey))
            {
                throw new ArgumentNullException(nameof(prefixKey));
            }

            try
            {
                DeleteByPatternProtected(prefixKey);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot DeleteByPattern key {prefixKey}", e);
            }

        }

        public void FlushAll()
        {

            try
            {
                FlushAllProtected();
            }
            catch (Exception e)
            {
                throw new Exception("Cannot Flush the Cached Data", e);
            }

        }

        public string GetString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                return GetStringProtected(key);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot Get key {key}", e);
            }
        }

        public void SetString(string key, string objectToCache, TimeSpan? expiry = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                SetStringProtected(key, objectToCache, expiry);
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot Set {key}", e);
            }

        }


        protected abstract void SetStringProtected(string key, string objectToCache, TimeSpan? expiry = null);
        protected abstract string GetStringProtected(string key);
        protected abstract void DeleteProtected(string key);
        protected abstract void FlushAllProtected();
        protected abstract void DeleteByPatternProtected(string key);
        public abstract bool IsCacheRunning { get; }
    }
}
