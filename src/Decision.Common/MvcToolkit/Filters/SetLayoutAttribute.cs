using System;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public sealed class SetLayoutAttribute : ActionFilterAttribute
    {
        private readonly string _masterName;
        public SetLayoutAttribute(string masterName)
        {
            _masterName = masterName;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = _masterName;
            }
        }
    }
}