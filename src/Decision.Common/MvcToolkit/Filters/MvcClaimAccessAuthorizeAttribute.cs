using System;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class MvcClaimAccessAuthorizeAttribute : AuthorizeAttribute
    {
        #region Properties
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }
        #endregion

        #region Protected Methods
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            return context.User.Identity.IsAuthenticated
            && context.User.Identity is ClaimsIdentity
            && ((ClaimsIdentity)context.User.Identity).HasClaim(x =>
            x.Issuer == Issuer && x.Type == ClaimType && x.Value == Value
            );
        }
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

