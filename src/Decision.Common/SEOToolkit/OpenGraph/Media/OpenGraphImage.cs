using System.Text;
using NTierMvcFramework.Common.Extensions;

namespace Decision.Common.SEOToolkit.OpenGraph.Media
{
    /// <summary>
    ///     An image which should represent your object within the graph. Use images that are at least 1200 x 630 pixels
    ///     for the best display on high resolution devices. At the minimum, you should use images that are 600 x 315 pixels
    ///     to display link page posts with larger images. If your image is smaller than 600 x 315 pixels, it will still
    ///     display in the link page post, but the size will be much smaller.  Try to keep your images as close to
    ///     1.91:1 aspect ratio as possible to display the full image in News Feed without any cropping. The minimum size
    ///     of an image is 200 x 200. If you try to use an image smaller than this you will see an error in the URL Debugger.
    /// </summary>
    public class OpenGraphImage : OpenGraphSizedMedia
    {
        #region Public Methods

        /// <summary>
        ///     Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder" /> containing the
        ///     Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            stringBuilder.AppendMetaPropertyContent("og:image", Url);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:image:secure_url", UrlSecure);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:image:type", Type);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:image:width", Width);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:image:height", Height);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphImage" /> class.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if imageUrl is <c>null</c>.</exception>
        public OpenGraphImage(string imageUrl)
            : base(imageUrl)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphImage" /> class.
        /// </summary>
        /// <param name="imageUrl">The media URL.</param>
        /// <param name="type">
        ///     The MIME type of the media e.g. media/jpeg. This is optional if your media URL ends with
        ///     a file extension, otherwise it is recommended.
        /// </param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="imageUrl" /> is <c>null</c>.</exception>
        public OpenGraphImage(string imageUrl, string type) : this(imageUrl)
        {
            Type = type;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphImage" /> class.
        /// </summary>
        /// <param name="imageUrl">The media URL.</param>
        /// <param name="width">The width of the media in pixels. This is optional.</param>
        /// <param name="height">The height of the media in pixels. This is optional.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="imageUrl" /> is <c>null</c>.</exception>
        public OpenGraphImage(string imageUrl, int width, int height) : this(imageUrl)
        {
            Height = height;
            Width = width;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphImage" /> class.
        /// </summary>
        /// <param name="imageUrl">The media URL.</param>
        /// <param name="type">
        ///     The MIME type of the media e.g. media/jpeg. This is optional if your media URL ends with
        ///     a file extension, otherwise it is recommended.
        /// </param>
        /// <param name="width">The width of the media in pixels. This is optional.</param>
        /// <param name="height">The height of the media in pixels. This is optional.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="imageUrl" /> is <c>null</c>.</exception>
        public OpenGraphImage(string imageUrl, string type, int width, int height) : this(imageUrl, type)
        {
            Height = height;
            Width = width;
        }

        #endregion
    }
}