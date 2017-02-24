using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Decision.Common.SignalRToolkit.Filters;
using Microsoft.AspNet.SignalR;

namespace Decision.Common.SignalRToolkit
{
    
    [SignalRAuthorize]
    public abstract class HubBase : Hub
    {
        protected static readonly ConcurrentDictionary<string, SignalRUser> Users =
            new ConcurrentDictionary<string, SignalRUser>();

        public override Task OnConnected()
        {
            Connect();
            return base.OnConnected();
        }

        private void Connect()
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName,
                _ => new SignalRUser
                {
                    Name = userName,
                    ConnectionIds = new HashSet<string>()
                });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);

                // TODO: Broadcast the connected user
                //Clients.AllExcept(user.ConnectionIds.ToArray()).userConnected(userName);
                //or
                //if (user.ConnectionIds.Count == 1)
                //{
                //    Clients.Others.userConnected(userName);
                //}
            }
        }

        public override Task OnReconnected()
        {
            Connect();
            return base.OnReconnected();
        }

        /// <summary>
        /// use blow method for disconnect manually
        /// window.onbeforeunload = function (e) {
        ///$.connection.hub.stop();};
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            SignalRUser user;
            Users.TryGetValue(userName, out user);
            if (user == null) return base.OnDisconnected(stopCalled);
            lock (user.ConnectionIds)
            {
                user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

                if (user.ConnectionIds.Any()) return base.OnDisconnected(stopCalled);

                SignalRUser removedUser;
                Users.TryRemove(userName, out removedUser);
                //todo
                // You might want to only broadcast this info if this 
                // is the last connection of the user and the user actual is 
                // now disconnected from all connections.
                //Clients.Others.userDisconnected(userName);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}