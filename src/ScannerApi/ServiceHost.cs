using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace ScannerService
{
    public class ServiceHost
    {
        private IDisposable webApp;

        public static string BaseAddress
        {
            get
            {
                return "http://localhost:8000/";
            }
        }

        public void Start()
        {
            webApp = WebApp.Start<Startup>(BaseAddress);
        }

        public void Stop()
        {
            webApp.Dispose();
        }
    }
}
