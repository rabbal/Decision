using System;
using System.Collections.Generic;
using System.Text;
using Decision.Framework.Extensions;
using Decision.Framework.SEO.OpenGraph.Enums;
using Decision.Framework.SEO.OpenGraph.Media;
using Decision.Framework.SEO.OpenGraph.Structs;

namespace Decision.Framework.SEO.OpenGraph.ObjectTypes.Facebook
{
    /// <summary>
    ///     This object type represents a place of business that has a location, operating hours and contact information. This
    ///     object type is not part of
    ///     the Open Graph standard but is used by Facebook.
    ///     See https://developers.facebook.com/docs/reference/opengraph/object-type/business.business/
    /// </summary>
    public class OpenGraphBusiness : OpenGraphMetadata
    {
        private const string TimeOfDayFormat = "hh:mm";

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphBusiness" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="contactData">The contact data for the business.</param>
        /// <param name="location">The location of the business.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        /// <exception cref="System.ArgumentNullException">contactData or location is <c>null</c>.</exception>
        public OpenGraphBusiness(string title, OpenGraphImage image, OpenGraphContactData contactData,
            OpenGraphLocation location, string url = null)
            : base(title, image, url)
        {
            if (contactData == null)
            {
                throw new ArgumentNullException(nameof(contactData));
            }

            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            Location = location;
            ContactData = contactData;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder" /> containing the
        ///     Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:street_address",
                ContactData.StreetAddress);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:locality", ContactData.Locality);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:region", ContactData.Region);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:postal_code",
                ContactData.PostalCode);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:country_name",
                ContactData.Country);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:email", ContactData.Email);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:phone_number",
                ContactData.Phone);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:fax_number", ContactData.Fax);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:website", ContactData.Website);

            if (OpeningHours != null)
            {
                foreach (var hours in OpeningHours)
                {
                    stringBuilder.AppendMetaPropertyContent("business:hours:day", hours.Day.ToLowercaseString());
                    stringBuilder.AppendMetaPropertyContent("business:hours:start",
                        hours.Start.ToString(TimeOfDayFormat));
                    stringBuilder.AppendMetaPropertyContent("business:hours:end", hours.End.ToString(TimeOfDayFormat));
                }
            }

            stringBuilder.AppendMetaPropertyContent("place:location:latitude", Location.Latitude);
            stringBuilder.AppendMetaPropertyContent("place:location:longitude", Location.Longitude);
            stringBuilder.AppendMetaPropertyContentIfNotNull("place:location:altitude", Location.Altitude);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the contact data for the business.
        /// </summary>
        public OpenGraphContactData ContactData { get; }

        /// <summary>
        ///     Gets the location of the business.
        /// </summary>
        public OpenGraphLocation Location { get; }

        /// <summary>
        ///     Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace => "business: http://ogp.me/ns/business# place: http://ogp.me/ns/place#";

        /// <summary>
        ///     Gets or sets the opening hours of the business.
        /// </summary>
        public IEnumerable<OpenGraphHours> OpeningHours { get; set; }

        /// <summary>
        ///     Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type => OpenGraphType.Business;

        #endregion
    }
}