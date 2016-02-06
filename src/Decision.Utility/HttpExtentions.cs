using System;
using System.Linq;
using System.Web;

namespace Decision.Utility
{
    public static class HttpExtentions
    {
        #region PhysicalToVirtualPathConverter
        public static string PhysicalToVirtualPathConverter(this HttpServerUtilityBase utility, string path, HttpRequestBase context)
        {
            return path.Replace(context.ServerVariables["APPL_PHYSICAL_PATH"], "/").Replace(@"\", "/");
        }
        #endregion

        #region Get Ip Address
        public static string GetIp(this HttpRequestBase request)
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
            catch { ip = null; }

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
            catch { ip = null; }

            return ip;

        }
        #endregion

        #region User-Agent

        public static string GetBrowser(this HttpRequest request)
        {
            return $"{ request.Browser.Browser} - { request.Browser.Version}";
        }
        #endregion

    }
}