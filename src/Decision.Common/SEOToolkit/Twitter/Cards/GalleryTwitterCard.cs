using System;
using System.Text;
using Decision.Common.SEOToolkit.Twitter.Enums;
using Decision.Common.SEOToolkit.Twitter.Structs;
using NTierMvcFramework.Common.Extensions;

namespace Decision.Common.SEOToolkit.Twitter.Cards
{
    /// <summary>
    ///     The Gallery Card allows you to represent collections of photos within a Tweet. This Card type is
    ///     designed to let the user know that there’s more than just a single image at the URL shared, but
    ///     rather a gallery of related images. You can specify up to 4 different images to show in the gallery
    ///     card. You can also provide attribution to the photographer of the  gallery by specifying the value of the creator
    ///     tag.
    ///     See https://dev.twitter.com/cards/types/gallery.
    /// </summary>
    public class GalleryTwitterCard : TwitterCard
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GalleryTwitterCard" /> class.
        /// </summary>
        /// <param name="username">
        ///     The Twitter username of the creator of the content on the page e.g. @RehanSaeedUK. This is an
        ///     optional property.
        /// </param>
        /// <param name="image0">The image0.</param>
        /// <param name="image1">The image1.</param>
        /// <param name="image2">The image2.</param>
        /// <param name="image3">The image3.</param>
        /// <exception cref="System.ArgumentNullException">username or image0 or image1 or image2 or image3 is <c>null</c>.</exception>
        public GalleryTwitterCard(string username, TwitterImage image0, TwitterImage image1, TwitterImage image2,
            TwitterImage image3)
            : base(username)
        {
            if (image0 == null)
            {
                throw new ArgumentNullException(nameof(image0));
            }
            if (image1 == null)
            {
                throw new ArgumentNullException(nameof(image1));
            }
            if (image2 == null)
            {
                throw new ArgumentNullException(nameof(image2));
            }
            if (image3 == null)
            {
                throw new ArgumentNullException(nameof(image3));
            }

            Image0 = image0;
            Image1 = image1;
            Image2 = image2;
            Image3 = image3;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder" /> containing the
        ///     Twitter card meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaNameContentIfNotNull("twitter:title", Title);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:description", Description);

            if (Image0 != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image0", Image0.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image0:height", Image0.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image0:width", Image0.Width);
            }

            if (Image1 != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image1", Image1.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image1:height", Image1.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image1:width", Image1.Width);
            }

            if (Image2 != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image2", Image2.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image2:height", Image2.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image2:width", Image2.Width);
            }

            if (Image3 != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image3", Image3.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image3:height", Image3.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image3:width", Image3.Width);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the Twitter username of the creator of the content on the page e.g. @RehanSaeedUK. This is an optional
        ///     property.
        /// </summary>
        public string CreatorUsername { get; set; }

        /// <summary>
        ///     Gets or sets the description that concisely summarizes the content of the page, as appropriate for
        ///     presentation within a Tweet. Do not re-use the title text as the description, or use this field
        ///     to describe the general services provided by the website. Description text will be truncated at
        ///     the word to 200 characters. This is an optional property. If you are using Facebook's Open Graph og:description,
        ///     do not use this unless you want a different description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets the URL to the first image representing the content of the page.  Images must be less than 1MB in size.
        ///     If you are using Facebook's Open Graph og:image, you do not have to use this unless you want a different image but
        ///     it is recommended to supply a smaller image that fits Twitters needs.
        /// </summary>
        public TwitterImage Image0 { get; }

        /// <summary>
        ///     Gets the URL to the second image representing the content of the page.  Images must be less than 1MB in size.
        ///     If you are using Facebook's Open Graph og:image, you do not have to use this unless you want a different image but
        ///     it is recommended to supply a smaller image that fits Twitters needs.
        /// </summary>
        public TwitterImage Image1 { get; }

        /// <summary>
        ///     Gets the URL to the third image representing the content of the page.  Images must be less than 1MB in size.
        ///     If you are using Facebook's Open Graph og:image, you do not have to use this unless you want a different image but
        ///     it is recommended to supply a smaller image that fits Twitters needs.
        /// </summary>
        public TwitterImage Image2 { get; }

        /// <summary>
        ///     Gets the URL to the fourth image representing the content of the page.  Images must be less than 1MB in size.
        ///     If you are using Facebook's Open Graph og:image, you do not have to use this unless you want a different image but
        ///     it is recommended to supply a smaller image that fits Twitters needs.
        /// </summary>
        public TwitterImage Image3 { get; }

        /// <summary>
        ///     Gets or sets the title of your content as it should appear in the card. You may specify an empty
        ///     string if you wish no title to render.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets the type of the Twitter card.
        /// </summary>
        public override TwitterCardType Type => TwitterCardType.Gallery;

        #endregion
    }
}