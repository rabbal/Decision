using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Decision.Common.Filters;
using Decision.DomainClasses.Entities.Users;
using Decision.ServiceLayer.Contracts.Users;
using Microsoft.AspNet.SignalR;

namespace Decision.Web.Hubs
{
    public abstract class BaseHub : Hub
    {
        private static readonly ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();

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
                    UserName = userName,
                    ConnectionIds = new HashSet<string>()
                });
            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
            }
        }

        public override Task OnReconnected()
        {
            Connect();
            return base.OnReconnected();
        }

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
            }

            return base.OnDisconnected(stopCalled);
        }

       

    }
}