using System.Web.Mvc;
using System.Web.Routing;

namespace Decision.Web
{
    public static class RoutingConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            #region IgnoreRoutes
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.ico");
            routes.IgnoreRoute("{resource}.png");
            routes.IgnoreRoute("{resource}.jpg");
            routes.IgnoreRoute("{resource}.gif");
            routes.IgnoreRoute("{resource}.txt");
            #endregion

            routes.LowercaseUrls = true;
            routes.MapMvcAttributeRoutes();
           // AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults:
                    new
                    {
                        controller = MVC.Home.Name,
                        action = MVC.Home.ActionNames.Index,
                        id = UrlParameter.Optional
                    },
                namespaces: new[] {$"{typeof (RoutingConfig).Namespace}.Controllers"}
                );
        }

    }
}
