using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Decision.Framework.WebAPIToolkit.DelegatingHandlers
{
    /// <summary>
    ///     when display error details
    ///     todo: config.MessageHandlers.Add(new PolicySetterHandler());
    /// </summary>
    public class PolicySetterHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.GetRequestContext().IncludeErrorDetail =
                request.GetRequestContext().Principal.IsInRole("Administrator");
            return base.SendAsync(request, cancellationToken);
        }
    }
}