using System.Web.Mvc;

namespace Decision.Framework.MvcToolkit.Helpers
{
    public class DashboardHtmlHelper
    {
        private readonly HtmlHelper _htmlHelper;

        public DashboardHtmlHelper(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }
    }

}