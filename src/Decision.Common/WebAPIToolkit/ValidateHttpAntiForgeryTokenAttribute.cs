using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.WebAPIToolkit
{
    public sealed class ValidateHttpAntiForgeryTokenAttribute : AuthorizationFilterAttribute
    {
        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.ControllerContext.Request;
            try
            {
                if (IsAjaxRequest(request))
                {
                    ValidateRequestHeader(request);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException e)
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, e);
            }
        }

        private static bool IsAjaxRequest(HttpRequestMessage request)
        {
            IEnumerable<string> xRequestedWithHeaders;
            if (!request.Headers.TryGetValues("X-Requested-With", out xRequestedWithHeaders)) return false;
            var headerValue = xRequestedWithHeaders.FirstOrDefault();
            return !string.IsNullOrEmpty(headerValue) &&
                   string.Equals(headerValue, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
        }

        private static void ValidateRequestHeader(HttpRequestMessage request)
        {
            var cookieToken = string.Empty;
            var formToken = string.Empty;
            IEnumerable<string> tokenHeaders;
            if (request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
            {
                var tokenValue = tokenHeaders.FirstOrDefault();
                if (!string.IsNullOrEmpty(tokenValue))
                {
                    var tokens = tokenValue.Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }
            }
            AntiForgery.Validate(cookieToken, formToken);
        }
    }
}