using System;
using System.Threading;
using AutoMapper;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{

    public static class IoC
    {

        #region Fields (1)

        private static readonly Lazy<Container> ContainerBuilder =
            new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion

        #region Properties (1)
        public static IContainer Container => ContainerBuilder.Value;

        #endregion

        #region Methods (6)

        private static Container DefaultContainer()
        {
            var container = new Container(ioc =>
            {
                ioc.AddRegistry<StandardRegistry>();
                ioc.AddRegistry<ControllerRegistry>();
                ioc.AddRegistry<MvcRegistry>();
                ioc.AddRegistry<TaskRegistry>();
                ioc.AddRegistry<ModelMetadataRegistry>();
                ioc.AddRegistry<IdentityRegistry>();
                ioc.AddRegistry<ServicesRegistry>();
                ioc.AddRegistry<SignalrRegistry>();
                ioc.AddRegistry<CommonRegistry>();
                ioc.AddRegistry<ValidatorsRegistry>();
                ioc.AddRegistry<AutoMapperRegistry>();
                ioc.AddRegistry<WebApiRegistry>();
            });

            ConfigureAutoMapper(container);

            //container.AssertConfigurationIsValid();
            return container;
        }

        private static void ConfigureAutoMapper(IContainer container)
        {
            var configuration = container.TryGetInstance<IConfiguration>();
            if (configuration == null) return;
            //saying AutoMapper how to resolve services
            configuration.ConstructServicesUsing(Container.GetInstance);
            foreach (var profile in container.GetAllInstances<Profile>())
            {
                configuration.AddProfile(profile);
            }
            Resolve<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
        }
        public static T Resolve<T>()
        {
            return Container.GetInstance<T>();
        }
        public static T Resolve<T>(string name)
        {
            return Container.GetInstance<T>(name);
        }
        public static void BuildUp(object target)
        {
            Container.BuildUp(target);
        }

        /// <summary>
        /// a simple helper method that return a string with the contents
        ///  (resolved objects) in the container. Really helpful when troubleshooting
        /// </summary>
        /// <returns></returns>
        public static string WhatDoIHave()
        {
            return Container.WhatDoIHave();
        }
        #endregion
    }
}