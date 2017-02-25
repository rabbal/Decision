using System;
using System.Net;

namespace Decision.Framework.WebAPIToolkit
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    actionContext.ModelState);
            }
        }

        public override bool AllowMultiple => false;
        
    }
}