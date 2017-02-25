using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Decision.Framework.WebAPIToolkit.DelegatingHandlers
{
    /// <summary>
    ///     todo: config.MessageHandlers.Add(new AntiForgeryHandler());
    /// </summary>
    public class AntiForgeryHandler : DelegatingHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string cookieToken = null;
            string formToken = null;
            if (request.IsAjaxRequest())
            {
                IEnumerable<string> tokenHeaders;
                if (request.Headers.TryGetValues("__RequestVerificationToken", out tokenHeaders))
                {
                    var cookie = request.Headers.GetCookies(AntiForgeryConfig.CookieName).
                        FirstOrDefault();
                    if (cookie != null)
                    {
                        cookieToken = cookie[AntiForgeryConfig.CookieName].Value;
                    }
                    formToken = tokenHeaders.FirstOrDefault();
                }
            }
            try
            {
                if (cookieToken != null && formToken != null)
                {
                    AntiForgery.Validate(cookieToken, formToken);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException)
            {
                return request.CreateResponse(HttpStatusCode.Forbidden);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}