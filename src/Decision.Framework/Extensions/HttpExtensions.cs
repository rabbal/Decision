using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using Decision.Framework.KendoLinq;
using Newtonsoft.Json;

namespace Decision.Framework.Extensions
{
    public static class HttpExtensions
    {
        private const string HttpContext = "MS_HttpContext";

        public static T ToListRequest<T>(this HttpRequestBase requestBase) where T : BaseListRequest
        {

            var queryString = requestBase.QueryString[0];

            return JsonConvert.DeserializeObject<T>(queryString);
        }

        public static Uri GetUrlReferrer(this HttpRequestMessage request)
        {
            if (!request.Properties.ContainsKey(HttpContext)) return null;
            var ctx = request.Properties[HttpContext] as HttpContextWrapper;
            return ctx?.Request.UrlReferrer;
        }

        #region PhysicalToVirtualPathConverter

        public static string PhysicalToVirtualPathConverter(this HttpServerUtilityBase utility, string path,
            HttpRequestBase context)
        {
            return path.Replace(context.ServerVariables["APPL_PHYSICAL_PATH"], "/").Replace(@"\", "/");
        }

        public static string AbsoluteContent(HttpRequest request,
          string contentPath)
        {
            return new Uri(request.Url, VirtualPathUtility.ToAbsolute(contentPath)).ToString();
        }

        #endregion

        #region Get Ip Address

        public static string GetUserIp(this HttpRequestBase request)
        {
            string ip = null;
            try
            {
                if (request.IsSecureConnection)
                {
                    ip = request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.IsNullOrEmpty(ip))
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ip))
                    {
                        if (ip.IndexOf(",", StringComparison.Ordinal) > 0)
                        {
                            ip = ip.Split(',').Last();
                        }
                    }
                    else
                    {
                        ip = request.UserHostAddress;
                    }
                }
            }
            catch (Exception)
            {
                ip = null;
            }

            return ip;
        }

        public static string GetIp(this HttpRequest request)
        {
            string ip = null;
            try
            {
                if (request.IsSecureConnection)
                {
                    ip = request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.IsNullOrEmpty(ip))
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ip))
                    {
                        if (ip.IndexOf(",", StringComparison.Ordinal) > 0)
                        {
                            ip = ip.Split(',').Last();
                        }
                    }
                    else
                    {
                        ip = request.UserHostAddress;
                    }
                }
            }
            catch (Exception)
            {
                ip = null;
            }

            return ip;
        }

        #endregion

        #region User-Agent

        public static string GetUserAgent(this HttpRequestBase request)
        {
            return $"{request.Browser.Browser} - {request.Browser.Version}";
        }

        public static string GetBrowser(this HttpRequest request)
        {
            return $"{request.Browser.Browser} - {request.Browser.Version}";
        }

        #endregion
    }
}