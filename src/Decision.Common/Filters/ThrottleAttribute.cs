using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Decision.Common.Caching;
using Decision.Common.Extentions;
using Decision.Utility;

namespace Decision.Common.Filters
{
    public enum TimeUnit
    {
        Minute = 60,
        Hour = 3600,
        Day = 86400
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ThrottleAttribute : ActionFilterAttribute
    {
        public HttpContextBase ContextBase { get; set; }
        public TimeUnit TimeUnit { get; set; }
        public int Count { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var seconds = Convert.ToInt32(TimeUnit);

            var key = string.Join(
                "-",
                seconds,
                filterContext.HttpContext.Request.HttpMethod,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName,
                filterContext.HttpContext.Request.GetIp()
            );


            // increment the cache value
            var cnt = 1;
            var cachedValue = ContextBase.CacheRead(key);
            if (cachedValue != null)
            {
                cnt = (int)cachedValue + 1;
            }

            ContextBase.CacheInsertWithSeconds(key, cnt, seconds);

            if (cnt <= Count) return;
            filterContext.Result = new ContentResult
            {
                Content = "You are allowed to make only " + Count + " requests per " + TimeUnit.ToString().ToLower()
            };
            // see 429 - Rate Limit Exceeded HTTP error
            filterContext.HttpContext.Response.StatusCode = 429;
        }
    }
}
