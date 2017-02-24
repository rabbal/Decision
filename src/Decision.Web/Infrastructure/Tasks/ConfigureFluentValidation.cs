using System.Web.Mvc;
using FluentValidation.Mvc;
using Decision.Web.Infrastructure.IocConfig;
using Decision.Web.Infrastructure.Tasks.Contracts;
using StructureMap;

namespace Decision.Web.Infrastructure.Tasks
{
    public class ConfigureFluentValidation : IBootstrapperTask
    {
        private readonly IContainer _container;
        public int Order => int.MaxValue;

        public ConfigureFluentValidation(IContainer container)
        {
            _container = container;
        }

        public void Execute()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            FluentValidationModelValidatorProvider.Configure(
                provider =>
                {
                    provider.AddImplicitRequiredValidator = false;
                    provider.ValidatorFactory = _container.GetInstance<StructureMapValidatorFactory>();
                });
        }
    }
}