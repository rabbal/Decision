using System.Web;
using System.Web.Mvc;
using Elmah;

namespace NTierMvcFramework.Common.MvcToolkit.Filters
{
    public class ElmahRequestValidationErrorFilter : IExceptionFilter
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
