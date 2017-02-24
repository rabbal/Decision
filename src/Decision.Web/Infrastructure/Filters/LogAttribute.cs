using System.Web.Mvc;
using StructureMap;

namespace Decision.Web.Infrastructure.Filters
{
    public sealed class LogAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///     if using global register for filter then your
        ///     should inject IContainer instead of Service
        /// </summary>
        private readonly IContainer _container;

        public LogAttribute(IContainer container)
        {
            _container = container;
        }
    }
}