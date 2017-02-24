using System;
using System.Linq;
using AutoMapper;
using StructureMap;
using StructureMap.Graph;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            var profiles =
                typeof(AutoMapperRegistry).Assembly.GetTypes()
                    .Where(t => typeof(Profile).IsAssignableFrom(t))
                    .Select(t => (Profile) Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.AddAllTypesOf<Profile>().NameBy(item => item.FullName);
            });

            For<MapperConfiguration>().Use(config);
            For<IMapper>().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));
        }
    }
}