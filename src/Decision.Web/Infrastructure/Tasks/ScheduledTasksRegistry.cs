using System;
using System.Diagnostics;
using System.Net;
using DNTScheduler;
using Decision.Common.Configuration;
using Decision.Common.Logging;
using Decision.Web.Infrastructure.Tasks.Contracts;
using Decision.Web.Infrastructure.WebTasks;

namespace Decision.Web.Infrastructure.Tasks
{
    public class ScheduledTasksRegistry : IBootstrapperTask, IRunOnEnd
    {
        private readonly ILogger _logger;
        private readonly IAppConfiguration _configuration;

        public ScheduledTasksRegistry(ILogger logger, IAppConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        int IRunOnEnd.Order => int.MinValue;

        int IBootstrapperTask.Order => int.MinValue;

        void IBootstrapperTask.Execute()
        {
            Init();
        }

        private static void Init()
        {
            ScheduledTasksCoordinator.Current.AddScheduledTasks(
                new FullBackUpTask());

            ScheduledTasksCoordinator.Current.OnUnexpectedException = (exception, scheduledTask) =>
            {
                //todo: log the exception.
                Trace.WriteLine(scheduledTask.Name + ":" + exception.Message);
            };

            ScheduledTasksCoordinator.Current.Start();
        }

        public static void End()
        {
            ScheduledTasksCoordinator.Current.Dispose();
        }

        public void WakeUp(string pageUrl)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.Headers.Add("User-Agent", "ScheduledTasks 1.0");
                    client.DownloadData(pageUrl);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex, "Exception On WakeUp DNTScheduler");
                Trace.WriteLine(ex.Message);
            }
        }

        void IRunOnEnd.Execute()
        {
            End();
            WakeUp(_configuration.SiteRootUrl);
        }
    }
}