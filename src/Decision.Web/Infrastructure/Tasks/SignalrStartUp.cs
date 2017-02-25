using Microsoft.AspNet.SignalR;
using Decision.Framework.SignalRToolkit;
using Decision.Framework.SignalRToolkit.AntiSpam;
using Decision.Web.Infrastructure.Tasks.Contracts;

namespace Decision.Web.Infrastructure.Tasks
{
    public class SignalrStartUp:IRunStartUp
    {
        public void Execute()
        {
            GlobalHost.HubPipeline.AddModule(new ElmahPipelineModule());
            GlobalHost.HubPipeline.AddModule(new SpamDetectionPiplelineModule());
        }

        public int Order => 1;
    }
}