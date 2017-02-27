using System.Web.Mvc;
using Decision.Framework.Filters;
using Decision.ServiceLayer.Security;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [MvcAuthorize(AssignableToRolePermissions.CanManageTrainingCenter,
        AssignableToRolePermissions.CanManageTitle,
        AssignableToRolePermissions.CanManageAppraiser,
        AssignableToRolePermissions.CanManageInstitution,
        AssignableToRolePermissions.CanManageQuestion
        )]
    [RoutePrefix("BaseSetting")]
    [Route("{action}")]
    public partial class BaseSettingController : Controller
    {
        [MvcSiteMapNode(ParentKey = "Home_Index", Title = "تنظیمات پایه",Key = "Base_Index")]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}