﻿using Decision.Common.Caching;
using Decision.Common.Configuration;
using Decision.Common.Logging;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<ICache>().Use<HttpContextCacheAdapter>();
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