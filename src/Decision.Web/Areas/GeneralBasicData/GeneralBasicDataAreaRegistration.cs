using System.Web.Mvc;

namespace Decision.Web.Areas.GeneralBasicData
{
    public class GeneralBasicDataAreaRegistration : AreaRegistration 
    {
        public override string AreaName => nameof(GeneralBasicData);

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GeneralBasicData_default",
                "GeneralBasicData/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}