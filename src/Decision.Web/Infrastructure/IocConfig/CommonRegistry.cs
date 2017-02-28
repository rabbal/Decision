﻿using Decision.Framework.Caching;
using Decision.Framework.Configuration;
using Decision.Framework.Logging;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<IConfigurationManager>().Singleton().Use<ConfigurationManagerWrapper>();

            //For<IRedisCache>().Singleton().Add<RedisCache>();
            //For<IStorageProvider>().Use<FileSystemStorageProvider>();
            //For<IVirtualPathProvider>().Use<DefaultVirtualPathProvider>();

            Scan(a =>
            {
                a.AssemblyContainingType<ILogger>();
                a.WithDefaultConventions();
            });
        }
    }
}