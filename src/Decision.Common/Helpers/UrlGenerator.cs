using System.Web;
using System.Web.Mvc;

namespace Decision.Common.Helpers
{
    public static class UrlGenerator
    {
        public static MvcHtmlString ReturnUrl(this HtmlHelper htmlHelper, HttpContextBase contextBase,
             UrlHelper urlHelper)
        {
            var currentUrl = contextBase.Request.RawUrl;
            if (currentUrl == "/")
            {
                currentUrl = urlHelper.Action("Index", "Home");
            }
            return MvcHtmlString.Create(currentUrl);
        }
    }
}