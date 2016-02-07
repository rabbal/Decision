using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.Users;

namespace Decision.Web.Filters
{
    /// <summary>
    /// به منظور اعمال 
    /// AOP 
    /// برای لاگ فعالیت های کاربران
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ActivityAttribute : ActionFilterAttribute
    {
        #region Properties
        public Type LoggableType { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
        public AuditLogType LogType { get; set; }
        public IActivityLogService ActivityLogService { get; set; }
        #endregion
        
        #region OnActionExecuting
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (LogType == AuditLogType.JustDescription)
            //{
            //    var propertyValue = string.Empty;
            //    if (Property.HasValue())
            //    {
            //        var parameters = filterContext.ActionDescriptor.GetParameters();
            //        var parameterToAudit = parameters.SingleOrDefault(p => p.ParameterType == LoggableType);
            //        if (parameterToAudit != null)
            //        {
            //            var argumentToAudit = filterContext.ActionParameters[parameterToAudit.ParameterName];
            //            var propertyInfo = parameterToAudit.ParameterType.GetProperties()
            //                .First(p => Property == p.Name);

            //            var pi = argumentToAudit.GetType().GetProperty(propertyInfo.Name);
            //            propertyValue = pi.GetValue(argumentToAudit, null).ToString();
            //        }
            //    }

            //    ActivityLogService.Create(Description, propertyValue);

            //}

            //base.OnActionExecuting(filterContext);
        }

        #endregion

        #region OnActionExecuted
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if (LogType == AuditLogType.Login)
            //{
            //    ActivityLogService.Create(Description, LogType);
            //}
            //if (LogType != AuditLogType.Serialize) return;
            //if (filterContext.Controller.ViewData.ModelState.IsValid)
            //{
            //    ActivityLogService.Create(Description, LogType);
            //}

            //base.OnActionExecuted(filterContext);
        }

        #endregion

    }
}
