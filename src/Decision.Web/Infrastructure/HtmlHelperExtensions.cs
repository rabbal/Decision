using System.Web.Mvc;
using Decision.Models.Web;


namespace Decision.Web.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        public static string GetSiteShortName(this HtmlHelper helper)
        {
            return helper.ViewBag.SiteShortName;
        }
        public static string GetSiteName(this HtmlHelper helper)
        {
            return helper.ViewBag.SiteName;
        }

        public static AuthenticationViewModel GetAuthentication(this HtmlHelper helper)
        {
            return (AuthenticationViewModel)helper.ViewBag.Authentication;
        }
    }
}