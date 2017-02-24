using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using NTierMvcFramework.Common.SignalRToolkit.Filters;

namespace NTierMvcFramework.Common.SignalRToolkit
{
    
    [SignalRAuthorize]
    public abstract class BaseHub : Hub
    {
        protected static readonly ConcurrentDictionary<string, User> Users =
            new ConcurrentDictionary<string, User>();

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
                _ => new User
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

            User user;
            Users.TryGetValue(userName, out user);
            if (user == null) return base.OnDisconnected(stopCalled);
            lock (user.ConnectionIds)
            {
                user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

                if (user.ConnectionIds.Any()) return base.OnDisconnected(stopCalled);

                User removedUser;
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