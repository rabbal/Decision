//using System.Web.Mvc;

//namespace Decision.Common.Filters
//{
//    public class SecurityHeadersConstants
//    {
//        public static readonly string XXssProtectionHeader = "X-XSS-Protection";
//        public static readonly string XFrameOptionsHeader = "X-Frame-Options";
//        public static readonly string XWebKitCspHeader = "X-WebKit-CSP";
//        public static readonly string XContentSecurityPolicyHeader = "X-Content-Security-Policy";
//        public static readonly string ContentSecurityPolicyHeader = "Content-Security-Policy";
//        public static readonly string XContentTypeOptionsHeader = "X-Content-Type-Options";
//    }
//    public class ContentSecurityPolicyFilterAttribute : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {


//            //response.Headers.Set(SecurityHeadersConstants.XFrameOptionsHeader, "SameOrigin");

//            //// For IE 8+
//            //response.Headers.Set(SecurityHeadersConstants.XXssProtectionHeader, "1; mode=block");
//            //response.Headers.Set(SecurityHeadersConstants.XContentTypeOptionsHeader, "nosniff");

//            ////todo: Add /Home/Report --> public JsonResult Report() { return Json(true); }

//            //const string cspValue = "default-src 'self';";
//            //// For Chrome 16+
//            //response.Headers.Set(SecurityHeadersConstants.XWebKitCspHeader, cspValue);

//            //// For Firefox 4+
//            //response.Headers.Set(SecurityHeadersConstants.XContentSecurityPolicyHeader, cspValue);
//            //response.Headers.Set(SecurityHeadersConstants.ContentSecurityPolicyHeader, cspValue);
//            base.OnActionExecuting(filterContext);
//        }
//    }
//}