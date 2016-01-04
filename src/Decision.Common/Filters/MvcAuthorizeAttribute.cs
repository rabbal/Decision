//using System;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using Microsoft.Owin.Security;
//using Decision.ServiceLayer.Contracts;

//namespace Web.Filters
//{
//    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
//        Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
//    public class MvcAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
//    {

//        #region Properties

//        public IApplicationUserManager ApplicationUserManager { get; set; }
//        public IAuthenticationManager AuthenticationManager { get; set; }
//        public string DependencyActionNames { get; set; }
//        public string AreaName { get; set; }
//        public bool IsMenu { get; set; }
//        #endregion

//        #region OnAuthorization
//        public virtual void OnAuthorization(AuthorizationContext filterContext)
//        {
//            if (filterContext == null)
//            {
//                throw new ArgumentNullException("filterContext");
//            }

//            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
//            {
//                // If a child action cache block is active, we need to fail immediately, even if authorization
//                // would have succeeded. The reason is that there's no way to hook a callback to rerun
//                // authorization before the fragment is served from the cache, so we can't guarantee that this
//                // filter will be re-run on subsequent requests.
//                throw new InvalidOperationException("AuthorizeAttribute_CannotUseWithinChildActionCache");
//            }

//            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute),
//                inherit: true)
//                                    ||
//                                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
//                                        typeof(AllowAnonymousAttribute), inherit: true);

//            if (skipAuthorization)
//            {
//                return;
//            }
//            if (AuthorizeCore(filterContext))
//            {
//                var cachePolicy = filterContext.HttpContext.Response.Cache;
//                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
//                cachePolicy.AddValidationCallback(CacheValidateHandler, filterContext /* data */);
//            }
//            else
//            {
//                HandleUnauthorizedRequest(filterContext);
//            }
//        }

//        #endregion

//        #region AuthorizeCore
//        protected virtual bool AuthorizeCore(AuthorizationContext filterContext)
//        {
//            var user = filterContext.HttpContext.User;
//            if (!user.Identity.IsAuthenticated)
//            {
//                return false;
//            }

//            var userId = user.Identity.GetUserId<int>();

//            var actionName = filterContext.ActionDescriptor.ActionName;
//            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
//            var areaName = AreaName;

//            return ApplicationUserManager.CanAccess(userId, areaName, controllerName, actionName,
//                DependencyActionNames);
//        }

//        #endregion

//        #region OnCacheAuthorization
//        // This method must be thread-safe since it is called by the caching module.
//        private HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext, AuthorizationContext filterContext)
//        {
//            if (httpContext == null)
//            {
//                throw new ArgumentNullException("httpContext");
//            }

//            var isAuthorized = AuthorizeCore(filterContext);
//            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
//        }
//        #endregion

//        #region CacheValidateHandler

//        #endregion

//        #region CacheValidateHandler
//        // ReSharper disable once RedundantAssignment
//        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
//        {
//            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context), (AuthorizationContext)data);
//        }
//        #endregion

//        #region HandleUnauthorizedRequest
//        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext)
//        {
//            if (filterContext.HttpContext.Request.IsAuthenticated)
//            {
//                throw new UnauthorizedAccessException(); //to avoid multiple redirects
//            }
//            else
//            {
//                // this line have issue
//                HandleAjaxRequest(filterContext);
//                filterContext.Result = new HttpUnauthorizedResult();
//            }

//        }
//        #endregion

//        #region HandleAjaxRequest
//        private static void HandleAjaxRequest(ControllerContext filterContext)
//        {
//            var ctx = filterContext.HttpContext;
//            if (!ctx.Request.IsAjaxRequest())
//                return;

//            ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden; //براي درخواست‌هاي اجكسي اعتبار سنجي نشده
//            ctx.Response.End();
//        }
//        #endregion
//    }
//}