using System.Web.Mvc;
using Elmah;

namespace NTierMvcFramework.Common.MvcToolkit.Filters
{

    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
        }
    }
}