using System;
using System.Web.Mvc;
using Decision.Common.Infrastructure;

namespace Decision.Common.MvcToolkit.Filters
{

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ContinueEditingAttribute : FilterAttribute, IActionFilter
    {
        private readonly string _elementName;

        public ContinueEditingAttribute(string elementName)
        {
            _elementName = elementName;
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var formValue = filterContext.RequestContext.HttpContext.Request.Form[_elementName];
            var viewModel = filterContext.ActionParameters["viewModel"] as IContinuableForEditing;

            if (viewModel != null)
                viewModel.ContinueEditing = !string.IsNullOrEmpty(formValue);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}