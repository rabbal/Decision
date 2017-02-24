using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DNTScheduler;

namespace Decision.Web.Infrastructure.WebTasks
{
    public class FullBackUpTask : ScheduledTaskTemplate
    {
        /// <summary>
        ///     If you have multiple jobs at the same time, this value indicates the order of their execution.
        /// </summary>
        public override int Order => 2;
        public override string Name => "تهيه پشتيبان کامل";
        public override bool RunAt(DateTime utcNow)
        {
            if (IsShuttingDown || Pause)
                return false;

            var now = utcNow.AddHours(3.5);
            return now.Hour == 0 && now.Minute == 0 && now.Second == 1;

            //(now.Day % 3 == 0) && now.Hour == 0 && now.Minute == 1 && now.Second == 1;

            /*(now.DayOfWeek == DayOfWeek.Friday) &&
               (now.Hour == 3) &&
               (now.Minute == 1) &&
               (now.Second == 1)*/
            //now.Hour == 23 && now.Minute == 33 && now.Second == 1;
        }

        public override void Run()
        {
            if (IsShuttingDown || Pause)
                return;

            Trace.WriteLine("Running Do Backup");
        }

        public override Task RunAsync()
        {
            if (IsShuttingDown || Pause)
                return Task.FromResult(0);

            Trace.WriteLine("Running DoAsync Backup");

            return base.RunAsync();
        }
    }
}