using System;
using System.Net.Http;
using System.Web;
using Elmah;

namespace Decision.Framework.WebAPIToolkit.ExceptionHandling
{
    public class ElmahExceptionLogger : ExceptionLogger
    {
        private const string HttpContextBaseKey = "MS_HttpContext";

        public override void Log(ExceptionLoggerContext context)
        {
            var httpContext = GetHttpContext(context.Request);

            if (httpContext == null)
            {
                return;
            }

            Exception exceptionToRaise = new HttpUnhandledException(message: null, innerException: context.Exception);

            var signal = ErrorSignal.FromContext(httpContext);
            signal.Raise(exceptionToRaise, httpContext);
        }

        private static HttpContext GetHttpContext(HttpRequestMessage request)
        {
            var contextBase = GetHttpContextBase(request);

            return contextBase == null ? null : ToHttpContext(contextBase);
        }

        private static HttpContextBase GetHttpContextBase(HttpRequestMessage request)
        {
            if (request == null)
            {
                return null;
            }

            object value;

            if (!request.Properties.TryGetValue(HttpContextBaseKey, out value))
            {
                return null;
            }

            return value as HttpContextBase;
        }

        private static HttpContext ToHttpContext(HttpContextBase contextBase)
        {
            return contextBase.ApplicationInstance.Context;
        }
    }
}