
using System.Web.Http;
using Owin;

namespace ScannerService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
         
            config.EnableCors();
        
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{type}"
            );

            appBuilder.UseWebApi(config);
        }
    }
}
