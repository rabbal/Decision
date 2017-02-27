
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decision.Framework.Controller;
using Decision.Framework.Filters;
using Decision.ServiceLayer.Security;

namespace Decision.Web.Controllers
{
    [RoutePrefix("Admin")]
    [MvcAuthorize(AssignableToRolePermissions.CanAccessToSystemMaintenance)]
    public partial class ElmahController : Controller
    {
        [Route("Elmah/{type?}")]
        public virtual ActionResult Index(string type)
        {
            return new ElmahResult(type);
        }
    }
}