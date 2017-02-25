using System;
using System.Web;
using System.Web.Caching;

namespace Decision.Framework.Caching
{
    public class HttpCache : CacheBase
    {
        #region Fields
        private readonly HttpContextBase _httpContext;

        #endregion

        #region Properties
        public override bool IsCacheRunning
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Costructors
        public HttpCache(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }
        #endregion

        #region Protected Methods
        protected override void SetStringProtected(string key, string objectToCache, TimeSpan? expiry = default(TimeSpan?))
        {
            if (expiry.HasValue)
                _httpContext.Cache.Add(
                    key,
                    objectToCache,
                    null,
                    DateTime.UtcNow.Add(expiry.Value),
                    TimeSpan.Zero,
                    CacheItemPriority.AboveNormal,
                    null);
            else _httpContext.Cache.Insert(key, objectToCache);
        }

        protected override string GetStringProtected(string key)
        {
            return _httpContext.Cache.Get(key)?.ToString();
        }

        protected override void DeleteProtected(string key)
        {
            _httpContext.Cache.Remove(key);
        }

        protected override void FlushAllProtected()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteByPatternProtected(string key)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}