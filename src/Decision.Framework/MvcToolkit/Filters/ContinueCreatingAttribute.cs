using System;
using System.Web.Mvc;
using Decision.Framework.Infrastructure;

namespace Decision.Framework.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ContinueCreatingAttribute : FilterAttribute, IActionFilter
    {
        private readonly string _elementName;

        public ContinueCreatingAttribute(string elementName)
        {
            _elementName = elementName;
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var formValue = filterContext.RequestContext.HttpContext.Request.Form[_elementName];
            var viewModel = filterContext.ActionParameters["viewModel"] as IContinuableForCreating;

            if (viewModel != null)
                viewModel.ContinueCreating = !string.IsNullOrEmpty(formValue);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}