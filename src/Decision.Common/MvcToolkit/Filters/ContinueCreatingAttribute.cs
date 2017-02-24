using System;
using System.Web.Mvc;
using NTierMvcFramework.Common.Infrastructure;

namespace NTierMvcFramework.Common.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ContinueCreatingAttribute : FilterAttribute, IActionFilter
    {
        public readonly string _elementName;

        public ContinueCreatingAttribute(string elementName)
        {
            _elementName = elementName;
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var formValue = filterContext.RequestContext.HttpContext.Request.Form[_elementName];
            var viewModel = filterContext.ActionParameters["viewModel"] as IContinuableForCreating;

            if (viewModel != null)
                ((IContinuableForCreating)viewModel).ContinueCreating = !string.IsNullOrEmpty(formValue);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}