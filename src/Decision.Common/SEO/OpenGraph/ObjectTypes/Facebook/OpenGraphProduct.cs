using System;
using System.Collections.Generic;
using System.Text;
using Decision.Common.Extensions;
using Decision.Common.SEO.OpenGraph.Enums;
using Decision.Common.SEO.OpenGraph.Media;
using Decision.Common.SEO.OpenGraph.ObjectTypes.Standard;
using Decision.Common.SEO.OpenGraph.Structs;

namespace Decision.Common.SEO.OpenGraph.ObjectTypes.Facebook
{
    /// <summary>
    ///     This object type represents a product. This includes both virtual and physical products, but it typically
    ///     represents items that are available in
    ///     an online store. This object type is not part of the Open Graph standard but is used by Facebook.
    ///     See https://developers.facebook.com/docs/reference/opengraph/object-type/product/
    /// </summary>
    public class OpenGraphProduct : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphProduct" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphProduct(string title, OpenGraphImage image, string url = null)
            : base(title, image, url)
        {
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

            if (AgeGroup.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("product:age_group", AgeGroup.Value.ToLowercaseString());
            }

            if (Availability.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("product:availability",
                    Availability.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:brand", Brand);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:category", Category);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:color", Colour);

            if (Condition.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("product:condition", Condition.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:ean", EAN);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:expiration_time", ExpirationTime);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:is_product_shareable", IsShareable);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:isbn", ISBN);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:material", Material);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:mfr_part_no", ManufacturerPartNumber);

            if (OriginalPrices != null)
            {
                foreach (var originalPrice in OriginalPrices)
                {
                    stringBuilder.AppendMetaPropertyContent("product:original_price:amount", originalPrice.Amount);
                    stringBuilder.AppendMetaPropertyContent("product:original_price:currency", originalPrice.Currency);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:pattern", Pattern);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:plural_title", PluralTitle);

            if (PretaxPrices != null)
            {
                foreach (var pretaxPrice in PretaxPrices)
                {
                    stringBuilder.AppendMetaPropertyContent("product:pretax_price:amount", pretaxPrice.Amount);
                    stringBuilder.AppendMetaPropertyContent("product:pretax_price:currency", pretaxPrice.Currency);
                }
            }

            if (Prices != null)
            {
                foreach (var price in Prices)
                {
                    stringBuilder.AppendMetaPropertyContent("product:price:amount", price.Amount);
                    stringBuilder.AppendMetaPropertyContent("product:price:currency", price.Currency);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:product_link", ProductLinkUrl);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:purchase_limit", PurchaseLimit);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer", RetailerUrl);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_category", RetailerCategory);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_part_no", RetailerPartNumber);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_title", RetailerTitle);

            if (SalePrice != null)
            {
                stringBuilder.AppendMetaPropertyContent("product:sale_price:amount", SalePrice.Amount);
                stringBuilder.AppendMetaPropertyContent("product:sale_price:currency", SalePrice.Currency);
            }

            if (SalePriceDates != null)
            {
                stringBuilder.AppendMetaPropertyContent("product:sale_price_dates:start", SalePriceDates.Start);
                stringBuilder.AppendMetaPropertyContent("product:sale_price_dates:end", SalePriceDates.End);
            }

            if (ShippingCost != null)
            {
                foreach (var shippingCost in ShippingCost)
                {
                    stringBuilder.AppendMetaPropertyContent("product:shipping_cost:amount", shippingCost.Amount);
                    stringBuilder.AppendMetaPropertyContent("product:shipping_cost:currency", shippingCost.Currency);
                }
            }

            if (ShippingWeight != null)
            {
                stringBuilder.AppendMetaPropertyContent("product:shipping_weight:value", ShippingWeight.Value);
                stringBuilder.AppendMetaPropertyContent("product:shipping_weight:units", ShippingWeight.Units);
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:size", Size);

            if (TargetGender.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("product:target_gender",
                    TargetGender.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:upc", UPC);

            if (Weight != null)
            {
                stringBuilder.AppendMetaPropertyContent("product:weight:value", Weight.Value);
                stringBuilder.AppendMetaPropertyContent("product:weight:units", Weight.Units);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the target age group of the product, one of 'kids' or 'adult'.
        /// </summary>
        public OpenGraphAgeGroup? AgeGroup { get; set; }

        /// <summary>
        ///     Gets or sets the availability of the product, one of 'instock', 'oos', or 'pending'.
        /// </summary>
        public OpenGraphAvailability? Availability { get; set; }

        /// <summary>
        ///     Gets or sets the brand of the product or its original manufacturer.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        ///     Gets or sets the category for the product.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        ///     Gets or sets the colour of the product.
        /// </summary>
        public string Colour { get; set; }

        /// <summary>
        ///     Gets or sets the condition of the item, one of 'new', 'refurbished', or 'used'.
        /// </summary>
        public OpenGraphCondition? Condition { get; set; }

        /// <summary>
        ///     Gets or sets an International Article Number, or European Article Number (EAN), for the product.
        /// </summary>
        public string EAN { get; set; }

        /// <summary>
        ///     Gets or sets a time representing when the product expired (or will expire).
        /// </summary>
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the associated story has a share button on it.
        /// </summary>
        public bool? IsShareable { get; set; }

        /// <summary>
        ///     Gets or sets a International Standard Book Number (ISBN) for the product, intended for when it is a book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        ///     Gets or sets the manufacturers part number for the product.
        /// </summary>
        public string ManufacturerPartNumber { get; set; }

        /// <summary>
        ///     Gets or sets a description of the material used to make the product.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        ///     Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace => "product: http://ogp.me/ns/product#";

        /// <summary>
        ///     Gets or sets the original prices of the product.
        /// </summary>
        public IEnumerable<OpenGraphCurrency> OriginalPrices { get; set; }

        /// <summary>
        ///     Gets or sets a description of the pattern used on the product.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        ///     Gets or sets a title to be used to describe multiple items of this product.
        /// </summary>
        public string PluralTitle { get; set; }

        /// <summary>
        ///     Gets or sets the pre-tax prices of the product.
        /// </summary>
        public IEnumerable<OpenGraphCurrency> PretaxPrices { get; set; }

        /// <summary>
        ///     Gets or sets the prices of the product.
        /// </summary>
        public IEnumerable<OpenGraphCurrency> Prices { get; set; }

        /// <summary>
        ///     Gets or sets a URL link to find out more about the product
        /// </summary>
        public string ProductLinkUrl { get; set; }

        /// <summary>
        ///     Gets or sets the maximum number of times a person can purchase the product.
        /// </summary>
        public int PurchaseLimit { get; set; }

        /// <summary>
        ///     Gets or sets the URL to the page about the retailer of the product. This URL must contain profile meta tags
        ///     <see cref="OpenGraphProfile" />.
        /// </summary>
        public string RetailerUrl { get; set; }

        /// <summary>
        ///     Gets or sets the retailer's category for the product.
        /// </summary>
        public string RetailerCategory { get; set; }

        /// <summary>
        ///     Gets or sets the retailer's part number for the product.
        /// </summary>
        public string RetailerPartNumber { get; set; }

        /// <summary>
        ///     Gets or sets the name of the retailer of the product.
        /// </summary>
        public string RetailerTitle { get; set; }

        /// <summary>
        ///     Gets or sets the sale price of the product.
        /// </summary>
        public OpenGraphCurrency SalePrice { get; set; }

        /// <summary>
        ///     Gets or sets the date range for which the sale price is valid.
        /// </summary>
        public OpenGraphDateTimeRange SalePriceDates { get; set; }

        /// <summary>
        ///     Gets or sets the shipping cost of the product.
        /// </summary>
        public IEnumerable<OpenGraphCurrency> ShippingCost { get; set; }

        /// <summary>
        ///     Gets or sets the shipping weight of the product.
        /// </summary>
        public OpenGraphQuantity ShippingWeight { get; set; }

        /// <summary>
        ///     Gets or sets a size describing the product(such as 'S', 'M', 'L').
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        ///     Gets or sets the target gender for the product.
        /// </summary>
        public OpenGraphTargetGender? TargetGender { get; set; }

        /// <summary>
        ///     Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type => OpenGraphType.Product;

        /// <summary>
        ///     Gets or sets a Universal Product Code(UPC) for the product.
        /// </summary>
        public string UPC { get; set; }

        /// <summary>
        ///     Gets or sets the weight of the product.
        /// </summary>
        public OpenGraphQuantity Weight { get; set; }

        #endregion
    }
}