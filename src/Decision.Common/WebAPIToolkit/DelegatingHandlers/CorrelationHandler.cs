using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Decision.Common.WebAPIToolkit.DelegatingHandlers
{
    public class CorrelationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var reqId = request.GetCorrelationId().ToString();
            if (!Global.Storage.TryAdd(reqId, request)) return await base.SendAsync(request, cancellationToken);
            var result = await base.SendAsync(request, cancellationToken);
            object req;
            Global.Storage.TryRemove(reqId, out req);
            return result;
        }
    }
}