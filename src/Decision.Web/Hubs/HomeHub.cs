using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Decision.Framework.Filters;
using Microsoft.AspNet.SignalR;

namespace Decision.Web.Hubs
{
    [SignalrAuthorize()]
    public class HomeHub : BaseHub
    {
       
    }
}