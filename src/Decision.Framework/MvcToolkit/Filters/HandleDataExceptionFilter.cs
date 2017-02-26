using System;
using System.Data;
using System.Web.Mvc;
using Decision.Framework.GuardToolkit;

namespace Decision.Framework.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public sealed class HandleDataExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Check.ArgumentNotNull(filterContext, nameof(filterContext));

            if (filterContext.ExceptionHandled) return;

            var dataException = filterContext.Exception as DataException;
            if (dataException == null) return;

            filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty,
                Resources.UnableToSaveChanges);
            //todo:log

            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult { ViewData = filterContext.Controller.ViewData };

        }
    }
}