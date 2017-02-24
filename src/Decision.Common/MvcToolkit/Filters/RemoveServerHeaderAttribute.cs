using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Filters
{
    
    public sealed class RemoveServerHeaderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            // for prevent attack
            response.Headers.Remove("Server");
            base.OnActionExecuting(filterContext);
        }
    }
}
