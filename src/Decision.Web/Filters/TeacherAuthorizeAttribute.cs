using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Decision.ServiceLayer.Contracts.ApplicantInfo;

namespace Decision.Web.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class ApplicantAuthorizeAttribute : ActionFilterAttribute
    {
        public IReferentialApplicantService ReferentialApplicantService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var routeData = filterContext.RouteData;
            var id = Guid.Parse(routeData.Values["ApplicantId"].ToString());

            var isAuthorised = ReferentialApplicantService.CanManageApplicant(id);
            if (!isAuthorised)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }
    }
}