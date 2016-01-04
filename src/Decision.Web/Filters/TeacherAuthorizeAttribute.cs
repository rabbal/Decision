using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Decision.ServiceLayer.Contracts.TeacherInfo;

namespace Decision.Web.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class TeacherAuthorizeAttribute : ActionFilterAttribute
    {
        public IReferentialTeacherService ReferentialTeacherService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var routeData = filterContext.RouteData;
            var id = Guid.Parse(routeData.Values["TeacherId"].ToString());

            var isAuthorised = ReferentialTeacherService.CanManageTeacher(id);
            if (!isAuthorised)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }
    }
}