using System.Web;
using System.Web.Mvc;
using Elmah;

namespace Decision.Framework.MvcToolkit.Filters
{
    public sealed class ElmahRequestValidationErrorFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is HttpRequestValidationException)
            {
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
            }
        }
    }
}
