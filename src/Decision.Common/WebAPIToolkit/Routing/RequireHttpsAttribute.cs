using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace NTierMvcFramework.Common.WebAPIToolkit.Routing
{
    public class RequireHttpsAttribute : IAuthenticationFilter
    {
        public bool AllowMultiple => true;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken
            cancellationToken)
        {
            if (context.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                context.ActionContext.Response = new HttpResponseMessage(HttpStatusCode.
                    Forbidden)
                {
                    ReasonPhrase = "HTTPS Required"
                };
            }
            return Task.FromResult(true);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken
            cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}