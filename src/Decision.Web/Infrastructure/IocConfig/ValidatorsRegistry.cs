using System.Reflection;
using FluentValidation;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class ValidatorsRegistry : Registry
    {
        public ValidatorsRegistry()
        {
            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(result =>
                {
                    For(result.InterfaceType)
                        .Singleton()
                        .Use(result.ValidatorType);
                });
        }
    }
}