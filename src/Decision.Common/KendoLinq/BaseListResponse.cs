using System.Collections;
using System.Collections.Generic;

namespace NTierMvcFramework.Common.KendoLinq
{
    /// <summary>
    ///     Describes the result of Kendo DataSource read operation.
    /// </summary>
    public abstract class BaseListResponse
    {
        /// <summary>
        ///     Represents a single page of processed data.
        /// </summary>
        public IEnumerable Data { get; set; }

        /// <summary>
        ///     The total number of records available.
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        ///     Represents a requested aggregates.
        /// </summary>
        public object Aggregates { get; set; }

        /// <summary>
        /// Represents The Server side Errors
        /// </summary>
        public IList<string> Errors { get; set; }

    }
}