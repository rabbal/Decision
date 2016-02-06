using System;

namespace Decision.Common.OpenGraph.Structs
{
    /// <summary>
    /// A date and time range.
    /// </summary>
    public class OpenGraphDateTimeRange
    {
        private readonly DateTime _end;
        private readonly DateTime _start;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphDateTimeRange"/> class.
        /// </summary>
        /// <param name="start">The start date and time.</param>
        /// <param name="end">The end date and time.</param>
        public OpenGraphDateTimeRange(DateTime start, DateTime end)
        {
            this._start = start;
            this._end = end;
        }

        /// <summary>
        /// Gets the end date and time.
        /// </summary>
        public DateTime End { get { return this._end; } }

        /// <summary>
        /// Gets the start date and time.
        /// </summary>
        public DateTime Start { get { return this._start; } }
    }
}
