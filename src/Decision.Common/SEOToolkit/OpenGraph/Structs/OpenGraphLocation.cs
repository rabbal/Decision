namespace Decision.Common.SEOToolkit.OpenGraph.Structs
{
    /// <summary>
    ///     A location specified by latitude and longitude.
    /// </summary>
    public class OpenGraphLocation
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphLocation" /> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        public OpenGraphLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphLocation" /> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        /// <param name="altitude">The altitude of the location.</param>
        public OpenGraphLocation(double latitude, double longitude, double altitude)
            : this(latitude, longitude)
        {
            Altitude = altitude;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the altitude of the location.
        /// </summary>
        public double? Altitude { get; }

        /// <summary>
        ///     Gets the latitude of the location.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        ///     Gets the longitude of the location.
        /// </summary>
        public double Longitude { get; }

        #endregion
    }
}