using System.Collections.Generic;

namespace NTierMvcFramework.Common.KendoLinq
{
    /// <summary>
    ///     Describes a Kendo Datasource request.
    /// </summary>
    public abstract class BaseListRequest
    {
        /// <summary>
        ///     Specifies how many items to take.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        ///     Specifies how many items to skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        ///     Specifies the requested sort order.
        /// </summary>
        public IEnumerable<Sort> Sorts { get; set; }

        /// <summary>
        ///     Specifies the requested filter.
        /// </summary>
        public Filter Filter { get; set; }
    }
}