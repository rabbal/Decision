using System;
using System.Web;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Caching
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