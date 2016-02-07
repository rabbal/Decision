using System;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Decision.Common.Caching;
using Decision.Common.Extentions;
using Decision.Utility;

namespace Decision.Common.Filters
{
    /// <summary>
    /// Decorates any MVC route that needs to have client requests limited by time.
    /// </summary>
    /// <remarks>
    /// Uses the current System.Web.Caching.Cache to store each client request to the decorated route.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DosFilterAttribute : ActionFilterAttribute
    {
        public HttpContextBase ContextBase { get; set; }


        /// <summary>
        /// A unique name for this Throttle.
        /// </summary>
        /// <remarks>
        /// We'll be inserting a Cache record based on this name and client IP, e.g. "Name-192.168.0.1"
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// The number of seconds clients must wait before executing this decorated route again.
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// A text message that will be sent to the client upon throttling.  You can include the token {n} to
        /// show this.Seconds in the message, e.g. "Wait {n} seconds before trying again".
        /// </summary>
        public string Message { get; set; }
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var key = string.Concat(Name, "-", actionContext.HttpContext.Request.GetIp());
            var allowExecute = false;

            if (ContextBase.CacheRead(key) == null)
            {
                ContextBase.CacheInsertWithSeconds(key, true, Seconds);
                allowExecute = true;
            }

            if (allowExecute) return;

            if (!Message.HasValue())
                Message = "You may only perform this action every {n} seconds.";

            actionContext.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString(CultureInfo.InvariantCulture)) };
            // see 429 - Rate Limit Exceeded HTTP error
            actionContext.HttpContext.Response.StatusCode = 429;
        }

    }
}
