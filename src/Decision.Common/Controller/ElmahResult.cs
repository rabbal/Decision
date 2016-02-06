
using System.Web;
using System.Web.Mvc;

namespace Decision.Common.Controller
{
    public class ElmahResult : ActionResult
    {
        private readonly string _resouceType;

        public ElmahResult(string resouceType)
        {
            _resouceType = resouceType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var factory = new Elmah.ErrorLogPageFactory();

            if (!string.IsNullOrEmpty(_resouceType))
            {
                var pathInfo = "/" + _resouceType;
                context.HttpContext.RewritePath(FilePath(context), pathInfo,
                    context.HttpContext.Request.QueryString.ToString());
            }

            var currentApplication = (HttpApplication) context.HttpContext.GetService(typeof (HttpApplication));
            if (currentApplication == null) return;
            var currentContext = currentApplication.Context;

            var httpHandler = factory.GetHandler(currentContext, null, null, null);
            var handler = httpHandler as IHttpAsyncHandler;
            if (handler != null)
            {
                var asyncHttpHandler = handler;
                asyncHttpHandler.BeginProcessRequest(currentContext, r => { }, null);
            }
            else
            {
                httpHandler?.ProcessRequest(currentContext);
            }
        }

        private string FilePath(ControllerContext context)
        {
            return _resouceType != "stylesheet"
                ? context.HttpContext.Request.Path.Replace($"/{_resouceType}", string.Empty)
                : context.HttpContext.Request.Path;
        }

    }
}
