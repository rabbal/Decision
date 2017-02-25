using System.Web.Helpers;
using Decision.Framework.Configuration;
using Decision.Web.Infrastructure.Tasks.Contracts;

namespace Decision.Web.Infrastructure.Tasks
{
    public class ConfigureAntiForgery : IBootstrapperTask
    {
        private readonly IAppConfiguration _configuration;
        public int Order => int.MaxValue;

        public ConfigureAntiForgery(IAppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Execute()
        {
            ConfigureAntiForgeryTokens();
        }

        /// <summary>
        ///     Configures the anti-forgery tokens. See
        ///     http://www.asp.net/mvc/overview/security/xsrfcsrf-prevention-in-aspnet-mvc-and-web-pages
        /// </summary>
        private  void ConfigureAntiForgeryTokens()
        {
            AntiForgeryConfig.CookieName = "f";
            AntiForgeryConfig.RequireSsl = _configuration.RquiredHttps;
        }
    }
}