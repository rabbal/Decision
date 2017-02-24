using System;
using System.Collections.Generic;
using System.Text;
using Decision.Common.Extensions;
using Decision.Common.SEO.OpenGraph.Enums;
using Decision.Common.SEO.OpenGraph.Media;
using Decision.Common.SEO.OpenGraph.Structs;

namespace Decision.Common.SEO.OpenGraph.ObjectTypes.Facebook
{
    /// <summary>
    ///     This object type represents a restaurant at a specific location. This object type is not part of the Open Graph
    ///     standard but is used by Facebook.
    ///     See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.restaurant/
    /// </summary>
    public class OpenGraphRestaurant : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphRestaurant" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="location">The location of the business.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        /// <exception cref="System.ArgumentNullException">location is <c>null</c>.</exception>
        public OpenGraphRestaurant(string title, OpenGraphImage image, OpenGraphLocation location, string url = null)
            : base(title, image, url)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            Location = location;
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

            stringBuilder.AppendMetaPropertyContent("place:location:latitude", Location.Latitude);
            stringBuilder.AppendMetaPropertyContent("place:location:longitude", Location.Longitude);
            stringBuilder.AppendMetaPropertyContentIfNotNull("place:location:altitude", Location.Altitude);

            if (Categories != null)
            {
                foreach (var category in Categories)
                {
                    stringBuilder.AppendMetaPropertyContent("restaurant:category", category);
                }
            }

            if (ContactInfo != null)
            {
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:street_address",
                    ContactInfo.StreetAddress);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:locality",
                    ContactInfo.Locality);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:region",
                    ContactInfo.Region);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:postal_code",
                    ContactInfo.PostalCode);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:country_name",
                    ContactInfo.Country);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:email", ContactInfo.Email);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:phone_number",
                    ContactInfo.Phone);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:fax_number",
                    ContactInfo.Fax);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:website",
                    ContactInfo.Website);
            }

            if (MenuUrls != null)
            {
                foreach (var menuUrl in MenuUrls)
                {
                    stringBuilder.AppendMetaPropertyContent("restaurant:menu", menuUrl);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:price_rating", PriceRating);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a collection of categories describing this restaurant's food.
        /// </summary>
        public IEnumerable<string> Categories { get; set; }

        /// <summary>
        ///     Gets or sets the contact information for the restaurant.
        /// </summary>
        public OpenGraphContactData ContactInfo { get; set; }

        /// <summary>
        ///     Gets the location of the place.
        /// </summary>
        public OpenGraphLocation Location { get; }

        /// <summary>
        ///     Gets or sets the URL's to the pages about the menus. This URL must contain restaurant.menu meta tags
        ///     <see cref="OpenGraphRestaurantMenu" />.
        /// </summary>
        public IEnumerable<string> MenuUrls { get; set; }

        /// <summary>
        ///     Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace => "restaurant: http://ogp.me/ns/restaurant# place: http://ogp.me/ns/place#";

        /// <summary>
        ///     Gets or sets the price rating for this restaurant (from 1 to 4).
        /// </summary>
        public int? PriceRating { get; set; }

        /// <summary>
        ///     Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type => OpenGraphType.Restaurant;

        #endregion
    }
}