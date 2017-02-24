using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Decision.Common.WebAPIToolkit.Controller
{
    public abstract class ApiControllerWithHub<THub> : BaseApiController
        where THub : IHub
    {
        private readonly Lazy<IHubContext> _hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<THub>()
            );

        protected IHubContext Hub => _hub.Value;
    }
}