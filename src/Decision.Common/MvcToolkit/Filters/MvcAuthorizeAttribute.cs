using System;
using System.Net;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class MvcAuthorizeAttribute : AuthorizeAttribute
    {
        #region Ctor
        public MvcAuthorizeAttribute(params string[] permissions)
        {
            Roles = string.Join(",", permissions);
        }
        #endregion

        #region Protected Methods
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                throw new UnauthorizedAccessException(); //to avoid multiple redirects
            }

            HandleAjaxRequest(filterContext);
            base.HandleUnauthorizedRequest(filterContext);
        }
        #endregion

        #region Private Methods
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