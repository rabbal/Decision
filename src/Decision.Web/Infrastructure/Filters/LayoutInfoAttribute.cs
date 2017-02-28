using System;
using System.Web.Mvc;
using Decision.Framework.Configuration;
using Decision.ServiceLayer.Interfaces.Identity;

namespace Decision.Web.Infrastructure.Filters
{
    public sealed class LayoutInfoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction || filterContext.HttpContext.Request.IsAjaxRequest()) return;


            base.OnActionExecuted(filterContext);
        }

    }


}