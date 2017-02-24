using System;
using System.Net;
using System.Web.Mvc;
using NTierMvcFramework.Common.Extensions;

namespace NTierMvcFramework.Common.MvcToolkit.Maintenance
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OfflineAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ipAddress = filterContext.HttpContext.Request.GetIp();

            var offlineHelper = new OfflineHelper(ipAddress,
                 filterContext.HttpContext.Server.MapPath);
            if (offlineHelper.ThisUserShouldBeOffline)
            {
                if (filterContext.IsChildAction)
                {
                    filterContext.Result = new ContentResult { Content = string.Empty };
                    return;
                }

                filterContext.Result = new ViewResult
                {
                    ViewName = "Offline"
                };

                var response = filterContext.HttpContext.Response;
                response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                response.TrySkipIisCustomErrors = true;

                return;
            }

            //otherwise we let this through as normal
            base.OnActionExecuting(filterContext);
        }
    }
}