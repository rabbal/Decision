using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Decision.Common.Filters
{
    public class Mvc5ClaimAccessAuthorizeAttribute : AuthorizeAttribute
    {
        #region Properties
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }
        #endregion

        #region AuthorizeCore
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            return context.User.Identity.IsAuthenticated
            && context.User.Identity is ClaimsIdentity
            && ((ClaimsIdentity)context.User.Identity).HasClaim(x =>
            x.Issuer == Issuer && x.Type == ClaimType && x.Value == Value
            );
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

