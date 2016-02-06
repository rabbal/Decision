using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Decision.Common.Extentions
{
    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";

        public static Uri GetUrlReferrer(this HttpRequestMessage request)
        {
            if (!request.Properties.ContainsKey(HttpContext)) return null;
            var ctx = request.Properties[HttpContext] as HttpContextWrapper;
            return ctx?.Request.UrlReferrer;
        }
    }
}
