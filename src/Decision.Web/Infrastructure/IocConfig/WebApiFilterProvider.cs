using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class WebApiFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {

        private readonly IContainer _container;
        public WebApiFilterProvider(IContainer container)
        {
            _container = container;
        }

        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration,
            HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);

            foreach (var filter in filters)
            {
                _container.BuildUp(filter.Instance);
                yield return filter;
            }
        }
    }
}