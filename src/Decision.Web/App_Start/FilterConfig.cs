using System.Web.Mvc;
using Decision.Common.MvcToolkit.Filters;

namespace Decision.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            // logg action errors
            filters.Add(new ElmahHandledErrorLoggerFilter());
            //  logg xss attacks
            filters.Add(new ElmahRequestValidationErrorFilter());

            //  filters.Add(new RemoveServerHeaderFilterAttribute());

        }
    }
}
