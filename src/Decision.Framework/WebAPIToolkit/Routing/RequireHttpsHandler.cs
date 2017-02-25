using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Decision.Framework.WebAPIToolkit.Routing
{
    public class RequireHttpsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.RequestUri.Scheme == Uri.UriSchemeHttps) return base.SendAsync(request, cancellationToken);
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                ReasonPhrase = "HTTPS Required"
            };

            return Task.FromResult(response);
        }
    }
}