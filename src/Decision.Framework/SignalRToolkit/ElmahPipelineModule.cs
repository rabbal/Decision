using System;
using System.Reflection;
using Elmah;
using Microsoft.AspNet.SignalR.Hubs;

namespace Decision.Framework.SignalRToolkit
{
    public class ElmahPipelineModule : HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext exContext, IHubIncomingInvokerContext context)
        {
            var exception = exContext.Error;

            if (exception is TargetInvocationException)
            {
                exception = exception.InnerException;
            }
            else if (exception is AggregateException)
            {
                exception = exception.InnerException;
            }

            ErrorSignal.FromCurrentContext().Raise(exception);
        }
    }
}