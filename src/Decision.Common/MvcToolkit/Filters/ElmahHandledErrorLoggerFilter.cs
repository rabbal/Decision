using System.Web.Mvc;
using Elmah;

namespace Decision.Common.MvcToolkit.Filters
{

    public sealed class ElmahHandledErrorLoggerFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
        }
    }
}