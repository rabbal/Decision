namespace Decision.Common.OpenGraph.Structs
{
    /// <summary>
    /// A location specified by latitude and longitude.
    /// </summary>
    public class OpenGraphLocation
    {
        private readonly double? _altitude;
        private readonly double _latitude;
        private readonly double _longitude;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphLocation"/> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        public OpenGraphLocation(double latitude, double longitude)
        {
            this._latitude = latitude;
            this._longitude = longitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphLocation"/> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        /// <param name="altitude">The altitude of the location.</param>
        public OpenGraphLocation(double latitude, double longitude, double altitude)
            : this(latitude, longitude)
        {
            this._altitude = altitude;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the altitude of the location.
        /// </summary>
        public double? Altitude { get { return this._altitude; } }

        /// <summary>
        /// Gets the latitude of the location.
        /// </summary>
        public double Latitude { get { return this._latitude; } }

        /// <summary>
        /// Gets the longitude of the location.
        /// </summary>
        public double Longitude { get { return this._longitude; } }

        #endregion
    }
}
