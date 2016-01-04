
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decision.Common.Controller;
using Decision.Common.Filters;
using Decision.ServiceLayer.Security;

namespace Decision.Web.Controllers
{
    [RoutePrefix("Admin")]
    [Mvc5Authorize(AssignableToRolePermissions.CanAccessToSystemMaintenance)]
    public partial class ElmahController : Controller
    {
        [Route("Elmah/{type?}")]
        public virtual ActionResult Index(string type)
        {
            return new ElmahResult(type);
        }
    }
}