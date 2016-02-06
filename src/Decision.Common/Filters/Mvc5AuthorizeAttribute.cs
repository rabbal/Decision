using System;
using System.Net;
using System.Web.Mvc;

namespace Decision.Common.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class Mvc5AuthorizeAttribute : AuthorizeAttribute
    {
        #region Ctor
        public Mvc5AuthorizeAttribute(params string[] permissions)
            : base()
        {
            Roles = string.Join(",", permissions);
        }
        #endregion


        #region HandleUnauthorizedRequest
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                // throw new UnauthorizedAccessException(); //to avoid multiple redirects
            }
            else
            {
                HandleAjaxRequest(filterContext);
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
        #endregion

        #region Private
        private static void HandleAjaxRequest(ControllerContext filterContext)
        {
            var ctx = filterContext.HttpContext;
            if (!ctx.Request.IsAjaxRequest())
                return;

            ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden; //براي درخواست‌هاي اجكسي اعتبار سنجي نشده
            ctx.Response.End();
        }
        #endregion
    }

}