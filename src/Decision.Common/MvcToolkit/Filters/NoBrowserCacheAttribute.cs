using System;
using System.Web.Mvc;
using NTierMvcFramework.Common.MvcToolkit.Caching;

namespace NTierMvcFramework.Common.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class NoBrowserCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.DisableBrowserCache();
            base.OnResultExecuting(filterContext);
        }
    }
}
