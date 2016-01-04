using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ScannerService
{
    public class ServiceHostFactory
    {
        public static void Run()
        {
            HostFactory.Run(config =>
            {
                config.SetServiceName("ApiServices");
                config.SetDisplayName("Api Services ]");
                config.SetDescription("No Description");

                config.RunAsLocalService();

                config.Service<ServiceHost>(cfg =>
                {
                    cfg.ConstructUsing(builder => new ServiceHost());

                    cfg.WhenStarted(service => service.Start());
                    cfg.WhenStopped(service => service.Stop());
                });
            });
        }
    }
}
