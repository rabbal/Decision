using System.Web.Mvc;

namespace Decision.Framework.MvcToolkit.Filters
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
