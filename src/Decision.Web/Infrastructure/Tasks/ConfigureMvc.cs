using System.Linq;
using System.Web.Mvc;
using CaptchaMvc.Infrastructure;
using Decision.Common.MvcToolkit.Json;
using Decision.Web.Infrastructure.IocConfig;
using Decision.Web.Infrastructure.Tasks.Contracts;
using StructureMap;

namespace Decision.Web.Infrastructure.Tasks
{
    public class ConfigureMvc : IBootstrapperTask
    {
        private readonly IContainer _container;
        public int Order => int.MaxValue;
        public ConfigureMvc(IContainer container)
        {
            _container = container;
        }

        public void Execute()
        {
            ControllerBuilder.Current.SetControllerFactory(IoC.Resolve<StructureMapControllerFactory>());

            var filterProvider = FilterProviders.Providers.Single(provider => provider is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProvider);
            FilterProviders.Providers.Add(IoC.Resolve<StructureMapFilterProvider>());

            var defaultJsonFactory = ValueProviderFactories.Factories
                .OfType<JsonValueProviderFactory>().FirstOrDefault();
            var index = ValueProviderFactories.Factories.IndexOf(defaultJsonFactory);
            ValueProviderFactories.Factories.Remove(defaultJsonFactory);
            ValueProviderFactories.Factories.Insert(index, new JsonNetValueProviderFactory());

            MvcHandler.DisableMvcResponseHeader = true;

            CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
        }
    }
}