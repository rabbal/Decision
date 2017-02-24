using System;
using System.Collections.Generic;
using System.Text;
using Decision.Common.SEOToolkit.OpenGraph.Enums;
using Decision.Common.SEOToolkit.OpenGraph.Media;
using Decision.Common.SEOToolkit.OpenGraph.Structs;
using NTierMvcFramework.Common.Extensions;

namespace Decision.Common.SEOToolkit.OpenGraph.ObjectTypes.Standard
{
    /// <summary>
    ///     This object type represents a TV show, and contains references to the actors and other professionals involved in
    ///     its production. For individual
    ///     episodes of a series, use the video.episode object type. A TV show is defined by us as a series or set of episodes
    ///     that are produced under the
    ///     same title (eg. a television or online series). This object type is part of the Open Graph standard.
    ///     See http://ogp.me/
    ///     See https://developers.facebook.com/docs/reference/opengraph/object-type/video.tv_show/
    /// </summary>
    public class OpenGraphVideoTvShow : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphVideoTvShow" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphVideoTvShow(string title, OpenGraphImage image, string url = null)
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

            if (Actors != null)
            {
                foreach (var actor in Actors)
                {
                    stringBuilder.AppendMetaPropertyContentIfNotNull("video:actor", actor.ActorUrl);
                    stringBuilder.AppendMetaPropertyContentIfNotNull("video:actor:role", actor.Role);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("video:director", DirectorUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:writer", WriterUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:duration", Duration);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:release_date", ReleaseDate);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:tag", Tags);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the actors in the television show.
        /// </summary>
        public IEnumerable<OpenGraphActor> Actors { get; set; }

        /// <summary>
        ///     Gets or sets the URL's to the pages about the directors. This URL's must contain profile meta tags
        ///     <see cref="OpenGraphProfile" />.
        /// </summary>
        public IEnumerable<string> DirectorUrls { get; set; }

        /// <summary>
        ///     Gets or sets the duration of the television show in seconds.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        ///     Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace => "video: http://ogp.me/ns/video#";

        /// <summary>
        ///     Gets or sets the release date of the television show.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the tag words associated with the television show.
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        ///     Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type => OpenGraphType.VideoTvShow;

        /// <summary>
        ///     Gets or sets the URL's to the pages about the writers. This URL's must contain profile meta tags
        ///     <see cref="OpenGraphProfile" />.
        /// </summary>
        public IEnumerable<string> WriterUrls { get; set; }

        #endregion
    }
}