using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elmah;

namespace Decision.Common.Filters
{
    public class ElmahRequestValidationErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is HttpRequestValidationException)
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(context.Exception));
        }
    }
}
