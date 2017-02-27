using System;
using System.Linq;
using AutoMapper;
using Decision.Framework.MapperToolkit;
using Decision.ViewModels.GeneralBasicData.Applicants;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<ApplicantViewModel>();
                scan.WithDefaultConventions();

                scan.AddAllTypesOf<Profile>().NameBy(item => item.FullName);
                scan.AddAllTypesOf<IHaveCustomMappings>().NameBy(item => item.FullName);

                scan.AssemblyContainingType(typeof(IMapFrom<>));
                scan.ConnectImplementationsToTypesClosing(typeof(IMapFrom<>));
            });

            For<MapperConfiguration>().Singleton().Use("MapperConfig", context =>
            {
                var config = new MapperConfiguration(configuration =>
                {
                    configuration.CreateMissingTypeMaps = false; // It will not connect `Person` & `PersonViewModel` automatically.
                    AddAllCustomAutoMapperProfiles(context, configuration);
                    AddAllCustomMappings(context, configuration);
                    AddAllStandardMappings(configuration);
                });

                config.AssertConfigurationIsValid();

                return config;
            });

            For<IMapper>().Singleton().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));
        }

        private static void AddAllCustomAutoMapperProfiles(IContext ctx, IMapperConfigurationExpression cfg)
        {
            var profiles = ctx.GetAllInstances<Profile>().ToList();
            foreach (var profile in profiles)
            {
                cfg.AddProfile(profile);
            }
        }

        private static void AddAllCustomMappings(IContext context, IMapperConfigurationExpression configuration)
        {
            var profiles = context.GetAllInstances<IHaveCustomMappings>().ToList();
            foreach (var profile in profiles)
            {
                profile.CreateMappings(configuration);
            }
        }

        private static  void AddAllStandardMappings(IProfileExpression configuration)
        {
            var types = typeof(ApplicantViewModel).Assembly.GetExportedTypes();

            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) && !t.IsAbstract && !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                configuration.CreateMap(map.Source, map.Destination);
            }
        }
    }
}