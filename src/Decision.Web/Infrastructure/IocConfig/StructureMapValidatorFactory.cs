using System;
using FluentValidation;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        private readonly IContainer _container;
        public StructureMapValidatorFactory(IContainer container)
        {
            _container = container;
        }
        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.TryGetInstance(validatorType) as IValidator;
        }
    }
}