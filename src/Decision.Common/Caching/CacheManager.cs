using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Decision.Common.Caching
{
    public static class CacheManager
    {
        public static void DisableBrowserCache(this HttpContextBase httpContext)
        {
            httpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            httpContext.Response.Cache.SetValidUntilExpires(false);
            httpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            httpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            httpContext.Response.Cache.SetNoStore();
        }

        public static void CacheInsert(this HttpContextBase httpContext, string key, object data, int durationMinutes)
        {
            if (data == null) return;
            httpContext.Cache.Add(
                key,
                data,
                null,
                DateTime.UtcNow.AddMinutes(durationMinutes),
                TimeSpan.Zero,
                CacheItemPriority.AboveNormal,
                null);
        }
        public static void CacheInsertWithSeconds(this HttpContextBase httpContext, string key, object data, int seconds)
        {
            if (data == null) return;

            httpContext.Cache.Add(
                key,
                data,
                null,
                DateTime.UtcNow.AddMinutes(seconds),
                Cache.NoSlidingExpiration,
                CacheItemPriority.Low,
                null);
        }
        public static T CacheRead<T>(this HttpContextBase httpContext, string key)
        {
            var data = httpContext.Cache[key];
            if (data != null)
                return (T)data;
            return default(T);
        }
        public static object CacheRead(this HttpContextBase httpContext, string key)
        {
            var data = httpContext.Cache[key];
            return data;
        }
        public static void InvalidateCache(this HttpContextBase httpContext, string key)
        {
            httpContext.Cache.Remove(key);
        }
        public static void InvalidateOutPutCache(string url)
        {
            HttpResponse.RemoveOutputCacheItem(url);
        }

        public static void InvalidateChildActionsCache()
        {
            OutputCacheAttribute.ChildActionCache = new System.Runtime.Caching.MemoryCache(Guid.NewGuid().ToString());
        }
    }
}