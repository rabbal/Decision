using System;
using System.Web.Mvc;
using Decision.Common.Configuration;

namespace Decision.Common.MvcToolkit.Filters
{
    /// <summary>
    /// http://www.dotnettips.info/post/821
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CheckReferrerAttribute : AuthorizeAttribute
    {
        public IAppConfiguration AppConfiguration { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext != null)
            {
                if (filterContext.HttpContext.Request.UrlReferrer == null)
                    throw new System.Web.HttpException("Invalid submission");

                if (filterContext.HttpContext.Request.UrlReferrer.Host != AppConfiguration.HostName)
                    throw new System.Web.HttpException("This form wasn't submitted from this site!");
            }

            base.OnAuthorization(filterContext);
        }
    }
}
