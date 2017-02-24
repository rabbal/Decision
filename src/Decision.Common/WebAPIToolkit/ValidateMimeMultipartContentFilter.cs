using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace NTierMvcFramework.Common.WebAPIToolkit
{
    public sealed class ValidateMimeMultipartContentFilter : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(actionContext.Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    "This request is not properly formatted"));
            }
        }
    }
}