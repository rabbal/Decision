using System;

namespace Decision.Common.OpenGraph.Structs
{
    /// <summary>
    /// A period of time on the specified day.
    /// </summary>
    public class OpenGraphHours
    {
        private readonly DayOfWeek _day;
        private readonly TimeSpan _end;
        private readonly TimeSpan _start;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphHours"/> class.
        /// </summary>
        /// <param name="day">The day in the week.</param>
        /// <param name="start">The start time of the day. This can be a value from 00:00 to 24:00 hours.</param>
        /// <param name="end">The end time of the day. This can be a value from 00:00 to 24:00 hours.</param>
        public OpenGraphHours(DayOfWeek day, TimeSpan start, TimeSpan end)
        {
            this._day = day;
            this._end = end;
            this._start = start;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the day in the week.
        /// </summary>
        public DayOfWeek Day { get { return this._day; } }

        /// <summary>
        /// Gets the end time of the day. This can be a value from 00:00 to 24:00 hours.
        /// </summary>
        public TimeSpan End { get { return this._end; } }

        /// <summary>
        /// Gets the start time of the day. This can be a value from 00:00 to 24:00 hours.
        /// </summary>
        public TimeSpan Start { get { return this._start; } } 

        #endregion
    }
}
