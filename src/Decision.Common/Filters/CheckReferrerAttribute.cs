using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Decision.Common.Filters
{
    public class CheckReferrerAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext != null)
            {
                if (filterContext.HttpContext.Request.UrlReferrer == null)
                    throw new System.Web.HttpException("Invalid submission");
               
                if (filterContext.HttpContext.Request.UrlReferrer.Host != "site.com")
                    throw new System.Web.HttpException("This form wasn't submitted from this site!");
            }

            base.OnAuthorization(filterContext);
        }
    }
}
