using System.Text;
using Decision.Common.Extensions;

namespace Decision.Common.SEO.OpenGraph.Media
{
    /// <summary>
    ///     A audio file that complements this object.
    /// </summary>
    public class OpenGraphAudio : OpenGraphMedia
    {
        #region Public Methods

        /// <summary>
        ///     Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder" /> containing the
        ///     Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            stringBuilder.AppendMetaPropertyContent("og:audio", Url);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:audio:secure_url", UrlSecure);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:audio:type", Type);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphAudio" /> class.
        /// </summary>
        /// <param name="audioUrl">The audio URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if audioUrl is <c>null</c>.</exception>
        public OpenGraphAudio(string audioUrl)
            : base(audioUrl)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphAudio" /> class.
        /// </summary>
        /// <param name="mediaUrl">The media URL.</param>
        /// <param name="type">
        ///     The MIME type of the media e.g. media/ogg. This is optional if your media URL ends with
        ///     a file extension, otherwise it is recommended.
        /// </param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="mediaUrl" /> is <c>null</c>.</exception>
        public OpenGraphAudio(string mediaUrl, string type) : this(mediaUrl)
        {
            Type = type;
        }

        #endregion
    }
}