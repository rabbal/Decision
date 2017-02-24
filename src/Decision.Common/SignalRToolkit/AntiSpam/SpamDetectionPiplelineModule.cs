using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;

namespace Decision.Common.SignalRToolkit.AntiSpam
{
    public class SpamDetectionPiplelineModule : HubPipelineModule
    {
        /// <summary>
        /// for chatHub methods
        /// </summary>
        private static readonly HashSet<string> MethodsListForDetect = new HashSet<string>
        {
            "SendMessage",
            //todo: any chat methods names
        };

        private static readonly HashSet<ActivityInfo> SpamDetection =
            new HashSet<ActivityInfo>();

        private readonly object _spamDetectionLock = new object();

        public bool IsSpam(IHubIncomingInvokerContext context)
        {
            lock (_spamDetectionLock)
            {
                var methodName = context.MethodDescriptor.Name;
                if (!MethodsListForDetect.Contains(methodName))
                    return false;

                var connectionId = context.Hub.Context.ConnectionId;
                
                SpamDetection.RemoveWhere(q => q.Time < DateTime.Now.AddSeconds(-3));

                SpamDetection.Add(new ActivityInfo(connectionId));
                
                return SpamDetection.Count(q => q.ConnectionId == connectionId) > 3;
            }
        }

        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            return !IsSpam(context) && base.OnBeforeIncoming(context);
        }
    }
}