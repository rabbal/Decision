using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Decision.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.AppendTrailingSlash = false;
            routes.LowercaseUrls = true;

            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("Client/{*pathInfo}");
            routes.IgnoreRoute("Error/Forbidden.html");
            routes.IgnoreRoute("Error/GatewayTimeout.html");
            routes.IgnoreRoute("Error/ServiceUnavailable.html");
            routes.IgnoreRoute("{file}.htm");
            routes.IgnoreRoute("{file}.html");
            routes.IgnoreRoute("humans.txt");
            routes.IgnoreRoute("{resource}.png");
            routes.IgnoreRoute("{resource}.jpg");
            routes.IgnoreRoute("{resource}.gif");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

            routes.MapMvcAttributeRoutes();

            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { $"{typeof(RouteConfig).Namespace}.Controllers" }
                );
        }
    }
}

