using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace Decision.Web.Infrastructure.IocConfig
{
    /// <summary>
    ///     The structure map filter provider.
    /// </summary>
    public class StructureMapFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IContainer _container;
        public StructureMapFilterProvider(IContainer container)
        {
            _container = container;
        }

        #region Methods (1)

        /// <summary>
        ///     The get filters.
        /// </summary>
        /// <param name="controllerContext">
        ///     The controller context.
        /// </param>
        /// <param name="actionDescriptor">
        ///     The action descriptor.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);

            foreach (var filter in filters)
            {
                _container.BuildUp(filter.Instance);
                yield return filter;
            }
        }

        #endregion
    }
}