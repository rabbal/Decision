using System;
using System.Web.Mvc;
using Decision.Framework.MvcToolkit.Caching;

namespace Decision.Framework.MvcToolkit.Filters
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
