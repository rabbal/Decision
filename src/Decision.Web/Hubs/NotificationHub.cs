using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Decision.Web.Hubs
{
    public class NotificationHub : BaseHub
    {
        public  void NotifyConversation()
        {
           // Clients.Clients().newConversation("پیغام جدیدی دریافت کردید");
        }
    }
}