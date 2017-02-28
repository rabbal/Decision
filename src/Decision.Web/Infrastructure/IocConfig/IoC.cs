using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{

    public static class IoC
    {
        #region Fields 

        private static readonly Lazy<Container> ContainerBuilder =
            new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        
        #endregion

        #region Properties
        public static IContainer Container => ContainerBuilder.Value;

        #endregion

        #region Private Methods
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

            //container.AssertConfigurationIsValid();
            return container;
        }

        #endregion

        #region Public Methods 
        public static IEnumerable<T> GetAllInstances<T>()
        {
            return Container.GetAllInstances<T>();
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