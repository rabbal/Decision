using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NTierMvcFramework.Common.WebAPIToolkit.DelegatingHandlers
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