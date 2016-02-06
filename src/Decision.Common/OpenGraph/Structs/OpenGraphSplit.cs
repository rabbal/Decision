using System;
using System.Collections.Generic;

namespace Decision.Common.OpenGraph.Structs
{
    /// <summary>
    /// A split value in a fitness event.
    /// </summary>
    public class OpenGraphSplit
    {
        private readonly bool _isMiles;
        private readonly IEnumerable<OpenGraphQuantity> _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphSplit"/> class.
        /// </summary>
        /// <param name="isMiles">if set to <c>true</c> distance is measured in miles, otherwise kilometres.</param>
        /// <param name="values">The values.</param>
        /// <exception cref="System.ArgumentNullException">values</exception>
        public OpenGraphSplit(bool isMiles, IEnumerable<OpenGraphQuantity> values)
        {
            if (values == null) { throw new ArgumentNullException("values"); }

            this._isMiles = isMiles;
            this._values = values;
        }

        /// <summary>
        /// Gets a value indicating whether distance is measured in miles.
        /// </summary>
        public bool IsMiles { get { return this._isMiles; } }

        /// <summary>
        /// Gets a value indicating whether distance is measured in kilometres.
        /// </summary>
        public bool IsKilometers { get { return !this._isMiles; } }

        /// <summary>
        /// Gets the split values.
        /// </summary>
        public IEnumerable<OpenGraphQuantity> Values { get { return this._values; } }
    }
}
