using System.Linq;
using Decision.Web.Infrastructure.IocConfig;
using Decision.Web.Infrastructure.Tasks.Contracts;
using StructureMap.Web.Pipeline;

namespace Decision.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            var tasksToRun = IoC.GetAllInstances<IBootstrapperTask>().OrderBy(a => a.Order).ToList();

            foreach (var item in tasksToRun)
            {
                item.Execute();
            }

        }

        public static void RunOnStartUp()
        {
            var tasksToRun = IoC.GetAllInstances<IRunStartUp>().OrderBy(a => a.Order).ToList();
            foreach (var item in tasksToRun)
            {
                item.Execute();
            }
        }

        public static void RunOnEnd()
        {
            var tasksToRun = IoC.GetAllInstances<IRunOnEnd>().OrderBy(a => a.Order).ToList();
            foreach (var item in tasksToRun)
            {
                item.Execute();
            }

        }


        public static void RunOnError()
        {
            var tasksToRun = IoC.GetAllInstances<IRunOnError>().OrderBy(a => a.Order).ToList();
            foreach (var item in tasksToRun)
            {
                item.Execute();
            }

        }


        public static void RunOnEachRequest()
        {

            var tasksToRun = IoC.GetAllInstances<IRunOnEachRequest>().OrderBy(a => a.Order).ToList();
            foreach (var item in tasksToRun)
            {
                item.Execute();
            }

        }

        public static void RunAfterEachRequest()
        {
            try
            {

                var tasksToRun = IoC.GetAllInstances<IRunAfterEachRequest>().OrderBy(a => a.Order).ToList();
                foreach (var item in tasksToRun)
                {
                    item.Execute();
                }
            }
            finally
            {
                HttpContextLifecycle.DisposeAndClearAll();
            }

        }

    }
}
