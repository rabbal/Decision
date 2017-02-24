using System;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ExportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
                {
                    filterContext.Controller.TempData[Key] = filterContext.Controller.ViewData.ModelState;
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}